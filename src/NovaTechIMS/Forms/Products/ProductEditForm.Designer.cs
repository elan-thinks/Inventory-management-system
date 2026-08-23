using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Products;

partial class ProductEditForm
{
    private System.ComponentModel.IContainer components = null;

    private Label lblName;
    private TextBox txtName;
    private Label lblCategory;
    private ComboBox cboCategory;
    private Label lblSupplier;
    private ComboBox cboSupplier;
    private Label lblDescription;
    private TextBox txtDescription;
    private Label lblPurchase;
    private TextBox txtPurchase;
    private Label lblSelling;
    private TextBox txtSelling;
    private Label lblMinStock;
    private TextBox txtMinStock;
    private Label lblQty;
    private Label lblQtyValue;
    private Label lblQtyHint;
    private CheckBox chkActive;
    private Label lblError;
    private Button btnSave;
    private Button btnCancel;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        lblName = new Label();
        txtName = new TextBox();
        lblCategory = new Label();
        cboCategory = new ComboBox();
        lblSupplier = new Label();
        cboSupplier = new ComboBox();
        lblDescription = new Label();
        txtDescription = new TextBox();
        lblPurchase = new Label();
        txtPurchase = new TextBox();
        lblSelling = new Label();
        txtSelling = new TextBox();
        lblMinStock = new Label();
        txtMinStock = new TextBox();
        lblQty = new Label();
        lblQtyValue = new Label();
        lblQtyHint = new Label();
        chkActive = new CheckBox();
        lblError = new Label();
        btnSave = new Button();
        btnCancel = new Button();
        SuspendLayout();

        // ProductEditForm — no UiTheme here (Designer-safe BCL only)
        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        BackColor = Color.White;
        ClientSize = new Size(468, 560);
        Name = "ProductEditForm";
        Text = "Product";

        // lblName
        lblName.AutoSize = true;
        lblName.Location = new Point(24, 16);
        lblName.Name = "lblName";
        lblName.Text = "Product name *";

        // txtName
        txtName.BorderStyle = BorderStyle.FixedSingle;
        txtName.Location = new Point(24, 34);
        txtName.MaxLength = 100;
        txtName.Name = "txtName";
        txtName.Size = new Size(420, 25);
        txtName.TabIndex = 0;

        // lblCategory
        lblCategory.AutoSize = true;
        lblCategory.Location = new Point(24, 64);
        lblCategory.Name = "lblCategory";
        lblCategory.Text = "Category *";

        // cboCategory
        cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCategory.FlatStyle = FlatStyle.Flat;
        cboCategory.Location = new Point(24, 82);
        cboCategory.Name = "cboCategory";
        cboCategory.Size = new Size(420, 25);
        cboCategory.TabIndex = 1;

        // lblSupplier
        lblSupplier.AutoSize = true;
        lblSupplier.Location = new Point(24, 112);
        lblSupplier.Name = "lblSupplier";
        lblSupplier.Text = "Default supplier *";

        // cboSupplier
        cboSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
        cboSupplier.FlatStyle = FlatStyle.Flat;
        cboSupplier.Location = new Point(24, 130);
        cboSupplier.Name = "cboSupplier";
        cboSupplier.Size = new Size(420, 25);
        cboSupplier.TabIndex = 2;

        // lblDescription
        lblDescription.AutoSize = true;
        lblDescription.Location = new Point(24, 160);
        lblDescription.Name = "lblDescription";
        lblDescription.Text = "Description";

        // txtDescription
        txtDescription.BorderStyle = BorderStyle.FixedSingle;
        txtDescription.Location = new Point(24, 178);
        txtDescription.MaxLength = 500;
        txtDescription.Multiline = true;
        txtDescription.Name = "txtDescription";
        txtDescription.ScrollBars = ScrollBars.Vertical;
        txtDescription.Size = new Size(420, 52);
        txtDescription.TabIndex = 3;

        // lblPurchase
        lblPurchase.AutoSize = true;
        lblPurchase.Location = new Point(24, 238);
        lblPurchase.Name = "lblPurchase";
        lblPurchase.Text = "Purchase price *";

