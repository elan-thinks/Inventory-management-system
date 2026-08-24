using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Reports;

/// <summary>Reports hub layout — chooser cards + results visible on design surface.</summary>
partial class ReportsHubForm
{
    private System.ComponentModel.IContainer components = null;

    private Panel pnlChooser;
    private Label lblChoose;
    private FlowLayoutPanel cardsPanel;

    private Panel pnlResults;
    private Panel resultsHeader;
    private Label lblReportTitle;
    private Label lblFilters;
    private Button btnBack;
    private Button btnPrint;
    private DataGridView grid;
    private Label lblSummary;
    private Label lblError;

    private Panel filterBar;
    private Label lblFrom;
    private DateTimePicker dtpFrom;
    private Label lblTo;
    private DateTimePicker dtpTo;

    // Design-time sample cards (runtime rebuilds real cards)
    private Panel cardInventory;
    private Panel cardLowStock;
    private Panel cardOutOfStock;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        pnlChooser = new Panel();
        lblChoose = new Label();
        cardsPanel = new FlowLayoutPanel();
        filterBar = new Panel();
        lblFrom = new Label();
        dtpFrom = new DateTimePicker();
        lblTo = new Label();
        dtpTo = new DateTimePicker();
        cardInventory = new Panel();
        cardLowStock = new Panel();
        cardOutOfStock = new Panel();

        pnlResults = new Panel();
        resultsHeader = new Panel();
        lblReportTitle = new Label();
        lblFilters = new Label();
        btnBack = new Button();
        btnPrint = new Button();
        grid = new DataGridView();
        lblSummary = new Label();
        lblError = new Label();

        pnlChooser.SuspendLayout();
        cardsPanel.SuspendLayout();
        filterBar.SuspendLayout();
        pnlResults.SuspendLayout();
        resultsHeader.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        SuspendLayout();

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.FromArgb(244, 246, 248);
        ClientSize = new Size(960, 600);
        FormBorderStyle = FormBorderStyle.None;
        Name = "ReportsHubForm";
        Text = "Reports";
        Padding = new Padding(16);

        // ---- Chooser ----
        pnlChooser.Dock = DockStyle.Fill;
        pnlChooser.BackColor = Color.FromArgb(244, 246, 248);
        pnlChooser.Name = "pnlChooser";

        lblChoose.Text = "Choose a report";
        lblChoose.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblChoose.ForeColor = Color.FromArgb(31, 41, 51);
        lblChoose.AutoSize = true;
        lblChoose.Location = new Point(4, 4);
        lblChoose.Name = "lblChoose";

        filterBar.Location = new Point(4, 36);
        filterBar.Size = new Size(900, 36);
        filterBar.BackColor = Color.FromArgb(244, 246, 248);
        filterBar.Name = "filterBar";
        filterBar.Visible = true; // designer; runtime may hide until date reports selected

        lblFrom.Text = "From";
        lblFrom.AutoSize = true;
        lblFrom.ForeColor = Color.FromArgb(107, 119, 133);
        lblFrom.Location = new Point(0, 8);
        dtpFrom.Location = new Point(48, 4);
        dtpFrom.Width = 140;
        dtpFrom.Format = DateTimePickerFormat.Short;

        lblTo.Text = "To";
        lblTo.AutoSize = true;
        lblTo.ForeColor = Color.FromArgb(107, 119, 133);
        lblTo.Location = new Point(200, 8);
        dtpTo.Location = new Point(232, 4);
        dtpTo.Width = 140;
        dtpTo.Format = DateTimePickerFormat.Short;

        filterBar.Controls.Add(lblFrom);
        filterBar.Controls.Add(dtpFrom);
        filterBar.Controls.Add(lblTo);
        filterBar.Controls.Add(dtpTo);

        cardsPanel.Location = new Point(0, 80);
        cardsPanel.Dock = DockStyle.Fill;
        cardsPanel.Padding = new Padding(0, 48, 0, 0);
        cardsPanel.AutoScroll = true;
        cardsPanel.WrapContents = true;
        cardsPanel.BackColor = Color.FromArgb(244, 246, 248);
        cardsPanel.Name = "cardsPanel";

        // Sample cards for View Designer
        cardInventory.Size = new Size(280, 88);
        cardInventory.Margin = new Padding(0, 0, 12, 12);
        cardInventory.BackColor = Color.White;
        cardInventory.BorderStyle = BorderStyle.FixedSingle;
        cardInventory.Name = "cardInventory";
        var lblInv = new Label
        {
            Text = "Current Inventory\nFull stock list with pricing and status",
            Dock = DockStyle.Fill,
            Padding = new Padding(14),
            ForeColor = Color.FromArgb(31, 41, 51)
        };
        cardInventory.Controls.Add(lblInv);

        cardLowStock.Size = new Size(280, 88);
        cardLowStock.Margin = new Padding(0, 0, 12, 12);
        cardLowStock.BackColor = Color.White;
        cardLowStock.BorderStyle = BorderStyle.FixedSingle;
        cardLowStock.Name = "cardLowStock";
        var lblLow = new Label
        {
            Text = "Low Stock\nProducts at or below their minimum level",
            Dock = DockStyle.Fill,
            Padding = new Padding(14),
            ForeColor = Color.FromArgb(31, 41, 51)
        };
        cardLowStock.Controls.Add(lblLow);

