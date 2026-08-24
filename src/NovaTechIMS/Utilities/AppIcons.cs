using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace NovaTechIMS.Utilities;

/// <summary>
/// Loads PNG icons from Resources and draws lightweight grid action icons.
/// Safe: returns null if a file is missing so UI still works.
/// </summary>
internal static class AppIcons
{
    private static readonly Dictionary<string, Image> Cache = new(StringComparer.OrdinalIgnoreCase);

    // Grid action icon colors (secondary to data — not loud buttons)
    private static readonly Color EditStroke = Color.FromArgb(75, 85, 99);       // blue-gray
    private static readonly Color DeleteStroke = Color.FromArgb(185, 55, 55);    // muted destructive

    public static string ResourcesDirectory
    {
        get
        {
            var baseDir = AppContext.BaseDirectory;
            var candidate = Path.Combine(baseDir, "Resources");
            if (Directory.Exists(candidate))
                return candidate;

            candidate = Path.Combine(baseDir, "..", "..", "..", "Resources");
            return Path.GetFullPath(candidate);
        }
    }

    public static Image? Get(string fileName, int size = 16)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return null;

        var key = fileName + "@" + size;
        if (Cache.TryGetValue(key, out var cached))
            return cached;

        try
        {
            var path = Path.Combine(ResourcesDirectory, fileName);
            if (!File.Exists(path))
                return null;

            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var original = Image.FromStream(fs);
            var resized = new Bitmap(size, size);
            using (var g = Graphics.FromImage(resized))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.Clear(Color.Transparent);
                g.DrawImage(original, 0, 0, size, size);
            }

