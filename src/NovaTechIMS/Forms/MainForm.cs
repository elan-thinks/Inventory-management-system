using System;
using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Forms.Categories;
using NovaTechIMS.Forms.Customers;
using NovaTechIMS.Forms.Delegations;
using NovaTechIMS.Forms.Inventory;
using NovaTechIMS.Forms.Products;
using NovaTechIMS.Forms.Reports;
using NovaTechIMS.Forms.Suppliers;
using NovaTechIMS.Forms.Users;
using NovaTechIMS.Models;
using NovaTechIMS.Models.Enums;
using NovaTechIMS.Security;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms;

/// <summary>
/// Application shell — Milestone 19 final integration (approved UI only).
/// </summary>
public partial class MainForm : Form
{
    private readonly CurrentUser _currentUser;
    private Button? _activeNavButton;
    private Form? _hostedContent;

    public MainForm(CurrentUser currentUser)
    {
        _currentUser = currentUser;
        InitializeComponent();
        ApplyRuntimeStyling();
        BuildNavigation();
        NavigateTo("Dashboard");
    }

    private void ApplyRuntimeStyling()
    {
        BackColor = UiTheme.Background;
        Font = UiTheme.Body;
        MinimumSize = new Size(UiTheme.MinWindowWidth, UiTheme.MinWindowHeight);

        pnlSidebar.BackColor = UiTheme.SidebarBackground;
        pnlSidebar.Width = UiTheme.SidebarWidth;
        flpNav.BackColor = UiTheme.SidebarBackground;

        lblBrand.Font = UiTheme.SectionTitle;
        lblBrand.ForeColor = Color.White;

        btnLogout.Font = UiTheme.SidebarItem;
        btnLogout.ForeColor = UiTheme.SidebarText;
        btnLogout.BackColor = UiTheme.SidebarBackground;
        btnLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 255, 255, 255);

        pnlMain.BackColor = UiTheme.Background;
        pnlHeader.BackColor = UiTheme.Surface;
        pnlHeader.Paint += (_, e) =>
        {
            using var pen = new Pen(UiTheme.Border, 1);
            e.Graphics.DrawLine(pen, 0, pnlHeader.Height - 1, pnlHeader.Width, pnlHeader.Height - 1);
        };

        lblBreadcrumb.Font = UiTheme.Label;
        lblBreadcrumb.ForeColor = UiTheme.TextMuted;
        lblScreenTitle.Font = UiTheme.ScreenTitle;
        lblScreenTitle.ForeColor = UiTheme.Text;

        lblRoleBadge.Font = UiTheme.BadgeText;
        lblRoleBadge.ForeColor = UiTheme.Primary;
        lblRoleBadge.BackColor = UiTheme.InfoTint;
        UiTheme.ApplyRoundedRegion(lblRoleBadge, 10);

        pnlContent.BackColor = UiTheme.Background;
        lblPlaceholderTitle.Font = UiTheme.ScreenTitle;
        lblPlaceholderTitle.ForeColor = UiTheme.Text;
        lblPlaceholderBody.Font = UiTheme.Body;
        lblPlaceholderBody.ForeColor = UiTheme.TextMuted;

