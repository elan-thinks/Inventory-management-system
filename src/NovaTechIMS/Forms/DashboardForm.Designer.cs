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
        Padding = new Padding(8, 4, 8, 8);

        // Mockup layout: caption on top, large number below
        ConfigureTile(tileProducts, lblProducts, capProducts, "lblProducts", "Total Active Products");
        ConfigureTile(tileCategories, lblCategories, capCategories, "lblCategories", "Total Categories");
        ConfigureTile(tileSuppliers, lblSuppliers, capSuppliers, "lblSuppliers", "Total Suppliers");
        ConfigureTile(tileTotalQty, lblTotalQty, capTotalQty, "lblTotalQty", "Total Inventory Quantity");
        ConfigureTile(tileLow, lblLow, capLow, "lblLow", "Low-Stock Products");
        ConfigureTile(tileOut, lblOut, capOut, "lblOut", "Out-of-Stock Products");

        metricsPanel.Dock = DockStyle.Top;
        metricsPanel.Height = 120;
        metricsPanel.Padding = new Padding(0, 4, 0, 12);
        metricsPanel.WrapContents = false;
        metricsPanel.AutoScroll = true;
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
        lnkViewAll.ActiveLinkColor = Color.FromArgb(22, 58, 110);
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

        Controls.Add(bottom);
        Controls.Add(lblError);
        Controls.Add(metricsPanel);

        tileProducts.ResumeLayout(false);
        tileProducts.PerformLayout();
        tileCategories.ResumeLayout(false);
        tileCategories.PerformLayout();
        tileSuppliers.ResumeLayout(false);
        tileSuppliers.PerformLayout();
        tileTotalQty.ResumeLayout(false);
        tileTotalQty.PerformLayout();
        tileLow.ResumeLayout(false);
        tileLow.PerformLayout();
        tileOut.ResumeLayout(false);
        tileOut.PerformLayout();
        metricsPanel.ResumeLayout(false);
        activityHeader.ResumeLayout(false);
        activityHeader.PerformLayout();
        activityCard.ResumeLayout(false);
        actionsPanel.ResumeLayout(false);
        bottom.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(false);
    }

    /// <summary>Caption on top (mockup), large value below. No Region clipping.</summary>
    private static void ConfigureTile(Panel tile, Label valueLabel, Label capLabel, string valueName, string caption)
    {
        tile.Width = 168;
        tile.Height = 100;
        tile.Margin = new Padding(0, 0, 12, 0);
        tile.BackColor = Color.White;
        tile.Padding = new Padding(14, 12, 14, 12);
        tile.Name = "tile" + valueName;

        // Caption first (top) — matches mockup
        capLabel.Text = caption;
        capLabel.Font = new Font("Segoe UI", 8.5F);
        capLabel.ForeColor = Color.FromArgb(107, 119, 133);
        capLabel.Location = new Point(14, 12);
        capLabel.AutoSize = true;
        capLabel.MaximumSize = new Size(140, 0);
        capLabel.Name = "cap" + valueName;

        valueLabel.Text = "—";
        valueLabel.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        valueLabel.ForeColor = Color.FromArgb(31, 41, 51);
        valueLabel.Location = new Point(14, 48);
        valueLabel.AutoSize = true;
        valueLabel.Name = valueName;

        tile.Controls.Add(valueLabel);
        tile.Controls.Add(capLabel);
    }
}
