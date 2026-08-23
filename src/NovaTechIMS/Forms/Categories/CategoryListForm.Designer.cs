using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Categories;

partial class CategoryListForm
{
    private System.ComponentModel.IContainer components = null;

    private Panel toolbar;
    private TextBox txtSearch;
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
        toolbar.BackColor = Color.FromArgb(244, 246, 248);
        toolbar.Controls.Add(txtSearch);
        toolbar.Controls.Add(cboStatus);
        toolbar.Controls.Add(btnClearFilters);
        toolbar.Controls.Add(btnRefresh);
        toolbar.Controls.Add(btnAdd);
        toolbar.Dock = DockStyle.Top;
        toolbar.Location = new Point(0, 0);
        toolbar.Name = "toolbar";
        toolbar.Padding = new Padding(0, 4, 0, 4);
        toolbar.Size = new Size(900, 48);
        toolbar.TabIndex = 8;
        toolbar.Resize += Toolbar_Resize;
        // 
        // txtSearch
        // 
        txtSearch.Location = new Point(0, 8);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "Search by name…";
        txtSearch.Size = new Size(220, 27);
        txtSearch.TabIndex = 0;
        txtSearch.TextChanged += TxtSearch_TextChanged;
        // 
        // cboStatus
        // 
        cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cboStatus.Items.AddRange(new object[] { "All statuses", "Active", "Inactive" });
        cboStatus.Location = new Point(232, 8);
        cboStatus.Name = "cboStatus";
        cboStatus.Size = new Size(140, 28);
        cboStatus.TabIndex = 1;
        cboStatus.SelectedIndexChanged += CboStatus_SelectedIndexChanged;
        // 
        // btnClearFilters
        // 
        btnClearFilters.Location = new Point(384, 6);
        btnClearFilters.Name = "btnClearFilters";
        btnClearFilters.Size = new Size(100, 30);
        btnClearFilters.TabIndex = 2;
        btnClearFilters.Text = "Clear filters";
        btnClearFilters.Visible = false;
        btnClearFilters.Click += BtnClearFilters_Click;
        // 
        // btnRefresh
        // 
        btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefresh.Location = new Point(700, 8);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(88, 30);
        btnRefresh.TabIndex = 3;
        btnRefresh.Text = "Refresh";
        btnRefresh.Click += BtnRefresh_Click;
        // 
        // btnAdd
        // 
        btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAdd.Location = new Point(796, 8);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(130, 30);
        btnAdd.TabIndex = 4;
        btnAdd.Text = "+ Add Category";
        btnAdd.Click += BtnAdd_Click;
        // 
        // grid
        // 
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.ColumnHeadersHeight = 34;
        grid.Dock = DockStyle.Fill;
        grid.Location = new Point(0, 48);
        grid.MultiSelect = false;
        grid.Name = "grid";
        grid.ReadOnly = true;
        grid.RowHeadersVisible = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.Size = new Size(900, 424);
        grid.TabIndex = 5;
        grid.CellContentClick += Grid_CellContentClick;
        // 
        // lblCount
        // 
        lblCount.Dock = DockStyle.Bottom;
        lblCount.Font = new Font("Segoe UI", 9F);
        lblCount.ForeColor = Color.FromArgb(107, 119, 133);
        lblCount.Location = new Point(0, 472);
        lblCount.Name = "lblCount";
        lblCount.Size = new Size(900, 28);
        lblCount.TabIndex = 7;
        lblCount.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblEmpty
        // 
        lblEmpty.Dock = DockStyle.Fill;
        lblEmpty.Font = new Font("Segoe UI", 10F);
        lblEmpty.ForeColor = Color.FromArgb(107, 119, 133);
        lblEmpty.Location = new Point(0, 48);
        lblEmpty.Name = "lblEmpty";
        lblEmpty.Size = new Size(900, 424);
        lblEmpty.TabIndex = 6;
        lblEmpty.Text = "No categories yet. Add your first category to get started.";
        lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
        lblEmpty.Visible = false;
        // 
        // CategoryListForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(244, 246, 248);
        ClientSize = new Size(900, 500);
        Controls.Add(grid);
        Controls.Add(lblEmpty);
        Controls.Add(lblCount);
        Controls.Add(toolbar);
        Font = new Font("Segoe UI", 10F);
        FormBorderStyle = FormBorderStyle.None;
        Name = "CategoryListForm";
        Text = "Categories";
        toolbar.ResumeLayout(false);
        toolbar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(false);
    }
}
