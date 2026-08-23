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
        components = new System.ComponentModel.Container();
        toolbar = new Panel();
        cboStatus = new ComboBox();
        cboResponsibility = new ComboBox();
        btnNew = new Button();
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
        Name = "DelegationListForm";
        Text = "Delegations";

        toolbar.Dock = DockStyle.Top;
        toolbar.Height = 48;
        toolbar.Name = "toolbar";
        toolbar.Resize += Toolbar_Resize;

        cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cboStatus.Location = new Point(0, 8);
        cboStatus.Size = new Size(130, 25);
        cboStatus.Name = "cboStatus";
        cboStatus.TabIndex = 0;
        cboStatus.Items.AddRange(new object[] { "All statuses", "Active", "Expired", "Revoked" });
        cboStatus.SelectedIndex = 0;
        cboStatus.SelectedIndexChanged += CboFilter_SelectedIndexChanged;

        cboResponsibility.DropDownStyle = ComboBoxStyle.DropDownList;
        cboResponsibility.Location = new Point(140, 8);
        cboResponsibility.Size = new Size(140, 25);
        cboResponsibility.Name = "cboResponsibility";
        cboResponsibility.TabIndex = 1;
        cboResponsibility.Items.AddRange(new object[] { "All responsibilities", "Stock-In", "Stock-Out", "Report Access" });
        cboResponsibility.SelectedIndex = 0;
        cboResponsibility.SelectedIndexChanged += CboFilter_SelectedIndexChanged;

        btnRefresh.Text = "Refresh";
        btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefresh.Size = new Size(88, 30);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.TabIndex = 2;
        btnRefresh.Click += BtnRefresh_Click;

        btnNew.Text = "+ New Delegation";
        btnNew.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnNew.Size = new Size(140, 30);
        btnNew.Name = "btnNew";
        btnNew.TabIndex = 3;
        btnNew.Click += BtnNew_Click;

        toolbar.Controls.Add(cboStatus);
        toolbar.Controls.Add(cboResponsibility);
        toolbar.Controls.Add(btnRefresh);
        toolbar.Controls.Add(btnNew);

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

        lblEmpty.Text = "No delegations yet. Create one to temporarily extend a Staff member's access.";
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
