using System;
using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Categories;

/// <summary>
/// SCR-006 Category List (Milestone 5).
/// Hosted inside MainForm content area (TopLevel = false).
/// </summary>
public class CategoryListForm : Form
{
    private readonly CategoryService _service = new();

    private TextBox txtSearch = null!;
    private ComboBox cboStatus = null!;
    private Button btnClearFilters = null!;
    private Button btnAdd = null!;
    private Button btnRefresh = null!;
    private DataGridView grid = null!;
    private Label lblCount = null!;
    private Label lblEmpty = null!;

    public CategoryListForm()
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

        var toolbar = new Panel
        {
            Dock = DockStyle.Top,
            Height = 48,
            BackColor = UiTheme.Background,
            Padding = new Padding(0, 4, 0, 4)
        };

        txtSearch = UiTheme.StyleTextBox(new TextBox
        {
            Width = 220,
            Height = 28,
            Location = new Point(0, 8),
            PlaceholderText = "Search by name…"
        });
        txtSearch.TextChanged += (_, _) => ReloadGrid();

        cboStatus = UiTheme.StyleComboBox(new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Width = 140,
            Location = new Point(232, 8)
        });
        cboStatus.Items.AddRange(new object[] { "All statuses", "Active", "Inactive" });
        cboStatus.SelectedIndex = 0;
        cboStatus.SelectedIndexChanged += (_, _) => ReloadGrid();

        btnClearFilters = UiTheme.StyleButton(new Button
        {
            Text = "Clear filters",
            Location = new Point(384, 6),
            Size = new Size(100, 30),
            Visible = false
        }, UiTheme.ButtonKind.Ghost);
        btnClearFilters.Click += (_, _) =>
        {
            txtSearch.Clear();
            cboStatus.SelectedIndex = 0;
            ReloadGrid();
        };

        btnRefresh = UiTheme.StyleButton(new Button
        {
            Text = "Refresh",
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Size = new Size(88, 30)
        }, UiTheme.ButtonKind.Secondary);
        btnRefresh.Click += (_, _) => ReloadGrid();

        btnAdd = UiTheme.StyleButton(new Button
        {
            Text = "+ Add Category",
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Size = new Size(130, 30)
        }, UiTheme.ButtonKind.Primary);
        btnAdd.Click += BtnAdd_Click;

        toolbar.Controls.Add(txtSearch);
        toolbar.Controls.Add(cboStatus);
        toolbar.Controls.Add(btnClearFilters);
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
            ReadOnly = true,
            MultiSelect = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        };
        UiTheme.ApplyGridTheme(grid);
        UiTheme.WireStatusBadgeColumn(grid, "Status", value => (value?.ToString() ?? "") switch
        {
            "Active" => ("Active", UiTheme.BadgeTone.Success, '\u2713'),
            _ => ("Inactive", UiTheme.BadgeTone.Muted, '\u2715')
        });
        grid.CellContentClick += Grid_CellContentClick;

        lblEmpty = new Label
        {
            Text = "No categories yet. Add your first category to get started.",
            Font = UiTheme.Body,
            ForeColor = UiTheme.TextMuted,
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Fill,
            Visible = false
        };

        lblCount = new Label
        {
            Dock = DockStyle.Bottom,
            Height = 28,
            Font = UiTheme.Label,
            ForeColor = UiTheme.TextMuted,
            TextAlign = ContentAlignment.MiddleLeft,
            Text = ""
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
            bool? activeFilter = cboStatus.SelectedIndex switch
            {
                1 => true,
                2 => false,
                _ => null
            };

            btnClearFilters.Visible = search is not null || activeFilter is not null;

            var rows = _service.GetList(search, activeFilter);

            grid.DataSource = null;
            grid.Columns.Clear();

            if (rows.Count == 0)
            {
                grid.Visible = false;
                lblEmpty.Visible = true;
                lblEmpty.Text = search is not null || activeFilter is not null
                    ? "No categories match your search/filters."
                    : "No categories yet. Add your first category to get started.";
                lblCount.Text = "0 categories";
                return;
            }

            lblEmpty.Visible = false;
            grid.Visible = true;

            grid.DataSource = rows;
            ConfigureColumns();
            lblCount.Text = rows.Count == 1 ? "1 category" : $"{rows.Count} categories";
        }
        catch (Exception ex)
        {
            grid.Visible = false;
            lblEmpty.Visible = true;
            lblEmpty.Text = "Unable to load categories. Check the database connection.\n" + ex.Message;
            lblCount.Text = "";
        }
    }

    private void ConfigureColumns()
    {
        foreach (DataGridViewColumn col in grid.Columns)
            col.Visible = false;

        void Show(string name, string header, float fillWeight = 1f)
        {
            if (grid.Columns[name] is not DataGridViewColumn c)
                return;
            c.Visible = true;
            c.HeaderText = header;
            c.FillWeight = fillWeight;
        }

        Show(nameof(CategoryListRow.CategoryID), "ID", 0.5f);
        Show(nameof(CategoryListRow.CategoryName), "Name", 1.5f);
        Show(nameof(CategoryListRow.Description), "Description", 2f);
        Show(nameof(CategoryListRow.ProductCount), "Products", 0.7f);
        Show(nameof(CategoryListRow.Status), "Status", 0.7f);

        if (grid.Columns.Contains("colEdit"))
            grid.Columns.Remove("colEdit");
        if (grid.Columns.Contains("colDelete"))
            grid.Columns.Remove("colDelete");

        var colEdit = new DataGridViewButtonColumn
        {
            Name = "colEdit",
            HeaderText = "",
            Text = "Edit",
            UseColumnTextForButtonValue = true,
            FillWeight = 0.5f,
            FlatStyle = FlatStyle.Flat,
            DefaultCellStyle = { ForeColor = UiTheme.Primary, Font = UiTheme.LabelSemibold }
        };
        var colDelete = new DataGridViewButtonColumn
        {
            Name = "colDelete",
            HeaderText = "",
            Text = "Delete",
            UseColumnTextForButtonValue = true,
            FillWeight = 0.5f,
            FlatStyle = FlatStyle.Flat,
            DefaultCellStyle = { ForeColor = UiTheme.Error, Font = UiTheme.LabelSemibold }
        };
        grid.Columns.Add(colEdit);
        grid.Columns.Add(colDelete);
    }

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        using var dlg = new CategoryEditForm();
        if (dlg.ShowDialog(FindForm()) == DialogResult.OK)
            ReloadGrid();
    }

    private void Grid_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
            return;

        var col = grid.Columns[e.ColumnIndex];
        if (grid.Rows[e.RowIndex].DataBoundItem is not CategoryListRow row)
            return;

        if (col.Name == "colEdit")
        {
            using var dlg = new CategoryEditForm(row.CategoryID);
            if (dlg.ShowDialog(FindForm()) == DialogResult.OK)
                ReloadGrid();
            return;
        }

        if (col.Name == "colDelete")
            HandleDelete(row);
    }

    private void HandleDelete(CategoryListRow row)
    {
        var confirm = MessageBox.Show(
            FindForm(),
            $"Delete category \"{row.CategoryName}\"?",
            "Delete Category",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes)
            return;

        try
        {
            _service.DeleteOrThrowIfReferenced(row.CategoryID);
            MessageBox.Show(FindForm(), "Category deleted.", "Categories",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReloadGrid();
        }
        catch (BusinessRuleException ex)
        {
            var result = MessageBox.Show(
                FindForm(),
                ex.Message + "\n\nMark this category Inactive instead?",
                "Cannot Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _service.Deactivate(row.CategoryID);
                    MessageBox.Show(FindForm(), "Category marked Inactive.", "Categories",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReloadGrid();
                }
                catch (Exception deactivateEx)
                {
                    MessageBox.Show(FindForm(), deactivateEx.Message, "Categories",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        catch (AppException ex)
        {
            MessageBox.Show(FindForm(), ex.Message, "Categories",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show(FindForm(), "Delete failed: " + ex.Message, "Categories",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>Called by MainForm when the user re-selects Categories.</summary>
    public void RefreshList() => ReloadGrid();
}
