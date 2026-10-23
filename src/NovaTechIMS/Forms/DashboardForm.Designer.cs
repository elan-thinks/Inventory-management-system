using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms;

/// <summary>
/// Dashboard — soft cards + metric icons (Designer-visible).
/// </summary>
partial class DashboardForm
{
    private System.ComponentModel.IContainer components = null;

    private Panel welcomePanel;
    private Label lblWelcome;
    private Label lblWelcomeSub;

    private FlowLayoutPanel metricsPanel;
    private Panel tileProducts;
    private Panel tileCategories;
    private Panel tileSuppliers;
    private Panel tileTotalQty;
    private Panel tileLow;
    private Panel tileOut;

    private PictureBox icoProducts;
    private PictureBox icoCategories;
    private PictureBox icoSuppliers;
    private PictureBox icoTotalQty;
    private PictureBox icoLow;
    private PictureBox icoOut;

    private Label capProducts;
    private Label lblProducts;
    private Label subProducts;
    private Label capCategories;
    private Label lblCategories;
    private Label subCategories;
    private Label capSuppliers;
    private Label lblSuppliers;
    private Label subSuppliers;
    private Label capTotalQty;
    private Label lblTotalQty;
    private Label subTotalQty;
    private Label capLow;
    private Label lblLow;
    private Label subLow;
    private Label capOut;
    private Label lblOut;
    private Label subOut;

    private Label lblError;
    private TableLayoutPanel bottom;

    private Panel activityCard;
    private Panel activityHeader;
    private Label lblAct;
    private LinkLabel lnkViewAll;
    private DataGridView grid;
    private Label lblEmpty;

    private Panel insightsCard;
    private Label lblInsightsTitle;
    private Panel pnlStockHealth;
    private Label lblHealthTitle;
    private Panel pnlMovement;
    private Label lblMovementTitle;

    private Panel actionsPanel;
    private Label lblQa;
    private Button btnQaStockIn;
    private Button btnQaStockOut;
    private Button btnQaProducts;
    private Button btnQaAdjustment;
    private Button btnQaUsers;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        welcomePanel = new Panel();
        lblWelcome = new Label();
        lblWelcomeSub = new Label();

        metricsPanel = new FlowLayoutPanel();
        tileProducts = new Panel();
        icoProducts = new PictureBox();
        capProducts = new Label();
        lblProducts = new Label();
        subProducts = new Label();
        tileCategories = new Panel();
        icoCategories = new PictureBox();
        capCategories = new Label();
        lblCategories = new Label();
        subCategories = new Label();
        tileSuppliers = new Panel();
        icoSuppliers = new PictureBox();
        capSuppliers = new Label();
        lblSuppliers = new Label();
        subSuppliers = new Label();
        tileTotalQty = new Panel();
        icoTotalQty = new PictureBox();
        capTotalQty = new Label();
        lblTotalQty = new Label();
        subTotalQty = new Label();
        tileLow = new Panel();
        icoLow = new PictureBox();
        capLow = new Label();
        lblLow = new Label();
        subLow = new Label();
        tileOut = new Panel();
        icoOut = new PictureBox();
        capOut = new Label();
        lblOut = new Label();
        subOut = new Label();

        lblError = new Label();
        bottom = new TableLayoutPanel();

        activityCard = new Panel();
        activityHeader = new Panel();
        lblAct = new Label();
        lnkViewAll = new LinkLabel();
        grid = new DataGridView();
        lblEmpty = new Label();

        insightsCard = new Panel();
        lblInsightsTitle = new Label();
        pnlStockHealth = new Panel();
        lblHealthTitle = new Label();
        pnlMovement = new Panel();
        lblMovementTitle = new Label();

        actionsPanel = new Panel();
        lblQa = new Label();
        btnQaStockIn = new Button();
        btnQaStockOut = new Button();
        btnQaProducts = new Button();
        btnQaAdjustment = new Button();
        btnQaUsers = new Button();

