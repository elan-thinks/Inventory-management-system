using System;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Models.Enums;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Products;

/// <summary>
/// SCR-003 Product List — Designer-based (partial).
/// </summary>
public partial class ProductListForm : Form
{
    private readonly ProductService _service = new();
    private readonly CategoryService _categoryService = new();
    private readonly SupplierService _supplierService = new();

    public ProductListForm()
    {
        InitializeComponent();
        ApplyRuntimeStyling();
        LoadFilterLookups();
        Load += (_, _) => ReloadGrid();
    }

    private void ApplyRuntimeStyling()
    {
        UiTheme.StyleTextBox(txtSearch);
        UiTheme.StyleComboBox(cboCategory);
        UiTheme.StyleComboBox(cboSupplier);
        UiTheme.StyleComboBox(cboStock);
        UiTheme.StyleComboBox(cboStatus);
        UiTheme.StyleButton(btnClearFilters, UiTheme.ButtonKind.Ghost);
        UiTheme.StyleButton(btnRefresh, UiTheme.ButtonKind.Secondary);
        UiTheme.StyleButton(btnAdd, UiTheme.ButtonKind.Primary);
        UiTheme.ApplyGridTheme(grid);
        UiTheme.WireStatusBadgeColumn(grid, nameof(ProductListRow.StockStatusLabel), value => (value?.ToString() ?? "") switch
        {
            "In Stock" => ("In Stock", UiTheme.BadgeTone.Success, '\u2713'),
            "Low Stock" => ("Low Stock", UiTheme.BadgeTone.Warning, '!'),
            _ => ("Out of Stock", UiTheme.BadgeTone.Error, '\u2715')
        });
        UiTheme.WireStatusBadgeColumn(grid, nameof(ProductListRow.StatusLabel), value => (value?.ToString() ?? "") switch
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
    private void CboFilter_SelectedIndexChanged(object? sender, EventArgs e) => ReloadGrid();
    private void BtnRefresh_Click(object? sender, EventArgs e) => ReloadGrid();

    private void BtnClearFilters_Click(object? sender, EventArgs e)
    {
        txtSearch.Clear();
        cboCategory.SelectedIndex = 0;
        cboSupplier.SelectedIndex = 0;
        cboStock.SelectedIndex = 0;
        cboStatus.SelectedIndex = 0;
        ReloadGrid();
    }

    private void LoadFilterLookups()
    {
        cboCategory.Items.Clear();
        cboCategory.Items.Add(new FilterItem(null, "All categories"));
        foreach (var c in _categoryService.GetList(isActiveFilter: true))
            cboCategory.Items.Add(new FilterItem(c.CategoryID, c.CategoryName));
        cboCategory.SelectedIndex = 0;

        cboSupplier.Items.Clear();
        cboSupplier.Items.Add(new FilterItem(null, "All suppliers"));
        foreach (var s in _supplierService.GetList(isActiveFilter: true))
            cboSupplier.Items.Add(new FilterItem(s.SupplierID, s.SupplierName));
        cboSupplier.SelectedIndex = 0;

        cboStock.Items.Clear();
        cboStock.Items.Add(new StockFilterItem(null, "All stock"));
        cboStock.Items.Add(new StockFilterItem(StockStatus.InStock, "In Stock"));
        cboStock.Items.Add(new StockFilterItem(StockStatus.LowStock, "Low Stock"));
        cboStock.Items.Add(new StockFilterItem(StockStatus.OutOfStock, "Out of Stock"));
        cboStock.SelectedIndex = 0;

        cboStatus.Items.Clear();
        cboStatus.Items.AddRange(new object[] { "All statuses", "Active", "Inactive" });
        cboStatus.SelectedIndex = 0;
    }

    private void ReloadGrid()
    {
        try
        {
            string? search = string.IsNullOrWhiteSpace(txtSearch.Text) ? null : txtSearch.Text;
            int? categoryId = (cboCategory.SelectedItem as FilterItem)?.Id;
            int? supplierId = (cboSupplier.SelectedItem as FilterItem)?.Id;
            StockStatus? stock = (cboStock.SelectedItem as StockFilterItem)?.Status;
            bool? active = cboStatus.SelectedIndex switch
            {
                1 => true,
                2 => false,
                _ => null
            };

            var filtersOn = search is not null || categoryId is not null || supplierId is not null
                || stock is not null || active is not null;
            btnClearFilters.Visible = filtersOn;

            var rows = _service.GetList(search, categoryId, supplierId, stock, active);

            grid.DataSource = null;
            grid.Columns.Clear();

            if (rows.Count == 0)
            {
                grid.Visible = false;
                lblEmpty.Visible = true;
                lblEmpty.Text = filtersOn
                    ? "No products match your search/filters."
                    : "No products yet. Add your first product to get started.";
                lblCount.Text = "0 products";
                return;
            }

            lblEmpty.Visible = false;
            grid.Visible = true;
            grid.DataSource = rows;
            ConfigureColumns();
            lblCount.Text = rows.Count == 1 ? "1 product" : $"{rows.Count} products";
        }
        catch (Exception ex)
        {
            grid.Visible = false;
            lblEmpty.Visible = true;
            lblEmpty.Text = "Unable to load products. Check the database connection.\n" + ex.Message;
            lblCount.Text = "";
        }
    }

    private void ConfigureColumns()
    {
        foreach (DataGridViewColumn col in grid.Columns)
            col.Visible = false;

        void Show(string name, string header, float w = 1f)
        {
            if (grid.Columns[name] is not DataGridViewColumn c) return;
            c.Visible = true;
            c.HeaderText = header;
            c.FillWeight = w;
        }

        Show(nameof(ProductListRow.ProductID), "ID", 0.4f);
        Show(nameof(ProductListRow.ProductName), "Name", 1.4f);
        Show(nameof(ProductListRow.CategoryName), "Category", 1.0f);
        Show(nameof(ProductListRow.SupplierName), "Supplier", 1.0f);
        Show(nameof(ProductListRow.QuantityOnHand), "Qty", 0.5f);
        Show(nameof(ProductListRow.MinimumStockLevel), "Min", 0.4f);
        Show(nameof(ProductListRow.StockStatusLabel), "Stock", 0.8f);
        Show(nameof(ProductListRow.SellingPrice), "Sell", 0.6f);
        Show(nameof(ProductListRow.StatusLabel), "Status", 0.6f);

        if (grid.Columns.Contains("colEdit")) grid.Columns.Remove("colEdit");
        if (grid.Columns.Contains("colDelete")) grid.Columns.Remove("colDelete");

        grid.Columns.Add(new DataGridViewButtonColumn
        {
            Name = "colEdit",
            Text = "Edit",
            UseColumnTextForButtonValue = true,
            FillWeight = 0.45f,
            FlatStyle = FlatStyle.Flat,
            DefaultCellStyle = { ForeColor = UiTheme.Primary, Font = UiTheme.LabelSemibold }
        });
        grid.Columns.Add(new DataGridViewButtonColumn
        {
            Name = "colDelete",
            Text = "Delete",
            UseColumnTextForButtonValue = true,
            FillWeight = 0.5f,
            FlatStyle = FlatStyle.Flat,
            DefaultCellStyle = { ForeColor = UiTheme.Error, Font = UiTheme.LabelSemibold }
        });
    }

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        using var dlg = new ProductEditForm();
        if (dlg.ShowDialog(FindForm()) == DialogResult.OK)
            ReloadGrid();
    }

    private void Grid_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        var col = grid.Columns[e.ColumnIndex];
        if (grid.Rows[e.RowIndex].DataBoundItem is not ProductListRow row) return;

        if (col.Name == "colEdit")
        {
            using var dlg = new ProductEditForm(row.ProductID);
            if (dlg.ShowDialog(FindForm()) == DialogResult.OK)
                ReloadGrid();
            return;
        }

        if (col.Name == "colDelete")
            HandleDelete(row);
    }

