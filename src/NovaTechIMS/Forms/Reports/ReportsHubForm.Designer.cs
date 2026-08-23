using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Reports;

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

    // Date filters (shown when needed on chooser)
    private Panel filterBar;
    private Label lblFrom;
    private DateTimePicker dtpFrom;
    private Label lblTo;
    private DateTimePicker dtpTo;

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

        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        FormBorderStyle = FormBorderStyle.None;
        Dock = DockStyle.Fill;
        TopLevel = false;
        Name = "ReportsHubForm";
        Text = "Reports";
        Padding = new Padding(16);

        // ---- Chooser ----
        pnlChooser.Dock = DockStyle.Fill;
        pnlChooser.Name = "pnlChooser";

        lblChoose.Text = "Choose a report";
        lblChoose.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblChoose.AutoSize = true;
        lblChoose.Location = new Point(4, 4);
        lblChoose.Name = "lblChoose";

        filterBar.Location = new Point(4, 36);
        filterBar.Size = new Size(900, 36);
        filterBar.Name = "filterBar";
        filterBar.Visible = false;

        lblFrom.Text = "From";
        lblFrom.AutoSize = true;
        lblFrom.Location = new Point(0, 8);
        dtpFrom.Location = new Point(48, 4);
        dtpFrom.Width = 140;
        dtpFrom.Format = DateTimePickerFormat.Short;

        lblTo.Text = "To";
        lblTo.AutoSize = true;
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
        cardsPanel.Name = "cardsPanel";

        lblError.Dock = DockStyle.Bottom;
        lblError.Height = 28;
        lblError.Visible = false;
        lblError.Name = "lblError";

        pnlChooser.Controls.Add(cardsPanel);
        pnlChooser.Controls.Add(filterBar);
        pnlChooser.Controls.Add(lblChoose);
        pnlChooser.Controls.Add(lblError);

        // ---- Results ----
        pnlResults.Dock = DockStyle.Fill;
        pnlResults.Visible = false;
        pnlResults.Name = "pnlResults";

        resultsHeader.Dock = DockStyle.Top;
        resultsHeader.Height = 64;
        resultsHeader.Name = "resultsHeader";

        lblReportTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblReportTitle.AutoSize = true;
        lblReportTitle.Location = new Point(8, 8);
        lblReportTitle.Name = "lblReportTitle";

        lblFilters.AutoSize = true;
        lblFilters.Location = new Point(8, 36);
        lblFilters.Name = "lblFilters";

        btnPrint.Text = "  Print";
        btnPrint.Size = new Size(100, 32);
        btnPrint.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnPrint.Location = new Point(700, 16);
        btnPrint.Name = "btnPrint";
        btnPrint.Click += BtnPrint_Click;

        btnBack.Text = "Back to Reports";
        btnBack.Size = new Size(130, 32);
        btnBack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnBack.Location = new Point(560, 16);
        btnBack.Name = "btnBack";
        btnBack.Click += BtnBack_Click;

        resultsHeader.Controls.Add(lblReportTitle);
        resultsHeader.Controls.Add(lblFilters);
        resultsHeader.Controls.Add(btnBack);
        resultsHeader.Controls.Add(btnPrint);
        resultsHeader.Resize += (_, _) =>
        {
            btnPrint.Left = resultsHeader.ClientSize.Width - btnPrint.Width - 8;
            btnBack.Left = btnPrint.Left - btnBack.Width - 8;
        };

        grid.Dock = DockStyle.Fill;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.ReadOnly = true;
        grid.MultiSelect = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.BorderStyle = BorderStyle.None;
        grid.Name = "grid";

        lblSummary.Dock = DockStyle.Bottom;
        lblSummary.Height = 28;
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
