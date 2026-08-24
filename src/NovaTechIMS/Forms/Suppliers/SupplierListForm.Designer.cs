using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Suppliers;

/// <summary>
/// Supplier list layout — full controls on the design surface (drag/drop).
/// </summary>
partial class SupplierListForm
{
    private System.ComponentModel.IContainer components = null;

    private Panel toolbar;
    private Label lblSearch;
    private TextBox txtSearch;
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

        toolbar.BackColor = Color.FromArgb(244, 246, 248);
        toolbar.Controls.Add(lblSearch);
        toolbar.Controls.Add(txtSearch);
        toolbar.Controls.Add(lblStatus);
        toolbar.Controls.Add(cboStatus);
        toolbar.Controls.Add(btnClearFilters);
        toolbar.Controls.Add(btnRefresh);
        toolbar.Controls.Add(btnAdd);
        toolbar.Dock = DockStyle.Top;
        toolbar.Location = new Point(0, 0);
        toolbar.Name = "toolbar";
        toolbar.Padding = new Padding(8, 6, 8, 6);
        toolbar.Size = new Size(900, 72);
        toolbar.TabIndex = 0;
        toolbar.Resize += Toolbar_Resize;

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
        txtSearch.PlaceholderText = "Search by name…";
        txtSearch.Size = new Size(220, 25);
        txtSearch.TabIndex = 0;
        txtSearch.TextChanged += TxtSearch_TextChanged;

        lblStatus.AutoSize = true;
        lblStatus.Font = new Font("Segoe UI", 8.25F);
        lblStatus.ForeColor = Color.FromArgb(107, 119, 133);
        lblStatus.Location = new Point(240, 6);
        lblStatus.Name = "lblStatus";
        lblStatus.Text = "Status";

        cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cboStatus.Font = new Font("Segoe UI", 10F);
        cboStatus.Items.AddRange(new object[] { "All statuses", "Active", "Inactive" });
        cboStatus.Location = new Point(240, 24);
        cboStatus.Name = "cboStatus";
        cboStatus.Size = new Size(140, 25);
        cboStatus.TabIndex = 1;
        cboStatus.SelectedIndexChanged += CboStatus_SelectedIndexChanged;

        btnClearFilters.FlatStyle = FlatStyle.Flat;
        btnClearFilters.Font = new Font("Segoe UI", 9F);
        btnClearFilters.ForeColor = Color.FromArgb(30, 75, 143);
        btnClearFilters.Location = new Point(392, 22);
        btnClearFilters.Name = "btnClearFilters";
        btnClearFilters.Size = new Size(100, 30);
        btnClearFilters.TabIndex = 2;
        btnClearFilters.Text = "Clear filters";
        btnClearFilters.UseVisualStyleBackColor = true;
        btnClearFilters.Visible = true;
        btnClearFilters.Click += BtnClearFilters_Click;

        btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefresh.BackColor = Color.White;
        btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(215, 220, 225);
        btnRefresh.FlatStyle = FlatStyle.Flat;
        btnRefresh.Font = new Font("Segoe UI", 9F);
        btnRefresh.Location = new Point(680, 20);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(88, 32);
        btnRefresh.TabIndex = 3;
        btnRefresh.Text = "Refresh";
        btnRefresh.UseVisualStyleBackColor = false;
        btnRefresh.Click += BtnRefresh_Click;

        btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAdd.BackColor = Color.FromArgb(30, 75, 143);
        btnAdd.FlatAppearance.BorderSize = 0;
        btnAdd.FlatStyle = FlatStyle.Flat;
        btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnAdd.ForeColor = Color.White;
        btnAdd.Location = new Point(776, 20);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(116, 32);
        btnAdd.TabIndex = 4;
        btnAdd.Text = "+ Add Supplier";
        btnAdd.UseVisualStyleBackColor = false;
        btnAdd.Click += BtnAdd_Click;

        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AllowUserToResizeRows = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.BackgroundColor = Color.White;
        grid.BorderStyle = BorderStyle.FixedSingle;
        grid.ColumnHeadersHeight = 34;
        grid.Dock = DockStyle.Fill;
        grid.Location = new Point(0, 72);
        grid.MultiSelect = false;
        grid.Name = "grid";
        grid.ReadOnly = true;
        grid.RowHeadersVisible = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.Size = new Size(900, 400);
        grid.TabIndex = 1;
        grid.CellContentClick += Grid_CellContentClick;

        grid.ColumnCount = 6;
        grid.Columns[0].HeaderText = "ID";
        grid.Columns[1].HeaderText = "Name";
        grid.Columns[2].HeaderText = "Contact";
        grid.Columns[3].HeaderText = "Phone";
        grid.Columns[4].HeaderText = "Email";
        grid.Columns[5].HeaderText = "Status";

        lblEmpty.Dock = DockStyle.Fill;
        lblEmpty.Font = new Font("Segoe UI", 10F);
        lblEmpty.ForeColor = Color.FromArgb(107, 119, 133);
        lblEmpty.Location = new Point(0, 72);
        lblEmpty.Name = "lblEmpty";
        lblEmpty.Size = new Size(900, 400);
        lblEmpty.TabIndex = 2;
        lblEmpty.Text = "No suppliers yet. Add your first supplier to get started.";
        lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
        lblEmpty.Visible = false;

        lblCount.BackColor = Color.FromArgb(244, 246, 248);
        lblCount.Dock = DockStyle.Bottom;
        lblCount.Font = new Font("Segoe UI", 9F);
        lblCount.ForeColor = Color.FromArgb(107, 119, 133);
        lblCount.Location = new Point(0, 472);
        lblCount.Name = "lblCount";
        lblCount.Padding = new Padding(8, 0, 0, 0);
        lblCount.Size = new Size(900, 28);
        lblCount.TabIndex = 3;
        lblCount.Text = "0 suppliers";
        lblCount.TextAlign = ContentAlignment.MiddleLeft;

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
        Name = "SupplierListForm";
        Text = "Suppliers";

        toolbar.ResumeLayout(false);
        toolbar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(false);
    }
}
