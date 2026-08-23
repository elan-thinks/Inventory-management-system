using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Inventory;

partial class AdjustmentForm
{
    private System.ComponentModel.IContainer components = null;

    private Panel scrollHost;
    private Panel formPanel;
    private Label secProduct;
    private Label lblProduct;
    private ComboBox cboProduct;
    private Label helpProduct;
    private Label secMovement;
    private Label lblPreviousCaption;
    private Label lblPreviousQty;
    private Label lblNewQty;
    private NumericUpDown nudNewQty;
    private Label lblDiffCaption;
    private Label lblDifference;
    private Label lblDate;
    private DateTimePicker dtpDate;
    private Panel previewBanner;
    private Label lblPreview;
    private Label secReason;
    private Label lblReason;
    private TextBox txtReason;
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
        scrollHost = new Panel();
        formPanel = new Panel();
        secProduct = new Label();
        lblProduct = new Label();
        cboProduct = new ComboBox();
        helpProduct = new Label();
        secMovement = new Label();
        lblPreviousCaption = new Label();
        lblPreviousQty = new Label();
        lblNewQty = new Label();
        nudNewQty = new NumericUpDown();
        lblDate = new Label();
        dtpDate = new DateTimePicker();
        lblDiffCaption = new Label();
        lblDifference = new Label();
        previewBanner = new Panel();
        lblPreview = new Label();
        secReason = new Label();
        lblReason = new Label();
        txtReason = new TextBox();
        lblNotes = new Label();
        txtNotes = new TextBox();
        lblError = new Label();
        btnSave = new Button();
        lblHint = new Label();
        grid = new DataGridView();
        scrollHost.SuspendLayout();
        formPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudNewQty).BeginInit();
        previewBanner.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        SuspendLayout();
        // 
        // scrollHost
        // 
        scrollHost.AutoScroll = true;
        scrollHost.Controls.Add(formPanel);
        scrollHost.Dock = DockStyle.Top;
        scrollHost.Location = new Point(8, 8);
        scrollHost.Name = "scrollHost";
        scrollHost.Size = new Size(132, 500);
        scrollHost.TabIndex = 8;
        // 
        // formPanel
        // 
        formPanel.BackColor = Color.White;
        formPanel.Controls.Add(secProduct);
        formPanel.Controls.Add(lblProduct);
        formPanel.Controls.Add(cboProduct);
        formPanel.Controls.Add(helpProduct);
        formPanel.Controls.Add(secMovement);
        formPanel.Controls.Add(lblPreviousCaption);
        formPanel.Controls.Add(lblPreviousQty);
        formPanel.Controls.Add(lblNewQty);
        formPanel.Controls.Add(nudNewQty);
        formPanel.Controls.Add(lblDate);
        formPanel.Controls.Add(dtpDate);
        formPanel.Controls.Add(lblDiffCaption);
        formPanel.Controls.Add(lblDifference);
        formPanel.Controls.Add(previewBanner);
        formPanel.Controls.Add(secReason);
        formPanel.Controls.Add(lblReason);
        formPanel.Controls.Add(txtReason);
        formPanel.Controls.Add(lblNotes);
        formPanel.Controls.Add(txtNotes);
        formPanel.Controls.Add(lblError);
        formPanel.Controls.Add(btnSave);
        formPanel.Location = new Point(0, 0);
        formPanel.Name = "formPanel";
        formPanel.Padding = new Padding(20);
        formPanel.Size = new Size(720, 490);
        formPanel.TabIndex = 0;
        // 
        // secProduct
        // 
        secProduct.AutoSize = true;
        secProduct.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        secProduct.ForeColor = Color.FromArgb(59, 122, 143);
        secProduct.Location = new Point(20, 12);
        secProduct.Name = "secProduct";
        secProduct.Size = new Size(79, 20);
        secProduct.TabIndex = 0;
        secProduct.Text = "PRODUCT";
        // 
        // lblProduct
        // 
        lblProduct.AutoSize = true;
        lblProduct.Location = new Point(20, 36);
        lblProduct.Name = "lblProduct";
        lblProduct.Size = new Size(82, 23);
        lblProduct.TabIndex = 1;
        lblProduct.Text = "Product *";
        // 
        // cboProduct
        // 
        cboProduct.DropDownStyle = ComboBoxStyle.DropDownList;
        cboProduct.FlatStyle = FlatStyle.Flat;
        cboProduct.Location = new Point(20, 56);
        cboProduct.Name = "cboProduct";
        cboProduct.Size = new Size(400, 31);
        cboProduct.TabIndex = 0;
        cboProduct.SelectedIndexChanged += CboProduct_SelectedIndexChanged;
        // 
        // helpProduct
        // 
        helpProduct.AutoSize = true;
        helpProduct.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
        helpProduct.ForeColor = Color.FromArgb(107, 119, 133);
        helpProduct.Location = new Point(20, 86);
        helpProduct.Name = "helpProduct";
        helpProduct.Size = new Size(205, 20);
        helpProduct.TabIndex = 2;
        helpProduct.Text = "Only active products are listed.";
        // 
        // secMovement
        // 
        secMovement.AutoSize = true;
        secMovement.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        secMovement.ForeColor = Color.FromArgb(59, 122, 143);
        secMovement.Location = new Point(20, 120);
        secMovement.Name = "secMovement";
        secMovement.Size = new Size(172, 20);
        secMovement.TabIndex = 3;
        secMovement.Text = "ADJUSTMENT DETAILS";
        // 
        // lblPreviousCaption
        // 
        lblPreviousCaption.AutoSize = true;
        lblPreviousCaption.Location = new Point(20, 144);
        lblPreviousCaption.Name = "lblPreviousCaption";
        lblPreviousCaption.Size = new Size(209, 23);
        lblPreviousCaption.TabIndex = 4;
        lblPreviousCaption.Text = "Previous quantity (system)";
        // 
        // lblPreviousQty
        // 
        lblPreviousQty.AutoSize = true;
        lblPreviousQty.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblPreviousQty.Location = new Point(20, 164);
        lblPreviousQty.Name = "lblPreviousQty";
        lblPreviousQty.Size = new Size(38, 32);
        lblPreviousQty.TabIndex = 5;
        lblPreviousQty.Text = "—";
        // 
        // lblNewQty
        // 
        lblNewQty.AutoSize = true;
        lblNewQty.Location = new Point(160, 144);
        lblNewQty.Name = "lblNewQty";
        lblNewQty.Size = new Size(124, 23);
        lblNewQty.TabIndex = 6;
        lblNewQty.Text = "New quantity *";
        // 
        // nudNewQty
        // 
        nudNewQty.Location = new Point(160, 164);
        nudNewQty.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
        nudNewQty.Name = "nudNewQty";
        nudNewQty.Size = new Size(120, 30);
        nudNewQty.TabIndex = 1;
        nudNewQty.ValueChanged += NudNewQty_ValueChanged;
        // 
        // lblDate
        // 
        lblDate.AutoSize = true;
        lblDate.Location = new Point(300, 144);
        lblDate.Name = "lblDate";
        lblDate.Size = new Size(58, 23);
        lblDate.TabIndex = 7;
        lblDate.Text = "Date *";
        // 
        // dtpDate
        // 
        dtpDate.Format = DateTimePickerFormat.Short;
        dtpDate.Location = new Point(300, 164);
        dtpDate.Name = "dtpDate";
        dtpDate.Size = new Size(140, 30);
        dtpDate.TabIndex = 2;
        // 
        // lblDiffCaption
        // 
        lblDiffCaption.AutoSize = true;
        lblDiffCaption.Location = new Point(20, 204);
        lblDiffCaption.Name = "lblDiffCaption";
        lblDiffCaption.Size = new Size(87, 23);
        lblDiffCaption.TabIndex = 8;
        lblDiffCaption.Text = "Difference";
        // 
        // lblDifference
        // 
        lblDifference.AutoSize = true;
        lblDifference.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblDifference.Location = new Point(100, 200);
        lblDifference.Name = "lblDifference";
        lblDifference.Size = new Size(32, 28);
        lblDifference.TabIndex = 9;
        lblDifference.Text = "—";
        // 
        // previewBanner
        // 
        previewBanner.BackColor = Color.FromArgb(231, 240, 247);
        previewBanner.Controls.Add(lblPreview);
        previewBanner.Location = new Point(20, 236);
        previewBanner.Name = "previewBanner";
        previewBanner.Size = new Size(420, 36);
        previewBanner.TabIndex = 10;
        // 
        // lblPreview
        // 
        lblPreview.Dock = DockStyle.Fill;
        lblPreview.Location = new Point(0, 0);
        lblPreview.Name = "lblPreview";
        lblPreview.Size = new Size(420, 36);
        lblPreview.TabIndex = 0;
        lblPreview.Text = "  Select a product and set the new quantity.";
        lblPreview.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // secReason
        // 
        secReason.AutoSize = true;
        secReason.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        secReason.ForeColor = Color.FromArgb(59, 122, 143);
        secReason.Location = new Point(20, 284);
        secReason.Name = "secReason";
        secReason.Size = new Size(124, 20);
        secReason.TabIndex = 11;
        secReason.Text = "REASON & NOTES";
        // 
        // lblReason
        // 
        lblReason.AutoSize = true;
        lblReason.Location = new Point(20, 308);
        lblReason.Name = "lblReason";
        lblReason.Size = new Size(77, 23);
        lblReason.TabIndex = 12;
        lblReason.Text = "Reason *";
        // 
        // txtReason
        // 
        txtReason.BorderStyle = BorderStyle.FixedSingle;
        txtReason.Location = new Point(20, 328);
        txtReason.MaxLength = 250;
        txtReason.Name = "txtReason";
        txtReason.PlaceholderText = "Required — e.g. stock count variance, damage";
        txtReason.Size = new Size(420, 30);
        txtReason.TabIndex = 3;
        // 
        // lblNotes
        // 
        lblNotes.AutoSize = true;
        lblNotes.Location = new Point(20, 364);
        lblNotes.Name = "lblNotes";
        lblNotes.Size = new Size(55, 23);
        lblNotes.TabIndex = 13;
        lblNotes.Text = "Notes";
        // 
        // txtNotes
        // 
        txtNotes.BorderStyle = BorderStyle.FixedSingle;
        txtNotes.Location = new Point(20, 384);
        txtNotes.MaxLength = 500;
        txtNotes.Multiline = true;
        txtNotes.Name = "txtNotes";
        txtNotes.PlaceholderText = "Optional additional detail";
        txtNotes.ScrollBars = ScrollBars.Vertical;
        txtNotes.Size = new Size(420, 48);
        txtNotes.TabIndex = 4;
        // 
        // lblError
        // 
        lblError.Location = new Point(20, 440);
        lblError.Name = "lblError";
        lblError.Size = new Size(280, 32);
        lblError.TabIndex = 14;
        lblError.Visible = false;
        // 
        // btnSave
        // 
        btnSave.Location = new Point(320, 440);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(180, 36);
        btnSave.TabIndex = 5;
        btnSave.Text = "Record Adjustment";
        btnSave.Click += BtnSave_Click;
        // 
        // lblHint
        // 
        lblHint.Dock = DockStyle.Top;
        lblHint.Location = new Point(8, 508);
        lblHint.Name = "lblHint";
        lblHint.Size = new Size(132, 28);
        lblHint.TabIndex = 7;
        lblHint.Text = "  Recent adjustments";
        lblHint.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // grid
        // 
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AllowUserToResizeRows = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.BackgroundColor = Color.White;
        grid.BorderStyle = BorderStyle.None;
        grid.ColumnHeadersHeight = 29;
        grid.Dock = DockStyle.Fill;
        grid.Location = new Point(8, 536);
        grid.MultiSelect = false;
        grid.Name = "grid";
        grid.ReadOnly = true;
        grid.RowHeadersVisible = false;
        grid.RowHeadersWidth = 51;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.Size = new Size(132, 0);
        grid.TabIndex = 6;
        // 
        // AdjustmentForm
        // 
        AutoScaleDimensions = new SizeF(9F, 23F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(244, 246, 248);
        ClientSize = new Size(148, 0);
        Controls.Add(grid);
        Controls.Add(lblHint);
        Controls.Add(scrollHost);
        Font = new Font("Segoe UI", 10F);
        FormBorderStyle = FormBorderStyle.None;
        Name = "AdjustmentForm";
        Padding = new Padding(8);
        Text = "Inventory Adjustment";
        scrollHost.ResumeLayout(false);
        formPanel.ResumeLayout(false);
        formPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudNewQty).EndInit();
        previewBanner.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(false);
    }
}
