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
        if (disposing && (components != null))
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
        pnlCard.SuspendLayout();
        SuspendLayout();

        // LoginForm
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(244, 246, 248);
        ClientSize = new Size(420, 420);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = true;
        Name = "LoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Inventory Management System (IMS) — Sign In";

        // pnlCard
        pnlCard.BackColor = Color.White;
        pnlCard.Location = new Point(40, 40);
        pnlCard.Name = "pnlCard";
        pnlCard.Size = new Size(340, 320);
        pnlCard.TabIndex = 0;

        // lblBrand
        lblBrand.Text = "IMS";
        lblBrand.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblBrand.ForeColor = Color.FromArgb(30, 75, 143);
        lblBrand.AutoSize = true;
        lblBrand.Location = new Point(28, 24);
        lblBrand.Name = "lblBrand";

        // lblSubtitle
        lblSubtitle.Text = "Inventory Management System (IMS)";
        lblSubtitle.Font = new Font("Segoe UI", 9F);
        lblSubtitle.ForeColor = Color.FromArgb(107, 119, 133);
        lblSubtitle.AutoSize = true;
        lblSubtitle.Location = new Point(28, 52);
        lblSubtitle.Name = "lblSubtitle";

        // lblUsername
        lblUsername.Text = "Username";
        lblUsername.Font = new Font("Segoe UI", 9F);
        lblUsername.ForeColor = Color.FromArgb(31, 41, 51);
        lblUsername.AutoSize = true;
        lblUsername.Location = new Point(28, 90);
        lblUsername.Name = "lblUsername";

        // txtUsername
        txtUsername.Location = new Point(28, 110);
        txtUsername.Name = "txtUsername";
        txtUsername.Size = new Size(280, 23);
        txtUsername.TabIndex = 1;

        // lblPassword
        lblPassword.Text = "Password";
        lblPassword.Font = new Font("Segoe UI", 9F);
        lblPassword.ForeColor = Color.FromArgb(31, 41, 51);
        lblPassword.AutoSize = true;
        lblPassword.Location = new Point(28, 145);
        lblPassword.Name = "lblPassword";

        // txtPassword
        txtPassword.Location = new Point(28, 165);
        txtPassword.Name = "txtPassword";
        txtPassword.Size = new Size(280, 23);
        txtPassword.UseSystemPasswordChar = true;
        txtPassword.TabIndex = 2;

        // lblError
        lblError.Text = "";
        lblError.ForeColor = Color.FromArgb(180, 35, 24);
        lblError.AutoSize = false;
        lblError.Size = new Size(280, 36);
        lblError.Location = new Point(28, 198);
        lblError.Name = "lblError";

        // btnLogin
        btnLogin.Text = "Sign In";
        btnLogin.Location = new Point(28, 245);
        btnLogin.Size = new Size(280, 36);
        btnLogin.Name = "btnLogin";
        btnLogin.TabIndex = 3;
        btnLogin.UseVisualStyleBackColor = true;
        btnLogin.AccessibleName = "Sign In";
        btnLogin.Click += BtnLogin_Click;

        // btnCancel
        btnCancel.Text = "Exit";
        btnCancel.Location = new Point(28, 288);
        btnCancel.Size = new Size(280, 28);
        btnCancel.Name = "btnCancel";
        btnCancel.TabIndex = 4;
        btnCancel.Click += BtnCancel_Click;

        // lblHint
        lblHint.Text = "Sign in with your IMS account to continue.";
        lblHint.Font = new Font("Segoe UI", 8F);
        lblHint.ForeColor = Color.FromArgb(107, 119, 133);
        lblHint.AutoSize = true;
        lblHint.Location = new Point(28, 70);
        lblHint.Name = "lblHint";

        pnlCard.Controls.Add(lblBrand);
        pnlCard.Controls.Add(lblSubtitle);
        pnlCard.Controls.Add(lblHint);
        pnlCard.Controls.Add(lblUsername);
        pnlCard.Controls.Add(txtUsername);
        pnlCard.Controls.Add(lblPassword);
        pnlCard.Controls.Add(txtPassword);
        pnlCard.Controls.Add(lblError);
        pnlCard.Controls.Add(btnLogin);
        pnlCard.Controls.Add(btnCancel);

        Controls.Add(pnlCard);
        pnlCard.ResumeLayout(false);
        pnlCard.PerformLayout();
        ResumeLayout(false);
    }
}
