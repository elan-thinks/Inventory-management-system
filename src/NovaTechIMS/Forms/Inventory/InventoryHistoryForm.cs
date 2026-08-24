using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>
/// SCR-015 Inventory History — append-only read-only; designer owns layout.
/// </summary>
public partial class InventoryHistoryForm : Form
{
    private InventoryHistoryService? _service;
    private int _printRow;

    private InventoryHistoryService Service => _service ??= new InventoryHistoryService();

    public InventoryHistoryForm()
    {
        InitializeComponent();

        if (DesignTime.IsActive)
            return;

        grid.Columns.Clear();
        grid.ColumnCount = 0;

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

        banner.BackColor = UiTheme.InfoTint;
        lblBanner.ForeColor = UiTheme.Info;
        lblBanner.Font = UiTheme.Label;
        lblBanner.BackColor = UiTheme.InfoTint;

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
        lblEmpty.BackColor = UiTheme.Background;

        UiTheme.StyleDatePicker(dtpFrom);
        UiTheme.StyleDatePicker(dtpTo);
        UiTheme.StyleComboBox(cboType);
        UiTheme.StyleComboBox(cboProduct);
        UiTheme.StyleButton(btnClear, UiTheme.ButtonKind.Ghost);
        UiTheme.StyleButton(btnPrint, UiTheme.ButtonKind.Secondary);

        UiTheme.ApplyGridTheme(grid);
        UiTheme.WireStatusBadgeColumn(grid, nameof(TransactionHistoryRow.TypeLabel), value => (value?.ToString() ?? "") switch
        {
            "Stock-In" => ("Stock In", UiTheme.BadgeTone.Success, '\u2713'),
            "Stock-Out" => ("Stock Out", UiTheme.BadgeTone.Error, '\u2715'),
            _ => ("Adjustment", UiTheme.BadgeTone.Info, '!')
        });

        Toolbar_Resize(toolbar, EventArgs.Empty);
    }

    private void Toolbar_Resize(object? sender, EventArgs e)
    {
        if (toolbar is null || btnPrint is null) return;
        btnPrint.Left = Math.Max(8, toolbar.ClientSize.Width - btnPrint.Width - 4);
    }

    private void Filter_Changed(object? sender, EventArgs e)
    {
        if (DesignTime.IsActive) return;
        ReloadGrid();
    }

    private void BtnClear_Click(object? sender, EventArgs e)
    {
        if (DesignTime.IsActive) return;
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
            cboProduct.Items.Add(new LookupItem(0, "Product: All"));
            foreach (var p in Service.GetProductsForFilter())
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

            string? type = cboType.SelectedIndex switch
            {
                1 => "Stock-In",
                2 => "Stock-Out",
                3 => "Adjustment",
                _ => null
            };

            var rows = Service.GetHistory(
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

            Show(nameof(TransactionHistoryRow.TransactionID), "Transaction ID", 0.55f);
            Show(nameof(TransactionHistoryRow.TransactionDate), "Date", 0.85f);
            Show(nameof(TransactionHistoryRow.TypeLabel), "Type", 0.75f);
            Show(nameof(TransactionHistoryRow.ProductName), "Product", 1.4f);
            Show(nameof(TransactionHistoryRow.Quantity), "Quantity", 0.55f);
            Show(nameof(TransactionHistoryRow.SupplierName), "Supplier", 1.0f);
            Show(nameof(TransactionHistoryRow.CustomerName), "Customer", 1.0f);
            Show(nameof(TransactionHistoryRow.UserFullName), "User", 0.9f);
            Show(nameof(TransactionHistoryRow.Notes), "Notes", 1.1f);

            if (grid.Columns[nameof(TransactionHistoryRow.TransactionDate)] is DataGridViewColumn dateCol)
                dateCol.DefaultCellStyle.Format = "g";

            if (grid.Columns[nameof(TransactionHistoryRow.Quantity)] is DataGridViewColumn qtyCol)
            {
                qtyCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                grid.CellFormatting -= Grid_CellFormattingQty;
                grid.CellFormatting += Grid_CellFormattingQty;
            }

            lblCount.Text = rows.Count == 1
                ? "Showing 1 of 1 transactions — no Edit or Delete on this screen"
                : $"Showing {rows.Count} of {rows.Count} transactions — no Edit or Delete on this screen";
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
            lblEmpty.Text = "Unable to load history.\n" + ErrorPresenter.Classify(DbExceptionMapper.Map(ex)).Message;
            lblCount.Text = "";
        }
    }

