using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>Stock-Out layout — full controls on design surface.</summary>
partial class StockOutForm
{
    private System.ComponentModel.IContainer components = null;

    private Panel scrollHost;
    private Panel formPanel;
    private Label secProduct;
    private Label lblProduct;
    private ComboBox cboProduct;
    private Label helpProduct;
    private Label lblCustomer;
    private ComboBox cboCustomer;
    private Label helpCustomer;
    private Label secMovement;
    private Label lblQuantity;
    private NumericUpDown nudQuantity;
    private Label lblDate;
    private DateTimePicker dtpDate;
    private Label lblUnitPrice;
    private TextBox txtUnitPrice;
    private Panel previewBanner;
    private Label lblPreview;
    private Label secNotes;
    private Label lblNotes;
    private TextBox txtNotes;
    private Label lblError;
    private Button btnSave;
    private Label lblHint;
    private DataGridView grid;
    private Label lblCurrentQty;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        scrollHost = new Panel();
        formPanel = new Panel();
        secProduct = new Label();
        lblProduct = new Label();
        cboProduct = new ComboBox();
        helpProduct = new Label();
        lblCustomer = new Label();
        cboCustomer = new ComboBox();
        helpCustomer = new Label();
        secMovement = new Label();
        lblQuantity = new Label();
        nudQuantity = new NumericUpDown();
        lblDate = new Label();
        dtpDate = new DateTimePicker();
        lblUnitPrice = new Label();
        txtUnitPrice = new TextBox();
        previewBanner = new Panel();
        lblPreview = new Label();
        secNotes = new Label();
        lblNotes = new Label();
        txtNotes = new TextBox();
        lblError = new Label();
        btnSave = new Button();
        lblHint = new Label();
        grid = new DataGridView();
        lblCurrentQty = new Label();

