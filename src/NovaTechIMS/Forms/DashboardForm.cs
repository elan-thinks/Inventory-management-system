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

/// <summary>SCR-002 Dashboard — UI aligned to approved mockup (this page only).</summary>
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
        ApplyRuntimeStyling();
        BuildQuickActions();
        Load += (_, _) =>
        {
            // Anchor View all to the right of the header after layout
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
        }

        if (lblQa is not null)
        {
            lblQa.Font = UiTheme.SectionTitle;
            lblQa.ForeColor = UiTheme.Text;
        }

        if (lblEmpty is not null)
        {
            lblEmpty.Font = UiTheme.Body;
            lblEmpty.ForeColor = UiTheme.TextMuted;
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
        }

        // Caption top + value below; low/out use warning/error colors
        StyleMetricTile(tileProducts, lblProducts, capProducts, UiTheme.Text);
        StyleMetricTile(tileCategories, lblCategories, capCategories, UiTheme.Text);
        StyleMetricTile(tileSuppliers, lblSuppliers, capSuppliers, UiTheme.Text);
        StyleMetricTile(tileTotalQty, lblTotalQty, capTotalQty, UiTheme.Text);
        StyleMetricTile(tileLow, lblLow, capLow, UiTheme.Warning);
        StyleMetricTile(tileOut, lblOut, capOut, UiTheme.Error);

        if (grid is not null)
            UiTheme.ApplyGridTheme(grid);
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

    private static void StyleMetricTile(Panel? tile, Label? valueLabel, Label? capLabel, Color valueColor)
    {
        if (tile is null) return;

        tile.BackColor = UiTheme.Surface;
        // Soft border instead of Region (Region was clipping labels to blank tiles)
        tile.Paint += (_, e) =>
        {
            using var pen = new Pen(UiTheme.Border, 1);
            e.Graphics.DrawRectangle(pen, 0, 0, tile.Width - 1, tile.Height - 1);
        };

        if (capLabel is not null)
        {
            capLabel.Font = UiTheme.Label;
            capLabel.ForeColor = UiTheme.TextMuted;
        }

        if (valueLabel is not null)
        {
            valueLabel.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            valueLabel.ForeColor = valueColor;
        }
    }

    private void BuildQuickActions()
    {
        if (actionsPanel is null || _user is null) return;

        // Remove any prior action buttons (keep title label)
        for (int i = actionsPanel.Controls.Count - 1; i >= 0; i--)
        {
            if (actionsPanel.Controls[i] is Button)
                actionsPanel.Controls.RemoveAt(i);
        }

        int ay = 44;
        int btnWidth = Math.Max(160, actionsPanel.ClientSize.Width - 32);
        bool first = true;

        void AddAction(string text, string navKey, bool primary)
        {
            var kind = primary ? UiTheme.ButtonKind.Primary : UiTheme.ButtonKind.Secondary;
            var b = UiTheme.StyleButton(new Button
            {
                Text = text,
                Location = new Point(16, ay),
                Size = new Size(btnWidth, 36),
                Tag = navKey,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(12, 0, 8, 0)
            }, kind);
            b.Click += (_, _) => _navigate?.Invoke(navKey);
            actionsPanel.Controls.Add(b);
            ay += 44;
            first = false;
        }

        if (AuthorizationService.HasPermission(_user, Permissions.StockIn))
            AddAction("  ↓  Record Stock-In", "Stock In", primary: true);
        if (AuthorizationService.HasPermission(_user, Permissions.StockOut))
            AddAction("  ↑  Record Stock-Out", "Stock Out", primary: first);
        if (AuthorizationService.HasPermission(_user, Permissions.ViewProducts))
            AddAction("  🔍  Search Products", "Products", primary: false);
        if (AuthorizationService.HasPermission(_user, Permissions.PerformAdjustment))
            AddAction("  ⇄  Inventory Adjustment", "Inventory Adjustment", primary: false);
        if (AuthorizationService.HasPermission(_user, Permissions.ManageUsers))
            AddAction("  👤  Manage Users", "User Management", primary: false);

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

            if (lblProducts is not null)
                lblProducts.Text = m.ActiveProductCount.ToString("N0", nfi);
            if (lblCategories is not null)
                lblCategories.Text = m.CategoryCount.ToString("N0", nfi);
            if (lblSuppliers is not null)
                lblSuppliers.Text = m.SupplierCount.ToString("N0", nfi);
            if (lblTotalQty is not null)
                lblTotalQty.Text = m.TotalQuantityOnHand.ToString("N0", nfi);
            if (lblLow is not null)
                lblLow.Text = m.LowStockCount.ToString("N0", nfi);
            if (lblOut is not null)
                lblOut.Text = m.OutOfStockCount.ToString("N0", nfi);

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

                void Show(string name, string header, float w = 1f)
                {
                    if (grid.Columns[name] is not DataGridViewColumn c) return;
                    c.Visible = true;
                    c.HeaderText = header;
                    c.FillWeight = w;
                    c.DisplayIndex = grid.Columns.GetColumnCount(DataGridViewElementStates.Visible) - 1;
                }

                // Mockup order: Date | Type | Product | Qty | User
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
            if (lblError is not null)
            {
                lblError.Text = "Unable to load dashboard data. " + ErrorPresenter.ToUserMessage(ex);
                lblError.Visible = true;
            }
        }
    }
}
