using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Inventory;

partial class StockOutForm
{
    private System.ComponentModel.IContainer components = null;

    private Panel formPanel;
    private Label lblProduct;
    private ComboBox cboProduct;
    private Label lblCustomer;
    private ComboBox cboCustomer;
    private Label lblQuantity;
    private NumericUpDown nudQuantity;
    private Label lblDate;
    private DateTimePicker dtpDate;
    private Label lblUnitPrice;
    private TextBox txtUnitPrice;
    private Label lblCurrentQtyCaption;
    private Label lblCurrentQty;
    private Label lblNotes;
    private TextBox txtNotes;
    private Label lblError;
    private Button btnSave;
    private Label lblHint;
    private DataGridView grid;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        formPanel = new Panel();
        lblProduct = new Label();
        cboProduct = new ComboBox();
        lblCustomer = new Label();
        cboCustomer = new ComboBox();
        lblQuantity = new Label();
        nudQuantity = new NumericUpDown();
        lblDate = new Label();
        dtpDate = new DateTimePicker();
        lblUnitPrice = new Label();
        txtUnitPrice = new TextBox();
        lblCurrentQtyCaption = new Label();
        lblCurrentQty = new Label();
        lblNotes = new Label();
        txtNotes = new TextBox();
        lblError = new Label();
        btnSave = new Button();
        lblHint = new Label();
        grid = new DataGridView();
        formPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudQuantity).BeginInit();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        SuspendLayout();

        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.FromArgb(244, 246, 248);
        FormBorderStyle = FormBorderStyle.None;
        Dock = DockStyle.Fill;
        TopLevel = false;
        Name = "StockOutForm";
        Text = "Stock-Out";

        formPanel.Dock = DockStyle.Top;
        formPanel.Height = 280;
        formPanel.BackColor = Color.White;
        formPanel.Padding = new Padding(16);
        formPanel.Name = "formPanel";

        lblProduct.AutoSize = true;
        lblProduct.Location = new Point(16, 12);
        lblProduct.Name = "lblProduct";
        lblProduct.Text = "Product *";

        cboProduct.DropDownStyle = ComboBoxStyle.DropDownList;
        cboProduct.FlatStyle = FlatStyle.Flat;
        cboProduct.Location = new Point(16, 30);
        cboProduct.Name = "cboProduct";
        cboProduct.Size = new Size(280, 25);
        cboProduct.TabIndex = 0;
        cboProduct.SelectedIndexChanged += CboProduct_SelectedIndexChanged;

        lblCustomer.AutoSize = true;
        lblCustomer.Location = new Point(16, 62);
        lblCustomer.Name = "lblCustomer";
        lblCustomer.Text = "Customer (optional)";

        cboCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCustomer.FlatStyle = FlatStyle.Flat;
        cboCustomer.Location = new Point(16, 80);
        cboCustomer.Name = "cboCustomer";
        cboCustomer.Size = new Size(280, 25);
        cboCustomer.TabIndex = 1;

        lblQuantity.AutoSize = true;
        lblQuantity.Location = new Point(16, 112);
        lblQuantity.Name = "lblQuantity";
        lblQuantity.Text = "Quantity *";

        nudQuantity.Location = new Point(16, 130);
        nudQuantity.Minimum = 1;
        nudQuantity.Maximum = 1000000;
        nudQuantity.Value = 1;
        nudQuantity.Name = "nudQuantity";
        nudQuantity.Size = new Size(120, 25);
        nudQuantity.TabIndex = 2;

        lblDate.AutoSize = true;
        lblDate.Location = new Point(336, 12);
        lblDate.Name = "lblDate";
        lblDate.Text = "Transaction date *";

        dtpDate.Format = DateTimePickerFormat.Short;
        dtpDate.Location = new Point(336, 30);
        dtpDate.Name = "dtpDate";
        dtpDate.Size = new Size(200, 25);
        dtpDate.TabIndex = 3;

        lblUnitPrice.AutoSize = true;
        lblUnitPrice.Location = new Point(336, 62);
        lblUnitPrice.Name = "lblUnitPrice";
        lblUnitPrice.Text = "Selling price *";

        txtUnitPrice.BorderStyle = BorderStyle.FixedSingle;
        txtUnitPrice.Location = new Point(336, 80);
        txtUnitPrice.Name = "txtUnitPrice";
        txtUnitPrice.Size = new Size(120, 25);
        txtUnitPrice.TabIndex = 4;
        txtUnitPrice.Text = "0.00";

        lblCurrentQtyCaption.AutoSize = true;
        lblCurrentQtyCaption.Location = new Point(336, 112);
        lblCurrentQtyCaption.Name = "lblCurrentQtyCaption";
        lblCurrentQtyCaption.Text = "Current quantity on hand";

        lblCurrentQty.AutoSize = true;
        lblCurrentQty.Location = new Point(336, 130);
        lblCurrentQty.Name = "lblCurrentQty";
        lblCurrentQty.Text = "—";

        lblNotes.AutoSize = true;
        lblNotes.Location = new Point(16, 162);
        lblNotes.Name = "lblNotes";
        lblNotes.Text = "Notes";

        txtNotes.BorderStyle = BorderStyle.FixedSingle;
        txtNotes.Location = new Point(16, 180);
        txtNotes.Multiline = true;
        txtNotes.MaxLength = 500;
        txtNotes.ScrollBars = ScrollBars.Vertical;
        txtNotes.Name = "txtNotes";
        txtNotes.Size = new Size(520, 48);
        txtNotes.TabIndex = 5;

        lblError.Location = new Point(16, 236);
        lblError.Name = "lblError";
        lblError.Size = new Size(520, 28);
        lblError.Visible = false;

        btnSave.Location = new Point(376, 236);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(160, 34);
        btnSave.TabIndex = 6;
        btnSave.Text = "Record Stock-Out";
        btnSave.UseVisualStyleBackColor = true;
        btnSave.Click += BtnSave_Click;

        formPanel.Controls.Add(lblProduct);
        formPanel.Controls.Add(cboProduct);
        formPanel.Controls.Add(lblCustomer);
        formPanel.Controls.Add(cboCustomer);
        formPanel.Controls.Add(lblQuantity);
        formPanel.Controls.Add(nudQuantity);
        formPanel.Controls.Add(lblDate);
        formPanel.Controls.Add(dtpDate);
        formPanel.Controls.Add(lblUnitPrice);
        formPanel.Controls.Add(txtUnitPrice);
        formPanel.Controls.Add(lblCurrentQtyCaption);
        formPanel.Controls.Add(lblCurrentQty);
        formPanel.Controls.Add(lblNotes);
        formPanel.Controls.Add(txtNotes);
        formPanel.Controls.Add(lblError);
        formPanel.Controls.Add(btnSave);

        lblHint.Dock = DockStyle.Top;
        lblHint.Height = 28;
        lblHint.Text = "  Recent Stock-Out transactions";
        lblHint.TextAlign = ContentAlignment.MiddleLeft;
        lblHint.Name = "lblHint";

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
        grid.Name = "grid";
        grid.TabIndex = 7;

        Controls.Add(grid);
        Controls.Add(lblHint);
        Controls.Add(formPanel);

        formPanel.ResumeLayout(false);
        formPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudQuantity).EndInit();
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(false);
    }
}
