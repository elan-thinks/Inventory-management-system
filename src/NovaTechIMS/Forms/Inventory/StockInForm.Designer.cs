using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>
/// Stock-In — full content area, no inner scroll, mockup-style fields.
/// </summary>
partial class StockInForm
{
    private System.ComponentModel.IContainer components = null;

    private Panel formCard;
    private Panel headerPanel;
    private Label lblFormTitle;
    private Label lblFormSub;

    private TableLayoutPanel gridFields;

    private Label lblProduct;
    private ComboBox cboProduct;
    private Label helpProduct;
    private Label lblSupplier;
    private ComboBox cboSupplier;
    private Label helpSupplier;

    private Label lblQuantity;
    private NumericUpDown nudQuantity;
    private Label lblDate;
    private DateTimePicker dtpDate;
    private Label lblUnitPrice;
    private TextBox txtUnitPrice;

    private Panel previewBanner;
    private Label lblPreview;
    private Label lblCurrentQty;

    private Label lblNotes;
    private Label helpNotes;
    private TextBox txtNotes;

    private Panel actionsBar;
    private Label lblError;
    private Button btnClear;
    private Button btnSave;

    // Kept for code-behind compatibility (hidden / unused in new layout)
    private Panel contentHost;
    private Panel secProductPanel;
    private Panel secMovementPanel;
    private Panel secNotesPanel;
    private Label secProduct;
    private Label secMovement;
    private Label secNotes;

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
        headerPanel = new Panel();
        lblFormTitle = new Label();
        lblFormSub = new Label();

        gridFields = new TableLayoutPanel();

        lblProduct = new Label();
        cboProduct = new ComboBox();
        helpProduct = new Label();
        lblSupplier = new Label();
        cboSupplier = new ComboBox();
        helpSupplier = new Label();

        lblQuantity = new Label();
        nudQuantity = new NumericUpDown();
        lblDate = new Label();
        dtpDate = new DateTimePicker();
        lblUnitPrice = new Label();
        txtUnitPrice = new TextBox();

        previewBanner = new Panel();
        lblPreview = new Label();
        lblCurrentQty = new Label();

        lblNotes = new Label();
        helpNotes = new Label();
        txtNotes = new TextBox();

        actionsBar = new Panel();
        lblError = new Label();
        btnClear = new Button();
        btnSave = new Button();

        // Compatibility shells (empty, not shown)
        contentHost = new Panel();
        secProductPanel = new Panel();
        secMovementPanel = new Panel();
        secNotesPanel = new Panel();
        secProduct = new Label();
        secMovement = new Label();
        secNotes = new Label();

