using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Utilities;

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
        components = new System.ComponentModel.Container();
        toolbar = new Panel();
        txtSearch = new TextBox();
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
        Name = "CategoryListForm";
        Text = "Categories";

        toolbar.Dock = DockStyle.Top;
        toolbar.Height = 48;
        toolbar.BackColor = UiTheme.Background;
        toolbar.Padding = new Padding(0, 4, 0, 4);
        toolbar.Name = "toolbar";
        toolbar.Resize += Toolbar_Resize;

        txtSearch.Width = 220;
        txtSearch.Height = 28;
        txtSearch.Location = new Point(0, 8);
        txtSearch.PlaceholderText = "Search by name…";
        txtSearch.Name = "txtSearch";
        txtSearch.TabIndex = 0;
        txtSearch.TextChanged += TxtSearch_TextChanged;

        cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cboStatus.Width = 140;
        cboStatus.Location = new Point(232, 8);
        cboStatus.Name = "cboStatus";
        cboStatus.TabIndex = 1;
        cboStatus.Items.AddRange(new object[] { "All statuses", "Active", "Inactive" });
        cboStatus.SelectedIndex = 0;
        cboStatus.SelectedIndexChanged += CboStatus_SelectedIndexChanged;

        btnClearFilters.Text = "Clear filters";
        btnClearFilters.Location = new Point(384, 6);
        btnClearFilters.Size = new Size(100, 30);
        btnClearFilters.Visible = false;
        btnClearFilters.Name = "btnClearFilters";
        btnClearFilters.TabIndex = 2;
        btnClearFilters.Click += BtnClearFilters_Click;

        btnRefresh.Text = "Refresh";
        btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefresh.Size = new Size(88, 30);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.TabIndex = 3;
        btnRefresh.Click += BtnRefresh_Click;

        btnAdd.Text = "+ Add Category";
        btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAdd.Size = new Size(130, 30);
        btnAdd.Name = "btnAdd";
        btnAdd.TabIndex = 4;
        btnAdd.Click += BtnAdd_Click;

        toolbar.Controls.Add(txtSearch);
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
        grid.TabIndex = 5;
        grid.CellContentClick += Grid_CellContentClick;

        lblEmpty.Text = "No categories yet. Add your first category to get started.";
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
        lblCount.Text = "";
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
