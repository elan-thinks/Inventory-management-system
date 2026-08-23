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
        components = new System.ComponentModel.Container();
        toolbar = new Panel();
        txtSearch = new TextBox();
        cboStatus = new ComboBox();
        btnAdd = new Button();
        btnRefresh = new Button();
        grid = new DataGridView();
        lblCount = new Label();
        lblEmpty = new Label();
        toolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        SuspendLayout();

        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.FromArgb(244, 246, 248);
        FormBorderStyle = FormBorderStyle.None;
        Dock = DockStyle.Fill;
        TopLevel = false;
        Name = "UserListForm";
        Text = "Users";

        toolbar.Dock = DockStyle.Top;
        toolbar.Height = 48;
        toolbar.Name = "toolbar";
        toolbar.Resize += Toolbar_Resize;

        txtSearch.Location = new Point(0, 8);
        txtSearch.Size = new Size(220, 25);
        txtSearch.PlaceholderText = "Search username or name…";
        txtSearch.Name = "txtSearch";
        txtSearch.TabIndex = 0;
        txtSearch.TextChanged += TxtSearch_TextChanged;

        cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cboStatus.Location = new Point(232, 8);
        cboStatus.Size = new Size(140, 25);
        cboStatus.Name = "cboStatus";
        cboStatus.TabIndex = 1;
        cboStatus.Items.AddRange(new object[] { "All statuses", "Active", "Inactive" });
        cboStatus.SelectedIndex = 0;
        cboStatus.SelectedIndexChanged += CboStatus_SelectedIndexChanged;

        btnRefresh.Text = "Refresh";
        btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefresh.Size = new Size(88, 30);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.TabIndex = 2;
        btnRefresh.Click += BtnRefresh_Click;

        btnAdd.Text = "+ Add User";
        btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAdd.Size = new Size(110, 30);
        btnAdd.Name = "btnAdd";
        btnAdd.TabIndex = 3;
        btnAdd.Click += BtnAdd_Click;

        toolbar.Controls.Add(txtSearch);
        toolbar.Controls.Add(cboStatus);
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
        grid.TabIndex = 4;
        grid.CellContentClick += Grid_CellContentClick;

        lblEmpty.Text = "No users found.";
        lblEmpty.Dock = DockStyle.Fill;
        lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
        lblEmpty.Visible = false;
        lblEmpty.Name = "lblEmpty";

        lblCount.Dock = DockStyle.Bottom;
        lblCount.Height = 28;
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
