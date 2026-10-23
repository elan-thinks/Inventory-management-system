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
/// SCR-002 Dashboard — Starline-style pastel metrics + activity + quick actions.
/// </summary>
public partial class DashboardForm : Form
{
    private DashboardService? _service;
    private readonly CurrentUser _user;
    private readonly Action<string>? _navigate;
    private bool _badgeWired;

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
        Load += (_, _) =>
        {
            lblWelcome.Text = $"Welcome, {_user.FullName.Split(' ')[0]} 👋";
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

        foreach (var b in new[] { btnQaStockOut, btnQaProducts, btnQaAdjustment, btnQaUsers })
        {
            b.ForeColor = UiTheme.Primary;
            b.TextAlign = ContentAlignment.MiddleLeft;
            b.Padding = new Padding(10, 0, 8, 0);
        }
        btnQaStockIn.TextAlign = ContentAlignment.MiddleLeft;
        btnQaStockIn.Padding = new Padding(10, 0, 8, 0);

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