        welcomePanel.SuspendLayout();
        metricsPanel.SuspendLayout();
        tileProducts.SuspendLayout();
        tileCategories.SuspendLayout();
        tileSuppliers.SuspendLayout();
        tileTotalQty.SuspendLayout();
        tileLow.SuspendLayout();
        tileOut.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)icoProducts).BeginInit();
        ((System.ComponentModel.ISupportInitialize)icoCategories).BeginInit();
        ((System.ComponentModel.ISupportInitialize)icoSuppliers).BeginInit();
        ((System.ComponentModel.ISupportInitialize)icoTotalQty).BeginInit();
        ((System.ComponentModel.ISupportInitialize)icoLow).BeginInit();
        ((System.ComponentModel.ISupportInitialize)icoOut).BeginInit();
        bottom.SuspendLayout();
        activityCard.SuspendLayout();
        activityHeader.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        insightsCard.SuspendLayout();
        actionsPanel.SuspendLayout();
        SuspendLayout();

        var pageBg = Color.FromArgb(246, 248, 250);
        var accent = Color.FromArgb(15, 118, 70);
        var textMain = Color.FromArgb(28, 35, 43);
        var textMuted = Color.FromArgb(130, 140, 152);

        // Welcome — hierarchy: title only (page header already in MainForm)
        welcomePanel.Dock = DockStyle.Top;
        welcomePanel.Height = 48;
        welcomePanel.BackColor = pageBg;
        welcomePanel.Name = "welcomePanel";
        welcomePanel.BorderStyle = BorderStyle.None;

        lblWelcome.AutoSize = true;
        lblWelcome.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblWelcome.ForeColor = textMain;
        lblWelcome.Location = new Point(2, 2);
        lblWelcome.Name = "lblWelcome";
        lblWelcome.Text = "Inventory overview";

        lblWelcomeSub.AutoSize = true;
        lblWelcomeSub.Font = new Font("Segoe UI", 9F);
        lblWelcomeSub.ForeColor = textMuted;
        lblWelcomeSub.Location = new Point(4, 28);
        lblWelcomeSub.Name = "lblWelcomeSub";
        lblWelcomeSub.Text = "Electronics stock health and recent movement at a glance.";

        welcomePanel.Controls.Add(lblWelcome);
        welcomePanel.Controls.Add(lblWelcomeSub);

        metricsPanel.AutoScroll = true;
        metricsPanel.BackColor = pageBg;
        metricsPanel.Controls.Add(tileProducts);
        metricsPanel.Controls.Add(tileCategories);
        metricsPanel.Controls.Add(tileSuppliers);
        metricsPanel.Controls.Add(tileTotalQty);
        metricsPanel.Controls.Add(tileLow);
        metricsPanel.Controls.Add(tileOut);
        metricsPanel.Dock = DockStyle.Top;
        metricsPanel.Padding = new Padding(0, 6, 0, 14);
        metricsPanel.Size = new Size(1020, 128);
        metricsPanel.Name = "metricsPanel";
        metricsPanel.WrapContents = false;

        // Primary hero card + 5 supporting cards with icon slots
        ConfigureMetricTile(tileProducts, icoProducts, capProducts, lblProducts, subProducts,
            "Active Products", "In catalog",
            accent, Color.White, Color.FromArgb(190, 225, 205), 0, true);

        ConfigureMetricTile(tileCategories, icoCategories, capCategories, lblCategories, subCategories,
            "Categories", "Groupings",
            Color.White, textMain, textMuted, 1, false);

        ConfigureMetricTile(tileSuppliers, icoSuppliers, capSuppliers, lblSuppliers, subSuppliers,
            "Suppliers", "Vendors",
            Color.White, textMain, textMuted, 2, false);

        ConfigureMetricTile(tileTotalQty, icoTotalQty, capTotalQty, lblTotalQty, subTotalQty,
            "Units on hand", "Total stock",
            Color.White, textMain, textMuted, 3, false);

        ConfigureMetricTile(tileLow, icoLow, capLow, lblLow, subLow,
            "Low stock", "Below min",
            Color.White, Color.FromArgb(180, 110, 20), textMuted, 4, false);

        ConfigureMetricTile(tileOut, icoOut, capOut, lblOut, subOut,
            "Out of stock", "Zero qty",
            Color.White, Color.FromArgb(180, 55, 50), textMuted, 5, false);

        lblError.Dock = DockStyle.Top;
        lblError.ForeColor = Color.FromArgb(180, 55, 50);
        lblError.Font = new Font("Segoe UI", 9F);
        lblError.Height = 22;
        lblError.Name = "lblError";
        lblError.Visible = false;
        lblError.BorderStyle = BorderStyle.None;

        bottom.ColumnCount = 3;
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44F));
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26F));
        bottom.Controls.Add(activityCard, 0, 0);
        bottom.Controls.Add(insightsCard, 1, 0);
        bottom.Controls.Add(actionsPanel, 2, 0);
        bottom.Dock = DockStyle.Fill;
        bottom.Name = "bottom";
        bottom.RowCount = 1;
        bottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        bottom.Padding = new Padding(0, 2, 0, 0);
        bottom.BackColor = pageBg;

        StyleSoftCard(activityCard);
        activityCard.Controls.Add(grid);
        activityCard.Controls.Add(lblEmpty);
        activityCard.Controls.Add(activityHeader);
        activityCard.Dock = DockStyle.Fill;
        activityCard.Margin = new Padding(0, 0, 12, 0);
        activityCard.Padding = new Padding(16);
        activityCard.Name = "activityCard";

        activityHeader.Controls.Add(lblAct);
        activityHeader.Controls.Add(lnkViewAll);
        activityHeader.Dock = DockStyle.Top;
        activityHeader.Height = 30;
        activityHeader.BackColor = Color.White;
        activityHeader.BorderStyle = BorderStyle.None;
        activityHeader.Name = "activityHeader";

        lblAct.AutoSize = true;
        lblAct.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        lblAct.ForeColor = textMain;
        lblAct.Location = new Point(0, 4);
        lblAct.Name = "lblAct";
        lblAct.Text = "Recent Activity";

        lnkViewAll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lnkViewAll.AutoSize = true;
        lnkViewAll.LinkBehavior = LinkBehavior.HoverUnderline;
        lnkViewAll.LinkColor = accent;
        lnkViewAll.Location = new Point(360, 6);
        lnkViewAll.Name = "lnkViewAll";
        lnkViewAll.Text = "View all";
        lnkViewAll.Click += LnkViewAll_Click;

        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AllowUserToResizeRows = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.BackgroundColor = Color.White;
        grid.BorderStyle = BorderStyle.None;
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        grid.GridColor = Color.FromArgb(238, 241, 244);
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        grid.ColumnHeadersHeight = 34;
        grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
        {
            BackColor = Color.FromArgb(248, 250, 252),
            ForeColor = textMuted,
            Font = new Font("Segoe UI", 8.25F, FontStyle.Bold),
            SelectionBackColor = Color.FromArgb(248, 250, 252),
            SelectionForeColor = textMuted,
            Alignment = DataGridViewContentAlignment.MiddleLeft,
            Padding = new Padding(4, 0, 0, 0)
        };
        grid.DefaultCellStyle = new DataGridViewCellStyle
        {
            BackColor = Color.White,
            ForeColor = textMain,
            SelectionBackColor = Color.FromArgb(232, 245, 238),
            SelectionForeColor = textMain,
            Font = new Font("Segoe UI", 9F),
            Padding = new Padding(4, 2, 0, 2)
        };
        grid.EnableHeadersVisualStyles = false;
        grid.RowTemplate.Height = 34;
        grid.Dock = DockStyle.Fill;
        grid.MultiSelect = false;
        grid.Name = "grid";
        grid.ReadOnly = true;
        grid.RowHeadersVisible = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        lblEmpty.Dock = DockStyle.Fill;
        lblEmpty.Font = new Font("Segoe UI", 10F);
        lblEmpty.ForeColor = textMuted;
        lblEmpty.Name = "lblEmpty";
        lblEmpty.Text = "No recent activity yet. Record a Stock-In to get started.";
        lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
        lblEmpty.Visible = false;

        StyleSoftCard(insightsCard);
        insightsCard.Dock = DockStyle.Fill;
        insightsCard.Margin = new Padding(0, 0, 12, 0);
        insightsCard.Padding = new Padding(14);
        insightsCard.Name = "insightsCard";

        lblInsightsTitle.Dock = DockStyle.Top;
        lblInsightsTitle.Height = 26;
        lblInsightsTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        lblInsightsTitle.ForeColor = textMain;
        lblInsightsTitle.Text = "Insights";
        lblInsightsTitle.TextAlign = ContentAlignment.MiddleLeft;
        lblInsightsTitle.Name = "lblInsightsTitle";

        lblHealthTitle.Dock = DockStyle.Top;
        lblHealthTitle.Height = 20;
        lblHealthTitle.Font = new Font("Segoe UI", 8.25F);
        lblHealthTitle.ForeColor = textMuted;
        lblHealthTitle.Text = "Stock health";
        lblHealthTitle.Name = "lblHealthTitle";

        pnlStockHealth.Dock = DockStyle.Top;
        pnlStockHealth.Height = 150;
        pnlStockHealth.BackColor = Color.White;
        pnlStockHealth.BorderStyle = BorderStyle.None;
        pnlStockHealth.Name = "pnlStockHealth";

        lblMovementTitle.Dock = DockStyle.Top;
        lblMovementTitle.Height = 20;
        lblMovementTitle.Font = new Font("Segoe UI", 8.25F);
        lblMovementTitle.ForeColor = textMuted;
        lblMovementTitle.Text = "Weekly movement";
        lblMovementTitle.Name = "lblMovementTitle";

        pnlMovement.Dock = DockStyle.Fill;
        pnlMovement.BackColor = Color.White;
        pnlMovement.BorderStyle = BorderStyle.None;
        pnlMovement.Name = "pnlMovement";

        insightsCard.Controls.Add(pnlMovement);
        insightsCard.Controls.Add(lblMovementTitle);
        insightsCard.Controls.Add(pnlStockHealth);
        insightsCard.Controls.Add(lblHealthTitle);
        insightsCard.Controls.Add(lblInsightsTitle);

        StyleSoftCard(actionsPanel);
        actionsPanel.Controls.Add(btnQaUsers);
        actionsPanel.Controls.Add(btnQaAdjustment);
        actionsPanel.Controls.Add(btnQaProducts);
        actionsPanel.Controls.Add(btnQaStockOut);
        actionsPanel.Controls.Add(btnQaStockIn);
        actionsPanel.Controls.Add(lblQa);
        actionsPanel.Dock = DockStyle.Fill;
        actionsPanel.Padding = new Padding(12);
        actionsPanel.Name = "actionsPanel";

        lblQa.Dock = DockStyle.Top;
        lblQa.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        lblQa.ForeColor = textMain;
        lblQa.Height = 28;
        lblQa.Name = "lblQa";
        lblQa.Text = "Quick Actions";
        lblQa.TextAlign = ContentAlignment.MiddleLeft;

        ConfigureQaButton(btnQaStockIn, "btnQaStockIn", "  Record Stock-In", 40, true, accent);
        ConfigureQaButton(btnQaStockOut, "btnQaStockOut", "  Record Stock-Out", 86, false, accent);
        ConfigureQaButton(btnQaProducts, "btnQaProducts", "  Search Products", 132, false, accent);
        ConfigureQaButton(btnQaAdjustment, "btnQaAdjustment", "  Inventory Adjustment", 178, false, accent);
        ConfigureQaButton(btnQaUsers, "btnQaUsers", "  Manage Users", 224, false, accent);

        btnQaStockIn.Click += BtnQaStockIn_Click;
        btnQaStockOut.Click += BtnQaStockOut_Click;
        btnQaProducts.Click += BtnQaProducts_Click;
        btnQaAdjustment.Click += BtnQaAdjustment_Click;
        btnQaUsers.Click += BtnQaUsers_Click;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = pageBg;
        ClientSize = new Size(1040, 640);
        Controls.Add(bottom);
        Controls.Add(lblError);
        Controls.Add(metricsPanel);
        Controls.Add(welcomePanel);
        Font = new Font("Segoe UI", 10F);
        FormBorderStyle = FormBorderStyle.None;
        Name = "DashboardForm";
        Padding = new Padding(20, 6, 20, 14);
        Text = "Dashboard";

        welcomePanel.ResumeLayout(false);
        welcomePanel.PerformLayout();
        metricsPanel.ResumeLayout(false);
        tileProducts.ResumeLayout(false);
        tileCategories.ResumeLayout(false);
        tileSuppliers.ResumeLayout(false);
        tileTotalQty.ResumeLayout(false);
        tileLow.ResumeLayout(false);
        tileOut.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)icoProducts).EndInit();
        ((System.ComponentModel.ISupportInitialize)icoCategories).EndInit();
        ((System.ComponentModel.ISupportInitialize)icoSuppliers).EndInit();
        ((System.ComponentModel.ISupportInitialize)icoTotalQty).EndInit();
        ((System.ComponentModel.ISupportInitialize)icoLow).EndInit();
        ((System.ComponentModel.ISupportInitialize)icoOut).EndInit();
        bottom.ResumeLayout(false);
        activityCard.ResumeLayout(false);
        activityHeader.ResumeLayout(false);
        activityHeader.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        insightsCard.ResumeLayout(false);
        actionsPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    private static void StyleSoftCard(Panel card)
    {
        card.BackColor = Color.White;
        card.BorderStyle = BorderStyle.None;
    }

    private static void ConfigureMetricTile(
        Panel tile, PictureBox ico,
        Label cap, Label value, Label sub,
        string capText, string subText,
        Color tileBg, Color valueColor, Color mutedColor,
        int tab, bool primary)
    {
        tile.BackColor = tileBg;
        tile.BorderStyle = BorderStyle.None;
        tile.Margin = new Padding(0, 0, 10, 0);
        tile.Size = new Size(162, 108);
        tile.TabIndex = tab;

        // Icon top-right (loaded at runtime from Resources)
        ico.Size = new Size(20, 20);
        ico.Location = new Point(130, 12);
        ico.SizeMode = PictureBoxSizeMode.Zoom;
        ico.BackColor = Color.Transparent;
        ico.TabStop = false;

        cap.AutoSize = false;
        cap.Font = new Font("Segoe UI", 8F);
        cap.ForeColor = primary ? Color.FromArgb(200, 232, 215) : Color.FromArgb(130, 140, 152);
        cap.Location = new Point(14, 12);
        cap.Size = new Size(110, 18);
        cap.Text = capText;

        value.AutoSize = false;
        value.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        value.ForeColor = valueColor;
        value.Location = new Point(14, 36);
        value.Size = new Size(134, 38);
        value.Text = "—";

        sub.AutoSize = false;
        sub.Font = new Font("Segoe UI", 7.5F);
        sub.ForeColor = mutedColor;
        sub.Location = new Point(14, 80);
        sub.Size = new Size(134, 16);
        sub.Text = subText;

        tile.Controls.Add(sub);
        tile.Controls.Add(value);
        tile.Controls.Add(cap);
        tile.Controls.Add(ico);
    }

    private static void ConfigureQaButton(
        Button btn, string name, string text, int top, bool primary, Color accent)
    {
        btn.FlatStyle = FlatStyle.Flat;
        btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btn.Location = new Point(12, top);
        btn.Name = name;
        btn.Size = new Size(220, 38);
        btn.Text = text;
        btn.UseVisualStyleBackColor = false;
        btn.Cursor = Cursors.Hand;
        btn.TextAlign = ContentAlignment.MiddleLeft;
        btn.Padding = new Padding(10, 0, 6, 0);
        btn.FlatAppearance.BorderSize = 0;

        if (primary)
        {
            btn.BackColor = accent;
            btn.ForeColor = Color.White;
        }
        else
        {
            btn.BackColor = Color.FromArgb(246, 248, 250);
            btn.ForeColor = accent;
        }
    }
}
