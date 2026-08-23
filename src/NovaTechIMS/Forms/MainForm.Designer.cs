using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    private Panel pnlSidebar;
    private Label lblBrand;
    private FlowLayoutPanel flpNav;
    private Button btnLogout;
    private Panel pnlMain;
    private Panel pnlHeader;
    private Label lblBreadcrumb;
    private Label lblScreenTitle;
    private Label lblRoleBadge;
    private Panel pnlContent;
    private Label lblPlaceholderTitle;
    private Label lblPlaceholderBody;
    private StatusStrip statusStrip;
    private ToolStripStatusLabel lblUserStatus;
    private ToolStripStatusLabel lblDateStatus;
    private ToolStripStatusLabel lblSpacer;
    private ToolStripStatusLabel lblMessageStatus;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        pnlSidebar = new Panel();
        flpNav = new FlowLayoutPanel();
        btnLogout = new Button();
        lblBrand = new Label();
        pnlMain = new Panel();
        pnlContent = new Panel();
        lblPlaceholderTitle = new Label();
        lblPlaceholderBody = new Label();
        pnlHeader = new Panel();
        lblBreadcrumb = new Label();
        lblScreenTitle = new Label();
        lblRoleBadge = new Label();
        statusStrip = new StatusStrip();
        lblUserStatus = new ToolStripStatusLabel();
        lblDateStatus = new ToolStripStatusLabel();
        lblSpacer = new ToolStripStatusLabel();
        lblMessageStatus = new ToolStripStatusLabel();
        pnlSidebar.SuspendLayout();
        pnlMain.SuspendLayout();
        pnlContent.SuspendLayout();
        pnlHeader.SuspendLayout();
        statusStrip.SuspendLayout();
        SuspendLayout();
        // 
        // pnlSidebar
        // 
        pnlSidebar.BackColor = Color.FromArgb(22, 50, 79);
        pnlSidebar.Controls.Add(flpNav);
        pnlSidebar.Controls.Add(btnLogout);
        pnlSidebar.Controls.Add(lblBrand);
        pnlSidebar.Dock = DockStyle.Left;
        pnlSidebar.Location = new Point(0, 0);
        pnlSidebar.Name = "pnlSidebar";
        pnlSidebar.Size = new Size(220, 721);
        pnlSidebar.TabIndex = 2;
        // 
        // flpNav
        // 
        flpNav.AutoScroll = true;
        flpNav.BackColor = Color.FromArgb(22, 50, 79);
        flpNav.Dock = DockStyle.Fill;
        flpNav.FlowDirection = FlowDirection.TopDown;
        flpNav.Location = new Point(0, 56);
        flpNav.Name = "flpNav";
        flpNav.Padding = new Padding(0, 8, 0, 8);
        flpNav.Size = new Size(220, 621);
        flpNav.TabIndex = 0;
        flpNav.WrapContents = false;
        // 
        // btnLogout
        // 
        btnLogout.AccessibleName = "Sign Out";
        btnLogout.BackColor = Color.FromArgb(22, 50, 79);
        btnLogout.Cursor = Cursors.Hand;
        btnLogout.Dock = DockStyle.Bottom;
        btnLogout.FlatAppearance.BorderSize = 0;
        btnLogout.FlatStyle = FlatStyle.Flat;
        btnLogout.Font = new Font("Segoe UI", 9.5F);
        btnLogout.ForeColor = Color.FromArgb(199, 211, 224);
        btnLogout.Location = new Point(0, 677);
        btnLogout.Name = "btnLogout";
        btnLogout.Size = new Size(220, 44);
        btnLogout.TabIndex = 1;
        btnLogout.Text = "  Sign Out";
        btnLogout.TextAlign = ContentAlignment.MiddleLeft;
        btnLogout.UseVisualStyleBackColor = false;
        btnLogout.Click += BtnLogout_Click;
        // 
        // lblBrand
        // 
        lblBrand.Dock = DockStyle.Top;
        lblBrand.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblBrand.ForeColor = Color.White;
        lblBrand.Location = new Point(0, 0);
        lblBrand.Name = "lblBrand";
        lblBrand.Padding = new Padding(8, 0, 0, 0);
        lblBrand.Size = new Size(220, 56);
        lblBrand.TabIndex = 2;
        lblBrand.Text = "  NovaTech IMS";
        lblBrand.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // pnlMain
        // 
        pnlMain.BackColor = Color.FromArgb(244, 246, 248);
        pnlMain.Controls.Add(pnlContent);
        pnlMain.Controls.Add(pnlHeader);
        pnlMain.Dock = DockStyle.Fill;
        pnlMain.Location = new Point(220, 0);
        pnlMain.Name = "pnlMain";
        pnlMain.Size = new Size(880, 695);
        pnlMain.TabIndex = 0;
        // 
        // pnlContent
        // 
        pnlContent.BackColor = Color.FromArgb(244, 246, 248);
        pnlContent.Controls.Add(lblPlaceholderTitle);
        pnlContent.Controls.Add(lblPlaceholderBody);
        pnlContent.Dock = DockStyle.Fill;
        pnlContent.Location = new Point(0, 72);
        pnlContent.Name = "pnlContent";
        pnlContent.Padding = new Padding(32);
        pnlContent.Size = new Size(880, 623);
        pnlContent.TabIndex = 0;
        // 
        // lblPlaceholderTitle
        // 
        lblPlaceholderTitle.AutoSize = true;
        lblPlaceholderTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblPlaceholderTitle.ForeColor = Color.FromArgb(31, 41, 51);
        lblPlaceholderTitle.Location = new Point(32, 40);
        lblPlaceholderTitle.Name = "lblPlaceholderTitle";
        lblPlaceholderTitle.Size = new Size(237, 32);
        lblPlaceholderTitle.TabIndex = 0;
        lblPlaceholderTitle.Text = "Welcome some one";
        lblPlaceholderTitle.Click += lblPlaceholderTitle_Click;
        // 
        // lblPlaceholderBody
        // 
        lblPlaceholderBody.Font = new Font("Segoe UI", 10F);
        lblPlaceholderBody.ForeColor = Color.FromArgb(107, 119, 133);
        lblPlaceholderBody.Location = new Point(32, 57);
        lblPlaceholderBody.MaximumSize = new Size(700, 0);
        lblPlaceholderBody.Name = "lblPlaceholderBody";
        lblPlaceholderBody.Size = new Size(700, 80);
        lblPlaceholderBody.TabIndex = 1;
        // 
        // pnlHeader
        // 
        pnlHeader.BackColor = Color.White;
        pnlHeader.Controls.Add(lblBreadcrumb);
        pnlHeader.Controls.Add(lblScreenTitle);
        pnlHeader.Controls.Add(lblRoleBadge);
        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Location = new Point(0, 0);
        pnlHeader.Name = "pnlHeader";
        pnlHeader.Padding = new Padding(24, 12, 24, 12);
        pnlHeader.Size = new Size(880, 72);
        pnlHeader.TabIndex = 1;
        // 
        // lblBreadcrumb
        // 
        lblBreadcrumb.AutoSize = true;
        lblBreadcrumb.Font = new Font("Segoe UI", 9F);
        lblBreadcrumb.ForeColor = Color.FromArgb(107, 119, 133);
        lblBreadcrumb.Location = new Point(24, 12);
        lblBreadcrumb.Name = "lblBreadcrumb";
        lblBreadcrumb.Size = new Size(50, 20);
        lblBreadcrumb.TabIndex = 0;
        lblBreadcrumb.Text = "Home";
        // 
        // lblScreenTitle
        // 
        lblScreenTitle.AutoSize = true;
        lblScreenTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblScreenTitle.ForeColor = Color.FromArgb(31, 41, 51);
        lblScreenTitle.Location = new Point(24, 32);
        lblScreenTitle.Name = "lblScreenTitle";
        lblScreenTitle.Size = new Size(138, 32);
        lblScreenTitle.TabIndex = 1;
        lblScreenTitle.Text = "Dashboard";
        // 
        // lblRoleBadge
        // 
        lblRoleBadge.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblRoleBadge.BackColor = Color.FromArgb(231, 240, 247);
        lblRoleBadge.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblRoleBadge.ForeColor = Color.FromArgb(30, 75, 143);
        lblRoleBadge.Location = new Point(1580, 28);
        lblRoleBadge.Name = "lblRoleBadge";
        lblRoleBadge.Size = new Size(44, 20);
        lblRoleBadge.TabIndex = 2;
        lblRoleBadge.Text = "v1.3";
        lblRoleBadge.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // statusStrip
        // 
        statusStrip.BackColor = Color.FromArgb(238, 241, 244);
        statusStrip.Font = new Font("Segoe UI", 9F);
        statusStrip.ImageScalingSize = new Size(20, 20);
        statusStrip.Items.AddRange(new ToolStripItem[] { lblUserStatus, lblDateStatus, lblSpacer, lblMessageStatus });
        statusStrip.Location = new Point(220, 695);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new Size(880, 26);
        statusStrip.TabIndex = 1;
        // 
        // lblUserStatus
        // 
        lblUserStatus.Name = "lblUserStatus";
        lblUserStatus.Size = new Size(38, 20);
        lblUserStatus.Text = "User";
        // 
        // lblDateStatus
        // 
        lblDateStatus.BorderSides = ToolStripStatusLabelBorderSides.Left;
        lblDateStatus.BorderStyle = Border3DStyle.Etched;
        lblDateStatus.Name = "lblDateStatus";
        lblDateStatus.Size = new Size(4, 20);
        // 
        // lblSpacer
        // 
        lblSpacer.Name = "lblSpacer";
        lblSpacer.Size = new Size(773, 20);
        lblSpacer.Spring = true;
        // 
        // lblMessageStatus
        // 
        lblMessageStatus.Name = "lblMessageStatus";
        lblMessageStatus.Size = new Size(50, 20);
        lblMessageStatus.Text = "Ready";
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(9F, 23F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(244, 246, 248);
        ClientSize = new Size(1100, 721);
        Controls.Add(pnlMain);
        Controls.Add(statusStrip);
        Controls.Add(pnlSidebar);
        Font = new Font("Segoe UI", 10F);
        MinimumSize = new Size(1024, 768);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "NovaTech IMS";
        Load += MainForm_Load;
        pnlSidebar.ResumeLayout(false);
        pnlMain.ResumeLayout(false);
        pnlContent.ResumeLayout(false);
        pnlContent.PerformLayout();
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
