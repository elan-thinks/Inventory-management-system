using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Users;

/// <summary>User create/edit dialog — BCL-only Designer layout.</summary>
partial class UserEditForm
{
    private System.ComponentModel.IContainer components = null;
    private Label lblUsername;
    private TextBox txtUsername;
    private Label lblFullName;
    private TextBox txtFullName;
    private Label lblPassword;
    private TextBox txtPassword;
    private Label lblPasswordHint;
    private Label lblRole;
    private ComboBox cboRole;
    private CheckBox chkActive;
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
        lblUsername = new Label();
        txtUsername = new TextBox();
        lblFullName = new Label();
        txtFullName = new TextBox();
        lblPassword = new Label();
        txtPassword = new TextBox();
        lblPasswordHint = new Label();
        lblRole = new Label();
        cboRole = new ComboBox();
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
        BackColor = Color.White;
        ClientSize = new Size(408, 380);
        Name = "UserEditForm";
        Text = "User";

        lblUsername.AutoSize = true;
        lblUsername.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblUsername.ForeColor = Color.FromArgb(31, 41, 51);
        lblUsername.Location = new Point(24, 20);
        lblUsername.Name = "lblUsername";
        lblUsername.Text = "Username *";

        txtUsername.BorderStyle = BorderStyle.FixedSingle;
        txtUsername.Location = new Point(24, 38);
        txtUsername.MaxLength = 50;
        txtUsername.Name = "txtUsername";
        txtUsername.Size = new Size(360, 25);
        txtUsername.TabIndex = 0;

        lblFullName.AutoSize = true;
        lblFullName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblFullName.ForeColor = Color.FromArgb(31, 41, 51);
        lblFullName.Location = new Point(24, 70);
        lblFullName.Name = "lblFullName";
        lblFullName.Text = "Full name *";

        txtFullName.BorderStyle = BorderStyle.FixedSingle;
        txtFullName.Location = new Point(24, 88);
        txtFullName.MaxLength = 100;
        txtFullName.Name = "txtFullName";
        txtFullName.Size = new Size(360, 25);
        txtFullName.TabIndex = 1;

        lblPassword.AutoSize = true;
        lblPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblPassword.ForeColor = Color.FromArgb(31, 41, 51);
        lblPassword.Location = new Point(24, 120);
        lblPassword.Name = "lblPassword";
        lblPassword.Text = "Password";

        txtPassword.BorderStyle = BorderStyle.FixedSingle;
        txtPassword.Location = new Point(24, 138);
        txtPassword.Name = "txtPassword";
        txtPassword.Size = new Size(360, 25);
        txtPassword.TabIndex = 2;
        txtPassword.UseSystemPasswordChar = true;

        lblPasswordHint.AutoSize = true;
        lblPasswordHint.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
        lblPasswordHint.ForeColor = Color.FromArgb(107, 119, 133);
        lblPasswordHint.Location = new Point(24, 168);
        lblPasswordHint.Name = "lblPasswordHint";
        lblPasswordHint.Text = "Password required (min 6 characters).";

        lblRole.AutoSize = true;
        lblRole.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblRole.ForeColor = Color.FromArgb(31, 41, 51);
        lblRole.Location = new Point(24, 196);
        lblRole.Name = "lblRole";
        lblRole.Text = "Role *";

        cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
        cboRole.FlatStyle = FlatStyle.Flat;
        cboRole.Location = new Point(24, 214);
        cboRole.Name = "cboRole";
        cboRole.Size = new Size(360, 25);
        cboRole.TabIndex = 3;
        cboRole.Items.AddRange(new object[] { "Administrator", "Inventory Staff" });
        cboRole.SelectedIndex = 1;

        chkActive.AutoSize = true;
        chkActive.Checked = true;
        chkActive.Location = new Point(24, 250);
        chkActive.Name = "chkActive";
        chkActive.TabIndex = 4;
        chkActive.Text = "Active";

        lblError.Location = new Point(24, 280);
        lblError.Name = "lblError";
        lblError.Size = new Size(360, 40);
        lblError.ForeColor = Color.FromArgb(192, 57, 43);
        lblError.Visible = false;

        btnSave.BackColor = Color.FromArgb(30, 75, 143);
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(176, 330);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(100, 32);
        btnSave.TabIndex = 5;
        btnSave.Text = "Save";
        btnSave.UseVisualStyleBackColor = false;
        btnSave.Click += BtnSave_Click;

        btnCancel.BackColor = Color.White;
        btnCancel.FlatAppearance.BorderColor = Color.FromArgb(215, 220, 225);
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.Location = new Point(284, 330);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(100, 32);
        btnCancel.TabIndex = 6;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = false;
        btnCancel.Click += BtnCancel_Click;

        AcceptButton = btnSave;
        CancelButton = btnCancel;

        Controls.Add(lblUsername);
        Controls.Add(txtUsername);
        Controls.Add(lblFullName);
        Controls.Add(txtFullName);
        Controls.Add(lblPassword);
        Controls.Add(txtPassword);
        Controls.Add(lblPasswordHint);
        Controls.Add(lblRole);
        Controls.Add(cboRole);
        Controls.Add(chkActive);
        Controls.Add(lblError);
        Controls.Add(btnSave);
        Controls.Add(btnCancel);

        ResumeLayout(false);
        PerformLayout();
    }
}
