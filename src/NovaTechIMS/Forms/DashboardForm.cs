using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Models;
using NovaTechIMS.Security;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms;

/// <summary>
/// SCR-002 Dashboard — soft metrics, charts, clock, quick actions.
/// </summary>
public partial class DashboardForm : Form
{
    private static readonly Color AccentGreen = Color.FromArgb(15, 118, 70);
    private static readonly Color AccentGreenMid = Color.FromArgb(52, 168, 110);
    private static readonly Color AccentGreenSoft = Color.FromArgb(200, 230, 210);
    private static readonly Color PageBg = Color.FromArgb(244, 245, 247);
    private static readonly Color TextMain = Color.FromArgb(26, 29, 33);
    private static readonly Color TextMuted = Color.FromArgb(139, 148, 158);

    private DashboardService? _service;
    private readonly CurrentUser _user;
    private readonly Action<string>? _navigate;
    private bool _badgeWired;

    private int _healthy;
    private int _low;
    private int _out;
    private readonly List<(string Label, int TotalQty)> _movementBars = new();
    private Label? _lblClock;
    private Label? _lblTodayMoves;
    private System.Windows.Forms.Timer? _clockTimer;

    private DashboardService Service => _service ??= new DashboardService();

    public DashboardForm()
        : this(DesignTime.PlaceholderUser, null)
    {
    }

    public DashboardForm(CurrentUser user, Action<string>? navigate = null)
    {
        _user = user ?? DesignTime.PlaceholderUser;
        _navigate = navigate;

        InitializeComponent();

        if (DesignTime.IsActive)
            return;

        ApplyRuntimeTouches();
        ApplyMetricIcons();
        ApplyQuickActionIcons();
        ApplyQuickActionVisibility();
        WireChartPaint();
        EnsureWelcomeExtras();

        Load += (_, _) =>
        {
            lblWelcome.Text = $"Welcome, {GetDisplayName()}";
            PositionViewAllLink();
            UpdateClock();
            Reload();
        };
        activityHeader.Resize += (_, _) => PositionViewAllLink();
        actionsPanel.Resize += (_, _) => ResizeQuickActionButtons();
    }

    private void ApplyMetricIcons()
    {
        void Set(PictureBox box, string file, bool onGreen)
        {
            var img = AppIcons.Get(file, 18);
            if (img is null) return;
            box.Image = img;
            box.Visible = true;
        }

        Set(icoProducts, "icons-totalProduct.png", true);
        Set(icoCategories, "icons-catagory-tag.png", false);
        Set(icoSuppliers, "icons-suppliers.png", false);
        Set(icoTotalQty, "icons-dashboard.png", false);
        Set(icoLow, "icons-warning.png", false);
        Set(icoOut, "icons-x-error.png", false);
    }

    private void WireChartPaint()
    {
        pnlStockHealth.Paint += PnlStockHealth_Paint;
        pnlMovement.Paint += PnlMovement_Paint;
        pnlStockHealth.Resize += (_, _) => pnlStockHealth.Invalidate();
        pnlMovement.Resize += (_, _) => pnlMovement.Invalidate();

        void SoftEdge(object? s, PaintEventArgs e)
        {
            if (s is not Panel p) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using var pen = new Pen(UiTheme.Border, 1);
            var r = new Rectangle(0, 0, p.Width - 1, p.Height - 1);
            using var path = UiTheme.RoundedRect(r, UiTheme.RadiusMd);
            e.Graphics.DrawPath(pen, path);
        }

        activityCard.Paint += SoftEdge;
        insightsCard.Paint += SoftEdge;
        actionsPanel.Paint += SoftEdge;
    }

    private void PositionViewAllLink()
    {
        if (lnkViewAll is null || activityHeader is null) return;
        lnkViewAll.Left = Math.Max(80, activityHeader.ClientSize.Width - lnkViewAll.Width - 2);
    }

