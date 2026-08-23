using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NovaTechIMS.Utilities;

/// <summary>
/// Dark-mode design tokens (system-wide).
/// </summary>
internal static class UiTheme
{
    // ---- Dark color tokens ------------------------------------------------
    public static readonly Color Primary = ColorTranslator.FromHtml("#3B82F6");
    public static readonly Color PrimaryHover = ColorTranslator.FromHtml("#2563EB");
    public static readonly Color Secondary = ColorTranslator.FromHtml("#38BDF8");
    public static readonly Color Background = ColorTranslator.FromHtml("#0F1419");
    public static readonly Color Surface = ColorTranslator.FromHtml("#1A2332");
    public static readonly Color Text = ColorTranslator.FromHtml("#E8EEF5");
    public static readonly Color TextMuted = ColorTranslator.FromHtml("#8B9BB4");

    public static readonly Color Success = ColorTranslator.FromHtml("#34D399");
    public static readonly Color SuccessTint = ColorTranslator.FromHtml("#0F2E22");
    public static readonly Color Warning = ColorTranslator.FromHtml("#FBBF24");
    public static readonly Color WarningTint = ColorTranslator.FromHtml("#3D2E0A");
    public static readonly Color Error = ColorTranslator.FromHtml("#F87171");
    public static readonly Color ErrorTint = ColorTranslator.FromHtml("#3B1515");
    public static readonly Color Info = ColorTranslator.FromHtml("#60A5FA");
    public static readonly Color InfoTint = ColorTranslator.FromHtml("#0F2744");

    public static readonly Color Border = ColorTranslator.FromHtml("#2D3A4F");
    public static readonly Color BorderFocus = ColorTranslator.FromHtml("#3B82F6");
    public static readonly Color DisabledBackground = ColorTranslator.FromHtml("#1E2736");
    public static readonly Color DisabledText = ColorTranslator.FromHtml("#5A6A80");

    public static readonly Color RowHover = ColorTranslator.FromHtml("#243044");
    public static readonly Color RowSelected = ColorTranslator.FromHtml("#1E3A5F");
    public static readonly Color RowAlternate = ColorTranslator.FromHtml("#151D2A");

    public static readonly Color SidebarBackground = ColorTranslator.FromHtml("#0B1220");
    public static readonly Color SidebarText = ColorTranslator.FromHtml("#9AADC4");
    public static readonly Color SidebarTextActive = Color.White;
    public static readonly Color SidebarAccent = ColorTranslator.FromHtml("#3B82F6");
    public static readonly Color StatusStripBackground = ColorTranslator.FromHtml("#121A27");

    // ---- Typography ---------------------------------------------------
    public static readonly Font ScreenTitle = new("Segoe UI", 14f, FontStyle.Bold);
    public static readonly Font SectionTitle = new("Segoe UI", 11f, FontStyle.Bold);
    public static readonly Font Body = new("Segoe UI", 10f, FontStyle.Regular);
    public static readonly Font Label = new("Segoe UI", 9f, FontStyle.Regular);
    public static readonly Font LabelSemibold = new("Segoe UI", 9f, FontStyle.Bold);
    public static readonly Font Button = new("Segoe UI", 10f, FontStyle.Bold);
    public static readonly Font HelperText = new("Segoe UI", 8.5f, FontStyle.Italic);
    public static readonly Font ErrorText = new("Segoe UI", 8.5f, FontStyle.Regular);
    public static readonly Font BadgeText = new("Segoe UI", 8.5f, FontStyle.Bold);
    public static readonly Font SidebarItem = new("Segoe UI", 9.5f, FontStyle.Regular);
    public static readonly Font SidebarItemActive = new("Segoe UI", 9.5f, FontStyle.Bold);
    public static readonly Font SidebarGroup = new("Segoe UI", 8f, FontStyle.Bold);
    public static readonly Font StatusStrip = new("Segoe UI", 9f, FontStyle.Regular);

    // ---- Spacing / radius / dimensions ---------------------------------
    public const int Sp1 = 4, Sp2 = 8, Sp3 = 12, Sp4 = 16, Sp6 = 24, Sp8 = 32;
    public const int RadiusSm = 4, RadiusLg = 6;
    public const int SidebarWidth = 220;
    public const int MinWindowWidth = 1024;
    public const int MinWindowHeight = 768;
    public const int InputHeight = 28;
    public const int ButtonHeight = 32;
    public const int GridRowHeight = 30;

    public enum ButtonKind { Primary, Secondary, Danger, Ghost }

    public static GraphicsPath RoundedRect(Rectangle bounds, int radius)
    {
        var path = new GraphicsPath();
        if (radius <= 0)
        {
            path.AddRectangle(bounds);
            return path;
        }

        int d = radius * 2;
        path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
        path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
        path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
        path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
        path.CloseFigure();
        return path;
    }

    public static void ApplyRoundedRegion(Control control, int radius = RadiusSm)
    {
        void Apply()
        {
            if (control.Width <= 0 || control.Height <= 0) return;
            using var path = RoundedRect(new Rectangle(0, 0, control.Width, control.Height), radius);
            control.Region = new Region(path);
        }
        Apply();
        control.Resize += (_, _) => Apply();
    }