    private void HandleDelete(ProductListRow row)
    {
        var confirm = MessageBox.Show(
            FindForm(),
            $"Delete product \"{row.ProductName}\"?",
            "Delete Product",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes) return;

        try
        {
            _service.DeleteOrThrowIfReferenced(row.ProductID);
            MessageBox.Show(FindForm(), "Product deleted.", "Products",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReloadGrid();
        }
        catch (BusinessRuleException ex)
        {
            var result = MessageBox.Show(
                FindForm(),
                ex.Message + "\n\nMark this product Inactive instead?",
                "Cannot Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _service.Deactivate(row.ProductID);
                    MessageBox.Show(FindForm(), "Product marked Inactive.", "Products",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReloadGrid();
                }
                catch (Exception de)
                {
                    MessageBox.Show(FindForm(), de.Message, "Products",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        catch (AppException ex)
        {
            MessageBox.Show(FindForm(), ex.Message, "Products",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show(FindForm(), "Delete failed: " + ex.Message, "Products",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private sealed class FilterItem
    {
        public int? Id { get; }
        public string Text { get; }
        public FilterItem(int? id, string text) { Id = id; Text = text; }
        public override string ToString() => Text;
    }

    private sealed class StockFilterItem
    {
        public StockStatus? Status { get; }
        public string Text { get; }
        public StockFilterItem(StockStatus? status, string text) { Status = status; Text = text; }
        public override string ToString() => Text;
    }
}
