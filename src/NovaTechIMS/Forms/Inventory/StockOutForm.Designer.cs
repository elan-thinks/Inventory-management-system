using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>Stock-Out — soft field boxes, full panel (matches Stock-In).</summary>
partial class StockOutForm
{
    private System.ComponentModel.IContainer components = null;

    private Panel formCard;
    private Panel headerPanel;
    private Label lblFormTitle;
    private Label lblFormSub;

    private Label lblProduct;
    private Panel wrapProduct;
    private ComboBox cboProduct;
    private Label helpProduct;

    private Label lblCustomer;
    private Panel wrapCustomer;
    private ComboBox cboCustomer;
    private Label helpCustomer;

    private Label lblQuantity;
    private Panel wrapQty;
    private NumericUpDown nudQuantity;

    private Label lblDate;
    private Panel wrapDate;
    private DateTimePicker dtpDate;

    private Label lblUnitPrice;
    private Panel wrapPrice;
    private TextBox txtUnitPrice;

    private Panel previewBanner;
    private Label lblPreview;
    private Label lblCurrentQty;

    private Label lblNotes;
    private Label helpNotes;
    private Panel wrapNotes;
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
        wrapProduct = new Panel();
        cboProduct = new ComboBox();
        helpProduct = new Label();

        lblCustomer = new Label();
        wrapCustomer = new Panel();
        cboCustomer = new ComboBox();
        helpCustomer = new Label();

        lblQuantity = new Label();
        wrapQty = new Panel();
        nudQuantity = new NumericUpDown();

        lblDate = new Label();
        wrapDate = new Panel();
        dtpDate = new DateTimePicker();

        lblUnitPrice = new Label();
        wrapPrice = new Panel();
        txtUnitPrice = new TextBox();

        previewBanner = new Panel();
        lblPreview = new Label();
        lblCurrentQty = new Label();

        lblNotes = new Label();
        helpNotes = new Label();
        wrapNotes = new Panel();
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
        wrapProduct.SuspendLayout();
        wrapCustomer.SuspendLayout();
        wrapQty.SuspendLayout();
        wrapDate.SuspendLayout();
        wrapPrice.SuspendLayout();
        wrapNotes.SuspendLayout();
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

        wrapProduct.Location = new Point(28, 102);
        wrapProduct.Size = new Size(420, 40);
        wrapProduct.BackColor = Color.White;
        wrapProduct.Name = "wrapProduct";
        wrapProduct.Padding = new Padding(8, 6, 8, 6);

        cboProduct.DropDownStyle = ComboBoxStyle.DropDownList;
        cboProduct.FlatStyle = FlatStyle.Flat;
        cboProduct.Font = new Font("Segoe UI", 10F);
        cboProduct.Dock = DockStyle.Fill;
        cboProduct.Items.AddRange(new object[] { "— Select product —" });
        cboProduct.Name = "cboProduct";
        cboProduct.TabIndex = 0;
        cboProduct.SelectedIndexChanged += CboProduct_SelectedIndexChanged;
        wrapProduct.Controls.Add(cboProduct);

        helpProduct.Text = "Only active products with stock are listed.";
        helpProduct.Font = new Font("Segoe UI", 8F);
        helpProduct.ForeColor = textMuted;
        helpProduct.AutoSize = true;
        helpProduct.Location = new Point(28, 146);
        helpProduct.Name = "helpProduct";

        lblCustomer.Text = "Customer (optional)";
        lblCustomer.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblCustomer.ForeColor = textMain;
        lblCustomer.AutoSize = true;
        lblCustomer.Location = new Point(468, 80);
        lblCustomer.Name = "lblCustomer";

        wrapCustomer.Location = new Point(468, 102);
        wrapCustomer.Size = new Size(420, 40);
        wrapCustomer.BackColor = Color.White;
        wrapCustomer.Name = "wrapCustomer";
        wrapCustomer.Padding = new Padding(8, 6, 8, 6);

        cboCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCustomer.FlatStyle = FlatStyle.Flat;
        cboCustomer.Font = new Font("Segoe UI", 10F);
        cboCustomer.Dock = DockStyle.Fill;
        cboCustomer.Items.AddRange(new object[] { "— None (optional) —" });
        cboCustomer.Name = "cboCustomer";
        cboCustomer.TabIndex = 1;
        wrapCustomer.Controls.Add(cboCustomer);

        helpCustomer.Text = "Leave empty for internal issue.";
        helpCustomer.Font = new Font("Segoe UI", 8F);
        helpCustomer.ForeColor = textMuted;
        helpCustomer.AutoSize = true;
        helpCustomer.Location = new Point(468, 146);
        helpCustomer.Name = "helpCustomer";

        lblQuantity.Text = "Quantity *";
        lblQuantity.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblQuantity.ForeColor = textMain;
        lblQuantity.AutoSize = true;
        lblQuantity.Location = new Point(28, 180);
        lblQuantity.Name = "lblQuantity";

