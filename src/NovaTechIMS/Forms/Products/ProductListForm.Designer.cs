using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Products;

/// <summary>
/// Product list layout — full controls on the design surface (drag/drop).
/// Filter data and grid rows load at runtime only.
/// </summary>
partial class ProductListForm
{
    private System.ComponentModel.IContainer components = null;

    private Panel toolbar;
    private Label lblSearch;
    private TextBox txtSearch;
    private Label lblCategory;
    private ComboBox cboCategory;
    private Label lblSupplier;
    private ComboBox cboSupplier;
    private Label lblStock;
    private ComboBox cboStock;
    private Label lblStatus;
    private ComboBox cboStatus;
    private Button btnClearFilters;
    private Button btnAdd;
    private Button btnRefresh;
    private DataGridView grid;
    private Label lblCount;
    private Label lblEmpty;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        toolbar = new Panel();
        lblSearch = new Label();
        txtSearch = new TextBox();
        lblCategory = new Label();
        cboCategory = new ComboBox();
        lblSupplier = new Label();
        cboSupplier = new ComboBox();
        lblStock = new Label();
        cboStock = new ComboBox();
        lblStatus = new Label();
        cboStatus = new ComboBox();
        btnClearFilters = new Button();
        btnRefresh = new Button();
        btnAdd = new Button();
        grid = new DataGridView();
        lblCount = new Label();
        lblEmpty = new Label();

        toolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        SuspendLayout();

        // ---- toolbar (two rows: filters + actions) ----
        toolbar.BackColor = Color.FromArgb(244, 246, 248);
        toolbar.Controls.Add(lblSearch);
        toolbar.Controls.Add(txtSearch);
        toolbar.Controls.Add(lblCategory);
        toolbar.Controls.Add(cboCategory);
        toolbar.Controls.Add(lblSupplier);
        toolbar.Controls.Add(cboSupplier);
        toolbar.Controls.Add(lblStock);
        toolbar.Controls.Add(cboStock);
        toolbar.Controls.Add(lblStatus);
        toolbar.Controls.Add(cboStatus);
        toolbar.Controls.Add(btnClearFilters);
        toolbar.Controls.Add(btnRefresh);
        toolbar.Controls.Add(btnAdd);
        toolbar.Dock = DockStyle.Top;
        toolbar.Location = new Point(0, 0);
        toolbar.Name = "toolbar";
        toolbar.Padding = new Padding(8, 6, 8, 6);
        toolbar.Size = new Size(1000, 96);
        toolbar.TabIndex = 0;
        toolbar.Resize += Toolbar_Resize;

        // Row 1 — filters
        lblSearch.AutoSize = true;
        lblSearch.Font = new Font("Segoe UI", 8.25F);
        lblSearch.ForeColor = Color.FromArgb(107, 119, 133);
        lblSearch.Location = new Point(8, 6);
        lblSearch.Name = "lblSearch";
        lblSearch.Text = "Search";

        txtSearch.BorderStyle = BorderStyle.FixedSingle;
        txtSearch.Font = new Font("Segoe UI", 10F);
        txtSearch.Location = new Point(8, 24);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "Search by name or ID…";
        txtSearch.Size = new Size(180, 25);
        txtSearch.TabIndex = 0;
        txtSearch.TextChanged += TxtSearch_TextChanged;

        lblCategory.AutoSize = true;
        lblCategory.Font = new Font("Segoe UI", 8.25F);
        lblCategory.ForeColor = Color.FromArgb(107, 119, 133);
        lblCategory.Location = new Point(198, 6);
        lblCategory.Name = "lblCategory";
        lblCategory.Text = "Category";

        cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCategory.Font = new Font("Segoe UI", 10F);
        cboCategory.Items.AddRange(new object[] { "Category: All" });
        cboCategory.Location = new Point(198, 24);
        cboCategory.Name = "cboCategory";
        cboCategory.Size = new Size(150, 25);
        cboCategory.TabIndex = 1;
        cboCategory.SelectedIndexChanged += CboFilter_SelectedIndexChanged;

        lblSupplier.AutoSize = true;
        lblSupplier.Font = new Font("Segoe UI", 8.25F);
        lblSupplier.ForeColor = Color.FromArgb(107, 119, 133);
        lblSupplier.Location = new Point(358, 6);
        lblSupplier.Name = "lblSupplier";
        lblSupplier.Text = "Supplier";

        cboSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
        cboSupplier.Font = new Font("Segoe UI", 10F);
        cboSupplier.Items.AddRange(new object[] { "Supplier: All" });
        cboSupplier.Location = new Point(358, 24);
        cboSupplier.Name = "cboSupplier";
        cboSupplier.Size = new Size(150, 25);
        cboSupplier.TabIndex = 2;
        cboSupplier.SelectedIndexChanged += CboFilter_SelectedIndexChanged;

        lblStock.AutoSize = true;
        lblStock.Font = new Font("Segoe UI", 8.25F);
        lblStock.ForeColor = Color.FromArgb(107, 119, 133);
        lblStock.Location = new Point(518, 6);
        lblStock.Name = "lblStock";
        lblStock.Text = "Stock";

