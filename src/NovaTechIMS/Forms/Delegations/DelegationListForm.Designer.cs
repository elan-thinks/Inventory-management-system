using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Delegations;

partial class DelegationListForm
{
    private System.ComponentModel.IContainer components = null;
    private Panel toolbar;
    private ComboBox cboStatus;
    private ComboBox cboResponsibility;
    private Button btnNew;
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
        cboStatus = new ComboBox();
        cboResponsibility = new ComboBox();
        btnRefresh = new Button();
        btnNew = new Button();
        grid = new DataGridView();
        lblCount = new Label();
        lblEmpty = new Label();
        toolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        SuspendLayout();
        // 
        // toolbar
        // 
        toolbar.Controls.Add(cboStatus);
        toolbar.Controls.Add(cboResponsibility);
        toolbar.Controls.Add(btnRefresh);
        toolbar.Controls.Add(btnNew);
        toolbar.Dock = DockStyle.Top;
        toolbar.Location = new Point(0, 0);
        toolbar.Name = "toolbar";
        toolbar.Size = new Size(0, 48);
        toolbar.TabIndex = 7;
        toolbar.Resize += Toolbar_Resize;
        // 
        // cboStatus
        // 
        cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cboStatus.Items.AddRange(new object[] { "All statuses", "Active", "Expired", "Revoked" });
        cboStatus.Location = new Point(0, 8);
        cboStatus.Name = "cboStatus";
        cboStatus.Size = new Size(130, 31);
        cboStatus.TabIndex = 0;
        cboStatus.SelectedIndexChanged += CboFilter_SelectedIndexChanged;
        // 
        // cboResponsibility
        // 
        cboResponsibility.DropDownStyle = ComboBoxStyle.DropDownList;
        cboResponsibility.Items.AddRange(new object[] { "All responsibilities", "Stock-In", "Stock-Out", "Report Access" });
        cboResponsibility.Location = new Point(140, 8);
        cboResponsibility.Name = "cboResponsibility";
        cboResponsibility.Size = new Size(140, 31);
        cboResponsibility.TabIndex = 1;
        cboResponsibility.SelectedIndexChanged += CboFilter_SelectedIndexChanged;
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
        // btnNew
        // 
        btnNew.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnNew.Location = new Point(-200, 0);
        btnNew.Name = "btnNew";
        btnNew.Size = new Size(140, 30);
        btnNew.TabIndex = 3;
        btnNew.Text = "+ New Delegation";
        btnNew.Click += BtnNew_Click;
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
        lblEmpty.Text = "No delegations yet. Create one to temporarily extend a Staff member's access.";
        lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
        lblEmpty.Visible = false;
        // 
        // DelegationListForm
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
        Name = "DelegationListForm";
        Text = "Delegations";
        toolbar.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(false);
    }
}
