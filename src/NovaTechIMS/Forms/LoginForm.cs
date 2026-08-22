using System;
using System.Windows.Forms;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms;

/// <summary>
/// SCR-001 Login (Milestone 9 — real authentication against PostgreSQL).
/// </summary>
public partial class LoginForm : Form
{
    private readonly AuthService _auth = new();

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
        btnLogin.Enabled = false;

        var username = txtUsername.Text.Trim();
        var password = txtPassword.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ShowError("Please enter username and password.");
            if (string.IsNullOrEmpty(username))
                txtUsername.Focus();
            else
                txtPassword.Focus();
            btnLogin.Enabled = true;
            return;
        }

        try
        {
            var current = _auth.Authenticate(username, password);
            SessionContext.SignIn(current);

            Hide();
            using var main = new MainForm(current);
            main.ShowDialog(this);

            // After MainForm closes (logout), clear session and show login again.
            SessionContext.SignOut();
            txtPassword.Clear();
            txtUsername.SelectAll();
            lblError.Visible = false;
            Show();
            txtUsername.Focus();
        }
        catch (AuthenticationException ex)
        {
            ShowError(ex.Message);
            txtPassword.SelectAll();
            txtPassword.Focus();
        }
        catch (Exception ex)
        {
            ShowError("Unable to sign in. Check the database connection.\n" + ex.Message);
        }
        finally
        {
            btnLogin.Enabled = true;
        }
    }

    private void ShowError(string message)
    {
        lblError.Text = message;
        lblError.Visible = true;
    }

    private void BtnCancel_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
