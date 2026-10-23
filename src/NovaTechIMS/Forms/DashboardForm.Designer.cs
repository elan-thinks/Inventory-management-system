using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms;

/// <summary>
/// Dashboard — SRMS-inspired metric cards (colored icon chips) + activity + QA.
/// All controls on the design surface for View Designer.
/// </summary>
partial class DashboardForm
{
    private System.ComponentModel.IContainer components = null;

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
    private Label capCategories;
    private Label lblCategories;
    private Label capSuppliers;
    private Label lblSuppliers;
    private Label capTotalQty;
    private Label lblTotalQty;
    private Label capLow;
    private Label lblLow;
    private Label capOut;
    private Label lblOut;

    private Label lblError;
    private TableLayoutPanel bottom;
    private Panel activityCard;
    private Panel activityHeader;
    private Label lblAct;
    private LinkLabel lnkViewAll;
    private DataGridView grid;
    private Label lblEmpty;
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
        metricsPanel = new FlowLayoutPanel();
        tileProducts = new Panel();
        chipProducts = new Panel();
        icoProducts = new PictureBox();
        capProducts = new Label();
        lblProducts = new Label();
        tileCategories = new Panel();
        chipCategories = new Panel();
        icoCategories = new PictureBox();
        capCategories = new Label();
        lblCategories = new Label();
        tileSuppliers = new Panel();
        chipSuppliers = new Panel();
        icoSuppliers = new PictureBox();
        capSuppliers = new Label();
        lblSuppliers = new Label();
        tileTotalQty = new Panel();
        chipTotalQty = new Panel();
        icoTotalQty = new PictureBox();
        capTotalQty = new Label();
        lblTotalQty = new Label();
        tileLow = new Panel();
        chipLow = new Panel();
        icoLow = new PictureBox();
        capLow = new Label();
        lblLow = new Label();
        tileOut = new Panel();
        chipOut = new Panel();
        icoOut = new PictureBox();
        capOut = new Label();
        lblOut = new Label();
        lblError = new Label();
        bottom = new TableLayoutPanel();
        activityCard = new Panel();
        activityHeader = new Panel();
        lblAct = new Label();
        lnkViewAll = new LinkLabel();
        grid = new DataGridView();
        lblEmpty = new Label();
        actionsPanel = new Panel();
        lblQa = new Label();
        btnQaStockIn = new Button();
        btnQaStockOut = new Button();
        btnQaProducts = new Button();
        btnQaAdjustment = new Button();
        btnQaUsers = new Button();

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
        actionsPanel.SuspendLayout();
        SuspendLayout();

        // Metrics row — full width flow
        metricsPanel.AutoScroll = false;
        metricsPanel.BackColor = Color.FromArgb(244, 246, 248);
        metricsPanel.Controls.Add(tileProducts);
        metricsPanel.Controls.Add(tileCategories);
        metricsPanel.Controls.Add(tileSuppliers);
        metricsPanel.Controls.Add(tileTotalQty);
        metricsPanel.Controls.Add(tileLow);
        metricsPanel.Controls.Add(tileOut);
        metricsPanel.Dock = DockStyle.Top;
        metricsPanel.Padding = new Padding(0, 4, 0, 12);
        metricsPanel.Size = new Size(960, 118);
        metricsPanel.Name = "metricsPanel";
        metricsPanel.WrapContents = false;

        // Colored chips inspired by SRMS dashboard
        ConfigureMetricTile(tileProducts, chipProducts, icoProducts,
            capProducts, lblProducts, "Total Active Products",
            Color.FromArgb(232, 240, 254), 0);
        ConfigureMetricTile(tileCategories, chipCategories, icoCategories,
            capCategories, lblCategories, "Total Categories",
            Color.FromArgb(232, 248, 240), 1);
        ConfigureMetricTile(tileSuppliers, chipSuppliers, icoSuppliers,
            capSuppliers, lblSuppliers, "Total Suppliers",
            Color.FromArgb(243, 232, 255), 2);
        ConfigureMetricTile(tileTotalQty, chipTotalQty, icoTotalQty,
            capTotalQty, lblTotalQty, "Total Inventory Quantity",
            Color.FromArgb(255, 247, 230), 3);
        ConfigureMetricTile(tileLow, chipLow, icoLow,
            capLow, lblLow, "Low-Stock Products",
            Color.FromArgb(255, 243, 224), 4, Color.FromArgb(199, 119, 0));
        ConfigureMetricTile(tileOut, chipOut, icoOut,
            capOut, lblOut, "Out-of-Stock Products",
            Color.FromArgb(254, 236, 234), 5, Color.FromArgb(192, 57, 43));

        lblError.Dock = DockStyle.Top;
        lblError.ForeColor = Color.FromArgb(192, 57, 43);
        lblError.Font = new Font("Segoe UI", 9F);
        lblError.Height = 24;
        lblError.Name = "lblError";
        lblError.Visible = false;