        wrapQty.Location = new Point(28, 202);
        wrapQty.Size = new Size(200, 40);
        wrapQty.BackColor = Color.White;
        wrapQty.Name = "wrapQty";
        wrapQty.Padding = new Padding(8, 6, 8, 6);

        nudQuantity.Font = new Font("Segoe UI", 10F);
        nudQuantity.Dock = DockStyle.Fill;
        nudQuantity.Minimum = 1;
        nudQuantity.Maximum = 1000000;
        nudQuantity.Value = 1;
        nudQuantity.BorderStyle = BorderStyle.None;
        nudQuantity.Name = "nudQuantity";
        nudQuantity.TabIndex = 2;
        nudQuantity.ValueChanged += NudQuantity_ValueChanged;
        wrapQty.Controls.Add(nudQuantity);

        lblDate.Text = "Date *";
        lblDate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblDate.ForeColor = textMain;
        lblDate.AutoSize = true;
        lblDate.Location = new Point(248, 180);
        lblDate.Name = "lblDate";

        wrapDate.Location = new Point(248, 202);
        wrapDate.Size = new Size(220, 40);
        wrapDate.BackColor = Color.White;
        wrapDate.Name = "wrapDate";
        wrapDate.Padding = new Padding(8, 6, 8, 6);

        dtpDate.Font = new Font("Segoe UI", 10F);
        dtpDate.Format = DateTimePickerFormat.Short;
        dtpDate.Dock = DockStyle.Fill;
        dtpDate.Name = "dtpDate";
        dtpDate.TabIndex = 3;
        wrapDate.Controls.Add(dtpDate);

        lblUnitPrice.Text = "Selling price *";
        lblUnitPrice.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblUnitPrice.ForeColor = textMain;
        lblUnitPrice.AutoSize = true;
        lblUnitPrice.Location = new Point(488, 180);
        lblUnitPrice.Name = "lblUnitPrice";

        wrapPrice.Location = new Point(488, 202);
        wrapPrice.Size = new Size(200, 40);
        wrapPrice.BackColor = Color.White;
        wrapPrice.Name = "wrapPrice";
        wrapPrice.Padding = new Padding(8, 6, 8, 6);

        txtUnitPrice.BorderStyle = BorderStyle.None;
        txtUnitPrice.Font = new Font("Segoe UI", 10F);
        txtUnitPrice.Dock = DockStyle.Fill;
        txtUnitPrice.Name = "txtUnitPrice";
        txtUnitPrice.TabIndex = 4;
        txtUnitPrice.Text = "0.00";
        wrapPrice.Controls.Add(txtUnitPrice);

        previewBanner.Location = new Point(28, 260);
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
        lblNotes.Location = new Point(28, 310);
        lblNotes.Name = "lblNotes";

        helpNotes.Text = "Optional — reason for issue, reference number";
        helpNotes.Font = new Font("Segoe UI", 8F);
        helpNotes.ForeColor = textMuted;
        helpNotes.AutoSize = true;
        helpNotes.Location = new Point(28, 330);
        helpNotes.Name = "helpNotes";

        wrapNotes.Location = new Point(28, 352);
        wrapNotes.Size = new Size(860, 100);
        wrapNotes.BackColor = Color.White;
        wrapNotes.Name = "wrapNotes";
        wrapNotes.Padding = new Padding(10, 8, 10, 8);

        txtNotes.BorderStyle = BorderStyle.None;
        txtNotes.Font = new Font("Segoe UI", 10F);
        txtNotes.Dock = DockStyle.Fill;
        txtNotes.Multiline = true;
        txtNotes.MaxLength = 500;
        txtNotes.ScrollBars = ScrollBars.Vertical;
        txtNotes.Name = "txtNotes";
        txtNotes.TabIndex = 5;
        txtNotes.PlaceholderText = "Describe details here…";
        wrapNotes.Controls.Add(txtNotes);

        formCard.Controls.Add(headerPanel);
        formCard.Controls.Add(lblProduct);
        formCard.Controls.Add(wrapProduct);
        formCard.Controls.Add(helpProduct);
        formCard.Controls.Add(lblCustomer);
        formCard.Controls.Add(wrapCustomer);
        formCard.Controls.Add(helpCustomer);
        formCard.Controls.Add(lblQuantity);
        formCard.Controls.Add(wrapQty);
        formCard.Controls.Add(lblDate);
        formCard.Controls.Add(wrapDate);
        formCard.Controls.Add(lblUnitPrice);
        formCard.Controls.Add(wrapPrice);
        formCard.Controls.Add(previewBanner);
        formCard.Controls.Add(lblCurrentQty);
        formCard.Controls.Add(lblNotes);
        formCard.Controls.Add(helpNotes);
        formCard.Controls.Add(wrapNotes);
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
        wrapProduct.ResumeLayout(false);
        wrapCustomer.ResumeLayout(false);
        wrapQty.ResumeLayout(false);
        wrapDate.ResumeLayout(false);
        wrapPrice.ResumeLayout(false);
        wrapNotes.ResumeLayout(false);
        previewBanner.ResumeLayout(false);
        actionsBar.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)nudQuantity).EndInit();
        ResumeLayout(false);
    }
}
