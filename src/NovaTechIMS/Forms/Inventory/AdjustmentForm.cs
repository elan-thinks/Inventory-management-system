using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>Inventory Adjustment — controls styled directly.</summary>
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
        WireCardBorder();
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
            using var pen = new Pen(Color.FromArgb(228, 232, 236), 1);
            e.Graphics.DrawRectangle(pen, 0, 0, formCard.Width - 1, formCard.Height - 1);
        };

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
        int usable = Math.Max(600, formCard.ClientSize.Width - pad * 2);

        lblProduct.Left = pad;
        cboProduct.Left = pad;
        cboProduct.Width = usable;
        helpProduct.Left = pad;

        lblPreviousCaption.Left = pad;
        lblPreviousQty.Left = pad;

        lblNewQty.Left = pad + 180;
        nudNewQty.Left = pad + 180;
        nudNewQty.Width = 160;

        lblDate.Left = pad + 360;
        dtpDate.Left = pad + 360;
        dtpDate.Width = 180;

        lblDiffCaption.Left = pad + 560;
        lblDifference.Left = pad + 560;

        previewBanner.Left = pad;
        previewBanner.Width = usable;

        lblReason.Left = pad;
        txtReason.Left = pad;
        txtReason.Width = usable;

        lblNotes.Left = pad;
        helpNotes.Left = pad;
        txtNotes.Left = pad;
        txtNotes.Width = usable;
        int notesBottom = formCard.ClientSize.Height - actionsBar.Height - 16;
        txtNotes.Height = Math.Max(60, notesBottom - txtNotes.Top);

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

        cboProduct.FlatStyle = FlatStyle.Standard;
        cboProduct.BackColor = Color.White;
        cboProduct.ForeColor = UiTheme.Text;

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
