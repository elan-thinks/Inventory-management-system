using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NovaTechIMS.Utilities;

/// <summary>
/// Loads PNG icons from the Resources folder (copied next to the EXE).
/// Safe: returns null if a file is missing so UI still works.
/// </summary>
internal static class AppIcons
{
    private static readonly Dictionary<string, Image> Cache = new(StringComparer.OrdinalIgnoreCase);

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
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
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

    /// <summary>Icon-only Edit action column (icons-edit.png).</summary>
    public static DataGridViewImageColumn CreateEditColumn(float fillWeight = 0.4f)
    {
        var img = Get("icons-edit.png", 16);
        return new DataGridViewImageColumn
        {
            Name = "colEdit",
            HeaderText = "",
            Image = img,
            ImageLayout = DataGridViewImageCellLayout.Zoom,
            FillWeight = fillWeight,
            MinimumWidth = 36,
            ToolTipText = "Edit",
            Description = "Edit",
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                NullValue = null,
                BackColor = UiTheme.Surface,
                SelectionBackColor = UiTheme.RowSelected
            }
        };
    }

    /// <summary>Icon-only Delete action column (icon-delete.png).</summary>
    public static DataGridViewImageColumn CreateDeleteColumn(float fillWeight = 0.4f)
    {
        var img = Get("icon-delete.png", 16);
        return new DataGridViewImageColumn
        {
            Name = "colDelete",
            HeaderText = "",
            Image = img,
            ImageLayout = DataGridViewImageCellLayout.Zoom,
            FillWeight = fillWeight,
            MinimumWidth = 36,
            ToolTipText = "Delete",
            Description = "Delete",
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                NullValue = null,
                BackColor = UiTheme.Surface,
                SelectionBackColor = UiTheme.RowSelected
            }
        };
    }

    /// <summary>
    /// After DataSource bind, force every row to show the column icon
    /// (image columns need a non-null cell value or they stay blank).
    /// </summary>
    public static void FillActionIcons(DataGridView grid)
    {
        var editImg = Get("icons-edit.png", 16);
        var delImg = Get("icon-delete.png", 16);

        foreach (DataGridViewRow row in grid.Rows)
        {
            if (row.IsNewRow) continue;
            if (grid.Columns.Contains("colEdit") && editImg is not null)
                row.Cells["colEdit"].Value = editImg;
            if (grid.Columns.Contains("colDelete") && delImg is not null)
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
    public static Image? Edit => Get("icons-edit.png");
    public static Image? Delete => Get("icon-delete.png");
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
