using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>Stock-Out — full-width, no recent grid, actions always visible.</summary>
public partial class StockOutForm : Form
{
    private StockOutService? _service;
    private int _currentOnHand;

    private StockOutService Service => _service ??= new StockOutService();

    public StockOutForm()
    {
        InitializeComponent();

        if (DesignTime.IsActive)
            return;

        ApplyRuntimeStyling();
        Load += (_, _) =>
        {
            LayoutFullWidth();
            LoadLookups();
            UpdatePreview();
        };
        Resize += (_, _) => LayoutFullWidth();
        actionsBar.Resize += (_, _) => LayoutActions();
    }

    private void LayoutFullWidth()
    {
        int w = Math.Max(640, contentHost.ClientSize.Width - 4);
        secProductPanel.Width = w;
        secMovementPanel.Width = w;
        secNotesPanel.Width = w;

        int fieldW = Math.Max(400, w - 40);
        cboProduct.Width = fieldW;
        cboCustomer.Width = fieldW;
        txtNotes.Width = fieldW;
        previewBanner.Width = fieldW;

        LayoutActions();
    }

    private void LayoutActions()
    {
        btnSave.Left = Math.Max(200, actionsBar.ClientSize.Width - btnSave.Width - 4);
        btnClear.Left = btnSave.Left - btnClear.Width - 12;
    }

    private void ApplyRuntimeStyling()
    {
        Font = UiTheme.Body;
        BackColor = UiTheme.Background;
        actionsBar.BackColor = UiTheme.Background;

        foreach (var p in new[] { secProductPanel, secMovementPanel, secNotesPanel })
            p.BackColor = UiTheme.Surface;

        foreach (var sec in new[] { secProduct, secMovement, secNotes })
        {
            sec.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            sec.ForeColor = UiTheme.Secondary;
        }

        foreach (var lbl in new[] { lblProduct, lblCustomer, lblQuantity, lblDate, lblUnitPrice, lblNotes })
        {
            lbl.Font = UiTheme.LabelSemibold;
            lbl.ForeColor = UiTheme.Text;
        }

        helpProduct.ForeColor = UiTheme.TextMuted;
        helpCustomer.ForeColor = UiTheme.TextMuted;
        previewBanner.BackColor = UiTheme.ErrorTint;
        lblPreview.Font = UiTheme.Label;
        lblPreview.ForeColor = UiTheme.Text;
        lblError.Font = UiTheme.Label;
        lblError.ForeColor = UiTheme.Error;

        UiTheme.StyleComboBox(cboProduct);
        UiTheme.StyleComboBox(cboCustomer);
        UiTheme.StyleTextBox(txtUnitPrice);
        UiTheme.StyleTextBox(txtNotes);
        UiTheme.StyleDatePicker(dtpDate);
        UiTheme.StyleButton(btnSave, UiTheme.ButtonKind.Primary);
        UiTheme.StyleButton(btnClear, UiTheme.ButtonKind.Secondary);
        btnClear.ForeColor = UiTheme.Primary;
        btnClear.FlatAppearance.BorderColor = UiTheme.Primary;
        AppIcons.ApplyToButton(btnSave, "icons-stockOut.png", 16);

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

            cboCustomer.Items.Clear();
            cboCustomer.Items.Add(new LookupItem(0, "— None (optional) —"));
            foreach (var c in Service.GetActiveCustomers())
                cboCustomer.Items.Add(new LookupItem(c.CustomerID, c.CustomerName));
            cboCustomer.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            lblError.Text = "Could not load products/customers. " + ErrorPresenter.Classify(DbExceptionMapper.Map(ex)).Message;
            lblError.Visible = true;
        }
    }

    private void BtnClear_Click(object? sender, EventArgs e)
    {
        if (DesignTime.IsActive) return;
        if (cboProduct.Items.Count > 0) cboProduct.SelectedIndex = 0;
        if (cboCustomer.Items.Count > 0) cboCustomer.SelectedIndex = 0;
        nudQuantity.Value = 1;
        dtpDate.Value = DateTime.Today;
        txtUnitPrice.Text = "0.00";
        txtNotes.Clear();
        lblError.Visible = false;
        UpdatePreview();
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
            txtUnitPrice.Text = p.SellingPrice.ToString("0.00", CultureInfo.InvariantCulture);
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
        var neu = _currentOnHand - qty;
        if (neu < 0)
            lblPreview.Text = $"  Not enough stock: on hand {_currentOnHand:N0}, requested {qty:N0}.";
        else
            lblPreview.Text = $"  New on hand: {_currentOnHand:N0}  −  {qty:N0}  =  {neu:N0}";
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        if (DesignTime.IsActive) return;
        lblError.Visible = false;

        try
        {
            if (cboProduct.SelectedItem is not LookupItem prod || prod.Value <= 0)
                throw new ValidationException("Product is required.");

            int? customerId = null;
            string customerName = "(none)";
            if (cboCustomer.SelectedItem is LookupItem cust && cust.Value > 0)
            {
                customerId = cust.Value;
                customerName = cust.Text;
            }

            if (!decimal.TryParse(txtUnitPrice.Text.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out var price)
                && !decimal.TryParse(txtUnitPrice.Text.Trim(), out price))
                throw new ValidationException("Selling price must be a valid number.");

            var qty = (int)nudQuantity.Value;
            var neu = _currentOnHand - qty;
            var productName = prod.Text.Split('—')[0].Trim();

            var confirm = MessageBox.Show(
                FindForm(),
                $"Remove {qty} × {productName}?\n\n" +
                $"Current on hand:  {_currentOnHand}\n" +
                $"Quantity out:     −{qty}\n" +
                $"New on hand:       {neu}\n" +
                $"Customer:          {customerName}",
                "Confirm Stock-Out",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.OK)
                return;

            btnSave.Enabled = false;

            var txnId = Service.Record(
                prod.Value,
                customerId,
                qty,
                dtpDate.Value.Date,
                price,
                txtNotes.Text);

            MessageBox.Show(
                FindForm(),
                $"Stock-Out recorded successfully.\nTransaction ID: {txnId}",
                "Stock-Out",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            BtnClear_Click(null, EventArgs.Empty);
        }
        catch (AppException ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }
        catch (Exception ex)
        {
            lblError.Text = "Could not record Stock-Out. " + ErrorPresenter.Classify(DbExceptionMapper.Map(ex)).Message;
            lblError.Visible = true;
        }
        finally
        {
            btnSave.Enabled = true;
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
