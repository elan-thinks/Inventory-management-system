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

    public DashboardForm(CurrentUser user, Action<string>? navigate = null)
    {
        _user = user;
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
        metricsPanel.BackColor = UiTheme.Background;
        actionsPanel.BackColor = UiTheme.Surface;
        lblAct.Font = UiTheme.SectionTitle;
        lblAct.ForeColor = UiTheme.Text;
        lblQa.Font = UiTheme.SectionTitle;
        lblQa.ForeColor = UiTheme.Text;
        lblEmpty.Font = UiTheme.Body;
        lblEmpty.ForeColor = UiTheme.TextMuted;
        lblError.Font = UiTheme.Label;
        lblError.ForeColor = UiTheme.Error;

        foreach (var tile in new[] { tileProducts, tileCategories, tileSuppliers, tileTotalQty, tileLow, tileOut })
        {
            tile.BackColor = UiTheme.Surface;
            UiTheme.ApplyRoundedRegion(tile, UiTheme.RadiusLg);
        }

        lblProducts.ForeColor = UiTheme.Text;
        lblCategories.ForeColor = UiTheme.Text;
        lblSuppliers.ForeColor = UiTheme.Text;
        lblTotalQty.ForeColor = UiTheme.Text;
        lblLow.ForeColor = UiTheme.Warning;
        lblOut.ForeColor = UiTheme.Error;

        foreach (Control c in tileProducts.Controls)
            if (c is Label l && l != lblProducts) { l.Font = UiTheme.Label; l.ForeColor = UiTheme.TextMuted; }
        foreach (Control c in tileCategories.Controls)
            if (c is Label l && l != lblCategories) { l.Font = UiTheme.Label; l.ForeColor = UiTheme.TextMuted; }
        foreach (Control c in tileSuppliers.Controls)
            if (c is Label l && l != lblSuppliers) { l.Font = UiTheme.Label; l.ForeColor = UiTheme.TextMuted; }
        foreach (Control c in tileTotalQty.Controls)
            if (c is Label l && l != lblTotalQty) { l.Font = UiTheme.Label; l.ForeColor = UiTheme.TextMuted; }
        foreach (Control c in tileLow.Controls)
            if (c is Label l && l != lblLow) { l.Font = UiTheme.Label; l.ForeColor = UiTheme.TextMuted; }
        foreach (Control c in tileOut.Controls)
            if (c is Label l && l != lblOut) { l.Font = UiTheme.Label; l.ForeColor = UiTheme.TextMuted; }

        UiTheme.ApplyGridTheme(grid);
        UiTheme.WireStatusBadgeColumn(grid, nameof(TransactionHistoryRow.TypeLabel), value => (value?.ToString() ?? "") switch
        {
            "Stock-In" => ("Stock-In", UiTheme.BadgeTone.Success, '\u2713'),
            "Stock-Out" => ("Stock-Out", UiTheme.BadgeTone.Error, '\u2715'),
            _ => ("Adjustment", UiTheme.BadgeTone.Info, '!')
        });

        actionsPanel.Paint += (_, e) =>
        {
            using var pen = new Pen(UiTheme.Border, 1);
            e.Graphics.DrawRectangle(pen, 0, 0, actionsPanel.Width - 1, actionsPanel.Height - 1);
        };
    }

    private void BuildQuickActions()
    {
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
        lblError.Visible = false;

        try
        {
            var m = _service.GetMetrics();
            lblProducts.Text = m.ActiveProductCount.ToString();
            lblCategories.Text = m.CategoryCount.ToString();
            lblSuppliers.Text = m.SupplierCount.ToString();
            lblTotalQty.Text = m.TotalQuantityOnHand.ToString();
            lblLow.Text = m.LowStockCount.ToString();
            lblOut.Text = m.OutOfStockCount.ToString();

            var rows = _service.GetRecentActivity(10);
            if (rows.Count == 0)
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
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "Unable to load dashboard data. " + ErrorPresenter.ToUserMessage(ex);
            lblError.Visible = true;
        }
    }
}
