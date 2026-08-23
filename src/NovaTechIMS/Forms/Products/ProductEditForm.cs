using System;
using System.Globalization;
using System.Windows.Forms;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Products;

/// <summary>
/// SCR-005 Product Add/Edit modal — Designer-based (partial).
/// Quantity on hand is display-only; starts at 0 on create.
/// </summary>
public partial class ProductEditForm : Form
{
    private readonly ProductService _productService = new();
    private readonly CategoryService _categoryService = new();
    private readonly SupplierService _supplierService = new();
    private readonly int? _productId;

    public ProductEditForm(int? productId = null)
    {
        _productId = productId;
        InitializeComponent();
        ApplyRuntimeStyling();
        LoadLookups();

        if (_productId is int id)
        {
            Text = "Edit Product";
            LoadExisting(id);
        }
        else
        {
            Text = "Add Product";
            lblQtyValue.Text = "0 (starts at zero)";
            chkActive.Checked = true;
            chkActive.Visible = false;
            chkActive.Enabled = false;
        }
    }

    private void ApplyRuntimeStyling()
    {
        UiTheme.StyleTextBox(txtName);
        UiTheme.StyleTextBox(txtDescription);
        UiTheme.StyleTextBox(txtPurchase);
        UiTheme.StyleTextBox(txtSelling);
        UiTheme.StyleTextBox(txtMinStock);
        UiTheme.StyleComboBox(cboCategory);
        UiTheme.StyleComboBox(cboSupplier);
        UiTheme.StyleButton(btnSave, UiTheme.ButtonKind.Primary);
        UiTheme.StyleButton(btnCancel, UiTheme.ButtonKind.Secondary);
    }

    private void BtnCancel_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void LoadLookups()
    {
        cboCategory.Items.Clear();
        cboCategory.DisplayMember = "Text";
        cboCategory.ValueMember = "Value";

        var categories = _categoryService.GetList(isActiveFilter: true);
        cboCategory.Items.Add(new LookupItem(0, "— Select category —"));
        foreach (var c in categories)
            cboCategory.Items.Add(new LookupItem(c.CategoryID, c.CategoryName));
        cboCategory.SelectedIndex = 0;

        cboSupplier.Items.Clear();
        cboSupplier.DisplayMember = "Text";
        cboSupplier.ValueMember = "Value";

        var suppliers = _supplierService.GetList(isActiveFilter: true);
        cboSupplier.Items.Add(new LookupItem(0, "— Select supplier —"));
        foreach (var s in suppliers)
            cboSupplier.Items.Add(new LookupItem(s.SupplierID, s.SupplierName));
        cboSupplier.SelectedIndex = 0;
    }

    private void LoadExisting(int id)
    {
        try
        {
            var p = _productService.GetById(id);
            txtName.Text = p.ProductName;
            txtDescription.Text = p.Description ?? string.Empty;
            txtPurchase.Text = p.PurchasePrice.ToString("0.00", CultureInfo.InvariantCulture);
            txtSelling.Text = p.SellingPrice.ToString("0.00", CultureInfo.InvariantCulture);
            txtMinStock.Text = p.MinimumStockLevel.ToString(CultureInfo.InvariantCulture);
            lblQtyValue.Text = p.QuantityOnHand.ToString(CultureInfo.InvariantCulture);
            chkActive.Checked = p.IsActive;
            chkActive.Visible = true;
            chkActive.Enabled = true;

            SelectLookup(cboCategory, p.CategoryID);
            SelectLookup(cboSupplier, p.SupplierID);
        }
        catch (AppException ex)
        {
            MessageBox.Show(this, ex.Message, "Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            DialogResult = DialogResult.Cancel;
        }
    }

    private static void SelectLookup(ComboBox combo, int id)
    {
        for (var i = 0; i < combo.Items.Count; i++)
        {
            if (combo.Items[i] is LookupItem item && item.Value == id)
            {
                combo.SelectedIndex = i;
                return;
            }
        }
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        lblError.Visible = false;
        btnSave.Enabled = false;

        try
        {
            if (cboCategory.SelectedItem is not LookupItem cat || cat.Value <= 0)
                throw new ValidationException("Category is required.");

            if (cboSupplier.SelectedItem is not LookupItem sup || sup.Value <= 0)
                throw new ValidationException("Default supplier is required.");

            if (!decimal.TryParse(txtPurchase.Text.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out var purchase)
                && !decimal.TryParse(txtPurchase.Text.Trim(), out purchase))
                throw new ValidationException("Purchase price must be a valid number.");

            if (!decimal.TryParse(txtSelling.Text.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out var selling)
                && !decimal.TryParse(txtSelling.Text.Trim(), out selling))
                throw new ValidationException("Selling price must be a valid number.");

            if (!int.TryParse(txtMinStock.Text.Trim(), out var minStock))
                throw new ValidationException("Minimum stock level must be a whole number.");

            if (_productId is int id)
            {
                _productService.Update(
                    id,
                    txtName.Text,
                    cat.Value,
                    sup.Value,
                    txtDescription.Text,
                    purchase,
                    selling,
                    minStock,
                    chkActive.Checked);
            }
            else
            {
                _productService.Create(
                    txtName.Text,
                    cat.Value,
                    sup.Value,
                    txtDescription.Text,
                    purchase,
                    selling,
                    minStock);
            }

            DialogResult = DialogResult.OK;
        }
        catch (ValidationException ex)
        {
            ShowError(ex.Message);
        }
        catch (AppException ex)
        {
            ShowError(ex.Message);
        }
        catch (Exception ex)
        {
            ShowError("Could not save product. " + ex.Message);
        }
        finally
        {
            btnSave.Enabled = true;
        }
    }

    private void ShowError(string message)
    {
        lblError.Text = message;
        lblError.Visible = true;
    }

    private sealed class LookupItem
    {
        public int Value { get; }
        public string Text { get; }
        public LookupItem(int value, string text)
        {
            Value = value;
            Text = text;
        }
        public override string ToString() => Text;
    }
}
