using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms;

/// <summary>
/// Dashboard — Donezo-inspired green metrics + insights (Designer-visible).
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
        capProducts = new Label();
        lblProducts = new Label();
        subProducts = new Label();
        tileCategories = new Panel();
        capCategories = new Label();
        lblCategories = new Label();
        subCategories = new Label();
        tileSuppliers = new Panel();
        capSuppliers = new Label();
        lblSuppliers = new Label();
        subSuppliers = new Label();
        tileTotalQty = new Panel();
        capTotalQty = new Label();
        lblTotalQty = new Label();
        subTotalQty = new Label();
        tileLow = new Panel();
        capLow = new Label();
        lblLow = new Label();
        subLow = new Label();
        tileOut = new Panel();
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
        bottom.SuspendLayout();
        activityCard.SuspendLayout();
        activityHeader.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        insightsCard.SuspendLayout();
        actionsPanel.SuspendLayout();
        SuspendLayout();

        var pageBg = Color.FromArgb(245, 247, 250);
        var accentGreen = Color.FromArgb(15, 118, 70);
        var cardBorder = Color.FromArgb(228, 232, 238);

        // Welcome
        welcomePanel.Dock = DockStyle.Top;
        welcomePanel.Height = 52;
        welcomePanel.BackColor = pageBg;
        welcomePanel.Name = "welcomePanel";

        lblWelcome.AutoSize = true;
        lblWelcome.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
        lblWelcome.ForeColor = Color.FromArgb(24, 32, 40);
        lblWelcome.Location = new Point(0, 2);
        lblWelcome.Name = "lblWelcome";
        lblWelcome.Text = "Dashboard";

        lblWelcomeSub.AutoSize = true;
        lblWelcomeSub.Font = new Font("Segoe UI", 9F);
        lblWelcomeSub.ForeColor = Color.FromArgb(120, 130, 145);
        lblWelcomeSub.Location = new Point(2, 30);
        lblWelcomeSub.Name = "lblWelcomeSub";
        lblWelcomeSub.Text = "Track inventory health and daily stock movement.";

        welcomePanel.Controls.Add(lblWelcome);
        welcomePanel.Controls.Add(lblWelcomeSub);

        // Metrics row
        metricsPanel.AutoScroll = true;
        metricsPanel.BackColor = pageBg;
        metricsPanel.Controls.Add(tileProducts);
        metricsPanel.Controls.Add(tileCategories);
        metricsPanel.Controls.Add(tileSuppliers);
        metricsPanel.Controls.Add(tileTotalQty);
        metricsPanel.Controls.Add(tileLow);
        metricsPanel.Controls.Add(tileOut);
        metricsPanel.Dock = DockStyle.Top;
        metricsPanel.Padding = new Padding(0, 4, 0, 12);
        metricsPanel.Size = new Size(1000, 118);
        metricsPanel.Name = "metricsPanel";
        metricsPanel.WrapContents = false;

        ConfigureMetricTile(tileProducts, capProducts, lblProducts, subProducts,
            "Active Products", "In catalog",
            accentGreen, Color.White, Color.FromArgb(200, 230, 210), 0, true);

        ConfigureMetricTile(tileCategories, capCategories, lblCategories, subCategories,
            "Categories", "Groupings",
            Color.White, Color.FromArgb(24, 32, 40), Color.FromArgb(120, 130, 145), 1, false);

        ConfigureMetricTile(tileSuppliers, capSuppliers, lblSuppliers, subSuppliers,
            "Suppliers", "Active vendors",
            Color.White, Color.FromArgb(24, 32, 40), Color.FromArgb(120, 130, 145), 2, false);

        ConfigureMetricTile(tileTotalQty, capTotalQty, lblTotalQty, subTotalQty,
            "Units on hand", "Total quantity",
            Color.White, Color.FromArgb(24, 32, 40), Color.FromArgb(120, 130, 145), 3, false);

        ConfigureMetricTile(tileLow, capLow, lblLow, subLow,
            "Low stock", "Below minimum",
            Color.White, Color.FromArgb(199, 119, 0), Color.FromArgb(160, 120, 60), 4, false);

        ConfigureMetricTile(tileOut, capOut, lblOut, subOut,
            "Out of stock", "Zero quantity",
            Color.White, Color.FromArgb(192, 57, 43), Color.FromArgb(160, 90, 80), 5, false);

        lblError.Dock = DockStyle.Top;
        lblError.ForeColor = Color.FromArgb(192, 57, 43);
        lblError.Font = new Font("Segoe UI", 9F);
        lblError.Height = 22;
        lblError.Name = "lblError";
        lblError.Visible = false;

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
        activityHeader.BackColor = Color.White;
        activityHeader.Name = "activityHeader";

        lblAct.AutoSize = true;
        lblAct.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        lblAct.ForeColor = Color.FromArgb(24, 32, 40);
        lblAct.Location = new Point(0, 4);
        lblAct.Name = "lblAct";
        lblAct.Text = "Recent Activity";

        lnkViewAll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lnkViewAll.AutoSize = true;
        lnkViewAll.LinkBehavior = LinkBehavior.HoverUnderline;
        lnkViewAll.LinkColor = accentGreen;
        lnkViewAll.Location = new Point(360, 6);
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
        lblEmpty.ForeColor = Color.FromArgb(120, 130, 145);
        lblEmpty.Name = "lblEmpty";
        lblEmpty.Text = "No recent activity yet.";
        lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
        lblEmpty.Visible = false;

        insightsCard.BackColor = Color.White;
        insightsCard.BorderStyle = BorderStyle.FixedSingle;
        insightsCard.Dock = DockStyle.Fill;
        insightsCard.Margin = new Padding(0, 0, 10, 0);
        insightsCard.Padding = new Padding(12);
        insightsCard.Name = "insightsCard";

        lblInsightsTitle.Dock = DockStyle.Top;
        lblInsightsTitle.Height = 28;
        lblInsightsTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        lblInsightsTitle.ForeColor = Color.FromArgb(24, 32, 40);
        lblInsightsTitle.Text = "Insights";
        lblInsightsTitle.TextAlign = ContentAlignment.MiddleLeft;
        lblInsightsTitle.Name = "lblInsightsTitle";

        lblHealthTitle.Dock = DockStyle.Top;
        lblHealthTitle.Height = 20;
        lblHealthTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblHealthTitle.ForeColor = Color.FromArgb(100, 110, 125);
        lblHealthTitle.Text = "Stock health";
        lblHealthTitle.Name = "lblHealthTitle";

        pnlStockHealth.Dock = DockStyle.Top;
        pnlStockHealth.Height = 148;
        pnlStockHealth.BackColor = Color.White;
        pnlStockHealth.Name = "pnlStockHealth";

        lblMovementTitle.Dock = DockStyle.Top;
        lblMovementTitle.Height = 20;
        lblMovementTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblMovementTitle.ForeColor = Color.FromArgb(100, 110, 125);
        lblMovementTitle.Text = "Weekly movement";
        lblMovementTitle.Name = "lblMovementTitle";

        pnlMovement.Dock = DockStyle.Fill;
        pnlMovement.BackColor = Color.White;
        pnlMovement.Name = "pnlMovement";

        insightsCard.Controls.Add(pnlMovement);
        insightsCard.Controls.Add(lblMovementTitle);
        insightsCard.Controls.Add(pnlStockHealth);
        insightsCard.Controls.Add(lblHealthTitle);
        insightsCard.Controls.Add(lblInsightsTitle);

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
        lblQa.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        lblQa.ForeColor = Color.FromArgb(24, 32, 40);
        lblQa.Height = 30;
        lblQa.Name = "lblQa";
        lblQa.Text = "Quick Actions";
        lblQa.TextAlign = ContentAlignment.MiddleLeft;

        ConfigureQaButton(btnQaStockIn, "btnQaStockIn", "  Record Stock-In", 42, true, accentGreen, cardBorder);
        ConfigureQaButton(btnQaStockOut, "btnQaStockOut", "  Record Stock-Out", 86, false, accentGreen, cardBorder);
        ConfigureQaButton(btnQaProducts, "btnQaProducts", "  Search Products", 130, false, accentGreen, cardBorder);
        ConfigureQaButton(btnQaAdjustment, "btnQaAdjustment", "  Inventory Adjustment", 174, false, accentGreen, cardBorder);
        ConfigureQaButton(btnQaUsers, "btnQaUsers", "  Manage Users", 218, false, accentGreen, cardBorder);

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
        Padding = new Padding(18, 6, 18, 10);
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
        Panel tile,
        Label cap, Label value, Label sub,
        string capText, string subText,
        Color tileBg, Color valueColor, Color mutedColor,
        int tab, bool primary)
    {
        tile.BackColor = tileBg;
        tile.BorderStyle = primary ? BorderStyle.None : BorderStyle.FixedSingle;
        tile.Margin = new Padding(0, 0, 10, 0);
        tile.Size = new Size(158, 100);
        tile.TabIndex = tab;
        tile.Padding = new Padding(12, 10, 12, 8);

        cap.AutoSize = false;
        cap.Font = new Font("Segoe UI", 8F);
        cap.ForeColor = primary ? Color.FromArgb(200, 230, 210) : Color.FromArgb(110, 120, 135);
        cap.Location = new Point(12, 10);
        cap.Size = new Size(134, 18);
        cap.Text = capText;

        value.AutoSize = false;
        value.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        value.ForeColor = valueColor;
        value.Location = new Point(12, 32);
        value.Size = new Size(134, 36);
        value.Text = "—";

        sub.AutoSize = false;
        sub.Font = new Font("Segoe UI", 7.5F);
        sub.ForeColor = mutedColor;
        sub.Location = new Point(12, 72);
        sub.Size = new Size(134, 18);
        sub.Text = subText;

        tile.Controls.Add(sub);
        tile.Controls.Add(value);
        tile.Controls.Add(cap);
    }

    private static void ConfigureQaButton(
        Button btn, string name, string text, int top, bool primary,
        Color accentGreen, Color cardBorder)
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
            btn.BackColor = accentGreen;
            btn.ForeColor = Color.White;
            btn.FlatAppearance.BorderSize = 0;
        }
        else
        {
            btn.BackColor = Color.White;
            btn.ForeColor = accentGreen;
            btn.FlatAppearance.BorderColor = cardBorder;
            btn.FlatAppearance.BorderSize = 1;
        }
    }
}
