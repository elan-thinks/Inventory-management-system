using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>Stock-In (FR-SIN) — M19 preview banner + Clear Form.</summary>
public class StockInForm : Form
{
    private readonly StockInService _service = new();

    private ComboBox cboProduct = null!;
    private ComboBox cboSupplier = null!;
    private NumericUpDown nudQuantity = null!;
    private DateTimePicker dtpDate = null!;
    private TextBox txtUnitPrice = null!;
    private TextBox txtNotes = null!;
    private Label lblCurrentQty = null!;
    private Label lblPreview = null!;
    private Label lblError = null!;
    private Button btnSave = null!;
    private Button btnClear = null!;
    private DataGridView grid = null!;

    private int _currentOnHand;

    public StockInForm()
    {
        BuildUi();
        Load += (_, _) =>
        {
            LoadLookups();
            ReloadRecent();
        };
    }

    private void BuildUi()
    {
        AutoScaleMode = AutoScaleMode.Font;
        Font = UiTheme.Body;
        BackColor = UiTheme.Background;
        FormBorderStyle = FormBorderStyle.None;
        Dock = DockStyle.Fill;
        TopLevel = false;

        var formPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 320,
            BackColor = UiTheme.Surface,
            Padding = new Padding(16)
        };

        const int lx = 16;
        const int fw = 280;
        var yL = 12;

        void L(string t, int x, ref int yy)
        {
            formPanel.Controls.Add(new Label
            {
                Text = t,
                Font = UiTheme.Label,
                Location = new Point(x, yy),
                AutoSize = true
            });
            yy += 18;
        }

        L("Product *", lx, ref yL);
        cboProduct = new ComboBox
        {
            Location = new Point(lx, yL),
            Width = fw,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = UiTheme.Body
        };
        cboProduct.SelectedIndexChanged += (_, _) => UpdateProductAndPreview();
        formPanel.Controls.Add(cboProduct);
        yL += 32;

        L("Actual supplier *", lx, ref yL);
        cboSupplier = new ComboBox
        {
            Location = new Point(lx, yL),
            Width = fw,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = UiTheme.Body
        };
        formPanel.Controls.Add(cboSupplier);
        yL += 32;

        L("Quantity *", lx, ref yL);
        nudQuantity = new NumericUpDown
        {
            Location = new Point(lx, yL),
            Width = 120,
            Minimum = 1,
            Maximum = 1_000_000,
            Value = 1,
            Font = UiTheme.Body
        };
        nudQuantity.ValueChanged += (_, _) => UpdatePreview();
        formPanel.Controls.Add(nudQuantity);
        yL += 32;

        var xR = lx + fw + 40;
        var yR = 12;
        L("Transaction date *", xR, ref yR);
        dtpDate = new DateTimePicker
        {
            Location = new Point(xR, yR),
            Width = 200,
            Format = DateTimePickerFormat.Short,
            MaxDate = DateTime.Today,
            Value = DateTime.Today,
            Font = UiTheme.Body
        };
        formPanel.Controls.Add(dtpDate);
        yR += 32;

        L("Purchase price (at receipt) *", xR, ref yR);
        txtUnitPrice = new TextBox
        {
            Location = new Point(xR, yR),
            Width = 120,
            Font = UiTheme.Body,
            Text = "0.00"
        };
        formPanel.Controls.Add(txtUnitPrice);
        yR += 32;

        L("Current quantity on hand", xR, ref yR);
        lblCurrentQty = new Label
        {
            Text = "—",
            Font = UiTheme.Body,
            ForeColor = UiTheme.TextMuted,
            Location = new Point(xR, yR),
            AutoSize = true
        };
        formPanel.Controls.Add(lblCurrentQty);
        yR += 28;

        lblPreview = new Label
        {
            Text = "Select a product to see the new quantity on hand.",
            Font = UiTheme.Label,
            BackColor = UiTheme.InfoTint,
            ForeColor = UiTheme.Info,
            Location = new Point(lx, yL),
            Size = new Size(fw + 240, 36),
            Padding = new Padding(8, 8, 8, 8),
            TextAlign = ContentAlignment.MiddleLeft
        };
        formPanel.Controls.Add(lblPreview);
        yL += 44;