    public static Button StyleButton(Button btn, ButtonKind kind)
    {
        btn.FlatStyle = FlatStyle.Flat;
        btn.Cursor = Cursors.Hand;
        btn.Font = Button;
        btn.Height = Math.Max(btn.Height, ButtonHeight);
        btn.FlatAppearance.BorderSize = kind == ButtonKind.Secondary ? 1 : 0;
        btn.TextAlign = ContentAlignment.MiddleCenter;
        btn.UseVisualStyleBackColor = false;

        Color baseColor, hoverColor, textColor;
        switch (kind)
        {
            case ButtonKind.Primary:
                baseColor = Primary; hoverColor = PrimaryHover; textColor = Color.White;
                break;
            case ButtonKind.Danger:
                baseColor = Error; hoverColor = ColorTranslator.FromHtml("#DC2626"); textColor = Color.White;
                break;
            case ButtonKind.Secondary:
                baseColor = Surface; hoverColor = RowHover; textColor = Text;
                btn.FlatAppearance.BorderColor = Border;
                break;
            default:
                baseColor = Background; hoverColor = RowHover; textColor = Primary;
                btn.Font = Label;
                break;
        }

        btn.BackColor = baseColor;
        btn.ForeColor = textColor;
        btn.FlatAppearance.MouseOverBackColor = hoverColor;
        btn.FlatAppearance.MouseDownBackColor = hoverColor;
        return btn;
    }

    public static Button CreateButton(string text, ButtonKind kind, int width = 120, int height = ButtonHeight)
    {
        var btn = new Button { Text = text, Width = width, Height = height };
        return StyleButton(btn, kind);
    }

    public static TextBox StyleTextBox(TextBox box)
    {
        box.Font = Body;
        box.BorderStyle = BorderStyle.FixedSingle;
        box.BackColor = Surface;
        box.ForeColor = Text;
        return box;
    }

    public static ComboBox StyleComboBox(ComboBox combo)
    {
        combo.Font = Body;
        combo.FlatStyle = FlatStyle.Flat;
        combo.BackColor = Surface;
        combo.ForeColor = Text;
        return combo;
    }

    public static DateTimePicker StyleDatePicker(DateTimePicker picker)
    {
        picker.Font = Body;
        picker.CalendarForeColor = Text;
        picker.CalendarMonthBackground = Surface;
        return picker;
    }

    public static void WireFocusBorder(Control input, Panel wrapper)
    {
        wrapper.Paint += (_, e) =>
        {
            using var pen = new Pen(input.Focused ? BorderFocus : Border, input.Focused ? 2 : 1);
            e.Graphics.DrawRectangle(pen, 0, 0, wrapper.Width - 1, wrapper.Height - 1);
        };
        input.Enter += (_, _) => wrapper.Invalidate();
        input.Leave += (_, _) => wrapper.Invalidate();
    }

    public enum BadgeTone { Success, Warning, Error, Muted, Info }

    private static (Color Fore, Color Tint) BadgeColors(BadgeTone tone) => tone switch
    {
        BadgeTone.Success => (Success, SuccessTint),
        BadgeTone.Warning => (Warning, WarningTint),
        BadgeTone.Error => (Error, ErrorTint),
        BadgeTone.Info => (Info, InfoTint),
        _ => (TextMuted, DisabledBackground),
    };

    private static void DrawGlyph(Graphics g, char glyph, Rectangle bounds, Color color)
    {
        using var pen = new Pen(color, 1.6f) { StartCap = LineCap.Round, EndCap = LineCap.Round };
        g.SmoothingMode = SmoothingMode.AntiAlias;
        var r = bounds;
        switch (glyph)
        {
            case '\u2713':
                g.DrawLines(pen, new[]
                {
                    new PointF(r.Left, r.Top + r.Height * 0.55f),
                    new PointF(r.Left + r.Width * 0.4f, r.Bottom - 1),
                    new PointF(r.Right - 1, r.Top + 1)
                });
                break;
            case '\u2715':
                g.DrawLine(pen, r.Left, r.Top, r.Right, r.Bottom);
                g.DrawLine(pen, r.Right, r.Top, r.Left, r.Bottom);
                break;
            case '!':
                g.DrawPolygon(pen, new[]
                {
                    new PointF(r.Left + r.Width / 2f, r.Top),
                    new PointF(r.Right, r.Bottom),
                    new PointF(r.Left, r.Bottom)
                });
                g.DrawLine(pen, r.Left + r.Width / 2f, r.Top + r.Height * 0.4f, r.Left + r.Width / 2f, r.Top + r.Height * 0.7f);
                using (var dotBrush = new SolidBrush(color))
                    g.FillEllipse(dotBrush, r.Left + r.Width / 2f - 0.75f, r.Bottom - 2.5f, 1.5f, 1.5f);
                break;
            case 'c':
                g.DrawEllipse(pen, r);
                g.DrawLine(pen, r.Left + r.Width / 2f, r.Top + r.Height * 0.25f, r.Left + r.Width / 2f, r.Top + r.Height / 2f);
                g.DrawLine(pen, r.Left + r.Width / 2f, r.Top + r.Height / 2f, r.Right - r.Width * 0.2f, r.Top + r.Height * 0.55f);
                break;
            default:
                using (var dotBrush = new SolidBrush(color))
                    g.FillEllipse(dotBrush, r);
                break;
        }
    }

