using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>Inventory Adjustment — fixed action bar, no overlap with notes.</summary>
public partial class AdjustmentForm : Form
{
    private AdjustmentService? _service;
    private int _previousQty;
    private int _contentRight = 700;

    private const int Pad = 32;
    private const int Gap = 20;

    private AdjustmentService Service => _service ??= new AdjustmentService();

    public AdjustmentForm()
    {
        InitializeComponent();

        if (DesignTime.IsActive)
            return;

        ApplyRuntimeStyling();
        WireCardBorder();

        // Keep action bar on top of free-positioned fields so notes never cover buttons
        actionsBar.BringToFront();

        Load += (_, _) =>
        {
            LayoutResponsive();
            LoadProducts();
        };
        formCard.Resize += (_, _) => LayoutResponsive();
        actionsBar.Resize += (_, _) => LayoutActions();
    }

    private void WireCardBorder()
    {
        formCard.Paint += (_, e) =>
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var pen = new Pen(UiTheme.Border, 1);
            var r = new Rectangle(0, 0, formCard.Width - 1, formCard.Height - 1);
            using var path = UiTheme.RoundedRect(r, UiTheme.RadiusMd);
            e.Graphics.DrawPath(pen, path);
        };

        previewBanner.Paint += (_, e) =>
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var pen = new Pen(Color.FromArgb(200, 215, 230), 1);
            var r = new Rectangle(0, 0, previewBanner.Width - 1, previewBanner.Height - 1);
            using var path = UiTheme.RoundedRect(r, UiTheme.RadiusSm);
            e.Graphics.DrawPath(pen, path);
        };
    }

    private void LayoutResponsive()
    {
        if (formCard is null || actionsBar is null) return;

        // Content width ~72% of card, max 640 — balanced, not stretched
        int usable = Math.Max(520, formCard.ClientSize.Width - Pad * 2);
        int blockW = Math.Clamp((int)(usable * 0.72), 480, 640);
        _contentRight = Pad + blockW;

        // Product
        lblProduct.Left = Pad;
        cboProduct.Left = Pad;
        cboProduct.Width = blockW;
        helpProduct.Left = Pad;

        // Qty row: Current | New | Date | Difference
        int qtyW = 120;
        int dateW = 140;

        lblPreviousCaption.Left = Pad;
        lblPreviousQty.Left = Pad;

        lblNewQty.Left = Pad + 150;
        nudNewQty.Left = Pad + 150;
        nudNewQty.Width = qtyW;

        lblDate.Left = Pad + 150 + qtyW + Gap;
        dtpDate.Left = Pad + 150 + qtyW + Gap;
        dtpDate.Width = dateW;

        lblDiffCaption.Left = Pad + 150 + qtyW + Gap + dateW + Gap;
        lblDifference.Left = lblDiffCaption.Left;

        previewBanner.Left = Pad;
        previewBanner.Width = blockW;

        lblReason.Left = Pad;
        txtReason.Left = Pad;
        txtReason.Width = blockW;

        lblNotes.Left = Pad;
        helpNotes.Left = Pad;
        txtNotes.Left = Pad;
        txtNotes.Width = blockW;

        // Hard stop: notes must end above the docked action bar (never overlap)
        int actionTop = formCard.ClientSize.Height - actionsBar.Height;
        int notesMaxBottom = actionTop - 16;
        int notesDesired = Math.Min(100, Math.Max(64, notesMaxBottom - txtNotes.Top));
        if (txtNotes.Top + notesDesired > notesMaxBottom)
            notesDesired = Math.Max(48, notesMaxBottom - txtNotes.Top);
        txtNotes.Height = notesDesired;

        LayoutActions();
        actionsBar.BringToFront();
    }

    private void LayoutActions()
    {
        if (actionsBar is null || btnSave is null || btnClear is null) return;
        int right = Math.Min(_contentRight, actionsBar.ClientSize.Width - Pad);
        btnSave.Left = Math.Max(Pad + 180, right - btnSave.Width);
        btnSave.Top = 12;
        btnClear.Left = btnSave.Left - btnClear.Width - 12;
        btnClear.Top = 12;
        lblError.Left = Pad;
        lblError.Top = 16;
        lblError.Width = Math.Max(120, btnClear.Left - Pad - 12);
    }

    private void ApplyRuntimeStyling()
    {
        Font = UiTheme.Body;
        BackColor = UiTheme.Background;
        formCard.BackColor = UiTheme.Surface;
        actionsBar.BackColor = UiTheme.Background; // subtle bar separation

        lblFormTitle.Font = UiTheme.ScreenTitle;
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
        lblPreviousQty.Font = new Font("Segoe UI", 16f, FontStyle.Bold);
        lblDifference.Font = new Font("Segoe UI", 16f, FontStyle.Bold);

        previewBanner.BackColor = UiTheme.InfoTint;
        lblPreview.ForeColor = UiTheme.Text;
        lblError.ForeColor = UiTheme.Error;

        cboProduct.FlatStyle = FlatStyle.Standard;
        cboProduct.BackColor = Color.White;
        cboProduct.ForeColor = UiTheme.Text;
        cboProduct.DropDownWidth = 480;

        nudNewQty.BorderStyle = BorderStyle.FixedSingle;
        nudNewQty.BackColor = Color.White;
        txtReason.BorderStyle = BorderStyle.FixedSingle;
        txtReason.BackColor = Color.White;
        txtNotes.BorderStyle = BorderStyle.FixedSingle;
        txtNotes.BackColor = Color.White;

        btnSave.BackColor = UiTheme.Primary;
        btnSave.ForeColor = Color.White;
        btnSave.FlatAppearance.BorderSize = 0;
        btnClear.BackColor = Color.White;
        btnClear.ForeColor = UiTheme.Text;
        btnClear.FlatAppearance.BorderColor = UiTheme.Border;
        btnClear.FlatAppearance.BorderSize = 1;

        UiTheme.ApplyRoundedRegion(btnSave, UiTheme.RadiusSm);
        UiTheme.ApplyRoundedRegion(btnClear, UiTheme.RadiusSm);

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

            if (!AppDialog.Confirm(
                FindForm(),
                $"Adjust {productName}?\n\n" +
                $"Previous on hand: {_previousQty}\n" +
                $"New on hand:      {newQty}\n" +
                $"Difference:       {(diff >= 0 ? "+" : "")}{diff}\n" +
                $"Reason:           {txtReason.Text.Trim()}",
                "Confirm Adjustment"))
                return;

            btnSave.Enabled = false;

            var txnId = Service.Record(
                prod.Value,
                newQty,
                dtpDate.Value.Date,
                txtReason.Text,
                txtNotes.Text);

            AppDialog.Info(
                FindForm(),
                $"Adjustment recorded successfully.\nTransaction ID: {txnId}",
                "Inventory Adjustment");

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
