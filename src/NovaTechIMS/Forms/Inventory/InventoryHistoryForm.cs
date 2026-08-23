using System;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>Read-only Inventory History — Designer-based (partial).</summary>
public partial class InventoryHistoryForm : Form
{
    private readonly InventoryHistoryService _service = new();

    public InventoryHistoryForm()
    {
        InitializeComponent();
        ApplyRuntimeStyling();
        Load += (_, _) =>
        {
            LoadProducts();
            dtpFrom.Value = DateTime.Today.AddDays(-30);
            dtpTo.Value = DateTime.Today;
            ReloadGrid();
        };
    }

    private void ApplyRuntimeStyling()
    {
        Font = UiTheme.Body;
        BackColor = UiTheme.Background;
        toolbar.BackColor = UiTheme.Background;
        foreach (var lbl in new[] { lblFrom, lblTo, lblType, lblProd })
        {
            lbl.Font = UiTheme.Label;
            lbl.ForeColor = UiTheme.TextMuted;
        }
        lblCount.Font = UiTheme.Label;
        lblCount.ForeColor = UiTheme.TextMuted;
        lblEmpty.Font = UiTheme.Body;
        lblEmpty.ForeColor = UiTheme.TextMuted;
        UiTheme.StyleDatePicker(dtpFrom);
        UiTheme.StyleDatePicker(dtpTo);
        UiTheme.StyleComboBox(cboType);
        UiTheme.StyleComboBox(cboProduct);
        UiTheme.StyleButton(btnApply, UiTheme.ButtonKind.Primary);
        UiTheme.StyleButton(btnClear, UiTheme.ButtonKind.Secondary);
        UiTheme.ApplyGridTheme(grid);
        UiTheme.WireStatusBadgeColumn(grid, nameof(TransactionHistoryRow.TypeLabel), value => (value?.ToString() ?? "") switch
        {
            "Stock-In" => ("Stock-In", UiTheme.BadgeTone.Success, '\u2713'),
            "Stock-Out" => ("Stock-Out", UiTheme.BadgeTone.Error, '\u2715'),
            _ => ("Adjustment", UiTheme.BadgeTone.Info, '!')
        });
    }

    private void BtnApply_Click(object? sender, EventArgs e) => ReloadGrid();

    private void BtnClear_Click(object? sender, EventArgs e)
    {
        dtpFrom.Value = DateTime.Today.AddDays(-30);
        dtpTo.Value = DateTime.Today;
        cboType.SelectedIndex = 0;
        if (cboProduct.Items.Count > 0)
            cboProduct.SelectedIndex = 0;
        ReloadGrid();
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
        catch { }
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
                lblEmpty.Text = "No transactions match the current filters.";
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
