using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Delegations;

/// <summary>Delegation list layout — full controls on design surface.</summary>
partial class DelegationListForm
{
    private System.ComponentModel.IContainer components = null;
    private Panel toolbar;
    private Label lblStatus;
    private ComboBox cboStatus;
    private Label lblResp;
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
        lblStatus = new Label();
        cboStatus = new ComboBox();
        lblResp = new Label();
        cboResponsibility = new ComboBox();
        btnNew = new Button();
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
        Name = "DelegationListForm";
        Text = "Delegations";

        toolbar.BackColor = Color.FromArgb(244, 246, 248);
        toolbar.Dock = DockStyle.Top;
        toolbar.Height = 72;
        toolbar.Name = "toolbar";
        toolbar.Padding = new Padding(8, 6, 8, 6);
        toolbar.Resize += Toolbar_Resize;

        lblStatus.AutoSize = true;
        lblStatus.Font = new Font("Segoe UI", 8.25F);
        lblStatus.ForeColor = Color.FromArgb(107, 119, 133);
        lblStatus.Location = new Point(8, 6);
        lblStatus.Name = "lblStatus";
        lblStatus.Text = "Status";

        cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cboStatus.Location = new Point(8, 24);
        cboStatus.Size = new Size(130, 25);
        cboStatus.Name = "cboStatus";
        cboStatus.TabIndex = 0;
        cboStatus.Items.AddRange(new object[] { "All statuses", "Active", "Expired", "Revoked" });
        cboStatus.SelectedIndexChanged += CboFilter_SelectedIndexChanged;

        lblResp.AutoSize = true;
        lblResp.Font = new Font("Segoe UI", 8.25F);
        lblResp.ForeColor = Color.FromArgb(107, 119, 133);
        lblResp.Location = new Point(150, 6);
        lblResp.Name = "lblResp";
        lblResp.Text = "Responsibility";

        cboResponsibility.DropDownStyle = ComboBoxStyle.DropDownList;
        cboResponsibility.Location = new Point(150, 24);
        cboResponsibility.Size = new Size(150, 25);
        cboResponsibility.Name = "cboResponsibility";
        cboResponsibility.TabIndex = 1;
        cboResponsibility.Items.AddRange(new object[] { "All responsibilities", "Stock-In", "Stock-Out", "Report Access" });
        cboResponsibility.SelectedIndexChanged += CboFilter_SelectedIndexChanged;

        btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefresh.BackColor = Color.White;
        btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(215, 220, 225);
        btnRefresh.FlatStyle = FlatStyle.Flat;
        btnRefresh.Text = "Refresh";
        btnRefresh.Size = new Size(88, 32);
        btnRefresh.Location = new Point(650, 20);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.TabIndex = 2;
        btnRefresh.UseVisualStyleBackColor = false;
        btnRefresh.Click += BtnRefresh_Click;

        btnNew.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnNew.BackColor = Color.FromArgb(30, 75, 143);
        btnNew.FlatAppearance.BorderSize = 0;
        btnNew.FlatStyle = FlatStyle.Flat;
        btnNew.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnNew.ForeColor = Color.White;
        btnNew.Text = "+ New Delegation";
        btnNew.Size = new Size(140, 32);
        btnNew.Location = new Point(746, 20);
        btnNew.Name = "btnNew";
        btnNew.TabIndex = 3;
        btnNew.UseVisualStyleBackColor = false;
        btnNew.Click += BtnNew_Click;

        toolbar.Controls.Add(lblStatus);
        toolbar.Controls.Add(cboStatus);
        toolbar.Controls.Add(lblResp);
        toolbar.Controls.Add(cboResponsibility);
        toolbar.Controls.Add(btnRefresh);
        toolbar.Controls.Add(btnNew);

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
        grid.Columns[1].HeaderText = "To";
        grid.Columns[2].HeaderText = "Responsibility";
        grid.Columns[3].HeaderText = "Status";
        grid.Columns[4].HeaderText = "Reason";

        lblEmpty.Text = "No delegations yet. Create one to temporarily extend a Staff member's access.";
        lblEmpty.Dock = DockStyle.Fill;
        lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
        lblEmpty.ForeColor = Color.FromArgb(107, 119, 133);
        lblEmpty.Visible = false;
        lblEmpty.Name = "lblEmpty";

        lblCount.BackColor = Color.FromArgb(244, 246, 248);
        lblCount.Dock = DockStyle.Bottom;
        lblCount.Height = 28;
        lblCount.Padding = new Padding(8, 0, 0, 0);
        lblCount.Text = "0 delegations";
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
