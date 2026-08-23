using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Reports;

/// <summary>
/// SCR-016/017 Reports hub — card chooser + embedded results (mockup).
/// </summary>
public partial class ReportsHubForm : Form
{
    private readonly ReportService _service = new();
    private string _currentTitle = "";
    private int _printRow;
    private Panel? _selectedCard;

    private static readonly (string Title, string Desc, string Icon, int Index)[] ReportDefs =
    {
        ("Current Inventory", "Full stock list with pricing and status", "icons-totalProduct.png", 0),
        ("Low Stock", "Products at or below their minimum level", "icons-warning.png", 1),
        ("Out-of-Stock", "Products currently at zero quantity", "icons-x-error.png", 2),
        ("Stock-In", "Goods received within a date range", "icons-stockIn.png", 3),
        ("Stock-Out", "Goods issued within a date range", "icons-stockOut.png", 4),
        ("Transaction History", "All stock movements within a date range", "icons-history.png", 5),
    };

    public ReportsHubForm()
    {
        InitializeComponent();
        ApplyRuntimeStyling();
        BuildCards();
        dtpFrom.Value = DateTime.Today.AddDays(-30);
        dtpTo.Value = DateTime.Today;
    }

    private void ApplyRuntimeStyling()
    {
        Font = UiTheme.Body;
        BackColor = UiTheme.Background;

        lblChoose.Font = UiTheme.SectionTitle;
        lblChoose.ForeColor = UiTheme.Text;

        lblFrom.ForeColor = UiTheme.TextMuted;
        lblTo.ForeColor = UiTheme.TextMuted;
        UiTheme.StyleDatePicker(dtpFrom);
        UiTheme.StyleDatePicker(dtpTo);

        cardsPanel.BackColor = UiTheme.Background;
        filterBar.BackColor = UiTheme.Background;

        resultsHeader.BackColor = UiTheme.Surface;
        lblReportTitle.ForeColor = UiTheme.Text;
        lblFilters.ForeColor = UiTheme.TextMuted;
        lblSummary.ForeColor = UiTheme.TextMuted;
        lblSummary.BackColor = UiTheme.Surface;
        lblError.ForeColor = UiTheme.Error;

        UiTheme.StyleButton(btnBack, UiTheme.ButtonKind.Secondary);
        UiTheme.StyleButton(btnPrint, UiTheme.ButtonKind.Primary);
        AppIcons.ApplyToButton(btnPrint, "icons-print.png", 16);

        UiTheme.ApplyGridTheme(grid);
        pnlResults.BackColor = UiTheme.Surface;
    }

    private void BuildCards()
    {
        cardsPanel.Controls.Clear();
        cardsPanel.Padding = new Padding(0, 8, 0, 8);

        foreach (var def in ReportDefs)
        {
            var card = CreateReportCard(def.Title, def.Desc, def.Icon, def.Index);
            cardsPanel.Controls.Add(card);
        }
    }

    private Panel CreateReportCard(string title, string desc, string iconFile, int reportIndex)
    {
        var card = new Panel
        {
            Size = new Size(280, 88),
            Margin = new Padding(0, 0, 12, 12),
            BackColor = UiTheme.Surface,
            Cursor = Cursors.Hand,
            Tag = reportIndex,
            Padding = new Padding(14)
        };

        card.Paint += (_, e) =>
        {
            bool selected = _selectedCard == card;
            using var pen = new Pen(selected ? UiTheme.Primary : UiTheme.Border, selected ? 2 : 1);
            e.Graphics.DrawRectangle(pen, 1, 1, card.Width - 3, card.Height - 3);
        };

        var icon = new PictureBox
        {
            Size = new Size(20, 20),
            Location = new Point(14, 16),
            SizeMode = PictureBoxSizeMode.Zoom,
            BackColor = Color.Transparent
        };
        var img = AppIcons.Get(iconFile, 18);
        if (img is not null) icon.Image = img;

        var lblTitle = new Label
        {
            Text = title,
            Font = UiTheme.LabelSemibold,
            ForeColor = UiTheme.Text,
            AutoSize = true,
            Location = new Point(42, 14),
            BackColor = Color.Transparent
        };

        var lblDesc = new Label
        {
            Text = desc,
            Font = UiTheme.Label,
            ForeColor = UiTheme.TextMuted,
            AutoSize = false,
            Size = new Size(220, 36),
            Location = new Point(42, 38),
            BackColor = Color.Transparent
        };

        void ClickAll(object? s, EventArgs e) => OnCardClicked(card, reportIndex);

        card.Click += ClickAll;
        icon.Click += ClickAll;
        lblTitle.Click += ClickAll;
        lblDesc.Click += ClickAll;

        card.Controls.Add(icon);
        card.Controls.Add(lblTitle);
        card.Controls.Add(lblDesc);
        return card;
    }

