using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Users;

partial class UserListForm
{
    private System.ComponentModel.IContainer components = null;
    private Panel toolbar;
    private TextBox txtSearch;
    private ComboBox cboStatus;
    private Button btnAdd;
    private Button btnRefresh;
    private DataGridView grid;
    private Label lblCount;
    private Label lblEmpty;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        toolbar = new Panel();
        txtSearch = new TextBox();
        cboStatus = new ComboBox();
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
        toolbar.Controls.Add(txtSearch);
        toolbar.Controls.Add(cboStatus);
        toolbar.Controls.Add(btnRefresh);
        toolbar.Controls.Add(btnAdd);
        toolbar.Dock = DockStyle.Top;
        toolbar.Location = new Point(0, 0);
        toolbar.Name = "toolbar";
        toolbar.Size = new Size(0, 48);
        toolbar.TabIndex = 7;
        toolbar.Resize += Toolbar_Resize;
        // 
        // txtSearch
        // 
        txtSearch.Location = new Point(0, 8);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "Search username or name…";
        txtSearch.Size = new Size(220, 30);
        txtSearch.TabIndex = 0;
        txtSearch.TextChanged += TxtSearch_TextChanged;
        // 
        // cboStatus
        // 
        cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cboStatus.Items.AddRange(new object[] { "All statuses", "Active", "Inactive" });
        cboStatus.Location = new Point(232, 8);
        cboStatus.Name = "cboStatus";
        cboStatus.Size = new Size(140, 31);
        cboStatus.TabIndex = 1;
        cboStatus.SelectedIndexChanged += CboStatus_SelectedIndexChanged;
        // 
        // btnRefresh
        // 
        btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefresh.Location = new Point(-200, 0);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(88, 30);
        btnRefresh.TabIndex = 2;
        btnRefresh.Text = "Refresh";
        btnRefresh.Click += BtnRefresh_Click;
        // 
        // btnAdd
        // 
        btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAdd.Location = new Point(-200, 0);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(110, 30);
        btnAdd.TabIndex = 3;
        btnAdd.Text = "+ Add User";
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
        grid.TabIndex = 4;
        grid.CellContentClick += Grid_CellContentClick;
        // 
        // lblCount
        // 
        lblCount.Dock = DockStyle.Bottom;
        lblCount.Location = new Point(0, -28);
        lblCount.Name = "lblCount";
        lblCount.Size = new Size(0, 28);
        lblCount.TabIndex = 6;
        // 
        // lblEmpty
        // 
        lblEmpty.Dock = DockStyle.Fill;
        lblEmpty.Location = new Point(0, 48);
        lblEmpty.Name = "lblEmpty";
        lblEmpty.Size = new Size(0, 0);
        lblEmpty.TabIndex = 5;
        lblEmpty.Text = "No users found.";
        lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
        lblEmpty.Visible = false;
        // 
        // UserListForm
        // 
        AutoScaleDimensions = new SizeF(9F, 23F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(244, 246, 248);
        ClientSize = new Size(0, 0);
        Controls.Add(grid);
        Controls.Add(lblEmpty);
        Controls.Add(lblCount);
        Controls.Add(toolbar);
        Font = new Font("Segoe UI", 10F);
        FormBorderStyle = FormBorderStyle.None;
        Name = "UserListForm";
        Text = "Users";
        toolbar.ResumeLayout(false);
        toolbar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(false);
    }
}
