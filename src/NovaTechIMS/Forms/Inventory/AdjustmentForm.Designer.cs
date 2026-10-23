using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>Inventory Adjustment — soft field boxes, full panel.</summary>
partial class AdjustmentForm
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

    private Label lblPreviousCaption;
    private Label lblPreviousQty;
    private Label lblNewQty;
    private Panel wrapNewQty;
    private NumericUpDown nudNewQty;
    private Label lblDate;
    private Panel wrapDate;
    private DateTimePicker dtpDate;
    private Label lblDiffCaption;
    private Label lblDifference;

    private Panel previewBanner;
    private Label lblPreview;

    private Label lblReason;
    private Panel wrapReason;
    private TextBox txtReason;
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
    private Panel secReasonPanel;
    private Label secProduct;
    private Label secMovement;
    private Label secReason;

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

        lblPreviousCaption = new Label();
        lblPreviousQty = new Label();
        lblNewQty = new Label();
        wrapNewQty = new Panel();
        nudNewQty = new NumericUpDown();
        lblDate = new Label();
        wrapDate = new Panel();
        dtpDate = new DateTimePicker();
        lblDiffCaption = new Label();
        lblDifference = new Label();

        previewBanner = new Panel();
        lblPreview = new Label();

        lblReason = new Label();
        wrapReason = new Panel();
        txtReason = new TextBox();
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
        secReasonPanel = new Panel();
        secProduct = new Label();
        secMovement = new Label();
        secReason = new Label();

        formCard.SuspendLayout();
        headerPanel.SuspendLayout();
        wrapProduct.SuspendLayout();
        wrapNewQty.SuspendLayout();
        wrapDate.SuspendLayout();
        wrapReason.SuspendLayout();
        wrapNotes.SuspendLayout();
        previewBanner.SuspendLayout();
        actionsBar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudNewQty).BeginInit();
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
        Name = "AdjustmentForm";
        Text = "Inventory Adjustment";
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
        lblFormTitle.Text = "Inventory Adjustment";

        lblFormSub.AutoSize = true;
        lblFormSub.Font = new Font("Segoe UI", 9F);
        lblFormSub.ForeColor = textMuted;
        lblFormSub.Location = new Point(30, 40);
        lblFormSub.Name = "lblFormSub";
        lblFormSub.Text = "Correct on-hand quantity after count, damage, or audit.";

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
        btnSave.Text = "  Record Adjustment";
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
        wrapProduct.Size = new Size(860, 40);
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

        helpProduct.Text = "Select the product whose on-hand quantity needs correction.";
        helpProduct.Font = new Font("Segoe UI", 8F);
        helpProduct.ForeColor = textMuted;
        helpProduct.AutoSize = true;
        helpProduct.Location = new Point(28, 146);
        helpProduct.Name = "helpProduct";

        lblPreviousCaption.Text = "Current on hand";
        lblPreviousCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblPreviousCaption.ForeColor = textMain;
        lblPreviousCaption.AutoSize = true;
        lblPreviousCaption.Location = new Point(28, 180);
        lblPreviousCaption.Name = "lblPreviousCaption";

        lblPreviousQty.Text = "—";
        lblPreviousQty.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblPreviousQty.ForeColor = textMuted;
        lblPreviousQty.AutoSize = true;
        lblPreviousQty.Location = new Point(28, 206);
        lblPreviousQty.Name = "lblPreviousQty";

        lblNewQty.Text = "New quantity *";
        lblNewQty.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblNewQty.ForeColor = textMain;
        lblNewQty.AutoSize = true;
        lblNewQty.Location = new Point(200, 180);
        lblNewQty.Name = "lblNewQty";

        wrapNewQty.Location = new Point(200, 202);
        wrapNewQty.Size = new Size(180, 40);
        wrapNewQty.BackColor = Color.White;
        wrapNewQty.Name = "wrapNewQty";
        wrapNewQty.Padding = new Padding(8, 6, 8, 6);

        nudNewQty.Font = new Font("Segoe UI", 10F);
        nudNewQty.Dock = DockStyle.Fill;
        nudNewQty.Minimum = 0;
        nudNewQty.Maximum = 1000000;
        nudNewQty.Value = 0;
        nudNewQty.BorderStyle = BorderStyle.None;
        nudNewQty.Name = "nudNewQty";
        nudNewQty.TabIndex = 1;
        nudNewQty.ValueChanged += NudNewQty_ValueChanged;
        wrapNewQty.Controls.Add(nudNewQty);

        lblDate.Text = "Date *";
        lblDate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblDate.ForeColor = textMain;
        lblDate.AutoSize = true;
        lblDate.Location = new Point(400, 180);
        lblDate.Name = "lblDate";

        wrapDate.Location = new Point(400, 202);
        wrapDate.Size = new Size(200, 40);
        wrapDate.BackColor = Color.White;
        wrapDate.Name = "wrapDate";
        wrapDate.Padding = new Padding(8, 6, 8, 6);

        dtpDate.Font = new Font("Segoe UI", 10F);
        dtpDate.Format = DateTimePickerFormat.Short;
        dtpDate.Dock = DockStyle.Fill;
        dtpDate.Name = "dtpDate";
        dtpDate.TabIndex = 2;
        wrapDate.Controls.Add(dtpDate);

        lblDiffCaption.Text = "Difference";
        lblDiffCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblDiffCaption.ForeColor = textMain;
        lblDiffCaption.AutoSize = true;
        lblDiffCaption.Location = new Point(620, 180);
        lblDiffCaption.Name = "lblDiffCaption";

        lblDifference.Text = "0";
        lblDifference.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblDifference.ForeColor = textMuted;
        lblDifference.AutoSize = true;
        lblDifference.Location = new Point(620, 206);
        lblDifference.Name = "lblDifference";

        previewBanner.Location = new Point(28, 260);
        previewBanner.Size = new Size(860, 36);
        previewBanner.BackColor = Color.FromArgb(231, 240, 247);
        previewBanner.Name = "previewBanner";

        lblPreview.Dock = DockStyle.Fill;
        lblPreview.Font = new Font("Segoe UI", 9.5F);
        lblPreview.ForeColor = textMain;
        lblPreview.Text = "  Select a product and set the new quantity.";
        lblPreview.TextAlign = ContentAlignment.MiddleLeft;
        lblPreview.Name = "lblPreview";
        previewBanner.Controls.Add(lblPreview);

        lblReason.Text = "Reason *";
        lblReason.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblReason.ForeColor = textMain;
        lblReason.AutoSize = true;
        lblReason.Location = new Point(28, 310);
        lblReason.Name = "lblReason";

        wrapReason.Location = new Point(28, 332);
        wrapReason.Size = new Size(860, 40);
        wrapReason.BackColor = Color.White;
        wrapReason.Name = "wrapReason";
        wrapReason.Padding = new Padding(8, 6, 8, 6);

        txtReason.BorderStyle = BorderStyle.None;
        txtReason.Font = new Font("Segoe UI", 10F);
        txtReason.Dock = DockStyle.Fill;
        txtReason.Name = "txtReason";
        txtReason.TabIndex = 3;
        txtReason.PlaceholderText = "e.g. Stock count, damaged goods, audit correction";
        wrapReason.Controls.Add(txtReason);

        lblNotes.Text = "Notes";
        lblNotes.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblNotes.ForeColor = textMain;
        lblNotes.AutoSize = true;
        lblNotes.Location = new Point(28, 386);
        lblNotes.Name = "lblNotes";

        helpNotes.Text = "Optional extra detail";
        helpNotes.Font = new Font("Segoe UI", 8F);
        helpNotes.ForeColor = textMuted;
        helpNotes.AutoSize = true;
        helpNotes.Location = new Point(28, 406);
        helpNotes.Name = "helpNotes";

        wrapNotes.Location = new Point(28, 428);
        wrapNotes.Size = new Size(860, 80);
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
        txtNotes.TabIndex = 4;
        wrapNotes.Controls.Add(txtNotes);

        formCard.Controls.Add(headerPanel);
        formCard.Controls.Add(lblProduct);
        formCard.Controls.Add(wrapProduct);
        formCard.Controls.Add(helpProduct);
        formCard.Controls.Add(lblPreviousCaption);
        formCard.Controls.Add(lblPreviousQty);
        formCard.Controls.Add(lblNewQty);
        formCard.Controls.Add(wrapNewQty);
        formCard.Controls.Add(lblDate);
        formCard.Controls.Add(wrapDate);
        formCard.Controls.Add(lblDiffCaption);
        formCard.Controls.Add(lblDifference);
        formCard.Controls.Add(previewBanner);
        formCard.Controls.Add(lblReason);
        formCard.Controls.Add(wrapReason);
        formCard.Controls.Add(lblNotes);
        formCard.Controls.Add(helpNotes);
        formCard.Controls.Add(wrapNotes);
        formCard.Controls.Add(actionsBar);

        contentHost.Visible = false;
        secProductPanel.Visible = false;
        secMovementPanel.Visible = false;
        secReasonPanel.Visible = false;

        Controls.Add(formCard);

        formCard.ResumeLayout(false);
        formCard.PerformLayout();
        headerPanel.ResumeLayout(false);
        headerPanel.PerformLayout();
        wrapProduct.ResumeLayout(false);
        wrapNewQty.ResumeLayout(false);
        wrapDate.ResumeLayout(false);
        wrapReason.ResumeLayout(false);
        wrapNotes.ResumeLayout(false);
        previewBanner.ResumeLayout(false);
        actionsBar.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)nudNewQty).EndInit();
        ResumeLayout(false);
    }
}
