using System;
using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>
/// Read-only Inventory History (Milestone 13). Filters: date range, type, product.
/// </summary>
public class InventoryHistoryForm : Form
{
    private readonly InventoryHistoryService _service = new();

    private DateTimePicker dtpFrom = null!;
    private DateTimePicker dtpTo = null!;
    private ComboBox cboType = null!;
    private ComboBox cboProduct = null!;
    private Button btnApply = null!;
    private Button btnClear = null!;
    private DataGridView grid = null!;
    private Label lblCount = null!;
    private Label lblEmpty = null!;

    public InventoryHistoryForm()
    {
        BuildUi();
        Load += (_, _) =>
        {
            LoadProducts();
            // Default: last 30 days
            dtpFrom.Value = DateTime.Today.AddDays(-30);
            dtpTo.Value = DateTime.Today;
            ReloadGrid();
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

        var toolbar = new Panel
        {
            Dock = DockStyle.Top,
            Height = 56,
            BackColor = UiTheme.Background,
            Padding = new Padding(0, 8, 0, 8)
        };

        var lblFrom = new Label { Text = "From", Location = new Point(0, 14), AutoSize = true, Font = UiTheme.Label };
        dtpFrom = new DateTimePicker
        {
            Location = new Point(40, 10),
            Width = 120,
            Format = DateTimePickerFormat.Short,
            Font = UiTheme.Body
        };

        var lblTo = new Label { Text = "To", Location = new Point(170, 14), AutoSize = true, Font = UiTheme.Label };
        dtpTo = new DateTimePicker
        {
            Location = new Point(195, 10),
            Width = 120,
            Format = DateTimePickerFormat.Short,
            Font = UiTheme.Body
        };

        var lblType = new Label { Text = "Type", Location = new Point(330, 14), AutoSize = true, Font = UiTheme.Label };
        cboType = new ComboBox
        {
            Location = new Point(365, 10),
            Width = 120,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = UiTheme.Body
        };
        cboType.Items.AddRange(new object[] { "All", "Stock-In", "Stock-Out", "Adjustment" });
        cboType.SelectedIndex = 0;

        var lblProd = new Label { Text = "Product", Location = new Point(500, 14), AutoSize = true, Font = UiTheme.Label };
        cboProduct = new ComboBox
        {
            Location = new Point(555, 10),
            Width = 200,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = UiTheme.Body
        };

        btnApply = new Button
        {
            Text = "Apply",
            Location = new Point(770, 8),
            Size = new Size(80, 30),
            FlatStyle = FlatStyle.Flat,
            Font = UiTheme.Button,
            BackColor = UiTheme.Primary,
            ForeColor = Color.White,
            Cursor = Cursors.Hand
        };
        btnApply.FlatAppearance.BorderSize = 0;
        btnApply.Click += (_, _) => ReloadGrid();

        btnClear = new Button
        {
            Text = "Clear",
            Location = new Point(858, 8),
            Size = new Size(70, 30),
            FlatStyle = FlatStyle.Flat,
            Font = UiTheme.Label,
            BackColor = UiTheme.Surface,
            Cursor = Cursors.Hand
        };
        btnClear.FlatAppearance.BorderColor = UiTheme.Border;
        btnClear.Click += (_, _) =>
        {
            dtpFrom.Value = DateTime.Today.AddDays(-30);
            dtpTo.Value = DateTime.Today;
            cboType.SelectedIndex = 0;
            if (cboProduct.Items.Count > 0)
                cboProduct.SelectedIndex = 0;
            ReloadGrid();
        };

        toolbar.Controls.Add(lblFrom);
        toolbar.Controls.Add(dtpFrom);
        toolbar.Controls.Add(lblTo);
        toolbar.Controls.Add(dtpTo);
        toolbar.Controls.Add(lblType);
        toolbar.Controls.Add(cboType);
        toolbar.Controls.Add(lblProd);
        toolbar.Controls.Add(cboProduct);
        toolbar.Controls.Add(btnApply);
        toolbar.Controls.Add(btnClear);

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

        lblEmpty = new Label
        {
            Text = "No transactions match the current filters.",
            Font = UiTheme.Body,
            ForeColor = UiTheme.TextMuted,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            Visible = false
        };

        lblCount = new Label
        {
            Dock = DockStyle.Bottom,
            Height = 28,
            Font = UiTheme.Label,
            ForeColor = UiTheme.TextMuted,
            TextAlign = ContentAlignment.MiddleLeft
        };

        Controls.Add(grid);
        Controls.Add(lblEmpty);
        Controls.Add(lblCount);
        Controls.Add(toolbar);
    }

    private void LoadProducts()
    {
        try
        {
            cboProduct.Items.Clear();
            cboProduct.Items.Add(new LookupItem(0, "All products"));
            foreach (var p in _service.GetProductsForFilter())
                cboProduct.Items.Add(new LookupItem(p.ProductID, p.ProductName));
            cboProduct.SelectedIndex = 0;
        }
        catch
        {
            // grid load will surface errors
        }
    }

    private void ReloadGrid()
    {
        try
        {
            int? productId = null;
            if (cboProduct.SelectedItem is LookupItem item && item.Value > 0)
                productId = item.Value;

            string? type = cboType.SelectedItem?.ToString();

            var rows = _service.GetHistory(
                dtpFrom.Value.Date,
                dtpTo.Value.Date,
                type,
                productId);

            grid.DataSource = null;
            grid.Columns.Clear();

            if (rows.Count == 0)
            {
                grid.Visible = false;
                lblEmpty.Visible = true;
                lblCount.Text = "0 transactions";
                return;
            }

            lblEmpty.Visible = false;
            grid.Visible = true;
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

            Show(nameof(TransactionHistoryRow.TransactionID), "ID", 0.4f);
            Show(nameof(TransactionHistoryRow.TransactionDate), "Date", 0.7f);
            Show(nameof(TransactionHistoryRow.TypeLabel), "Type", 0.7f);
            Show(nameof(TransactionHistoryRow.ProductName), "Product", 1.3f);
            Show(nameof(TransactionHistoryRow.Quantity), "Qty", 0.45f);
            Show(nameof(TransactionHistoryRow.UnitPrice), "Price", 0.55f);
            Show(nameof(TransactionHistoryRow.PreviousQuantity), "Prev", 0.45f);
            Show(nameof(TransactionHistoryRow.NewQuantity), "New", 0.45f);
            Show(nameof(TransactionHistoryRow.SupplierName), "Supplier", 0.9f);
            Show(nameof(TransactionHistoryRow.CustomerName), "Customer", 0.9f);
            Show(nameof(TransactionHistoryRow.UserFullName), "User", 0.9f);
            Show(nameof(TransactionHistoryRow.Reason), "Reason", 0.8f);
            Show(nameof(TransactionHistoryRow.Notes), "Notes", 1.0f);

            lblCount.Text = rows.Count == 1
                ? "1 transaction (max 500 shown)"
                : $"{rows.Count} transactions (max 500 shown)";
        }
        catch (AppException ex)
        {
            grid.Visible = false;
            lblEmpty.Visible = true;
            lblEmpty.Text = ex.Message;
            lblCount.Text = "";
        }
        catch (Exception ex)
        {
            grid.Visible = false;
            lblEmpty.Visible = true;
            lblEmpty.Text = "Unable to load history.\n" + ex.Message;
            lblCount.Text = "";
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
