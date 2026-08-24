using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>Inventory History layout — full controls on design surface (read-only).</summary>
partial class InventoryHistoryForm
{
    private System.ComponentModel.IContainer components = null;

    private Panel banner;
    private Label lblBanner;
    private Panel toolbar;
    private Label lblFrom;
    private DateTimePicker dtpFrom;
    private Label lblTo;
    private DateTimePicker dtpTo;
    private Label lblType;
    private ComboBox cboType;
    private Label lblProd;
    private ComboBox cboProduct;
    private Button btnClear;
    private Button btnPrint;
    private Label lblCount;
    private DataGridView grid;
    private Label lblEmpty;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        banner = new Panel();
        lblBanner = new Label();
        toolbar = new Panel();
        lblFrom = new Label();
        dtpFrom = new DateTimePicker();
        lblTo = new Label();
        dtpTo = new DateTimePicker();
        lblType = new Label();
        cboType = new ComboBox();
        lblProd = new Label();
        cboProduct = new ComboBox();
        btnClear = new Button();
        btnPrint = new Button();
        lblCount = new Label();
        grid = new DataGridView();
        lblEmpty = new Label();

        banner.SuspendLayout();
        toolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        SuspendLayout();

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.FromArgb(244, 246, 248);
        ClientSize = new Size(1000, 560);
        FormBorderStyle = FormBorderStyle.None;
        Name = "InventoryHistoryForm";
        Text = "Inventory History";
        Padding = new Padding(12, 8, 12, 8);

        banner.Dock = DockStyle.Top;
        banner.Height = 44;
        banner.BackColor = Color.FromArgb(231, 240, 247);
        banner.Padding = new Padding(12, 8, 12, 8);
        banner.Name = "banner";

        lblBanner.Dock = DockStyle.Fill;
        lblBanner.Text = "  Read-only record.  Transactions are permanent and cannot be edited or deleted, by any role, once recorded.";
        lblBanner.TextAlign = ContentAlignment.MiddleLeft;
        lblBanner.ForeColor = Color.FromArgb(46, 110, 158);
        lblBanner.Name = "lblBanner";
        banner.Controls.Add(lblBanner);

        toolbar.Dock = DockStyle.Top;
        toolbar.Height = 56;
        toolbar.BackColor = Color.FromArgb(244, 246, 248);
        toolbar.Padding = new Padding(0, 10, 0, 6);
        toolbar.Name = "toolbar";
        toolbar.Resize += Toolbar_Resize;

        lblFrom.Text = "From";
        lblFrom.AutoSize = true;
        lblFrom.ForeColor = Color.FromArgb(107, 119, 133);
        lblFrom.Location = new Point(0, 16);
        lblFrom.Name = "lblFrom";

        dtpFrom.Format = DateTimePickerFormat.Short;
        dtpFrom.Location = new Point(40, 12);
        dtpFrom.Size = new Size(120, 25);
        dtpFrom.Name = "dtpFrom";
        dtpFrom.ValueChanged += Filter_Changed;

        lblTo.Text = "To";
        lblTo.AutoSize = true;
        lblTo.ForeColor = Color.FromArgb(107, 119, 133);
        lblTo.Location = new Point(172, 16);
        lblTo.Name = "lblTo";

        dtpTo.Format = DateTimePickerFormat.Short;
        dtpTo.Location = new Point(196, 12);
        dtpTo.Size = new Size(120, 25);
        dtpTo.Name = "dtpTo";
        dtpTo.ValueChanged += Filter_Changed;

        lblType.Text = "Type";
        lblType.AutoSize = true;
        lblType.ForeColor = Color.FromArgb(107, 119, 133);
        lblType.Location = new Point(330, 16);
        lblType.Name = "lblType";

        cboType.DropDownStyle = ComboBoxStyle.DropDownList;
        cboType.Location = new Point(365, 12);
        cboType.Size = new Size(130, 25);
        cboType.Name = "cboType";
        cboType.Items.AddRange(new object[] { "Type: All", "Stock-In", "Stock-Out", "Adjustment" });
        cboType.SelectedIndexChanged += Filter_Changed;

        lblProd.Text = "Product";
        lblProd.AutoSize = true;
        lblProd.ForeColor = Color.FromArgb(107, 119, 133);
        lblProd.Location = new Point(510, 16);
        lblProd.Name = "lblProd";

        cboProduct.DropDownStyle = ComboBoxStyle.DropDownList;
        cboProduct.Items.AddRange(new object[] { "Product: All" });
        cboProduct.Location = new Point(565, 12);
        cboProduct.Size = new Size(200, 25);
        cboProduct.Name = "cboProduct";
        cboProduct.SelectedIndexChanged += Filter_Changed;

        btnClear.FlatStyle = FlatStyle.Flat;
        btnClear.ForeColor = Color.FromArgb(30, 75, 143);
        btnClear.Text = "Clear Filters";
        btnClear.Location = new Point(780, 10);
        btnClear.Size = new Size(110, 30);
        btnClear.Name = "btnClear";
        btnClear.Click += BtnClear_Click;

        btnPrint.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnPrint.BackColor = Color.White;
        btnPrint.FlatAppearance.BorderColor = Color.FromArgb(215, 220, 225);
        btnPrint.FlatStyle = FlatStyle.Flat;
        btnPrint.Text = "Print";
        btnPrint.Size = new Size(90, 30);
        btnPrint.Location = new Point(900, 10);
        btnPrint.Name = "btnPrint";
        btnPrint.UseVisualStyleBackColor = false;
        btnPrint.Click += BtnPrint_Click;

        toolbar.Controls.Add(lblFrom);
        toolbar.Controls.Add(dtpFrom);
        toolbar.Controls.Add(lblTo);
        toolbar.Controls.Add(dtpTo);
        toolbar.Controls.Add(lblType);
        toolbar.Controls.Add(cboType);
        toolbar.Controls.Add(lblProd);
        toolbar.Controls.Add(cboProduct);
        toolbar.Controls.Add(btnClear);
        toolbar.Controls.Add(btnPrint);

        lblCount.Dock = DockStyle.Top;
        lblCount.Height = 24;
        lblCount.Text = "0 transactions";
        lblCount.TextAlign = ContentAlignment.MiddleLeft;
        lblCount.ForeColor = Color.FromArgb(107, 119, 133);
        lblCount.Name = "lblCount";

        grid.Dock = DockStyle.Fill;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AllowUserToResizeRows = false;
        grid.ReadOnly = true;
        grid.MultiSelect = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.RowHeadersVisible = false;
        grid.BorderStyle = BorderStyle.FixedSingle;
        grid.BackgroundColor = Color.White;
        grid.ColumnHeadersHeight = 34;
        grid.Name = "grid";
        grid.ColumnCount = 5;
        grid.Columns[0].HeaderText = "Transaction ID";
        grid.Columns[1].HeaderText = "Date";
        grid.Columns[2].HeaderText = "Type";
        grid.Columns[3].HeaderText = "Product";
        grid.Columns[4].HeaderText = "Quantity";

        lblEmpty.Dock = DockStyle.Fill;
        lblEmpty.Text = "No transactions match the current filters.";
        lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
        lblEmpty.ForeColor = Color.FromArgb(107, 119, 133);
        lblEmpty.Visible = false;
        lblEmpty.Name = "lblEmpty";

        Controls.Add(grid);
        Controls.Add(lblEmpty);
        Controls.Add(lblCount);
        Controls.Add(toolbar);
        Controls.Add(banner);

        banner.ResumeLayout(false);
        toolbar.ResumeLayout(false);
        toolbar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(false);
    }
}
