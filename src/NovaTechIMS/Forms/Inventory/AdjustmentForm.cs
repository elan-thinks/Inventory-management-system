using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>
/// Inventory Adjustment — compact fixed layout, action bar INSIDE white card, no scroll/cut.
/// </summary>
public partial class AdjustmentForm : Form
{
    private AdjustmentService? _service;
    private int _previousQty;
    private int _contentRight = 700;

    private const int Pad = 28;

    private AdjustmentService Service => _service ??= new AdjustmentService();

    public AdjustmentForm()
    {
        InitializeComponent();

        if (DesignTime.IsActive)
            return;

        ApplyRuntimeStyling();
        WireCardBorder();

        // Dock order: header top, actions bottom, fields free — keep actions above fields
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

        // Soft top rule on action bar so it reads as part of the card
        actionsBar.Paint += (_, e) =>
        {
            using var pen = new Pen(UiTheme.Border, 1);
            e.Graphics.DrawLine(pen, 0, 0, actionsBar.Width, 0);
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

        // Content block width — balanced, not edge-to-edge empty stretch
        int usable = Math.Max(480, formCard.ClientSize.Width - Pad * 2);
        int blockW = Math.Clamp((int)(usable * 0.70), 460, 620);
        _contentRight = Pad + blockW;

        // Compact fixed vertical rhythm (fits typical MDI host without scroll)
        int y = 72; // under header

        lblProduct.Location = new Point(Pad, y);
        y += 22;
        cboProduct.Location = new Point(Pad, y);
        cboProduct.Width = blockW;
        y += 30;
        helpProduct.Location = new Point(Pad, y);
        y += 28;

        // Qty row
        int qtyW = 110;
        int dateW = 130;
        int col2 = Pad + 140;
        int col3 = col2 + qtyW + 16;
        int col4 = col3 + dateW + 16;

        lblPreviousCaption.Location = new Point(Pad, y);
        lblNewQty.Location = new Point(col2, y);
        lblDate.Location = new Point(col3, y);
        lblDiffCaption.Location = new Point(col4, y);
        y += 22;

        lblPreviousQty.Location = new Point(Pad, y);
        nudNewQty.Location = new Point(col2, y);
        nudNewQty.Width = qtyW;
        dtpDate.Location = new Point(col3, y);
        dtpDate.Width = dateW;
        lblDifference.Location = new Point(col4, y);
        y += 36;

        previewBanner.Location = new Point(Pad, y);
        previewBanner.Size = new Size(blockW, 34);
        y += 44;

        lblReason.Location = new Point(Pad, y);
        y += 22;
        txtReason.Location = new Point(Pad, y);
        txtReason.Width = blockW;
        y += 34;

        lblNotes.Location = new Point(Pad, y);
        y += 20;
        helpNotes.Location = new Point(Pad, y);
        y += 18;

        txtNotes.Location = new Point(Pad, y);
        txtNotes.Width = blockW;

        // Notes height: fixed compact, always above action bar — never cut
        int actionH = Math.Max(56, actionsBar.Height);
        int maxNotesBottom = formCard.ClientSize.Height - actionH - 12;
        int notesH = Math.Max(52, Math.Min(72, maxNotesBottom - y));
        txtNotes.Height = notesH;

        LayoutActions();
        actionsBar.BringToFront();
    }

    private void LayoutActions()
    {
        if (actionsBar is null || btnSave is null || btnClear is null) return;
        int right = Math.Min(_contentRight, actionsBar.ClientSize.Width - Pad);
        btnSave.Top = 10;
        btnClear.Top = 10;
        btnSave.Left = Math.Max(Pad + 160, right - btnSave.Width);
        btnClear.Left = btnSave.Left - btnClear.Width - 10;
        lblError.Left = Pad;
        lblError.Top = 14;
        lblError.Width = Math.Max(100, btnClear.Left - Pad - 8);
    }

    private void ApplyRuntimeStyling()
    {
        Font = UiTheme.Body;
        BackColor = UiTheme.Background;
        formCard.BackColor = UiTheme.Surface;

        // IMPORTANT: action bar stays WHITE so it reads as part of the card
        actionsBar.BackColor = UiTheme.Surface;
        actionsBar.Height = 56;

        lblFormTitle.Font = UiTheme.ScreenTitle;
        lblFormTitle.ForeColor = UiTheme.Text;
        lblFormSub.ForeColor = UiTheme.TextMuted;

        foreach (var lbl in new[] { lblProduct, lblPreviousCaption, lblNewQty, lblDate, lblDiffCaption, lblReason, lblNotes })
        {
            lbl.Font = new Font("Segoe UI", 9.25f, FontStyle.Bold);
            lbl.ForeColor = UiTheme.Text;
        }

        helpProduct.ForeColor = UiTheme.TextMuted;
        helpNotes.ForeColor = UiTheme.TextMuted;
        lblPreviousQty.ForeColor = UiTheme.TextMuted;
        lblPreviousQty.Font = new Font("Segoe UI", 15f, FontStyle.Bold);
        lblDifference.Font = new Font("Segoe UI", 15f, FontStyle.Bold);

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
        // No inner scroll needed for short notes
        txtNotes.ScrollBars = ScrollBars.None;

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
