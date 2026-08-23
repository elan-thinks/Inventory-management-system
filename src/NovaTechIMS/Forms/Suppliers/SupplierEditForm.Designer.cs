using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Suppliers;

partial class SupplierEditForm
{
    private System.ComponentModel.IContainer components = null;

    private Label lblName;
    private TextBox txtName;
    private Label lblContact;
    private TextBox txtContact;
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
        lblContact = new Label();
        txtContact = new TextBox();
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
        ClientSize = new Size(460, 440);
        BackColor = UiTheme.Surface;
        Name = "SupplierEditForm";
        Text = "Supplier";

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

        lblContact.Text = "Contact person";
        lblContact.Font = UiTheme.Label;
        lblContact.ForeColor = UiTheme.Text;
        lblContact.Location = new Point(24, 72);
        lblContact.AutoSize = true;
        lblContact.Name = "lblContact";

        txtContact.Location = new Point(24, 92);
        txtContact.Width = 412;
        txtContact.MaxLength = 100;
        txtContact.BorderStyle = BorderStyle.FixedSingle;
        txtContact.Name = "txtContact";
        txtContact.TabIndex = 1;

        lblPhone.Text = "Phone";
        lblPhone.Font = UiTheme.Label;
        lblPhone.ForeColor = UiTheme.Text;
        lblPhone.Location = new Point(24, 124);
        lblPhone.AutoSize = true;
        lblPhone.Name = "lblPhone";

        txtPhone.Location = new Point(24, 144);
        txtPhone.Width = 412;
        txtPhone.MaxLength = 30;
        txtPhone.BorderStyle = BorderStyle.FixedSingle;
        txtPhone.Name = "txtPhone";
        txtPhone.TabIndex = 2;

        lblEmail.Text = "Email";
        lblEmail.Font = UiTheme.Label;
        lblEmail.ForeColor = UiTheme.Text;
        lblEmail.Location = new Point(24, 176);
        lblEmail.AutoSize = true;
        lblEmail.Name = "lblEmail";

        txtEmail.Location = new Point(24, 196);
        txtEmail.Width = 412;
        txtEmail.MaxLength = 100;
        txtEmail.BorderStyle = BorderStyle.FixedSingle;
        txtEmail.Name = "txtEmail";
        txtEmail.TabIndex = 3;

        lblAddress.Text = "Address";
        lblAddress.Font = UiTheme.Label;
        lblAddress.ForeColor = UiTheme.Text;
        lblAddress.Location = new Point(24, 228);
        lblAddress.AutoSize = true;
        lblAddress.Name = "lblAddress";

        txtAddress.Location = new Point(24, 248);
        txtAddress.Size = new Size(412, 56);
        txtAddress.MaxLength = 300;
        txtAddress.Multiline = true;
        txtAddress.ScrollBars = ScrollBars.Vertical;
        txtAddress.BorderStyle = BorderStyle.FixedSingle;
        txtAddress.Name = "txtAddress";
        txtAddress.TabIndex = 4;

        chkActive.Text = "Active";
        chkActive.Font = UiTheme.Body;
        chkActive.ForeColor = UiTheme.Text;
        chkActive.Location = new Point(24, 316);
        chkActive.AutoSize = true;
        chkActive.Checked = true;
        chkActive.Name = "chkActive";
        chkActive.TabIndex = 5;

        lblError.ForeColor = UiTheme.Error;
        lblError.Font = UiTheme.Label;
        lblError.Location = new Point(24, 344);
        lblError.Size = new Size(412, 36);
        lblError.Visible = false;
        lblError.Name = "lblError";

        btnSave.Text = "Save";
        btnSave.Size = new Size(100, 32);
        btnSave.Location = new Point(232, 392);
        btnSave.Name = "btnSave";
        btnSave.TabIndex = 6;
        btnSave.Click += BtnSave_Click;

        btnCancel.Text = "Cancel";
        btnCancel.Size = new Size(100, 32);
        btnCancel.Location = new Point(336, 392);
        btnCancel.Name = "btnCancel";
        btnCancel.TabIndex = 7;
        btnCancel.Click += BtnCancel_Click;

        AcceptButton = btnSave;
        CancelButton = btnCancel;

        Controls.Add(lblName);
        Controls.Add(txtName);
        Controls.Add(lblContact);
        Controls.Add(txtContact);
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