        cboStock.DropDownStyle = ComboBoxStyle.DropDownList;
        cboStock.Font = new Font("Segoe UI", 10F);
        cboStock.Items.AddRange(new object[] { "Stock Status: All", "In Stock", "Low Stock", "Out of Stock" });
        cboStock.Location = new Point(518, 24);
        cboStock.Name = "cboStock";
        cboStock.Size = new Size(130, 25);
        cboStock.TabIndex = 3;
        cboStock.SelectedIndexChanged += CboFilter_SelectedIndexChanged;

        lblStatus.AutoSize = true;
        lblStatus.Font = new Font("Segoe UI", 8.25F);
        lblStatus.ForeColor = Color.FromArgb(107, 119, 133);
        lblStatus.Location = new Point(658, 6);
        lblStatus.Name = "lblStatus";
        lblStatus.Text = "Status";

        cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cboStatus.Font = new Font("Segoe UI", 10F);
        cboStatus.Items.AddRange(new object[] { "Status: All", "Active", "Inactive" });
        cboStatus.Location = new Point(658, 24);
        cboStatus.Name = "cboStatus";
        cboStatus.Size = new Size(120, 25);
        cboStatus.TabIndex = 4;
        cboStatus.SelectedIndexChanged += CboFilter_SelectedIndexChanged;

        // Row 2 — actions
        btnClearFilters.FlatStyle = FlatStyle.Flat;
        btnClearFilters.Font = new Font("Segoe UI", 9F);
        btnClearFilters.ForeColor = Color.FromArgb(30, 75, 143);
        btnClearFilters.Location = new Point(8, 58);
        btnClearFilters.Name = "btnClearFilters";
        btnClearFilters.Size = new Size(100, 30);
        btnClearFilters.TabIndex = 5;
        btnClearFilters.Text = "Clear filters";
        btnClearFilters.UseVisualStyleBackColor = true;
        btnClearFilters.Visible = true; // visible in designer; runtime toggles
        btnClearFilters.Click += BtnClearFilters_Click;

        btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefresh.BackColor = Color.White;
        btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(215, 220, 225);
        btnRefresh.FlatStyle = FlatStyle.Flat;
        btnRefresh.Font = new Font("Segoe UI", 9F);
        btnRefresh.Location = new Point(780, 56);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(88, 32);
        btnRefresh.TabIndex = 6;
        btnRefresh.Text = "Refresh";
        btnRefresh.UseVisualStyleBackColor = false;
        btnRefresh.Click += BtnRefresh_Click;

        btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAdd.BackColor = Color.FromArgb(30, 75, 143);
        btnAdd.FlatAppearance.BorderSize = 0;
        btnAdd.FlatStyle = FlatStyle.Flat;
        btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnAdd.ForeColor = Color.White;
        btnAdd.Location = new Point(876, 56);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(116, 32);
        btnAdd.TabIndex = 7;
        btnAdd.Text = "+ Add Product";
        btnAdd.UseVisualStyleBackColor = false;
        btnAdd.Click += BtnAdd_Click;

        // ---- grid ----
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AllowUserToResizeRows = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.BackgroundColor = Color.White;
        grid.BorderStyle = BorderStyle.FixedSingle;
        grid.ColumnHeadersHeight = 34;
        grid.Dock = DockStyle.Fill;
        grid.Location = new Point(0, 96);
        grid.MultiSelect = false;
        grid.Name = "grid";
        grid.ReadOnly = true;
        grid.RowHeadersVisible = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.Size = new Size(1000, 376);
        grid.TabIndex = 1;
        grid.CellContentClick += Grid_CellContentClick;

        // design-time sample columns so the grid is not empty in View Designer
        grid.ColumnCount = 5;
        grid.Columns[0].HeaderText = "Product ID";
        grid.Columns[1].HeaderText = "Name";
        grid.Columns[2].HeaderText = "Category";
        grid.Columns[3].HeaderText = "Qty";
        grid.Columns[4].HeaderText = "Status";

        // ---- empty / count ----
        lblEmpty.Dock = DockStyle.Fill;
        lblEmpty.Font = new Font("Segoe UI", 10F);
        lblEmpty.ForeColor = Color.FromArgb(107, 119, 133);
        lblEmpty.Location = new Point(0, 96);
        lblEmpty.Name = "lblEmpty";
        lblEmpty.Size = new Size(1000, 376);
        lblEmpty.TabIndex = 2;
        lblEmpty.Text = "No products yet. Add your first product to get started.";
        lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
        lblEmpty.Visible = false;

        lblCount.BackColor = Color.FromArgb(244, 246, 248);
        lblCount.Dock = DockStyle.Bottom;
        lblCount.Font = new Font("Segoe UI", 9F);
        lblCount.ForeColor = Color.FromArgb(107, 119, 133);
        lblCount.Location = new Point(0, 472);
        lblCount.Name = "lblCount";
        lblCount.Padding = new Padding(8, 0, 0, 0);
        lblCount.Size = new Size(1000, 28);
        lblCount.TabIndex = 3;
        lblCount.Text = "0 products";
        lblCount.TextAlign = ContentAlignment.MiddleLeft;

        // ---- form ----
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(244, 246, 248);
        ClientSize = new Size(1000, 500);
        Controls.Add(grid);
        Controls.Add(lblEmpty);
        Controls.Add(lblCount);
        Controls.Add(toolbar);
        Font = new Font("Segoe UI", 10F);
        FormBorderStyle = FormBorderStyle.None;
        Name = "ProductListForm";
        Text = "Products";

        toolbar.ResumeLayout(false);
        toolbar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(false);
    }
}