    private void ResizeQuickActionButtons()
    {
        if (actionsPanel is null) return;
        int w = Math.Max(140, actionsPanel.ClientSize.Width - 24);
        foreach (Control c in actionsPanel.Controls)
        {
            if (c is Button b)
                b.Width = w;
        }
    }

    private void ApplyQuickActionIcons()
    {
        AppIcons.ApplyToButton(btnQaStockIn, "icons-stockIn.png", 16);
        AppIcons.ApplyToButton(btnQaStockOut, "icons-stockOut.png", 16);
        AppIcons.ApplyToButton(btnQaProducts, "icons-search.png", 16);
        AppIcons.ApplyToButton(btnQaAdjustment, "icons-Inventory-adjust.png", 16);
        AppIcons.ApplyToButton(btnQaUsers, "icons-user.png", 16);

        btnQaStockOut.ForeColor = AccentGreen;
        btnQaProducts.ForeColor = AccentGreen;
        btnQaAdjustment.ForeColor = AccentGreen;
        btnQaUsers.ForeColor = AccentGreen;
    }

    private void ApplyRuntimeTouches()
    {
        Font = UiTheme.Body;
        BackColor = UiTheme.Background;
        metricsPanel.BackColor = UiTheme.Background;
        welcomePanel.BackColor = UiTheme.Background;
        bottom.BackColor = UiTheme.Background;

        lblWelcome.Font = UiTheme.ScreenTitle;
        lblWelcome.ForeColor = UiTheme.Text;
        lblWelcomeSub.ForeColor = UiTheme.TextMuted;

        activityCard.BackColor = Color.White;
        insightsCard.BackColor = Color.White;
        actionsPanel.BackColor = Color.White;
        activityHeader.BackColor = Color.White;

        activityCard.BorderStyle = BorderStyle.None;
        insightsCard.BorderStyle = BorderStyle.None;
        actionsPanel.BorderStyle = BorderStyle.None;

        void StyleTile(Panel tile, Label value, Label sub, Label cap, Color tint, Color valueColor)
        {
            tile.BackColor = tint;
            tile.BorderStyle = BorderStyle.None;
            UiTheme.ApplyRoundedRegion(tile, UiTheme.RadiusMd);
            value.Font = UiTheme.MetricValue;
            value.ForeColor = valueColor;
            sub.ForeColor = UiTheme.TextMuted;
            cap.ForeColor = valueColor == Color.White ? Color.FromArgb(200, 232, 215) : UiTheme.TextMuted;
        }

        StyleTile(tileProducts, lblProducts, subProducts, capProducts, UiTheme.Primary, Color.White);
        StyleTile(tileCategories, lblCategories, subCategories, capCategories, UiTheme.TileBlue, UiTheme.Text);
        StyleTile(tileSuppliers, lblSuppliers, subSuppliers, capSuppliers, UiTheme.TilePurple, UiTheme.Text);
        StyleTile(tileTotalQty, lblTotalQty, subTotalQty, capTotalQty, UiTheme.TileGreen, UiTheme.Text);
        StyleTile(tileLow, lblLow, subLow, capLow, UiTheme.TileOrange, Color.FromArgb(180, 110, 20));
        StyleTile(tileOut, lblOut, subOut, capOut, UiTheme.TilePink, UiTheme.Error);

        foreach (var card in new[] { activityCard, insightsCard, actionsPanel })
            UiTheme.ApplyRoundedRegion(card, UiTheme.RadiusMd);

        lblAct.ForeColor = UiTheme.Text;
        lblInsightsTitle.ForeColor = UiTheme.Text;
        lblQa.ForeColor = UiTheme.Text;
        lblEmpty.ForeColor = UiTheme.TextMuted;
        lblError.ForeColor = UiTheme.Error;

        lnkViewAll.LinkColor = UiTheme.Primary;
        lnkViewAll.ActiveLinkColor = AccentGreenMid;

        btnQaStockIn.BackColor = UiTheme.Primary;
        btnQaStockIn.ForeColor = Color.White;
        btnQaStockIn.FlatAppearance.BorderSize = 0;
        UiTheme.ApplyRoundedRegion(btnQaStockIn, UiTheme.RadiusSm);

        foreach (var b in new[] { btnQaStockOut, btnQaProducts, btnQaAdjustment, btnQaUsers })
        {
            b.BackColor = UiTheme.Background;
            b.ForeColor = UiTheme.Primary;
            b.FlatAppearance.BorderSize = 0;
            b.TextAlign = ContentAlignment.MiddleLeft;
            b.Padding = new Padding(12, 0, 6, 0);
            UiTheme.ApplyRoundedRegion(b, UiTheme.RadiusSm);
        }
        btnQaStockIn.TextAlign = ContentAlignment.MiddleLeft;
        btnQaStockIn.Padding = new Padding(12, 0, 6, 0);

        grid.EnableHeadersVisualStyles = false;
        grid.BackgroundColor = Color.White;
        grid.BorderStyle = BorderStyle.None;
        grid.GridColor = UiTheme.BorderSubtle;
        grid.DefaultCellStyle.SelectionBackColor = UiTheme.RowSelected;
        grid.DefaultCellStyle.SelectionForeColor = UiTheme.Text;

        ResizeQuickActionButtons();
    }

