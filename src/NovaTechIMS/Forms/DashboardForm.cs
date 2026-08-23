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

/// <summary>
/// SCR-002 Dashboard — layout is designer-owned; this file is data + permissions only.
/// </summary>
public partial class DashboardForm : Form
{
    private DashboardService? _service;
    private readonly CurrentUser _user;
    private readonly Action<string>? _navigate;
    private bool _badgeWired;

    private DashboardService Service => _service ??= new DashboardService();

    /// <summary>Required by the WinForms designer.</summary>
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
        ApplyQuickActionVisibility();
        Load += (_, _) =>
        {
            PositionViewAllLink();
            Reload();
        };
        activityHeader.Resize += (_, _) => PositionViewAllLink();
        actionsPanel.Resize += (_, _) => ResizeQuickActionButtons();
    }

    private void PositionViewAllLink()
    {
        if (lnkViewAll is null || activityHeader is null) return;
        lnkViewAll.Left = Math.Max(80, activityHeader.ClientSize.Width - lnkViewAll.Width - 4);
    }

    private void ResizeQuickActionButtons()
    {
        if (actionsPanel is null) return;
        int w = Math.Max(160, actionsPanel.ClientSize.Width - 24);
        foreach (Control c in actionsPanel.Controls)
        {
            if (c is Button b)
                b.Width = w;
        }
    }

    private void ApplyRuntimeTouches()
    {
        Font = UiTheme.Body;
        BackColor = UiTheme.Background;
        metricsPanel.BackColor = UiTheme.Background;

        activityCard.BackColor = UiTheme.Surface;
        actionsPanel.BackColor = UiTheme.Surface;
        activityHeader.BackColor = UiTheme.Surface;

        lblAct.Font = UiTheme.SectionTitle;
        lblAct.ForeColor = UiTheme.Text;
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

        int y = 48;
        foreach (Control c in actionsPanel.Controls)
        {
            if (c is Button { Visible: true } b)
            {
                b.Top = y;
                y += 40;
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

            var rows = Service.GetRecentActivity(10);
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
            lblProducts.Text = "0";
            lblCategories.Text = "0";
            lblSuppliers.Text = "0";
            lblTotalQty.Text = "0";
            lblLow.Text = "0";
            lblOut.Text = "0";

            // Safe user message (no stack / SQL)
            var mapped = DbExceptionMapper.Map(ex);
            var friendly = ErrorPresenter.Classify(mapped).Message;
            lblError.Text = "Unable to load dashboard data. " + friendly;
            lblError.Visible = true;
        }
    }
}