        statusStrip.BackColor = UiTheme.StatusStripBackground;
        statusStrip.Font = UiTheme.StatusStrip;
        lblUserStatus.ForeColor = UiTheme.Text;
        lblDateStatus.ForeColor = UiTheme.TextMuted;
        lblMessageStatus.ForeColor = UiTheme.TextMuted;
    }

    private void MainForm_Load(object? sender, EventArgs e)
    {
        var roleLabel = _currentUser.Role == UserRole.Administrator
            ? "Administrator"
            : "Inventory Staff";

        lblUserStatus.Text = $"{_currentUser.FullName}  ·  {roleLabel}";
        lblDateStatus.Text = DateTime.Now.ToString("dddd, dd MMM yyyy");
        lblMessageStatus.Text = "Ready";
        MinimumSize = new Size(UiTheme.MinWindowWidth, UiTheme.MinWindowHeight);
        lblRoleBadge.Text = "v1.3";
    }

    private void BuildNavigation()
    {
        flpNav.Controls.Clear();

        AddNavItem("Dashboard");
        AddGroupLabel("CATALOG");
        AddNavItem("Products");
        AddNavItem("Categories");
        AddNavItem("Suppliers");
        AddNavItem("Customers");
        AddGroupLabel("INVENTORY OPERATIONS");

        if (AuthorizationService.HasPermission(_currentUser, Permissions.StockIn))
            AddNavItem("Stock In");
        if (AuthorizationService.HasPermission(_currentUser, Permissions.StockOut))
            AddNavItem("Stock Out");
        if (AuthorizationService.HasPermission(_currentUser, Permissions.PerformAdjustment))
            AddNavItem("Inventory Adjustment");
        if (AuthorizationService.HasPermission(_currentUser, Permissions.ViewInventoryHistory))
            AddNavItem("Inventory History");

        if (AuthorizationService.HasPermission(_currentUser, Permissions.ViewReports))
        {
            AddGroupLabel("REPORTS");
            AddNavItem("Reports");
        }

        if (AuthorizationService.HasPermission(_currentUser, Permissions.ManageUsers)
            || AuthorizationService.HasPermission(_currentUser, Permissions.ManageDelegations))
        {
            AddGroupLabel("ADMINISTRATION");
            if (AuthorizationService.HasPermission(_currentUser, Permissions.ManageUsers))
                AddNavItem("User Management");
            if (AuthorizationService.HasPermission(_currentUser, Permissions.ManageDelegations))
                AddNavItem("Delegations");
        }
    }

    private void AddGroupLabel(string text)
    {
        var lbl = new Label
        {
            Text = text,
            Font = UiTheme.SidebarGroup,
            ForeColor = UiTheme.SidebarText,
            AutoSize = false,
            Width = UiTheme.SidebarWidth - 24,
            Height = 28,
            Padding = new Padding(12, 12, 0, 0),
            Margin = new Padding(0, 8, 0, 0)
        };
        flpNav.Controls.Add(lbl);
    }

    private void AddNavItem(string text)
    {
        var btn = new Button
        {
            Text = "  " + text,
            Font = UiTheme.SidebarItem,
            ForeColor = UiTheme.SidebarText,
            BackColor = UiTheme.SidebarBackground,
            FlatStyle = FlatStyle.Flat,
            TextAlign = ContentAlignment.MiddleLeft,
            Width = UiTheme.SidebarWidth - 16,
            Height = 36,
            Margin = new Padding(8, 2, 8, 2),
            Cursor = Cursors.Hand,
            Tag = text,
            TabStop = true,
            AccessibleName = text
        };
        btn.FlatAppearance.BorderSize = 0;
        btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 255, 255, 255);
        btn.Click += NavItem_Click;
        btn.Paint += NavButton_Paint;
        flpNav.Controls.Add(btn);

        if (text == "Dashboard")
            SetActiveNav(btn);
    }

    private void NavItem_Click(object? sender, EventArgs e)
    {
        if (sender is not Button btn || btn.Tag is not string key)
            return;

        SetActiveNav(btn);
        NavigateTo(key);
    }

    /// <summary>Central navigation used by sidebar and Dashboard quick actions.</summary>
    public void NavigateTo(string key)
    {
        foreach (Control c in flpNav.Controls)
        {
            if (c is Button b && b.Tag is string t && t == key)
            {
                SetActiveNav(b);
                break;
            }
        }

        try
        {
            switch (key)
            {
                case "Dashboard":
                    HostList(new DashboardForm(_currentUser, NavigateTo), "Dashboard");
                    lblMessageStatus.Text = "Opened: Dashboard";
                    return;
                case "Products":
                    HostList(new ProductListForm(), "Products");
                    lblMessageStatus.Text = "Opened: Products";
                    return;
                case "Categories":
                    HostList(new CategoryListForm(), "Categories");
                    lblMessageStatus.Text = "Opened: Categories";
                    return;
                case "Suppliers":
                    HostList(new SupplierListForm(), "Suppliers");
                    lblMessageStatus.Text = "Opened: Suppliers";
                    return;
                case "Customers":
                    HostList(new CustomerListForm(), "Customers");
                    lblMessageStatus.Text = "Opened: Customers";
                    return;
                case "User Management":
                    HostList(new UserListForm(), "User Management");
                    lblMessageStatus.Text = "Opened: User Management";
                    return;
                case "Stock In":
                    HostList(new StockInForm(), "Stock In");
                    lblMessageStatus.Text = "Opened: Stock In";
                    return;
                case "Stock Out":
                    HostList(new StockOutForm(), "Stock Out");
                    lblMessageStatus.Text = "Opened: Stock Out";
                    return;
                case "Inventory History":
                    HostList(new InventoryHistoryForm(), "Inventory History");
                    lblMessageStatus.Text = "Opened: Inventory History";
                    return;
                case "Inventory Adjustment":
                    HostList(new AdjustmentForm(), "Inventory Adjustment");
                    lblMessageStatus.Text = "Opened: Inventory Adjustment";
                    return;
                case "Delegations":
                    HostList(new DelegationListForm(), "Delegations");
                    lblMessageStatus.Text = "Opened: Delegations";
                    return;
                case "Reports":
                    HostList(new ReportsHubForm(), "Reports");
                    lblMessageStatus.Text = "Opened: Reports";
                    return;
            }
        }
        catch (Exception ex)
        {
            ErrorPresenter.Show(this, DbExceptionMapper.Map(ex));
            lblMessageStatus.Text = "Error opening screen";
            return;
        }

        ShowPlaceholder(key, "Screen not available", "This navigation target is not mapped.");
        lblMessageStatus.Text = $"Opened: {key}";
    }

    private void NavButton_Paint(object? sender, PaintEventArgs e)
    {
        if (sender is not Button btn || btn != _activeNavButton) return;
        using var brush = new SolidBrush(UiTheme.SidebarAccent);
        e.Graphics.FillRectangle(brush, 0, 4, 3, btn.Height - 8);
    }

    private void SetActiveNav(Button btn)
    {
        if (_activeNavButton != null)
        {
            var previous = _activeNavButton;
            _activeNavButton.BackColor = UiTheme.SidebarBackground;
            _activeNavButton.ForeColor = UiTheme.SidebarText;
            _activeNavButton.Font = UiTheme.SidebarItem;
            _activeNavButton.FlatAppearance.BorderSize = 0;
            _activeNavButton = null;
            previous.Invalidate();
        }

        _activeNavButton = btn;
        btn.BackColor = Color.FromArgb(40, 255, 255, 255);
        btn.ForeColor = UiTheme.SidebarTextActive;
        btn.Font = UiTheme.SidebarItemActive;
        btn.Invalidate();
    }

    private void ShowPlaceholder(string title, string subtitle, string body)
    {
        ClearHostedContent();
        lblPlaceholderTitle.Visible = true;
        lblPlaceholderBody.Visible = true;
        lblPlaceholderTitle.Text = subtitle;
        lblPlaceholderBody.Text = body;
        lblScreenTitle.Text = title;
        lblBreadcrumb.Text = title == "Dashboard" ? "Home" : $"Home › {title}";
    }

    private void HostList(Form listForm, string title)
    {
        ClearHostedContent();
        lblPlaceholderTitle.Visible = false;
        lblPlaceholderBody.Visible = false;
        lblScreenTitle.Text = title;
        lblBreadcrumb.Text = title == "Dashboard" ? "Home" : $"Home › {title}";

        listForm.TopLevel = false;
        listForm.FormBorderStyle = FormBorderStyle.None;
        listForm.Dock = DockStyle.Fill;
        pnlContent.Controls.Add(listForm);
        listForm.BringToFront();
        listForm.Show();
        _hostedContent = listForm;
    }

    private void ClearHostedContent()
    {
        if (_hostedContent is null) return;
        pnlContent.Controls.Remove(_hostedContent);
        _hostedContent.Dispose();
        _hostedContent = null;
    }

    private void BtnLogout_Click(object? sender, EventArgs e)
    {
        var result = MessageBox.Show(
            this,
            "Sign out and return to the login screen?",
            "Sign Out",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            SessionContext.SignOut();
            Close();
        }
    }
}
