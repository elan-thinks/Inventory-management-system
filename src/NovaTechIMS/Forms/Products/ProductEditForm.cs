using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Products;

/// <summary>
/// SCR-005 Product Add/Edit modal (Milestone 8).
/// Quantity on hand is display-only; starts at 0 on create.
/// </summary>
public class ProductEditForm : Form
{
    private readonly ProductService _productService = new();
    private readonly CategoryService _categoryService = new();
    private readonly SupplierService _supplierService = new();
    private readonly int? _productId;

    private TextBox txtName = null!;
    private ComboBox cboCategory = null!;
    private ComboBox cboSupplier = null!;
    private TextBox txtDescription = null!;
    private TextBox txtPurchase = null!;
    private TextBox txtSelling = null!;
    private TextBox txtMinStock = null!;
    private Label lblQtyValue = null!;
    private CheckBox chkActive = null!;
    private Label lblError = null!;
    private Button btnSave = null!;
    private Button btnCancel = null!;

    public ProductEditForm(int? productId = null)
    {
        _productId = productId;
        BuildUi();
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

    private void BuildUi()
    {
        AutoScaleMode = AutoScaleMode.Font;
        Font = UiTheme.Body;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        BackColor = UiTheme.Surface;

        int y = 16;
        const int lx = 24;
        const int fw = 420;

        void Label(string t)
        {
            Controls.Add(new Label
            {
                Text = t,
                Font = UiTheme.Label,
                ForeColor = UiTheme.Text,
                Location = new Point(lx, y),
                AutoSize = true
            });
            y += 18;
        }

        TextBox Tb(int max, bool multi = false)
        {
            var tb = new TextBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(lx, y),
                Width = fw,
                MaxLength = max,
                Font = UiTheme.Body
            };
            if (multi)
            {
                tb.Multiline = true;
                tb.Height = 52;
                tb.ScrollBars = ScrollBars.Vertical;
                y += 58;
            }
            else
            {
                y += 30;
            }
            Controls.Add(tb);
            return tb;
        }

        ComboBox Cb()
        {
            var cb = new ComboBox
            {
                FlatStyle = FlatStyle.Flat,
                Location = new Point(lx, y),
                Width = fw,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = UiTheme.Body
            };
            y += 30;
            Controls.Add(cb);
            return cb;
        }

        Label("Product name *");
        txtName = Tb(100);

        Label("Category *");
        cboCategory = Cb();

        Label("Default supplier *");
        cboSupplier = Cb();

        Label("Description");
        txtDescription = Tb(500, multi: true);

        Label("Purchase price *");
        txtPurchase = Tb(20);

        Label("Selling price *");
        txtSelling = Tb(20);

        Label("Minimum stock level *");
        txtMinStock = Tb(10);

        Label("Quantity on hand");
        lblQtyValue = new Label
        {
            Text = "0",
            Font = UiTheme.Body,
            ForeColor = UiTheme.TextMuted,
            Location = new Point(lx, y),
            AutoSize = true
        };
        Controls.Add(lblQtyValue);
        y += 20;

        Controls.Add(new Label
        {
            Text = "Adjust stock using Stock-In, Stock-Out, or Inventory Adjustment.",
            Font = UiTheme.Label,
            ForeColor = UiTheme.TextMuted,
            Location = new Point(lx, y),
            AutoSize = true
        });
        y += 24;

        chkActive = new CheckBox
        {
            Text = "Active",
            Font = UiTheme.Body,
            Location = new Point(lx, y),
            AutoSize = true,
            Checked = true
        };
        Controls.Add(chkActive);
        y += 28;

        lblError = new Label
        {
            ForeColor = UiTheme.Error,
            Font = UiTheme.Label,
            Location = new Point(lx, y),
            Size = new Size(fw, 40),
            Visible = false
        };
        Controls.Add(lblError);
        y += 44;

        btnSave = UiTheme.StyleButton(new Button
        {
            Text = "Save",
            Size = new Size(100, 32),
            Location = new Point(lx + fw - 208, y)
        }, UiTheme.ButtonKind.Primary);
        btnSave.Click += BtnSave_Click;
btnSave.Click += BtnSave_Click;

        btnCancel = UiTheme.StyleButton(new Button
        {
            Text = "Cancel",
            Size = new Size(100, 32),
            Location = new Point(lx + fw - 100, y)
        }, UiTheme.ButtonKind.Secondary);
        btnCancel.Click += (_, _) => DialogResult = DialogResult.Cancel;

        AcceptButton = btnSave;
        CancelButton = btnCancel;
        Controls.Add(btnSave);
        Controls.Add(btnCancel);

        ClientSize = new Size(lx * 2 + fw, y + 52);
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
