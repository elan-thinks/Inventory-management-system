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
        grid = new DataGridView();
        lblEmpty = new Label();
        activityHeader = new Panel();
        lblAct = new Label();
        lnkViewAll = new LinkLabel();
        actionsPanel = new Panel();
        lblQa = new Label();
        lblError = new Label();
        metricsPanel.SuspendLayout();
        bottom.SuspendLayout();
        activityCard.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        activityHeader.SuspendLayout();
        actionsPanel.SuspendLayout();
        SuspendLayout();
        // 
        // metricsPanel
        // 
        metricsPanel.AutoScroll = true;
        metricsPanel.Controls.Add(tileProducts);
        metricsPanel.Controls.Add(tileCategories);
        metricsPanel.Controls.Add(tileSuppliers);
        metricsPanel.Controls.Add(tileTotalQty);
        metricsPanel.Controls.Add(tileLow);
        metricsPanel.Controls.Add(tileOut);
        metricsPanel.Dock = DockStyle.Top;
        metricsPanel.Location = new Point(12, 8);
        metricsPanel.Name = "metricsPanel";
        metricsPanel.Padding = new Padding(0, 0, 0, 12);
        metricsPanel.Size = new Size(1090, 128);
        metricsPanel.TabIndex = 2;
        metricsPanel.WrapContents = false;
        // 
        // tileProducts
        // 
        tileProducts.Location = new Point(3, 3);
        tileProducts.Name = "tileProducts";
        tileProducts.Size = new Size(200, 100);
        tileProducts.TabIndex = 0;
        // 
        // tileCategories
        // 
        tileCategories.Location = new Point(209, 3);
        tileCategories.Name = "tileCategories";
        tileCategories.Size = new Size(200, 100);
        tileCategories.TabIndex = 1;
        // 
        // tileSuppliers
        // 
        tileSuppliers.Location = new Point(415, 3);
        tileSuppliers.Name = "tileSuppliers";
        tileSuppliers.Size = new Size(200, 100);
        tileSuppliers.TabIndex = 2;
        // 
        // tileTotalQty
        // 
        tileTotalQty.Location = new Point(621, 3);
        tileTotalQty.Name = "tileTotalQty";
        tileTotalQty.Size = new Size(200, 100);
        tileTotalQty.TabIndex = 3;
        // 
        // tileLow
        // 
        tileLow.Location = new Point(827, 3);
        tileLow.Name = "tileLow";
        tileLow.Size = new Size(200, 100);
        tileLow.TabIndex = 4;
        // 
        // tileOut
        // 
        tileOut.Location = new Point(1033, 3);
        tileOut.Name = "tileOut";
        tileOut.Size = new Size(200, 100);
        tileOut.TabIndex = 5;
        // 
        // lblProducts
        // 
        lblProducts.Location = new Point(0, 0);
        lblProducts.Name = "lblProducts";
        lblProducts.Size = new Size(100, 23);
        lblProducts.TabIndex = 0;
        // 
        // lblCategories
        // 
        lblCategories.Location = new Point(0, 0);
        lblCategories.Name = "lblCategories";
        lblCategories.Size = new Size(100, 23);
        lblCategories.TabIndex = 0;
        // 
        // lblSuppliers
        // 
        lblSuppliers.Location = new Point(0, 0);
        lblSuppliers.Name = "lblSuppliers";
        lblSuppliers.Size = new Size(100, 23);
        lblSuppliers.TabIndex = 0;
        // 
        // lblTotalQty
        // 
        lblTotalQty.Location = new Point(0, 0);
        lblTotalQty.Name = "lblTotalQty";
        lblTotalQty.Size = new Size(100, 23);
        lblTotalQty.TabIndex = 0;
        // 
        // lblLow
        // 
        lblLow.Location = new Point(0, 0);
        lblLow.Name = "lblLow";
        lblLow.Size = new Size(100, 23);
        lblLow.TabIndex = 0;
        // 
        // lblOut
        // 
        lblOut.Location = new Point(0, 0);
        lblOut.Name = "lblOut";
        lblOut.Size = new Size(100, 23);
        lblOut.TabIndex = 0;
        // 
        // capProducts
        // 
        capProducts.Location = new Point(0, 0);
        capProducts.Name = "capProducts";
        capProducts.Size = new Size(100, 23);
        capProducts.TabIndex = 0;
        // 
        // capCategories
        // 
        capCategories.Location = new Point(0, 0);
        capCategories.Name = "capCategories";
        capCategories.Size = new Size(100, 23);
        capCategories.TabIndex = 0;
        // 
        // capSuppliers
        // 
        capSuppliers.Location = new Point(0, 0);
        capSuppliers.Name = "capSuppliers";
        capSuppliers.Size = new Size(100, 23);
        capSuppliers.TabIndex = 0;
        // 
        // capTotalQty
        // 
        capTotalQty.Location = new Point(0, 0);
        capTotalQty.Name = "capTotalQty";
        capTotalQty.Size = new Size(100, 23);
        capTotalQty.TabIndex = 0;
        // 
        // capLow
        // 
        capLow.Location = new Point(0, 0);
        capLow.Name = "capLow";
        capLow.Size = new Size(100, 23);
        capLow.TabIndex = 0;
        // 
        // capOut
        // 
        capOut.Location = new Point(0, 0);
        capOut.Name = "capOut";
        capOut.Size = new Size(100, 23);
        capOut.TabIndex = 0;
        // 
        // bottom
        // 
        bottom.ColumnCount = 2;
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 72F));
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
        bottom.Controls.Add(activityCard, 0, 0);
        bottom.Controls.Add(actionsPanel, 1, 0);
        bottom.Dock = DockStyle.Fill;
        bottom.Location = new Point(12, 164);
        bottom.Name = "bottom";
        bottom.RowCount = 1;
        bottom.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        bottom.Size = new Size(1090, 326);
        bottom.TabIndex = 0;
        // 
        // activityCard
        // 
        activityCard.Controls.Add(grid);
        activityCard.Controls.Add(lblEmpty);
        activityCard.Controls.Add(activityHeader);
        activityCard.Dock = DockStyle.Fill;
        activityCard.Location = new Point(0, 0);
        activityCard.Margin = new Padding(0, 0, 12, 0);
        activityCard.Name = "activityCard";
        activityCard.Padding = new Padding(16);
        activityCard.Size = new Size(772, 326);
        activityCard.TabIndex = 0;
        // 
        // grid
        // 
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.BorderStyle = BorderStyle.None;
        grid.ColumnHeadersHeight = 29;
        grid.Dock = DockStyle.Fill;
        grid.Location = new Point(16, 52);
        grid.MultiSelect = false;
        grid.Name = "grid";
        grid.ReadOnly = true;
        grid.RowHeadersWidth = 51;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.Size = new Size(740, 258);
        grid.TabIndex = 0;
        // 
        // lblEmpty
        // 
        lblEmpty.Dock = DockStyle.Fill;
        lblEmpty.Location = new Point(16, 52);
        lblEmpty.Name = "lblEmpty";
        lblEmpty.Size = new Size(740, 258);
        lblEmpty.TabIndex = 1;
        lblEmpty.Text = "No recent activity yet — record your first Stock-In to get started.";
        lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
        lblEmpty.Visible = false;
        // 
        // activityHeader
        // 
        activityHeader.Controls.Add(lblAct);
        activityHeader.Controls.Add(lnkViewAll);
        activityHeader.Dock = DockStyle.Top;
        activityHeader.Location = new Point(16, 16);
        activityHeader.Name = "activityHeader";
        activityHeader.Size = new Size(740, 36);
        activityHeader.TabIndex = 2;
        // 
        // lblAct
        // 
        lblAct.AutoSize = true;
        lblAct.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblAct.Location = new Point(0, 6);
        lblAct.Name = "lblAct";
        lblAct.Size = new Size(143, 25);
        lblAct.TabIndex = 0;
        lblAct.Text = "Recent Activity";
        lblAct.Click += lblAct_Click;
        // 
        // lnkViewAll
        // 
        lnkViewAll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lnkViewAll.AutoSize = true;
        lnkViewAll.LinkBehavior = LinkBehavior.HoverUnderline;
        lnkViewAll.Location = new Point(960, 8);
        lnkViewAll.Name = "lnkViewAll";
        lnkViewAll.Size = new Size(90, 23);
        lnkViewAll.TabIndex = 1;
        lnkViewAll.TabStop = true;
        lnkViewAll.Text = "View all  >";
        lnkViewAll.Click += LnkViewAll_Click;
        // 
        // actionsPanel
        // 
        actionsPanel.Controls.Add(lblQa);
        actionsPanel.Dock = DockStyle.Fill;
        actionsPanel.Location = new Point(787, 3);
        actionsPanel.Name = "actionsPanel";
        actionsPanel.Padding = new Padding(16);
        actionsPanel.Size = new Size(300, 320);
        actionsPanel.TabIndex = 1;
        // 
        // lblQa
        // 
        lblQa.Dock = DockStyle.Top;
        lblQa.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblQa.Location = new Point(16, 16);
        lblQa.Name = "lblQa";
        lblQa.Size = new Size(268, 51);
        lblQa.TabIndex = 0;
        lblQa.Text = "Quick Actions";
        // 
        // lblError
        // 
        lblError.Dock = DockStyle.Top;
        lblError.Location = new Point(12, 136);
        lblError.Name = "lblError";
        lblError.Size = new Size(1090, 28);
        lblError.TabIndex = 1;
        lblError.Visible = false;
        // 
        // DashboardForm
        // 
        AutoScaleDimensions = new SizeF(9F, 23F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1114, 498);
        Controls.Add(bottom);
        Controls.Add(lblError);
        Controls.Add(metricsPanel);
        Font = new Font("Segoe UI", 10F);
        FormBorderStyle = FormBorderStyle.None;
        Name = "DashboardForm";
        Padding = new Padding(12, 8, 12, 8);
        Text = "Dashboard";
        metricsPanel.ResumeLayout(false);
        bottom.ResumeLayout(false);
        activityCard.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        activityHeader.ResumeLayout(false);
        activityHeader.PerformLayout();
        actionsPanel.ResumeLayout(false);
        ResumeLayout(false);
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
