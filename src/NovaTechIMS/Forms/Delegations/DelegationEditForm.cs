using System;
using System.Windows.Forms;
using NovaTechIMS.Models.Enums;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Delegations;

/// <summary>Create Delegation modal — Designer-based (partial).</summary>
public partial class DelegationEditForm : Form
{
    private readonly DelegationService _service = new();

    public DelegationEditForm()
    {
        InitializeComponent();
        ApplyRuntimeStyling();
        Load += (_, _) => LoadRecipients();
    }

    private void ApplyRuntimeStyling()
    {
        Font = UiTheme.Body;
        BackColor = UiTheme.Surface;
        foreach (var lbl in new[] { lblRecipient, lblResponsibility, lblStart, lblEnd, lblReason })
        {
            lbl.Font = UiTheme.Label;
            lbl.ForeColor = UiTheme.Text;
        }
        lblError.Font = UiTheme.ErrorText;
        lblError.ForeColor = UiTheme.Error;
        UiTheme.StyleComboBox(cboRecipient);
        UiTheme.StyleComboBox(cboResponsibility);
        UiTheme.StyleDatePicker(dtpStart);
        UiTheme.StyleDatePicker(dtpEnd);
        UiTheme.StyleTextBox(txtReason);
        UiTheme.StyleButton(btnSave, UiTheme.ButtonKind.Primary);
        UiTheme.StyleButton(btnCancel, UiTheme.ButtonKind.Secondary);
        dtpStart.Value = DateTime.Today;
        dtpEnd.Value = DateTime.Today.AddDays(7);
    }

    private void BtnCancel_Click(object? sender, EventArgs e) => DialogResult = DialogResult.Cancel;

    private void LoadRecipients()
    {
        try
        {
            cboRecipient.Items.Clear();
            cboRecipient.Items.Add(new LookupItem(0, "— Select staff user —"));
            foreach (var u in _service.GetEligibleRecipients())
                cboRecipient.Items.Add(new LookupItem(u.UserID, $"{u.FullName} ({u.Username})"));
            cboRecipient.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        lblError.Visible = false;
        btnSave.Enabled = false;
        try
        {
            if (cboRecipient.SelectedItem is not LookupItem rec || rec.Value <= 0)
                throw new ValidationException("Recipient is required.");

            var resp = cboResponsibility.SelectedIndex switch
            {
                0 => DelegatableResponsibility.StockIn,
                1 => DelegatableResponsibility.StockOut,
                _ => DelegatableResponsibility.ReportAccess
            };

            var id = _service.Create(rec.Value, resp, dtpStart.Value.Date, dtpEnd.Value.Date, txtReason.Text);
            MessageBox.Show(this, $"Delegation created (ID {id}).", "Delegations",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
        }
        catch (AppException ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }
        catch (Exception ex)
        {
            lblError.Text = "Could not create delegation. " + ex.Message;
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