    public static void PaintBadge(Graphics g, Rectangle cellBounds, string label, BadgeTone tone, char glyph)
    {
        var (fore, tint) = BadgeColors(tone);
        g.SmoothingMode = SmoothingMode.AntiAlias;

        var font = BadgeText;
        var textSize = g.MeasureString(label, font);
        int iconSize = 10;
        int pad = 8;
        int pillWidth = (int)textSize.Width + iconSize + pad * 3;
        int pillHeight = Math.Min(cellBounds.Height - 6, 20);
        var pillRect = new Rectangle(
            cellBounds.X + 6,
            cellBounds.Y + (cellBounds.Height - pillHeight) / 2,
            pillWidth,
            pillHeight);

        using (var path = RoundedRect(pillRect, pillHeight / 2))
        using (var brush = new SolidBrush(tint))
            g.FillPath(brush, path);

        var iconRect = new Rectangle(pillRect.X + pad, pillRect.Y + (pillHeight - iconSize) / 2, iconSize, iconSize);
        DrawGlyph(g, glyph, iconRect, fore);

        var textRect = new RectangleF(iconRect.Right + 5, pillRect.Y, textSize.Width + pad, pillHeight);
        using var textBrush = new SolidBrush(fore);
        using var sf = new StringFormat { LineAlignment = StringAlignment.Center };
        g.DrawString(label, font, textBrush, textRect, sf);
    }

    public static void ApplyGridTheme(DataGridView grid)
    {
        grid.BackgroundColor = Surface;
        grid.BorderStyle = BorderStyle.None;
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        grid.GridColor = Border;
        grid.RowHeadersVisible = false;
        grid.EnableHeadersVisualStyles = false;
        grid.AllowUserToResizeRows = false;
        grid.Font = Body;
        grid.RowTemplate.Height = GridRowHeight;

        grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
        {
            Font = LabelSemibold,
            BackColor = StatusStripBackground,
            ForeColor = Text,
            Alignment = DataGridViewContentAlignment.MiddleLeft,
            Padding = new Padding(Sp2, 0, 0, 0)
        };
        grid.ColumnHeadersHeight = 34;
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

        grid.DefaultCellStyle = new DataGridViewCellStyle
        {
            BackColor = Surface,
            ForeColor = Text,
            SelectionBackColor = RowSelected,
            SelectionForeColor = Text,
            Padding = new Padding(Sp2, 0, 0, 0)
        };
        grid.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
        {
            BackColor = RowAlternate,
            SelectionBackColor = RowSelected,
            SelectionForeColor = Text
        };

        int hoverRow = -1;
        grid.CellMouseEnter += (_, e) =>
        {
            if (e.RowIndex < 0 || e.RowIndex == hoverRow) return;
            hoverRow = e.RowIndex;
            grid.InvalidateRow(e.RowIndex);
        };
        grid.CellMouseLeave += (_, e) =>
        {
            if (e.RowIndex < 0) return;
            hoverRow = -1;
            grid.InvalidateRow(e.RowIndex);
        };
        grid.CellPainting += (_, e) =>
        {
            if (e.RowIndex == hoverRow && e.RowIndex >= 0
                && !grid.Rows[e.RowIndex].Selected
                && (e.State & DataGridViewElementStates.Selected) == 0)
            {
                using var brush = new SolidBrush(RowHover);
                e.Graphics?.FillRectangle(brush, e.CellBounds);
                e.PaintContent(e.CellBounds);
                e.Handled = true;
            }
        };
    }

    public static void WireStatusBadgeColumn(
        DataGridView grid,
        string columnName,
        Func<object?, (string Label, BadgeTone Tone, char Glyph)> valueMap)
    {
        grid.CellPainting += (_, e) =>
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (grid.Columns[e.ColumnIndex].Name != columnName) return;

            var (label, tone, glyph) = valueMap(e.Value);
            bool selected = grid.Rows[e.RowIndex].Selected;
            var bg = selected ? RowSelected : (e.RowIndex % 2 == 1 ? RowAlternate : Surface);
            using var bgBrush = new SolidBrush(bg);
            e.Graphics?.FillRectangle(bgBrush, e.CellBounds);
            if (e.Graphics != null) PaintBadge(e.Graphics, e.CellBounds, label, tone, glyph);
            e.Handled = true;
        };
    }

    public static Label CreateEmptyStateLabel(string text)
    {
        return new Label
        {
            Text = text,
            Font = Body,
            ForeColor = TextMuted,
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Fill,
            Visible = false
        };
    }
}
