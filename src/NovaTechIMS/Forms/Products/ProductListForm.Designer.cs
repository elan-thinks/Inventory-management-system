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
        components = new System.ComponentModel.Container();
        toolbar = new Panel();
        txtSearch = new TextBox();
        cboCategory = new ComboBox();
        cboSupplier = new ComboBox();
        cboStock = new ComboBox();
        cboStatus = new ComboBox();
        btnClearFilters = new Button();
        btnAdd = new Button();
        btnRefresh = new Button();
        grid = new DataGridView();
        lblCount = new Label();
        lblEmpty = new Label();
        toolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        SuspendLayout();

        AutoScaleMode = AutoScaleMode.Font;
        Font = UiTheme.Body;
        BackColor = UiTheme.Background;
        FormBorderStyle = FormBorderStyle.None;
        Dock = DockStyle.Fill;
        TopLevel = false;
        Name = "ProductListForm";
        Text = "Products";

        toolbar.Dock = DockStyle.Top;
        toolbar.Height = 84;
        toolbar.BackColor = UiTheme.Background;
        toolbar.Name = "toolbar";
        toolbar.Resize += Toolbar_Resize;

        txtSearch.Width = 200;
        txtSearch.Location = new Point(0, 8);
        txtSearch.PlaceholderText = "Search by name or ID…";
        txtSearch.Name = "txtSearch";
        txtSearch.TabIndex = 0;
        txtSearch.TextChanged += TxtSearch_TextChanged;

        cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCategory.Location = new Point(210, 8);
        cboCategory.Width = 160;
        cboCategory.Font = UiTheme.Body;
        cboCategory.Name = "cboCategory";
        cboCategory.TabIndex = 1;
        cboCategory.SelectedIndexChanged += CboFilter_SelectedIndexChanged;

        cboSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
        cboSupplier.Location = new Point(380, 8);
        cboSupplier.Width = 160;
        cboSupplier.Font = UiTheme.Body;
        cboSupplier.Name = "cboSupplier";
        cboSupplier.TabIndex = 2;
        cboSupplier.SelectedIndexChanged += CboFilter_SelectedIndexChanged;

        cboStock.DropDownStyle = ComboBoxStyle.DropDownList;
        cboStock.Location = new Point(550, 8);
        cboStock.Width = 130;
        cboStock.Font = UiTheme.Body;
        cboStock.Name = "cboStock";
        cboStock.TabIndex = 3;
        cboStock.SelectedIndexChanged += CboFilter_SelectedIndexChanged;

        cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cboStatus.Location = new Point(690, 8);
        cboStatus.Width = 120;
        cboStatus.Font = UiTheme.Body;
        cboStatus.Name = "cboStatus";
        cboStatus.TabIndex = 4;
        cboStatus.SelectedIndexChanged += CboFilter_SelectedIndexChanged;

        btnClearFilters.Text = "Clear filters";
        btnClearFilters.Location = new Point(0, 44);
        btnClearFilters.Size = new Size(100, 28);
        btnClearFilters.Visible = false;
        btnClearFilters.Name = "btnClearFilters";
        btnClearFilters.TabIndex = 5;
        btnClearFilters.Click += BtnClearFilters_Click;

        btnRefresh.Text = "Refresh";
        btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefresh.Size = new Size(88, 30);
        btnRefresh.Location = new Point(600, 44);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.TabIndex = 6;
        btnRefresh.Click += BtnRefresh_Click;

        btnAdd.Text = "+ Add Product";
        btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAdd.Size = new Size(130, 30);
        btnAdd.Location = new Point(696, 44);
        btnAdd.Name = "btnAdd";
        btnAdd.TabIndex = 7;
        btnAdd.Click += BtnAdd_Click;

        toolbar.Controls.Add(txtSearch);
        toolbar.Controls.Add(cboCategory);
        toolbar.Controls.Add(cboSupplier);
        toolbar.Controls.Add(cboStock);
        toolbar.Controls.Add(cboStatus);
        toolbar.Controls.Add(btnClearFilters);
        toolbar.Controls.Add(btnRefresh);
        toolbar.Controls.Add(btnAdd);

        grid.Dock = DockStyle.Fill;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.ReadOnly = true;
        grid.MultiSelect = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.Name = "grid";
        grid.TabIndex = 8;
        grid.CellContentClick += Grid_CellContentClick;

        lblEmpty.Text = "No products yet. Add your first product to get started.";
        lblEmpty.Font = UiTheme.Body;
        lblEmpty.ForeColor = UiTheme.TextMuted;
        lblEmpty.AutoSize = false;
        lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
        lblEmpty.Dock = DockStyle.Fill;
        lblEmpty.Visible = false;
        lblEmpty.Name = "lblEmpty";

        lblCount.Dock = DockStyle.Bottom;
        lblCount.Height = 28;
        lblCount.Font = UiTheme.Label;
        lblCount.ForeColor = UiTheme.TextMuted;
        lblCount.TextAlign = ContentAlignment.MiddleLeft;
        lblCount.Name = "lblCount";

        Controls.Add(grid);
        Controls.Add(lblEmpty);
        Controls.Add(lblCount);
        Controls.Add(toolbar);

        toolbar.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(false);
    }
}