        // txtPurchase
        txtPurchase.BorderStyle = BorderStyle.FixedSingle;
        txtPurchase.Location = new Point(24, 256);
        txtPurchase.MaxLength = 20;
        txtPurchase.Name = "txtPurchase";
        txtPurchase.Size = new Size(420, 25);
        txtPurchase.TabIndex = 4;

        // lblSelling
        lblSelling.AutoSize = true;
        lblSelling.Location = new Point(24, 286);
        lblSelling.Name = "lblSelling";
        lblSelling.Text = "Selling price *";

        // txtSelling
        txtSelling.BorderStyle = BorderStyle.FixedSingle;
        txtSelling.Location = new Point(24, 304);
        txtSelling.MaxLength = 20;
        txtSelling.Name = "txtSelling";
        txtSelling.Size = new Size(420, 25);
        txtSelling.TabIndex = 5;

        // lblMinStock
        lblMinStock.AutoSize = true;
        lblMinStock.Location = new Point(24, 334);
        lblMinStock.Name = "lblMinStock";
        lblMinStock.Text = "Minimum stock level *";

        // txtMinStock
        txtMinStock.BorderStyle = BorderStyle.FixedSingle;
        txtMinStock.Location = new Point(24, 352);
        txtMinStock.MaxLength = 10;
        txtMinStock.Name = "txtMinStock";
        txtMinStock.Size = new Size(420, 25);
        txtMinStock.TabIndex = 6;

        // lblQty
        lblQty.AutoSize = true;
        lblQty.Location = new Point(24, 382);
        lblQty.Name = "lblQty";
        lblQty.Text = "Quantity on hand";

        // lblQtyValue
        lblQtyValue.AutoSize = true;
        lblQtyValue.Location = new Point(24, 400);
        lblQtyValue.Name = "lblQtyValue";
        lblQtyValue.Text = "0";

        // lblQtyHint
        lblQtyHint.AutoSize = true;
        lblQtyHint.Location = new Point(24, 422);
        lblQtyHint.Name = "lblQtyHint";
        lblQtyHint.Text = "Adjust stock using Stock-In, Stock-Out, or Inventory Adjustment.";

        // chkActive
        chkActive.AutoSize = true;
        chkActive.Checked = true;
        chkActive.CheckState = CheckState.Checked;
        chkActive.Location = new Point(24, 448);
        chkActive.Name = "chkActive";
        chkActive.TabIndex = 7;
        chkActive.Text = "Active";
        chkActive.UseVisualStyleBackColor = true;

        // lblError
        lblError.Location = new Point(24, 476);
        lblError.Name = "lblError";
        lblError.Size = new Size(420, 36);
        lblError.Visible = false;

        // btnSave
        btnSave.Location = new Point(236, 520);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(100, 32);
        btnSave.TabIndex = 8;
        btnSave.Text = "Save";
        btnSave.UseVisualStyleBackColor = true;
        btnSave.Click += BtnSave_Click;

        // btnCancel
        btnCancel.Location = new Point(344, 520);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(100, 32);
        btnCancel.TabIndex = 9;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += BtnCancel_Click;

        AcceptButton = btnSave;
        CancelButton = btnCancel;

        Controls.Add(lblName);
        Controls.Add(txtName);
        Controls.Add(lblCategory);
        Controls.Add(cboCategory);
        Controls.Add(lblSupplier);
        Controls.Add(cboSupplier);
        Controls.Add(lblDescription);
        Controls.Add(txtDescription);
        Controls.Add(lblPurchase);
        Controls.Add(txtPurchase);
        Controls.Add(lblSelling);
        Controls.Add(txtSelling);
        Controls.Add(lblMinStock);
        Controls.Add(txtMinStock);
        Controls.Add(lblQty);
        Controls.Add(lblQtyValue);
        Controls.Add(lblQtyHint);
        Controls.Add(chkActive);
        Controls.Add(lblError);
        Controls.Add(btnSave);
        Controls.Add(btnCancel);

        ResumeLayout(false);
        PerformLayout();
    }
}