        scrollHost.SuspendLayout();
        formPanel.SuspendLayout();
        previewBanner.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudQuantity).BeginInit();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        SuspendLayout();

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.FromArgb(244, 246, 248);
        ClientSize = new Size(900, 600);
        FormBorderStyle = FormBorderStyle.None;
        Name = "StockOutForm";
        Text = "Stock-Out";
        Padding = new Padding(8);

        scrollHost.Dock = DockStyle.Top;
        scrollHost.Height = 480;
        scrollHost.AutoScroll = true;
        scrollHost.Name = "scrollHost";

        formPanel.Location = new Point(0, 0);
        formPanel.Size = new Size(720, 470);
        formPanel.BackColor = Color.White;
        formPanel.BorderStyle = BorderStyle.FixedSingle;
        formPanel.Padding = new Padding(20);
        formPanel.Name = "formPanel";

        secProduct.Text = "PRODUCT & CUSTOMER";
        secProduct.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        secProduct.ForeColor = Color.FromArgb(59, 122, 143);
        secProduct.Location = new Point(20, 12);
        secProduct.AutoSize = true;
        secProduct.Name = "secProduct";

        lblProduct.Text = "Product *";
        lblProduct.Location = new Point(20, 36);
        lblProduct.AutoSize = true;
        lblProduct.Name = "lblProduct";

        cboProduct.DropDownStyle = ComboBoxStyle.DropDownList;
        cboProduct.FlatStyle = FlatStyle.Flat;
        cboProduct.Items.AddRange(new object[] { "— Select product —" });
        cboProduct.Location = new Point(20, 56);
        cboProduct.Size = new Size(400, 28);
        cboProduct.Name = "cboProduct";
        cboProduct.TabIndex = 0;
        cboProduct.SelectedIndexChanged += CboProduct_SelectedIndexChanged;

        helpProduct.Text = "Only active products are listed.";
        helpProduct.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
        helpProduct.ForeColor = Color.FromArgb(107, 119, 133);
        helpProduct.Location = new Point(20, 86);
        helpProduct.AutoSize = true;
        helpProduct.Name = "helpProduct";

        lblCustomer.Text = "Customer (optional)";
        lblCustomer.Location = new Point(20, 112);
        lblCustomer.AutoSize = true;
        lblCustomer.Name = "lblCustomer";

        cboCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCustomer.FlatStyle = FlatStyle.Flat;
        cboCustomer.Items.AddRange(new object[] { "— None (optional) —" });
        cboCustomer.Location = new Point(20, 132);
        cboCustomer.Size = new Size(400, 28);
        cboCustomer.Name = "cboCustomer";
        cboCustomer.TabIndex = 1;

        helpCustomer.Text = "Optional — leave empty if no customer is associated.";
        helpCustomer.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
        helpCustomer.ForeColor = Color.FromArgb(107, 119, 133);
        helpCustomer.Location = new Point(20, 162);
        helpCustomer.AutoSize = true;
        helpCustomer.Name = "helpCustomer";

        secMovement.Text = "MOVEMENT DETAILS";
        secMovement.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        secMovement.ForeColor = Color.FromArgb(59, 122, 143);
        secMovement.Location = new Point(20, 196);
        secMovement.AutoSize = true;
        secMovement.Name = "secMovement";

        lblQuantity.Text = "Quantity *";
        lblQuantity.Location = new Point(20, 220);
        lblQuantity.AutoSize = true;
        lblQuantity.Name = "lblQuantity";

        nudQuantity.Location = new Point(20, 240);
        nudQuantity.Minimum = 1;
        nudQuantity.Maximum = 1000000;
        nudQuantity.Value = 1;
        nudQuantity.Size = new Size(120, 28);
        nudQuantity.Name = "nudQuantity";
        nudQuantity.TabIndex = 2;
        nudQuantity.ValueChanged += NudQuantity_ValueChanged;

        lblDate.Text = "Date *";
        lblDate.Location = new Point(160, 220);
        lblDate.AutoSize = true;
        lblDate.Name = "lblDate";

        dtpDate.Format = DateTimePickerFormat.Short;
        dtpDate.Location = new Point(160, 240);
        dtpDate.Size = new Size(140, 28);
        dtpDate.Name = "dtpDate";
        dtpDate.TabIndex = 3;

        lblUnitPrice.Text = "Selling price *";
        lblUnitPrice.Location = new Point(320, 220);
        lblUnitPrice.AutoSize = true;
        lblUnitPrice.Name = "lblUnitPrice";

        txtUnitPrice.BorderStyle = BorderStyle.FixedSingle;
        txtUnitPrice.Location = new Point(320, 240);
        txtUnitPrice.Size = new Size(120, 28);
        txtUnitPrice.Name = "txtUnitPrice";
        txtUnitPrice.TabIndex = 4;
        txtUnitPrice.Text = "0.00";

        previewBanner.Location = new Point(20, 278);
        previewBanner.Size = new Size(420, 36);
        previewBanner.BackColor = Color.FromArgb(251, 234, 232);
        previewBanner.Name = "previewBanner";

        lblPreview.Dock = DockStyle.Fill;
        lblPreview.Text = "  Select a product to see the new quantity on hand.";
        lblPreview.TextAlign = ContentAlignment.MiddleLeft;
        lblPreview.Name = "lblPreview";
        previewBanner.Controls.Add(lblPreview);

        lblCurrentQty.Visible = false;
        lblCurrentQty.Name = "lblCurrentQty";
        lblCurrentQty.Text = "—";

        secNotes.Text = "NOTES";
        secNotes.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        secNotes.ForeColor = Color.FromArgb(59, 122, 143);
        secNotes.Location = new Point(20, 326);
        secNotes.AutoSize = true;
        secNotes.Name = "secNotes";

        lblNotes.Text = "Notes";
        lblNotes.Location = new Point(20, 348);
        lblNotes.AutoSize = true;
        lblNotes.Name = "lblNotes";

        txtNotes.BorderStyle = BorderStyle.FixedSingle;
        txtNotes.Location = new Point(20, 368);
        txtNotes.Multiline = true;
        txtNotes.MaxLength = 500;
        txtNotes.ScrollBars = ScrollBars.Vertical;
        txtNotes.Size = new Size(420, 48);
        txtNotes.Name = "txtNotes";
        txtNotes.TabIndex = 5;
        txtNotes.PlaceholderText = "Optional — e.g. invoice ref, destination";

        lblError.Location = new Point(20, 424);
        lblError.Size = new Size(300, 32);
        lblError.ForeColor = Color.FromArgb(192, 57, 43);
        lblError.Visible = false;
        lblError.Name = "lblError";

        btnSave.BackColor = Color.FromArgb(30, 75, 143);
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(340, 424);
        btnSave.Size = new Size(160, 36);
        btnSave.Text = "Record Stock-Out";
        btnSave.Name = "btnSave";
        btnSave.TabIndex = 6;
        btnSave.UseVisualStyleBackColor = false;
        btnSave.Click += BtnSave_Click;

        formPanel.Controls.Add(secProduct);
        formPanel.Controls.Add(lblProduct);
        formPanel.Controls.Add(cboProduct);
        formPanel.Controls.Add(helpProduct);
        formPanel.Controls.Add(lblCustomer);
        formPanel.Controls.Add(cboCustomer);
        formPanel.Controls.Add(helpCustomer);
        formPanel.Controls.Add(secMovement);
        formPanel.Controls.Add(lblQuantity);
        formPanel.Controls.Add(nudQuantity);
        formPanel.Controls.Add(lblDate);
        formPanel.Controls.Add(dtpDate);
        formPanel.Controls.Add(lblUnitPrice);
        formPanel.Controls.Add(txtUnitPrice);
        formPanel.Controls.Add(previewBanner);
        formPanel.Controls.Add(lblCurrentQty);
        formPanel.Controls.Add(secNotes);
        formPanel.Controls.Add(lblNotes);
        formPanel.Controls.Add(txtNotes);
        formPanel.Controls.Add(lblError);
        formPanel.Controls.Add(btnSave);

        scrollHost.Controls.Add(formPanel);

        lblHint.Dock = DockStyle.Top;
        lblHint.Height = 28;
        lblHint.Text = "  Recent Stock-Out transactions";
        lblHint.TextAlign = ContentAlignment.MiddleLeft;
        lblHint.ForeColor = Color.FromArgb(107, 119, 133);
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
        grid.ColumnHeadersHeight = 34;
        grid.Name = "grid";
        grid.TabIndex = 7;
        grid.ColumnCount = 5;
        grid.Columns[0].HeaderText = "Txn ID";
        grid.Columns[1].HeaderText = "Product";
        grid.Columns[2].HeaderText = "Qty";
        grid.Columns[3].HeaderText = "Date";
        grid.Columns[4].HeaderText = "Customer";

        Controls.Add(grid);
        Controls.Add(lblHint);
        Controls.Add(scrollHost);

        formPanel.ResumeLayout(false);
        formPanel.PerformLayout();
        previewBanner.ResumeLayout(false);
        scrollHost.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)nudQuantity).EndInit();
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(false);
    }
}