        L("Notes", lx, ref yL);
        txtNotes = new TextBox
        {
            Location = new Point(lx, yL),
            Width = fw + 240,
            Height = 40,
            Multiline = true,
            MaxLength = 500,
            Font = UiTheme.Body,
            ScrollBars = ScrollBars.Vertical
        };
        formPanel.Controls.Add(txtNotes);
        yL += 48;

        lblError = new Label
        {
            ForeColor = UiTheme.Error,
            Font = UiTheme.Label,
            Location = new Point(lx, yL),
            Size = new Size(400, 28),
            Visible = false
        };
        formPanel.Controls.Add(lblError);

        btnClear = new Button
        {
            Text = "Clear Form",
            Font = UiTheme.Label,
            FlatStyle = FlatStyle.Flat,
            Size = new Size(100, 34),
            Location = new Point(xR, yL),
            BackColor = UiTheme.Surface,
            Cursor = Cursors.Hand
        };
        btnClear.FlatAppearance.BorderColor = UiTheme.Border;
        btnClear.Click += (_, _) => ClearForm();
        formPanel.Controls.Add(btnClear);

        btnSave = new Button
        {
            Text = "Record Stock-In",
            Font = UiTheme.Button,
            BackColor = UiTheme.Primary,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Size = new Size(150, 34),
            Location = new Point(xR + 110, yL),
            Cursor = Cursors.Hand
        };
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.Click += BtnSave_Click;
        formPanel.Controls.Add(btnSave);

        var lblHint = new Label
        {
            Dock = DockStyle.Top,
            Height = 28,
            Text = "  Recent Stock-In transactions",
            Font = UiTheme.Label,
            ForeColor = UiTheme.TextMuted,
            TextAlign = ContentAlignment.MiddleLeft
        };

        grid = new DataGridView
        {
            Dock = DockStyle.Fill,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,
            ReadOnly = true,
            MultiSelect = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            RowHeadersVisible = false
        };
        GridStyles.Apply(grid);

