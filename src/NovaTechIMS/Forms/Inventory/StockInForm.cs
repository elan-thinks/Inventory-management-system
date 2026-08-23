using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>
/// Stock-In screen (FR-SIN, Milestone 11).
/// Hosted in MainForm content area.
/// </summary>
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
    private Label lblError = null!;
    private Button btnSave = null!;
    private DataGridView grid = null!;
    private Label lblHint = null!;

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
            Height = 280,
            BackColor = UiTheme.Surface,
            Padding = new Padding(16)
        };

        int y = 12;
        const int lx = 16;
        const int fw = 280;

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

        // Left column
        var yL = y;
        L("Product *", lx, ref yL);
        cboProduct = new ComboBox
        {
            FlatStyle = FlatStyle.Flat,
            Location = new Point(lx, yL),
            Width = fw,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = UiTheme.Body
        };
        cboProduct.SelectedIndexChanged += CboProduct_SelectedIndexChanged;
        formPanel.Controls.Add(cboProduct);
        yL += 32;

        L("Actual supplier *", lx, ref yL);
        cboSupplier = new ComboBox
        {
            FlatStyle = FlatStyle.Flat,
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
        formPanel.Controls.Add(nudQuantity);
        yL += 32;

        // Right column
        var xR = lx + fw + 40;
        var yR = y;
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
            BorderStyle = BorderStyle.FixedSingle,
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

        L("Notes", lx, ref yL);
        txtNotes = new TextBox
        {
            BorderStyle = BorderStyle.FixedSingle,
            Location = new Point(lx, yL),
            Width = fw + 240,
            Height = 48,
            Multiline = true,
            MaxLength = 500,
            Font = UiTheme.Body,
            ScrollBars = ScrollBars.Vertical
        };
        formPanel.Controls.Add(txtNotes);
        yL += 56;

        lblError = new Label
        {
            ForeColor = UiTheme.Error,
            Font = UiTheme.Label,
            Location = new Point(lx, yL),
            Size = new Size(520, 28),
            Visible = false
        };
        formPanel.Controls.Add(lblError);

        btnSave = UiTheme.StyleButton(new Button
        {
            Text = "Record Stock-In",
            Size = new Size(150, 34),
            Location = new Point(xR + 50, yL)
        }, UiTheme.ButtonKind.Primary);
        btnSave.Click += BtnSave_Click;
        formPanel.Controls.Add(btnSave);

        lblHint = new Label
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
            BackgroundColor = UiTheme.Surface,
            BorderStyle = BorderStyle.FixedSingle,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,
            ReadOnly = true,
            MultiSelect = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            RowHeadersVisible = false,
            Font = UiTheme.Body,
            ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Font = UiTheme.Label,
                BackColor = UiTheme.StatusStripBackground,
                ForeColor = UiTheme.Text
            },
            EnableHeadersVisualStyles = false
        };

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
            lblError.Text = "Could not load products/suppliers. " + ex.Message;
            lblError.Visible = true;
        }
    }

    private void CboProduct_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cboProduct.SelectedItem is not LookupItem item || item.Value <= 0)
        {
            lblCurrentQty.Text = "—";
            return;
        }

        try
        {
            var p = _service.GetProduct(item.Value);
            lblCurrentQty.Text = p.QuantityOnHand.ToString(CultureInfo.InvariantCulture);
            txtUnitPrice.Text = p.PurchasePrice.ToString("0.00", CultureInfo.InvariantCulture);

            // Prefer product default supplier if present in list
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
            lblCurrentQty.Text = "—";
        }
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
            var txnId = _service.Record(
                prod.Value,
                sup.Value,
                qty,
                dtpDate.Value.Date,
                price,
                txtNotes.Text);

            MessageBox.Show(
                FindForm(),
                $"Stock-In recorded successfully.\nTransaction ID: {txnId}",
                "Stock-In",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // Reset partial form
            nudQuantity.Value = 1;
            txtNotes.Clear();
            CboProduct_SelectedIndexChanged(null, EventArgs.Empty);
            ReloadRecent();
        }
        catch (AppException ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }
        catch (Exception ex)
        {
            lblError.Text = "Could not record Stock-In. " + ex.Message;
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
        catch
        {
            // ignore list load errors; form still usable
        }
    }

    private sealed class LookupItem
    {
        public int Value { get; }
        public string Text { get; }
        public LookupItem(int value, string text) { Value = value; Text = text; }
        public override string ToString() => Text;
    }
}
