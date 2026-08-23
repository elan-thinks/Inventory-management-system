using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Inventory;

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

        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        FormBorderStyle = FormBorderStyle.None;
        Dock = DockStyle.Fill;
        TopLevel = false;
        Name = "InventoryHistoryForm";
        Text = "Inventory History";
        Padding = new Padding(12, 8, 12, 8);

        // Read-only banner (mockup)
        banner.Dock = DockStyle.Top;
        banner.Height = 44;
        banner.Padding = new Padding(12, 8, 12, 8);
        banner.Name = "banner";

        lblBanner.Dock = DockStyle.Fill;
        lblBanner.Text = "  Read-only record.  Transactions are permanent and cannot be edited or deleted, by any role, once recorded.";
        lblBanner.TextAlign = ContentAlignment.MiddleLeft;
        lblBanner.Name = "lblBanner";
        banner.Controls.Add(lblBanner);

        // Toolbar filters
        toolbar.Dock = DockStyle.Top;
        toolbar.Height = 52;
        toolbar.Padding = new Padding(0, 10, 0, 6);
        toolbar.Name = "toolbar";

        lblFrom.Text = "From";
        lblFrom.AutoSize = true;
        lblFrom.Location = new Point(0, 14);
        dtpFrom.Format = DateTimePickerFormat.Short;
        dtpFrom.Location = new Point(40, 10);
        dtpFrom.Size = new Size(120, 25);
        dtpFrom.Name = "dtpFrom";

        lblTo.Text = "To";
        lblTo.AutoSize = true;
        lblTo.Location = new Point(172, 14);
        dtpTo.Format = DateTimePickerFormat.Short;
        dtpTo.Location = new Point(196, 10);
        dtpTo.Size = new Size(120, 25);
        dtpTo.Name = "dtpTo";

        lblType.Text = "Type";
        lblType.AutoSize = true;
        lblType.Location = new Point(330, 14);
        cboType.DropDownStyle = ComboBoxStyle.DropDownList;
        cboType.Location = new Point(365, 10);
        cboType.Size = new Size(130, 25);
        cboType.Name = "cboType";
        cboType.Items.AddRange(new object[] { "Type: All", "Stock-In", "Stock-Out", "Adjustment" });
        cboType.SelectedIndex = 0;

        lblProd.Text = "Product";
        lblProd.AutoSize = true;
        lblProd.Location = new Point(510, 14);
        cboProduct.DropDownStyle = ComboBoxStyle.DropDownList;
        cboProduct.Location = new Point(565, 10);
        cboProduct.Size = new Size(200, 25);
        cboProduct.Name = "cboProduct";

        btnClear.Text = "Clear Filters";
        btnClear.Location = new Point(780, 8);
        btnClear.Size = new Size(110, 30);
        btnClear.Name = "btnClear";
        btnClear.Click += BtnClear_Click;

        btnPrint.Text = "  Print";
        btnPrint.Size = new Size(90, 30);
        btnPrint.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnPrint.Location = new Point(900, 8);
        btnPrint.Name = "btnPrint";
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
        toolbar.Resize += (_, _) =>
        {
            btnPrint.Left = toolbar.ClientSize.Width - btnPrint.Width;
        };

        // Auto-apply when filters change
        dtpFrom.ValueChanged += (_, _) => ReloadGrid();
        dtpTo.ValueChanged += (_, _) => ReloadGrid();
        cboType.SelectedIndexChanged += (_, _) => ReloadGrid();
        cboProduct.SelectedIndexChanged += (_, _) => ReloadGrid();

        lblCount.Dock = DockStyle.Top;
        lblCount.Height = 24;
        lblCount.TextAlign = ContentAlignment.MiddleLeft;
        lblCount.Name = "lblCount";

        grid.Dock = DockStyle.Fill;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.ReadOnly = true;
        grid.MultiSelect = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.BorderStyle = BorderStyle.None;
        grid.Name = "grid";

        lblEmpty.Dock = DockStyle.Fill;
        lblEmpty.Text = "No transactions match the current filters.";
        lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
        lblEmpty.Visible = false;
        lblEmpty.Name = "lblEmpty";

        // Dock order: Fill first, then tops bottom-up
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
