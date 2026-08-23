using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Utilities;

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

        // ProductEditForm
        AutoScaleMode = AutoScaleMode.Font;
        Font = UiTheme.Body;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        BackColor = UiTheme.Surface;
        ClientSize = new Size(468, 560);
        Name = "ProductEditForm";
        Text = "Product";

        // lblName
        lblName.Text = "Product name *";
        lblName.Font = UiTheme.Label;
        lblName.ForeColor = UiTheme.Text;
        lblName.Location = new Point(24, 16);
        lblName.AutoSize = true;
        lblName.Name = "lblName";

        // txtName
        txtName.BorderStyle = BorderStyle.FixedSingle;
        txtName.Location = new Point(24, 34);
        txtName.Size = new Size(420, 23);
        txtName.MaxLength = 100;
        txtName.Name = "txtName";
        txtName.TabIndex = 0;

        // lblCategory
        lblCategory.Text = "Category *";
        lblCategory.Font = UiTheme.Label;
        lblCategory.ForeColor = UiTheme.Text;
        lblCategory.Location = new Point(24, 64);
        lblCategory.AutoSize = true;
        lblCategory.Name = "lblCategory";

        // cboCategory
        cboCategory.FlatStyle = FlatStyle.Flat;
        cboCategory.Location = new Point(24, 82);
        cboCategory.Size = new Size(420, 23);
        cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCategory.Name = "cboCategory";
        cboCategory.TabIndex = 1;

        // lblSupplier
        lblSupplier.Text = "Default supplier *";
        lblSupplier.Font = UiTheme.Label;
        lblSupplier.ForeColor = UiTheme.Text;
        lblSupplier.Location = new Point(24, 112);
        lblSupplier.AutoSize = true;
        lblSupplier.Name = "lblSupplier";

        // cboSupplier
        cboSupplier.FlatStyle = FlatStyle.Flat;
        cboSupplier.Location = new Point(24, 130);
        cboSupplier.Size = new Size(420, 23);
        cboSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
        cboSupplier.Name = "cboSupplier";
        cboSupplier.TabIndex = 2;

        // lblDescription
        lblDescription.Text = "Description";
        lblDescription.Font = UiTheme.Label;
        lblDescription.ForeColor = UiTheme.Text;
        lblDescription.Location = new Point(24, 160);
        lblDescription.AutoSize = true;
        lblDescription.Name = "lblDescription";

        // txtDescription
        txtDescription.BorderStyle = BorderStyle.FixedSingle;
        txtDescription.Location = new Point(24, 178);
        txtDescription.Size = new Size(420, 52);
        txtDescription.MaxLength = 500;
        txtDescription.Multiline = true;
        txtDescription.ScrollBars = ScrollBars.Vertical;
        txtDescription.Name = "txtDescription";
        txtDescription.TabIndex = 3;

        // lblPurchase
        lblPurchase.Text = "Purchase price *";
        lblPurchase.Font = UiTheme.Label;
        lblPurchase.ForeColor = UiTheme.Text;
        lblPurchase.Location = new Point(24, 238);
        lblPurchase.AutoSize = true;
        lblPurchase.Name = "lblPurchase";

        // txtPurchase
        txtPurchase.BorderStyle = BorderStyle.FixedSingle;
        txtPurchase.Location = new Point(24, 256);
        txtPurchase.Size = new Size(420, 23);
        txtPurchase.MaxLength = 20;
        txtPurchase.Name = "txtPurchase";
        txtPurchase.TabIndex = 4;

        // lblSelling
        lblSelling.Text = "Selling price *";
        lblSelling.Font = UiTheme.Label;
        lblSelling.ForeColor = UiTheme.Text;
        lblSelling.Location = new Point(24, 286);
        lblSelling.AutoSize = true;
        lblSelling.Name = "lblSelling";

        // txtSelling
        txtSelling.BorderStyle = BorderStyle.FixedSingle;
        txtSelling.Location = new Point(24, 304);
        txtSelling.Size = new Size(420, 23);
        txtSelling.MaxLength = 20;
        txtSelling.Name = "txtSelling";
        txtSelling.TabIndex = 5;

        // lblMinStock
        lblMinStock.Text = "Minimum stock level *";
        lblMinStock.Font = UiTheme.Label;
        lblMinStock.ForeColor = UiTheme.Text;
        lblMinStock.Location = new Point(24, 334);
        lblMinStock.AutoSize = true;
        lblMinStock.Name = "lblMinStock";

        // txtMinStock
        txtMinStock.BorderStyle = BorderStyle.FixedSingle;
        txtMinStock.Location = new Point(24, 352);
        txtMinStock.Size = new Size(420, 23);
        txtMinStock.MaxLength = 10;
        txtMinStock.Name = "txtMinStock";
        txtMinStock.TabIndex = 6;

        // lblQty
        lblQty.Text = "Quantity on hand";
        lblQty.Font = UiTheme.Label;
        lblQty.ForeColor = UiTheme.Text;
        lblQty.Location = new Point(24, 382);
        lblQty.AutoSize = true;
        lblQty.Name = "lblQty";

        // lblQtyValue
        lblQtyValue.Text = "0";
        lblQtyValue.Font = UiTheme.Body;
        lblQtyValue.ForeColor = UiTheme.TextMuted;
        lblQtyValue.Location = new Point(24, 400);
        lblQtyValue.AutoSize = true;
        lblQtyValue.Name = "lblQtyValue";

        // lblQtyHint
        lblQtyHint.Text = "Adjust stock using Stock-In, Stock-Out, or Inventory Adjustment.";
        lblQtyHint.Font = UiTheme.Label;
        lblQtyHint.ForeColor = UiTheme.TextMuted;
        lblQtyHint.Location = new Point(24, 422);
        lblQtyHint.AutoSize = true;
        lblQtyHint.Name = "lblQtyHint";

        // chkActive
        chkActive.Text = "Active";
        chkActive.Font = UiTheme.Body;
        chkActive.Location = new Point(24, 448);
        chkActive.AutoSize = true;
        chkActive.Checked = true;
        chkActive.Name = "chkActive";
        chkActive.TabIndex = 7;

        // lblError
        lblError.ForeColor = UiTheme.Error;
        lblError.Font = UiTheme.Label;
        lblError.Location = new Point(24, 476);
        lblError.Size = new Size(420, 36);
        lblError.Visible = false;
        lblError.Name = "lblError";

        // btnSave
        btnSave.Text = "Save";
        btnSave.Size = new Size(100, 32);
        btnSave.Location = new Point(236, 520);
        btnSave.Name = "btnSave";
        btnSave.TabIndex = 8;
        btnSave.Click += BtnSave_Click;

        // btnCancel
        btnCancel.Text = "Cancel";
        btnCancel.Size = new Size(100, 32);
        btnCancel.Location = new Point(344, 520);
        btnCancel.Name = "btnCancel";
        btnCancel.TabIndex = 9;
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
