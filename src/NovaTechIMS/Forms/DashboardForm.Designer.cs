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
    private Panel activityPanel;
    private Label lblAct;
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
        activityPanel = new Panel();
        lblAct = new Label();
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
        activityPanel.SuspendLayout();
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

        // ---- Metric tiles (explicit, Designer-safe) ----
        ConfigureTile(tileProducts, lblProducts, capProducts, "lblProducts", "Active products");
        ConfigureTile(tileCategories, lblCategories, capCategories, "lblCategories", "Categories");
        ConfigureTile(tileSuppliers, lblSuppliers, capSuppliers, "lblSuppliers", "Suppliers");
        ConfigureTile(tileTotalQty, lblTotalQty, capTotalQty, "lblTotalQty", "Total qty on hand");
        ConfigureTile(tileLow, lblLow, capLow, "lblLow", "Low stock");
        ConfigureTile(tileOut, lblOut, capOut, "lblOut", "Out of stock");

        metricsPanel.Dock = DockStyle.Top;
        metricsPanel.Height = 110;
        metricsPanel.Padding = new Padding(0, 8, 0, 8);
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
        bottom.Padding = new Padding(0, 8, 0, 0);
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
        bottom.Name = "bottom";

        activityPanel.Dock = DockStyle.Fill;
        activityPanel.Padding = new Padding(0, 0, 8, 0);
        activityPanel.Name = "activityPanel";

        lblAct.Text = "Recent activity";
        lblAct.Dock = DockStyle.Top;
        lblAct.Height = 28;
        lblAct.Name = "lblAct";

        grid.Dock = DockStyle.Fill;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.ReadOnly = true;
        grid.MultiSelect = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.Name = "grid";

        lblEmpty.Text = "No recent activity yet — record your first Stock-In to get started.";
        lblEmpty.Dock = DockStyle.Fill;
        lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
        lblEmpty.Visible = false;
        lblEmpty.Name = "lblEmpty";

        activityPanel.Controls.Add(grid);
        activityPanel.Controls.Add(lblEmpty);
        activityPanel.Controls.Add(lblAct);

        actionsPanel.Dock = DockStyle.Fill;
        actionsPanel.BackColor = Color.White;
        actionsPanel.Padding = new Padding(12);
        actionsPanel.Name = "actionsPanel";

        lblQa.Text = "Quick actions";
        lblQa.Dock = DockStyle.Top;
        lblQa.Height = 28;
        lblQa.Name = "lblQa";
        actionsPanel.Controls.Add(lblQa);

        bottom.Controls.Add(activityPanel, 0, 0);
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
        activityPanel.ResumeLayout(false);
        actionsPanel.ResumeLayout(false);
        bottom.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(false);
    }

    private static void ConfigureTile(Panel tile, Label valueLabel, Label capLabel, string valueName, string caption)
    {
        tile.Width = 140;
        tile.Height = 88;
        tile.Margin = new Padding(0, 0, 10, 0);
        tile.BackColor = Color.White;
        tile.Padding = new Padding(10);
        tile.Name = "tile" + valueName;

        valueLabel.Text = "—";
        valueLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        valueLabel.Location = new Point(10, 12);
        valueLabel.AutoSize = true;
        valueLabel.Name = valueName;

        capLabel.Text = caption;
        capLabel.Font = new Font("Segoe UI", 9F);
        capLabel.Location = new Point(10, 52);
        capLabel.AutoSize = true;
        capLabel.Name = "cap" + valueName;

        tile.Controls.Add(valueLabel);
        tile.Controls.Add(capLabel);
    }
}
