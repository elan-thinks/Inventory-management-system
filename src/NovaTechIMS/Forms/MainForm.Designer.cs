using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms;

/// <summary>
/// Application shell layout — sidebar, header, content, status strip (View Designer).
/// Runtime navigation buttons are added in MainForm.cs (BuildNavigation).
/// </summary>
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

    private Label lblNavGroupCatalog;
    private Button btnNavDashboard;
    private Button btnNavProducts;
    private Button btnNavCategories;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        pnlSidebar = new Panel();
        lblBrand = new Label();
        flpNav = new FlowLayoutPanel();
        btnLogout = new Button();
        pnlMain = new Panel();
        pnlHeader = new Panel();
        lblBreadcrumb = new Label();
        lblScreenTitle = new Label();
        lblRoleBadge = new Label();
        pnlContent = new Panel();
        lblPlaceholderTitle = new Label();
        lblPlaceholderBody = new Label();
        statusStrip = new StatusStrip();
        lblUserStatus = new ToolStripStatusLabel();
        lblDateStatus = new ToolStripStatusLabel();
        lblSpacer = new ToolStripStatusLabel();
        lblMessageStatus = new ToolStripStatusLabel();
        lblNavGroupCatalog = new Label();
        btnNavDashboard = new Button();
        btnNavProducts = new Button();
        btnNavCategories = new Button();

        SuspendLayout();
        pnlSidebar.SuspendLayout();
        flpNav.SuspendLayout();
        pnlMain.SuspendLayout();
        pnlHeader.SuspendLayout();
        pnlContent.SuspendLayout();
        statusStrip.SuspendLayout();

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(244, 246, 248);
        ClientSize = new Size(1100, 700);
        MinimumSize = new Size(1024, 768);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Inventory Management System (IMS)";
        Font = new Font("Segoe UI", 10F);
        Load += MainForm_Load;

        pnlSidebar.Dock = DockStyle.Left;
        pnlSidebar.Width = 220;
        pnlSidebar.BackColor = Color.FromArgb(22, 50, 79);
        pnlSidebar.Name = "pnlSidebar";

        lblBrand.Text = "  IMS";
        lblBrand.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblBrand.ForeColor = Color.White;
        lblBrand.Dock = DockStyle.Top;
        lblBrand.Height = 56;
        lblBrand.TextAlign = ContentAlignment.MiddleLeft;
        lblBrand.Padding = new Padding(8, 0, 0, 0);
        lblBrand.Name = "lblBrand";

        flpNav.Dock = DockStyle.Fill;
        flpNav.FlowDirection = FlowDirection.TopDown;
        flpNav.WrapContents = false;
        flpNav.AutoScroll = true;
        flpNav.Padding = new Padding(0, 8, 0, 8);
        flpNav.BackColor = Color.FromArgb(22, 50, 79);
        flpNav.Name = "flpNav";

        btnNavDashboard.Text = "  Dashboard";
        btnNavDashboard.FlatStyle = FlatStyle.Flat;
        btnNavDashboard.FlatAppearance.BorderSize = 0;
        btnNavDashboard.ForeColor = Color.White;
        btnNavDashboard.BackColor = Color.FromArgb(40, 255, 255, 255);
        btnNavDashboard.TextAlign = ContentAlignment.MiddleLeft;
        btnNavDashboard.Size = new Size(204, 36);
        btnNavDashboard.Margin = new Padding(8, 2, 8, 2);
        btnNavDashboard.Name = "btnNavDashboard";

        lblNavGroupCatalog.Text = "CATALOG";
        lblNavGroupCatalog.ForeColor = Color.FromArgb(199, 211, 224);
        lblNavGroupCatalog.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        lblNavGroupCatalog.AutoSize = false;
        lblNavGroupCatalog.Size = new Size(196, 28);
        lblNavGroupCatalog.Padding = new Padding(12, 12, 0, 0);
        lblNavGroupCatalog.Margin = new Padding(0, 8, 0, 0);
        lblNavGroupCatalog.Name = "lblNavGroupCatalog";

        btnNavProducts.Text = "  Products";
        btnNavProducts.FlatStyle = FlatStyle.Flat;
        btnNavProducts.FlatAppearance.BorderSize = 0;
        btnNavProducts.ForeColor = Color.FromArgb(199, 211, 224);
        btnNavProducts.BackColor = Color.FromArgb(22, 50, 79);
        btnNavProducts.TextAlign = ContentAlignment.MiddleLeft;
        btnNavProducts.Size = new Size(204, 36);
        btnNavProducts.Margin = new Padding(8, 2, 8, 2);
        btnNavProducts.Name = "btnNavProducts";

        btnNavCategories.Text = "  Categories";
        btnNavCategories.FlatStyle = FlatStyle.Flat;
        btnNavCategories.FlatAppearance.BorderSize = 0;
        btnNavCategories.ForeColor = Color.FromArgb(199, 211, 224);
        btnNavCategories.BackColor = Color.FromArgb(22, 50, 79);
        btnNavCategories.TextAlign = ContentAlignment.MiddleLeft;
        btnNavCategories.Size = new Size(204, 36);
        btnNavCategories.Margin = new Padding(8, 2, 8, 2);
        btnNavCategories.Name = "btnNavCategories";

        flpNav.Controls.Add(btnNavDashboard);
        flpNav.Controls.Add(lblNavGroupCatalog);
        flpNav.Controls.Add(btnNavProducts);
        flpNav.Controls.Add(btnNavCategories);

        btnLogout.Text = "  Sign Out";
        btnLogout.Font = new Font("Segoe UI", 9.5F);
        btnLogout.ForeColor = Color.FromArgb(199, 211, 224);
        btnLogout.BackColor = Color.FromArgb(22, 50, 79);
        btnLogout.FlatStyle = FlatStyle.Flat;
        btnLogout.FlatAppearance.BorderSize = 0;
        btnLogout.Dock = DockStyle.Bottom;
        btnLogout.Height = 44;
        btnLogout.TextAlign = ContentAlignment.MiddleLeft;
        btnLogout.Cursor = Cursors.Hand;
        btnLogout.Name = "btnLogout";
        btnLogout.AccessibleName = "Sign Out";
        btnLogout.Click += BtnLogout_Click;

        pnlSidebar.Controls.Add(flpNav);
        pnlSidebar.Controls.Add(btnLogout);
        pnlSidebar.Controls.Add(lblBrand);

        pnlMain.Dock = DockStyle.Fill;
        pnlMain.BackColor = Color.FromArgb(244, 246, 248);
        pnlMain.Name = "pnlMain";

        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Height = 72;
        pnlHeader.BackColor = Color.White;
        pnlHeader.Padding = new Padding(24, 12, 24, 12);
        pnlHeader.Name = "pnlHeader";

        lblBreadcrumb.Text = "Home";
        lblBreadcrumb.Font = new Font("Segoe UI", 9F);
        lblBreadcrumb.ForeColor = Color.FromArgb(107, 119, 133);
        lblBreadcrumb.AutoSize = true;
        lblBreadcrumb.Location = new Point(24, 12);
        lblBreadcrumb.Name = "lblBreadcrumb";

        lblScreenTitle.Text = "Dashboard";
        lblScreenTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblScreenTitle.ForeColor = Color.FromArgb(31, 41, 51);
        lblScreenTitle.AutoSize = true;
        lblScreenTitle.Location = new Point(24, 32);
        lblScreenTitle.Name = "lblScreenTitle";

        lblRoleBadge.Text = "v1.3";
        lblRoleBadge.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblRoleBadge.ForeColor = Color.FromArgb(30, 75, 143);
        lblRoleBadge.BackColor = Color.FromArgb(231, 240, 247);
        lblRoleBadge.AutoSize = false;
        lblRoleBadge.Size = new Size(44, 20);
        lblRoleBadge.TextAlign = ContentAlignment.MiddleCenter;
        lblRoleBadge.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblRoleBadge.Location = new Point(1000, 28);
        lblRoleBadge.Name = "lblRoleBadge";

        pnlHeader.Controls.Add(lblBreadcrumb);
        pnlHeader.Controls.Add(lblScreenTitle);
        pnlHeader.Controls.Add(lblRoleBadge);

        pnlContent.Dock = DockStyle.Fill;
        pnlContent.BackColor = Color.FromArgb(244, 246, 248);
        pnlContent.Padding = new Padding(32);
        pnlContent.Name = "pnlContent";

        lblPlaceholderTitle.Text = "Welcome";
        lblPlaceholderTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblPlaceholderTitle.ForeColor = Color.FromArgb(31, 41, 51);
        lblPlaceholderTitle.AutoSize = true;
        lblPlaceholderTitle.Location = new Point(32, 40);
        lblPlaceholderTitle.Name = "lblPlaceholderTitle";

        lblPlaceholderBody.Text = "Select a navigation item. Content hosts here at runtime.";
        lblPlaceholderBody.Font = new Font("Segoe UI", 10F);
        lblPlaceholderBody.ForeColor = Color.FromArgb(107, 119, 133);
        lblPlaceholderBody.AutoSize = false;
        lblPlaceholderBody.MaximumSize = new Size(700, 0);
        lblPlaceholderBody.Size = new Size(700, 80);
        lblPlaceholderBody.Location = new Point(32, 80);
        lblPlaceholderBody.Name = "lblPlaceholderBody";

        pnlContent.Controls.Add(lblPlaceholderTitle);
        pnlContent.Controls.Add(lblPlaceholderBody);

        statusStrip.BackColor = Color.FromArgb(238, 241, 244);
        statusStrip.Font = new Font("Segoe UI", 9F);
        statusStrip.SizingGrip = true;
        statusStrip.Name = "statusStrip";
        statusStrip.Dock = DockStyle.Bottom;

        lblUserStatus.Text = "User";
        lblUserStatus.Name = "lblUserStatus";

        lblDateStatus.Text = "";
        lblDateStatus.BorderSides = ToolStripStatusLabelBorderSides.Left;
        lblDateStatus.BorderStyle = Border3DStyle.Etched;
        lblDateStatus.Name = "lblDateStatus";

        lblSpacer.Spring = true;
        lblSpacer.Name = "lblSpacer";

        lblMessageStatus.Text = "Ready";
        lblMessageStatus.Name = "lblMessageStatus";

        statusStrip.Items.AddRange(new ToolStripItem[]
        {
            lblUserStatus, lblDateStatus, lblSpacer, lblMessageStatus
        });

        pnlMain.Controls.Add(pnlContent);
        pnlMain.Controls.Add(pnlHeader);

        Controls.Add(pnlMain);
        Controls.Add(statusStrip);
        Controls.Add(pnlSidebar);

        flpNav.ResumeLayout(false);
        pnlSidebar.ResumeLayout(false);
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        pnlContent.ResumeLayout(false);
        pnlContent.PerformLayout();
        pnlMain.ResumeLayout(false);
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
