using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>Stock-In (FR-SIN) — designer owns layout; this file is data + save only.</summary>
public partial class StockInForm : Form
{
    private StockInService? _service;
    private int _currentOnHand;

    private StockInService Service => _service ??= new StockInService();

    public StockInForm()
    {
        InitializeComponent();

        if (DesignTime.IsActive)
            return;

        grid.Columns.Clear();
        grid.ColumnCount = 0;

        ApplyRuntimeStyling();
        Load += (_, _) =>
        {
            LoadLookups();
            ReloadRecent();
            UpdatePreview();
        };
    }

    private void ApplyRuntimeStyling()
    {
        Font = UiTheme.Body;
        BackColor = UiTheme.Background;
        formPanel.BackColor = UiTheme.Surface;
        formPanel.Paint += (_, e) =>
        {
            using var pen = new Pen(UiTheme.Border, 1);
            e.Graphics.DrawRectangle(pen, 0, 0, formPanel.Width - 1, formPanel.Height - 1);
        };

        foreach (var sec in new[] { secProduct, secMovement, secNotes })
        {
            sec.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            sec.ForeColor = UiTheme.Secondary;
        }

        foreach (Control c in formPanel.Controls)
        {
            if (c is Label lbl && lbl != lblError && lbl != lblPreview
                && lbl != secProduct && lbl != secMovement && lbl != secNotes
                && lbl != helpProduct && lbl != helpSupplier)
            {
                lbl.Font = UiTheme.Label;
                lbl.ForeColor = UiTheme.Text;
            }
        }

        helpProduct.ForeColor = UiTheme.TextMuted;
        helpSupplier.ForeColor = UiTheme.TextMuted;
        previewBanner.BackColor = UiTheme.InfoTint;
        lblPreview.Font = UiTheme.Label;
        lblPreview.ForeColor = UiTheme.Text;
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
            foreach (var p in Service.GetActiveProducts())
                cboProduct.Items.Add(new LookupItem(p.ProductID,
                    $"{p.ProductName} — On hand: {p.QuantityOnHand}"));
            cboProduct.SelectedIndex = 0;

            cboSupplier.Items.Clear();
            cboSupplier.Items.Add(new LookupItem(0, "— Select supplier —"));
            foreach (var s in Service.GetActiveSuppliers())
                cboSupplier.Items.Add(new LookupItem(s.SupplierID, s.SupplierName));
            cboSupplier.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            lblError.Text = "Could not load products/suppliers. " + ErrorPresenter.Classify(DbExceptionMapper.Map(ex)).Message;
            lblError.Visible = true;
        }
    }

    private void CboProduct_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (DesignTime.IsActive) return;

        if (cboProduct.SelectedItem is not LookupItem item || item.Value <= 0)
        {
            _currentOnHand = 0;
            lblCurrentQty.Text = "—";
            UpdatePreview();
            return;
        }

        try
        {
            var p = Service.GetProduct(item.Value);
            _currentOnHand = p.QuantityOnHand;
            lblCurrentQty.Text = _currentOnHand.ToString(CultureInfo.InvariantCulture);
            txtUnitPrice.Text = p.PurchasePrice.ToString("0.00", CultureInfo.InvariantCulture);

            for (var i = 0; i < cboSupplier.Items.Count; i++)
            {
                if (cboSupplier.Items[i] is LookupItem s && s.Value == p.SupplierID)
                {
                    cboSupplier.SelectedIndex = i;
                    break;
                }
            }

            UpdatePreview();
        }
        catch
        {
            _currentOnHand = 0;
            lblCurrentQty.Text = "—";
            UpdatePreview();
        }
    }

    private void NudQuantity_ValueChanged(object? sender, EventArgs e)
    {
        if (DesignTime.IsActive) return;
        UpdatePreview();
    }

    private void UpdatePreview()
    {
        if (cboProduct.SelectedItem is not LookupItem item || item.Value <= 0)
        {
            lblPreview.Text = "  Select a product to see the new quantity on hand.";
            return;
        }

        var qty = (int)nudQuantity.Value;
        var neu = _currentOnHand + qty;
        lblPreview.Text = $"  New quantity on hand will be: {_currentOnHand} + {qty} = {neu}";
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        if (DesignTime.IsActive) return;
        lblError.Visible = false;

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
            var productName = prod.Text;
            var supplierName = sup.Text;
            var neu = _currentOnHand + qty;

            var confirm = MessageBox.Show(
                FindForm(),
                $"Add {qty} × {productName.Split('—')[0].Trim()} from {supplierName}?\n\n" +
                $"Current on hand:  {_currentOnHand}\n" +
                $"Quantity received: +{qty}\n" +
                $"New on hand:       {neu}",
                "Confirm Stock-In",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.OK)
                return;

            btnSave.Enabled = false;

            var txnId = Service.Record(
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
            lblError.Text = "Could not record Stock-In. " + ErrorPresenter.Classify(DbExceptionMapper.Map(ex)).Message;
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
            var rows = Service.GetRecent(50);
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
            Show(nameof(StockInListRow.ProductName), "Product", 1.4f);
            Show(nameof(StockInListRow.Quantity), "Qty", 0.5f);
            Show(nameof(StockInListRow.TransactionDate), "Date", 0.7f);
            Show(nameof(StockInListRow.UnitPrice), "Price", 0.6f);
            Show(nameof(StockInListRow.SupplierName), "Supplier", 1.0f);
            Show(nameof(StockInListRow.UserFullName), "User", 1.0f);
            Show(nameof(StockInListRow.PreviousQuantity), "Prev", 0.5f);
            Show(nameof(StockInListRow.NewQuantity), "New", 0.5f);
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