        Controls.Add(grid);
        Controls.Add(lblHint);
        Controls.Add(formPanel);
    }

    private void LoadLookups()
    {
        try
        {
            cboProduct.Items.Clear();
            cboProduct.Items.Add(new LookupItem(0, "— Select product —"));
            foreach (var p in _service.GetActiveProducts())
                cboProduct.Items.Add(new LookupItem(p.ProductID, $"{p.ProductName} (ID {p.ProductID})"));
            cboProduct.SelectedIndex = 0;

            cboSupplier.Items.Clear();
            cboSupplier.Items.Add(new LookupItem(0, "— Select supplier —"));
            foreach (var s in _service.GetActiveSuppliers())
                cboSupplier.Items.Add(new LookupItem(s.SupplierID, s.SupplierName));
            cboSupplier.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            lblError.Text = ErrorPresenter.ToUserMessage(DbExceptionMapper.Map(ex));
            lblError.Visible = true;
        }
    }

    private void UpdateProductAndPreview()
    {
        if (cboProduct.SelectedItem is not LookupItem item || item.Value <= 0)
        {
            _currentOnHand = 0;
            lblCurrentQty.Text = "—";
            UpdatePreview();
            return;
        }

        try
        {
            var p = _service.GetProduct(item.Value);
            _currentOnHand = p.QuantityOnHand;
            lblCurrentQty.Text = _currentOnHand.ToString(CultureInfo.InvariantCulture);
            txtUnitPrice.Text = p.PurchasePrice.ToString("0.00", CultureInfo.InvariantCulture);

            for (var i = 0; i < cboSupplier.Items.Count; i++)
            {
                if (cboSupplier.Items[i] is LookupItem s && s.Value == p.SupplierID)
                {
                    cboSupplier.SelectedIndex = i;
                    break;
                }
            }
        }
        catch
        {
            _currentOnHand = 0;
            lblCurrentQty.Text = "—";
        }

        UpdatePreview();
    }

    private void UpdatePreview()
    {
        if (cboProduct.SelectedItem is not LookupItem item || item.Value <= 0)
        {
            lblPreview.Text = "Select a product to see the new quantity on hand.";
            lblPreview.BackColor = UiTheme.InfoTint;
            lblPreview.ForeColor = UiTheme.Info;
            return;
        }

        var qty = (int)nudQuantity.Value;
        var neu = _currentOnHand + qty;
        lblPreview.Text =
            $"New quantity on hand will be: {_currentOnHand} + {qty} = {neu}";
        lblPreview.BackColor = UiTheme.InfoTint;
        lblPreview.ForeColor = UiTheme.Info;
    }

    private void ClearForm()
    {
        if (cboProduct.Items.Count > 0) cboProduct.SelectedIndex = 0;
        if (cboSupplier.Items.Count > 0) cboSupplier.SelectedIndex = 0;
        nudQuantity.Value = 1;
        dtpDate.Value = DateTime.Today;
        txtUnitPrice.Text = "0.00";
        txtNotes.Clear();
        lblError.Visible = false;
        UpdateProductAndPreview();
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        lblError.Visible = false;
        btnSave.Enabled = false;

        try
        {
            if (cboProduct.SelectedItem is not LookupItem prod || prod.Value <= 0)
                throw new ValidationException("Product is required.");

            if (cboSupplier.SelectedItem is not LookupItem sup || sup.Value <= 0)
                throw new ValidationException("Supplier is required.");

            if (!decimal.TryParse(txtUnitPrice.Text.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out var price)
                && !decimal.TryParse(txtUnitPrice.Text.Trim(), out price))
                throw new ValidationException("Purchase price must be a valid number.");

            var qty = (int)nudQuantity.Value;
            var neu = _currentOnHand + qty;

            var confirm = MessageBox.Show(
                FindForm(),
                $"Record Stock-In of {qty} unit(s)?\nNew quantity on hand will be: {neu}",
                "Confirm Stock-In",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            var txnId = _service.Record(
                prod.Value,
                sup.Value,
                qty,
                dtpDate.Value.Date,
                price,
                txtNotes.Text);

            MessageBox.Show(
                FindForm(),
                $"Stock-In recorded successfully.\nTransaction ID: {txnId}\nNew quantity on hand: {neu}",
                "Stock-In",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ClearForm();
            ReloadRecent();
        }
        catch (AppException ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }
        catch (Exception ex)
        {
            lblError.Text = ErrorPresenter.ToUserMessage(DbExceptionMapper.Map(ex));
            lblError.Visible = true;
        }
        finally
        {
            btnSave.Enabled = true;
        }
    }

    private void ReloadRecent()
    {
        try
        {
            var rows = _service.GetRecent(50);
            grid.DataSource = null;
            grid.Columns.Clear();
            grid.DataSource = rows;

            foreach (DataGridViewColumn col in grid.Columns)
                col.Visible = false;

            void Show(string name, string header, float w = 1f)
            {
                if (grid.Columns[name] is not DataGridViewColumn c) return;
                c.Visible = true;
                c.HeaderText = header;
                c.FillWeight = w;
            }

            Show(nameof(StockInListRow.TransactionID), "Txn ID", 0.5f);
            Show(nameof(StockInListRow.TransactionDate), "Date", 0.7f);
            Show(nameof(StockInListRow.ProductName), "Product", 1.4f);
            Show(nameof(StockInListRow.Quantity), "Qty", 0.5f);
            Show(nameof(StockInListRow.UnitPrice), "Price", 0.6f);
            Show(nameof(StockInListRow.SupplierName), "Supplier", 1.0f);
            Show(nameof(StockInListRow.PreviousQuantity), "Prev", 0.5f);
            Show(nameof(StockInListRow.NewQuantity), "New", 0.5f);
            Show(nameof(StockInListRow.UserFullName), "User", 1.0f);
        }
        catch { /* form still usable */ }
    }

    private sealed class LookupItem
    {
        public int Value { get; }
        public string Text { get; }
        public LookupItem(int value, string text) { Value = value; Text = text; }
        public override string ToString() => Text;
    }
}
