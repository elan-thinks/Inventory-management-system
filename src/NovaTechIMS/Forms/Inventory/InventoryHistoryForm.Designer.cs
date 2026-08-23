using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Inventory;

partial class InventoryHistoryForm
{
    private System.ComponentModel.IContainer components = null;

    private Panel toolbar;
    private Label lblFrom;
    private DateTimePicker dtpFrom;
    private Label lblTo;
    private DateTimePicker dtpTo;
    private Label lblType;
    private ComboBox cboType;
    private Label lblProd;
    private ComboBox cboProduct;
    private Button btnApply;
    private Button btnClear;
    private DataGridView grid;
    private Label lblCount;
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
        toolbar = new Panel();
        lblFrom = new Label();
        dtpFrom = new DateTimePicker();
        lblTo = new Label();
        dtpTo = new DateTimePicker();
        lblType = new Label();
        cboType = new ComboBox();
        lblProd = new Label();
        cboProduct = new ComboBox();
        btnApply = new Button();
        btnClear = new Button();
        grid = new DataGridView();
        lblCount = new Label();
        lblEmpty = new Label();
        toolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        SuspendLayout();

        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.FromArgb(244, 246, 248);
        FormBorderStyle = FormBorderStyle.None;
        Dock = DockStyle.Fill;
        TopLevel = false;
        Name = "InventoryHistoryForm";
        Text = "Inventory History";

        toolbar.Dock = DockStyle.Top;
        toolbar.Height = 56;
        toolbar.BackColor = Color.FromArgb(244, 246, 248);
        toolbar.Padding = new Padding(0, 8, 0, 8);
        toolbar.Name = "toolbar";

        lblFrom.AutoSize = true;
        lblFrom.Location = new Point(0, 14);
        lblFrom.Name = "lblFrom";
        lblFrom.Text = "From";

        dtpFrom.Format = DateTimePickerFormat.Short;
        dtpFrom.Location = new Point(40, 10);
        dtpFrom.Name = "dtpFrom";
        dtpFrom.Size = new Size(120, 25);
        dtpFrom.TabIndex = 0;

        lblTo.AutoSize = true;
        lblTo.Location = new Point(170, 14);
        lblTo.Name = "lblTo";
        lblTo.Text = "To";

        dtpTo.Format = DateTimePickerFormat.Short;
        dtpTo.Location = new Point(195, 10);
        dtpTo.Name = "dtpTo";
        dtpTo.Size = new Size(120, 25);
        dtpTo.TabIndex = 1;

        lblType.AutoSize = true;
        lblType.Location = new Point(330, 14);
        lblType.Name = "lblType";
        lblType.Text = "Type";

        cboType.DropDownStyle = ComboBoxStyle.DropDownList;
        cboType.Location = new Point(365, 10);
        cboType.Name = "cboType";
        cboType.Size = new Size(120, 25);
        cboType.TabIndex = 2;
        cboType.Items.AddRange(new object[] { "All", "Stock-In", "Stock-Out", "Adjustment" });
        cboType.SelectedIndex = 0;

        lblProd.AutoSize = true;
        lblProd.Location = new Point(500, 14);
        lblProd.Name = "lblProd";
        lblProd.Text = "Product";

        cboProduct.DropDownStyle = ComboBoxStyle.DropDownList;
        cboProduct.Location = new Point(555, 10);
        cboProduct.Name = "cboProduct";
        cboProduct.Size = new Size(200, 25);
        cboProduct.TabIndex = 3;

        btnApply.Location = new Point(770, 8);
        btnApply.Name = "btnApply";
        btnApply.Size = new Size(80, 30);
        btnApply.TabIndex = 4;
        btnApply.Text = "Apply";
        btnApply.UseVisualStyleBackColor = true;
        btnApply.Click += BtnApply_Click;

        btnClear.Location = new Point(858, 8);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(70, 30);
        btnClear.TabIndex = 5;
        btnClear.Text = "Clear";
        btnClear.UseVisualStyleBackColor = true;
        btnClear.Click += BtnClear_Click;

        toolbar.Controls.Add(lblFrom);
        toolbar.Controls.Add(dtpFrom);
        toolbar.Controls.Add(lblTo);
        toolbar.Controls.Add(dtpTo);
        toolbar.Controls.Add(lblType);
        toolbar.Controls.Add(cboType);
        toolbar.Controls.Add(lblProd);
        toolbar.Controls.Add(cboProduct);
        toolbar.Controls.Add(btnApply);
        toolbar.Controls.Add(btnClear);

        grid.Dock = DockStyle.Fill;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.ReadOnly = true;
        grid.MultiSelect = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.Name = "grid";
        grid.TabIndex = 6;

        lblEmpty.Dock = DockStyle.Fill;
        lblEmpty.Text = "No transactions match the current filters.";
        lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
        lblEmpty.Visible = false;
        lblEmpty.Name = "lblEmpty";

        lblCount.Dock = DockStyle.Bottom;
        lblCount.Height = 28;
        lblCount.TextAlign = ContentAlignment.MiddleLeft;
        lblCount.Name = "lblCount";

        Controls.Add(grid);
        Controls.Add(lblEmpty);
        Controls.Add(lblCount);
        Controls.Add(toolbar);

        toolbar.ResumeLayout(false);
        toolbar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(false);
    }
}
