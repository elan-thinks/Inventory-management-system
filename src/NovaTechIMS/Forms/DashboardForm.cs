using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Models;
using NovaTechIMS.Security;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms;

/// <summary>SCR-002 Dashboard — dark theme, painted metric tiles, icons.</summary>
public partial class DashboardForm : Form
{
    private readonly DashboardService _service = new();
    private readonly CurrentUser _user;
    private readonly Action<string>? _navigate;
    private bool _badgeWired;

    public DashboardForm(CurrentUser user, Action<string>? navigate = null)
    {
        _user = user ?? throw new ArgumentNullException(nameof(user));
        _navigate = navigate;
        InitializeComponent();
        WireMetricPaints();
        ApplyRuntimeStyling();
        BuildQuickActions();
        Load += (_, _) =>
        {
            if (activityHeader is not null && lnkViewAll is not null)
                lnkViewAll.Left = Math.Max(80, activityHeader.ClientSize.Width - lnkViewAll.Width - 4);
            Reload();
        };
        if (activityHeader is not null)
            activityHeader.Resize += (_, _) =>
            {
                if (lnkViewAll is not null)
                    lnkViewAll.Left = Math.Max(80, activityHeader.ClientSize.Width - lnkViewAll.Width - 4);
            };
    }

    private void WireMetricPaints()
    {
        WireTile(tileProducts, "Total Active Products", "icons-totalProduct.png", UiTheme.Text, () => lblProducts?.Text ?? "0");
        WireTile(tileCategories, "Total Categories", "icons-catagory-tag.png", UiTheme.Text, () => lblCategories?.Text ?? "0");
        WireTile(tileSuppliers, "Total Suppliers", "icons-suppliers.png", UiTheme.Text, () => lblSuppliers?.Text ?? "0");
        WireTile(tileTotalQty, "Total Inventory Quantity", "icons-totalProduct.png", UiTheme.Text, () => lblTotalQty?.Text ?? "0");
        WireTile(tileLow, "Low-Stock Products", "icons-warning.png", UiTheme.Warning, () => lblLow?.Text ?? "0");
        WireTile(tileOut, "Out-of-Stock Products", "icons-x-error.png", UiTheme.Error, () => lblOut?.Text ?? "0");
    }

    private static void WireTile(Panel? tile, string caption, string iconFile, Color valueColor, Func<string> getValue)
    {
        if (tile is null) return;

        tile.BackColor = UiTheme.Surface;
        tile.Paint += (_, e) =>
        {
            var g = e.Graphics;
            g.Clear(UiTheme.Surface);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (var pen = new Pen(UiTheme.Border, 1))
                g.DrawRectangle(pen, 0, 0, tile.Width - 1, tile.Height - 1);

            int x = 12;
            int y = 12;

            var icon = AppIcons.Get(iconFile, 16);
            if (icon is not null)
            {
                g.DrawImage(icon, x, y, 16, 16);
                x += 22;
            }

            using (var brush = new SolidBrush(UiTheme.TextMuted))
            using (var font = new Font("Segoe UI", 8.25f))
                g.DrawString(caption, font, brush, x, y + 1);

            var value = getValue();
            using (var brush = new SolidBrush(valueColor))
            using (var font = new Font("Segoe UI", 22f, FontStyle.Bold))
                g.DrawString(value, font, brush, 12, 48);
        };
    }

    private void ApplyRuntimeStyling()
    {
        Font = UiTheme.Body;
        BackColor = UiTheme.Background;

        if (metricsPanel is not null)
            metricsPanel.BackColor = UiTheme.Background;

        StyleCard(activityCard);
        StyleCard(actionsPanel);

        if (lblAct is not null)
        {
            lblAct.Font = UiTheme.SectionTitle;
            lblAct.ForeColor = UiTheme.Text;
            lblAct.BackColor = UiTheme.Surface;
        }

        if (lblQa is not null)
        {
            lblQa.Font = UiTheme.SectionTitle;
            lblQa.ForeColor = UiTheme.Text;
            lblQa.BackColor = UiTheme.Surface;
        }

        if (lblEmpty is not null)
        {
            lblEmpty.Font = UiTheme.Body;
            lblEmpty.ForeColor = UiTheme.TextMuted;
            lblEmpty.BackColor = UiTheme.Surface;
        }

        if (lblError is not null)
        {
            lblError.Font = UiTheme.Label;
            lblError.ForeColor = UiTheme.Error;
        }

        if (lnkViewAll is not null)
        {
            lnkViewAll.LinkColor = UiTheme.Primary;
            lnkViewAll.ActiveLinkColor = UiTheme.PrimaryHover;
            lnkViewAll.BackColor = UiTheme.Surface;
        }

        if (activityHeader is not null)
            activityHeader.BackColor = UiTheme.Surface;

        if (grid is not null)
        {
            grid.BackgroundColor = UiTheme.Surface;
            UiTheme.ApplyGridTheme(grid);
        }
    }

    private static void StyleCard(Panel? card)
    {
        if (card is null) return;
        card.BackColor = UiTheme.Surface;
        card.Paint += (_, e) =>
        {
            using var pen = new Pen(UiTheme.Border, 1);
            e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
        };
    }

