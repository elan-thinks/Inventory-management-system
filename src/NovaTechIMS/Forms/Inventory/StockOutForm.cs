using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>Stock-Out — soft field boxes, full panel (matches Stock-In).</summary>
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
        WireSoftFieldBorders();
        Load += (_, _) =>
        {
            LayoutResponsive();
            LoadLookups();
            UpdatePreview();
        };
        formCard.Resize += (_, _) => LayoutResponsive();
        actionsBar.Resize += (_, _) => LayoutActions();
    }

    private void WireSoftFieldBorders()
    {
        formCard.Paint += (_, e) =>
        {
            using var pen = new Pen(Color.FromArgb(228, 232, 236), 1);
            e.Graphics.DrawRectangle(pen, 0, 0, formCard.Width - 1, formCard.Height - 1);
        };

        void SoftField(object? s, PaintEventArgs e)
        {
            if (s is not Panel p) return;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var pen = new Pen(Color.FromArgb(210, 216, 222), 1);
            e.Graphics.DrawRectangle(pen, 0, 0, p.Width - 1, p.Height - 1);
        }

        foreach (var w in new[] { wrapProduct, wrapCustomer, wrapQty, wrapDate, wrapPrice, wrapNotes })
            w.Paint += SoftField;

        previewBanner.Paint += (_, e) =>
        {
            using var pen = new Pen(Color.FromArgb(230, 190, 185), 1);
            e.Graphics.DrawRectangle(pen, 0, 0, previewBanner.Width - 1, previewBanner.Height - 1);
        };
    }

    private void LayoutResponsive()
    {
        if (formCard is null) return;
        int pad = 28;
        int gap = 20;
        int usable = Math.Max(600, formCard.ClientSize.Width - pad * 2);
        int half = (usable - gap) / 2;

        lblProduct.Left = pad;
        wrapProduct.Left = pad;
        wrapProduct.Width = half;
        helpProduct.Left = pad;

        lblCustomer.Left = pad + half + gap;
        wrapCustomer.Left = pad + half + gap;
        wrapCustomer.Width = half;
        helpCustomer.Left = pad + half + gap;

        int third = (usable - gap * 2) / 3;
        lblQuantity.Left = pad;
        wrapQty.Left = pad;
        wrapQty.Width = third;

        lblDate.Left = pad + third + gap;
        wrapDate.Left = pad + third + gap;
        wrapDate.Width = third;

        lblUnitPrice.Left = pad + (third + gap) * 2;
        wrapPrice.Left = pad + (third + gap) * 2;
        wrapPrice.Width = third;

        previewBanner.Left = pad;
        previewBanner.Width = usable;

        lblNotes.Left = pad;
        helpNotes.Left = pad;
        wrapNotes.Left = pad;
        wrapNotes.Width = usable;
        int notesBottom = formCard.ClientSize.Height - actionsBar.Height - 16;
        wrapNotes.Height = Math.Max(80, notesBottom - wrapNotes.Top);

        LayoutActions();
    }

    private void LayoutActions()
    {
        if (actionsBar is null || btnSave is null || btnClear is null) return;
        btnSave.Left = Math.Max(200, actionsBar.ClientSize.Width - btnSave.Width - 28);
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

        cboProduct.FlatStyle = FlatStyle.Flat;
        cboCustomer.FlatStyle = FlatStyle.Flat;
        cboProduct.BackColor = Color.White;
        cboCustomer.BackColor = Color.White;
        txtUnitPrice.BackColor = Color.White;
        txtNotes.BackColor = Color.White;
        nudQuantity.BackColor = Color.White;

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