            Cache[key] = resized;
            return resized;
        }
        catch
        {
            return null;
        }
    }

    public static void ApplyToButton(Button btn, string fileName, int size = 16)
    {
        var img = Get(fileName, size);
        if (img is null) return;

        btn.Image = img;
        btn.ImageAlign = ContentAlignment.MiddleLeft;
        btn.TextAlign = ContentAlignment.MiddleLeft;
        btn.TextImageRelation = TextImageRelation.ImageBeforeText;
        btn.Padding = new Padding(8, 0, 4, 0);
    }

    public static void ApplyToLabel(Label lbl, string fileName, int size = 16)
    {
        var img = Get(fileName, size);
        if (img is null) return;

        lbl.Image = img;
        lbl.ImageAlign = ContentAlignment.MiddleLeft;
        lbl.TextAlign = ContentAlignment.MiddleLeft;
        if (!lbl.Text.StartsWith(" ", StringComparison.Ordinal))
            lbl.Text = "  " + lbl.Text.TrimStart();
    }

    /// <summary>
    /// Lightweight outline pencil icon for grid actions (~16–18px).
    /// Drawn in code so appearance is consistent and not dependent on heavy bitmaps.
    /// </summary>
    public static Image CreateOutlineEditIcon(int size = 16)
    {
        var key = "__outline_edit@" + size;
        if (Cache.TryGetValue(key, out var cached))
            return cached;

        var bmp = new Bitmap(size, size);
        using (var g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.Clear(Color.Transparent);

            // Coordinate space 0..16 scaled to size
            float s = size / 16f;
            using var pen = new Pen(EditStroke, Math.Max(1.2f, 1.35f * s))
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round,
                LineJoin = LineJoin.Round
            };

            // Pencil body (diagonal)
            g.DrawLine(pen, 11.5f * s, 3.2f * s, 5.2f * s, 9.5f * s);
            // Tip
            g.DrawLine(pen, 5.2f * s, 9.5f * s, 3.8f * s, 12.2f * s);
            g.DrawLine(pen, 3.8f * s, 12.2f * s, 6.5f * s, 10.8f * s);
            // Eraser end (small rectangle angle)
            g.DrawLine(pen, 11.5f * s, 3.2f * s, 13.0f * s, 4.7f * s);
            g.DrawLine(pen, 13.0f * s, 4.7f * s, 11.8f * s, 5.9f * s);
            g.DrawLine(pen, 11.8f * s, 5.9f * s, 10.3f * s, 4.4f * s);
        }

        Cache[key] = bmp;
        return bmp;
    }

    /// <summary>
    /// Lightweight outline trash icon for grid actions (~16–18px).
    /// Muted destructive color — icon, not a red square button.
    /// </summary>
    public static Image CreateOutlineDeleteIcon(int size = 16)
    {
        var key = "__outline_delete@" + size;
        if (Cache.TryGetValue(key, out var cached))
            return cached;

        var bmp = new Bitmap(size, size);
        using (var g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.Clear(Color.Transparent);

            float s = size / 16f;
            using var pen = new Pen(DeleteStroke, Math.Max(1.2f, 1.35f * s))
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round,
                LineJoin = LineJoin.Round
            };

            // Lid
            g.DrawLine(pen, 3.5f * s, 5.0f * s, 12.5f * s, 5.0f * s);
            // Handle on lid
            g.DrawLine(pen, 6.2f * s, 3.4f * s, 9.8f * s, 3.4f * s);
            g.DrawLine(pen, 6.2f * s, 3.4f * s, 6.2f * s, 5.0f * s);
            g.DrawLine(pen, 9.8f * s, 3.4f * s, 9.8f * s, 5.0f * s);
            // Can body
            g.DrawLine(pen, 4.5f * s, 5.0f * s, 5.2f * s, 13.0f * s);
            g.DrawLine(pen, 11.5f * s, 5.0f * s, 10.8f * s, 13.0f * s);
            g.DrawLine(pen, 5.2f * s, 13.0f * s, 10.8f * s, 13.0f * s);
            // Inner lines
            g.DrawLine(pen, 7.0f * s, 6.5f * s, 7.2f * s, 11.5f * s);
            g.DrawLine(pen, 9.0f * s, 6.5f * s, 8.8f * s, 11.5f * s);
        }

        Cache[key] = bmp;
        return bmp;
    }

    /// <summary>Icon-only Edit action column — outline pencil, centered, non-dominant.</summary>
    public static DataGridViewImageColumn CreateEditColumn(float fillWeight = 0.35f)
    {
        var img = CreateOutlineEditIcon(16);
        return new DataGridViewImageColumn
        {
            Name = "colEdit",
            HeaderText = "",
            Image = img,
            ImageLayout = DataGridViewImageCellLayout.Normal,
            FillWeight = fillWeight,
            MinimumWidth = 32,
            Width = 36,
            Resizable = DataGridViewTriState.False,
            ToolTipText = "Edit",
            Description = "Edit",
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                NullValue = null,
                Padding = new Padding(0),
                BackColor = UiTheme.Surface,
                SelectionBackColor = UiTheme.RowSelected
            }
        };
    }

    /// <summary>Icon-only Delete action column — outline trash, muted destructive color.</summary>
    public static DataGridViewImageColumn CreateDeleteColumn(float fillWeight = 0.35f)
    {
        var img = CreateOutlineDeleteIcon(16);
        return new DataGridViewImageColumn
        {
            Name = "colDelete",
            HeaderText = "",
            Image = img,
            ImageLayout = DataGridViewImageCellLayout.Normal,
            FillWeight = fillWeight,
            MinimumWidth = 32,
            Width = 36,
            Resizable = DataGridViewTriState.False,
            ToolTipText = "Delete",
            Description = "Delete",
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                NullValue = null,
                Padding = new Padding(0),
                BackColor = UiTheme.Surface,
                SelectionBackColor = UiTheme.RowSelected
            }
        };
    }

    /// <summary>
    /// After DataSource bind, force every row to show the outline action icons.
    /// </summary>
    public static void FillActionIcons(DataGridView grid)
    {
        var editImg = CreateOutlineEditIcon(16);
        var delImg = CreateOutlineDeleteIcon(16);

        foreach (DataGridViewRow row in grid.Rows)
        {
            if (row.IsNewRow) continue;
            if (grid.Columns.Contains("colEdit"))
                row.Cells["colEdit"].Value = editImg;
            if (grid.Columns.Contains("colDelete"))
                row.Cells["colDelete"].Value = delImg;
        }
    }

    public static Image? Dashboard => Get("icons-dashboard.png");
    public static Image? Products => Get("icons-totalProduct.png");
    public static Image? Category => Get("icons-catagory-tag.png");
    public static Image? Suppliers => Get("icons-suppliers.png");
    public static Image? Customers => Get("icons-customers.png");
    public static Image? StockIn => Get("icons-stockIn.png");
    public static Image? StockOut => Get("icons-stockOut.png");
    public static Image? Adjustment => Get("icons-Inventory-adjust.png");
    public static Image? History => Get("icons-history.png");
    public static Image? Report => Get("icons-report.png");
    public static Image? User => Get("icons-user.png");
    public static Image? Search => Get("icons-search.png");
    public static Image? Edit => CreateOutlineEditIcon(16);
    public static Image? Delete => CreateOutlineDeleteIcon(16);
    public static Image? Print => Get("icons-print.png");
    public static Image? Warning => Get("icons-warning.png");
    public static Image? Error => Get("icons-x-error.png");
    public static Image? Check => Get("icons-check.png");
    public static Image? Calendar => Get("icons-calendar.png");

    public static string? FileForNavKey(string key) => key switch
    {
        "Dashboard" => "icons-dashboard.png",
        "Products" => "icons-totalProduct.png",
        "Categories" => "icons-catagory-tag.png",
        "Suppliers" => "icons-suppliers.png",
        "Customers" => "icons-customers.png",
        "Stock In" => "icons-stockIn.png",
        "Stock Out" => "icons-stockOut.png",
        "Inventory Adjustment" => "icons-Inventory-adjust.png",
        "Inventory History" => "icons-history.png",
        "Reports" => "icons-report.png",
        "User Management" => "icons-user.png",
        "Delegations" => "icons-user.png",
        _ => null
    };
}
