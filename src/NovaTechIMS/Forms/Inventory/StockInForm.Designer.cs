using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>
/// Stock-In — modern card layout (designer-visible).
/// Sections: Product &amp; Supplier · Movement · Notes · Actions · Recent grid.
/// </summary>
partial class StockInForm
{
    private System.ComponentModel.IContainer components = null;

    private Panel formCard;
    private Panel secProductPanel;
    private Label secProduct;
    private Label lblProduct;
    private ComboBox cboProduct;
    private Label helpProduct;
    private Label lblSupplier;
    private ComboBox cboSupplier;
    private Label helpSupplier;

    private Panel secMovementPanel;
    private Label secMovement;
    private Label lblQuantity;
    private NumericUpDown nudQuantity;
    private Label lblDate;
    private DateTimePicker dtpDate;
    private Label lblUnitPrice;
    private TextBox txtUnitPrice;
    private Panel previewBanner;
    private Label lblPreview;
    private Label lblCurrentQty;

    private Panel secNotesPanel;
    private Label secNotes;
    private Label lblNotes;
    private TextBox txtNotes;

    private Panel actionsBar;
    private Label lblError;
    private Button btnClear;
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

        formCard = new Panel();
        secProductPanel = new Panel();
        secProduct = new Label();
        lblProduct = new Label();
        cboProduct = new ComboBox();
        helpProduct = new Label();
        lblSupplier = new Label();
        cboSupplier = new ComboBox();
        helpSupplier = new Label();

        secMovementPanel = new Panel();
        secMovement = new Label();
        lblQuantity = new Label();
        nudQuantity = new NumericUpDown();
        lblDate = new Label();
        dtpDate = new DateTimePicker();
        lblUnitPrice = new Label();
        txtUnitPrice = new TextBox();
        previewBanner = new Panel();
        lblPreview = new Label();
        lblCurrentQty = new Label();

        secNotesPanel = new Panel();
        secNotes = new Label();
        lblNotes = new Label();
        txtNotes = new TextBox();

        actionsBar = new Panel();
        lblError = new Label();
        btnClear = new Button();
        btnSave = new Button();

        lblHint = new Label();
        grid = new DataGridView();

