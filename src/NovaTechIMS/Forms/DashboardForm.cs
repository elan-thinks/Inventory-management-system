using System;
using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Models;
using NovaTechIMS.Security;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms;

/// <summary>SCR-002 Dashboard — Designer-based (partial). Quick-action buttons remain runtime (permission-based).</summary>
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
        Load += (_, _) => Reload();
    }

    private void ApplyRuntimeStyling()
    {
        Font = UiTheme.Body;
        BackColor = UiTheme.Background;

        if (metricsPanel is not null)
            metricsPanel.BackColor = UiTheme.Background;
        if (actionsPanel is not null)
            actionsPanel.BackColor = UiTheme.Surface;

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

        StyleMetricTile(tileProducts, lblProducts, UiTheme.Text);
        StyleMetricTile(tileCategories, lblCategories, UiTheme.Text);
        StyleMetricTile(tileSuppliers, lblSuppliers, UiTheme.Text);
        StyleMetricTile(tileTotalQty, lblTotalQty, UiTheme.Text);
        StyleMetricTile(tileLow, lblLow, UiTheme.Warning);
        StyleMetricTile(tileOut, lblOut, UiTheme.Error);

        if (grid is not null)
            UiTheme.ApplyGridTheme(grid);

        if (actionsPanel is not null)
        {
            actionsPanel.Paint += (_, e) =>
            {
                using var pen = new Pen(UiTheme.Border, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, actionsPanel.Width - 1, actionsPanel.Height - 1);
            };
        }
    }

    private static void StyleMetricTile(Panel? tile, Label? valueLabel, Color valueColor)
    {
        if (tile is null) return;

        tile.BackColor = UiTheme.Surface;
        UiTheme.ApplyRoundedRegion(tile, UiTheme.RadiusLg);

        if (valueLabel is not null)
            valueLabel.ForeColor = valueColor;

        foreach (Control c in tile.Controls)
        {
            if (c is Label l && l != valueLabel)
            {
                l.Font = UiTheme.Label;
                l.ForeColor = UiTheme.TextMuted;
            }
        }
    }

    private void BuildQuickActions()
    {
        if (actionsPanel is null || _user is null) return;

        int ay = 40;
        void AddAction(string text, string navKey)
        {
            var b = UiTheme.StyleButton(new Button
            {
                Text = text,
                Location = new Point(12, ay),
                Size = new Size(200, 34),
                Tag = navKey
            }, UiTheme.ButtonKind.Primary);
            b.Click += (_, _) => _navigate?.Invoke(navKey);
            actionsPanel.Controls.Add(b);
            ay += 42;
        }

        if (AuthorizationService.HasPermission(_user, Permissions.StockIn))
            AddAction("Stock-In", "Stock In");
        if (AuthorizationService.HasPermission(_user, Permissions.StockOut))
            AddAction("Stock-Out", "Stock Out");
        if (AuthorizationService.HasPermission(_user, Permissions.ViewProducts))
            AddAction("Products", "Products");
        if (AuthorizationService.HasPermission(_user, Permissions.PerformAdjustment))
            AddAction("Inventory Adjustment", "Inventory Adjustment");
        if (AuthorizationService.HasPermission(_user, Permissions.ManageUsers))
            AddAction("Manage Users", "User Management");
    }

    private void Reload()
    {
        if (lblError is not null)
            lblError.Visible = false;

        try
        {
            var m = _service.GetMetrics();
            if (lblProducts is not null) lblProducts.Text = m.ActiveProductCount.ToString();
            if (lblCategories is not null) lblCategories.Text = m.CategoryCount.ToString();
            if (lblSuppliers is not null) lblSuppliers.Text = m.SupplierCount.ToString();
            if (lblTotalQty is not null) lblTotalQty.Text = m.TotalQuantityOnHand.ToString();
            if (lblLow is not null) lblLow.Text = m.LowStockCount.ToString();
            if (lblOut is not null) lblOut.Text = m.OutOfStockCount.ToString();

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
                }

                Show(nameof(TransactionHistoryRow.TransactionDate), "Date", 0.8f);
                Show(nameof(TransactionHistoryRow.TypeLabel), "Type", 0.7f);
                Show(nameof(TransactionHistoryRow.ProductName), "Product", 1.4f);
                Show(nameof(TransactionHistoryRow.Quantity), "Qty", 0.5f);
                Show(nameof(TransactionHistoryRow.UserFullName), "User", 1.0f);

                if (!_badgeWired)
                {
                    UiTheme.WireStatusBadgeColumn(grid, nameof(TransactionHistoryRow.TypeLabel),
                        value => (value?.ToString() ?? "") switch
                        {
                            "Stock-In" => ("Stock-In", UiTheme.BadgeTone.Success, '\u2713'),
                            "Stock-Out" => ("Stock-Out", UiTheme.BadgeTone.Error, '\u2715'),
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
