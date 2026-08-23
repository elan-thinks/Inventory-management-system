using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms;

partial class DashboardForm
{
    private System.ComponentModel.IContainer components = null;

    private FlowLayoutPanel metricsPanel;
    private Panel tileProducts;
    private Label lblProducts;
    private Label capProducts;
    private Panel tileCategories;
    private Label lblCategories;
    private Label capCategories;
    private Panel tileSuppliers;
    private Label lblSuppliers;
    private Label capSuppliers;
    private Panel tileTotalQty;
    private Label lblTotalQty;
    private Label capTotalQty;
    private Panel tileLow;
    private Label lblLow;
    private Label capLow;
    private Panel tileOut;
    private Label lblOut;
    private Label capOut;

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

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        metricsPanel = new FlowLayoutPanel();
        tileProducts = new Panel();
        lblProducts = new Label();
        capProducts = new Label();
        tileCategories = new Panel();
        lblCategories = new Label();
        capCategories = new Label();
        tileSuppliers = new Panel();
        lblSuppliers = new Label();
        capSuppliers = new Label();
        tileTotalQty = new Panel();
        lblTotalQty = new Label();
        capTotalQty = new Label();
        tileLow = new Panel();
        lblLow = new Label();
        capLow = new Label();
        tileOut = new Panel();
        lblOut = new Label();
        capOut = new Label();

        bottom = new TableLayoutPanel();
        activityCard = new Panel();
        activityHeader = new Panel();
        lblAct = new Label();
        lnkViewAll = new LinkLabel();
        grid = new DataGridView();
        lblEmpty = new Label();
        actionsPanel = new Panel();
        lblQa = new Label();
        lblError = new Label();

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
        actionsPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        SuspendLayout();

        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.FromArgb(244, 246, 248);
        FormBorderStyle = FormBorderStyle.None;
        Dock = DockStyle.Fill;
        TopLevel = false;
        Name = "DashboardForm";
        Text = "Dashboard";
        Padding = new Padding(12, 8, 12, 8);

        BuildMetricTile(tileProducts, lblProducts, capProducts, "lblProducts", "Total Active Products");
        BuildMetricTile(tileCategories, lblCategories, capCategories, "lblCategories", "Total Categories");
        BuildMetricTile(tileSuppliers, lblSuppliers, capSuppliers, "lblSuppliers", "Total Suppliers");
        BuildMetricTile(tileTotalQty, lblTotalQty, capTotalQty, "lblTotalQty", "Total Inventory Quantity");
        BuildMetricTile(tileLow, lblLow, capLow, "lblLow", "Low-Stock Products");
        BuildMetricTile(tileOut, lblOut, capOut, "lblOut", "Out-of-Stock Products");

        metricsPanel.Dock = DockStyle.Top;
        metricsPanel.Height = 130;
        metricsPanel.Padding = new Padding(0, 0, 0, 12);
        metricsPanel.WrapContents = false;
        metricsPanel.AutoScroll = true;
        metricsPanel.BackColor = Color.FromArgb(244, 246, 248);
        metricsPanel.Name = "metricsPanel";
        metricsPanel.Controls.Add(tileProducts);
        metricsPanel.Controls.Add(tileCategories);
        metricsPanel.Controls.Add(tileSuppliers);
        metricsPanel.Controls.Add(tileTotalQty);
        metricsPanel.Controls.Add(tileLow);
        metricsPanel.Controls.Add(tileOut);

        lblError.Dock = DockStyle.Top;
        lblError.Height = 28;
        lblError.Visible = false;
        lblError.Name = "lblError";

