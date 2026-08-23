using System;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Categories;

/// <summary>
/// SCR-006 Category List — Designer-based; design-time safe constructor.
/// </summary>
public partial class CategoryListForm : Form
{
    private CategoryService? _service;
    private CategoryService Service => _service ??= new CategoryService();

    public CategoryListForm()
    {
        InitializeComponent();

        if (DesignTime.IsActive)
            return;

        ApplyRuntimeStyling();
        Load += (_, _) => ReloadGrid();
    }

    private void ApplyRuntimeStyling()
    {
        UiTheme.StyleTextBox(txtSearch);
        UiTheme.StyleComboBox(cboStatus);
        UiTheme.StyleButton(btnClearFilters, UiTheme.ButtonKind.Ghost);
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

    private void BtnClearFilters_Click(object? sender, EventArgs e)
    {
        txtSearch.Clear();
        cboStatus.SelectedIndex = 0;
        ReloadGrid();
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

            var rows = Service.GetList(search, activeFilter);

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

        grid.Columns.Add(AppIcons.CreateEditColumn(0.4f));
        grid.Columns.Add(AppIcons.CreateDeleteColumn(0.4f));
        AppIcons.FillActionIcons(grid);
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
            Service.DeleteOrThrowIfReferenced(row.CategoryID);
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
                    Service.Deactivate(row.CategoryID);
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

    public void RefreshList() => ReloadGrid();
}
