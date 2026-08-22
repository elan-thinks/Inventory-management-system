using System;
using System.Windows.Forms;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms;

/// <summary>
/// SCR-001 Login — real authentication; approved presentation (M19).
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

            SessionContext.SignOut();
            txtPassword.Clear();
            txtUsername.SelectAll();
            lblError.Visible = false;
            Show();
            txtUsername.Focus();
        }
        catch (AuthenticationException)
        {
            ShowError("Invalid username or password.");
            txtPassword.Clear();
            txtPassword.Focus();
        }
        catch (Exception ex)
        {
            var mapped = DbExceptionMapper.Map(ex);
            ShowError(ErrorPresenter.ToUserMessage(mapped));
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
}