        bottom.Dock = DockStyle.Fill;
        bottom.ColumnCount = 2;
        bottom.RowCount = 1;
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 72F));
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
        bottom.Name = "bottom";

        activityCard.Dock = DockStyle.Fill;
        activityCard.BackColor = Color.White;
        activityCard.Padding = new Padding(16);
        activityCard.Margin = new Padding(0, 0, 12, 0);
        activityCard.Name = "activityCard";

        activityHeader.Dock = DockStyle.Top;
        activityHeader.Height = 36;
        activityHeader.Name = "activityHeader";

        lblAct.Text = "Recent Activity";
        lblAct.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblAct.AutoSize = true;
        lblAct.Location = new Point(0, 6);
        lblAct.Name = "lblAct";

        lnkViewAll.Text = "View all  >";
        lnkViewAll.AutoSize = true;
        lnkViewAll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lnkViewAll.Location = new Point(420, 8);
        lnkViewAll.Name = "lnkViewAll";
        lnkViewAll.LinkColor = Color.FromArgb(30, 75, 143);
        lnkViewAll.LinkBehavior = LinkBehavior.HoverUnderline;
        lnkViewAll.Click += LnkViewAll_Click;

        activityHeader.Controls.Add(lblAct);
        activityHeader.Controls.Add(lnkViewAll);

        grid.Dock = DockStyle.Fill;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.ReadOnly = true;
        grid.MultiSelect = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.Name = "grid";
        grid.BackgroundColor = Color.White;
        grid.BorderStyle = BorderStyle.None;

        lblEmpty.Text = "No recent activity yet — record your first Stock-In to get started.";
        lblEmpty.Dock = DockStyle.Fill;
        lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
        lblEmpty.Visible = false;
        lblEmpty.Name = "lblEmpty";

        activityCard.Controls.Add(grid);
        activityCard.Controls.Add(lblEmpty);
        activityCard.Controls.Add(activityHeader);

        actionsPanel.Dock = DockStyle.Fill;
        actionsPanel.BackColor = Color.White;
        actionsPanel.Padding = new Padding(16);
        actionsPanel.Name = "actionsPanel";

        lblQa.Text = "Quick Actions";
        lblQa.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblQa.Dock = DockStyle.Top;
        lblQa.Height = 36;
        lblQa.Name = "lblQa";
        actionsPanel.Controls.Add(lblQa);

        bottom.Controls.Add(activityCard, 0, 0);
        bottom.Controls.Add(actionsPanel, 1, 0);

        // Order matters for Dock: Fill first, then Top panels
        Controls.Add(bottom);
        Controls.Add(lblError);
        Controls.Add(metricsPanel);

        tileProducts.ResumeLayout(true);
        tileCategories.ResumeLayout(true);
        tileSuppliers.ResumeLayout(true);
        tileTotalQty.ResumeLayout(true);
        tileLow.ResumeLayout(true);
        tileOut.ResumeLayout(true);
        metricsPanel.ResumeLayout(true);
        activityHeader.ResumeLayout(false);
        activityHeader.PerformLayout();
        activityCard.ResumeLayout(false);
        actionsPanel.ResumeLayout(false);
        bottom.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(true);
    }

    /// <summary>
    /// Dock-based tile: caption on top, big number below.
    /// Uses BorderStyle (always visible) — avoids Region clipping that hid labels.
    /// </summary>
    private static void BuildMetricTile(Panel tile, Label valueLabel, Label capLabel, string valueName, string caption)
    {
        tile.Size = new Size(175, 110);
        tile.Margin = new Padding(0, 0, 10, 0);
        tile.BackColor = Color.White;
        tile.BorderStyle = BorderStyle.FixedSingle;
        tile.Name = "tile" + valueName;

        // Caption (top)
        capLabel.Text = caption;
        capLabel.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular);
        capLabel.ForeColor = Color.FromArgb(90, 100, 110);
        capLabel.BackColor = Color.White;
        capLabel.Dock = DockStyle.Top;
        capLabel.Height = 42;
        capLabel.AutoSize = false;
        capLabel.TextAlign = ContentAlignment.BottomLeft;
        capLabel.Padding = new Padding(12, 0, 8, 2);
        capLabel.Name = "cap" + valueName;

        // Value (fill remainder)
        valueLabel.Text = "0";
        valueLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        valueLabel.ForeColor = Color.FromArgb(31, 41, 51);
        valueLabel.BackColor = Color.White;
        valueLabel.Dock = DockStyle.Fill;
        valueLabel.AutoSize = false;
        valueLabel.TextAlign = ContentAlignment.TopLeft;
        valueLabel.Padding = new Padding(12, 4, 8, 8);
        valueLabel.Name = valueName;

        // Dock order: Fill first, then Top
        tile.Controls.Add(valueLabel);
        tile.Controls.Add(capLabel);
    }
}
