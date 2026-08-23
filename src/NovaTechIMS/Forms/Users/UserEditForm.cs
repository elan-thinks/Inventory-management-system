using System;
using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Models.Enums;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Users;

/// <summary>User Add/Edit modal (FR-USR, Milestone 10).</summary>
public class UserEditForm : Form
{
    private readonly UserService _service = new();
    private readonly int? _userId;

    private TextBox txtUsername = null!;
    private TextBox txtFullName = null!;
    private TextBox txtPassword = null!;
    private ComboBox cboRole = null!;
    private CheckBox chkActive = null!;
    private Label lblPasswordHint = null!;
    private Label lblError = null!;
    private Button btnSave = null!;

    public UserEditForm(int? userId = null)
    {
        _userId = userId;
        BuildUi();

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

    private void BuildUi()
    {
        AutoScaleMode = AutoScaleMode.Font;
        Font = UiTheme.Body;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        BackColor = UiTheme.Surface;

        int y = 20;
        const int lx = 24;
        const int fw = 360;

        void L(string t)
        {
            Controls.Add(new Label
            {
                Text = t,
                Font = UiTheme.Label,
                Location = new Point(lx, y),
                AutoSize = true
            });
            y += 18;
        }

        TextBox Tb(bool password = false)
        {
            var tb = new TextBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(lx, y),
                Width = fw,
                Font = UiTheme.Body
            };
            if (password) tb.UseSystemPasswordChar = true;
            y += 30;
            Controls.Add(tb);
            return tb;
        }

        L("Username *");
        txtUsername = Tb();
        txtUsername.MaxLength = 50;

        L("Full name *");
        txtFullName = Tb();
        txtFullName.MaxLength = 100;

        L("Password");
        txtPassword = Tb(password: true);
        lblPasswordHint = new Label
        {
            Font = UiTheme.Label,
            ForeColor = UiTheme.TextMuted,
            Location = new Point(lx, y),
            AutoSize = true
        };
        Controls.Add(lblPasswordHint);
        y += 22;

        L("Role *");
        cboRole = new ComboBox
        {
            FlatStyle = FlatStyle.Flat,
            Location = new Point(lx, y),
            Width = fw,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = UiTheme.Body
        };
        cboRole.Items.AddRange(new object[] { "Administrator", "Inventory Staff" });
        cboRole.SelectedIndex = 1;
        Controls.Add(cboRole);
        y += 32;

        chkActive = new CheckBox
        {
            Text = "Active",
            Location = new Point(lx, y),
            AutoSize = true,
            Checked = true,
            Font = UiTheme.Body
        };
        Controls.Add(chkActive);
        y += 28;

        lblError = new Label
        {
            ForeColor = UiTheme.Error,
            Font = UiTheme.Label,
            Location = new Point(lx, y),
            Size = new Size(fw, 40),
            Visible = false
        };
        Controls.Add(lblError);
        y += 44;

        btnSave = UiTheme.StyleButton(new Button
        {
            Text = "Save",
            Size = new Size(100, 32),
            Location = new Point(lx + fw - 208, y)
        }, UiTheme.ButtonKind.Primary);
        btnSave.Click += BtnSave_Click;
btnSave.Click += BtnSave_Click;

        var btnCancel = UiTheme.StyleButton(new Button
        {
            Text = "Cancel",
            Size = new Size(100, 32),
            Location = new Point(lx + fw - 100, y)
        }, UiTheme.ButtonKind.Secondary);
btnCancel.Click += (_, _) => DialogResult = DialogResult.Cancel;

        AcceptButton = btnSave;
        CancelButton = btnCancel;
        Controls.Add(btnSave);
        Controls.Add(btnCancel);
        ClientSize = new Size(lx * 2 + fw, y + 52);
    }

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
            var role = cboRole.SelectedIndex == 0
                ? UserRole.Administrator
                : UserRole.InventoryStaff;

            if (_userId is int id)
            {
                _service.Update(
                    id,
                    txtFullName.Text,
                    role,
                    chkActive.Checked,
                    string.IsNullOrEmpty(txtPassword.Text) ? null : txtPassword.Text);
            }
            else
            {
                _service.Create(
                    txtUsername.Text,
                    txtPassword.Text,
                    txtFullName.Text,
                    role);
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