        formCard.SuspendLayout();
        secProductPanel.SuspendLayout();
        secMovementPanel.SuspendLayout();
        previewBanner.SuspendLayout();
        secNotesPanel.SuspendLayout();
        actionsBar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudQuantity).BeginInit();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        SuspendLayout();

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.FromArgb(244, 246, 248);
        ClientSize = new Size(980, 720);
        FormBorderStyle = FormBorderStyle.None;
        Name = "StockInForm";
        Text = "Stock In";
        Padding = new Padding(20, 12, 20, 12);

        // ---- Outer form card ----
        formCard.BackColor = Color.White;
        formCard.BorderStyle = BorderStyle.FixedSingle;
        formCard.Dock = DockStyle.Top;
        formCard.Height = 520;
        formCard.Padding = new Padding(20);
        formCard.Name = "formCard";

        // ---- Section: Product & Supplier ----
        secProductPanel.BackColor = Color.FromArgb(252, 253, 254);
        secProductPanel.BorderStyle = BorderStyle.FixedSingle;
        secProductPanel.Location = new Point(20, 16);
        secProductPanel.Size = new Size(920, 150);
        secProductPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        secProductPanel.Name = "secProductPanel";
        secProductPanel.Padding = new Padding(16);

        secProduct.Text = "PRODUCT  &  SUPPLIER";
        secProduct.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        secProduct.ForeColor = Color.FromArgb(59, 122, 143);
        secProduct.Location = new Point(16, 12);
        secProduct.AutoSize = true;
        secProduct.Name = "secProduct";

        lblProduct.Text = "Product *";
        lblProduct.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblProduct.ForeColor = Color.FromArgb(31, 41, 51);
        lblProduct.Location = new Point(16, 40);
        lblProduct.AutoSize = true;
        lblProduct.Name = "lblProduct";

        cboProduct.DropDownStyle = ComboBoxStyle.DropDownList;
        cboProduct.FlatStyle = FlatStyle.Flat;
        cboProduct.Font = new Font("Segoe UI", 10F);
        cboProduct.Items.AddRange(new object[] { "— Select product —" });
        cboProduct.Location = new Point(16, 62);
        cboProduct.Size = new Size(560, 28);
        cboProduct.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cboProduct.Name = "cboProduct";
        cboProduct.TabIndex = 0;
        cboProduct.SelectedIndexChanged += CboProduct_SelectedIndexChanged;

        helpProduct.Text = "Only active products are listed.";
        helpProduct.Font = new Font("Segoe UI", 8.25F);
        helpProduct.ForeColor = Color.FromArgb(107, 119, 133);
        helpProduct.Location = new Point(16, 94);
        helpProduct.AutoSize = true;
        helpProduct.Name = "helpProduct";

        lblSupplier.Text = "Actual Supplier *";
        lblSupplier.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblSupplier.ForeColor = Color.FromArgb(31, 41, 51);
        lblSupplier.Location = new Point(16, 118);
        lblSupplier.AutoSize = true;
        lblSupplier.Name = "lblSupplier";

        // Supplier row sits slightly lower — expand panel if needed; place on same visual block
        // Reposition supplier under product with more vertical space by using wider card
        // Actually put supplier fields shifted: product full width top, supplier below help

        secProductPanel.Controls.Add(secProduct);
        secProductPanel.Controls.Add(lblProduct);
        secProductPanel.Controls.Add(cboProduct);
        secProductPanel.Controls.Add(helpProduct);

        // Rebuild product section taller for both dropdowns
        secProductPanel.Size = new Size(920, 200);

        lblSupplier.Location = new Point(16, 118);
        cboSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
        cboSupplier.FlatStyle = FlatStyle.Flat;
        cboSupplier.Font = new Font("Segoe UI", 10F);
        cboSupplier.Items.AddRange(new object[] { "— Select supplier —" });
        cboSupplier.Location = new Point(16, 140);
        cboSupplier.Size = new Size(560, 28);
        cboSupplier.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cboSupplier.Name = "cboSupplier";
        cboSupplier.TabIndex = 1;

        helpSupplier.Text = "Defaults to the product’s usual supplier — change if needed.";
        helpSupplier.Font = new Font("Segoe UI", 8.25F);
        helpSupplier.ForeColor = Color.FromArgb(107, 119, 133);
        helpSupplier.Location = new Point(16, 172);
        helpSupplier.AutoSize = true;
        helpSupplier.Name = "helpSupplier";

        secProductPanel.Controls.Add(lblSupplier);
        secProductPanel.Controls.Add(cboSupplier);
        secProductPanel.Controls.Add(helpSupplier);

        // ---- Section: Movement Details ----
        secMovementPanel.BackColor = Color.FromArgb(252, 253, 254);
        secMovementPanel.BorderStyle = BorderStyle.FixedSingle;
        secMovementPanel.Location = new Point(20, 228);
        secMovementPanel.Size = new Size(920, 130);
        secMovementPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        secMovementPanel.Name = "secMovementPanel";

        secMovement.Text = "MOVEMENT DETAILS";
        secMovement.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        secMovement.ForeColor = Color.FromArgb(59, 122, 143);
        secMovement.Location = new Point(16, 12);
        secMovement.AutoSize = true;
        secMovement.Name = "secMovement";

        lblQuantity.Text = "Quantity *";
        lblQuantity.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblQuantity.ForeColor = Color.FromArgb(31, 41, 51);
        lblQuantity.Location = new Point(16, 40);
        lblQuantity.AutoSize = true;
        lblQuantity.Name = "lblQuantity";

        nudQuantity.Font = new Font("Segoe UI", 10F);
        nudQuantity.Location = new Point(16, 62);
        nudQuantity.Minimum = 1;
        nudQuantity.Maximum = 1000000;
        nudQuantity.Value = 1;
        nudQuantity.Size = new Size(140, 28);
        nudQuantity.Name = "nudQuantity";
        nudQuantity.TabIndex = 2;
        nudQuantity.ValueChanged += NudQuantity_ValueChanged;

        lblDate.Text = "Date *";
        lblDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblDate.ForeColor = Color.FromArgb(31, 41, 51);
        lblDate.Location = new Point(180, 40);
        lblDate.AutoSize = true;
        lblDate.Name = "lblDate";

        dtpDate.Font = new Font("Segoe UI", 10F);
        dtpDate.Format = DateTimePickerFormat.Short;
        dtpDate.Location = new Point(180, 62);
        dtpDate.Size = new Size(160, 28);
        dtpDate.Name = "dtpDate";
        dtpDate.TabIndex = 3;

        lblUnitPrice.Text = "Purchase price *";
        lblUnitPrice.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblUnitPrice.ForeColor = Color.FromArgb(31, 41, 51);
        lblUnitPrice.Location = new Point(360, 40);
        lblUnitPrice.AutoSize = true;
        lblUnitPrice.Name = "lblUnitPrice";

        txtUnitPrice.BorderStyle = BorderStyle.FixedSingle;
        txtUnitPrice.Font = new Font("Segoe UI", 10F);
        txtUnitPrice.Location = new Point(360, 62);
        txtUnitPrice.Size = new Size(140, 28);
        txtUnitPrice.Name = "txtUnitPrice";
        txtUnitPrice.TabIndex = 4;
        txtUnitPrice.Text = "0.00";

        previewBanner.Location = new Point(16, 98);
        previewBanner.Size = new Size(880, 24);
        previewBanner.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        previewBanner.BackColor = Color.FromArgb(231, 240, 247);
        previewBanner.Name = "previewBanner";

        lblPreview.Dock = DockStyle.Fill;
        lblPreview.Font = new Font("Segoe UI", 9F);
        lblPreview.ForeColor = Color.FromArgb(31, 41, 51);
        lblPreview.Text = "  Select a product to see the new quantity on hand.";
        lblPreview.TextAlign = ContentAlignment.MiddleLeft;
        lblPreview.Name = "lblPreview";
        previewBanner.Controls.Add(lblPreview);

        lblCurrentQty.Visible = false;
        lblCurrentQty.Name = "lblCurrentQty";
        lblCurrentQty.Text = "—";

        secMovementPanel.Controls.Add(secMovement);
        secMovementPanel.Controls.Add(lblQuantity);
        secMovementPanel.Controls.Add(nudQuantity);
        secMovementPanel.Controls.Add(lblDate);
        secMovementPanel.Controls.Add(dtpDate);
        secMovementPanel.Controls.Add(lblUnitPrice);
        secMovementPanel.Controls.Add(txtUnitPrice);
        secMovementPanel.Controls.Add(previewBanner);
        secMovementPanel.Controls.Add(lblCurrentQty);

        // ---- Section: Notes ----
        secNotesPanel.BackColor = Color.FromArgb(252, 253, 254);
        secNotesPanel.BorderStyle = BorderStyle.FixedSingle;
        secNotesPanel.Location = new Point(20, 370);
        secNotesPanel.Size = new Size(920, 88);
        secNotesPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        secNotesPanel.Name = "secNotesPanel";

        secNotes.Text = "NOTES";
        secNotes.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        secNotes.ForeColor = Color.FromArgb(59, 122, 143);
        secNotes.Location = new Point(16, 10);
        secNotes.AutoSize = true;
        secNotes.Name = "secNotes";

        lblNotes.Text = "Notes";
        lblNotes.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblNotes.ForeColor = Color.FromArgb(31, 41, 51);
        lblNotes.Location = new Point(16, 32);
        lblNotes.AutoSize = true;
        lblNotes.Name = "lblNotes";

        txtNotes.BorderStyle = BorderStyle.FixedSingle;
        txtNotes.Font = new Font("Segoe UI", 10F);
        txtNotes.Location = new Point(16, 52);
        txtNotes.Multiline = true;
        txtNotes.MaxLength = 500;
        txtNotes.ScrollBars = ScrollBars.Vertical;
        txtNotes.Size = new Size(880, 28);
        txtNotes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtNotes.Name = "txtNotes";
        txtNotes.TabIndex = 5;
        txtNotes.PlaceholderText = "Optional — e.g. delivery reference, condition of goods";

        secNotesPanel.Controls.Add(secNotes);
        secNotesPanel.Controls.Add(lblNotes);
        secNotesPanel.Controls.Add(txtNotes);

        // ---- Actions bar (never overlaps notes) ----
        actionsBar.Location = new Point(20, 468);
        actionsBar.Size = new Size(920, 40);
        actionsBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        actionsBar.Name = "actionsBar";

        lblError.Location = new Point(0, 6);
        lblError.Size = new Size(480, 28);
        lblError.ForeColor = Color.FromArgb(192, 57, 43);
        lblError.Font = new Font("Segoe UI", 9F);
        lblError.Visible = false;
        lblError.Name = "lblError";

        btnClear.FlatStyle = FlatStyle.Flat;
        btnClear.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnClear.BackColor = Color.White;
        btnClear.ForeColor = Color.FromArgb(30, 75, 143);
        btnClear.FlatAppearance.BorderColor = Color.FromArgb(30, 75, 143);
        btnClear.FlatAppearance.BorderSize = 1;
        btnClear.Size = new Size(110, 34);
        btnClear.Location = new Point(640, 2);
        btnClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnClear.Text = "Clear Form";
        btnClear.Name = "btnClear";
        btnClear.Cursor = Cursors.Hand;
        btnClear.TabIndex = 6;
        btnClear.Click += BtnClear_Click;

        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnSave.BackColor = Color.FromArgb(30, 75, 143);
        btnSave.ForeColor = Color.White;
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.Size = new Size(160, 34);
        btnSave.Location = new Point(760, 2);
        btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnSave.Text = "Record Stock-In";
        btnSave.Name = "btnSave";
        btnSave.Cursor = Cursors.Hand;
        btnSave.TabIndex = 7;
        btnSave.UseVisualStyleBackColor = false;
        btnSave.Click += BtnSave_Click;

        actionsBar.Controls.Add(lblError);
        actionsBar.Controls.Add(btnClear);
        actionsBar.Controls.Add(btnSave);

        formCard.Controls.Add(secProductPanel);
        formCard.Controls.Add(secMovementPanel);
        formCard.Controls.Add(secNotesPanel);
        formCard.Controls.Add(actionsBar);

        // ---- Recent list ----
        lblHint.Dock = DockStyle.Top;
        lblHint.Height = 32;
        lblHint.Text = "Recent Stock-In transactions";
        lblHint.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblHint.ForeColor = Color.FromArgb(31, 41, 51);
        lblHint.TextAlign = ContentAlignment.MiddleLeft;
        lblHint.Padding = new Padding(0, 8, 0, 0);
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
        grid.TabIndex = 8;

        // Dock order: Fill first, then tops
        Controls.Add(grid);
        Controls.Add(lblHint);
        Controls.Add(formCard);

        formCard.ResumeLayout(false);
        secProductPanel.ResumeLayout(false);
        secProductPanel.PerformLayout();
        secMovementPanel.ResumeLayout(false);
        secMovementPanel.PerformLayout();
        previewBanner.ResumeLayout(false);
        secNotesPanel.ResumeLayout(false);
        secNotesPanel.PerformLayout();
        actionsBar.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)nudQuantity).EndInit();
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(false);
    }
}
