using System;
using System.Windows.Forms;
using NovaTechIMS.Models.Enums;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Users;

/// <summary>User Add/Edit modal — Designer-based (partial).</summary>
public partial class UserEditForm : Form
{
    private readonly UserService _service = new();
    private readonly int? _userId;

    public UserEditForm(int? userId = null)
    {
        _userId = userId;
        InitializeComponent();
        ApplyRuntimeStyling();

        if (_userId is int id)
        {
            Text = "Edit User";
            txtUsername.ReadOnly = true;
            txtUsername.BackColor = UiTheme.Background;
            lblPasswordHint.Text = "Leave password blank to keep current password.";
            LoadExisting(id);
        }
        else
        {
            Text = "Add User";
            chkActive.Visible = false;
            chkActive.Enabled = false;
            lblPasswordHint.Text = "Password required (min 6 characters).";
        }
    }

    private void ApplyRuntimeStyling()
    {
        Font = UiTheme.Body;
        BackColor = UiTheme.Surface;
        foreach (var lbl in new[] { lblUsername, lblFullName, lblPassword, lblRole })
        {
            lbl.Font = UiTheme.Label;
            lbl.ForeColor = UiTheme.Text;
        }
        lblPasswordHint.Font = UiTheme.Label;
        lblPasswordHint.ForeColor = UiTheme.TextMuted;
        lblError.Font = UiTheme.Label;
        lblError.ForeColor = UiTheme.Error;
        chkActive.Font = UiTheme.Body;
        UiTheme.StyleTextBox(txtUsername);
        UiTheme.StyleTextBox(txtFullName);
        UiTheme.StyleTextBox(txtPassword);
        UiTheme.StyleComboBox(cboRole);
        UiTheme.StyleButton(btnSave, UiTheme.ButtonKind.Primary);
        UiTheme.StyleButton(btnCancel, UiTheme.ButtonKind.Secondary);
    }

    private void BtnCancel_Click(object? sender, EventArgs e) => DialogResult = DialogResult.Cancel;

    private void LoadExisting(int id)
    {
        try
        {
            var u = _service.GetById(id);
            txtUsername.Text = u.Username;
            txtFullName.Text = u.FullName;
            cboRole.SelectedIndex = u.Role == UserRole.Administrator ? 0 : 1;
            chkActive.Checked = u.IsActive;
            chkActive.Visible = true;
            chkActive.Enabled = true;
        }
        catch (AppException ex)
        {
            MessageBox.Show(this, ex.Message, "User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            DialogResult = DialogResult.Cancel;
        }
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        lblError.Visible = false;
        btnSave.Enabled = false;
        try
        {
            var role = cboRole.SelectedIndex == 0 ? UserRole.Administrator : UserRole.InventoryStaff;
            if (_userId is int id)
            {
                _service.Update(id, txtFullName.Text, role, chkActive.Checked,
                    string.IsNullOrEmpty(txtPassword.Text) ? null : txtPassword.Text);
            }
            else
            {
                _service.Create(txtUsername.Text, txtPassword.Text, txtFullName.Text, role);
            }
            DialogResult = DialogResult.OK;
        }
        catch (AppException ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }
        catch (Exception ex)
        {
            lblError.Text = "Could not save user. " + ex.Message;
            lblError.Visible = true;
        }
        finally
        {
            btnSave.Enabled = true;
        }
    }
}