        formCard.SuspendLayout();
        headerPanel.SuspendLayout();
        gridFields.SuspendLayout();
        previewBanner.SuspendLayout();
        actionsBar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudQuantity).BeginInit();
        SuspendLayout();

        var pageBg = Color.FromArgb(246, 248, 250);
        var accent = Color.FromArgb(15, 118, 70);
        var textMain = Color.FromArgb(28, 35, 43);
        var textMuted = Color.FromArgb(120, 130, 145);
        var borderSoft = Color.FromArgb(218, 224, 230);

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        BackColor = pageBg;
        ClientSize = new Size(960, 640);
        FormBorderStyle = FormBorderStyle.None;
        Name = "StockInForm";
        Text = "Stock In";
        Padding = new Padding(20, 12, 20, 12);

        // ---- Full-area white card (NO AutoScroll) ----
        formCard.Dock = DockStyle.Fill;
        formCard.BackColor = Color.White;
        formCard.BorderStyle = BorderStyle.None;
        formCard.Padding = new Padding(32, 24, 32, 16);
        formCard.Name = "formCard";

        // Header
        headerPanel.Dock = DockStyle.Top;
        headerPanel.Height = 58;
        headerPanel.BackColor = Color.White;
        headerPanel.Name = "headerPanel";

        lblFormTitle.AutoSize = true;
        lblFormTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblFormTitle.ForeColor = textMain;
        lblFormTitle.Location = new Point(0, 2);
        lblFormTitle.Name = "lblFormTitle";
        lblFormTitle.Text = "Record Stock-In";

        lblFormSub.AutoSize = true;
        lblFormSub.Font = new Font("Segoe UI", 9F);
        lblFormSub.ForeColor = textMuted;
        lblFormSub.Location = new Point(2, 32);
        lblFormSub.Name = "lblFormSub";
        lblFormSub.Text = "Receive goods into inventory from a supplier.";

        headerPanel.Controls.Add(lblFormTitle);
        headerPanel.Controls.Add(lblFormSub);

        // Actions inside card bottom (like mockup)
        actionsBar.Dock = DockStyle.Bottom;
        actionsBar.Height = 56;
        actionsBar.BackColor = Color.White;
        actionsBar.Name = "actionsBar";

        lblError.AutoSize = false;
        lblError.Location = new Point(0, 14);
        lblError.Size = new Size(360, 28);
        lblError.ForeColor = Color.FromArgb(180, 55, 50);
        lblError.Font = new Font("Segoe UI", 9F);
        lblError.Visible = false;
        lblError.Name = "lblError";

        btnClear.FlatStyle = FlatStyle.Flat;
        btnClear.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnClear.BackColor = Color.White;
        btnClear.ForeColor = textMain;
        btnClear.FlatAppearance.BorderColor = borderSoft;
        btnClear.FlatAppearance.BorderSize = 1;
        btnClear.Size = new Size(110, 38);
        btnClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnClear.Location = new Point(620, 8);
        btnClear.Text = "Reset";
        btnClear.Name = "btnClear";
        btnClear.Cursor = Cursors.Hand;
        btnClear.TabIndex = 6;
        btnClear.Click += BtnClear_Click;

        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnSave.BackColor = accent;
        btnSave.ForeColor = Color.White;
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.Size = new Size(168, 38);
        btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnSave.Location = new Point(742, 8);
        btnSave.Text = "  Record Stock-In";
        btnSave.Name = "btnSave";
        btnSave.Cursor = Cursors.Hand;
        btnSave.TabIndex = 7;
        btnSave.UseVisualStyleBackColor = false;
        btnSave.Click += BtnSave_Click;

        actionsBar.Controls.Add(lblError);
        actionsBar.Controls.Add(btnClear);
        actionsBar.Controls.Add(btnSave);

        // ---- Field grid: 2 columns ----
        gridFields.Dock = DockStyle.Fill;
        gridFields.ColumnCount = 2;
        gridFields.RowCount = 10;
        gridFields.BackColor = Color.White;
        gridFields.Name = "gridFields";
        gridFields.Padding = new Padding(0, 8, 0, 0);

        gridFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        gridFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

        // Row heights
        gridFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));  // label product
        gridFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));  // combo product
        gridFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));  // help product
        gridFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));  // spacer
        gridFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));  // label qty/date/price row
        gridFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));  // inputs qty/date/price
        gridFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));  // spacer
        gridFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));  // preview
        gridFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));  // notes label
        gridFields.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // notes box fills rest

        // --- Product | Supplier labels ---
        lblProduct.Text = "Product *";
        lblProduct.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblProduct.ForeColor = textMain;
        lblProduct.Dock = DockStyle.Fill;
        lblProduct.TextAlign = ContentAlignment.BottomLeft;
        lblProduct.Name = "lblProduct";
        lblProduct.Margin = new Padding(0, 0, 12, 0);

        lblSupplier.Text = "Supplier *";
        lblSupplier.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblSupplier.ForeColor = textMain;
        lblSupplier.Dock = DockStyle.Fill;
        lblSupplier.TextAlign = ContentAlignment.BottomLeft;
        lblSupplier.Name = "lblSupplier";
        lblSupplier.Margin = new Padding(12, 0, 0, 0);

        gridFields.Controls.Add(lblProduct, 0, 0);
        gridFields.Controls.Add(lblSupplier, 1, 0);

        // --- Combos ---
        cboProduct.DropDownStyle = ComboBoxStyle.DropDownList;
        cboProduct.FlatStyle = FlatStyle.Flat;
        cboProduct.Font = new Font("Segoe UI", 10F);
        cboProduct.Dock = DockStyle.Fill;
        cboProduct.Items.AddRange(new object[] { "— Select product —" });
        cboProduct.Name = "cboProduct";
        cboProduct.TabIndex = 0;
        cboProduct.Margin = new Padding(0, 4, 12, 0);
        cboProduct.SelectedIndexChanged += CboProduct_SelectedIndexChanged;

        cboSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
        cboSupplier.FlatStyle = FlatStyle.Flat;
        cboSupplier.Font = new Font("Segoe UI", 10F);
        cboSupplier.Dock = DockStyle.Fill;
        cboSupplier.Items.AddRange(new object[] { "— Select supplier —" });
        cboSupplier.Name = "cboSupplier";
        cboSupplier.TabIndex = 1;
        cboSupplier.Margin = new Padding(12, 4, 0, 0);

        gridFields.Controls.Add(cboProduct, 0, 1);
        gridFields.Controls.Add(cboSupplier, 1, 1);

        // --- Help text ---
        helpProduct.Text = "Only active products are listed.";
        helpProduct.Font = new Font("Segoe UI", 8F);
        helpProduct.ForeColor = textMuted;
        helpProduct.Dock = DockStyle.Fill;
        helpProduct.Name = "helpProduct";
        helpProduct.Margin = new Padding(0, 2, 12, 0);

        helpSupplier.Text = "Defaults to the product’s usual supplier.";
        helpSupplier.Font = new Font("Segoe UI", 8F);
        helpSupplier.ForeColor = textMuted;
        helpSupplier.Dock = DockStyle.Fill;
        helpSupplier.Name = "helpSupplier";
        helpSupplier.Margin = new Padding(12, 2, 0, 0);

        gridFields.Controls.Add(helpProduct, 0, 2);
        gridFields.Controls.Add(helpSupplier, 1, 2);

        // spacer row 3 — empty

        // --- Qty / Date / Price labels in a nested 3-col panel spanning both columns ---
        var movementHost = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Name = "movementHost" };
        var movementGrid = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 3,
            RowCount = 2,
            BackColor = Color.White,
            Name = "movementGrid"
        };
        movementGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
        movementGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36F));
        movementGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36F));
        movementGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
        movementGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));

        lblQuantity.Text = "Quantity *";
        lblQuantity.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblQuantity.ForeColor = textMain;
        lblQuantity.Dock = DockStyle.Fill;
        lblQuantity.TextAlign = ContentAlignment.BottomLeft;
        lblQuantity.Name = "lblQuantity";
        lblQuantity.Margin = new Padding(0, 0, 8, 0);

        lblDate.Text = "Date *";
        lblDate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblDate.ForeColor = textMain;
        lblDate.Dock = DockStyle.Fill;
        lblDate.TextAlign = ContentAlignment.BottomLeft;
        lblDate.Name = "lblDate";
        lblDate.Margin = new Padding(8, 0, 8, 0);

        lblUnitPrice.Text = "Purchase price *";
        lblUnitPrice.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblUnitPrice.ForeColor = textMain;
        lblUnitPrice.Dock = DockStyle.Fill;
        lblUnitPrice.TextAlign = ContentAlignment.BottomLeft;
        lblUnitPrice.Name = "lblUnitPrice";
        lblUnitPrice.Margin = new Padding(8, 0, 0, 0);

        nudQuantity.Font = new Font("Segoe UI", 10F);
        nudQuantity.Dock = DockStyle.Fill;
        nudQuantity.Minimum = 1;
        nudQuantity.Maximum = 1000000;
        nudQuantity.Value = 1;
        nudQuantity.Name = "nudQuantity";
        nudQuantity.TabIndex = 2;
        nudQuantity.Margin = new Padding(0, 4, 8, 0);
        nudQuantity.ValueChanged += NudQuantity_ValueChanged;

        dtpDate.Font = new Font("Segoe UI", 10F);
        dtpDate.Format = DateTimePickerFormat.Short;
        dtpDate.Dock = DockStyle.Fill;
        dtpDate.Name = "dtpDate";
        dtpDate.TabIndex = 3;
        dtpDate.Margin = new Padding(8, 4, 8, 0);

        txtUnitPrice.BorderStyle = BorderStyle.FixedSingle;
        txtUnitPrice.Font = new Font("Segoe UI", 10F);
        txtUnitPrice.Dock = DockStyle.Fill;
        txtUnitPrice.Name = "txtUnitPrice";
        txtUnitPrice.TabIndex = 4;
        txtUnitPrice.Text = "0.00";
        txtUnitPrice.Margin = new Padding(8, 4, 0, 0);

        movementGrid.Controls.Add(lblQuantity, 0, 0);
        movementGrid.Controls.Add(lblDate, 1, 0);
        movementGrid.Controls.Add(lblUnitPrice, 2, 0);
        movementGrid.Controls.Add(nudQuantity, 0, 1);
        movementGrid.Controls.Add(dtpDate, 1, 1);
        movementGrid.Controls.Add(txtUnitPrice, 2, 1);
        movementHost.Controls.Add(movementGrid);

        // Span movement across both columns of rows 4-5 by merging via single panel in row 4 spanning
        // Simpler: put movement host in row 4 spanning 2 cols, and empty row 5
        gridFields.SetColumnSpan(movementHost, 2);
        // Actually we need labels+inputs in consecutive rows. Put whole movement block as one cell spanning 2 cols in row 4, with taller row.
        gridFields.RowStyles[4] = new RowStyle(SizeType.Absolute, 64F);
        gridFields.RowStyles[5] = new RowStyle(SizeType.Absolute, 8F); // thin spacer
        gridFields.Controls.Add(movementHost, 0, 4);

        // Preview spanning 2 cols
        previewBanner.Dock = DockStyle.Fill;
        previewBanner.BackColor = Color.FromArgb(232, 245, 238);
        previewBanner.BorderStyle = BorderStyle.None;
        previewBanner.Name = "previewBanner";
        previewBanner.Margin = new Padding(0, 4, 0, 4);

        lblPreview.Dock = DockStyle.Fill;
        lblPreview.Font = new Font("Segoe UI", 9.5F);
        lblPreview.ForeColor = textMain;
        lblPreview.Text = "  Select a product to see the new quantity on hand.";
        lblPreview.TextAlign = ContentAlignment.MiddleLeft;
        lblPreview.Name = "lblPreview";
        previewBanner.Controls.Add(lblPreview);

        lblCurrentQty.Visible = false;
        lblCurrentQty.Name = "lblCurrentQty";
        lblCurrentQty.Text = "—";

        gridFields.SetColumnSpan(previewBanner, 2);
        gridFields.Controls.Add(previewBanner, 0, 7);

        // Notes label spanning
        var notesLabelHost = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
        lblNotes.Text = "Notes";
        lblNotes.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblNotes.ForeColor = textMain;
        lblNotes.AutoSize = true;
        lblNotes.Location = new Point(0, 0);
        lblNotes.Name = "lblNotes";

        helpNotes.Text = "Optional — delivery reference, condition of goods";
        helpNotes.Font = new Font("Segoe UI", 8F);
        helpNotes.ForeColor = textMuted;
        helpNotes.AutoSize = true;
        helpNotes.Location = new Point(0, 18);
        helpNotes.Name = "helpNotes";

        notesLabelHost.Controls.Add(lblNotes);
        notesLabelHost.Controls.Add(helpNotes);
        gridFields.RowStyles[8] = new RowStyle(SizeType.Absolute, 40F);
        gridFields.SetColumnSpan(notesLabelHost, 2);
        gridFields.Controls.Add(notesLabelHost, 0, 8);

        txtNotes.BorderStyle = BorderStyle.FixedSingle;
        txtNotes.Font = new Font("Segoe UI", 10F);
        txtNotes.Dock = DockStyle.Fill;
        txtNotes.Multiline = true;
        txtNotes.MaxLength = 500;
        txtNotes.ScrollBars = ScrollBars.Vertical;
        txtNotes.Name = "txtNotes";
        txtNotes.TabIndex = 5;
        txtNotes.PlaceholderText = "Describe details here…";
        txtNotes.Margin = new Padding(0, 4, 0, 0);

        gridFields.SetColumnSpan(txtNotes, 2);
        gridFields.Controls.Add(txtNotes, 0, 9);

        // Assemble formCard: header top, actions bottom, grid fill
        formCard.Controls.Add(gridFields);
        formCard.Controls.Add(actionsBar);
        formCard.Controls.Add(headerPanel);

        // Hide unused compatibility controls
        contentHost.Visible = false;
        secProductPanel.Visible = false;
        secMovementPanel.Visible = false;
        secNotesPanel.Visible = false;

        Controls.Add(formCard);

        formCard.ResumeLayout(false);
        headerPanel.ResumeLayout(false);
        headerPanel.PerformLayout();
        gridFields.ResumeLayout(false);
        previewBanner.ResumeLayout(false);
        actionsBar.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)nudQuantity).EndInit();
        ResumeLayout(false);
    }
}
