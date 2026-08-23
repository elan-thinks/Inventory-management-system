using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms;

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

    // Kept for compatibility with existing code references (hidden; values painted on panels)
    private Label lblProducts;
    private Label lblCategories;
    private Label lblSuppliers;
    private Label lblTotalQty;
    private Label lblLow;
    private Label lblOut;
    private Label capProducts;
    private Label capCategories;
    private Label capSuppliers;
    private Label capTotalQty;
    private Label capLow;
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
        tileCategories = new Panel();
        tileSuppliers = new Panel();
        tileTotalQty = new Panel();
        tileLow = new Panel();
        tileOut = new Panel();

        lblProducts = new Label();
        lblCategories = new Label();
        lblSuppliers = new Label();
        lblTotalQty = new Label();
        lblLow = new Label();
        lblOut = new Label();
        capProducts = new Label();
        capCategories = new Label();
        capSuppliers = new Label();
        capTotalQty = new Label();
        capLow = new Label();
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
        bottom.SuspendLayout();
        activityCard.SuspendLayout();
        activityHeader.SuspendLayout();
        actionsPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        SuspendLayout();

        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        FormBorderStyle = FormBorderStyle.None;
        Dock = DockStyle.Fill;
        TopLevel = false;
        Name = "DashboardForm";
        Text = "Dashboard";
        Padding = new Padding(12, 8, 12, 8);

        // Empty panels — content drawn in Paint (bulletproof visibility)
        InitTile(tileProducts, "tileProducts");
        InitTile(tileCategories, "tileCategories");
        InitTile(tileSuppliers, "tileSuppliers");
        InitTile(tileTotalQty, "tileTotalQty");
        InitTile(tileLow, "tileLow");
        InitTile(tileOut, "tileOut");

        // Hidden labels still hold string values for SetMetric
        HideValueLabel(lblProducts, "lblProducts");
        HideValueLabel(lblCategories, "lblCategories");
        HideValueLabel(lblSuppliers, "lblSuppliers");
        HideValueLabel(lblTotalQty, "lblTotalQty");
        HideValueLabel(lblLow, "lblLow");
        HideValueLabel(lblOut, "lblOut");

        metricsPanel.Dock = DockStyle.Top;
        metricsPanel.Height = 128;
        metricsPanel.Padding = new Padding(0, 0, 0, 12);
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

        metricsPanel.ResumeLayout(true);
        activityHeader.ResumeLayout(false);
        activityHeader.PerformLayout();
        activityCard.ResumeLayout(false);
        actionsPanel.ResumeLayout(false);
        bottom.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(true);
    }

    private static void InitTile(Panel tile, string name)
    {
        tile.Size = new Size(175, 110);
        tile.Margin = new Padding(0, 0, 10, 0);
        tile.Name = name;
    }

    private static void HideValueLabel(Label lbl, string name)
    {
        lbl.Text = "0";
        lbl.Visible = false;
        lbl.Name = name;
        lbl.Size = new Size(1, 1);
    }
}
