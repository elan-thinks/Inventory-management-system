using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>Inventory Adjustment — full-width, no recent grid, pinned actions.</summary>
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
        Load += (_, _) =>
        {
            LayoutFullWidth();
            LoadProducts();
        };
        Resize += (_, _) => LayoutFullWidth();
        actionsBar.Resize += (_, _) => LayoutActions();
    }

    private void LayoutFullWidth()
    {
        int w = Math.Max(640, contentHost.ClientSize.Width - 4);
        secProductPanel.Width = w;
        secMovementPanel.Width = w;
        secReasonPanel.Width = w;

        int fieldW = Math.Max(400, w - 40);
        cboProduct.Width = fieldW;
        txtReason.Width = fieldW;
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

        foreach (var p in new[] { secProductPanel, secMovementPanel, secReasonPanel })
            p.BackColor = UiTheme.Surface;

        foreach (var sec in new[] { secProduct, secMovement, secReason })
        {
            sec.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            sec.ForeColor = UiTheme.Primary;
        }

        foreach (var lbl in new[] { lblProduct, lblPreviousCaption, lblNewQty, lblDate, lblDiffCaption, lblReason, lblNotes })
        {
            lbl.Font = UiTheme.LabelSemibold;
            lbl.ForeColor = UiTheme.Text;
        }

        helpProduct.ForeColor = UiTheme.TextMuted;
        previewBanner.BackColor = UiTheme.InfoTint;
        lblPreview.Font = UiTheme.Label;
        lblPreview.ForeColor = UiTheme.Text;
        lblPreviousQty.ForeColor = UiTheme.TextMuted;
        lblError.Font = UiTheme.Label;
        lblError.ForeColor = UiTheme.Error;

        UiTheme.StyleComboBox(cboProduct);
        UiTheme.StyleTextBox(txtReason);
        UiTheme.StyleTextBox(txtNotes);
        UiTheme.StyleDatePicker(dtpDate);
        nudNewQty.BackColor = UiTheme.Surface;
        nudNewQty.ForeColor = UiTheme.Text;

        UiTheme.StyleButton(btnSave, UiTheme.ButtonKind.Primary);
        UiTheme.StyleButton(btnClear, UiTheme.ButtonKind.Secondary);
        btnClear.ForeColor = UiTheme.Primary;
        btnClear.FlatAppearance.BorderColor = UiTheme.Primary;
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
