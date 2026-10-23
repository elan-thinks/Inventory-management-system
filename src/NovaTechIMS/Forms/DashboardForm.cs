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
/// SCR-002 Dashboard — Donezo-style green metrics + insight charts.
/// </summary>
public partial class DashboardForm : Form
{
    // Palette lives only here (not in Designer) to avoid CS0102 on partial class.
    private static readonly Color AccentGreen = Color.FromArgb(15, 118, 70);
    private static readonly Color AccentGreenMid = Color.FromArgb(46, 160, 100);
    private static readonly Color BarMuted = Color.FromArgb(210, 230, 218);

    private DashboardService? _service;
    private readonly CurrentUser _user;
    private readonly Action<string>? _navigate;
    private bool _badgeWired;

    private int _healthy;
    private int _low;
    private int _out;
    private readonly List<(string Label, int TotalQty)> _movementBars = new();

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
        ApplyQuickActionIcons();
        ApplyQuickActionVisibility();
        WireChartPaint();

        Load += (_, _) =>
        {
            var first = _user.FullName.Split(' ')[0];
            lblWelcome.Text = $"Welcome, {first}";
            lblWelcomeSub.Text = "Track inventory health and daily stock movement.";
            PositionViewAllLink();
            Reload();
        };
        activityHeader.Resize += (_, _) => PositionViewAllLink();
        actionsPanel.Resize += (_, _) => ResizeQuickActionButtons();
    }

    private void WireChartPaint()
    {
        pnlStockHealth.Paint += PnlStockHealth_Paint;
        pnlMovement.Paint += PnlMovement_Paint;
        pnlStockHealth.Resize += (_, _) => pnlStockHealth.Invalidate();
        pnlMovement.Resize += (_, _) => pnlMovement.Invalidate();
    }

    private void PositionViewAllLink()
    {
        if (lnkViewAll is null || activityHeader is null) return;
        lnkViewAll.Left = Math.Max(80, activityHeader.ClientSize.Width - lnkViewAll.Width - 4);
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
        BackColor = Color.FromArgb(245, 247, 250);
        metricsPanel.BackColor = BackColor;
        welcomePanel.BackColor = BackColor;

        activityCard.BackColor = Color.White;
        insightsCard.BackColor = Color.White;
        actionsPanel.BackColor = Color.White;
        activityHeader.BackColor = Color.White;

        lblAct.ForeColor = Color.FromArgb(24, 32, 40);
        lblInsightsTitle.ForeColor = Color.FromArgb(24, 32, 40);
        lblQa.ForeColor = Color.FromArgb(24, 32, 40);
        lblEmpty.ForeColor = Color.FromArgb(120, 130, 145);
        lblError.ForeColor = UiTheme.Error;

        lnkViewAll.LinkColor = AccentGreen;
        lnkViewAll.ActiveLinkColor = AccentGreenMid;

        btnQaStockIn.BackColor = AccentGreen;
        btnQaStockIn.ForeColor = Color.White;
        btnQaStockIn.FlatAppearance.BorderSize = 0;

        foreach (var b in new[] { btnQaStockOut, btnQaProducts, btnQaAdjustment, btnQaUsers })
        {
            b.BackColor = Color.White;
            b.ForeColor = AccentGreen;
            b.FlatAppearance.BorderColor = Color.FromArgb(228, 232, 238);
            b.FlatAppearance.BorderSize = 1;
            b.TextAlign = ContentAlignment.MiddleLeft;
            b.Padding = new Padding(8, 0, 6, 0);
        }
        btnQaStockIn.TextAlign = ContentAlignment.MiddleLeft;
        btnQaStockIn.Padding = new Padding(8, 0, 6, 0);

        UiTheme.ApplyGridTheme(grid);
        ResizeQuickActionButtons();
    }

    private void ApplyQuickActionVisibility()
    {
        btnQaStockIn.Visible = AuthorizationService.HasPermission(_user, Permissions.StockIn);
        btnQaStockOut.Visible = AuthorizationService.HasPermission(_user, Permissions.StockOut);
        btnQaProducts.Visible = AuthorizationService.HasPermission(_user, Permissions.ViewProducts);
        btnQaAdjustment.Visible = AuthorizationService.HasPermission(_user, Permissions.PerformAdjustment);
        btnQaUsers.Visible = AuthorizationService.HasPermission(_user, Permissions.ManageUsers);

        int y = 42;
        foreach (Control c in actionsPanel.Controls)
        {
            if (c is Button { Visible: true } b)
            {
                b.Top = y;
                y += 44;
            }
        }
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
            subSuppliers.Text = "Active vendors";
            subTotalQty.Text = "Total quantity";
            subLow.Text = m.LowStockCount == 0 ? "All healthy" : "Needs attention";
            subOut.Text = m.OutOfStockCount == 0 ? "None out" : "Restock needed";

            _healthy = Math.Max(0, m.ActiveProductCount - m.LowStockCount - m.OutOfStockCount);
            _low = m.LowStockCount;
            _out = m.OutOfStockCount;

            var rows = Service.GetRecentActivity(40);
            BuildMovementBars(rows);
            pnlStockHealth.Invalidate();
            pnlMovement.Invalidate();

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
        for (int i = 6; i >= 0; i--)
        {
            var day = today.AddDays(-i);
            var dayRows = rows.Where(r => r.TransactionDate.Date == day);
            int total = dayRows.Sum(r => r.Quantity);
            _movementBars.Add((day.ToString("ddd")[..1], total));
        }
    }

    private void PnlStockHealth_Paint(object? sender, PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(Color.White);

        int total = _healthy + _low + _out;
        var rect = new Rectangle(24, 12, 110, 110);

        using (var track = new Pen(Color.FromArgb(230, 236, 232), 14))
            g.DrawEllipse(track, rect);

        if (total <= 0)
        {
            using (var emptyFont = new Font("Segoe UI", 9F, FontStyle.Bold))
            using (var emptyBrush = new SolidBrush(Color.FromArgb(140, 150, 160)))
                g.DrawString("No data", emptyFont, emptyBrush, 48, 58);
            DrawLegend(g, 150, 28);
            return;
        }

        float start = -90f;
        void Slice(int value, Color color)
        {
            if (value <= 0) return;
            float sweep = 360f * value / total;
            using (var slicePen = new Pen(color, 14) { StartCap = LineCap.Round, EndCap = LineCap.Round })
                g.DrawArc(slicePen, rect, start, sweep);
            start += sweep;
        }

        Slice(_healthy, AccentGreen);
        Slice(_low, Color.FromArgb(230, 160, 40));
        Slice(_out, Color.FromArgb(210, 70, 60));

        int healthyPct = (int)Math.Round(100.0 * _healthy / total);

        using (var pctFont = new Font("Segoe UI", 16F, FontStyle.Bold))
        using (var pctBrush = new SolidBrush(Color.FromArgb(24, 32, 40)))
        {
            var s = $"{healthyPct}%";
            var sz = g.MeasureString(s, pctFont);
            g.DrawString(s, pctFont, pctBrush, rect.X + (rect.Width - sz.Width) / 2, rect.Y + 36);
        }

        using (var labelFont = new Font("Segoe UI", 7.5F))
        using (var labelBrush = new SolidBrush(Color.FromArgb(120, 130, 145)))
        {
            const string s = "healthy";
            var sz = g.MeasureString(s, labelFont);
            g.DrawString(s, labelFont, labelBrush, rect.X + (rect.Width - sz.Width) / 2, rect.Y + 62);
        }

        DrawLegend(g, 150, 28);
    }

    private void DrawLegend(Graphics g, int x, int y)
    {
        void Row(Color c, string text, int count, int rowIndex)
        {
            int yy = y + rowIndex * 26;
            using (var brush = new SolidBrush(c))
                g.FillEllipse(brush, x, yy + 4, 10, 10);

            using (var legendFont = new Font("Segoe UI", 8F))
            using (var textBrush = new SolidBrush(Color.FromArgb(50, 60, 70)))
                g.DrawString($"{text}  ({count})", legendFont, textBrush, x + 16, yy);
        }

        Row(AccentGreen, "Healthy", _healthy, 0);
        Row(Color.FromArgb(230, 160, 40), "Low", _low, 1);
        Row(Color.FromArgb(210, 70, 60), "Out", _out, 2);
    }

    private void PnlMovement_Paint(object? sender, PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(Color.White);

        if (_movementBars.Count == 0)
        {
            using (var emptyFont = new Font("Segoe UI", 8.5F))
            using (var emptyBrush = new SolidBrush(Color.FromArgb(140, 150, 160)))
                g.DrawString("No movement yet", emptyFont, emptyBrush, 8, 20);
            return;
        }

        int max = Math.Max(1, _movementBars.Max(b => b.TotalQty));
        int left = 10;
        int bottom = pnlMovement.Height - 20;
        int top = 6;
        int chartH = Math.Max(24, bottom - top);
        int slotW = Math.Max(20, (pnlMovement.Width - 20) / _movementBars.Count);
        int barW = Math.Max(10, slotW - 8);

        using var dayFont = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        using var dayBrush = new SolidBrush(Color.FromArgb(120, 130, 145));

        for (int i = 0; i < _movementBars.Count; i++)
        {
            var (label, qty) = _movementBars[i];
            int x = left + i * slotW + (slotW - barW) / 2;

            using (var muted = new SolidBrush(BarMuted))
                FillRoundedRect(g, muted, x, top, barW, chartH, 6);

            int h = (int)(chartH * (qty / (double)max));
            if (h < 4 && qty > 0) h = 4;

            bool isPeak = qty == max && qty > 0;
            using (var barBrush = new SolidBrush(isPeak ? AccentGreen : AccentGreenMid))
            {
                if (h > 0)
                    FillRoundedRect(g, barBrush, x, bottom - h, barW, h, 6);
            }

            g.DrawString(label, dayFont, dayBrush, x + (barW - 8) / 2f, bottom + 2);
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
            e.CellStyle.ForeColor = UiTheme.Error;
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
