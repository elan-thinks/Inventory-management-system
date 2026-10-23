using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>Adjustment — full content, no recent grid, actions Dock=Bottom.</summary>
partial class AdjustmentForm
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

    private Panel secMovementPanel;
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

    private Panel secReasonPanel;
    private Label secReason;
    private Label lblReason;
    private TextBox txtReason;
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

        secMovementPanel = new Panel();
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

        secReasonPanel = new Panel();
        secReason = new Label();
        lblReason = new Label();
        txtReason = new TextBox();
        lblNotes = new Label();
        txtNotes = new TextBox();

        actionsBar.SuspendLayout();
        contentHost.SuspendLayout();
        secProductPanel.SuspendLayout();
        secMovementPanel.SuspendLayout();
        previewBanner.SuspendLayout();
        secReasonPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudNewQty).BeginInit();
        SuspendLayout();

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.FromArgb(244, 246, 248);
        ClientSize = new Size(960, 640);
        FormBorderStyle = FormBorderStyle.None;
        Name = "AdjustmentForm";
        Text = "Inventory Adjustment";
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
        btnClear.Location = new Point(660, 8);
        btnClear.Text = "Clear Form";
        btnClear.Name = "btnClear";
        btnClear.Cursor = Cursors.Hand;
        btnClear.TabIndex = 5;
        btnClear.Click += BtnClear_Click;

        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnSave.BackColor = Color.FromArgb(30, 75, 143);
        btnSave.ForeColor = Color.White;
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.Size = new Size(190, 38);
        btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnSave.Location = new Point(790, 8);
        btnSave.Text = "  Record Adjustment";
        btnSave.Name = "btnSave";
        btnSave.Cursor = Cursors.Hand;
        btnSave.TabIndex = 6;
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
        secProductPanel.Size = new Size(900, 110);
        secProductPanel.Name = "secProductPanel";

        secProduct.Text = "PRODUCT";
        secProduct.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        secProduct.ForeColor = Color.FromArgb(30, 75, 143);
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

        secProductPanel.Controls.Add(secProduct);
        secProductPanel.Controls.Add(lblProduct);
        secProductPanel.Controls.Add(cboProduct);
        secProductPanel.Controls.Add(helpProduct);

        secMovementPanel.BackColor = Color.White;
        secMovementPanel.BorderStyle = BorderStyle.FixedSingle;
        secMovementPanel.Location = new Point(0, 122);
        secMovementPanel.Size = new Size(900, 140);
        secMovementPanel.Name = "secMovementPanel";

        secMovement.Text = "ADJUSTMENT DETAILS";
        secMovement.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        secMovement.ForeColor = Color.FromArgb(30, 75, 143);
        secMovement.Location = new Point(20, 12);
        secMovement.AutoSize = true;
        secMovement.Name = "secMovement";

        lblPreviousCaption.Text = "Previous quantity (system)";
        lblPreviousCaption.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblPreviousCaption.Location = new Point(20, 40);
        lblPreviousCaption.AutoSize = true;
        lblPreviousCaption.Name = "lblPreviousCaption";

        lblPreviousQty.Text = "—";
        lblPreviousQty.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblPreviousQty.ForeColor = Color.FromArgb(107, 119, 133);
        lblPreviousQty.Location = new Point(20, 62);
        lblPreviousQty.AutoSize = true;
        lblPreviousQty.Name = "lblPreviousQty";

        lblNewQty.Text = "New quantity *";
        lblNewQty.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblNewQty.Location = new Point(220, 40);
        lblNewQty.AutoSize = true;
        lblNewQty.Name = "lblNewQty";

        nudNewQty.Font = new Font("Segoe UI", 10F);
        nudNewQty.Location = new Point(220, 62);
        nudNewQty.Minimum = 0;
        nudNewQty.Maximum = 10000000;
        nudNewQty.Value = 0;
        nudNewQty.Size = new Size(140, 28);
        nudNewQty.Name = "nudNewQty";
        nudNewQty.TabIndex = 1;
        nudNewQty.ValueChanged += NudNewQty_ValueChanged;

        lblDate.Text = "Date *";
        lblDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblDate.Location = new Point(390, 40);
        lblDate.AutoSize = true;
        lblDate.Name = "lblDate";

        dtpDate.Font = new Font("Segoe UI", 10F);
        dtpDate.Format = DateTimePickerFormat.Short;
        dtpDate.Location = new Point(390, 62);
        dtpDate.Size = new Size(160, 28);
        dtpDate.Name = "dtpDate";
        dtpDate.TabIndex = 2;

        lblDiffCaption.Text = "Difference";
        lblDiffCaption.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblDiffCaption.Location = new Point(580, 40);
        lblDiffCaption.AutoSize = true;
        lblDiffCaption.Name = "lblDiffCaption";

        lblDifference.Text = "—";
        lblDifference.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblDifference.Location = new Point(580, 62);
        lblDifference.AutoSize = true;
        lblDifference.Name = "lblDifference";

        previewBanner.Location = new Point(20, 100);
        previewBanner.Size = new Size(860, 28);
        previewBanner.BackColor = Color.FromArgb(231, 240, 247);
        previewBanner.Name = "previewBanner";

        lblPreview.Dock = DockStyle.Fill;
        lblPreview.Font = new Font("Segoe UI", 9F);
        lblPreview.Text = "  Select a product and set the new quantity.";
        lblPreview.TextAlign = ContentAlignment.MiddleLeft;
        lblPreview.Name = "lblPreview";
        previewBanner.Controls.Add(lblPreview);

        secMovementPanel.Controls.Add(secMovement);
        secMovementPanel.Controls.Add(lblPreviousCaption);
        secMovementPanel.Controls.Add(lblPreviousQty);
        secMovementPanel.Controls.Add(lblNewQty);
        secMovementPanel.Controls.Add(nudNewQty);
        secMovementPanel.Controls.Add(lblDate);
        secMovementPanel.Controls.Add(dtpDate);
        secMovementPanel.Controls.Add(lblDiffCaption);
        secMovementPanel.Controls.Add(lblDifference);
        secMovementPanel.Controls.Add(previewBanner);

        secReasonPanel.BackColor = Color.White;
        secReasonPanel.BorderStyle = BorderStyle.FixedSingle;
        secReasonPanel.Location = new Point(0, 274);
        secReasonPanel.Size = new Size(900, 140);
        secReasonPanel.Name = "secReasonPanel";

        secReason.Text = "REASON  &  NOTES";
        secReason.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        secReason.ForeColor = Color.FromArgb(30, 75, 143);
        secReason.Location = new Point(20, 12);
        secReason.AutoSize = true;
        secReason.Name = "secReason";

        lblReason.Text = "Reason *";
        lblReason.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblReason.Location = new Point(20, 40);
        lblReason.AutoSize = true;
        lblReason.Name = "lblReason";

        txtReason.BorderStyle = BorderStyle.FixedSingle;
        txtReason.Font = new Font("Segoe UI", 10F);
        txtReason.Location = new Point(20, 62);
        txtReason.MaxLength = 250;
        txtReason.Size = new Size(860, 28);
        txtReason.Name = "txtReason";
        txtReason.TabIndex = 3;
        txtReason.PlaceholderText = "Required — e.g. stock count variance, damage";

        lblNotes.Text = "Notes";
        lblNotes.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblNotes.Location = new Point(20, 96);
        lblNotes.AutoSize = true;
        lblNotes.Name = "lblNotes";

        txtNotes.BorderStyle = BorderStyle.FixedSingle;
        txtNotes.Font = new Font("Segoe UI", 10F);
        txtNotes.Location = new Point(20, 116);
        txtNotes.Multiline = true;
        txtNotes.MaxLength = 500;
        txtNotes.ScrollBars = ScrollBars.Vertical;
        txtNotes.Size = new Size(860, 18);
        txtNotes.Name = "txtNotes";
        txtNotes.TabIndex = 4;
        txtNotes.PlaceholderText = "Optional additional detail";

        secReasonPanel.Controls.Add(secReason);
        secReasonPanel.Controls.Add(lblReason);
        secReasonPanel.Controls.Add(txtReason);
        secReasonPanel.Controls.Add(lblNotes);
        secReasonPanel.Controls.Add(txtNotes);

        contentHost.Controls.Add(secReasonPanel);
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
        secReasonPanel.ResumeLayout(false);
        secReasonPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudNewQty).EndInit();
        ResumeLayout(false);
    }
}