    private void ApplyQuickActionVisibility()
    {
        btnQaStockIn.Visible = AuthorizationService.HasPermission(_user, Permissions.StockIn);
        btnQaStockOut.Visible = AuthorizationService.HasPermission(_user, Permissions.StockOut);
        btnQaProducts.Visible = AuthorizationService.HasPermission(_user, Permissions.ViewProducts);
        btnQaAdjustment.Visible = AuthorizationService.HasPermission(_user, Permissions.PerformAdjustment);
        btnQaUsers.Visible = AuthorizationService.HasPermission(_user, Permissions.ManageUsers);

        int y = 40;
        foreach (Control c in actionsPanel.Controls)
        {
            if (c is Button { Visible: true } b)
            {
                b.Top = y;
                y += 46;
            }
        }
    }

    private void EnsureWelcomeExtras()
    {
        metricsPanel.AutoScroll = false;
        welcomePanel.Height = 56;

        if (_lblClock is null)
        {
            _lblClock = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                ForeColor = UiTheme.Text,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Name = "lblClock"
            };
            welcomePanel.Controls.Add(_lblClock);
            welcomePanel.Resize += (_, _) => PositionClock();
        }

        if (_lblTodayMoves is null)
        {
            _lblTodayMoves = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9.25f),
                ForeColor = Color.FromArgb(90, 100, 112),
                Location = new Point(4, 32),
                Name = "lblTodayMoves"
            };
            welcomePanel.Controls.Add(_lblTodayMoves);
        }

        lblWelcomeSub.Visible = false;
        lblWelcomeSub.Text = "";

        if (_clockTimer is null)
        {
            _clockTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            _clockTimer.Tick += (_, _) => UpdateClock();
            _clockTimer.Start();
        }

        UpdateClock();
        PositionClock();
    }

    private void PositionClock()
    {
        if (_lblClock is null || welcomePanel is null) return;
        _lblClock.Left = Math.Max(200, welcomePanel.ClientSize.Width - _lblClock.PreferredWidth - 4);
        _lblClock.Top = 6;
    }

    private void UpdateClock()
    {
        if (_lblClock is null) return;
        var now = DateTime.Now;
        _lblClock.Text = now.ToString("dddd, d MMM yyyy  ·  HH:mm");
        PositionClock();
    }

    /// <summary>
    /// Prefer login username so we never show "System" from "System Administrator".
    /// </summary>
    private string GetDisplayName()
    {
        if (!string.IsNullOrWhiteSpace(_user.Username))
            return _user.Username.Trim();

        if (!string.IsNullOrWhiteSpace(_user.FullName))
        {
            var name = _user.FullName.Trim();
            if (name.StartsWith("System ", StringComparison.OrdinalIgnoreCase)
                || name.Equals("System", StringComparison.OrdinalIgnoreCase))
                return "Administrator";
            return name;
        }

        return "User";
    }

    private void Navigate(string key) => _navigate?.Invoke(key);

    private void BtnQaStockIn_Click(object? sender, EventArgs e) => Navigate("Stock In");
    private void BtnQaStockOut_Click(object? sender, EventArgs e) => Navigate("Stock Out");
    private void BtnQaProducts_Click(object? sender, EventArgs e) => Navigate("Products");
    private void BtnQaAdjustment_Click(object? sender, EventArgs e) => Navigate("Inventory Adjustment");
    private void BtnQaUsers_Click(object? sender, EventArgs e) => Navigate("User Management");

    private void LnkViewAll_Click(object? sender, EventArgs e)
    {
        if (DesignTime.IsActive) return;
        if (AuthorizationService.HasPermission(_user, Permissions.ViewInventoryHistory))
            _navigate?.Invoke("Inventory History");
    }

    private void Reload()
    {
        lblError.Visible = false;

        try
        {
            var m = Service.GetMetrics();
            var nfi = CultureInfo.CurrentCulture.NumberFormat;

            lblProducts.Text = m.ActiveProductCount.ToString("N0", nfi);
            lblCategories.Text = m.CategoryCount.ToString("N0", nfi);
            lblSuppliers.Text = m.SupplierCount.ToString("N0", nfi);
            lblTotalQty.Text = m.TotalQuantityOnHand.ToString("N0", nfi);
            lblLow.Text = m.LowStockCount.ToString("N0", nfi);
            lblOut.Text = m.OutOfStockCount.ToString("N0", nfi);

            subProducts.Text = "In catalog";
            subCategories.Text = "Groupings";
            subSuppliers.Text = "Vendors";
            subTotalQty.Text = "Total stock";
            subLow.Text = m.LowStockCount == 0 ? "All healthy" : "Needs attention";
            subOut.Text = m.OutOfStockCount == 0 ? "None out" : "Restock needed";

            _healthy = Math.Max(0, m.ActiveProductCount - m.LowStockCount - m.OutOfStockCount);
            _low = m.LowStockCount;
            _out = m.OutOfStockCount;

            var rows = Service.GetRecentActivity(40);
            BuildMovementBars(rows);
            pnlStockHealth.Invalidate();
            pnlMovement.Invalidate();

            int todayCount = rows.Count(r => r.TransactionDate.Date == DateTime.Today);
            if (_lblTodayMoves is not null)
            {
                _lblTodayMoves.ForeColor = Color.FromArgb(90, 100, 112);
                _lblTodayMoves.Text = todayCount == 0
                    ? "No stock movements recorded today."
                    : $"{todayCount} stock movement{(todayCount == 1 ? "" : "s")} today · overview of inventory health.";
            }

            var display = rows.Take(10).ToList();
            if (display.Count == 0)
            {
                grid.Visible = false;
                lblEmpty.Visible = true;
            }
            else
            {
                lblEmpty.Visible = false;
                grid.Visible = true;
                grid.DataSource = null;
                grid.Columns.Clear();
                grid.DataSource = display;

                foreach (DataGridViewColumn col in grid.Columns)
                    col.Visible = false;

                int di = 0;
                void Show(string name, string header, float w)
                {
                    if (grid.Columns[name] is not DataGridViewColumn c) return;
                    c.Visible = true;
                    c.HeaderText = header;
                    c.FillWeight = w;
                    c.DisplayIndex = di++;
                }

                Show(nameof(TransactionHistoryRow.TransactionDate), "Date", 0.9f);
                Show(nameof(TransactionHistoryRow.TypeLabel), "Type", 0.85f);
                Show(nameof(TransactionHistoryRow.ProductName), "Product", 1.4f);
                Show(nameof(TransactionHistoryRow.Quantity), "Qty", 0.5f);
                Show(nameof(TransactionHistoryRow.UserFullName), "User", 1.0f);

                if (grid.Columns[nameof(TransactionHistoryRow.TransactionDate)] is DataGridViewColumn dateCol)
                    dateCol.DefaultCellStyle.Format = "g";

                grid.CellFormatting -= Grid_CellFormattingQty;
                grid.CellFormatting += Grid_CellFormattingQty;

                if (!_badgeWired)
                {
                    UiTheme.WireStatusBadgeColumn(grid, nameof(TransactionHistoryRow.TypeLabel),
                        value => (value?.ToString() ?? "") switch
                        {
                            "Stock-In" => ("Stock In", UiTheme.BadgeTone.Success, '\u2713'),
                            "Stock-Out" => ("Stock Out", UiTheme.BadgeTone.Error, '\u2715'),
                            _ => ("Adjustment", UiTheme.BadgeTone.Info, '!')
                        });
                    _badgeWired = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblProducts.Text = "—";
            lblCategories.Text = "—";
            lblSuppliers.Text = "—";
            lblTotalQty.Text = "—";
            lblLow.Text = "—";
            lblOut.Text = "—";

            var mapped = DbExceptionMapper.Map(ex);
            var friendly = ErrorPresenter.Classify(mapped).Message;
            lblError.Text = "Unable to load dashboard data. " + friendly;
            lblError.Visible = true;
        }
    }

    private void BuildMovementBars(IReadOnlyList<TransactionHistoryRow> rows)
    {
        _movementBars.Clear();
        var today = DateTime.Today;
        string[] labels = { "Mo", "Tu", "We", "Th", "Fr", "Sa", "Su" };
        for (int i = 6; i >= 0; i--)
        {
            var day = today.AddDays(-i);
            int idx = ((int)day.DayOfWeek + 6) % 7;
            var dayRows = rows.Where(r => r.TransactionDate.Date == day);
            int total = dayRows.Sum(r => r.Quantity);
            _movementBars.Add((labels[idx], total));
        }
    }

    private void PnlStockHealth_Paint(object? sender, PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(Color.White);

        int total = _healthy + _low + _out;
        var rect = new Rectangle(18, 14, 110, 110);

        using (var track = new Pen(Color.FromArgb(236, 242, 238), 12))
            g.DrawEllipse(track, rect);

        if (total <= 0)
        {
            using (var emptyFont = new Font("Segoe UI", 9F))
            using (var emptyBrush = new SolidBrush(TextMuted))
                g.DrawString("No data", emptyFont, emptyBrush, 42, 58);
            DrawLegend(g, 146, 30);
            return;
        }

        float start = -90f;
        void Slice(int value, Color color)
        {
            if (value <= 0) return;
            float sweep = 360f * value / total;
            using (var slicePen = new Pen(color, 12) { StartCap = LineCap.Round, EndCap = LineCap.Round })
                g.DrawArc(slicePen, rect, start, sweep);
            start += sweep;
        }

        Slice(_healthy, AccentGreen);
        Slice(_low, Color.FromArgb(230, 160, 40));
        Slice(_out, Color.FromArgb(210, 75, 65));

        int healthyPct = (int)Math.Round(100.0 * _healthy / total);

        using (var pctFont = new Font("Segoe UI", 18F, FontStyle.Bold))
        using (var pctBrush = new SolidBrush(TextMain))
        {
            var s = $"{healthyPct}%";
            var sz = g.MeasureString(s, pctFont);
            g.DrawString(s, pctFont, pctBrush, rect.X + (rect.Width - sz.Width) / 2, rect.Y + 34);
        }

        using (var labelFont = new Font("Segoe UI", 7.5F))
        using (var labelBrush = new SolidBrush(TextMuted))
        {
            const string s = "healthy";
            var sz = g.MeasureString(s, labelFont);
            g.DrawString(s, labelFont, labelBrush, rect.X + (rect.Width - sz.Width) / 2, rect.Y + 62);
        }

        DrawLegend(g, 146, 30);
    }

    private void DrawLegend(Graphics g, int x, int y)
    {
        void Row(Color c, string text, int count, int rowIndex)
        {
            int yy = y + rowIndex * 28;
            using (var brush = new SolidBrush(c))
                g.FillEllipse(brush, x, yy + 5, 9, 9);

            using (var legendFont = new Font("Segoe UI", 8.5F))
            using (var textBrush = new SolidBrush(TextMain))
                g.DrawString($"{text}  ({count})", legendFont, textBrush, x + 16, yy);
        }

        Row(AccentGreen, "Healthy", _healthy, 0);
        Row(Color.FromArgb(230, 160, 40), "Low", _low, 1);
        Row(Color.FromArgb(210, 75, 65), "Out", _out, 2);
    }

    private void PnlMovement_Paint(object? sender, PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(Color.White);

        if (_movementBars.Count == 0)
        {
            using (var emptyFont = new Font("Segoe UI", 8.5F))
            using (var emptyBrush = new SolidBrush(TextMuted))
                g.DrawString("No movement yet", emptyFont, emptyBrush, 8, 20);
            return;
        }

        int max = Math.Max(1, _movementBars.Max(b => b.TotalQty));
        int left = 8;
        int bottomY = pnlMovement.Height - 22;
        int top = 8;
        int chartH = Math.Max(28, bottomY - top);
        int slotW = Math.Max(22, (pnlMovement.Width - 16) / _movementBars.Count);
        int barW = Math.Max(12, slotW - 10);

        using var dayFont = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        using var dayBrush = new SolidBrush(TextMuted);

        for (int i = 0; i < _movementBars.Count; i++)
        {
            var (label, qty) = _movementBars[i];
            int x = left + i * slotW + (slotW - barW) / 2;

            using (var muted = new SolidBrush(AccentGreenSoft))
                FillRoundedRect(g, muted, x, top, barW, chartH, 8);

            int h = (int)(chartH * (qty / (double)max));
            if (h < 6 && qty > 0) h = 6;

            bool isPeak = qty == max && qty > 0;
            using (var barBrush = new SolidBrush(isPeak ? AccentGreen : AccentGreenMid))
            {
                if (h > 0)
                    FillRoundedRect(g, barBrush, x, bottomY - h, barW, h, 8);
            }

            var labelSize = g.MeasureString(label, dayFont);
            g.DrawString(label, dayFont, dayBrush, x + (barW - labelSize.Width) / 2f, bottomY + 3);
        }
    }

    private static void FillRoundedRect(Graphics g, Brush brush, int x, int y, int w, int h, int r)
    {
        if (w <= 0 || h <= 0) return;
        r = Math.Min(r, Math.Min(w, h) / 2);
        using var path = new GraphicsPath();
        path.AddArc(x, y, r * 2, r * 2, 180, 90);
        path.AddArc(x + w - r * 2, y, r * 2, r * 2, 270, 90);
        path.AddArc(x + w - r * 2, y + h - r * 2, r * 2, r * 2, 0, 90);
        path.AddArc(x, y + h - r * 2, r * 2, r * 2, 90, 90);
        path.CloseFigure();
        g.FillPath(brush, path);
    }

    private void Grid_CellFormattingQty(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0) return;
        if (grid.Columns[e.ColumnIndex].Name != nameof(TransactionHistoryRow.Quantity)) return;
        if (grid.Rows[e.RowIndex].DataBoundItem is not TransactionHistoryRow row) return;
        if (e.Value is not int q) return;

        if (row.TypeLabel == "Stock-Out" || (row.TypeLabel == "Adjustment" && row.Difference < 0))
        {
            e.Value = q > 0 ? $"−{q}" : q.ToString();
            e.FormattingApplied = true;
            e.CellStyle.ForeColor = Color.FromArgb(180, 55, 50);
            e.CellStyle.Font = UiTheme.LabelSemibold;
        }
        else if (row.TypeLabel == "Stock-In" || (row.TypeLabel == "Adjustment" && row.Difference > 0))
        {
            e.Value = q > 0 ? $"+{q}" : q.ToString();
            e.FormattingApplied = true;
            e.CellStyle.ForeColor = AccentGreenMid;
            e.CellStyle.Font = UiTheme.LabelSemibold;
        }
    }
}
