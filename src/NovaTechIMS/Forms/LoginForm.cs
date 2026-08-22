using System;
using System.Windows.Forms;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms;

/// <summary>
/// SCR-001 Login shell (Milestone 1).
/// UI only — no authentication, no database, no password hashing.
/// Any non-empty username + password advances to MainForm for navigation demo.
/// </summary>
public partial class LoginForm : Form
{
    public LoginForm()
    {
        InitializeComponent();
    }

    private void LoginForm_Load(object? sender, EventArgs e)
    {
        AcceptButton = btnLogin;
        txtUsername.Focus();
        lblError.Visible = false;
    }

    private void BtnLogin_Click(object? sender, EventArgs e)
    {
        lblError.Visible = false;

        var username = txtUsername.Text.Trim();
        var password = txtPassword.Text;

        // Shell-level only: require non-empty fields. Not real authentication.
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            lblError.Text = "Please enter username and password.";
            lblError.Visible = true;
            if (string.IsNullOrEmpty(username))
                txtUsername.Focus();
            else
                txtPassword.Focus();
            return;
        }

        // Demo session label for StatusStrip (not a domain User entity).
        var displayName = username;
        Hide();
        using var main = new MainForm(displayName, "Administrator (demo)");
        main.ShowDialog(this);
        // After MainForm closes (logout), clear fields and show login again.
        txtPassword.Clear();
        txtUsername.SelectAll();
        lblError.Visible = false;
        Show();
        txtUsername.Focus();
    }

    private void BtnCancel_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
