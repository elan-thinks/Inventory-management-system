using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>Inventory Adjustment (FR-ADJ) — Designer-based (partial).</summary>
public partial class AdjustmentForm : Form
{
    private readonly AdjustmentService _service = new();
    private int _previousQty;

    public AdjustmentForm()
    {
        InitializeComponent();
        ApplyRuntimeStyling();
        Load += (_, _) =>
        {
            LoadProducts();
            ReloadRecent();
        };
    }

    private void ApplyRuntimeStyling()
    {
        Font = UiTheme.Body;
        BackColor = UiTheme.Background;
        formPanel.BackColor = UiTheme.Surface;
        foreach (Control c in formPanel.Controls)
        {
            if (c is Label lbl && lbl != lblError && lbl != lblPreviousQty && lbl != lblDifference)
            {
                lbl.Font = UiTheme.Label;
                lbl.ForeColor = UiTheme.Text;
            }
        }
        lblPreviousQty.Font = UiTheme.Body;
        lblPreviousQty.ForeColor = UiTheme.TextMuted;
        lblDifference.Font = UiTheme.Body;
        lblError.Font = UiTheme.Label;
        lblError.ForeColor = UiTheme.Error;
        lblHint.Font = UiTheme.Label;
        lblHint.ForeColor = UiTheme.TextMuted;
        UiTheme.StyleComboBox(cboProduct);
        UiTheme.StyleTextBox(txtReason);
        UiTheme.StyleTextBox(txtNotes);
        UiTheme.StyleDatePicker(dtpDate);
        UiTheme.StyleButton(btnSave, UiTheme.ButtonKind.Primary);
        UiTheme.ApplyGridTheme(grid);
        dtpDate.MaxDate = DateTime.Today;
        dtpDate.Value = DateTime.Today;
    }

    private void NudNewQty_ValueChanged(object? sender, EventArgs e) => UpdateDifference();

    private void LoadProducts()
    {
        try
        {
            cboProduct.Items.Clear();
            cboProduct.Items.Add(new LookupItem(0, "— Select product —"));
            foreach (var p in _service.GetActiveProducts())
                cboProduct.Items.Add(new LookupItem(p.ProductID, $"{p.ProductName} (ID {p.ProductID})"));
            cboProduct.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            lblError.Text = "Could not load products. " + ex.Message;
            lblError.Visible = true;
        }
    }

    private void CboProduct_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cboProduct.SelectedItem is not LookupItem item || item.Value <= 0)
        {
            _previousQty = 0;
            lblPreviousQty.Text = "—";
            UpdateDifference();
            return;
        }

        try
        {
            var p = _service.GetProduct(item.Value);
            _previousQty = p.QuantityOnHand;
            lblPreviousQty.Text = _previousQty.ToString(CultureInfo.InvariantCulture);
            nudNewQty.Value = _previousQty;
            UpdateDifference();
        }
        catch
        {
            lblPreviousQty.Text = "—";
        }
    }

    private void UpdateDifference()
    {
        var diff = (int)nudNewQty.Value - _previousQty;
        lblDifference.Text = diff.ToString(CultureInfo.InvariantCulture);
        lblDifference.ForeColor = diff == 0
            ? UiTheme.TextMuted
            : (diff > 0 ? Color.DarkGreen : UiTheme.Error);
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        lblError.Visible = false;
        btnSave.Enabled = false;

        try
        {
            if (cboProduct.SelectedItem is not LookupItem prod || prod.Value <= 0)
                throw new ValidationException("Product is required.");

            var txnId = _service.Record(
                prod.Value,
                (int)nudNewQty.Value,
                dtpDate.Value.Date,
                txtReason.Text,
                txtNotes.Text);

            MessageBox.Show(
                FindForm(),
                $"Adjustment recorded successfully.\nTransaction ID: {txnId}",
                "Inventory Adjustment",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            txtReason.Clear();
            txtNotes.Clear();
            CboProduct_SelectedIndexChanged(null, EventArgs.Empty);
            ReloadRecent();
        }
        catch (AppException ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }
        catch (Exception ex)
        {
            lblError.Text = "Could not record adjustment. " + ex.Message;
            lblError.Visible = true;
        }
        finally
        {
            btnSave.Enabled = true;
        }
    }

    private void ReloadRecent()
    {
        try
        {
            var rows = _service.GetRecentAdjustments(30);
            grid.DataSource = null;
            grid.Columns.Clear();
            grid.DataSource = rows;

            foreach (DataGridViewColumn col in grid.Columns)
                col.Visible = false;

            void Show(string name, string header, float w = 1f)
            {
                if (grid.Columns[name] is not DataGridViewColumn c) return;
                c.Visible = true;
                c.HeaderText = header;
                c.FillWeight = w;
            }

            Show(nameof(TransactionHistoryRow.TransactionID), "ID", 0.4f);
            Show(nameof(TransactionHistoryRow.TransactionDate), "Date", 0.7f);
            Show(nameof(TransactionHistoryRow.ProductName), "Product", 1.3f);
            Show(nameof(TransactionHistoryRow.PreviousQuantity), "Prev", 0.5f);
            Show(nameof(TransactionHistoryRow.NewQuantity), "New", 0.5f);
            Show(nameof(TransactionHistoryRow.Difference), "Diff", 0.5f);
            Show(nameof(TransactionHistoryRow.Reason), "Reason", 1.4f);
            Show(nameof(TransactionHistoryRow.UserFullName), "User", 1.0f);
        }
        catch { }
    }

    private sealed class LookupItem
    {
        public int Value { get; }
        public string Text { get; }
        public LookupItem(int value, string text) { Value = value; Text = text; }
        public override string ToString() => Text;
    }
}
