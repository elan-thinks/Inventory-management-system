using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Customers;

partial class CustomerEditForm
{
    private System.ComponentModel.IContainer components = null;

    private Label lblName;
    private TextBox txtName;
    private Label lblPhone;
    private TextBox txtPhone;
    private Label lblEmail;
    private TextBox txtEmail;
    private Label lblAddress;
    private TextBox txtAddress;
    private CheckBox chkActive;
    private Label lblError;
    private Button btnSave;
    private Button btnCancel;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        lblName = new Label();
        txtName = new TextBox();
        lblPhone = new Label();
        txtPhone = new TextBox();
        lblEmail = new Label();
        txtEmail = new TextBox();
        lblAddress = new Label();
        txtAddress = new TextBox();
        chkActive = new CheckBox();
        lblError = new Label();
        btnSave = new Button();
        btnCancel = new Button();
        SuspendLayout();

        AutoScaleMode = AutoScaleMode.Font;
        Font = UiTheme.Body;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(460, 380);
        BackColor = UiTheme.Surface;
        Name = "CustomerEditForm";
        Text = "Customer";

        lblName.Text = "Name *";
        lblName.Font = UiTheme.Label;
        lblName.ForeColor = UiTheme.Text;
        lblName.Location = new Point(24, 20);
        lblName.AutoSize = true;
        lblName.Name = "lblName";

        txtName.Location = new Point(24, 40);
        txtName.Width = 412;
        txtName.MaxLength = 150;
        txtName.BorderStyle = BorderStyle.FixedSingle;
        txtName.Name = "txtName";
        txtName.TabIndex = 0;

        lblPhone.Text = "Phone";
        lblPhone.Font = UiTheme.Label;
        lblPhone.ForeColor = UiTheme.Text;
        lblPhone.Location = new Point(24, 72);
        lblPhone.AutoSize = true;
        lblPhone.Name = "lblPhone";

        txtPhone.Location = new Point(24, 92);
        txtPhone.Width = 412;
        txtPhone.MaxLength = 30;
        txtPhone.BorderStyle = BorderStyle.FixedSingle;
        txtPhone.Name = "txtPhone";
        txtPhone.TabIndex = 1;

        lblEmail.Text = "Email";
        lblEmail.Font = UiTheme.Label;
        lblEmail.ForeColor = UiTheme.Text;
        lblEmail.Location = new Point(24, 124);
        lblEmail.AutoSize = true;
        lblEmail.Name = "lblEmail";

        txtEmail.Location = new Point(24, 144);
        txtEmail.Width = 412;
        txtEmail.MaxLength = 100;
        txtEmail.BorderStyle = BorderStyle.FixedSingle;
        txtEmail.Name = "txtEmail";
        txtEmail.TabIndex = 2;

        lblAddress.Text = "Address";
        lblAddress.Font = UiTheme.Label;
        lblAddress.ForeColor = UiTheme.Text;
        lblAddress.Location = new Point(24, 176);
        lblAddress.AutoSize = true;
        lblAddress.Name = "lblAddress";

        txtAddress.Location = new Point(24, 196);
        txtAddress.Size = new Size(412, 56);
        txtAddress.MaxLength = 250;
        txtAddress.Multiline = true;
        txtAddress.ScrollBars = ScrollBars.Vertical;
        txtAddress.BorderStyle = BorderStyle.FixedSingle;
        txtAddress.Name = "txtAddress";
        txtAddress.TabIndex = 3;

        chkActive.Text = "Active";
        chkActive.Font = UiTheme.Body;
        chkActive.ForeColor = UiTheme.Text;
        chkActive.Location = new Point(24, 264);
        chkActive.AutoSize = true;
        chkActive.Checked = true;
        chkActive.Name = "chkActive";
        chkActive.TabIndex = 4;

        lblError.ForeColor = UiTheme.Error;
        lblError.Font = UiTheme.Label;
        lblError.Location = new Point(24, 292);
        lblError.Size = new Size(412, 36);
        lblError.Visible = false;
        lblError.Name = "lblError";

        btnSave.Text = "Save";
        btnSave.Size = new Size(100, 32);
        btnSave.Location = new Point(232, 336);
        btnSave.Name = "btnSave";
        btnSave.TabIndex = 5;
        btnSave.Click += BtnSave_Click;

        btnCancel.Text = "Cancel";
        btnCancel.Size = new Size(100, 32);
        btnCancel.Location = new Point(336, 336);
        btnCancel.Name = "btnCancel";
        btnCancel.TabIndex = 6;
        btnCancel.Click += BtnCancel_Click;

        AcceptButton = btnSave;
        CancelButton = btnCancel;

        Controls.Add(lblName);
        Controls.Add(txtName);
        Controls.Add(lblPhone);
        Controls.Add(txtPhone);
        Controls.Add(lblEmail);
        Controls.Add(txtEmail);
        Controls.Add(lblAddress);
        Controls.Add(txtAddress);
        Controls.Add(chkActive);
        Controls.Add(lblError);
        Controls.Add(btnSave);
        Controls.Add(btnCancel);

        ResumeLayout(false);
        PerformLayout();
    }
}
