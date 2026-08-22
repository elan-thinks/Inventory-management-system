using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>Stock-Out — M19 available banner + insufficient UX + Clear Form.</summary>
public class StockOutForm : Form
{
    private readonly StockOutService _service = new();

    private ComboBox cboProduct = null!;
    private ComboBox cboCustomer = null!;
    private NumericUpDown nudQuantity = null!;
    private DateTimePicker dtpDate = null!;
    private TextBox txtUnitPrice = null!;
    private TextBox txtNotes = null!;
    private Label lblAvailable = null!;
    private Label lblError = null!;
    private Button btnSave = null!;
    private Button btnClear = null!;
    private DataGridView grid = null!;

    private int _available;

    public StockOutForm()
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
        cboProduct.SelectedIndexChanged += (_, _) => UpdateAvailable();
        formPanel.Controls.Add(cboProduct);
        yL += 32;

        lblAvailable = new Label
        {
            Text = "Available quantity: —",
            Font = UiTheme.Label,
            BackColor = UiTheme.InfoTint,
            ForeColor = UiTheme.Info,
            Location = new Point(lx, yL),
            Size = new Size(fw, 32),
            Padding = new Padding(8, 6, 8, 6),
            TextAlign = ContentAlignment.MiddleLeft
        };
        formPanel.Controls.Add(lblAvailable);
        yL += 40;

        L("Customer (optional)", lx, ref yL);
        cboCustomer = new ComboBox
        {
            Location = new Point(lx, yL),
            Width = fw,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = UiTheme.Body
        };
        formPanel.Controls.Add(cboCustomer);
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
        nudQuantity.ValueChanged += (_, _) => EvaluateInsufficient();
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

        L("Selling price *", xR, ref yR);
        txtUnitPrice = new TextBox
        {
            Location = new Point(xR, yR),
            Width = 120,
            Font = UiTheme.Body,
            Text = "0.00"
        };
        formPanel.Controls.Add(txtUnitPrice);
        yR += 32;

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
            Text = "Record Stock-Out",
            Font = UiTheme.Button,
            BackColor = UiTheme.Primary,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Size = new Size(160, 34),
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
            Text = "  Recent Stock-Out transactions",
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

            cboCustomer.Items.Clear();
            cboCustomer.Items.Add(new LookupItem(0, "— None (optional) —"));
            foreach (var c in _service.GetActiveCustomers())
                cboCustomer.Items.Add(new LookupItem(c.CustomerID, c.CustomerName));
            cboCustomer.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            lblError.Text = ErrorPresenter.ToUserMessage(DbExceptionMapper.Map(ex));
            lblError.Visible = true;
        }
    }

    private void UpdateAvailable()
    {
        if (cboProduct.SelectedItem is not LookupItem item || item.Value <= 0)
        {
            _available = 0;
            lblAvailable.Text = "Available quantity: —";
            lblAvailable.BackColor = UiTheme.InfoTint;
            lblAvailable.ForeColor = UiTheme.Info;
            EvaluateInsufficient();
            return;
        }

        try
        {
            var p = _service.GetProduct(item.Value);
            _available = p.QuantityOnHand;
            lblAvailable.Text = $"Available quantity: {_available}";
            txtUnitPrice.Text = p.SellingPrice.ToString("0.00", CultureInfo.InvariantCulture);
        }
        catch
        {
            _available = 0;
            lblAvailable.Text = "Available quantity: —";
        }

        EvaluateInsufficient();
    }

    private void EvaluateInsufficient()
    {
        var qty = (int)nudQuantity.Value;
        var insufficient = cboProduct.SelectedItem is LookupItem item
            && item.Value > 0
            && qty > _available;

        if (insufficient)
        {
            lblAvailable.Text = $"Insufficient stock. Available: {_available}.";
            lblAvailable.BackColor = UiTheme.ErrorTint;
            lblAvailable.ForeColor = UiTheme.Error;
            nudQuantity.BackColor = UiTheme.ErrorTint;
            btnSave.Enabled = false;
            btnSave.BackColor = UiTheme.DisabledBackground;
            btnSave.ForeColor = UiTheme.DisabledText;
            lblError.Text = $"Insufficient stock. Available: {_available}, requested: {qty}.";
            lblError.Visible = true;
        }
        else
        {
            if (cboProduct.SelectedItem is LookupItem sel && sel.Value > 0)
            {
                lblAvailable.Text = $"Available quantity: {_available}";
                lblAvailable.BackColor = UiTheme.InfoTint;
                lblAvailable.ForeColor = UiTheme.Info;
            }
            nudQuantity.BackColor = UiTheme.Surface;
            btnSave.Enabled = true;
            btnSave.BackColor = UiTheme.Primary;
            btnSave.ForeColor = Color.White;
            lblError.Visible = false;
        }
    }

    private void ClearForm()
    {
        if (cboProduct.Items.Count > 0) cboProduct.SelectedIndex = 0;
        if (cboCustomer.Items.Count > 0) cboCustomer.SelectedIndex = 0;
        nudQuantity.Value = 1;
        dtpDate.Value = DateTime.Today;
        txtUnitPrice.Text = "0.00";
        txtNotes.Clear();
        lblError.Visible = false;
        UpdateAvailable();
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        if (!btnSave.Enabled) return;

        lblError.Visible = false;
        btnSave.Enabled = false;

        try
        {
            if (cboProduct.SelectedItem is not LookupItem prod || prod.Value <= 0)
                throw new ValidationException("Product is required.");

            int? customerId = null;
            if (cboCustomer.SelectedItem is LookupItem cust && cust.Value > 0)
                customerId = cust.Value;

            if (!decimal.TryParse(txtUnitPrice.Text.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out var price)
                && !decimal.TryParse(txtUnitPrice.Text.Trim(), out price))
                throw new ValidationException("Selling price must be a valid number.");

            var qty = (int)nudQuantity.Value;
            if (qty > _available)
                throw new InsufficientStockException(_available, qty);

            var remaining = _available - qty;
            var confirm = MessageBox.Show(
                FindForm(),
                $"Record Stock-Out of {qty} unit(s)?\nRemaining stock will be: {remaining}",
                "Confirm Stock-Out",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            var txnId = _service.Record(
                prod.Value,
                customerId,
                qty,
                dtpDate.Value.Date,
                price,
                txtNotes.Text);

            MessageBox.Show(
                FindForm(),
                $"Stock-Out recorded successfully.\nTransaction ID: {txnId}\nRemaining: {remaining}",
                "Stock-Out",
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
            EvaluateInsufficient();
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

            Show(nameof(StockOutListRow.TransactionID), "Txn ID", 0.5f);
            Show(nameof(StockOutListRow.TransactionDate), "Date", 0.7f);
            Show(nameof(StockOutListRow.ProductName), "Product", 1.4f);
            Show(nameof(StockOutListRow.Quantity), "Qty", 0.5f);
            Show(nameof(StockOutListRow.UnitPrice), "Price", 0.6f);
            Show(nameof(StockOutListRow.CustomerName), "Customer", 1.0f);
            Show(nameof(StockOutListRow.PreviousQuantity), "Prev", 0.5f);
            Show(nameof(StockOutListRow.NewQuantity), "New", 0.5f);
            Show(nameof(StockOutListRow.UserFullName), "User", 1.0f);
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
