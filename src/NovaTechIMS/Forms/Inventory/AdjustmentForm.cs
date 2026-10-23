using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>Inventory Adjustment — soft field boxes, full panel.</summary>
public partial class AdjustmentForm : Form
{
    private AdjustmentService? _service;
    private int _previousQty;

    private AdjustmentService Service => _service ??= new AdjustmentService();

    public AdjustmentForm()
    {
        InitializeComponent();

        if (DesignTime.IsActive)
            return;

        ApplyRuntimeStyling();
        WireSoftFieldBorders();
        Load += (_, _) =>
        {
            LayoutResponsive();
            LoadProducts();
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

        foreach (var w in new[] { wrapProduct, wrapNewQty, wrapDate, wrapReason, wrapNotes })
            w.Paint += SoftField;

        previewBanner.Paint += (_, e) =>
        {
            using var pen = new Pen(Color.FromArgb(200, 215, 230), 1);
            e.Graphics.DrawRectangle(pen, 0, 0, previewBanner.Width - 1, previewBanner.Height - 1);
        };
    }

    private void LayoutResponsive()
    {
        if (formCard is null) return;
        int pad = 28;
        int gap = 20;
        int usable = Math.Max(600, formCard.ClientSize.Width - pad * 2);

        lblProduct.Left = pad;
        wrapProduct.Left = pad;
        wrapProduct.Width = usable;
        helpProduct.Left = pad;

        lblPreviousCaption.Left = pad;
        lblPreviousQty.Left = pad;

        int col2 = pad + 180;
        lblNewQty.Left = col2;
        wrapNewQty.Left = col2;
        wrapNewQty.Width = 180;

        int col3 = col2 + 200;
        lblDate.Left = col3;
        wrapDate.Left = col3;
        wrapDate.Width = 200;

        int col4 = col3 + 220;
        lblDiffCaption.Left = col4;
        lblDifference.Left = col4;

        previewBanner.Left = pad;
        previewBanner.Width = usable;

        lblReason.Left = pad;
        wrapReason.Left = pad;
        wrapReason.Width = usable;

        lblNotes.Left = pad;
        helpNotes.Left = pad;
        wrapNotes.Left = pad;
        wrapNotes.Width = usable;
        int notesBottom = formCard.ClientSize.Height - actionsBar.Height - 16;
        wrapNotes.Height = Math.Max(60, notesBottom - wrapNotes.Top);

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

        foreach (var lbl in new[] { lblProduct, lblPreviousCaption, lblNewQty, lblDate, lblDiffCaption, lblReason, lblNotes })
        {
            lbl.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            lbl.ForeColor = UiTheme.Text;
        }

        helpProduct.ForeColor = UiTheme.TextMuted;
        helpNotes.ForeColor = UiTheme.TextMuted;
        lblPreviousQty.ForeColor = UiTheme.TextMuted;

        previewBanner.BackColor = UiTheme.InfoTint;
        lblPreview.ForeColor = UiTheme.Text;
        lblError.ForeColor = UiTheme.Error;

        cboProduct.FlatStyle = FlatStyle.Flat;
        cboProduct.BackColor = Color.White;
        txtReason.BackColor = Color.White;
        txtNotes.BackColor = Color.White;
        nudNewQty.BackColor = Color.White;

        btnSave.BackColor = UiTheme.Primary;
        btnSave.ForeColor = Color.White;
        btnSave.FlatAppearance.BorderSize = 0;
        btnClear.BackColor = Color.White;
        btnClear.ForeColor = UiTheme.Text;
        btnClear.FlatAppearance.BorderColor = UiTheme.Border;
        btnClear.FlatAppearance.BorderSize = 1;

        AppIcons.ApplyToButton(btnSave, "icons-Inventory-adjust.png", 16);

        dtpDate.MaxDate = DateTime.Today;
        dtpDate.Value = DateTime.Today;
    }

    private void BtnClear_Click(object? sender, EventArgs e)
    {
        if (DesignTime.IsActive) return;
        if (cboProduct.Items.Count > 0) cboProduct.SelectedIndex = 0;
        nudNewQty.Value = 0;
        dtpDate.Value = DateTime.Today;
        txtReason.Clear();
        txtNotes.Clear();
        lblError.Visible = false;
        UpdateDifference();
    }

    private void NudNewQty_ValueChanged(object? sender, EventArgs e)
    {
        if (DesignTime.IsActive) return;
        UpdateDifference();
    }

    private void LoadProducts()
    {
        try
        {
            cboProduct.Items.Clear();
            cboProduct.Items.Add(new LookupItem(0, "— Select product —"));
            foreach (var p in Service.GetActiveProducts())
                cboProduct.Items.Add(new LookupItem(p.ProductID,
                    $"{p.ProductName} — On hand: {p.QuantityOnHand}"));
            cboProduct.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            lblError.Text = "Could not load products. " + ErrorPresenter.Classify(DbExceptionMapper.Map(ex)).Message;
            lblError.Visible = true;
        }
    }

    private void CboProduct_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (DesignTime.IsActive) return;

        if (cboProduct.SelectedItem is not LookupItem item || item.Value <= 0)
        {
            _previousQty = 0;
            lblPreviousQty.Text = "—";
            UpdateDifference();
            return;
        }

        try
        {
            var p = Service.GetProduct(item.Value);
            _previousQty = p.QuantityOnHand;
            lblPreviousQty.Text = _previousQty.ToString(CultureInfo.InvariantCulture);
            nudNewQty.Value = Math.Min(nudNewQty.Maximum, Math.Max(nudNewQty.Minimum, _previousQty));
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
        lblDifference.Text = diff >= 0 ? $"+{diff}" : diff.ToString(CultureInfo.InvariantCulture);
        lblDifference.ForeColor = diff == 0
            ? UiTheme.TextMuted
            : (diff > 0 ? UiTheme.Success : UiTheme.Error);

        if (cboProduct.SelectedItem is not LookupItem item || item.Value <= 0)
            lblPreview.Text = "  Select a product and set the new quantity.";
        else
            lblPreview.Text = $"  Quantity will change from {_previousQty:N0} to {(int)nudNewQty.Value:N0} (diff {lblDifference.Text}).";
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        if (DesignTime.IsActive) return;
        lblError.Visible = false;

        try
        {
            if (cboProduct.SelectedItem is not LookupItem prod || prod.Value <= 0)
                throw new ValidationException("Product is required.");

            var newQty = (int)nudNewQty.Value;
            var productName = prod.Text.Split('—')[0].Trim();
            var diff = newQty - _previousQty;

            var confirm = MessageBox.Show(
                FindForm(),
                $"Adjust {productName}?\n\n" +
                $"Previous on hand: {_previousQty}\n" +
                $"New on hand:      {newQty}\n" +
                $"Difference:       {(diff >= 0 ? "+" : "")}{diff}\n" +
                $"Reason:           {txtReason.Text.Trim()}",
                "Confirm Adjustment",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.OK)
                return;

            btnSave.Enabled = false;

            var txnId = Service.Record(
                prod.Value,
                newQty,
                dtpDate.Value.Date,
                txtReason.Text,
                txtNotes.Text);

            MessageBox.Show(
                FindForm(),
                $"Adjustment recorded successfully.\nTransaction ID: {txnId}",
                "Inventory Adjustment",
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
            lblError.Text = "Could not record adjustment. " + ErrorPresenter.Classify(DbExceptionMapper.Map(ex)).Message;
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
