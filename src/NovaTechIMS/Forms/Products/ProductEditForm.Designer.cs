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

        const int lx = 24;
        const int fw = 420;

        AutoScaleMode = AutoScaleMode.Font;
        Font = UiTheme.Body;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        BackColor = UiTheme.Surface;
        ClientSize = new Size(lx * 2 + fw, 560);
        Name = "ProductEditForm";
        Text = "Product";

        int y = 16;

        lblName.Text = "Product name *";
        lblName.Font = UiTheme.Label;
        lblName.ForeColor = UiTheme.Text;
        lblName.Location = new Point(lx, y);
        lblName.AutoSize = true;
        lblName.Name = "lblName";
        y += 18;

        txtName.BorderStyle = BorderStyle.FixedSingle;
        txtName.Location = new Point(lx, y);
        txtName.Width = fw;
        txtName.MaxLength = 100;
        txtName.Name = "txtName";
        txtName.TabIndex = 0;
        y += 30;

        lblCategory.Text = "Category *";
        lblCategory.Font = UiTheme.Label;
        lblCategory.ForeColor = UiTheme.Text;
        lblCategory.Location = new Point(lx, y);
        lblCategory.AutoSize = true;
        lblCategory.Name = "lblCategory";
        y += 18;

        cboCategory.FlatStyle = FlatStyle.Flat;
        cboCategory.Location = new Point(lx, y);
        cboCategory.Width = fw;
        cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCategory.Name = "cboCategory";
        cboCategory.TabIndex = 1;
        y += 30;

        lblSupplier.Text = "Default supplier *";
        lblSupplier.Font = UiTheme.Label;
        lblSupplier.ForeColor = UiTheme.Text;
        lblSupplier.Location = new Point(lx, y);
        lblSupplier.AutoSize = true;
        lblSupplier.Name = "lblSupplier";
        y += 18;

        cboSupplier.FlatStyle = FlatStyle.Flat;
        cboSupplier.Location = new Point(lx, y);
        cboSupplier.Width = fw;
        cboSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
        cboSupplier.Name = "cboSupplier";
        cboSupplier.TabIndex = 2;
        y += 30;

        lblDescription.Text = "Description";
        lblDescription.Font = UiTheme.Label;
        lblDescription.ForeColor = UiTheme.Text;
        lblDescription.Location = new Point(lx, y);
        lblDescription.AutoSize = true;
        lblDescription.Name = "lblDescription";
        y += 18;

        txtDescription.BorderStyle = BorderStyle.FixedSingle;
        txtDescription.Location = new Point(lx, y);
        txtDescription.Width = fw;
        txtDescription.MaxLength = 500;
        txtDescription.Multiline = true;
        txtDescription.Height = 52;
        txtDescription.ScrollBars = ScrollBars.Vertical;
        txtDescription.Name = "txtDescription";
        txtDescription.TabIndex = 3;
        y += 58;

        lblPurchase.Text = "Purchase price *";
        lblPurchase.Font = UiTheme.Label;
        lblPurchase.ForeColor = UiTheme.Text;
        lblPurchase.Location = new Point(lx, y);
        lblPurchase.AutoSize = true;
        lblPurchase.Name = "lblPurchase";
        y += 18;

        txtPurchase.BorderStyle = BorderStyle.FixedSingle;
        txtPurchase.Location = new Point(lx, y);
        txtPurchase.Width = fw;
        txtPurchase.MaxLength = 20;
        txtPurchase.Name = "txtPurchase";
        txtPurchase.TabIndex = 4;
        y += 30;

        lblSelling.Text = "Selling price *";
        lblSelling.Font = UiTheme.Label;
        lblSelling.ForeColor = UiTheme.Text;
        lblSelling.Location = new Point(lx, y);
        lblSelling.AutoSize = true;
        lblSelling.Name = "lblSelling";
        y += 18;

        txtSelling.BorderStyle = BorderStyle.FixedSingle;
        txtSelling.Location = new Point(lx, y);
        txtSelling.Width = fw;
        txtSelling.MaxLength = 20;
        txtSelling.Name = "txtSelling";
        txtSelling.TabIndex = 5;
        y += 30;

        lblMinStock.Text = "Minimum stock level *";
        lblMinStock.Font = UiTheme.Label;
        lblMinStock.ForeColor = UiTheme.Text;
        lblMinStock.Location = new Point(lx, y);
        lblMinStock.AutoSize = true;
        lblMinStock.Name = "lblMinStock";
        y += 18;

        txtMinStock.BorderStyle = BorderStyle.FixedSingle;
        txtMinStock.Location = new Point(lx, y);
        txtMinStock.Width = fw;
        txtMinStock.MaxLength = 10;
        txtMinStock.Name = "txtMinStock";
        txtMinStock.TabIndex = 6;
        y += 30;

        lblQty.Text = "Quantity on hand";
        lblQty.Font = UiTheme.Label;
        lblQty.ForeColor = UiTheme.Text;
        lblQty.Location = new Point(lx, y);
        lblQty.AutoSize = true;
        lblQty.Name = "lblQty";
        y += 18;

        lblQtyValue.Text = "0";
        lblQtyValue.Font = UiTheme.Body;
        lblQtyValue.ForeColor = UiTheme.TextMuted;
        lblQtyValue.Location = new Point(lx, y);
        lblQtyValue.AutoSize = true;
        lblQtyValue.Name = "lblQtyValue";
        y += 20;

        lblQtyHint.Text = "Adjust stock using Stock-In, Stock-Out, or Inventory Adjustment.";
        lblQtyHint.Font = UiTheme.Label;
        lblQtyHint.ForeColor = UiTheme.TextMuted;
        lblQtyHint.Location = new Point(lx, y);
        lblQtyHint.AutoSize = true;
        lblQtyHint.Name = "lblQtyHint";
        y += 24;

        chkActive.Text = "Active";
        chkActive.Font = UiTheme.Body;
        chkActive.Location = new Point(lx, y);
        chkActive.AutoSize = true;
        chkActive.Checked = true;
        chkActive.Name = "chkActive";
        chkActive.TabIndex = 7;
        y += 28;

        lblError.ForeColor = UiTheme.Error;
        lblError.Font = UiTheme.Label;
        lblError.Location = new Point(lx, y);
        lblError.Size = new Size(fw, 40);
        lblError.Visible = false;
        lblError.Name = "lblError";
        y += 44;

        btnSave.Text = "Save";
        btnSave.Size = new Size(100, 32);
        btnSave.Location = new Point(lx + fw - 208, y);
        btnSave.Name = "btnSave";
        btnSave.TabIndex = 8;
        btnSave.Click += BtnSave_Click;

        btnCancel.Text = "Cancel";
        btnCancel.Size = new Size(100, 32);
        btnCancel.Location = new Point(lx + fw - 100, y);
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

        ClientSize = new Size(lx * 2 + fw, y + 52);

        ResumeLayout(false);
        PerformLayout();
    }
}