    private void OnCardClicked(Panel card, int reportIndex)
    {
        _selectedCard = card;
        foreach (Control c in cardsPanel.Controls)
            c.Invalidate();

        // Date-range reports: show filter bar first; click again or auto-run
        bool needsDates = reportIndex is 3 or 4 or 5;
        filterBar.Visible = needsDates;

        if (needsDates)
        {
            // Keep chooser visible so user can set dates, then run via double purpose:
            // run immediately with current dates (default last 30 days)
        }

        RunReport(reportIndex);
    }

    private void RunReport(int index)
    {
        lblError.Visible = false;

        try
        {
            object data;
            string title;
            string filters = "";

            switch (index)
            {
                case 0:
                    title = "Current Inventory Report";
                    data = _service.GetCurrentInventory();
                    filters = "Filters: Category — All · Stock Status — All";
                    break;
                case 1:
                    title = "Low Stock Report";
                    data = _service.GetLowStock();
                    filters = "Products at or below minimum level";
                    break;
                case 2:
                    title = "Out-of-Stock Report";
                    data = _service.GetOutOfStock();
                    filters = "Quantity on hand = 0";
                    break;
                case 3:
                    title = $"Stock-In Report";
                    data = _service.GetStockInReport(dtpFrom.Value, dtpTo.Value);
                    filters = $"From {dtpFrom.Value:yyyy-MM-dd} to {dtpTo.Value:yyyy-MM-dd}";
                    break;
                case 4:
                    title = "Stock-Out Report";
                    data = _service.GetStockOutReport(dtpFrom.Value, dtpTo.Value);
                    filters = $"From {dtpFrom.Value:yyyy-MM-dd} to {dtpTo.Value:yyyy-MM-dd}";
                    break;
                default:
                    title = "Transaction History Report";
                    data = _service.GetTransactionHistoryReport(dtpFrom.Value, dtpTo.Value, null, null);
                    filters = $"From {dtpFrom.Value:yyyy-MM-dd} to {dtpTo.Value:yyyy-MM-dd}";
                    break;
            }

            _currentTitle = title;
            lblReportTitle.Text = title;
            lblFilters.Text = filters;

            grid.DataSource = null;
            grid.Columns.Clear();
            grid.DataSource = data;
            FormatColumns();

            int count = 0;
            if (data is ICollection col) count = col.Count;
            else if (data is IEnumerable en)
                count = en.Cast<object>().Count();

            lblSummary.Text = count == 1 ? "1 row" : $"{count} rows";

            pnlChooser.Visible = false;
            pnlResults.Visible = true;
            pnlResults.BringToFront();
        }
        catch (AppException ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }
        catch (Exception ex)
        {
            lblError.Text = "Could not run report. " + ex.Message;
            lblError.Visible = true;
        }
    }

    private void FormatColumns()
    {
        foreach (DataGridViewColumn col in grid.Columns)
        {
            col.HeaderText = col.DataPropertyName switch
            {
                "ProductID" => "Product ID",
                "ProductName" => "Name",
                "CategoryName" => "Category",
                "SupplierName" => "Default Supplier",
                "QuantityOnHand" => "Qty on Hand",
                "MinimumStockLevel" => "Min Level",
                "PurchasePrice" => "Purchase",
                "SellingPrice" => "Selling Price",
                "StockStatusLabel" => "Status",
                "TransactionID" => "Txn ID",
                "TypeLabel" => "Type",
                "TransactionDate" => "Date",
                "UnitPrice" => "Price",
                "CustomerName" => "Customer",
                "PreviousQuantity" => "Prev",
                "NewQuantity" => "New",
                "UserFullName" => "User",
                "CreatedDateTime" => "Recorded",
                "Quantity" => "Qty",
                _ => col.HeaderText
            };
        }
    }

    private void BtnBack_Click(object? sender, EventArgs e)
    {
        pnlResults.Visible = false;
        pnlChooser.Visible = true;
        pnlChooser.BringToFront();
        _selectedCard = null;
        foreach (Control c in cardsPanel.Controls)
            c.Invalidate();
    }

    private void BtnPrint_Click(object? sender, EventArgs e)
    {
        using var doc = new PrintDocument();
        doc.DocumentName = _currentTitle;
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

        e.Graphics.DrawString(_currentTitle, titleFont, Brushes.Black, left, y);
        y += titleFont.GetHeight(e.Graphics) + 8;
        e.Graphics.DrawString("Printed: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
            cellFont, Brushes.Gray, left, y);
        y += cellFont.GetHeight(e.Graphics) + 12;

        var cols = grid.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).Take(6).ToList();
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
}
