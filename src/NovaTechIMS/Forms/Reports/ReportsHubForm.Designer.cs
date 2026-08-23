using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Reports;

partial class ReportsHubForm
{
    private System.ComponentModel.IContainer components = null;
    private Panel card;
    private Label lblSelect;
    private ComboBox cboReport;
    private Label lblFrom;
    private DateTimePicker dtpFrom;
    private Label lblTo;
    private DateTimePicker dtpTo;
    private Label lblType;
    private ComboBox cboType;
    private Label lblProduct;
    private ComboBox cboProduct;
    private Button btnRun;
    private Label lblError;
    private Label lblHint;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        card = new Panel();
        lblSelect = new Label();
        cboReport = new ComboBox();
        lblFrom = new Label();
        dtpFrom = new DateTimePicker();
        lblTo = new Label();
        dtpTo = new DateTimePicker();
        lblType = new Label();
        cboType = new ComboBox();
        lblProduct = new Label();
        cboProduct = new ComboBox();
        btnRun = new Button();
        lblError = new Label();
        lblHint = new Label();
        card.SuspendLayout();
        SuspendLayout();

        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.FromArgb(244, 246, 248);
        FormBorderStyle = FormBorderStyle.None;
        Dock = DockStyle.Fill;
        TopLevel = false;
        Name = "ReportsHubForm";
        Text = "Reports";

        card.Location = new Point(24, 24);
        card.Size = new Size(560, 320);
        card.BackColor = Color.White;
        card.Padding = new Padding(20);
        card.Name = "card";

        lblSelect.AutoSize = true;
        lblSelect.Location = new Point(20, 16);
        lblSelect.Name = "lblSelect";
        lblSelect.Text = "Select report";

        cboReport.DropDownStyle = ComboBoxStyle.DropDownList;
        cboReport.Location = new Point(20, 36);
        cboReport.Name = "cboReport";
        cboReport.Size = new Size(400, 25);
        cboReport.TabIndex = 0;
        cboReport.Items.AddRange(new object[]
        {
            "Current Inventory",
            "Low Stock",
            "Out of Stock",
            "Stock-In (date range)",
            "Stock-Out (date range)",
            "Inventory Transaction History"
        });
        cboReport.SelectedIndex = 0;
        cboReport.SelectedIndexChanged += CboReport_SelectedIndexChanged;

        lblFrom.AutoSize = true;
        lblFrom.Location = new Point(20, 76);
        lblFrom.Name = "lblFrom";
        lblFrom.Text = "From";

        dtpFrom.Format = DateTimePickerFormat.Short;
        dtpFrom.Location = new Point(70, 74);
        dtpFrom.Name = "dtpFrom";
        dtpFrom.Size = new Size(130, 25);
        dtpFrom.TabIndex = 1;

        lblTo.AutoSize = true;
        lblTo.Location = new Point(220, 76);
        lblTo.Name = "lblTo";
        lblTo.Text = "To";

        dtpTo.Format = DateTimePickerFormat.Short;
        dtpTo.Location = new Point(250, 74);
        dtpTo.Name = "dtpTo";
        dtpTo.Size = new Size(130, 25);
        dtpTo.TabIndex = 2;

        lblType.AutoSize = true;
        lblType.Location = new Point(20, 112);
        lblType.Name = "lblType";
        lblType.Text = "Type";

        cboType.DropDownStyle = ComboBoxStyle.DropDownList;
        cboType.Location = new Point(70, 110);
        cboType.Name = "cboType";
        cboType.Size = new Size(150, 25);
        cboType.TabIndex = 3;
        cboType.Items.AddRange(new object[] { "All", "Stock-In", "Stock-Out", "Adjustment" });
        cboType.SelectedIndex = 0;

        lblProduct.AutoSize = true;
        lblProduct.Location = new Point(20, 148);
        lblProduct.Name = "lblProduct";
        lblProduct.Text = "Product";

        cboProduct.DropDownStyle = ComboBoxStyle.DropDownList;
        cboProduct.Location = new Point(80, 146);
        cboProduct.Name = "cboProduct";
        cboProduct.Size = new Size(280, 25);
        cboProduct.TabIndex = 4;

        btnRun.Location = new Point(20, 192);
        btnRun.Name = "btnRun";
        btnRun.Size = new Size(130, 34);
        btnRun.TabIndex = 5;
        btnRun.Text = "Run report";
        btnRun.Click += BtnRun_Click;

        lblError.Location = new Point(170, 192);
        lblError.Name = "lblError";
        lblError.Size = new Size(320, 40);
        lblError.Visible = false;

        card.Controls.Add(lblSelect);
        card.Controls.Add(cboReport);
        card.Controls.Add(lblFrom);
        card.Controls.Add(dtpFrom);
        card.Controls.Add(lblTo);
        card.Controls.Add(dtpTo);
        card.Controls.Add(lblType);
        card.Controls.Add(cboType);
        card.Controls.Add(lblProduct);
        card.Controls.Add(cboProduct);
        card.Controls.Add(btnRun);
        card.Controls.Add(lblError);

        lblHint.Dock = DockStyle.Bottom;
        lblHint.Height = 40;
        lblHint.Text = "  Reports open in a separate window. Use Print… on the viewer if needed. Export is out of MVP scope.";
        lblHint.TextAlign = ContentAlignment.MiddleLeft;
        lblHint.Name = "lblHint";

        Controls.Add(card);
        Controls.Add(lblHint);

        card.ResumeLayout(false);
        card.PerformLayout();
        ResumeLayout(false);
    }
}
