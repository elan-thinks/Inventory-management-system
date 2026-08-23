using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Reports;

/// <summary>Report grid + Print — Designer-based (partial).</summary>
public partial class ReportViewerForm : Form
{
    private readonly string _title;
    private readonly object _data;
    private int _printRow;

    public ReportViewerForm(string title, object dataSource)
    {
        _title = title;
        _data = dataSource;
        InitializeComponent();
        ApplyRuntimeStyling();
        Text = _title;
        lblTitle.Text = "  " + _title;
        Load += (_, _) => Bind();
    }

    private void ApplyRuntimeStyling()
    {
        Font = UiTheme.Body;
        BackColor = UiTheme.Background;
        toolbar.BackColor = UiTheme.Surface;
        lblTitle.Font = UiTheme.SectionTitle;
        lblTitle.ForeColor = UiTheme.Text;
        UiTheme.StyleButton(btnPrint, UiTheme.ButtonKind.Primary);
        UiTheme.StyleButton(btnClose, UiTheme.ButtonKind.Secondary);
        UiTheme.ApplyGridTheme(grid);
        Toolbar_Resize(toolbar, EventArgs.Empty);
    }

    private void Toolbar_Resize(object? sender, EventArgs e)
    {
        btnClose.Left = toolbar.ClientSize.Width - btnClose.Width - 8;
    }

    private void BtnClose_Click(object? sender, EventArgs e) => Close();

    private void Bind()
    {
        grid.DataSource = _data;

        foreach (DataGridViewColumn col in grid.Columns)
        {
            col.HeaderText = col.DataPropertyName switch
            {
                "ProductID" => "Product ID",
                "ProductName" => "Product",
                "CategoryName" => "Category",
                "SupplierName" => "Supplier",
                "QuantityOnHand" => "Qty",
                "MinimumStockLevel" => "Min",
                "PurchasePrice" => "Purchase",
                "SellingPrice" => "Selling",
                "StockStatusLabel" => "Stock status",
                "TransactionID" => "Txn ID",
                "TypeLabel" => "Type",
                "TransactionDate" => "Date",
                "UnitPrice" => "Price",
                "CustomerName" => "Customer",
                "PreviousQuantity" => "Prev",
                "NewQuantity" => "New",
                "UserFullName" => "User",
                "CreatedDateTime" => "Recorded",
                _ => col.HeaderText
            };
        }
    }

    private void BtnPrint_Click(object? sender, EventArgs e)
    {
        using var doc = new PrintDocument();
        doc.DocumentName = _title;
        doc.PrintPage += Doc_PrintPage;
        _printRow = 0;

        using var dlg = new PrintDialog { Document = doc, UseEXDialog = true };
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                doc.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Print failed: " + ex.Message, "Print",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        e.Graphics.DrawString(_title, titleFont, Brushes.Black, left, y);
        y += titleFont.GetHeight(e.Graphics) + 8;
        e.Graphics.DrawString(
            "Printed: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
            cellFont, Brushes.Gray, left, y);
        y += cellFont.GetHeight(e.Graphics) + 12;

        var cols = grid.Columns.Cast<DataGridViewColumn>()
            .Where(c => c.Visible)
            .Take(6)
            .ToList();

        if (cols.Count == 0)
        {
            e.HasMorePages = false;
            return;
        }

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
}
