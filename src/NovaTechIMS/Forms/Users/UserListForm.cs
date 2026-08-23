using System;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Users;

/// <summary>User Management list — Designer-based (partial).</summary>
public partial class UserListForm : Form
{
    private readonly UserService _service = new();

    public UserListForm()
    {
        InitializeComponent();
        ApplyRuntimeStyling();
        Load += (_, _) => ReloadGrid();
    }

    private void ApplyRuntimeStyling()
    {
        Font = UiTheme.Body;
        BackColor = UiTheme.Background;
        toolbar.BackColor = UiTheme.Background;
        lblEmpty.Font = UiTheme.Body;
        lblEmpty.ForeColor = UiTheme.TextMuted;
        lblCount.Font = UiTheme.Label;
        lblCount.ForeColor = UiTheme.TextMuted;
        UiTheme.StyleTextBox(txtSearch);
        UiTheme.StyleComboBox(cboStatus);
        UiTheme.StyleButton(btnRefresh, UiTheme.ButtonKind.Secondary);
        UiTheme.StyleButton(btnAdd, UiTheme.ButtonKind.Primary);
        UiTheme.ApplyGridTheme(grid);
        UiTheme.WireStatusBadgeColumn(grid, "Status", value => (value?.ToString() ?? "") switch
        {
            "Active" => ("Active", UiTheme.BadgeTone.Success, '\u2713'),
            _ => ("Inactive", UiTheme.BadgeTone.Muted, '\u2715')
        });
        Toolbar_Resize(toolbar, EventArgs.Empty);
    }

    private void Toolbar_Resize(object? sender, EventArgs e)
    {
        btnAdd.Left = toolbar.ClientSize.Width - btnAdd.Width;
        btnRefresh.Left = btnAdd.Left - btnRefresh.Width - 8;
    }

    private void TxtSearch_TextChanged(object? sender, EventArgs e) => ReloadGrid();
    private void CboStatus_SelectedIndexChanged(object? sender, EventArgs e) => ReloadGrid();
    private void BtnRefresh_Click(object? sender, EventArgs e) => ReloadGrid();

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        using var dlg = new UserEditForm();
        if (dlg.ShowDialog(FindForm()) == DialogResult.OK)
            ReloadGrid();
    }

    private void ReloadGrid()
    {
        try
        {
            string? search = string.IsNullOrWhiteSpace(txtSearch.Text) ? null : txtSearch.Text;
            bool? active = cboStatus.SelectedIndex switch
            {
                1 => true,
                2 => false,
                _ => null
            };

            var rows = _service.GetList(search, active);
            grid.DataSource = null;
            grid.Columns.Clear();

            if (rows.Count == 0)
            {
                grid.Visible = false;
                lblEmpty.Visible = true;
                lblEmpty.Text = "No users found.";
                lblCount.Text = "0 users";
                return;
            }

            lblEmpty.Visible = false;
            grid.Visible = true;
            grid.DataSource = rows;

            foreach (DataGridViewColumn col in grid.Columns)
                col.Visible = false;

            void Show(string name, string header, float w = 1f)
            {
                if (grid.Columns[name] is not DataGridViewColumn c) return;
                c.Visible = true;
                c.HeaderText = header;
                c.FillWeight = w;
            }

            Show(nameof(UserListRow.UserID), "ID", 0.4f);
            Show(nameof(UserListRow.Username), "Username", 1.0f);
            Show(nameof(UserListRow.FullName), "Full name", 1.4f);
            Show(nameof(UserListRow.RoleLabel), "Role", 1.0f);
            Show(nameof(UserListRow.Status), "Status", 0.6f);

            if (grid.Columns.Contains("colEdit")) grid.Columns.Remove("colEdit");
            grid.Columns.Add(AppIcons.CreateEditColumn(0.4f));
            AppIcons.FillActionIcons(grid);

            grid.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "colDeactivate",
                HeaderText = "",
                Text = "Deactivate",
                UseColumnTextForButtonValue = true,
                FillWeight = 0.7f,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = { ForeColor = UiTheme.Warning, Font = UiTheme.LabelSemibold }
            });

            lblCount.Text = rows.Count == 1 ? "1 user" : $"{rows.Count} users";
        }
        catch (UnauthorizedException ex)
        {
            grid.Visible = false;
            lblEmpty.Visible = true;
            lblEmpty.Text = ex.Message;
            lblCount.Text = "";
        }
        catch (Exception ex)
        {
            grid.Visible = false;
            lblEmpty.Visible = true;
            lblEmpty.Text = "Unable to load users.\n" + ex.Message;
            lblCount.Text = "";
        }
    }

    private void Grid_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        var col = grid.Columns[e.ColumnIndex];
        if (grid.Rows[e.RowIndex].DataBoundItem is not UserListRow row) return;

        if (col.Name == "colEdit")
        {
            using var dlg = new UserEditForm(row.UserID);
            if (dlg.ShowDialog(FindForm()) == DialogResult.OK)
                ReloadGrid();
            return;
        }

        if (col.Name == "colDeactivate")
        {
            if (!row.IsActive)
            {
                MessageBox.Show(FindForm(), "User is already inactive.", "Users",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                FindForm(),
                $"Deactivate user \"{row.Username}\"?",
                "Deactivate User",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                _service.Deactivate(row.UserID);
                MessageBox.Show(FindForm(), "User deactivated.", "Users",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadGrid();
            }
            catch (AppException ex)
            {
                MessageBox.Show(FindForm(), ex.Message, "Users",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
