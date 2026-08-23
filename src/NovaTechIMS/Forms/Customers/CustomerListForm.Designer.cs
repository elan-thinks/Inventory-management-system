using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Customers;

partial class CustomerListForm
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
        toolbar.BackColor = Color.FromArgb(15, 20, 25);
        toolbar.Controls.Add(txtSearch);
        toolbar.Controls.Add(cboStatus);
        toolbar.Controls.Add(btnClearFilters);
        toolbar.Controls.Add(btnRefresh);
        toolbar.Controls.Add(btnAdd);
        toolbar.Dock = DockStyle.Top;
        toolbar.Location = new Point(0, 0);
        toolbar.Name = "toolbar";
        toolbar.Padding = new Padding(0, 4, 0, 4);
        toolbar.Size = new Size(0, 48);
        toolbar.TabIndex = 8;
        toolbar.Resize += Toolbar_Resize;
        // 
        // txtSearch
        // 
        txtSearch.BorderStyle = BorderStyle.FixedSingle;
        txtSearch.Location = new Point(0, 8);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "Search by name or phone…";
        txtSearch.Size = new Size(240, 30);
        txtSearch.TabIndex = 0;
        txtSearch.TextChanged += TxtSearch_TextChanged;
        // 
        // cboStatus
        // 
        cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cboStatus.FlatStyle = FlatStyle.Flat;
        cboStatus.Items.AddRange(new object[] { "All statuses", "Active", "Inactive" });
        cboStatus.Location = new Point(252, 8);
        cboStatus.Name = "cboStatus";
        cboStatus.Size = new Size(140, 31);
        cboStatus.TabIndex = 1;
        cboStatus.SelectedIndexChanged += CboStatus_SelectedIndexChanged;
        // 
        // btnClearFilters
        // 
        btnClearFilters.Location = new Point(404, 6);
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
        btnRefresh.Location = new Point(-200, 0);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(88, 30);
        btnRefresh.TabIndex = 3;
        btnRefresh.Text = "Refresh";
        btnRefresh.Click += BtnRefresh_Click;
        // 
        // btnAdd
        // 
        btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAdd.Location = new Point(-200, 0);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(130, 30);
        btnAdd.TabIndex = 4;
        btnAdd.Text = "+ Add Customer";
        btnAdd.Click += BtnAdd_Click;
        // 
        // grid
        // 
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.ColumnHeadersHeight = 29;
        grid.Dock = DockStyle.Fill;
        grid.Location = new Point(0, 48);
        grid.MultiSelect = false;
        grid.Name = "grid";
        grid.ReadOnly = true;
        grid.RowHeadersWidth = 51;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.Size = new Size(0, 0);
        grid.TabIndex = 5;
        grid.CellContentClick += Grid_CellContentClick;
        // 
        // lblCount
        // 
        lblCount.Dock = DockStyle.Bottom;
        lblCount.Font = new Font("Segoe UI", 9F);
        lblCount.ForeColor = Color.FromArgb(139, 155, 180);
        lblCount.Location = new Point(0, -28);
        lblCount.Name = "lblCount";
        lblCount.Size = new Size(0, 28);
        lblCount.TabIndex = 7;
        lblCount.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblEmpty
        // 
        lblEmpty.Dock = DockStyle.Fill;
        lblEmpty.Font = new Font("Segoe UI", 10F);
        lblEmpty.ForeColor = Color.FromArgb(139, 155, 180);
        lblEmpty.Location = new Point(0, 48);
        lblEmpty.Name = "lblEmpty";
        lblEmpty.Size = new Size(0, 0);
        lblEmpty.TabIndex = 6;
        lblEmpty.Text = "No items yet.";
        lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
        lblEmpty.Visible = false;
        // 
        // CustomerListForm
        // 
        AutoScaleDimensions = new SizeF(9F, 23F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(15, 20, 25);
        ClientSize = new Size(0, 0);
        Controls.Add(grid);
        Controls.Add(lblEmpty);
        Controls.Add(lblCount);
        Controls.Add(toolbar);
        Font = new Font("Segoe UI", 10F);
        FormBorderStyle = FormBorderStyle.None;
        Name = "CustomerListForm";
        Text = "Customers";
        toolbar.ResumeLayout(false);
        toolbar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(false);
    }
}
