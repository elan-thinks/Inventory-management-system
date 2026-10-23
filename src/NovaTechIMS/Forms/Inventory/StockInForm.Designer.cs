using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>
/// Stock-In — soft modern form card (Designer-visible).
/// No FixedSingle sharp borders; soft painted edges at runtime.
/// </summary>
partial class StockInForm
{
    private System.ComponentModel.IContainer components = null;

    private Panel actionsBar;
    private Label lblError;
    private Button btnClear;
    private Button btnSave;

    private Panel contentHost;
    private Panel formCard;

    private Panel headerPanel;
    private Label lblFormTitle;
    private Label lblFormSub;

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

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        actionsBar = new Panel();
        lblError = new Label();
        btnClear = new Button();
        btnSave = new Button();

        contentHost = new Panel();
        formCard = new Panel();

        headerPanel = new Panel();
        lblFormTitle = new Label();
        lblFormSub = new Label();

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

        actionsBar.SuspendLayout();
        contentHost.SuspendLayout();
        formCard.SuspendLayout();
        headerPanel.SuspendLayout();
        secProductPanel.SuspendLayout();
        secMovementPanel.SuspendLayout();
        previewBanner.SuspendLayout();
        secNotesPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudQuantity).BeginInit();
        SuspendLayout();

        var pageBg = Color.FromArgb(246, 248, 250);
        var accent = Color.FromArgb(15, 118, 70);
        var textMain = Color.FromArgb(28, 35, 43);
        var textMuted = Color.FromArgb(130, 140, 152);
        var borderSoft = Color.FromArgb(220, 226, 232);

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        BackColor = pageBg;
        ClientSize = new Size(960, 640);
        FormBorderStyle = FormBorderStyle.None;
        Name = "StockInForm";
        Text = "Stock In";
        Padding = new Padding(24, 16, 24, 12);

        // ---- Actions bar ----
        actionsBar.Dock = DockStyle.Bottom;
        actionsBar.Height = 60;
        actionsBar.BackColor = pageBg;
        actionsBar.Padding = new Padding(0, 10, 0, 8);
        actionsBar.Name = "actionsBar";
        actionsBar.BorderStyle = BorderStyle.None;

        lblError.AutoSize = false;
        lblError.Location = new Point(0, 14);
        lblError.Size = new Size(420, 28);
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
        btnClear.Size = new Size(120, 40);
        btnClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnClear.Location = new Point(680, 10);
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
        btnSave.Size = new Size(170, 40);
        btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnSave.Location = new Point(810, 10);
        btnSave.Text = "  Record Stock-In";
        btnSave.Name = "btnSave";
        btnSave.Cursor = Cursors.Hand;
        btnSave.TabIndex = 7;
        btnSave.UseVisualStyleBackColor = false;
        btnSave.Click += BtnSave_Click;

        actionsBar.Controls.Add(lblError);
        actionsBar.Controls.Add(btnClear);
        actionsBar.Controls.Add(btnSave);

        // ---- Content host ----
        contentHost.Dock = DockStyle.Fill;
        contentHost.AutoScroll = true;
        contentHost.Name = "contentHost";
        contentHost.Padding = new Padding(0, 0, 0, 8);
        contentHost.BackColor = pageBg;

        // Single soft white card
        formCard.BackColor = Color.White;
        formCard.BorderStyle = BorderStyle.None;
        formCard.Location = new Point(0, 0);
        formCard.Size = new Size(900, 520);
        formCard.Name = "formCard";
        formCard.Padding = new Padding(28, 20, 28, 20);

        // Header
        headerPanel.Dock = DockStyle.Top;
        headerPanel.Height = 56;
        headerPanel.BackColor = Color.White;
        headerPanel.BorderStyle = BorderStyle.None;
        headerPanel.Name = "headerPanel";

        lblFormTitle.AutoSize = true;
        lblFormTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblFormTitle.ForeColor = textMain;
        lblFormTitle.Location = new Point(0, 4);
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

        // Product section — two-column style
        secProductPanel.BackColor = Color.White;
        secProductPanel.BorderStyle = BorderStyle.None;
        secProductPanel.Location = new Point(0, 64);
        secProductPanel.Size = new Size(844, 150);
        secProductPanel.Name = "secProductPanel";

        secProduct.Text = "Product & supplier";
        secProduct.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        secProduct.ForeColor = textMuted;
        secProduct.Location = new Point(0, 4);
        secProduct.AutoSize = true;
        secProduct.Name = "secProduct";

        lblProduct.Text = "Product *";
        lblProduct.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblProduct.ForeColor = textMain;
        lblProduct.Location = new Point(0, 28);
        lblProduct.AutoSize = true;
        lblProduct.Name = "lblProduct";

        cboProduct.DropDownStyle = ComboBoxStyle.DropDownList;
        cboProduct.FlatStyle = FlatStyle.Flat;
        cboProduct.Font = new Font("Segoe UI", 10F);
        cboProduct.Items.AddRange(new object[] { "— Select product —" });
        cboProduct.Location = new Point(0, 50);
        cboProduct.Size = new Size(400, 28);
        cboProduct.Name = "cboProduct";
        cboProduct.TabIndex = 0;
        cboProduct.SelectedIndexChanged += CboProduct_SelectedIndexChanged;

        helpProduct.Text = "Only active products are listed.";
        helpProduct.Font = new Font("Segoe UI", 8F);
        helpProduct.ForeColor = textMuted;
        helpProduct.Location = new Point(0, 82);
        helpProduct.AutoSize = true;
        helpProduct.Name = "helpProduct";

        lblSupplier.Text = "Supplier *";
        lblSupplier.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblSupplier.ForeColor = textMain;
        lblSupplier.Location = new Point(420, 28);
        lblSupplier.AutoSize = true;
        lblSupplier.Name = "lblSupplier";

        cboSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
        cboSupplier.FlatStyle = FlatStyle.Flat;
        cboSupplier.Font = new Font("Segoe UI", 10F);
        cboSupplier.Items.AddRange(new object[] { "— Select supplier —" });
        cboSupplier.Location = new Point(420, 50);
        cboSupplier.Size = new Size(400, 28);
        cboSupplier.Name = "cboSupplier";
        cboSupplier.TabIndex = 1;

        helpSupplier.Text = "Defaults to the product’s usual supplier.";
        helpSupplier.Font = new Font("Segoe UI", 8F);
        helpSupplier.ForeColor = textMuted;
        helpSupplier.Location = new Point(420, 82);
        helpSupplier.AutoSize = true;
        helpSupplier.Name = "helpSupplier";

        secProductPanel.Controls.Add(secProduct);
        secProductPanel.Controls.Add(lblProduct);
        secProductPanel.Controls.Add(cboProduct);
        secProductPanel.Controls.Add(helpProduct);
        secProductPanel.Controls.Add(lblSupplier);
        secProductPanel.Controls.Add(cboSupplier);
        secProductPanel.Controls.Add(helpSupplier);

        // Movement — three fields in a row
        secMovementPanel.BackColor = Color.White;
        secMovementPanel.BorderStyle = BorderStyle.None;
        secMovementPanel.Location = new Point(0, 220);
        secMovementPanel.Size = new Size(844, 140);
        secMovementPanel.Name = "secMovementPanel";

        secMovement.Text = "Movement details";
        secMovement.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        secMovement.ForeColor = textMuted;
        secMovement.Location = new Point(0, 4);
        secMovement.AutoSize = true;
        secMovement.Name = "secMovement";

        lblQuantity.Text = "Quantity *";
        lblQuantity.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblQuantity.ForeColor = textMain;
        lblQuantity.Location = new Point(0, 28);
        lblQuantity.AutoSize = true;
        lblQuantity.Name = "lblQuantity";

        nudQuantity.Font = new Font("Segoe UI", 10F);
        nudQuantity.Location = new Point(0, 50);
        nudQuantity.Minimum = 1;
        nudQuantity.Maximum = 1000000;
        nudQuantity.Value = 1;
        nudQuantity.Size = new Size(160, 28);
        nudQuantity.Name = "nudQuantity";
        nudQuantity.TabIndex = 2;
        nudQuantity.ValueChanged += NudQuantity_ValueChanged;

        lblDate.Text = "Date *";
        lblDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblDate.ForeColor = textMain;
        lblDate.Location = new Point(190, 28);
        lblDate.AutoSize = true;
        lblDate.Name = "lblDate";

        dtpDate.Font = new Font("Segoe UI", 10F);
        dtpDate.Format = DateTimePickerFormat.Short;
        dtpDate.Location = new Point(190, 50);
        dtpDate.Size = new Size(180, 28);
        dtpDate.Name = "dtpDate";
        dtpDate.TabIndex = 3;

        lblUnitPrice.Text = "Purchase price *";
        lblUnitPrice.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblUnitPrice.ForeColor = textMain;
        lblUnitPrice.Location = new Point(400, 28);
        lblUnitPrice.AutoSize = true;
        lblUnitPrice.Name = "lblUnitPrice";

        txtUnitPrice.BorderStyle = BorderStyle.FixedSingle;
        txtUnitPrice.Font = new Font("Segoe UI", 10F);
        txtUnitPrice.Location = new Point(400, 50);
        txtUnitPrice.Size = new Size(160, 28);
        txtUnitPrice.Name = "txtUnitPrice";
        txtUnitPrice.TabIndex = 4;
        txtUnitPrice.Text = "0.00";

        previewBanner.Location = new Point(0, 96);
        previewBanner.Size = new Size(820, 32);
        previewBanner.BackColor = Color.FromArgb(232, 245, 238);
        previewBanner.BorderStyle = BorderStyle.None;
        previewBanner.Name = "previewBanner";

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

        secMovementPanel.Controls.Add(secMovement);
        secMovementPanel.Controls.Add(lblQuantity);
        secMovementPanel.Controls.Add(nudQuantity);
        secMovementPanel.Controls.Add(lblDate);
        secMovementPanel.Controls.Add(dtpDate);
        secMovementPanel.Controls.Add(lblUnitPrice);
        secMovementPanel.Controls.Add(txtUnitPrice);
        secMovementPanel.Controls.Add(previewBanner);
        secMovementPanel.Controls.Add(lblCurrentQty);

        // Notes
        secNotesPanel.BackColor = Color.White;
        secNotesPanel.BorderStyle = BorderStyle.None;
        secNotesPanel.Location = new Point(0, 368);
        secNotesPanel.Size = new Size(844, 120);
        secNotesPanel.Name = "secNotesPanel";

        secNotes.Text = "Notes";
        secNotes.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        secNotes.ForeColor = textMuted;
        secNotes.Location = new Point(0, 4);
        secNotes.AutoSize = true;
        secNotes.Name = "secNotes";

        lblNotes.Text = "Optional notes";
        lblNotes.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblNotes.ForeColor = textMain;
        lblNotes.Location = new Point(0, 28);
        lblNotes.AutoSize = true;
        lblNotes.Name = "lblNotes";

        txtNotes.BorderStyle = BorderStyle.FixedSingle;
        txtNotes.Font = new Font("Segoe UI", 10F);
        txtNotes.Location = new Point(0, 50);
        txtNotes.Multiline = true;
        txtNotes.MaxLength = 500;
        txtNotes.ScrollBars = ScrollBars.Vertical;
        txtNotes.Size = new Size(820, 56);
        txtNotes.Name = "txtNotes";
        txtNotes.TabIndex = 5;
        txtNotes.PlaceholderText = "e.g. delivery reference, condition of goods";

        secNotesPanel.Controls.Add(secNotes);
        secNotesPanel.Controls.Add(lblNotes);
        secNotesPanel.Controls.Add(txtNotes);

        formCard.Controls.Add(secNotesPanel);
        formCard.Controls.Add(secMovementPanel);
        formCard.Controls.Add(secProductPanel);
        formCard.Controls.Add(headerPanel);

        contentHost.Controls.Add(formCard);

        Controls.Add(contentHost);
        Controls.Add(actionsBar);

        actionsBar.ResumeLayout(false);
        contentHost.ResumeLayout(false);
        formCard.ResumeLayout(false);
        headerPanel.ResumeLayout(false);
        headerPanel.PerformLayout();
        secProductPanel.ResumeLayout(false);
        secProductPanel.PerformLayout();
        secMovementPanel.ResumeLayout(false);
        secMovementPanel.PerformLayout();
        previewBanner.ResumeLayout(false);
        secNotesPanel.ResumeLayout(false);
        secNotesPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudQuantity).EndInit();
        ResumeLayout(false);
    }
}
