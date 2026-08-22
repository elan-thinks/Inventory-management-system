using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Utilities;

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
        BackColor = UiTheme.Background;
        ClientSize = new Size(480, 420);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = true;
        Name = "LoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "NovaTech IMS — Sign In";
        Font = UiTheme.Body;
        Load += LoginForm_Load;

        pnlCard.BackColor = UiTheme.Surface;
        pnlCard.BorderStyle = BorderStyle.FixedSingle;
        pnlCard.Location = new Point(60, 40);
        pnlCard.Size = new Size(360, 320);
        pnlCard.Name = "pnlCard";

        lblBrand.Text = "NovaTech IMS";
        lblBrand.Font = UiTheme.ScreenTitle;
        lblBrand.ForeColor = UiTheme.Primary;
        lblBrand.AutoSize = true;
        lblBrand.Location = new Point(28, 24);
        lblBrand.Name = "lblBrand";

        lblSubtitle.Text = "Inventory Management System";
        lblSubtitle.Font = UiTheme.Label;
        lblSubtitle.ForeColor = UiTheme.TextMuted;
        lblSubtitle.AutoSize = true;
        lblSubtitle.Location = new Point(28, 52);
        lblSubtitle.Name = "lblSubtitle";

        lblUsername.Text = "Username";
        lblUsername.Font = UiTheme.Label;
        lblUsername.ForeColor = UiTheme.Text;
        lblUsername.AutoSize = true;
        lblUsername.Location = new Point(28, 90);
        lblUsername.Name = "lblUsername";

        txtUsername.Location = new Point(28, 112);
        txtUsername.Size = new Size(300, 28);
        txtUsername.Font = UiTheme.Body;
        txtUsername.Name = "txtUsername";
        txtUsername.TabIndex = 0;
        txtUsername.AccessibleName = "Username";

        lblPassword.Text = "Password";
        lblPassword.Font = UiTheme.Label;
        lblPassword.ForeColor = UiTheme.Text;
        lblPassword.AutoSize = true;
        lblPassword.Location = new Point(28, 150);
        lblPassword.Name = "lblPassword";

        txtPassword.Location = new Point(28, 172);
        txtPassword.Size = new Size(300, 28);
        txtPassword.Font = UiTheme.Body;
        txtPassword.UseSystemPasswordChar = true;
        txtPassword.Name = "txtPassword";
        txtPassword.TabIndex = 1;
        txtPassword.AccessibleName = "Password";

        lblError.Text = "";
        lblError.Font = UiTheme.Label;
        lblError.ForeColor = UiTheme.Error;
        lblError.AutoSize = false;
        lblError.Size = new Size(300, 20);
        lblError.Location = new Point(28, 208);
        lblError.Name = "lblError";
        lblError.Visible = false;

        btnLogin.Text = "Sign In";
        btnLogin.Font = UiTheme.Button;
        btnLogin.BackColor = UiTheme.Primary;
        btnLogin.ForeColor = Color.White;
        btnLogin.FlatStyle = FlatStyle.Flat;
        btnLogin.FlatAppearance.BorderSize = 0;
        btnLogin.Size = new Size(140, 34);
        btnLogin.Location = new Point(28, 240);
        btnLogin.Name = "btnLogin";
        btnLogin.TabIndex = 2;
        btnLogin.Cursor = Cursors.Hand;
        btnLogin.Click += BtnLogin_Click;
        btnLogin.AccessibleName = "Sign In";

        btnCancel.Text = "Exit";
        btnCancel.Font = UiTheme.Button;
        btnCancel.BackColor = UiTheme.Surface;
        btnCancel.ForeColor = UiTheme.Text;
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.FlatAppearance.BorderColor = UiTheme.Border;
        btnCancel.FlatAppearance.BorderSize = 1;
        btnCancel.Size = new Size(140, 34);
        btnCancel.Location = new Point(188, 240);
        btnCancel.Name = "btnCancel";
        btnCancel.TabIndex = 3;
        btnCancel.Cursor = Cursors.Hand;
        btnCancel.Click += BtnCancel_Click;
        btnCancel.AccessibleName = "Exit";

        lblHint.Text = "Milestone 1 shell — any username/password opens the app.";
        lblHint.Font = UiTheme.Label;
        lblHint.ForeColor = UiTheme.TextMuted;
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
