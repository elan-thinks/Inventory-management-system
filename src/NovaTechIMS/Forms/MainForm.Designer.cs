using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Utilities;

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

        SuspendLayout();
        pnlSidebar.SuspendLayout();
        pnlMain.SuspendLayout();
        pnlHeader.SuspendLayout();
        pnlContent.SuspendLayout();
        statusStrip.SuspendLayout();

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = UiTheme.Background;
        ClientSize = new Size(1100, 700);
        MinimumSize = new Size(UiTheme.MinWindowWidth, UiTheme.MinWindowHeight);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "NovaTech IMS";
        Font = UiTheme.Body;
        Load += MainForm_Load;

        pnlSidebar.Dock = DockStyle.Left;
        pnlSidebar.Width = UiTheme.SidebarWidth;
        pnlSidebar.BackColor = UiTheme.SidebarBackground;
        pnlSidebar.Name = "pnlSidebar";

        lblBrand.Text = "  NovaTech IMS";
        lblBrand.Font = UiTheme.SectionTitle;
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
        flpNav.BackColor = UiTheme.SidebarBackground;
        flpNav.Name = "flpNav";

        btnLogout.Text = "  Sign Out";
        btnLogout.Font = UiTheme.SidebarItem;
        btnLogout.ForeColor = UiTheme.SidebarText;
        btnLogout.BackColor = UiTheme.SidebarBackground;
        btnLogout.FlatStyle = FlatStyle.Flat;
        btnLogout.FlatAppearance.BorderSize = 0;
        btnLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 255, 255, 255);
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
        pnlMain.BackColor = UiTheme.Background;
        pnlMain.Name = "pnlMain";

        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Height = 72;
        pnlHeader.BackColor = UiTheme.Surface;
        pnlHeader.Padding = new Padding(24, 12, 24, 12);
        pnlHeader.Name = "pnlHeader";

        lblBreadcrumb.Text = "Home";
        lblBreadcrumb.Font = UiTheme.Label;
        lblBreadcrumb.ForeColor = UiTheme.TextMuted;
        lblBreadcrumb.AutoSize = true;
        lblBreadcrumb.Location = new Point(24, 12);
        lblBreadcrumb.Name = "lblBreadcrumb";

        lblScreenTitle.Text = "Dashboard";
        lblScreenTitle.Font = UiTheme.ScreenTitle;
        lblScreenTitle.ForeColor = UiTheme.Text;
        lblScreenTitle.AutoSize = true;
        lblScreenTitle.Location = new Point(24, 32);
        lblScreenTitle.Name = "lblScreenTitle";

        lblRoleBadge.Text = "Shell · Milestone 1";
        lblRoleBadge.Font = UiTheme.Label;
        lblRoleBadge.ForeColor = UiTheme.Primary;
        lblRoleBadge.AutoSize = true;
        lblRoleBadge.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblRoleBadge.Location = new Point(900, 28);
        lblRoleBadge.Name = "lblRoleBadge";

        pnlHeader.Controls.Add(lblBreadcrumb);
        pnlHeader.Controls.Add(lblScreenTitle);
        pnlHeader.Controls.Add(lblRoleBadge);

        pnlContent.Dock = DockStyle.Fill;
        pnlContent.BackColor = UiTheme.Background;
        pnlContent.Padding = new Padding(32);
        pnlContent.Name = "pnlContent";

        lblPlaceholderTitle.Text = "Welcome";
        lblPlaceholderTitle.Font = UiTheme.ScreenTitle;
        lblPlaceholderTitle.ForeColor = UiTheme.Text;
        lblPlaceholderTitle.AutoSize = true;
        lblPlaceholderTitle.Location = new Point(32, 40);
        lblPlaceholderTitle.Name = "lblPlaceholderTitle";

        lblPlaceholderBody.Text = "";
        lblPlaceholderBody.Font = UiTheme.Body;
        lblPlaceholderBody.ForeColor = UiTheme.TextMuted;
        lblPlaceholderBody.AutoSize = false;
        lblPlaceholderBody.MaximumSize = new Size(700, 0);
        lblPlaceholderBody.Size = new Size(700, 80);
        lblPlaceholderBody.Location = new Point(32, 80);
        lblPlaceholderBody.Name = "lblPlaceholderBody";

        pnlContent.Controls.Add(lblPlaceholderTitle);
        pnlContent.Controls.Add(lblPlaceholderBody);

        statusStrip.BackColor = UiTheme.StatusStripBackground;
        statusStrip.Font = UiTheme.StatusStrip;
        statusStrip.SizingGrip = true;
        statusStrip.Name = "statusStrip";
        statusStrip.Dock = DockStyle.Bottom;

        lblUserStatus.Text = "User";
        lblUserStatus.ForeColor = UiTheme.Text;
        lblUserStatus.Name = "lblUserStatus";

        lblDateStatus.Text = "";
        lblDateStatus.ForeColor = UiTheme.TextMuted;
        lblDateStatus.BorderSides = ToolStripStatusLabelBorderSides.Left;
        lblDateStatus.BorderStyle = Border3DStyle.Etched;
        lblDateStatus.Name = "lblDateStatus";

        lblSpacer.Spring = true;
        lblSpacer.Name = "lblSpacer";

        lblMessageStatus.Text = "Ready";
        lblMessageStatus.ForeColor = UiTheme.TextMuted;
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