        cardOutOfStock.Size = new Size(280, 88);
        cardOutOfStock.Margin = new Padding(0, 0, 12, 12);
        cardOutOfStock.BackColor = Color.White;
        cardOutOfStock.BorderStyle = BorderStyle.FixedSingle;
        cardOutOfStock.Name = "cardOutOfStock";
        var lblOut = new Label
        {
            Text = "Out-of-Stock\nProducts currently at zero quantity",
            Dock = DockStyle.Fill,
            Padding = new Padding(14),
            ForeColor = Color.FromArgb(31, 41, 51)
        };
        cardOutOfStock.Controls.Add(lblOut);

        cardsPanel.Controls.Add(cardInventory);
        cardsPanel.Controls.Add(cardLowStock);
        cardsPanel.Controls.Add(cardOutOfStock);

        lblError.Dock = DockStyle.Bottom;
        lblError.Height = 28;
        lblError.ForeColor = Color.FromArgb(192, 57, 43);
        lblError.Visible = false;
        lblError.Name = "lblError";

        pnlChooser.Controls.Add(cardsPanel);
        pnlChooser.Controls.Add(filterBar);
        pnlChooser.Controls.Add(lblChoose);
        pnlChooser.Controls.Add(lblError);

        // ---- Results (hidden at runtime until a report runs; visible structure for designer) ----
        pnlResults.Dock = DockStyle.Fill;
        pnlResults.Visible = false;
        pnlResults.BackColor = Color.White;
        pnlResults.Name = "pnlResults";

        resultsHeader.Dock = DockStyle.Top;
        resultsHeader.Height = 64;
        resultsHeader.BackColor = Color.White;
        resultsHeader.Name = "resultsHeader";
        resultsHeader.Resize += ResultsHeader_Resize;

        lblReportTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblReportTitle.ForeColor = Color.FromArgb(31, 41, 51);
        lblReportTitle.Text = "Report title";
        lblReportTitle.AutoSize = true;
        lblReportTitle.Location = new Point(8, 8);
        lblReportTitle.Name = "lblReportTitle";

        lblFilters.AutoSize = true;
        lblFilters.ForeColor = Color.FromArgb(107, 119, 133);
        lblFilters.Text = "Filters: —";
        lblFilters.Location = new Point(8, 36);
        lblFilters.Name = "lblFilters";

        btnPrint.BackColor = Color.FromArgb(30, 75, 143);
        btnPrint.FlatAppearance.BorderSize = 0;
        btnPrint.FlatStyle = FlatStyle.Flat;
        btnPrint.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnPrint.ForeColor = Color.White;
        btnPrint.Text = "Print";
        btnPrint.Size = new Size(100, 32);
        btnPrint.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnPrint.Location = new Point(820, 16);
        btnPrint.Name = "btnPrint";
        btnPrint.UseVisualStyleBackColor = false;
        btnPrint.Click += BtnPrint_Click;

        btnBack.BackColor = Color.White;
        btnBack.FlatAppearance.BorderColor = Color.FromArgb(215, 220, 225);
        btnBack.FlatStyle = FlatStyle.Flat;
        btnBack.Text = "Back to Reports";
        btnBack.Size = new Size(130, 32);
        btnBack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnBack.Location = new Point(680, 16);
        btnBack.Name = "btnBack";
        btnBack.UseVisualStyleBackColor = false;
        btnBack.Click += BtnBack_Click;

        resultsHeader.Controls.Add(lblReportTitle);
        resultsHeader.Controls.Add(lblFilters);
        resultsHeader.Controls.Add(btnBack);
        resultsHeader.Controls.Add(btnPrint);

        grid.Dock = DockStyle.Fill;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.ReadOnly = true;
        grid.MultiSelect = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.RowHeadersVisible = false;
        grid.BorderStyle = BorderStyle.FixedSingle;
        grid.BackgroundColor = Color.White;
        grid.ColumnHeadersHeight = 34;
        grid.Name = "grid";
        grid.ColumnCount = 4;
        grid.Columns[0].HeaderText = "Product ID";
        grid.Columns[1].HeaderText = "Name";
        grid.Columns[2].HeaderText = "Qty on Hand";
        grid.Columns[3].HeaderText = "Status";

        lblSummary.Dock = DockStyle.Bottom;
        lblSummary.Height = 28;
        lblSummary.Text = "0 rows";
        lblSummary.ForeColor = Color.FromArgb(107, 119, 133);
        lblSummary.Name = "lblSummary";

        pnlResults.Controls.Add(grid);
        pnlResults.Controls.Add(lblSummary);
        pnlResults.Controls.Add(resultsHeader);

        Controls.Add(pnlResults);
        Controls.Add(pnlChooser);

        filterBar.ResumeLayout(false);
        filterBar.PerformLayout();
        cardsPanel.ResumeLayout(false);
        pnlChooser.ResumeLayout(false);
        pnlChooser.PerformLayout();
        resultsHeader.ResumeLayout(false);
        resultsHeader.PerformLayout();
        pnlResults.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(false);
    }
}
