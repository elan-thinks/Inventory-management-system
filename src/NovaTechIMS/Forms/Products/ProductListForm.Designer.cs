using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Products;

partial class ProductListForm
{
    private System.ComponentModel.IContainer components = null;

    private Panel toolbar;
    private TextBox txtSearch;
    private ComboBox cboCategory;
    private ComboBox cboSupplier;
    private ComboBox cboStock;
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
        txtSearch = new TextBox();
        cboCategory = new ComboBox();
        cboSupplier = new ComboBox();
        cboStock = new ComboBox();
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
        // 
        // toolbar
        // 
        toolbar.BackColor = Color.FromArgb(15, 20, 25);
        toolbar.Controls.Add(txtSearch);
        toolbar.Controls.Add(cboCategory);
        toolbar.Controls.Add(cboSupplier);
        toolbar.Controls.Add(cboStock);
        toolbar.Controls.Add(cboStatus);
        toolbar.Controls.Add(btnClearFilters);
        toolbar.Controls.Add(btnRefresh);
        toolbar.Controls.Add(btnAdd);
        toolbar.Dock = DockStyle.Top;
        toolbar.Location = new Point(0, 0);
        toolbar.Name = "toolbar";
        toolbar.Size = new Size(1318, 84);
        toolbar.TabIndex = 11;
        toolbar.Resize += Toolbar_Resize;
        // 
        // txtSearch
        // 
        txtSearch.Location = new Point(0, 8);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "Search by name or ID…";
        txtSearch.Size = new Size(200, 30);
        txtSearch.TabIndex = 0;
        txtSearch.TextChanged += TxtSearch_TextChanged;
        // 
        // cboCategory
        // 
        cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCategory.Font = new Font("Segoe UI", 10F);
        cboCategory.Location = new Point(210, 8);
        cboCategory.Name = "cboCategory";
        cboCategory.Size = new Size(160, 31);
        cboCategory.TabIndex = 1;
        cboCategory.SelectedIndexChanged += CboFilter_SelectedIndexChanged;
        // 
        // cboSupplier
        // 
        cboSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
        cboSupplier.Font = new Font("Segoe UI", 10F);
        cboSupplier.Location = new Point(380, 8);
        cboSupplier.Name = "cboSupplier";
        cboSupplier.Size = new Size(160, 31);
        cboSupplier.TabIndex = 2;
        cboSupplier.SelectedIndexChanged += CboFilter_SelectedIndexChanged;
        // 
        // cboStock
        // 
        cboStock.DropDownStyle = ComboBoxStyle.DropDownList;
        cboStock.Font = new Font("Segoe UI", 10F);
        cboStock.Location = new Point(550, 8);
        cboStock.Name = "cboStock";
        cboStock.Size = new Size(130, 31);
        cboStock.TabIndex = 3;
        cboStock.SelectedIndexChanged += CboFilter_SelectedIndexChanged;
        // 
        // cboStatus
        // 
        cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cboStatus.Font = new Font("Segoe UI", 10F);
        cboStatus.Location = new Point(690, 8);
        cboStatus.Name = "cboStatus";
        cboStatus.Size = new Size(120, 31);
        cboStatus.TabIndex = 4;
        cboStatus.SelectedIndexChanged += CboFilter_SelectedIndexChanged;
        // 
        // btnClearFilters
        // 
        btnClearFilters.Location = new Point(0, 44);
        btnClearFilters.Name = "btnClearFilters";
        btnClearFilters.Size = new Size(100, 28);
        btnClearFilters.TabIndex = 5;
        btnClearFilters.Text = "Clear filters";
        btnClearFilters.Visible = false;
        btnClearFilters.Click += BtnClearFilters_Click;
        // 
        // btnRefresh
        // 
        btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefresh.Location = new Point(1718, 44);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(88, 30);
        btnRefresh.TabIndex = 6;
        btnRefresh.Text = "Refresh";
        btnRefresh.Click += BtnRefresh_Click;
        // 
        // btnAdd
        // 
        btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAdd.Location = new Point(1814, 44);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(130, 30);
        btnAdd.TabIndex = 7;
        btnAdd.Text = "+ Add Product";
        btnAdd.Click += BtnAdd_Click;
        // 
        // grid
        // 
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.ColumnHeadersHeight = 29;
        grid.Dock = DockStyle.Fill;
        grid.Location = new Point(0, 84);
        grid.MultiSelect = false;
        grid.Name = "grid";
        grid.ReadOnly = true;
        grid.RowHeadersWidth = 51;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.Size = new Size(1318, 476);
        grid.TabIndex = 8;
        grid.CellContentClick += Grid_CellContentClick;
        // 
        // lblCount
        // 
        lblCount.Dock = DockStyle.Bottom;
        lblCount.Font = new Font("Segoe UI", 9F);
        lblCount.ForeColor = Color.FromArgb(139, 155, 180);
        lblCount.Location = new Point(0, 560);
        lblCount.Name = "lblCount";
        lblCount.Size = new Size(1318, 28);
        lblCount.TabIndex = 10;
        lblCount.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblEmpty
        // 
        lblEmpty.Dock = DockStyle.Fill;
        lblEmpty.Font = new Font("Segoe UI", 10F);
        lblEmpty.ForeColor = Color.FromArgb(139, 155, 180);
        lblEmpty.Location = new Point(0, 84);
        lblEmpty.Name = "lblEmpty";
        lblEmpty.Size = new Size(1318, 476);
        lblEmpty.TabIndex = 9;
        lblEmpty.Text = "No products yet. Add your first product to get started.";
        lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
        lblEmpty.Visible = false;
        // 
        // ProductListForm
        // 
        AutoScaleDimensions = new SizeF(9F, 23F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(15, 20, 25);
        ClientSize = new Size(1318, 588);
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
