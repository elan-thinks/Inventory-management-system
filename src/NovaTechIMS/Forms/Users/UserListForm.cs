using System;
using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Users;

/// <summary>User Management list — M19 headers, badges, self-deactivate disabled.</summary>
public class UserListForm : Form
{
    private readonly UserService _service = new();

    private TextBox txtSearch = null!;
    private ComboBox cboStatus = null!;
    private Button btnAdd = null!;
    private Button btnRefresh = null!;
    private DataGridView grid = null!;
    private Label lblCount = null!;
    private Label lblEmpty = null!;

    public UserListForm()
    {
        BuildUi();
        Load += (_, _) => ReloadGrid();
    }

    private void BuildUi()
    {
        AutoScaleMode = AutoScaleMode.Font;
        Font = UiTheme.Body;
        BackColor = UiTheme.Background;
        FormBorderStyle = FormBorderStyle.None;
        Dock = DockStyle.Fill;
        TopLevel = false;

        var toolbar = new Panel { Dock = DockStyle.Top, Height = 48, BackColor = UiTheme.Background };

        txtSearch = new TextBox
        {
            Width = 220,
            Location = new Point(0, 8),
            PlaceholderText = "Search username or name…",
            Font = UiTheme.Body
        };
        txtSearch.TextChanged += (_, _) => ReloadGrid();

        cboStatus = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Width = 140,
            Location = new Point(232, 8),
            Font = UiTheme.Body
        };
        cboStatus.Items.AddRange(new object[] { "All statuses", "Active", "Inactive" });
        cboStatus.SelectedIndex = 0;
        cboStatus.SelectedIndexChanged += (_, _) => ReloadGrid();

        btnRefresh = new Button
        {
            Text = "Refresh",
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Size = new Size(88, 30),
            FlatStyle = FlatStyle.Flat,
            Font = UiTheme.Label,
            BackColor = UiTheme.Surface
        };
        btnRefresh.FlatAppearance.BorderColor = UiTheme.Border;
        btnRefresh.Click += (_, _) => ReloadGrid();

        btnAdd = new Button
        {
            Text = "+ Add User",
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Size = new Size(110, 30),
            FlatStyle = FlatStyle.Flat,
            Font = UiTheme.Button,
            BackColor = UiTheme.Primary,
            ForeColor = Color.White,
            Cursor = Cursors.Hand
        };
        btnAdd.FlatAppearance.BorderSize = 0;
        btnAdd.Click += (_, _) =>
        {
            using var dlg = new UserEditForm();
            if (dlg.ShowDialog(FindForm()) == DialogResult.OK)
                ReloadGrid();
        };

        toolbar.Controls.Add(txtSearch);
        toolbar.Controls.Add(cboStatus);
        toolbar.Controls.Add(btnRefresh);
        toolbar.Controls.Add(btnAdd);
        toolbar.Resize += (_, _) =>
        {
            btnAdd.Left = toolbar.ClientSize.Width - btnAdd.Width;
            btnRefresh.Left = btnAdd.Left - btnRefresh.Width - 8;
        };

        grid = new DataGridView
        {
            Dock = DockStyle.Fill,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,
            ReadOnly = true,
            MultiSelect = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            RowHeadersVisible = false
        };
        GridStyles.Apply(grid);
        grid.CellContentClick += Grid_CellContentClick;
        grid.CellFormatting += Grid_CellFormattingSelfDeactivate;

        lblEmpty = new Label
        {
            Text = "No users found.",
            Font = UiTheme.Body,
            ForeColor = UiTheme.TextMuted,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            Visible = false
        };

        lblCount = new Label
        {
            Dock = DockStyle.Bottom,
            Height = 28,
            Font = UiTheme.Label,
            ForeColor = UiTheme.TextMuted
        };

        Controls.Add(grid);
        Controls.Add(lblEmpty);
        Controls.Add(lblCount);
        Controls.Add(toolbar);
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

            grid.Columns.Add(GridStyles.ActionButton("colEdit", "Edit", 0.5f));
            grid.Columns.Add(GridStyles.ActionButton("colDeactivate", "Deactivate", 0.7f));

            GridStyles.ApplyStatusFormatting(grid,
                nameof(UserListRow.RoleLabel),
                nameof(UserListRow.Status));

            lblCount.Text = rows.Count == 1 ? "1 user" : $"{rows.Count} users";
        }
        catch (Exception ex)
        {
            grid.Visible = false;
            lblEmpty.Visible = true;
            lblEmpty.Text = ErrorPresenter.ToUserMessage(DbExceptionMapper.Map(ex));
            lblCount.Text = "";
        }
    }

    private void Grid_CellFormattingSelfDeactivate(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || grid.Columns[e.ColumnIndex].Name != "colDeactivate")
            return;
        if (grid.Rows[e.RowIndex].DataBoundItem is not UserListRow row)
            return;

        var currentId = SessionContext.Current?.UserID ?? -1;
        if (row.UserID == currentId)
        {
            e.CellStyle.ForeColor = UiTheme.DisabledText;
            e.CellStyle.BackColor = UiTheme.DisabledBackground;
            e.CellStyle.SelectionForeColor = UiTheme.DisabledText;
            e.CellStyle.SelectionBackColor = UiTheme.DisabledBackground;
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
            var currentId = SessionContext.Current?.UserID ?? -1;
            if (row.UserID == currentId)
            {
                MessageBox.Show(FindForm(),
                    "You cannot deactivate your own account while logged in.",
                    "Users",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

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
            catch (Exception ex)
            {
                ErrorPresenter.Show(FindForm(), DbExceptionMapper.Map(ex));
            }
        }
    }
}
