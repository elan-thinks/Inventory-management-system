using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>Inventory Adjustment — inputs styled directly.</summary>
partial class AdjustmentForm
{
    private System.ComponentModel.IContainer components = null;

    private Panel formCard;
    private Panel headerPanel;
    private Label lblFormTitle;
    private Label lblFormSub;

    private Label lblProduct;
    private ComboBox cboProduct;
    private Label helpProduct;

    private Label lblPreviousCaption;
    private Label lblPreviousQty;
    private Label lblNewQty;
    private NumericUpDown nudNewQty;
    private Label lblDate;
    private DateTimePicker dtpDate;
    private Label lblDiffCaption;
    private Label lblDifference;

    private Panel previewBanner;
    private Label lblPreview;

    private Label lblReason;
    private TextBox txtReason;
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
        cboProduct = new ComboBox();
        helpProduct = new Label();

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

        lblReason = new Label();
        txtReason = new TextBox();
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
        secReasonPanel = new Panel();
        secProduct = new Label();
        secMovement = new Label();
        secReason = new Label();

        formCard.SuspendLayout();
        headerPanel.SuspendLayout();
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

        cboProduct.DropDownStyle = ComboBoxStyle.DropDownList;
        cboProduct.FlatStyle = FlatStyle.Standard;
        cboProduct.Font = new Font("Segoe UI", 10F);
        cboProduct.Location = new Point(28, 104);
        cboProduct.Size = new Size(860, 28);
        cboProduct.Items.AddRange(new object[] { "— Select product —" });
        cboProduct.Name = "cboProduct";
        cboProduct.TabIndex = 0;
        cboProduct.SelectedIndexChanged += CboProduct_SelectedIndexChanged;

        helpProduct.Text = "Select the product whose on-hand quantity needs correction.";
        helpProduct.Font = new Font("Segoe UI", 8F);
        helpProduct.ForeColor = textMuted;
        helpProduct.AutoSize = true;
        helpProduct.Location = new Point(28, 136);
        helpProduct.Name = "helpProduct";

        lblPreviousCaption.Text = "Current on hand";
        lblPreviousCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblPreviousCaption.ForeColor = textMain;
        lblPreviousCaption.AutoSize = true;
        lblPreviousCaption.Location = new Point(28, 172);
        lblPreviousCaption.Name = "lblPreviousCaption";

        lblPreviousQty.Text = "—";
        lblPreviousQty.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblPreviousQty.ForeColor = textMuted;
        lblPreviousQty.AutoSize = true;
        lblPreviousQty.Location = new Point(28, 198);
        lblPreviousQty.Name = "lblPreviousQty";

        lblNewQty.Text = "New quantity *";
        lblNewQty.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblNewQty.ForeColor = textMain;
        lblNewQty.AutoSize = true;
        lblNewQty.Location = new Point(200, 172);
        lblNewQty.Name = "lblNewQty";

        nudNewQty.Font = new Font("Segoe UI", 10F);
        nudNewQty.Location = new Point(200, 196);
        nudNewQty.Size = new Size(160, 28);
        nudNewQty.Minimum = 0;
        nudNewQty.Maximum = 1000000;
        nudNewQty.Value = 0;
        nudNewQty.BorderStyle = BorderStyle.FixedSingle;
        nudNewQty.Name = "nudNewQty";
        nudNewQty.TabIndex = 1;
        nudNewQty.ValueChanged += NudNewQty_ValueChanged;

        lblDate.Text = "Date *";
        lblDate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblDate.ForeColor = textMain;
        lblDate.AutoSize = true;
        lblDate.Location = new Point(384, 172);
        lblDate.Name = "lblDate";

        dtpDate.Font = new Font("Segoe UI", 10F);
        dtpDate.Format = DateTimePickerFormat.Short;
        dtpDate.Location = new Point(384, 196);
        dtpDate.Size = new Size(180, 28);
        dtpDate.Name = "dtpDate";
        dtpDate.TabIndex = 2;

        lblDiffCaption.Text = "Difference";
        lblDiffCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblDiffCaption.ForeColor = textMain;
        lblDiffCaption.AutoSize = true;
        lblDiffCaption.Location = new Point(588, 172);
        lblDiffCaption.Name = "lblDiffCaption";

        lblDifference.Text = "0";
        lblDifference.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblDifference.ForeColor = textMuted;
        lblDifference.AutoSize = true;
        lblDifference.Location = new Point(588, 198);
        lblDifference.Name = "lblDifference";

        previewBanner.Location = new Point(28, 244);
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
        lblReason.Location = new Point(28, 296);
        lblReason.Name = "lblReason";

        txtReason.BorderStyle = BorderStyle.FixedSingle;
        txtReason.Font = new Font("Segoe UI", 10F);
        txtReason.Location = new Point(28, 318);
        txtReason.Size = new Size(860, 28);
        txtReason.Name = "txtReason";
        txtReason.TabIndex = 3;
        txtReason.PlaceholderText = "e.g. Stock count, damaged goods, audit correction";

        lblNotes.Text = "Notes";
        lblNotes.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblNotes.ForeColor = textMain;
        lblNotes.AutoSize = true;
        lblNotes.Location = new Point(28, 360);
        lblNotes.Name = "lblNotes";

        helpNotes.Text = "Optional extra detail";
        helpNotes.Font = new Font("Segoe UI", 8F);
        helpNotes.ForeColor = textMuted;
        helpNotes.AutoSize = true;
        helpNotes.Location = new Point(28, 380);
        helpNotes.Name = "helpNotes";

        txtNotes.BorderStyle = BorderStyle.FixedSingle;
        txtNotes.Font = new Font("Segoe UI", 10F);
        txtNotes.Location = new Point(28, 400);
        txtNotes.Size = new Size(860, 80);
        txtNotes.Multiline = true;
        txtNotes.MaxLength = 500;
        txtNotes.ScrollBars = ScrollBars.Vertical;
        txtNotes.Name = "txtNotes";
        txtNotes.TabIndex = 4;

        formCard.Controls.Add(headerPanel);
        formCard.Controls.Add(lblProduct);
        formCard.Controls.Add(cboProduct);
        formCard.Controls.Add(helpProduct);
        formCard.Controls.Add(lblPreviousCaption);
        formCard.Controls.Add(lblPreviousQty);
        formCard.Controls.Add(lblNewQty);
        formCard.Controls.Add(nudNewQty);
        formCard.Controls.Add(lblDate);
        formCard.Controls.Add(dtpDate);
        formCard.Controls.Add(lblDiffCaption);
        formCard.Controls.Add(lblDifference);
        formCard.Controls.Add(previewBanner);
        formCard.Controls.Add(lblReason);
        formCard.Controls.Add(txtReason);
        formCard.Controls.Add(lblNotes);
        formCard.Controls.Add(helpNotes);
        formCard.Controls.Add(txtNotes);
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
        previewBanner.ResumeLayout(false);
        actionsBar.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)nudNewQty).EndInit();
        ResumeLayout(false);
    }
}
