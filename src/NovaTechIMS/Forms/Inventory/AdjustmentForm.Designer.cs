using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Inventory;

partial class AdjustmentForm
{
    private System.ComponentModel.IContainer components = null;

    private Panel formPanel;
    private Label lblProduct;
    private ComboBox cboProduct;
    private Label lblPreviousCaption;
    private Label lblPreviousQty;
    private Label lblNewQty;
    private NumericUpDown nudNewQty;
    private Label lblDiffCaption;
    private Label lblDifference;
    private Label lblDate;
    private DateTimePicker dtpDate;
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
        formPanel = new Panel();
        lblProduct = new Label();
        cboProduct = new ComboBox();
        lblPreviousCaption = new Label();
        lblPreviousQty = new Label();
        lblNewQty = new Label();
        nudNewQty = new NumericUpDown();
        lblDiffCaption = new Label();
        lblDifference = new Label();
        lblDate = new Label();
        dtpDate = new DateTimePicker();
        lblReason = new Label();
        txtReason = new TextBox();
        lblNotes = new Label();
        txtNotes = new TextBox();
        lblError = new Label();
        btnSave = new Button();
        lblHint = new Label();
        grid = new DataGridView();
        formPanel.SuspendLayout();
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

        formPanel.Dock = DockStyle.Top;
        formPanel.Height = 300;
        formPanel.BackColor = Color.White;
        formPanel.Padding = new Padding(16);
        formPanel.Name = "formPanel";

        lblProduct.AutoSize = true;
        lblProduct.Location = new Point(16, 12);
        lblProduct.Name = "lblProduct";
        lblProduct.Text = "Product *";

        cboProduct.DropDownStyle = ComboBoxStyle.DropDownList;
        cboProduct.FlatStyle = FlatStyle.Flat;
        cboProduct.Location = new Point(16, 30);
        cboProduct.Name = "cboProduct";
        cboProduct.Size = new Size(300, 25);
        cboProduct.TabIndex = 0;
        cboProduct.SelectedIndexChanged += CboProduct_SelectedIndexChanged;

        lblPreviousCaption.AutoSize = true;
        lblPreviousCaption.Location = new Point(16, 62);
        lblPreviousCaption.Name = "lblPreviousCaption";
        lblPreviousCaption.Text = "Previous quantity (system)";

        lblPreviousQty.AutoSize = true;
        lblPreviousQty.Location = new Point(16, 80);
        lblPreviousQty.Name = "lblPreviousQty";
        lblPreviousQty.Text = "—";

        lblNewQty.AutoSize = true;
        lblNewQty.Location = new Point(16, 108);
        lblNewQty.Name = "lblNewQty";
        lblNewQty.Text = "New quantity *";

        nudNewQty.Location = new Point(16, 126);
        nudNewQty.Minimum = 0;
        nudNewQty.Maximum = 10000000;
        nudNewQty.Value = 0;
        nudNewQty.Name = "nudNewQty";
        nudNewQty.Size = new Size(120, 25);
        nudNewQty.TabIndex = 1;
        nudNewQty.ValueChanged += NudNewQty_ValueChanged;

        lblDiffCaption.AutoSize = true;
        lblDiffCaption.Location = new Point(16, 158);
        lblDiffCaption.Name = "lblDiffCaption";
        lblDiffCaption.Text = "Difference (New − Previous)";

        lblDifference.AutoSize = true;
        lblDifference.Location = new Point(16, 176);
        lblDifference.Name = "lblDifference";
        lblDifference.Text = "—";

        lblDate.AutoSize = true;
        lblDate.Location = new Point(356, 12);
        lblDate.Name = "lblDate";
        lblDate.Text = "Transaction date *";

        dtpDate.Format = DateTimePickerFormat.Short;
        dtpDate.Location = new Point(356, 30);
        dtpDate.Name = "dtpDate";
        dtpDate.Size = new Size(200, 25);
        dtpDate.TabIndex = 2;

        lblReason.AutoSize = true;
        lblReason.Location = new Point(356, 62);
        lblReason.Name = "lblReason";
        lblReason.Text = "Reason *";

        txtReason.BorderStyle = BorderStyle.FixedSingle;
        txtReason.Location = new Point(356, 80);
        txtReason.MaxLength = 250;
        txtReason.Name = "txtReason";
        txtReason.Size = new Size(280, 25);
        txtReason.TabIndex = 3;

        lblNotes.AutoSize = true;
        lblNotes.Location = new Point(356, 112);
        lblNotes.Name = "lblNotes";
        lblNotes.Text = "Notes";

        txtNotes.BorderStyle = BorderStyle.FixedSingle;
        txtNotes.Location = new Point(356, 130);
        txtNotes.Multiline = true;
        txtNotes.MaxLength = 500;
        txtNotes.ScrollBars = ScrollBars.Vertical;
        txtNotes.Name = "txtNotes";
        txtNotes.Size = new Size(280, 48);
        txtNotes.TabIndex = 4;

        lblError.Location = new Point(16, 210);
        lblError.Name = "lblError";
        lblError.Size = new Size(520, 28);
        lblError.Visible = false;

        btnSave.Location = new Point(476, 210);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(160, 34);
        btnSave.TabIndex = 5;
        btnSave.Text = "Record Adjustment";
        btnSave.UseVisualStyleBackColor = true;
        btnSave.Click += BtnSave_Click;

        formPanel.Controls.Add(lblProduct);
        formPanel.Controls.Add(cboProduct);
        formPanel.Controls.Add(lblPreviousCaption);
        formPanel.Controls.Add(lblPreviousQty);
        formPanel.Controls.Add(lblNewQty);
        formPanel.Controls.Add(nudNewQty);
        formPanel.Controls.Add(lblDiffCaption);
        formPanel.Controls.Add(lblDifference);
        formPanel.Controls.Add(lblDate);
        formPanel.Controls.Add(dtpDate);
        formPanel.Controls.Add(lblReason);
        formPanel.Controls.Add(txtReason);
        formPanel.Controls.Add(lblNotes);
        formPanel.Controls.Add(txtNotes);
        formPanel.Controls.Add(lblError);
        formPanel.Controls.Add(btnSave);

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
        grid.BorderStyle = BorderStyle.FixedSingle;
        grid.BackgroundColor = Color.White;
        grid.Name = "grid";
        grid.TabIndex = 6;

        Controls.Add(grid);
        Controls.Add(lblHint);
        Controls.Add(formPanel);

        formPanel.ResumeLayout(false);
        formPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudNewQty).EndInit();
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(false);
    }
}
