using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>Stock-Out — compact fields, no stretched dropdowns.</summary>
public partial class StockOutForm : Form
{
    private StockOutService? _service;
    private int _currentOnHand;

    private const int FieldComboW = 300;
    private const int FieldQtyW = 120;
    private const int FieldDateW = 140;
    private const int FieldPriceW = 130;
    private const int ColGap = 24;
    private const int Pad = 28;

    private StockOutService Service => _service ??= new StockOutService();

    public StockOutForm()
    {
        InitializeComponent();

        if (DesignTime.IsActive)
            return;

        ApplyRuntimeStyling();
        WireCardBorder();
        Load += (_, _) =>
        {
            LayoutResponsive();
            LoadLookups();
            UpdatePreview();
        };
        formCard.Resize += (_, _) => LayoutResponsive();
        actionsBar.Resize += (_, _) => LayoutActions();
    }

    private void WireCardBorder()
    {
        formCard.Paint += (_, e) =>
        {
            using var pen = new Pen(Color.FromArgb(228, 232, 236), 1);
            e.Graphics.DrawRectangle(pen, 0, 0, formCard.Width - 1, formCard.Height - 1);
        };

        previewBanner.Paint += (_, e) =>
        {
            using var pen = new Pen(Color.FromArgb(230, 190, 185), 1);
            e.Graphics.DrawRectangle(pen, 0, 0, previewBanner.Width - 1, previewBanner.Height - 1);
        };
    }

    private void LayoutResponsive()
    {
        if (formCard is null) return;

        lblProduct.Left = Pad;
        cboProduct.Left = Pad;
        cboProduct.Width = FieldComboW;
        helpProduct.Left = Pad;

        int col2 = Pad + FieldComboW + ColGap;
        lblCustomer.Left = col2;
        cboCustomer.Left = col2;
        cboCustomer.Width = FieldComboW;
        helpCustomer.Left = col2;

        lblQuantity.Left = Pad;
        nudQuantity.Left = Pad;
        nudQuantity.Width = FieldQtyW;

        int dLeft = Pad + FieldQtyW + ColGap;
        lblDate.Left = dLeft;
        dtpDate.Left = dLeft;
        dtpDate.Width = FieldDateW;

        int pLeft = dLeft + FieldDateW + ColGap;
        lblUnitPrice.Left = pLeft;
        txtUnitPrice.Left = pLeft;
        txtUnitPrice.Width = FieldPriceW;

        int blockW = FieldComboW * 2 + ColGap;
        previewBanner.Left = Pad;
        previewBanner.Width = blockW;

        lblNotes.Left = Pad;
        helpNotes.Left = Pad;
        txtNotes.Left = Pad;
        txtNotes.Width = blockW;
        int notesBottom = formCard.ClientSize.Height - actionsBar.Height - 16;
        txtNotes.Height = Math.Max(72, Math.Min(120, notesBottom - txtNotes.Top));

        LayoutActions();
    }

    private void LayoutActions()
    {
        if (actionsBar is null || btnSave is null || btnClear is null) return;
        btnSave.Left = Math.Max(200, actionsBar.ClientSize.Width - btnSave.Width - Pad);
        btnClear.Left = btnSave.Left - btnClear.Width - 12;
    }

    private void ApplyRuntimeStyling()
    {
        Font = UiTheme.Body;
        BackColor = UiTheme.Background;
        formCard.BackColor = UiTheme.Surface;
        actionsBar.BackColor = UiTheme.Surface;

        lblFormTitle.ForeColor = UiTheme.Text;
        lblFormSub.ForeColor = UiTheme.TextMuted;

        foreach (var lbl in new[] { lblProduct, lblCustomer, lblQuantity, lblDate, lblUnitPrice, lblNotes })
        {
            lbl.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            lbl.ForeColor = UiTheme.Text;
        }

        helpProduct.ForeColor = UiTheme.TextMuted;
        helpCustomer.ForeColor = UiTheme.TextMuted;
        helpNotes.ForeColor = UiTheme.TextMuted;

        previewBanner.BackColor = UiTheme.ErrorTint;
        lblPreview.ForeColor = UiTheme.Text;
        lblError.ForeColor = UiTheme.Error;

        foreach (var cbo in new[] { cboProduct, cboCustomer })
        {
            cbo.FlatStyle = FlatStyle.Standard;
            cbo.BackColor = Color.White;
            cbo.ForeColor = UiTheme.Text;
            cbo.DropDownWidth = 360;
        }

        nudQuantity.BorderStyle = BorderStyle.FixedSingle;
        nudQuantity.BackColor = Color.White;
        txtUnitPrice.BorderStyle = BorderStyle.FixedSingle;
        txtUnitPrice.BackColor = Color.White;
        txtNotes.BorderStyle = BorderStyle.FixedSingle;
        txtNotes.BackColor = Color.White;

        btnSave.BackColor = UiTheme.Primary;
        btnSave.ForeColor = Color.White;
        btnSave.FlatAppearance.BorderSize = 0;
        btnClear.BackColor = Color.White;
        btnClear.ForeColor = UiTheme.Text;
        btnClear.FlatAppearance.BorderColor = UiTheme.Border;
        btnClear.FlatAppearance.BorderSize = 1;

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
