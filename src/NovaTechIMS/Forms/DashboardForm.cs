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
/// SCR-002 Dashboard — metrics, insight charts, activity, quick actions.
/// </summary>
public partial class DashboardForm : Form
{
    private DashboardService? _service;
    private readonly CurrentUser _user;
    private readonly Action<string>? _navigate;
    private bool _badgeWired;

    private int _healthy;
    private int _low;
    private int _out;
    private readonly List<(string Label, int InQty, int OutQty)> _movementBars = new();

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

        Load += (_, _) =>
        {
            var first = _user.FullName.Split(' ')[0];
            lblWelcome.Text = $"Welcome, {first} 👋";
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

    private void ApplyMetricIcons()
    {
        void Set(PictureBox box, string file)
        {
            var img = AppIcons.Get(file, 18);
            if (img is not null)
                box.Image = img;
        }

        Set(icoProducts, "icons-totalProduct.png");
        Set(icoCategories, "icons-catagory-tag.png");
        Set(icoSuppliers, "icons-suppliers.png");
        Set(icoTotalQty, "icons-dashboard.png");
        Set(icoLow, "icons-warning.png");
        Set(icoOut, "icons-x-error.png");
    }

    private void ApplyQuickActionIcons()
    {
        AppIcons.ApplyToButton(btnQaStockIn, "icons-stockIn.png", 16);
        AppIcons.ApplyToButton(btnQaStockOut, "icons-stockOut.png", 16);
        AppIcons.ApplyToButton(btnQaProducts, "icons-search.png", 16);
        AppIcons.ApplyToButton(btnQaAdjustment, "icons-Inventory-adjust.png", 16);
        AppIcons.ApplyToButton(btnQaUsers, "icons-user.png", 16);

        btnQaStockOut.ForeColor = UiTheme.Primary;
        btnQaProducts.ForeColor = UiTheme.Primary;
        btnQaAdjustment.ForeColor = UiTheme.Primary;
        btnQaUsers.ForeColor = UiTheme.Primary;
    }

    private void ApplyRuntimeTouches()
    {
        Font = UiTheme.Body;
        BackColor = UiTheme.Background;
        metricsPanel.BackColor = UiTheme.Background;
        welcomePanel.BackColor = UiTheme.Background;

        activityCard.BackColor = UiTheme.Surface;
        insightsCard.BackColor = UiTheme.Surface;
        actionsPanel.BackColor = UiTheme.Surface;
        activityHeader.BackColor = UiTheme.Surface;

        lblAct.Font = UiTheme.SectionTitle;
        lblAct.ForeColor = UiTheme.Text;
        lblInsightsTitle.Font = UiTheme.SectionTitle;
        lblInsightsTitle.ForeColor = UiTheme.Text;
        lblQa.Font = UiTheme.SectionTitle;
        lblQa.ForeColor = UiTheme.Text;
        lblEmpty.Font = UiTheme.Body;
        lblEmpty.ForeColor = UiTheme.TextMuted;
        lblError.Font = UiTheme.Label;
        lblError.ForeColor = UiTheme.Error;

        lnkViewAll.LinkColor = UiTheme.Primary;
        lnkViewAll.ActiveLinkColor = UiTheme.PrimaryHover;

        UiTheme.StyleButton(btnQaStockIn, UiTheme.ButtonKind.Primary);
        UiTheme.StyleButton(btnQaStockOut, UiTheme.ButtonKind.Secondary);
        UiTheme.StyleButton(btnQaProducts, UiTheme.ButtonKind.Secondary);
        UiTheme.StyleButton(btnQaAdjustment, UiTheme.ButtonKind.Secondary);
        UiTheme.StyleButton(btnQaUsers, UiTheme.ButtonKind.Secondary);

        foreach (var b in new[] { btnQaStockOut, btnQaProducts, btnQaAdjustment, btnQaUsers })
        {
            b.ForeColor = UiTheme.Primary;
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

            subProducts.Text = m.ActiveProductCount == 1 ? "1 active item" : $"{m.ActiveProductCount:N0} active items";
            subCategories.Text = "Product groupings";
            subSuppliers.Text = "Active vendors";
            subTotalQty.Text = "Across all products";
            subLow.Text = m.LowStockCount == 0 ? "All healthy ✓" : "Needs attention";
            subOut.Text = m.OutOfStockCount == 0 ? "None out of stock ✓" : "Restock needed";

            _healthy = Math.Max(0, m.ActiveProductCount - m.LowStockCount - m.OutOfStockCount);
            _low = m.LowStockCount;
            _out = m.OutOfStockCount;

            var rows = Service.GetRecentActivity(30);
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
            var dayRows = rows.Where(r => r.TransactionDate.Date == day).ToList();
            int inQty = dayRows.Where(r => r.TypeLabel == "Stock-In").Sum(r => r.Quantity);
            int outQty = dayRows.Where(r => r.TypeLabel == "Stock-Out").Sum(r => r.Quantity);
            _movementBars.Add((day.ToString("ddd"), inQty, outQty));
        }
    }

    private void PnlStockHealth_Paint(object? sender, PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(Color.White);

        int total = _healthy + _low + _out;
        var rect = new Rectangle(18, 10, 100, 100);

        if (total <= 0)
        {
            using (var emptyPen = new Pen(Color.FromArgb(220, 225, 230), 14))
                g.DrawEllipse(emptyPen, rect);

            using (var emptyFont = new Font("Segoe UI", 9F, FontStyle.Bold))
            using (var emptyBrush = new SolidBrush(UiTheme.TextMuted))
                g.DrawString("No data", emptyFont, emptyBrush, 38, 52);

            DrawLegend(g, 130, 20);
            return;
        }

        float start = -90f;
        void Slice(int value, Color color)
        {
            if (value <= 0) return;
            float sweep = 360f * value / total;
            using (var slicePen = new Pen(color, 16) { StartCap = LineCap.Round, EndCap = LineCap.Round })
                g.DrawArc(slicePen, rect, start, sweep);
            start += sweep;
        }

        Slice(_healthy, Color.FromArgb(46, 160, 100));
        Slice(_low, Color.FromArgb(230, 160, 40));
        Slice(_out, Color.FromArgb(210, 70, 60));

        int healthyPct = (int)Math.Round(100.0 * _healthy / total);

        using (var pctFont = new Font("Segoe UI", 14F, FontStyle.Bold))
        using (var pctBrush = new SolidBrush(UiTheme.Text))
        {
            var s = $"{healthyPct}%";
            var sz = g.MeasureString(s, pctFont);
            g.DrawString(s, pctFont, pctBrush, rect.X + (rect.Width - sz.Width) / 2, rect.Y + 34);
        }

        using (var labelFont = new Font("Segoe UI", 7.5F))
        using (var labelBrush = new SolidBrush(UiTheme.TextMuted))
        {
            const string s = "healthy";
            var sz = g.MeasureString(s, labelFont);
            g.DrawString(s, labelFont, labelBrush, rect.X + (rect.Width - sz.Width) / 2, rect.Y + 58);
        }

        DrawLegend(g, 130, 18);
    }

    private void DrawLegend(Graphics g, int x, int y)
    {
        void Row(Color c, string text, int count, int rowIndex)
        {
            int yy = y + rowIndex * 28;
            using (var brush = new SolidBrush(c))
                g.FillEllipse(brush, x, yy + 4, 10, 10);

            using (var legendFont = new Font("Segoe UI", 8F))
            using (var textBrush = new SolidBrush(UiTheme.Text))
                g.DrawString($"{text}  ({count})", legendFont, textBrush, x + 16, yy);
        }

        Row(Color.FromArgb(46, 160, 100), "Healthy", _healthy, 0);
        Row(Color.FromArgb(230, 160, 40), "Low stock", _low, 1);
        Row(Color.FromArgb(210, 70, 60), "Out of stock", _out, 2);
    }

    private void PnlMovement_Paint(object? sender, PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(Color.White);

        if (_movementBars.Count == 0)
        {
            using (var emptyFont = new Font("Segoe UI", 8.5F))
            using (var emptyBrush = new SolidBrush(UiTheme.TextMuted))
                g.DrawString("No movement data yet", emptyFont, emptyBrush, 8, 20);
            return;
        }

        int max = Math.Max(1, _movementBars.Max(b => Math.Max(b.InQty, b.OutQty)));
        int left = 8;
        int bottom = pnlMovement.Height - 22;
        int top = 8;
        int chartH = Math.Max(20, bottom - top);
        int slotW = Math.Max(18, (pnlMovement.Width - 16) / _movementBars.Count);

        using var inBrush = new SolidBrush(Color.FromArgb(46, 160, 100));
        using var outBrush = new SolidBrush(Color.FromArgb(210, 90, 80));
        using var dayFont = new Font("Segoe UI", 7F);
        using var dayBrush = new SolidBrush(UiTheme.TextMuted);

        for (int i = 0; i < _movementBars.Count; i++)
        {
            var (label, inQty, outQty) = _movementBars[i];
            int x = left + i * slotW;
            int barW = Math.Max(4, slotW / 3);

            int inH = (int)(chartH * (inQty / (double)max));
            int outH = (int)(chartH * (outQty / (double)max));

            if (inH > 0)
                g.FillRectangle(inBrush, x, bottom - inH, barW, inH);
            if (outH > 0)
                g.FillRectangle(outBrush, x + barW + 2, bottom - outH, barW, outH);

            g.DrawString(label, dayFont, dayBrush, x, bottom + 2);
        }

        int lx = 8;
        int ly = 0;
        g.FillRectangle(inBrush, lx, ly, 8, 8);
        g.DrawString("In", dayFont, dayBrush, lx + 10, ly - 2);
        g.FillRectangle(outBrush, lx + 36, ly, 8, 8);
        g.DrawString("Out", dayFont, dayBrush, lx + 46, ly - 2);
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
            e.CellStyle.ForeColor = UiTheme.Success;
            e.CellStyle.Font = UiTheme.LabelSemibold;
        }
    }
}
