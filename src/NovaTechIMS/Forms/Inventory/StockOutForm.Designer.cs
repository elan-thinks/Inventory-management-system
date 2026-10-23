using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>Stock-Out — inputs styled directly (no wrapper panels).</summary>
partial class StockOutForm
{
    private System.ComponentModel.IContainer components = null;

    private Panel formCard;
    private Panel headerPanel;
    private Label lblFormTitle;
    private Label lblFormSub;

    private Label lblProduct;
    private ComboBox cboProduct;
    private Label helpProduct;

    private Label lblCustomer;
    private ComboBox cboCustomer;
    private Label helpCustomer;

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

        lblProduct = new Label();
        cboProduct = new ComboBox();
        helpProduct = new Label();

        lblCustomer = new Label();
        cboCustomer = new ComboBox();
        helpCustomer = new Label();

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

        contentHost = new Panel();
        secProductPanel = new Panel();
        secMovementPanel = new Panel();
        secNotesPanel = new Panel();
        secProduct = new Label();
        secMovement = new Label();
        secNotes = new Label();

        formCard.SuspendLayout();
        headerPanel.SuspendLayout();
        previewBanner.SuspendLayout();
        actionsBar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudQuantity).BeginInit();
        SuspendLayout();

        var pageBg = Color.FromArgb(246, 248, 250);
        var accent = Color.FromArgb(15, 118, 70);
        var textMain = Color.FromArgb(28, 35, 43);
        var textMuted = Color.FromArgb(120, 130, 145);
        var borderSoft = Color.FromArgb(210, 216, 222);

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        BackColor = pageBg;
        ClientSize = new Size(960, 640);
        FormBorderStyle = FormBorderStyle.None;
        Name = "StockOutForm";
        Text = "Stock Out";
        Padding = new Padding(20, 12, 20, 12);

        formCard.Dock = DockStyle.Fill;
        formCard.BackColor = Color.White;
        formCard.BorderStyle = BorderStyle.None;
        formCard.Name = "formCard";

        headerPanel.Dock = DockStyle.Top;
        headerPanel.Height = 64;
        headerPanel.BackColor = Color.White;
        headerPanel.Name = "headerPanel";

        lblFormTitle.AutoSize = true;
        lblFormTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblFormTitle.ForeColor = textMain;
        lblFormTitle.Location = new Point(28, 14);
        lblFormTitle.Name = "lblFormTitle";
        lblFormTitle.Text = "Record Stock-Out";

        lblFormSub.AutoSize = true;
        lblFormSub.Font = new Font("Segoe UI", 9F);
        lblFormSub.ForeColor = textMuted;
        lblFormSub.Location = new Point(30, 40);
        lblFormSub.Name = "lblFormSub";
        lblFormSub.Text = "Issue goods from inventory to a customer or internal use.";

        headerPanel.Controls.Add(lblFormTitle);
        headerPanel.Controls.Add(lblFormSub);

        actionsBar.Dock = DockStyle.Bottom;
        actionsBar.Height = 60;
        actionsBar.BackColor = Color.White;
        actionsBar.Name = "actionsBar";

        lblError.AutoSize = false;
        lblError.Location = new Point(28, 16);
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
        btnClear.Location = new Point(620, 10);
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
        btnSave.Location = new Point(742, 10);
        btnSave.Text = "  Record Stock-Out";
        btnSave.Name = "btnSave";
        btnSave.Cursor = Cursors.Hand;
        btnSave.TabIndex = 7;
        btnSave.UseVisualStyleBackColor = false;
        btnSave.Click += BtnSave_Click;

        actionsBar.Controls.Add(lblError);
        actionsBar.Controls.Add(btnClear);
        actionsBar.Controls.Add(btnSave);

        lblProduct.Text = "Product *";
        lblProduct.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblProduct.ForeColor = textMain;
        lblProduct.AutoSize = true;
        lblProduct.Location = new Point(28, 80);
        lblProduct.Name = "lblProduct";

        cboProduct.DropDownStyle = ComboBoxStyle.DropDownList;
        cboProduct.FlatStyle = FlatStyle.Standard;
        cboProduct.Font = new Font("Segoe UI", 10F);
        cboProduct.Location = new Point(28, 104);
        cboProduct.Size = new Size(420, 28);
        cboProduct.Items.AddRange(new object[] { "— Select product —" });
        cboProduct.Name = "cboProduct";
        cboProduct.TabIndex = 0;
        cboProduct.SelectedIndexChanged += CboProduct_SelectedIndexChanged;

        helpProduct.Text = "Only active products with stock are listed.";
        helpProduct.Font = new Font("Segoe UI", 8F);
        helpProduct.ForeColor = textMuted;
        helpProduct.AutoSize = true;
        helpProduct.Location = new Point(28, 136);
        helpProduct.Name = "helpProduct";

        lblCustomer.Text = "Customer (optional)";
        lblCustomer.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblCustomer.ForeColor = textMain;
        lblCustomer.AutoSize = true;
        lblCustomer.Location = new Point(468, 80);
        lblCustomer.Name = "lblCustomer";

        cboCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCustomer.FlatStyle = FlatStyle.Standard;
        cboCustomer.Font = new Font("Segoe UI", 10F);
        cboCustomer.Location = new Point(468, 104);
        cboCustomer.Size = new Size(420, 28);
        cboCustomer.Items.AddRange(new object[] { "— None (optional) —" });
        cboCustomer.Name = "cboCustomer";
        cboCustomer.TabIndex = 1;

        helpCustomer.Text = "Leave empty for internal issue.";
        helpCustomer.Font = new Font("Segoe UI", 8F);
        helpCustomer.ForeColor = textMuted;
        helpCustomer.AutoSize = true;
        helpCustomer.Location = new Point(468, 136);
        helpCustomer.Name = "helpCustomer";

        lblQuantity.Text = "Quantity *";
        lblQuantity.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblQuantity.ForeColor = textMain;
        lblQuantity.AutoSize = true;
        lblQuantity.Location = new Point(28, 172);
        lblQuantity.Name = "lblQuantity";

        nudQuantity.Font = new Font("Segoe UI", 10F);
        nudQuantity.Location = new Point(28, 196);
        nudQuantity.Size = new Size(200, 28);
        nudQuantity.Minimum = 1;
        nudQuantity.Maximum = 1000000;
        nudQuantity.Value = 1;
        nudQuantity.BorderStyle = BorderStyle.FixedSingle;
        nudQuantity.Name = "nudQuantity";
        nudQuantity.TabIndex = 2;
        nudQuantity.ValueChanged += NudQuantity_ValueChanged;

        lblDate.Text = "Date *";
        lblDate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblDate.ForeColor = textMain;
        lblDate.AutoSize = true;
        lblDate.Location = new Point(248, 172);
        lblDate.Name = "lblDate";

        dtpDate.Font = new Font("Segoe UI", 10F);
        dtpDate.Format = DateTimePickerFormat.Short;
        dtpDate.Location = new Point(248, 196);
        dtpDate.Size = new Size(220, 28);
        dtpDate.Name = "dtpDate";
        dtpDate.TabIndex = 3;

        lblUnitPrice.Text = "Selling price *";
        lblUnitPrice.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblUnitPrice.ForeColor = textMain;
        lblUnitPrice.AutoSize = true;
        lblUnitPrice.Location = new Point(488, 172);
        lblUnitPrice.Name = "lblUnitPrice";

        txtUnitPrice.BorderStyle = BorderStyle.FixedSingle;
        txtUnitPrice.Font = new Font("Segoe UI", 10F);
        txtUnitPrice.Location = new Point(488, 196);
        txtUnitPrice.Size = new Size(200, 28);
        txtUnitPrice.Name = "txtUnitPrice";
        txtUnitPrice.TabIndex = 4;
        txtUnitPrice.Text = "0.00";

        previewBanner.Location = new Point(28, 244);
        previewBanner.Size = new Size(860, 36);
        previewBanner.BackColor = Color.FromArgb(251, 234, 232);
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

        lblNotes.Text = "Notes";
        lblNotes.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblNotes.ForeColor = textMain;
        lblNotes.AutoSize = true;
        lblNotes.Location = new Point(28, 296);
        lblNotes.Name = "lblNotes";

        helpNotes.Text = "Optional — reason for issue, reference number";
        helpNotes.Font = new Font("Segoe UI", 8F);
        helpNotes.ForeColor = textMuted;
        helpNotes.AutoSize = true;
        helpNotes.Location = new Point(28, 316);
        helpNotes.Name = "helpNotes";

        txtNotes.BorderStyle = BorderStyle.FixedSingle;
        txtNotes.Font = new Font("Segoe UI", 10F);
        txtNotes.Location = new Point(28, 338);
        txtNotes.Size = new Size(860, 100);
        txtNotes.Multiline = true;
        txtNotes.MaxLength = 500;
        txtNotes.ScrollBars = ScrollBars.Vertical;
        txtNotes.Name = "txtNotes";
        txtNotes.TabIndex = 5;
        txtNotes.PlaceholderText = "Describe details here…";

        formCard.Controls.Add(headerPanel);
        formCard.Controls.Add(lblProduct);
        formCard.Controls.Add(cboProduct);
        formCard.Controls.Add(helpProduct);
        formCard.Controls.Add(lblCustomer);
        formCard.Controls.Add(cboCustomer);
        formCard.Controls.Add(helpCustomer);
        formCard.Controls.Add(lblQuantity);
        formCard.Controls.Add(nudQuantity);
        formCard.Controls.Add(lblDate);
        formCard.Controls.Add(dtpDate);
        formCard.Controls.Add(lblUnitPrice);
        formCard.Controls.Add(txtUnitPrice);
        formCard.Controls.Add(previewBanner);
        formCard.Controls.Add(lblCurrentQty);
        formCard.Controls.Add(lblNotes);
        formCard.Controls.Add(helpNotes);
        formCard.Controls.Add(txtNotes);
        formCard.Controls.Add(actionsBar);

        contentHost.Visible = false;
        secProductPanel.Visible = false;
        secMovementPanel.Visible = false;
        secNotesPanel.Visible = false;

        Controls.Add(formCard);

        formCard.ResumeLayout(false);
        formCard.PerformLayout();
        headerPanel.ResumeLayout(false);
        headerPanel.PerformLayout();
        previewBanner.ResumeLayout(false);
        actionsBar.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)nudQuantity).EndInit();
        ResumeLayout(false);
    }
}
