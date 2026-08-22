using System;
using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Users;

/// <summary>User Management list (FR-USR, Administrator only).</summary>
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
            BackgroundColor = UiTheme.Surface,
            BorderStyle = BorderStyle.FixedSingle,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,
            ReadOnly = true,
            MultiSelect = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            RowHeadersVisible = false,
            Font = UiTheme.Body,
            ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Font = UiTheme.Label,
                BackColor = UiTheme.StatusStripBackground,
                ForeColor = UiTheme.Text
            },
            EnableHeadersVisualStyles = false
        };
        grid.CellContentClick += Grid_CellContentClick;

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

            grid.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "colEdit",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                FillWeight = 0.5f,
                FlatStyle = FlatStyle.Flat
            });
            grid.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "colDeactivate",
                Text = "Deactivate",
                UseColumnTextForButtonValue = true,
                FillWeight = 0.7f,
                FlatStyle = FlatStyle.Flat
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
