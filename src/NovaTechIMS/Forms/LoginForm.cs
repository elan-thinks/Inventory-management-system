using System;
using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms;

/// <summary>
/// SCR-001 Login — Designer-compatible (no runtime work at design time).
/// </summary>
public partial class LoginForm : Form
{
    private AuthService? _auth;

    private AuthService Auth => _auth ??= new AuthService();

    public LoginForm()
    {
        InitializeComponent();

        if (DesignTime.IsActive)
            return;

        try
        {
            ApplyRuntimeStyling();
        }
        catch (Exception ex)
        {
            // Never block the login window from appearing because of theme polish
            System.Diagnostics.Debug.WriteLine("[Login] styling: " + ex);
        }
    }

    private void ApplyRuntimeStyling()
    {
        BackColor = UiTheme.Background;
        Font = UiTheme.Body;

        pnlCard.BackColor = UiTheme.Surface;
        // Avoid Region clipping issues on some hosts — simple border paint is enough
        pnlCard.Paint += (_, e) =>
        {
            using var pen = new Pen(UiTheme.Border, 1);
            e.Graphics.DrawRectangle(pen, 0, 0, pnlCard.Width - 1, pnlCard.Height - 1);
        };

        lblBrand.Font = UiTheme.ScreenTitle;
        lblBrand.ForeColor = UiTheme.Primary;
        lblSubtitle.Font = UiTheme.Label;
        lblSubtitle.ForeColor = UiTheme.TextMuted;
        lblUsername.Font = UiTheme.Label;
        lblUsername.ForeColor = UiTheme.Text;
        lblPassword.Font = UiTheme.Label;
        lblPassword.ForeColor = UiTheme.Text;
        lblError.Font = UiTheme.Label;
        lblError.ForeColor = UiTheme.Error;
        lblHint.Font = UiTheme.HelperText;
        lblHint.ForeColor = UiTheme.TextMuted;

        UiTheme.StyleTextBox(txtUsername);
        UiTheme.StyleTextBox(txtPassword);
        UiTheme.StyleButton(btnLogin, UiTheme.ButtonKind.Primary);
        UiTheme.StyleButton(btnCancel, UiTheme.ButtonKind.Secondary);
    }

    private void LoginForm_Load(object? sender, EventArgs e)
    {
        if (DesignTime.IsActive) return;

        AcceptButton = btnLogin;
        CancelButton = btnCancel;
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
            var current = Auth.Authenticate(username, password);
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

    private void BtnCancel_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private void ShowError(string message)
    {
        lblError.Text = message;
        lblError.Visible = true;
    }
}