        bottom.ColumnCount = 2;
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68F));
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
        bottom.Controls.Add(activityCard, 0, 0);
        bottom.Controls.Add(actionsPanel, 1, 0);
        bottom.Dock = DockStyle.Fill;
        bottom.Name = "bottom";
        bottom.RowCount = 1;
        bottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        bottom.Padding = new Padding(0, 4, 0, 0);

        activityCard.BackColor = Color.White;
        activityCard.BorderStyle = BorderStyle.FixedSingle;
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
        activityHeader.Height = 32;
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
        lnkViewAll.Location = new Point(560, 6);
        lnkViewAll.Name = "lnkViewAll";
        lnkViewAll.Text = "View all  >";
        lnkViewAll.Click += LnkViewAll_Click;

        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AllowUserToResizeRows = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.BackgroundColor = Color.White;
        grid.BorderStyle = BorderStyle.None;
        grid.ColumnHeadersHeight = 34;
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
        lblEmpty.Text = "No recent activity yet — record your first Stock-In to get started.";
        lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
        lblEmpty.Visible = false;

        actionsPanel.BackColor = Color.White;
        actionsPanel.BorderStyle = BorderStyle.FixedSingle;
        actionsPanel.Controls.Add(btnQaUsers);
        actionsPanel.Controls.Add(btnQaAdjustment);
        actionsPanel.Controls.Add(btnQaProducts);
        actionsPanel.Controls.Add(btnQaStockOut);
        actionsPanel.Controls.Add(btnQaStockIn);
        actionsPanel.Controls.Add(lblQa);
        actionsPanel.Dock = DockStyle.Fill;
        actionsPanel.Padding = new Padding(14);
        actionsPanel.Name = "actionsPanel";

        lblQa.Dock = DockStyle.Top;
        lblQa.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblQa.ForeColor = Color.FromArgb(31, 41, 51);
        lblQa.Height = 32;
        lblQa.Name = "lblQa";
        lblQa.Text = "Quick Actions";
        lblQa.TextAlign = ContentAlignment.MiddleLeft;

        ConfigureQaButton(btnQaStockIn, "btnQaStockIn", "  Record Stock-In", 48, true);
        ConfigureQaButton(btnQaStockOut, "btnQaStockOut", "  Record Stock-Out", 92, false);
        ConfigureQaButton(btnQaProducts, "btnQaProducts", "  Search Products", 136, false);
        ConfigureQaButton(btnQaAdjustment, "btnQaAdjustment", "  Inventory Adjustment", 180, false);
        ConfigureQaButton(btnQaUsers, "btnQaUsers", "  Manage Users", 224, false);

        btnQaStockIn.Click += BtnQaStockIn_Click;
        btnQaStockOut.Click += BtnQaStockOut_Click;
        btnQaProducts.Click += BtnQaProducts_Click;
        btnQaAdjustment.Click += BtnQaAdjustment_Click;
        btnQaUsers.Click += BtnQaUsers_Click;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(244, 246, 248);
        ClientSize = new Size(992, 560);
        Controls.Add(bottom);
        Controls.Add(lblError);
        Controls.Add(metricsPanel);
        Font = new Font("Segoe UI", 10F);
        FormBorderStyle = FormBorderStyle.None;
        Name = "DashboardForm";
        Padding = new Padding(20, 12, 20, 12);
        Text = "Dashboard";

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
        actionsPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    private static void ConfigureMetricTile(
        Panel tile, Panel chip, PictureBox ico,
        Label cap, Label value, string capText,
        Color chipColor, int tab,
        Color? valueColor = null)
    {
        tile.BackColor = Color.White;
        tile.BorderStyle = BorderStyle.FixedSingle;
        tile.Margin = new Padding(0, 0, 12, 0);
        tile.Size = new Size(168, 100);
        tile.TabIndex = tab;

        chip.BackColor = chipColor;
        chip.Location = new Point(12, 14);
        chip.Size = new Size(36, 36);
        chip.Name = "chip";

        ico.Location = new Point(8, 8);
        ico.Size = new Size(20, 20);
        ico.SizeMode = PictureBoxSizeMode.Zoom;
        ico.TabStop = false;
        chip.Controls.Add(ico);

        cap.AutoSize = false;
        cap.Font = new Font("Segoe UI", 8F);
        cap.ForeColor = Color.FromArgb(107, 119, 133);
        cap.Location = new Point(56, 12);
        cap.Size = new Size(104, 32);
        cap.Text = capText;

        value.AutoSize = false;
        value.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        value.ForeColor = valueColor ?? Color.FromArgb(31, 41, 51);
        value.Location = new Point(12, 56);
        value.Size = new Size(148, 36);
        value.Text = "0";

        tile.Controls.Add(value);
        tile.Controls.Add(cap);
        tile.Controls.Add(chip);
    }

    private static void ConfigureQaButton(Button btn, string name, string text, int top, bool primary)
    {
        btn.FlatStyle = FlatStyle.Flat;
        btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btn.Location = new Point(14, top);
        btn.Name = name;
        btn.Size = new Size(240, 36);
        btn.Text = text;
        btn.UseVisualStyleBackColor = false;
        btn.Cursor = Cursors.Hand;
        btn.TextAlign = ContentAlignment.MiddleLeft;
        btn.Padding = new Padding(10, 0, 8, 0);

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
