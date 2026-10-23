using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>Stock-Out — full content, no recent grid, actions Dock=Bottom.</summary>
partial class StockOutForm
{
    private System.ComponentModel.IContainer components = null;

    private Panel actionsBar;
    private Label lblError;
    private Button btnClear;
    private Button btnSave;

    private Panel contentHost;
    private Panel secProductPanel;
    private Label secProduct;
    private Label lblProduct;
    private ComboBox cboProduct;
    private Label helpProduct;
    private Label lblCustomer;
    private ComboBox cboCustomer;
    private Label helpCustomer;

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
        secProductPanel = new Panel();
        secProduct = new Label();
        lblProduct = new Label();
        cboProduct = new ComboBox();
        helpProduct = new Label();
        lblCustomer = new Label();
        cboCustomer = new ComboBox();
        helpCustomer = new Label();

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
        secProductPanel.SuspendLayout();
        secMovementPanel.SuspendLayout();
        previewBanner.SuspendLayout();
        secNotesPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudQuantity).BeginInit();
        SuspendLayout();

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.FromArgb(244, 246, 248);
        ClientSize = new Size(960, 640);
        FormBorderStyle = FormBorderStyle.None;
        Name = "StockOutForm";
        Text = "Stock Out";
        Padding = new Padding(20, 12, 20, 8);

        actionsBar.Dock = DockStyle.Bottom;
        actionsBar.Height = 56;
        actionsBar.BackColor = Color.FromArgb(244, 246, 248);
        actionsBar.Name = "actionsBar";

        lblError.Location = new Point(0, 12);
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
        btnClear.Size = new Size(120, 38);
        btnClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnClear.Location = new Point(680, 8);
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
        btnSave.Size = new Size(180, 38);
        btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnSave.Location = new Point(810, 8);
        btnSave.Text = "  Record Stock-Out";
        btnSave.Name = "btnSave";
        btnSave.Cursor = Cursors.Hand;
        btnSave.TabIndex = 7;
        btnSave.UseVisualStyleBackColor = false;
        btnSave.Click += BtnSave_Click;

        actionsBar.Controls.Add(lblError);
        actionsBar.Controls.Add(btnClear);
        actionsBar.Controls.Add(btnSave);

        contentHost.Dock = DockStyle.Fill;
        contentHost.AutoScroll = true;
        contentHost.Name = "contentHost";

        secProductPanel.BackColor = Color.White;
        secProductPanel.BorderStyle = BorderStyle.FixedSingle;
        secProductPanel.Location = new Point(0, 0);
        secProductPanel.Size = new Size(900, 168);
        secProductPanel.Name = "secProductPanel";

        secProduct.Text = "PRODUCT  &  CUSTOMER";
        secProduct.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        secProduct.ForeColor = Color.FromArgb(59, 122, 143);
        secProduct.Location = new Point(20, 14);
        secProduct.AutoSize = true;
        secProduct.Name = "secProduct";

        lblProduct.Text = "Product *";
        lblProduct.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblProduct.Location = new Point(20, 40);
        lblProduct.AutoSize = true;
        lblProduct.Name = "lblProduct";

        cboProduct.DropDownStyle = ComboBoxStyle.DropDownList;
        cboProduct.FlatStyle = FlatStyle.Flat;
        cboProduct.Font = new Font("Segoe UI", 10F);
        cboProduct.Items.AddRange(new object[] { "— Select product —" });
        cboProduct.Location = new Point(20, 62);
        cboProduct.Size = new Size(860, 28);
        cboProduct.Name = "cboProduct";
        cboProduct.TabIndex = 0;
        cboProduct.SelectedIndexChanged += CboProduct_SelectedIndexChanged;

        helpProduct.Text = "Only active products are listed.";
        helpProduct.Font = new Font("Segoe UI", 8.25F);
        helpProduct.ForeColor = Color.FromArgb(107, 119, 133);
        helpProduct.Location = new Point(20, 92);
        helpProduct.AutoSize = true;
        helpProduct.Name = "helpProduct";

        lblCustomer.Text = "Customer (optional)";
        lblCustomer.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblCustomer.Location = new Point(20, 112);
        lblCustomer.AutoSize = true;
        lblCustomer.Name = "lblCustomer";

        cboCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCustomer.FlatStyle = FlatStyle.Flat;
        cboCustomer.Font = new Font("Segoe UI", 10F);
        cboCustomer.Items.AddRange(new object[] { "— None (optional) —" });
        cboCustomer.Location = new Point(20, 132);
        cboCustomer.Size = new Size(860, 28);
        cboCustomer.Name = "cboCustomer";
        cboCustomer.TabIndex = 1;

        helpCustomer.Text = "Optional — leave empty if no customer is associated.";
        helpCustomer.Font = new Font("Segoe UI", 8.25F);
        helpCustomer.ForeColor = Color.FromArgb(107, 119, 133);
        helpCustomer.Location = new Point(480, 112);
        helpCustomer.AutoSize = true;
        helpCustomer.Name = "helpCustomer";

        secProductPanel.Controls.Add(secProduct);
        secProductPanel.Controls.Add(lblProduct);
        secProductPanel.Controls.Add(cboProduct);
        secProductPanel.Controls.Add(helpProduct);
        secProductPanel.Controls.Add(lblCustomer);
        secProductPanel.Controls.Add(cboCustomer);
        secProductPanel.Controls.Add(helpCustomer);

        secMovementPanel.BackColor = Color.White;
        secMovementPanel.BorderStyle = BorderStyle.FixedSingle;
        secMovementPanel.Location = new Point(0, 180);
        secMovementPanel.Size = new Size(900, 120);
        secMovementPanel.Name = "secMovementPanel";

        secMovement.Text = "MOVEMENT DETAILS";
        secMovement.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        secMovement.ForeColor = Color.FromArgb(59, 122, 143);
        secMovement.Location = new Point(20, 12);
        secMovement.AutoSize = true;
        secMovement.Name = "secMovement";

        lblQuantity.Text = "Quantity *";
        lblQuantity.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblQuantity.Location = new Point(20, 40);
        lblQuantity.AutoSize = true;
        lblQuantity.Name = "lblQuantity";

        nudQuantity.Font = new Font("Segoe UI", 10F);
        nudQuantity.Location = new Point(20, 62);
        nudQuantity.Minimum = 1;
        nudQuantity.Maximum = 1000000;
        nudQuantity.Value = 1;
        nudQuantity.Size = new Size(140, 28);
        nudQuantity.Name = "nudQuantity";
        nudQuantity.TabIndex = 2;
        nudQuantity.ValueChanged += NudQuantity_ValueChanged;

        lblDate.Text = "Date *";
        lblDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblDate.Location = new Point(190, 40);
        lblDate.AutoSize = true;
        lblDate.Name = "lblDate";

        dtpDate.Font = new Font("Segoe UI", 10F);
        dtpDate.Format = DateTimePickerFormat.Short;
        dtpDate.Location = new Point(190, 62);
        dtpDate.Size = new Size(160, 28);
        dtpDate.Name = "dtpDate";
        dtpDate.TabIndex = 3;

        lblUnitPrice.Text = "Selling price *";
        lblUnitPrice.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblUnitPrice.Location = new Point(380, 40);
        lblUnitPrice.AutoSize = true;
        lblUnitPrice.Name = "lblUnitPrice";

        txtUnitPrice.BorderStyle = BorderStyle.FixedSingle;
        txtUnitPrice.Font = new Font("Segoe UI", 10F);
        txtUnitPrice.Location = new Point(380, 62);
        txtUnitPrice.Size = new Size(140, 28);
        txtUnitPrice.Name = "txtUnitPrice";
        txtUnitPrice.TabIndex = 4;
        txtUnitPrice.Text = "0.00";

        previewBanner.Location = new Point(20, 96);
        previewBanner.Size = new Size(860, 18);
        previewBanner.BackColor = Color.FromArgb(251, 234, 232);
        previewBanner.Name = "previewBanner";

        lblPreview.Dock = DockStyle.Fill;
        lblPreview.Font = new Font("Segoe UI", 9F);
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

        secNotesPanel.BackColor = Color.White;
        secNotesPanel.BorderStyle = BorderStyle.FixedSingle;
        secNotesPanel.Location = new Point(0, 312);
        secNotesPanel.Size = new Size(900, 110);
        secNotesPanel.Name = "secNotesPanel";

        secNotes.Text = "NOTES";
        secNotes.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        secNotes.ForeColor = Color.FromArgb(59, 122, 143);
        secNotes.Location = new Point(20, 12);
        secNotes.AutoSize = true;
        secNotes.Name = "secNotes";

        lblNotes.Text = "Notes";
        lblNotes.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblNotes.Location = new Point(20, 36);
        lblNotes.AutoSize = true;
        lblNotes.Name = "lblNotes";

        txtNotes.BorderStyle = BorderStyle.FixedSingle;
        txtNotes.Font = new Font("Segoe UI", 10F);
        txtNotes.Location = new Point(20, 56);
        txtNotes.Multiline = true;
        txtNotes.MaxLength = 500;
        txtNotes.ScrollBars = ScrollBars.Vertical;
        txtNotes.Size = new Size(860, 42);
        txtNotes.Name = "txtNotes";
        txtNotes.TabIndex = 5;
        txtNotes.PlaceholderText = "Optional — e.g. invoice ref, destination";

        secNotesPanel.Controls.Add(secNotes);
        secNotesPanel.Controls.Add(lblNotes);
        secNotesPanel.Controls.Add(txtNotes);

        contentHost.Controls.Add(secNotesPanel);
        contentHost.Controls.Add(secMovementPanel);
        contentHost.Controls.Add(secProductPanel);

        Controls.Add(contentHost);
        Controls.Add(actionsBar);

        actionsBar.ResumeLayout(false);
        contentHost.ResumeLayout(false);
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
