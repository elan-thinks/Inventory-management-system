using System;
using System.Globalization;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>
/// Stock-In screen (FR-SIN) — Designer-based (partial).
/// </summary>
public partial class StockInForm : Form
{
    private readonly StockInService _service = new();

    public StockInForm()
    {
        InitializeComponent();
        ApplyRuntimeStyling();
        Load += (_, _) =>
        {
            LoadLookups();
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
            if (c is Label lbl && lbl != lblError && lbl != lblCurrentQty)
            {
                lbl.Font = UiTheme.Label;
                lbl.ForeColor = UiTheme.Text;
            }
        }
        lblCurrentQty.Font = UiTheme.Body;
        lblCurrentQty.ForeColor = UiTheme.TextMuted;
        lblError.Font = UiTheme.Label;
        lblError.ForeColor = UiTheme.Error;
        lblHint.Font = UiTheme.Label;
        lblHint.ForeColor = UiTheme.TextMuted;
        UiTheme.StyleComboBox(cboProduct);
        UiTheme.StyleComboBox(cboSupplier);
        UiTheme.StyleTextBox(txtUnitPrice);
        UiTheme.StyleTextBox(txtNotes);
        UiTheme.StyleDatePicker(dtpDate);
        UiTheme.StyleButton(btnSave, UiTheme.ButtonKind.Primary);
        UiTheme.ApplyGridTheme(grid);
        dtpDate.MaxDate = DateTime.Today;
        dtpDate.Value = DateTime.Today;
    }

    private void LoadLookups()
    {
        try
        {
            cboProduct.Items.Clear();
            cboProduct.Items.Add(new LookupItem(0, "— Select product —"));
            foreach (var p in _service.GetActiveProducts())
                cboProduct.Items.Add(new LookupItem(p.ProductID, $"{p.ProductName} (ID {p.ProductID})"));
            cboProduct.SelectedIndex = 0;

            cboSupplier.Items.Clear();
            cboSupplier.Items.Add(new LookupItem(0, "— Select supplier —"));
            foreach (var s in _service.GetActiveSuppliers())
                cboSupplier.Items.Add(new LookupItem(s.SupplierID, s.SupplierName));
            cboSupplier.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            lblError.Text = "Could not load products/suppliers. " + ex.Message;
            lblError.Visible = true;
        }
    }

    private void CboProduct_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cboProduct.SelectedItem is not LookupItem item || item.Value <= 0)
        {
            lblCurrentQty.Text = "—";
            return;
        }

        try
        {
            var p = _service.GetProduct(item.Value);
            lblCurrentQty.Text = p.QuantityOnHand.ToString(CultureInfo.InvariantCulture);
            txtUnitPrice.Text = p.PurchasePrice.ToString("0.00", CultureInfo.InvariantCulture);

            for (var i = 0; i < cboSupplier.Items.Count; i++)
            {
                if (cboSupplier.Items[i] is LookupItem s && s.Value == p.SupplierID)
                {
                    cboSupplier.SelectedIndex = i;
                    break;
                }
            }
        }
        catch
        {
            lblCurrentQty.Text = "—";
        }
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        lblError.Visible = false;
        btnSave.Enabled = false;

        try
        {
            if (cboProduct.SelectedItem is not LookupItem prod || prod.Value <= 0)
                throw new ValidationException("Product is required.");

            if (cboSupplier.SelectedItem is not LookupItem sup || sup.Value <= 0)
                throw new ValidationException("Supplier is required.");

            if (!decimal.TryParse(txtUnitPrice.Text.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out var price)
                && !decimal.TryParse(txtUnitPrice.Text.Trim(), out price))
                throw new ValidationException("Purchase price must be a valid number.");

            var qty = (int)nudQuantity.Value;
            var txnId = _service.Record(
                prod.Value,
                sup.Value,
                qty,
                dtpDate.Value.Date,
                price,
                txtNotes.Text);

            MessageBox.Show(
                FindForm(),
                $"Stock-In recorded successfully.\nTransaction ID: {txnId}",
                "Stock-In",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            nudQuantity.Value = 1;
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
            lblError.Text = "Could not record Stock-In. " + ex.Message;
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
            var rows = _service.GetRecent(50);
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

            Show(nameof(StockInListRow.TransactionID), "Txn ID", 0.5f);
            Show(nameof(StockInListRow.TransactionDate), "Date", 0.7f);
            Show(nameof(StockInListRow.ProductName), "Product", 1.4f);
            Show(nameof(StockInListRow.Quantity), "Qty", 0.5f);
            Show(nameof(StockInListRow.UnitPrice), "Price", 0.6f);
            Show(nameof(StockInListRow.SupplierName), "Supplier", 1.0f);
            Show(nameof(StockInListRow.PreviousQuantity), "Prev", 0.5f);
            Show(nameof(StockInListRow.NewQuantity), "New", 0.5f);
            Show(nameof(StockInListRow.UserFullName), "User", 1.0f);
        }
        catch
        {
        }
    }

    private sealed class LookupItem
    {
        public int Value { get; }
        public string Text { get; }
        public LookupItem(int value, string text) { Value = value; Text = text; }
        public override string ToString() => Text;
    }
}
