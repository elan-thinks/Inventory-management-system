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
        components = new System.ComponentModel.Container();

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
        lblDiffCaption = new Label();
        lblDifference = new Label();
        lblDate = new Label();
        dtpDate = new DateTimePicker();
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
        previewBanner.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudNewQty).BeginInit();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        SuspendLayout();

        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.FromArgb(244, 246, 248);
        FormBorderStyle = FormBorderStyle.None;
        Dock = DockStyle.Fill;
        TopLevel = false;
        Name = "AdjustmentForm";
        Text = "Inventory Adjustment";
        Padding = new Padding(8);

        scrollHost.Dock = DockStyle.Top;
        scrollHost.Height = 500;
        scrollHost.AutoScroll = true;
        scrollHost.Name = "scrollHost";

        formPanel.Location = new Point(0, 0);
        formPanel.Size = new Size(720, 490);
        formPanel.BackColor = Color.White;
        formPanel.Padding = new Padding(20);
        formPanel.Name = "formPanel";

        secProduct.Text = "PRODUCT";
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

        secMovement.Text = "ADJUSTMENT DETAILS";
        secMovement.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        secMovement.ForeColor = Color.FromArgb(59, 122, 143);
        secMovement.Location = new Point(20, 120);
        secMovement.AutoSize = true;
        secMovement.Name = "secMovement";

        lblPreviousCaption.Text = "Previous quantity (system)";
        lblPreviousCaption.Location = new Point(20, 144);
        lblPreviousCaption.AutoSize = true;
        lblPreviousCaption.Name = "lblPreviousCaption";

        lblPreviousQty.Text = "—";
        lblPreviousQty.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblPreviousQty.Location = new Point(20, 164);
        lblPreviousQty.AutoSize = true;
        lblPreviousQty.Name = "lblPreviousQty";

        lblNewQty.Text = "New quantity *";
        lblNewQty.Location = new Point(160, 144);
        lblNewQty.AutoSize = true;
        lblNewQty.Name = "lblNewQty";

        nudNewQty.Location = new Point(160, 164);
        nudNewQty.Minimum = 0;
        nudNewQty.Maximum = 10000000;
        nudNewQty.Value = 0;
        nudNewQty.Size = new Size(120, 28);
        nudNewQty.Name = "nudNewQty";
        nudNewQty.TabIndex = 1;
        nudNewQty.ValueChanged += NudNewQty_ValueChanged;

        lblDate.Text = "Date *";
        lblDate.Location = new Point(300, 144);
        lblDate.AutoSize = true;
        lblDate.Name = "lblDate";

        dtpDate.Format = DateTimePickerFormat.Short;
        dtpDate.Location = new Point(300, 164);
        dtpDate.Size = new Size(140, 28);
        dtpDate.Name = "dtpDate";
        dtpDate.TabIndex = 2;

        lblDiffCaption.Text = "Difference";
        lblDiffCaption.Location = new Point(20, 204);
        lblDiffCaption.AutoSize = true;
        lblDiffCaption.Name = "lblDiffCaption";

        lblDifference.Text = "—";
        lblDifference.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblDifference.Location = new Point(100, 200);
        lblDifference.AutoSize = true;
        lblDifference.Name = "lblDifference";

        previewBanner.Location = new Point(20, 236);
        previewBanner.Size = new Size(420, 36);
        previewBanner.BackColor = Color.FromArgb(231, 240, 247);
        previewBanner.Name = "previewBanner";

        lblPreview.Dock = DockStyle.Fill;
        lblPreview.Text = "  Select a product and set the new quantity.";
        lblPreview.TextAlign = ContentAlignment.MiddleLeft;
        lblPreview.Name = "lblPreview";
        previewBanner.Controls.Add(lblPreview);

        secReason.Text = "REASON & NOTES";
        secReason.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        secReason.ForeColor = Color.FromArgb(59, 122, 143);
        secReason.Location = new Point(20, 284);
        secReason.AutoSize = true;
        secReason.Name = "secReason";

        lblReason.Text = "Reason *";
        lblReason.Location = new Point(20, 308);
        lblReason.AutoSize = true;
        lblReason.Name = "lblReason";

        txtReason.BorderStyle = BorderStyle.FixedSingle;
        txtReason.Location = new Point(20, 328);
        txtReason.MaxLength = 250;
        txtReason.Size = new Size(420, 28);
        txtReason.Name = "txtReason";
        txtReason.TabIndex = 3;
        txtReason.PlaceholderText = "Required — e.g. stock count variance, damage";

        lblNotes.Text = "Notes";
        lblNotes.Location = new Point(20, 364);
        lblNotes.AutoSize = true;
        lblNotes.Name = "lblNotes";

        txtNotes.BorderStyle = BorderStyle.FixedSingle;
        txtNotes.Location = new Point(20, 384);
        txtNotes.Multiline = true;
        txtNotes.MaxLength = 500;
        txtNotes.ScrollBars = ScrollBars.Vertical;
        txtNotes.Size = new Size(420, 48);
        txtNotes.Name = "txtNotes";
        txtNotes.TabIndex = 4;
        txtNotes.PlaceholderText = "Optional additional detail";

        lblError.Location = new Point(20, 440);
        lblError.Size = new Size(280, 32);
        lblError.Visible = false;
        lblError.Name = "lblError";

        btnSave.Location = new Point(320, 440);
        btnSave.Size = new Size(180, 36);
        btnSave.Text = "Record Adjustment";
        btnSave.Name = "btnSave";
        btnSave.TabIndex = 5;
        btnSave.Click += BtnSave_Click;

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

        scrollHost.Controls.Add(formPanel);

        lblHint.Dock = DockStyle.Top;
        lblHint.Height = 28;
        lblHint.Text = "  Recent adjustments";
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
        grid.BorderStyle = BorderStyle.None;
        grid.BackgroundColor = Color.White;
        grid.Name = "grid";
        grid.TabIndex = 6;

        Controls.Add(grid);
        Controls.Add(lblHint);
        Controls.Add(scrollHost);

        formPanel.ResumeLayout(false);
        formPanel.PerformLayout();
        previewBanner.ResumeLayout(false);
        scrollHost.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)nudNewQty).EndInit();
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(false);
    }
}
