using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms;

/// <summary>
/// Dashboard — pastel metrics + insight charts (Designer-visible).
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

    private Panel chipProducts;
    private Panel chipCategories;
    private Panel chipSuppliers;
    private Panel chipTotalQty;
    private Panel chipLow;
    private Panel chipOut;

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
        chipProducts = new Panel();
        icoProducts = new PictureBox();
        capProducts = new Label();
        lblProducts = new Label();
        subProducts = new Label();
        tileCategories = new Panel();
        chipCategories = new Panel();
        icoCategories = new PictureBox();
        capCategories = new Label();
        lblCategories = new Label();
        subCategories = new Label();
        tileSuppliers = new Panel();
        chipSuppliers = new Panel();
        icoSuppliers = new PictureBox();
        capSuppliers = new Label();
        lblSuppliers = new Label();
        subSuppliers = new Label();
        tileTotalQty = new Panel();
        chipTotalQty = new Panel();
        icoTotalQty = new PictureBox();
        capTotalQty = new Label();
        lblTotalQty = new Label();
        subTotalQty = new Label();
        tileLow = new Panel();
        chipLow = new Panel();
        icoLow = new PictureBox();
        capLow = new Label();
        lblLow = new Label();
        subLow = new Label();
        tileOut = new Panel();
        chipOut = new Panel();
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
        chipProducts.SuspendLayout();
        tileCategories.SuspendLayout();
        chipCategories.SuspendLayout();
        tileSuppliers.SuspendLayout();
        chipSuppliers.SuspendLayout();
        tileTotalQty.SuspendLayout();
        chipTotalQty.SuspendLayout();
        tileLow.SuspendLayout();
        chipLow.SuspendLayout();
        tileOut.SuspendLayout();
        chipOut.SuspendLayout();
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

        // Welcome
        welcomePanel.Dock = DockStyle.Top;
        welcomePanel.Height = 52;
        welcomePanel.BackColor = Color.FromArgb(244, 246, 248);
        welcomePanel.Name = "welcomePanel";

        lblWelcome.AutoSize = true;
        lblWelcome.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblWelcome.ForeColor = Color.FromArgb(31, 41, 51);
        lblWelcome.Location = new Point(0, 2);
        lblWelcome.Name = "lblWelcome";
        lblWelcome.Text = "Inventory overview";

        lblWelcomeSub.AutoSize = true;
        lblWelcomeSub.Font = new Font("Segoe UI", 9F);
        lblWelcomeSub.ForeColor = Color.FromArgb(107, 119, 133);
        lblWelcomeSub.Location = new Point(2, 30);
        lblWelcomeSub.Name = "lblWelcomeSub";
        lblWelcomeSub.Text = "Here’s what’s happening in your stock right now.";

        welcomePanel.Controls.Add(lblWelcome);
        welcomePanel.Controls.Add(lblWelcomeSub);

        // Metrics
        metricsPanel.AutoScroll = true;
        metricsPanel.BackColor = Color.FromArgb(244, 246, 248);
        metricsPanel.Controls.Add(tileProducts);
        metricsPanel.Controls.Add(tileCategories);
        metricsPanel.Controls.Add(tileSuppliers);
        metricsPanel.Controls.Add(tileTotalQty);
        metricsPanel.Controls.Add(tileLow);
        metricsPanel.Controls.Add(tileOut);
        metricsPanel.Dock = DockStyle.Top;
        metricsPanel.Padding = new Padding(0, 0, 0, 10);
        metricsPanel.Size = new Size(980, 128);
        metricsPanel.Name = "metricsPanel";
        metricsPanel.WrapContents = false;

        ConfigureMetricTile(tileProducts, chipProducts, icoProducts,
            capProducts, lblProducts, subProducts,
            "Active Products", "Catalog items",
            Color.FromArgb(255, 248, 235), Color.FromArgb(255, 236, 200), 0);
        ConfigureMetricTile(tileCategories, chipCategories, icoCategories,
            capCategories, lblCategories, subCategories,
            "Categories", "Groupings",
            Color.FromArgb(243, 232, 255), Color.FromArgb(230, 210, 255), 1);
        ConfigureMetricTile(tileSuppliers, chipSuppliers, icoSuppliers,
            capSuppliers, lblSuppliers, subSuppliers,
            "Suppliers", "Active vendors",
            Color.FromArgb(230, 247, 255), Color.FromArgb(200, 235, 255), 2);
        ConfigureMetricTile(tileTotalQty, chipTotalQty, icoTotalQty,
            capTotalQty, lblTotalQty, subTotalQty,
            "Units on hand", "Total quantity",
            Color.FromArgb(232, 248, 240), Color.FromArgb(200, 240, 220), 3);
        ConfigureMetricTile(tileLow, chipLow, icoLow,
            capLow, lblLow, subLow,
            "Low stock", "Below minimum",
            Color.FromArgb(255, 243, 224), Color.FromArgb(255, 224, 180),
            4, Color.FromArgb(199, 119, 0));
        ConfigureMetricTile(tileOut, chipOut, icoOut,
            capOut, lblOut, subOut,
            "Out of stock", "Zero quantity",
            Color.FromArgb(254, 236, 234), Color.FromArgb(255, 210, 205),
            5, Color.FromArgb(192, 57, 43));

        lblError.Dock = DockStyle.Top;
        lblError.ForeColor = Color.FromArgb(192, 57, 43);
        lblError.Font = new Font("Segoe UI", 9F);
        lblError.Height = 22;
        lblError.Name = "lblError";
        lblError.Visible = false;

        // Bottom: Activity | Insights | Quick Actions
        bottom.ColumnCount = 3;
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46F));
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26F));
        bottom.Controls.Add(activityCard, 0, 0);
        bottom.Controls.Add(insightsCard, 1, 0);
        bottom.Controls.Add(actionsPanel, 2, 0);
        bottom.Dock = DockStyle.Fill;
        bottom.Name = "bottom";
        bottom.RowCount = 1;
        bottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        bottom.Padding = new Padding(0, 4, 0, 0);

        // Activity
        activityCard.BackColor = Color.White;
        activityCard.BorderStyle = BorderStyle.FixedSingle;
        activityCard.Controls.Add(grid);
        activityCard.Controls.Add(lblEmpty);
        activityCard.Controls.Add(activityHeader);
        activityCard.Dock = DockStyle.Fill;
        activityCard.Margin = new Padding(0, 0, 10, 0);
        activityCard.Padding = new Padding(14);
        activityCard.Name = "activityCard";

        activityHeader.Controls.Add(lblAct);
        activityHeader.Controls.Add(lnkViewAll);
        activityHeader.Dock = DockStyle.Top;
        activityHeader.Height = 30;
        activityHeader.Name = "activityHeader";

        lblAct.AutoSize = true;
        lblAct.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblAct.ForeColor = Color.FromArgb(31, 41, 51);
        lblAct.Location = new Point(0, 4);
        lblAct.Name = "lblAct";
        lblAct.Text = "Recent Activity";

        lnkViewAll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lnkViewAll.AutoSize = true;
        lnkViewAll.LinkBehavior = LinkBehavior.HoverUnderline;
        lnkViewAll.LinkColor = Color.FromArgb(30, 75, 143);
        lnkViewAll.Location = new Point(400, 6);
        lnkViewAll.Name = "lnkViewAll";
        lnkViewAll.Text = "View all  >";
        lnkViewAll.Click += LnkViewAll_Click;

        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AllowUserToResizeRows = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.BackgroundColor = Color.White;
        grid.BorderStyle = BorderStyle.None;
        grid.ColumnHeadersHeight = 32;
        grid.Dock = DockStyle.Fill;
        grid.MultiSelect = false;
        grid.Name = "grid";
        grid.ReadOnly = true;
        grid.RowHeadersVisible = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        lblEmpty.Dock = DockStyle.Fill;
        lblEmpty.Font = new Font("Segoe UI", 10F);
        lblEmpty.ForeColor = Color.FromArgb(107, 119, 133);
        lblEmpty.Name = "lblEmpty";
        lblEmpty.Text = "No recent activity yet.";
        lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
        lblEmpty.Visible = false;

        // Insights card (charts)
        insightsCard.BackColor = Color.White;
        insightsCard.BorderStyle = BorderStyle.FixedSingle;
        insightsCard.Dock = DockStyle.Fill;
        insightsCard.Margin = new Padding(0, 0, 10, 0);
        insightsCard.Padding = new Padding(12);
        insightsCard.Name = "insightsCard";

        lblInsightsTitle.Dock = DockStyle.Top;
        lblInsightsTitle.Height = 28;
        lblInsightsTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblInsightsTitle.ForeColor = Color.FromArgb(31, 41, 51);
        lblInsightsTitle.Text = "Insights";
        lblInsightsTitle.TextAlign = ContentAlignment.MiddleLeft;
        lblInsightsTitle.Name = "lblInsightsTitle";

        lblHealthTitle.Dock = DockStyle.Top;
        lblHealthTitle.Height = 22;
        lblHealthTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblHealthTitle.ForeColor = Color.FromArgb(90, 100, 115);
        lblHealthTitle.Text = "Stock health";
        lblHealthTitle.Name = "lblHealthTitle";

        pnlStockHealth.Dock = DockStyle.Top;
        pnlStockHealth.Height = 150;
        pnlStockHealth.BackColor = Color.White;
        pnlStockHealth.Name = "pnlStockHealth";

        lblMovementTitle.Dock = DockStyle.Top;
        lblMovementTitle.Height = 22;
        lblMovementTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblMovementTitle.ForeColor = Color.FromArgb(90, 100, 115);
        lblMovementTitle.Text = "Recent movement (qty)";
        lblMovementTitle.Name = "lblMovementTitle";

        pnlMovement.Dock = DockStyle.Fill;
        pnlMovement.BackColor = Color.White;
        pnlMovement.Name = "pnlMovement";

        // Add order: Fill first, then tops
        insightsCard.Controls.Add(pnlMovement);
        insightsCard.Controls.Add(lblMovementTitle);
        insightsCard.Controls.Add(pnlStockHealth);
        insightsCard.Controls.Add(lblHealthTitle);
        insightsCard.Controls.Add(lblInsightsTitle);

        // Quick Actions
        actionsPanel.BackColor = Color.White;
        actionsPanel.BorderStyle = BorderStyle.FixedSingle;
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
        lblQa.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblQa.ForeColor = Color.FromArgb(31, 41, 51);
        lblQa.Height = 30;
        lblQa.Name = "lblQa";
        lblQa.Text = "Quick Actions";
        lblQa.TextAlign = ContentAlignment.MiddleLeft;

        ConfigureQaButton(btnQaStockIn, "btnQaStockIn", "  Record Stock-In", 42, true);
        ConfigureQaButton(btnQaStockOut, "btnQaStockOut", "  Record Stock-Out", 86, false);
        ConfigureQaButton(btnQaProducts, "btnQaProducts", "  Search Products", 130, false);
        ConfigureQaButton(btnQaAdjustment, "btnQaAdjustment", "  Inventory Adjustment", 174, false);
        ConfigureQaButton(btnQaUsers, "btnQaUsers", "  Manage Users", 218, false);

        btnQaStockIn.Click += BtnQaStockIn_Click;
        btnQaStockOut.Click += BtnQaStockOut_Click;
        btnQaProducts.Click += BtnQaProducts_Click;
        btnQaAdjustment.Click += BtnQaAdjustment_Click;
        btnQaUsers.Click += BtnQaUsers_Click;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(244, 246, 248);
        ClientSize = new Size(1020, 620);
        Controls.Add(bottom);
        Controls.Add(lblError);
        Controls.Add(metricsPanel);
        Controls.Add(welcomePanel);
        Font = new Font("Segoe UI", 10F);
        FormBorderStyle = FormBorderStyle.None;
        Name = "DashboardForm";
        Padding = new Padding(18, 6, 18, 10);
        Text = "Dashboard";

        welcomePanel.ResumeLayout(false);
        welcomePanel.PerformLayout();
        metricsPanel.ResumeLayout(false);
        tileProducts.ResumeLayout(false);
        chipProducts.ResumeLayout(false);
        tileCategories.ResumeLayout(false);
        chipCategories.ResumeLayout(false);
        tileSuppliers.ResumeLayout(false);
        chipSuppliers.ResumeLayout(false);
        tileTotalQty.ResumeLayout(false);
        chipTotalQty.ResumeLayout(false);
        tileLow.ResumeLayout(false);
        chipLow.ResumeLayout(false);
        tileOut.ResumeLayout(false);
        chipOut.ResumeLayout(false);
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

    private static void ConfigureMetricTile(
        Panel tile, Panel chip, PictureBox ico,
        Label cap, Label value, Label sub,
        string capText, string subText,
        Color tileBg, Color chipBg, int tab,
        Color? valueColor = null)
    {
        tile.BackColor = tileBg;
        tile.BorderStyle = BorderStyle.None;
        tile.Margin = new Padding(0, 0, 12, 0);
        tile.Size = new Size(168, 112);
        tile.TabIndex = tab;

        chip.BackColor = chipBg;
        chip.Location = new Point(12, 12);
        chip.Size = new Size(36, 36);

        ico.Location = new Point(8, 8);
        ico.Size = new Size(20, 20);
        ico.SizeMode = PictureBoxSizeMode.Zoom;
        ico.TabStop = false;
        chip.Controls.Add(ico);

        cap.AutoSize = false;
        cap.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        cap.ForeColor = Color.FromArgb(90, 100, 115);
        cap.Location = new Point(54, 12);
        cap.Size = new Size(106, 30);
        cap.Text = capText;

        value.AutoSize = false;
        value.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        value.ForeColor = valueColor ?? Color.FromArgb(31, 41, 51);
        value.Location = new Point(12, 54);
        value.Size = new Size(144, 30);
        value.Text = "—";

        sub.AutoSize = false;
        sub.Font = new Font("Segoe UI", 7.5F);
        sub.ForeColor = Color.FromArgb(120, 130, 145);
        sub.Location = new Point(12, 88);
        sub.Size = new Size(144, 16);
        sub.Text = subText;

        tile.Controls.Add(sub);
        tile.Controls.Add(value);
        tile.Controls.Add(cap);
        tile.Controls.Add(chip);
    }

    private static void ConfigureQaButton(Button btn, string name, string text, int top, bool primary)
    {
        btn.FlatStyle = FlatStyle.Flat;
        btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btn.Location = new Point(12, top);
        btn.Name = name;
        btn.Size = new Size(220, 36);
        btn.Text = text;
        btn.UseVisualStyleBackColor = false;
        btn.Cursor = Cursors.Hand;
        btn.TextAlign = ContentAlignment.MiddleLeft;
        btn.Padding = new Padding(8, 0, 6, 0);

        if (primary)
        {
            btn.BackColor = Color.FromArgb(30, 75, 143);
            btn.ForeColor = Color.White;
            btn.FlatAppearance.BorderSize = 0;
        }
        else
        {
            btn.BackColor = Color.White;
            btn.ForeColor = Color.FromArgb(30, 75, 143);
            btn.FlatAppearance.BorderColor = Color.FromArgb(215, 220, 225);
            btn.FlatAppearance.BorderSize = 1;
        }
    }
}
