using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Delegations;

partial class DelegationEditForm
{
    private System.ComponentModel.IContainer components = null;
    private Label lblRecipient;
    private ComboBox cboRecipient;
    private Label lblResponsibility;
    private ComboBox cboResponsibility;
    private Label lblStart;
    private DateTimePicker dtpStart;
    private Label lblEnd;
    private DateTimePicker dtpEnd;
    private Label lblReason;
    private TextBox txtReason;
    private Label lblError;
    private Button btnSave;
    private Button btnCancel;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        lblRecipient = new Label();
        cboRecipient = new ComboBox();
        lblResponsibility = new Label();
        cboResponsibility = new ComboBox();
        lblStart = new Label();
        dtpStart = new DateTimePicker();
        lblEnd = new Label();
        dtpEnd = new DateTimePicker();
        lblReason = new Label();
        txtReason = new TextBox();
        lblError = new Label();
        btnSave = new Button();
        btnCancel = new Button();
        SuspendLayout();

        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        BackColor = Color.White;
        ClientSize = new Size(388, 420);
        Name = "DelegationEditForm";
        Text = "New Delegation";

        lblRecipient.AutoSize = true;
        lblRecipient.Location = new Point(24, 20);
        lblRecipient.Name = "lblRecipient";
        lblRecipient.Text = "Recipient (Inventory Staff) *";

        cboRecipient.DropDownStyle = ComboBoxStyle.DropDownList;
        cboRecipient.Location = new Point(24, 38);
        cboRecipient.Name = "cboRecipient";
        cboRecipient.Size = new Size(340, 25);
        cboRecipient.TabIndex = 0;

        lblResponsibility.AutoSize = true;
        lblResponsibility.Location = new Point(24, 72);
        lblResponsibility.Name = "lblResponsibility";
        lblResponsibility.Text = "Responsibility *";

        cboResponsibility.DropDownStyle = ComboBoxStyle.DropDownList;
        cboResponsibility.Location = new Point(24, 90);
        cboResponsibility.Name = "cboResponsibility";
        cboResponsibility.Size = new Size(340, 25);
        cboResponsibility.TabIndex = 1;
        cboResponsibility.Items.AddRange(new object[] { "Stock-In", "Stock-Out", "Report Access" });
        cboResponsibility.SelectedIndex = 0;

        lblStart.AutoSize = true;
        lblStart.Location = new Point(24, 124);
        lblStart.Name = "lblStart";
        lblStart.Text = "Start date *";

        dtpStart.Format = DateTimePickerFormat.Short;
        dtpStart.Location = new Point(24, 142);
        dtpStart.Name = "dtpStart";
        dtpStart.Size = new Size(160, 25);
        dtpStart.TabIndex = 2;

        lblEnd.AutoSize = true;
        lblEnd.Location = new Point(24, 176);
        lblEnd.Name = "lblEnd";
        lblEnd.Text = "End date *";

        dtpEnd.Format = DateTimePickerFormat.Short;
        dtpEnd.Location = new Point(24, 194);
        dtpEnd.Name = "dtpEnd";
        dtpEnd.Size = new Size(160, 25);
        dtpEnd.TabIndex = 3;

        lblReason.AutoSize = true;
        lblReason.Location = new Point(24, 228);
        lblReason.Name = "lblReason";
        lblReason.Text = "Reason *";

        txtReason.BorderStyle = BorderStyle.FixedSingle;
        txtReason.Location = new Point(24, 246);
        txtReason.Multiline = true;
        txtReason.MaxLength = 500;
        txtReason.ScrollBars = ScrollBars.Vertical;
        txtReason.Name = "txtReason";
        txtReason.Size = new Size(340, 60);
        txtReason.TabIndex = 4;

        lblError.Location = new Point(24, 316);
        lblError.Name = "lblError";
        lblError.Size = new Size(340, 40);
        lblError.Visible = false;

        btnSave.Location = new Point(156, 370);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(100, 32);
        btnSave.TabIndex = 5;
        btnSave.Text = "Create";
        btnSave.Click += BtnSave_Click;

        btnCancel.Location = new Point(264, 370);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(100, 32);
        btnCancel.TabIndex = 6;
        btnCancel.Text = "Cancel";
        btnCancel.Click += BtnCancel_Click;

        AcceptButton = btnSave;
        CancelButton = btnCancel;

        Controls.Add(lblRecipient);
        Controls.Add(cboRecipient);
        Controls.Add(lblResponsibility);
        Controls.Add(cboResponsibility);
        Controls.Add(lblStart);
        Controls.Add(dtpStart);
        Controls.Add(lblEnd);
        Controls.Add(dtpEnd);
        Controls.Add(lblReason);
        Controls.Add(txtReason);
        Controls.Add(lblError);
        Controls.Add(btnSave);
        Controls.Add(btnCancel);

        ResumeLayout(false);
        PerformLayout();
    }
}
