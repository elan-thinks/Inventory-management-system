using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms;

/// <summary>
/// Dashboard layout — all primary controls are on the design surface (drag/drop).
/// Metric values and data load at runtime only.
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
        capProducts = new Label();
        lblProducts = new Label();
        tileCategories = new Panel();
        capCategories = new Label();
        lblCategories = new Label();
        tileSuppliers = new Panel();
        capSuppliers = new Label();
        lblSuppliers = new Label();
        tileTotalQty = new Panel();
        capTotalQty = new Label();
        lblTotalQty = new Label();
        tileLow = new Panel();
        capLow = new Label();
        lblLow = new Label();
        tileOut = new Panel();
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
        tileCategories.SuspendLayout();
        tileSuppliers.SuspendLayout();
        tileTotalQty.SuspendLayout();
        tileLow.SuspendLayout();
        tileOut.SuspendLayout();
        bottom.SuspendLayout();
        activityCard.SuspendLayout();
        activityHeader.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        actionsPanel.SuspendLayout();
        SuspendLayout();

        // ---- metrics row ----
        metricsPanel.AutoScroll = true;
        metricsPanel.BackColor = Color.FromArgb(244, 246, 248);
        metricsPanel.Controls.Add(tileProducts);
        metricsPanel.Controls.Add(tileCategories);
        metricsPanel.Controls.Add(tileSuppliers);
        metricsPanel.Controls.Add(tileTotalQty);
        metricsPanel.Controls.Add(tileLow);
        metricsPanel.Controls.Add(tileOut);
        metricsPanel.Dock = DockStyle.Top;
        metricsPanel.Location = new Point(12, 8);
        metricsPanel.Name = "metricsPanel";
        metricsPanel.Padding = new Padding(0, 0, 0, 8);
        metricsPanel.Size = new Size(960, 120);
        metricsPanel.TabIndex = 0;
        metricsPanel.WrapContents = false;

        ConfigureMetricTile(tileProducts, "tileProducts", capProducts, "capProducts", "Total Active Products",
            lblProducts, "lblProducts", 0);
        ConfigureMetricTile(tileCategories, "tileCategories", capCategories, "capCategories", "Total Categories",
            lblCategories, "lblCategories", 1);
        ConfigureMetricTile(tileSuppliers, "tileSuppliers", capSuppliers, "capSuppliers", "Total Suppliers",
            lblSuppliers, "lblSuppliers", 2);
        ConfigureMetricTile(tileTotalQty, "tileTotalQty", capTotalQty, "capTotalQty", "Total Inventory Qty",
            lblTotalQty, "lblTotalQty", 3);
        ConfigureMetricTile(tileLow, "tileLow", capLow, "capLow", "Low-Stock Products",
            lblLow, "lblLow", 4, Color.FromArgb(199, 119, 0));
        ConfigureMetricTile(tileOut, "tileOut", capOut, "capOut", "Out-of-Stock Products",
            lblOut, "lblOut", 5, Color.FromArgb(192, 57, 43));

        // ---- error ----
        lblError.Dock = DockStyle.Top;
        lblError.ForeColor = Color.FromArgb(192, 57, 43);
        lblError.Font = new Font("Segoe UI", 9F);
        lblError.Location = new Point(12, 128);
        lblError.Name = "lblError";
        lblError.Size = new Size(960, 24);
        lblError.TabIndex = 1;
        lblError.Visible = false;

        // ---- bottom split ----
        bottom.ColumnCount = 2;
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
        bottom.Controls.Add(activityCard, 0, 0);
        bottom.Controls.Add(actionsPanel, 1, 0);
        bottom.Dock = DockStyle.Fill;
        bottom.Location = new Point(12, 152);
        bottom.Name = "bottom";
        bottom.RowCount = 1;
        bottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        bottom.Size = new Size(960, 340);
        bottom.TabIndex = 2;

        // activity card
        activityCard.BackColor = Color.White;
        activityCard.BorderStyle = BorderStyle.FixedSingle;
        activityCard.Controls.Add(grid);
        activityCard.Controls.Add(lblEmpty);
        activityCard.Controls.Add(activityHeader);
        activityCard.Dock = DockStyle.Fill;
        activityCard.Margin = new Padding(0, 0, 10, 0);
        activityCard.Name = "activityCard";
        activityCard.Padding = new Padding(12);
        activityCard.TabIndex = 0;

        activityHeader.Controls.Add(lblAct);
        activityHeader.Controls.Add(lnkViewAll);
        activityHeader.Dock = DockStyle.Top;
        activityHeader.Height = 32;
        activityHeader.Name = "activityHeader";
        activityHeader.TabIndex = 0;

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
        grid.TabIndex = 1;

        lblEmpty.Dock = DockStyle.Fill;
        lblEmpty.Font = new Font("Segoe UI", 10F);
        lblEmpty.ForeColor = Color.FromArgb(107, 119, 133);
        lblEmpty.Name = "lblEmpty";
        lblEmpty.Text = "No recent activity yet — record your first Stock-In to get started.";
        lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
        lblEmpty.Visible = false;

        // quick actions
        actionsPanel.BackColor = Color.White;
        actionsPanel.BorderStyle = BorderStyle.FixedSingle;
        actionsPanel.Controls.Add(btnQaUsers);
        actionsPanel.Controls.Add(btnQaAdjustment);
        actionsPanel.Controls.Add(btnQaProducts);
        actionsPanel.Controls.Add(btnQaStockOut);
        actionsPanel.Controls.Add(btnQaStockIn);
        actionsPanel.Controls.Add(lblQa);
        actionsPanel.Dock = DockStyle.Fill;
        actionsPanel.Name = "actionsPanel";
        actionsPanel.Padding = new Padding(12);
        actionsPanel.TabIndex = 1;

        lblQa.Dock = DockStyle.Top;
        lblQa.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblQa.ForeColor = Color.FromArgb(31, 41, 51);
        lblQa.Height = 32;
        lblQa.Name = "lblQa";
        lblQa.Text = "Quick Actions";
        lblQa.TextAlign = ContentAlignment.MiddleLeft;

        ConfigureQaButton(btnQaStockIn, "btnQaStockIn", "Record Stock-In", 48, true);
        ConfigureQaButton(btnQaStockOut, "btnQaStockOut", "Record Stock-Out", 88, false);
        ConfigureQaButton(btnQaProducts, "btnQaProducts", "Search Products", 128, false);
        ConfigureQaButton(btnQaAdjustment, "btnQaAdjustment", "Inventory Adjustment", 168, false);
        ConfigureQaButton(btnQaUsers, "btnQaUsers", "Manage Users", 208, false);

        btnQaStockIn.Click += (_, _) => Navigate("Stock In");
        btnQaStockOut.Click += (_, _) => Navigate("Stock Out");
        btnQaProducts.Click += (_, _) => Navigate("Products");
        btnQaAdjustment.Click += (_, _) => Navigate("Inventory Adjustment");
        btnQaUsers.Click += (_, _) => Navigate("User Management");

        // form
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(244, 246, 248);
        ClientSize = new Size(984, 500);
        Controls.Add(bottom);
        Controls.Add(lblError);
        Controls.Add(metricsPanel);
        Font = new Font("Segoe UI", 10F);
        FormBorderStyle = FormBorderStyle.None;
        Name = "DashboardForm";
        Padding = new Padding(12, 8, 12, 8);
        Text = "Dashboard";

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
        actionsPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    private static void ConfigureMetricTile(
        Panel tile, string tileName,
        Label cap, string capName, string capText,
        Label value, string valueName,
        int tab,
        Color? valueColor = null)
    {
        tile.BackColor = Color.White;
        tile.BorderStyle = BorderStyle.FixedSingle;
        tile.Margin = new Padding(0, 0, 10, 0);
        tile.Name = tileName;
        tile.Size = new Size(170, 100);
        tile.TabIndex = tab;

        cap.AutoSize = false;
        cap.Font = new Font("Segoe UI", 8.25F);
        cap.ForeColor = Color.FromArgb(107, 119, 133);
        cap.Location = new Point(10, 10);
        cap.Name = capName;
        cap.Size = new Size(148, 32);
        cap.Text = capText;

        value.AutoSize = false;
        value.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        value.ForeColor = valueColor ?? Color.FromArgb(31, 41, 51);
        value.Location = new Point(10, 48);
        value.Name = valueName;
        value.Size = new Size(148, 40);
        value.Text = "0";

        tile.Controls.Add(value);
        tile.Controls.Add(cap);
    }

    private static void ConfigureQaButton(Button btn, string name, string text, int top, bool primary)
    {
        btn.FlatStyle = FlatStyle.Flat;
        btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btn.Location = new Point(12, top);
        btn.Name = name;
        btn.Size = new Size(240, 34);
        btn.TabIndex = 0;
        btn.Text = text;
        btn.UseVisualStyleBackColor = false;
        btn.Cursor = Cursors.Hand;

        if (primary)
        {
            btn.BackColor = Color.FromArgb(30, 75, 143);
            btn.ForeColor = Color.White;
            btn.FlatAppearance.BorderSize = 0;
        }
        else
        {
            btn.BackColor = Color.White;
            btn.ForeColor = Color.FromArgb(31, 41, 51);
            btn.FlatAppearance.BorderColor = Color.FromArgb(215, 220, 225);
            btn.FlatAppearance.BorderSize = 1;
        }
    }
}
