using System;
using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Models.Enums;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Delegations;

/// <summary>Create Delegation modal (SCR-020).</summary>
public class DelegationEditForm : Form
{
    private readonly DelegationService _service = new();

    private ComboBox cboRecipient = null!;
    private ComboBox cboResponsibility = null!;
    private DateTimePicker dtpStart = null!;
    private DateTimePicker dtpEnd = null!;
    private TextBox txtReason = null!;
    private Label lblError = null!;
    private Button btnSave = null!;

    public DelegationEditForm()
    {
        BuildUi();
        Load += (_, _) => LoadRecipients();
    }

    private void BuildUi()
    {
        Text = "New Delegation";
        AutoScaleMode = AutoScaleMode.Font;
        Font = UiTheme.Body;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        BackColor = UiTheme.Surface;

        int y = 20;
        const int lx = 24;
        const int fw = 340;

        void L(string t)
        {
            Controls.Add(new Label { Text = t, Font = UiTheme.Label, Location = new Point(lx, y), AutoSize = true });
            y += 18;
        }

        L("Recipient (Inventory Staff) *");
        cboRecipient = new ComboBox
        {
            Location = new Point(lx, y),
            Width = fw,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = UiTheme.Body
        };
        Controls.Add(cboRecipient);
        y += 32;

        L("Responsibility *");
        cboResponsibility = new ComboBox
        {
            Location = new Point(lx, y),
            Width = fw,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = UiTheme.Body
        };
        cboResponsibility.Items.AddRange(new object[] { "Stock-In", "Stock-Out", "Report Access" });
        cboResponsibility.SelectedIndex = 0;
        Controls.Add(cboResponsibility);
        y += 32;

        L("Start date *");
        dtpStart = new DateTimePicker
        {
            Location = new Point(lx, y),
            Width = 160,
            Format = DateTimePickerFormat.Short,
            Value = DateTime.Today,
            Font = UiTheme.Body
        };
        Controls.Add(dtpStart);
        y += 32;

        L("End date *");
        dtpEnd = new DateTimePicker
        {
            Location = new Point(lx, y),
            Width = 160,
            Format = DateTimePickerFormat.Short,
            Value = DateTime.Today.AddDays(7),
            Font = UiTheme.Body
        };
        Controls.Add(dtpEnd);
        y += 32;

        L("Reason *");
        txtReason = new TextBox
        {
            Location = new Point(lx, y),
            Width = fw,
            Height = 60,
            Multiline = true,
            MaxLength = 500,
            Font = UiTheme.Body,
            ScrollBars = ScrollBars.Vertical
        };
        Controls.Add(txtReason);
        y += 72;

        lblError = new Label
        {
            ForeColor = UiTheme.Error,
            Font = UiTheme.Label,
            Location = new Point(lx, y),
            Size = new Size(fw, 40),
            Visible = false
        };
        Controls.Add(lblError);
        y += 44;

        btnSave = new Button
        {
            Text = "Create",
            Font = UiTheme.Button,
            BackColor = UiTheme.Primary,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Size = new Size(100, 32),
            Location = new Point(lx + fw - 208, y),
            Cursor = Cursors.Hand
        };
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.Click += BtnSave_Click;

        var btnCancel = new Button
        {
            Text = "Cancel",
            Size = new Size(100, 32),
            Location = new Point(lx + fw - 100, y),
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };
        btnCancel.FlatAppearance.BorderColor = UiTheme.Border;
        btnCancel.Click += (_, _) => DialogResult = DialogResult.Cancel;

        AcceptButton = btnSave;
        CancelButton = btnCancel;
        Controls.Add(btnSave);
        Controls.Add(btnCancel);
        ClientSize = new Size(lx * 2 + fw, y + 52);
    }

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

            var id = _service.Create(
                rec.Value,
                resp,
                dtpStart.Value.Date,
                dtpEnd.Value.Date,
                txtReason.Text);

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