    private void BuildQuickActions()
    {
        if (actionsPanel is null || _user is null) return;

        for (int i = actionsPanel.Controls.Count - 1; i >= 0; i--)
        {
            if (actionsPanel.Controls[i] is Button)
                actionsPanel.Controls.RemoveAt(i);
        }

        int ay = 44;
        int btnWidth = Math.Max(160, actionsPanel.ClientSize.Width - 32);
        bool primaryUsed = false;

        void AddAction(string text, string navKey, string iconFile)
        {
            var kind = !primaryUsed ? UiTheme.ButtonKind.Primary : UiTheme.ButtonKind.Secondary;
            primaryUsed = true;
            var b = UiTheme.StyleButton(new Button
            {
                Text = "  " + text,
                Location = new Point(16, ay),
                Size = new Size(btnWidth, 36),
                Tag = navKey,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 8, 0)
            }, kind);
            AppIcons.ApplyToButton(b, iconFile, 16);
            b.Click += (_, _) => _navigate?.Invoke(navKey);
            actionsPanel.Controls.Add(b);
            ay += 44;
        }

        if (AuthorizationService.HasPermission(_user, Permissions.StockIn))
            AddAction("Record Stock-In", "Stock In", "icons-stockIn.png");
        if (AuthorizationService.HasPermission(_user, Permissions.StockOut))
            AddAction("Record Stock-Out", "Stock Out", "icons-stockOut.png");
        if (AuthorizationService.HasPermission(_user, Permissions.ViewProducts))
            AddAction("Search Products", "Products", "icons-search.png");
        if (AuthorizationService.HasPermission(_user, Permissions.PerformAdjustment))
            AddAction("Inventory Adjustment", "Inventory Adjustment", "icons-Inventory-adjust.png");
        if (AuthorizationService.HasPermission(_user, Permissions.ManageUsers))
            AddAction("Manage Users", "User Management", "icons-user.png");

        actionsPanel.Resize += (_, _) =>
        {
            int w = Math.Max(160, actionsPanel.ClientSize.Width - 32);
            foreach (Control c in actionsPanel.Controls)
            {
                if (c is Button btn)
                    btn.Width = w;
            }
        };
    }

    private void LnkViewAll_Click(object? sender, EventArgs e)
    {
        if (AuthorizationService.HasPermission(_user, Permissions.ViewInventoryHistory))
            _navigate?.Invoke("Inventory History");
    }

    private void Reload()
    {
        if (lblError is not null)
            lblError.Visible = false;

        try
        {
            var m = _service.GetMetrics();
            var nfi = CultureInfo.CurrentCulture.NumberFormat;

            SetMetric(lblProducts, m.ActiveProductCount.ToString("N0", nfi));
            SetMetric(lblCategories, m.CategoryCount.ToString("N0", nfi));
            SetMetric(lblSuppliers, m.SupplierCount.ToString("N0", nfi));
            SetMetric(lblTotalQty, m.TotalQuantityOnHand.ToString("N0", nfi));
            SetMetric(lblLow, m.LowStockCount.ToString("N0", nfi));
            SetMetric(lblOut, m.OutOfStockCount.ToString("N0", nfi));

            foreach (var t in new[] { tileProducts, tileCategories, tileSuppliers, tileTotalQty, tileLow, tileOut })
                t?.Invalidate();

            var rows = _service.GetRecentActivity(10);
            if (grid is null) return;

            if (rows.Count == 0)
            {
                grid.Visible = false;
                if (lblEmpty is not null) lblEmpty.Visible = true;
            }
            else
            {
                if (lblEmpty is not null) lblEmpty.Visible = false;
                grid.Visible = true;
                grid.DataSource = null;
                grid.Columns.Clear();
                grid.DataSource = rows;

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
                Show(nameof(TransactionHistoryRow.ProductName), "Product", 1.5f);
                Show(nameof(TransactionHistoryRow.Quantity), "Qty", 0.5f);
                Show(nameof(TransactionHistoryRow.UserFullName), "User", 1.0f);

                if (grid.Columns[nameof(TransactionHistoryRow.TransactionDate)] is DataGridViewColumn dateCol)
                    dateCol.DefaultCellStyle.Format = "g";

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
            SetMetric(lblProducts, "0");
            SetMetric(lblCategories, "0");
            SetMetric(lblSuppliers, "0");
            SetMetric(lblTotalQty, "0");
            SetMetric(lblLow, "0");
            SetMetric(lblOut, "0");
            foreach (var t in new[] { tileProducts, tileCategories, tileSuppliers, tileTotalQty, tileLow, tileOut })
                t?.Invalidate();

            if (lblError is not null)
            {
                lblError.Text = "Unable to load dashboard data. " + ErrorPresenter.ToUserMessage(ex);
                lblError.Visible = true;
            }
        }
    }

    private static void SetMetric(Label? lbl, string text)
    {
        if (lbl is null) return;
        lbl.Text = text;
    }

    private void lblAct_Click(object sender, EventArgs e)
    {

    }
}
