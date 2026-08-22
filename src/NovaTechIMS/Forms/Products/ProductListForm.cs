using System;
using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Models.Enums;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Products;

/// <summary>SCR-003 Product List — M19 grid styling + badges + Clear filters.</summary>
public class ProductListForm : Form
{
    private readonly ProductService _service = new();
    private readonly CategoryService _categoryService = new();
    private readonly SupplierService _supplierService = new();

    private TextBox txtSearch = null!;
    private ComboBox cboCategory = null!;
    private ComboBox cboSupplier = null!;
    private ComboBox cboStock = null!;
    private ComboBox cboStatus = null!;
    private Button btnClearFilters = null!;
    private Button btnAdd = null!;
    private Button btnRefresh = null!;
    private DataGridView grid = null!;
    private Label lblCount = null!;
    private Label lblEmpty = null!;

    public ProductListForm()
    {
        BuildUi();
        LoadFilterLookups();
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
            Height = 84,
            BackColor = UiTheme.Background
        };

        txtSearch = new TextBox
        {
            Width = 200,
            Location = new Point(0, 8),
            PlaceholderText = "Search by name or ID…",
            Font = UiTheme.Body
        };
        txtSearch.TextChanged += (_, _) => ReloadGrid();

        cboCategory = MakeFilterCombo(210, 8, 160);
        cboSupplier = MakeFilterCombo(380, 8, 160);
        cboStock = MakeFilterCombo(550, 8, 130);
        cboStatus = MakeFilterCombo(690, 8, 120);

        cboCategory.SelectedIndexChanged += (_, _) => ReloadGrid();
        cboSupplier.SelectedIndexChanged += (_, _) => ReloadGrid();
        cboStock.SelectedIndexChanged += (_, _) => ReloadGrid();
        cboStatus.SelectedIndexChanged += (_, _) => ReloadGrid();

        btnClearFilters = new Button
        {
            Text = "Clear filters",
            Location = new Point(0, 44),
            Size = new Size(100, 28),
            FlatStyle = FlatStyle.Flat,
            Font = UiTheme.Label,
            ForeColor = UiTheme.Primary,
            Cursor = Cursors.Hand,
            Visible = false
        };
        btnClearFilters.FlatAppearance.BorderSize = 0;
        btnClearFilters.Click += (_, _) =>
        {
            txtSearch.Clear();
            cboCategory.SelectedIndex = 0;
            cboSupplier.SelectedIndex = 0;
            cboStock.SelectedIndex = 0;
            cboStatus.SelectedIndex = 0;
            ReloadGrid();
        };

        btnRefresh = new Button
        {
            Text = "Refresh",
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Size = new Size(88, 30),
            FlatStyle = FlatStyle.Flat,
            Font = UiTheme.Label,
            BackColor = UiTheme.Surface,
            Location = new Point(600, 44)
        };
        btnRefresh.FlatAppearance.BorderColor = UiTheme.Border;
        btnRefresh.Click += (_, _) => ReloadGrid();

        btnAdd = new Button
        {
            Text = "+ Add Product",
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Size = new Size(130, 30),
            FlatStyle = FlatStyle.Flat,
            Font = UiTheme.Button,
            BackColor = UiTheme.Primary,
            ForeColor = Color.White,
            Location = new Point(696, 44),
            Cursor = Cursors.Hand
        };
        btnAdd.FlatAppearance.BorderSize = 0;
        btnAdd.Click += BtnAdd_Click;

        toolbar.Controls.Add(txtSearch);
        toolbar.Controls.Add(cboCategory);
        toolbar.Controls.Add(cboSupplier);
        toolbar.Controls.Add(cboStock);
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
            AllowUserToResizeRows = false,
            ReadOnly = true,
            MultiSelect = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            RowHeadersVisible = false
        };
        GridStyles.Apply(grid);
        grid.CellContentClick += Grid_CellContentClick;

        lblEmpty = new Label
        {
            Text = "No products yet. Add your first product to get started.",
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
            TextAlign = ContentAlignment.MiddleLeft
        };

        Controls.Add(grid);
        Controls.Add(lblEmpty);
        Controls.Add(lblCount);
        Controls.Add(toolbar);
    }

    private static ComboBox MakeFilterCombo(int x, int y, int width)
    {
        return new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Location = new Point(x, y),
            Width = width,
            Font = UiTheme.Body
        };
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
                    ? "No products match your search or filters."
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
            lblEmpty.Text = "Unable to load products.\n" + ErrorPresenter.ToUserMessage(DbExceptionMapper.Map(ex));
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

        grid.Columns.Add(GridStyles.ActionButton("colEdit", "Edit", 0.45f));
        grid.Columns.Add(GridStyles.ActionButton("colDelete", "Delete", 0.5f));

        GridStyles.ApplyStatusFormatting(grid,
            nameof(ProductListRow.StockStatusLabel),
            nameof(ProductListRow.StatusLabel));
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
                    ErrorPresenter.Show(FindForm(), de);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorPresenter.Show(FindForm(), DbExceptionMapper.Map(ex));
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