    private void Grid_CellFormattingQty(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || grid.Columns[e.ColumnIndex].Name != nameof(TransactionHistoryRow.Quantity))
            return;

        if (e.Value is int q)
        {
            if (grid.Rows[e.RowIndex].DataBoundItem is TransactionHistoryRow row)
            {
                if (row.TypeLabel == "Stock-Out" || (row.TypeLabel == "Adjustment" && row.Difference < 0))
                {
                    e.Value = q > 0 ? $"−{q}" : q.ToString();
                    e.FormattingApplied = true;
                    e.CellStyle.ForeColor = UiTheme.Error;
                    e.CellStyle.Font = UiTheme.LabelSemibold;
                }
                else if (row.TypeLabel == "Stock-In" || (row.TypeLabel == "Adjustment" && row.Difference > 0))
                {
                    e.Value = q > 0 ? $"+{q}" : q.ToString();
                    e.FormattingApplied = true;
                    e.CellStyle.ForeColor = UiTheme.Success;
                    e.CellStyle.Font = UiTheme.LabelSemibold;
                }
            }
        }
    }

    private void BtnPrint_Click(object? sender, EventArgs e)
    {
        if (DesignTime.IsActive) return;

        using var doc = new PrintDocument();
        doc.DocumentName = "Inventory History";
        doc.PrintPage += Doc_PrintPage;
        _printRow = 0;

        using var dlg = new PrintDialog { Document = doc, UseEXDialog = true };
        if (dlg.ShowDialog(FindForm()) != DialogResult.OK) return;

        try { doc.Print(); }
        catch (Exception ex)
        {
            MessageBox.Show(FindForm(), "Print failed: " + ex.Message, "Print",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void Doc_PrintPage(object? sender, PrintPageEventArgs e)
    {
        if (e.Graphics is null) return;

        float y = e.MarginBounds.Top;
        float left = e.MarginBounds.Left;
        float width = e.MarginBounds.Width;

        using var titleFont = new Font("Segoe UI", 12, FontStyle.Bold);
        using var headerFont = new Font("Segoe UI", 8, FontStyle.Bold);
        using var cellFont = new Font("Segoe UI", 8);

        e.Graphics.DrawString("Inventory History", titleFont, Brushes.Black, left, y);
        y += titleFont.GetHeight(e.Graphics) + 6;
        e.Graphics.DrawString(
            $"{dtpFrom.Value:yyyy-MM-dd} – {dtpTo.Value:yyyy-MM-dd}  ·  Printed {DateTime.Now:yyyy-MM-dd HH:mm}",
            cellFont, Brushes.Gray, left, y);
        y += cellFont.GetHeight(e.Graphics) + 10;

        var cols = grid.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).Take(7).ToList();
        if (cols.Count == 0) { e.HasMorePages = false; return; }

        float colW = width / cols.Count;
        float x = left;
        foreach (var c in cols)
        {
            e.Graphics.DrawString(c.HeaderText, headerFont, Brushes.Black, x, y);
            x += colW;
        }

        y += headerFont.GetHeight(e.Graphics) + 4;
        e.Graphics.DrawLine(Pens.Gray, left, y, left + width, y);
        y += 4;

        while (_printRow < grid.Rows.Count)
        {
            var row = grid.Rows[_printRow];
            if (row.IsNewRow) { _printRow++; continue; }

            float rowH = cellFont.GetHeight(e.Graphics) + 2;
            if (y + rowH > e.MarginBounds.Bottom)
            {
                e.HasMorePages = true;
                return;
            }

            x = left;
            foreach (var c in cols)
            {
                var val = row.Cells[c.Index].FormattedValue?.ToString() ?? "";
                if (val.Length > 28) val = val[..25] + "…";
                e.Graphics.DrawString(val, cellFont, Brushes.Black, x, y);
                x += colW;
            }

            y += rowH;
            _printRow++;
        }

        e.HasMorePages = false;
    }

    private sealed class LookupItem
    {
        public int Value { get; }
        public string Text { get; }
        public LookupItem(int value, string text) { Value = value; Text = text; }
        public override string ToString() => Text;
    }
}
