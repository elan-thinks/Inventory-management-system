using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Reports;

/// <summary>
/// Displays a report grid and supports Print (FR-RPT-003).
/// </summary>
public class ReportViewerForm : Form
{
    private readonly string _title;
    private readonly object _data;
    private DataGridView grid = null!;
    private int _printRow;

    public ReportViewerForm(string title, object dataSource)
    {
        _title = title;
        _data = dataSource;
        BuildUi();
        Load += (_, _) => Bind();
    }

    private void BuildUi()
    {
        Text = _title;
        AutoScaleMode = AutoScaleMode.Font;
        Font = UiTheme.Body;
        StartPosition = FormStartPosition.CenterParent;
        Size = new Size(900, 560);
        MinimumSize = new Size(640, 400);
        BackColor = UiTheme.Background;

        var toolbar = new Panel
        {
            Dock = DockStyle.Top,
            Height = 44,
            BackColor = UiTheme.Surface,
            Padding = new Padding(8)
        };

        var btnPrint = new Button
        {
            Text = "Print…",
            Size = new Size(90, 28),
            Location = new Point(8, 8),
            FlatStyle = FlatStyle.Flat,
            Font = UiTheme.Button,
            BackColor = UiTheme.Primary,
            ForeColor = Color.White,
            Cursor = Cursors.Hand
        };
        btnPrint.FlatAppearance.BorderSize = 0;
        btnPrint.Click += BtnPrint_Click;

        var btnClose = new Button
        {
            Text = "Close",
            Size = new Size(80, 28),
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            FlatStyle = FlatStyle.Flat,
            Font = UiTheme.Label,
            BackColor = UiTheme.Surface
        };
        btnClose.FlatAppearance.BorderColor = UiTheme.Border;
        btnClose.Click += (_, _) => Close();
        toolbar.Resize += (_, _) => btnClose.Left = toolbar.ClientSize.Width - btnClose.Width - 8;

        toolbar.Controls.Add(btnPrint);
        toolbar.Controls.Add(btnClose);

        var lbl = new Label
        {
            Dock = DockStyle.Top,
            Height = 28,
            Text = "  " + _title,
            Font = UiTheme.Label,
            ForeColor = UiTheme.Text,
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
        Controls.Add(lbl);
        Controls.Add(toolbar);
    }

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
