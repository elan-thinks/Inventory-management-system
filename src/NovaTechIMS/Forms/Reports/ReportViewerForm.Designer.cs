using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Reports;

partial class ReportViewerForm
{
    private System.ComponentModel.IContainer components = null;
    private Panel toolbar;
    private Button btnPrint;
    private Button btnClose;
    private Label lblTitle;
    private DataGridView grid;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        toolbar = new Panel();
        btnPrint = new Button();
        btnClose = new Button();
        lblTitle = new Label();
        grid = new DataGridView();
        toolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        SuspendLayout();

        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        StartPosition = FormStartPosition.CenterParent;
        Size = new Size(900, 560);
        MinimumSize = new Size(640, 400);
        BackColor = Color.FromArgb(244, 246, 248);
        Name = "ReportViewerForm";
        Text = "Report";

        toolbar.Dock = DockStyle.Top;
        toolbar.Height = 44;
        toolbar.BackColor = Color.White;
        toolbar.Padding = new Padding(8);
        toolbar.Name = "toolbar";
        toolbar.Resize += Toolbar_Resize;

        btnPrint.Text = "Print…";
        btnPrint.Size = new Size(90, 28);
        btnPrint.Location = new Point(8, 8);
        btnPrint.Name = "btnPrint";
        btnPrint.TabIndex = 0;
        btnPrint.Click += BtnPrint_Click;

        btnClose.Text = "Close";
        btnClose.Size = new Size(80, 28);
        btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnClose.Name = "btnClose";
        btnClose.TabIndex = 1;
        btnClose.Click += BtnClose_Click;

        toolbar.Controls.Add(btnPrint);
        toolbar.Controls.Add(btnClose);

        lblTitle.Dock = DockStyle.Top;
        lblTitle.Height = 28;
        lblTitle.Text = "  Report";
        lblTitle.TextAlign = ContentAlignment.MiddleLeft;
        lblTitle.Name = "lblTitle";

        grid.Dock = DockStyle.Fill;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.ReadOnly = true;
        grid.MultiSelect = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.Name = "grid";
        grid.TabIndex = 2;

        Controls.Add(grid);
        Controls.Add(lblTitle);
        Controls.Add(toolbar);

        toolbar.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(false);
    }
}
