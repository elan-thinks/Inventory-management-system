using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Users;

/// <summary>User list layout — full controls on design surface.</summary>
partial class UserListForm
{
    private System.ComponentModel.IContainer components = null;
    private Panel toolbar;
    private Label lblSearch;
    private TextBox txtSearch;
    private Label lblStatus;
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
        lblSearch = new Label();
        txtSearch = new TextBox();
        lblStatus = new Label();
        cboStatus = new ComboBox();
        btnAdd = new Button();
        btnRefresh = new Button();
        grid = new DataGridView();
        lblCount = new Label();
        lblEmpty = new Label();
        toolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        SuspendLayout();

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.FromArgb(244, 246, 248);
        ClientSize = new Size(900, 500);
        FormBorderStyle = FormBorderStyle.None;
        Name = "UserListForm";
        Text = "Users";

        toolbar.BackColor = Color.FromArgb(244, 246, 248);
        toolbar.Dock = DockStyle.Top;
        toolbar.Height = 72;
        toolbar.Name = "toolbar";
        toolbar.Padding = new Padding(8, 6, 8, 6);
        toolbar.Resize += Toolbar_Resize;

        lblSearch.AutoSize = true;
        lblSearch.Font = new Font("Segoe UI", 8.25F);
        lblSearch.ForeColor = Color.FromArgb(107, 119, 133);
        lblSearch.Location = new Point(8, 6);
        lblSearch.Name = "lblSearch";
        lblSearch.Text = "Search";

        txtSearch.BorderStyle = BorderStyle.FixedSingle;
        txtSearch.Location = new Point(8, 24);
        txtSearch.Size = new Size(220, 25);
        txtSearch.PlaceholderText = "Search username or name…";
        txtSearch.Name = "txtSearch";
        txtSearch.TabIndex = 0;
        txtSearch.TextChanged += TxtSearch_TextChanged;

        lblStatus.AutoSize = true;
        lblStatus.Font = new Font("Segoe UI", 8.25F);
        lblStatus.ForeColor = Color.FromArgb(107, 119, 133);
        lblStatus.Location = new Point(240, 6);
        lblStatus.Name = "lblStatus";
        lblStatus.Text = "Status";

        cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cboStatus.Location = new Point(240, 24);
        cboStatus.Size = new Size(140, 25);
        cboStatus.Name = "cboStatus";
        cboStatus.TabIndex = 1;
        cboStatus.Items.AddRange(new object[] { "All statuses", "Active", "Inactive" });
        cboStatus.SelectedIndexChanged += CboStatus_SelectedIndexChanged;

        btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefresh.BackColor = Color.White;
        btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(215, 220, 225);
        btnRefresh.FlatStyle = FlatStyle.Flat;
        btnRefresh.Text = "Refresh";
        btnRefresh.Size = new Size(88, 32);
        btnRefresh.Location = new Point(680, 20);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.TabIndex = 2;
        btnRefresh.UseVisualStyleBackColor = false;
        btnRefresh.Click += BtnRefresh_Click;

        btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAdd.BackColor = Color.FromArgb(30, 75, 143);
        btnAdd.FlatAppearance.BorderSize = 0;
        btnAdd.FlatStyle = FlatStyle.Flat;
        btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnAdd.ForeColor = Color.White;
        btnAdd.Text = "+ Add User";
        btnAdd.Size = new Size(110, 32);
        btnAdd.Location = new Point(776, 20);
        btnAdd.Name = "btnAdd";
        btnAdd.TabIndex = 3;
        btnAdd.UseVisualStyleBackColor = false;
        btnAdd.Click += BtnAdd_Click;

        toolbar.Controls.Add(lblSearch);
        toolbar.Controls.Add(txtSearch);
        toolbar.Controls.Add(lblStatus);
        toolbar.Controls.Add(cboStatus);
        toolbar.Controls.Add(btnRefresh);
        toolbar.Controls.Add(btnAdd);

        grid.Dock = DockStyle.Fill;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AllowUserToResizeRows = false;
        grid.ReadOnly = true;
        grid.MultiSelect = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.RowHeadersVisible = false;
        grid.BackgroundColor = Color.White;
        grid.BorderStyle = BorderStyle.FixedSingle;
        grid.ColumnHeadersHeight = 34;
        grid.Name = "grid";
        grid.TabIndex = 4;
        grid.CellContentClick += Grid_CellContentClick;
        grid.ColumnCount = 5;
        grid.Columns[0].HeaderText = "ID";
        grid.Columns[1].HeaderText = "Username";
        grid.Columns[2].HeaderText = "Full name";
        grid.Columns[3].HeaderText = "Role";
        grid.Columns[4].HeaderText = "Status";

        lblEmpty.Text = "No users found.";
        lblEmpty.Dock = DockStyle.Fill;
        lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
        lblEmpty.ForeColor = Color.FromArgb(107, 119, 133);
        lblEmpty.Visible = false;
        lblEmpty.Name = "lblEmpty";

        lblCount.BackColor = Color.FromArgb(244, 246, 248);
        lblCount.Dock = DockStyle.Bottom;
        lblCount.Height = 28;
        lblCount.Padding = new Padding(8, 0, 0, 0);
        lblCount.Text = "0 users";
        lblCount.ForeColor = Color.FromArgb(107, 119, 133);
        lblCount.Name = "lblCount";
        lblCount.TextAlign = ContentAlignment.MiddleLeft;

        Controls.Add(grid);
        Controls.Add(lblEmpty);
        Controls.Add(lblCount);
        Controls.Add(toolbar);

        toolbar.ResumeLayout(false);
        toolbar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(false);
    }
}
