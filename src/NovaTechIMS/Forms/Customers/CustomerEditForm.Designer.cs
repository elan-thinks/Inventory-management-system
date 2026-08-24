using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Customers;

/// <summary>
/// Customer create/edit dialog — BCL-only in Designer (no UiTheme).
/// </summary>
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
        Font = new Font("Segoe UI", 10F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(460, 380);
        BackColor = Color.White;
        Name = "CustomerEditForm";
        Text = "Customer";

        lblName.AutoSize = true;
        lblName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblName.ForeColor = Color.FromArgb(31, 41, 51);
        lblName.Location = new Point(24, 20);
        lblName.Name = "lblName";
        lblName.Text = "Name *";

        txtName.BorderStyle = BorderStyle.FixedSingle;
        txtName.Location = new Point(24, 40);
        txtName.MaxLength = 150;
        txtName.Name = "txtName";
        txtName.Size = new Size(412, 25);
        txtName.TabIndex = 0;

        lblPhone.AutoSize = true;
        lblPhone.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblPhone.ForeColor = Color.FromArgb(31, 41, 51);
        lblPhone.Location = new Point(24, 72);
        lblPhone.Name = "lblPhone";
        lblPhone.Text = "Phone";

        txtPhone.BorderStyle = BorderStyle.FixedSingle;
        txtPhone.Location = new Point(24, 92);
        txtPhone.MaxLength = 30;
        txtPhone.Name = "txtPhone";
        txtPhone.Size = new Size(412, 25);
        txtPhone.TabIndex = 1;

        lblEmail.AutoSize = true;
        lblEmail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblEmail.ForeColor = Color.FromArgb(31, 41, 51);
        lblEmail.Location = new Point(24, 124);
        lblEmail.Name = "lblEmail";
        lblEmail.Text = "Email";

        txtEmail.BorderStyle = BorderStyle.FixedSingle;
        txtEmail.Location = new Point(24, 144);
        txtEmail.MaxLength = 100;
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(412, 25);
        txtEmail.TabIndex = 2;

        lblAddress.AutoSize = true;
        lblAddress.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblAddress.ForeColor = Color.FromArgb(31, 41, 51);
        lblAddress.Location = new Point(24, 176);
        lblAddress.Name = "lblAddress";
        lblAddress.Text = "Address";

        txtAddress.BorderStyle = BorderStyle.FixedSingle;
        txtAddress.Location = new Point(24, 196);
        txtAddress.MaxLength = 250;
        txtAddress.Multiline = true;
        txtAddress.Name = "txtAddress";
        txtAddress.ScrollBars = ScrollBars.Vertical;
        txtAddress.Size = new Size(412, 56);
        txtAddress.TabIndex = 3;

        chkActive.AutoSize = true;
        chkActive.Checked = true;
        chkActive.CheckState = CheckState.Checked;
        chkActive.Font = new Font("Segoe UI", 10F);
        chkActive.ForeColor = Color.FromArgb(31, 41, 51);
        chkActive.Location = new Point(24, 264);
        chkActive.Name = "chkActive";
        chkActive.TabIndex = 4;
        chkActive.Text = "Active";
        chkActive.UseVisualStyleBackColor = true;

        lblError.Font = new Font("Segoe UI", 8.5F);
        lblError.ForeColor = Color.FromArgb(192, 57, 43);
        lblError.Location = new Point(24, 292);
        lblError.Name = "lblError";
        lblError.Size = new Size(412, 36);
        lblError.Visible = false;

        btnSave.BackColor = Color.FromArgb(30, 75, 143);
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(232, 336);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(100, 32);
        btnSave.TabIndex = 5;
        btnSave.Text = "Save";
        btnSave.UseVisualStyleBackColor = false;
        btnSave.Click += BtnSave_Click;

        btnCancel.BackColor = Color.White;
        btnCancel.FlatAppearance.BorderColor = Color.FromArgb(215, 220, 225);
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.Font = new Font("Segoe UI", 10F);
        btnCancel.ForeColor = Color.FromArgb(31, 41, 51);
        btnCancel.Location = new Point(336, 336);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(100, 32);
        btnCancel.TabIndex = 6;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = false;
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
