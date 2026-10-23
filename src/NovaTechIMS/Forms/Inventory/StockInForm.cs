using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>Stock-In — full panel, no inner scroll, mockup field layout.</summary>
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

        ApplyRuntimeStyling();
        WireSoftBorders();
        Load += (_, _) =>
        {
            LayoutActions();
            LoadLookups();
            UpdatePreview();
        };
        actionsBar.Resize += (_, _) => LayoutActions();
        formCard.Resize += (_, _) => LayoutActions();
    }

    private void WireSoftBorders()
    {
        formCard.Paint += (_, e) =>
        {
            using var pen = new Pen(Color.FromArgb(228, 232, 236), 1);
            e.Graphics.DrawRectangle(pen, 0, 0, formCard.Width - 1, formCard.Height - 1);
        };

        previewBanner.Paint += (_, e) =>
        {
            using var pen = new Pen(Color.FromArgb(200, 230, 210), 1);
            e.Graphics.DrawRectangle(pen, 0, 0, previewBanner.Width - 1, previewBanner.Height - 1);
        };
    }

    private void LayoutActions()
    {
        if (actionsBar is null || btnSave is null || btnClear is null) return;
        btnSave.Left = Math.Max(200, actionsBar.ClientSize.Width - btnSave.Width - 4);
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

        foreach (var lbl in new[] { lblProduct, lblSupplier, lblQuantity, lblDate, lblUnitPrice, lblNotes })
        {
            lbl.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            lbl.ForeColor = UiTheme.Text;
        }

        helpProduct.ForeColor = UiTheme.TextMuted;
        helpSupplier.ForeColor = UiTheme.TextMuted;
        helpNotes.ForeColor = UiTheme.TextMuted;

        previewBanner.BackColor = UiTheme.SuccessTint;
        lblPreview.Font = UiTheme.Label;
        lblPreview.ForeColor = UiTheme.Text;
        lblError.Font = UiTheme.Label;
        lblError.ForeColor = UiTheme.Error;

        UiTheme.StyleComboBox(cboProduct);
        UiTheme.StyleComboBox(cboSupplier);
        UiTheme.StyleTextBox(txtUnitPrice);
        UiTheme.StyleTextBox(txtNotes);
        UiTheme.StyleDatePicker(dtpDate);

        // Taller, softer inputs
        cboProduct.Height = 32;
        cboSupplier.Height = 32;
        txtUnitPrice.Height = 32;

        btnSave.BackColor = UiTheme.Primary;
        btnSave.ForeColor = Color.White;
        btnSave.FlatAppearance.BorderSize = 0;
        btnClear.BackColor = Color.White;
        btnClear.ForeColor = UiTheme.Text;
        btnClear.FlatAppearance.BorderColor = UiTheme.Border;
        btnClear.FlatAppearance.BorderSize = 1;

        AppIcons.ApplyToButton(btnSave, "icons-stockIn.png", 16);

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

    private void BtnClear_Click(object? sender, EventArgs e)
    {
        if (DesignTime.IsActive) return;
        if (cboProduct.Items.Count > 0) cboProduct.SelectedIndex = 0;
        if (cboSupplier.Items.Count > 0) cboSupplier.SelectedIndex = 0;
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
        lblPreview.Text = $"  New on hand: {_currentOnHand:N0}  +  {qty:N0}  =  {neu:N0}";
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

            BtnClear_Click(null, EventArgs.Empty);
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

    private sealed class LookupItem
    {
        public int Value { get; }
        public string Text { get; }
        public LookupItem(int value, string text) { Value = value; Text = text; }
        public override string ToString() => Text;
    }
}
