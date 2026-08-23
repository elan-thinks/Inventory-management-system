using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms;

partial class LoginForm
{
    private System.ComponentModel.IContainer components = null;

    private Panel pnlCard;
    private Label lblBrand;
    private Label lblSubtitle;
    private Label lblUsername;
    private TextBox txtUsername;
    private Label lblPassword;
    private TextBox txtPassword;
    private Label lblError;
    private Button btnLogin;
    private Button btnCancel;
    private Label lblHint;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        pnlCard = new Panel();
        lblBrand = new Label();
        lblSubtitle = new Label();
        lblUsername = new Label();
        txtUsername = new TextBox();
        lblPassword = new Label();
        txtPassword = new TextBox();
        lblError = new Label();
        btnLogin = new Button();
        btnCancel = new Button();
        lblHint = new Label();

        SuspendLayout();
        pnlCard.SuspendLayout();

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(244, 246, 248);
        ClientSize = new Size(480, 420);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = true;
        Name = "LoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "NovaTech IMS — Sign In";
        Font = new Font("Segoe UI", 10F);
        Load += LoginForm_Load;

        pnlCard.BackColor = Color.White;
        pnlCard.Location = new Point(60, 40);
        pnlCard.Size = new Size(360, 320);
        pnlCard.Name = "pnlCard";

        lblBrand.Text = "NovaTech IMS";
        lblBrand.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblBrand.ForeColor = Color.FromArgb(30, 75, 143);
        lblBrand.AutoSize = true;
        lblBrand.Location = new Point(28, 24);
        lblBrand.Name = "lblBrand";

        lblSubtitle.Text = "Inventory Management System";
        lblSubtitle.Font = new Font("Segoe UI", 9F);
        lblSubtitle.ForeColor = Color.FromArgb(107, 119, 133);
        lblSubtitle.AutoSize = true;
        lblSubtitle.Location = new Point(28, 52);
        lblSubtitle.Name = "lblSubtitle";

        lblUsername.Text = "Username";
        lblUsername.Font = new Font("Segoe UI", 9F);
        lblUsername.AutoSize = true;
        lblUsername.Location = new Point(28, 90);
        lblUsername.Name = "lblUsername";

        txtUsername.Location = new Point(28, 112);
        txtUsername.Size = new Size(300, 28);
        txtUsername.BorderStyle = BorderStyle.FixedSingle;
        txtUsername.Name = "txtUsername";
        txtUsername.TabIndex = 0;
        txtUsername.AccessibleName = "Username";

        lblPassword.Text = "Password";
        lblPassword.Font = new Font("Segoe UI", 9F);
        lblPassword.AutoSize = true;
        lblPassword.Location = new Point(28, 150);
        lblPassword.Name = "lblPassword";

        txtPassword.Location = new Point(28, 172);
        txtPassword.Size = new Size(300, 28);
        txtPassword.BorderStyle = BorderStyle.FixedSingle;
        txtPassword.UseSystemPasswordChar = true;
        txtPassword.Name = "txtPassword";
        txtPassword.TabIndex = 1;
        txtPassword.AccessibleName = "Password";

        lblError.Text = "";
        lblError.Font = new Font("Segoe UI", 9F);
        lblError.ForeColor = Color.FromArgb(192, 57, 43);
        lblError.AutoSize = false;
        lblError.Size = new Size(300, 20);
        lblError.Location = new Point(28, 208);
        lblError.Name = "lblError";
        lblError.Visible = false;

        btnLogin.Text = "Sign In";
        btnLogin.Size = new Size(140, 34);
        btnLogin.Location = new Point(28, 240);
        btnLogin.Name = "btnLogin";
        btnLogin.TabIndex = 2;
        btnLogin.UseVisualStyleBackColor = true;
        btnLogin.Click += BtnLogin_Click;
        btnLogin.AccessibleName = "Sign In";

        btnCancel.Text = "Exit";
        btnCancel.Size = new Size(140, 34);
        btnCancel.Location = new Point(188, 240);
        btnCancel.Name = "btnCancel";
        btnCancel.TabIndex = 3;
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += BtnCancel_Click;
        btnCancel.AccessibleName = "Exit";

        lblHint.Text = "Enter your NovaTech IMS credentials to continue.";
        lblHint.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
        lblHint.ForeColor = Color.FromArgb(107, 119, 133);
        lblHint.AutoSize = false;
        lblHint.Size = new Size(300, 32);
        lblHint.Location = new Point(28, 282);
        lblHint.Name = "lblHint";

        pnlCard.Controls.Add(lblBrand);
        pnlCard.Controls.Add(lblSubtitle);
        pnlCard.Controls.Add(lblUsername);
        pnlCard.Controls.Add(txtUsername);
        pnlCard.Controls.Add(lblPassword);
        pnlCard.Controls.Add(txtPassword);
        pnlCard.Controls.Add(lblError);
        pnlCard.Controls.Add(btnLogin);
        pnlCard.Controls.Add(btnCancel);
        pnlCard.Controls.Add(lblHint);

        Controls.Add(pnlCard);

        pnlCard.ResumeLayout(false);
        pnlCard.PerformLayout();
        ResumeLayout(false);
    }
}
