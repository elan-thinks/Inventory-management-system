using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Utilities;

/// <summary>
/// Shared DataGridView styling (tokens: rowSelected, rowHover, headers).
/// </summary>
public static class GridStyles
{
    public static void Apply(DataGridView grid)
    {
        grid.BackgroundColor = UiTheme.Surface;
        grid.BorderStyle = BorderStyle.FixedSingle;
        grid.EnableHeadersVisualStyles = false;
        grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
        {
            Font = UiTheme.Label,
            BackColor = UiTheme.StatusStripBackground,
            ForeColor = UiTheme.Text,
            SelectionBackColor = UiTheme.StatusStripBackground,
            SelectionForeColor = UiTheme.Text
        };
        grid.DefaultCellStyle = new DataGridViewCellStyle
        {
            Font = UiTheme.Body,
            BackColor = UiTheme.Surface,
            ForeColor = UiTheme.Text,
            SelectionBackColor = UiTheme.RowSelected,
            SelectionForeColor = UiTheme.Text
        };
        grid.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
        {
            BackColor = Color.FromArgb(250, 251, 252),
            ForeColor = UiTheme.Text,
            SelectionBackColor = UiTheme.RowSelected,
            SelectionForeColor = UiTheme.Text
        };
        grid.RowTemplate.Height = 32;
        grid.ColumnHeadersHeight = 36;
        grid.GridColor = UiTheme.Border;
    }

    /// <summary>Action column with user-facing HeaderText (internal Name may stay colEdit).</summary>
    public static DataGridViewButtonColumn ActionButton(string name, string headerAndText, float fillWeight = 0.5f)
    {
        return new DataGridViewButtonColumn
        {
            Name = name,
            HeaderText = headerAndText,
            Text = headerAndText,
            UseColumnTextForButtonValue = true,
            FillWeight = fillWeight,
            FlatStyle = FlatStyle.Flat
        };
    }

    /// <summary>Apply badge-like colors for known status/type text cells.</summary>
    public static void ApplyStatusFormatting(DataGridView grid, params string[] columnNames)
    {
        grid.CellFormatting -= OnStatusCellFormatting;
        grid.Tag = columnNames;
        grid.CellFormatting += OnStatusCellFormatting;
    }

    private static void OnStatusCellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (sender is not DataGridView grid || e.RowIndex < 0 || e.ColumnIndex < 0)
            return;
        if (grid.Tag is not string[] cols)
            return;
        if (e.ColumnIndex >= grid.Columns.Count)
            return;

        var column = grid.Columns[e.ColumnIndex];
        if (column is null)
            return;

        var colName = column.Name;
        if (string.IsNullOrEmpty(colName))
            return;

        var match = false;
        foreach (var c in cols)
        {
            if (c == colName) { match = true; break; }
        }
        if (!match) return;

        var text = e.Value?.ToString() ?? string.Empty;
        var (fore, back) = StatusColors.ForLabel(text);
        e.CellStyle.ForeColor = fore;
        e.CellStyle.BackColor = back;
        e.CellStyle.SelectionForeColor = fore;
        e.CellStyle.SelectionBackColor = back;
        e.CellStyle.Font = UiTheme.BadgeText;
        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
    }
}

/// <summary>Maps status/type labels to approved tint pairs.</summary>
public static class StatusColors
{
    public static (Color Fore, Color Back) ForLabel(string label)
    {
        var t = label.Trim();
        if (t.Equals("Active", System.StringComparison.OrdinalIgnoreCase)
            || t.Equals("In Stock", System.StringComparison.OrdinalIgnoreCase)
            || t.Equals("Stock-In", System.StringComparison.OrdinalIgnoreCase)
            || t.Equals("StockIn", System.StringComparison.OrdinalIgnoreCase))
            return (UiTheme.Success, UiTheme.SuccessTint);

        if (t.Equals("Low Stock", System.StringComparison.OrdinalIgnoreCase)
            || t.Equals("LowStock", System.StringComparison.OrdinalIgnoreCase)
            || t.Equals("Expired", System.StringComparison.OrdinalIgnoreCase))
            return (UiTheme.Warning, UiTheme.WarningTint);

        if (t.Equals("Inactive", System.StringComparison.OrdinalIgnoreCase)
            || t.Equals("Out of Stock", System.StringComparison.OrdinalIgnoreCase)
            || t.Equals("OutOfStock", System.StringComparison.OrdinalIgnoreCase)
            || t.Equals("Revoked", System.StringComparison.OrdinalIgnoreCase)
            || t.Equals("Stock-Out", System.StringComparison.OrdinalIgnoreCase)
            || t.Equals("StockOut", System.StringComparison.OrdinalIgnoreCase))
            return (UiTheme.Error, UiTheme.ErrorTint);

        if (t.Equals("Administrator", System.StringComparison.OrdinalIgnoreCase)
            || t.Equals("Adjustment", System.StringComparison.OrdinalIgnoreCase)
            || t.Equals("Info", System.StringComparison.OrdinalIgnoreCase))
            return (UiTheme.Info, UiTheme.InfoTint);

        if (t.Equals("Inventory Staff", System.StringComparison.OrdinalIgnoreCase)
            || t.Equals("InventoryStaff", System.StringComparison.OrdinalIgnoreCase))
            return (UiTheme.Secondary, UiTheme.InfoTint);

        return (UiTheme.Text, UiTheme.Surface);
    }
}
