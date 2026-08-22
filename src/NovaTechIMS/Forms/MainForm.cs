using System;
using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Forms.Categories;
using NovaTechIMS.Forms.Customers;
using NovaTechIMS.Forms.Inventory;
using NovaTechIMS.Forms.Products;
using NovaTechIMS.Forms.Suppliers;
using NovaTechIMS.Forms.Users;
using NovaTechIMS.Models;
using NovaTechIMS.Models.Enums;
using NovaTechIMS.Security;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms;

/// <summary>Application shell (Milestone 12: Stock-Out).</summary>
public partial class MainForm : Form
{
    private readonly CurrentUser _currentUser;
    private Button? _activeNavButton;
    private Form? _hostedContent;

    public MainForm(CurrentUser currentUser)
    {
        _currentUser = currentUser;
        InitializeComponent();
        BuildNavigation();
        ShowPlaceholder("Dashboard", "Welcome to NovaTech IMS.",
            "Dashboard metrics and quick actions will arrive in a later milestone.");
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
        lblRoleBadge.Text = "Milestone 12";
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

        AddGroupLabel("REPORTS");
        AddNavItem("Reports");

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
        flpNav.Controls.Add(btn);

        if (text == "Dashboard")
            SetActiveNav(btn);
    }

    private void NavItem_Click(object? sender, EventArgs e)
    {
        if (sender is not Button btn || btn.Tag is not string key)
            return;

        SetActiveNav(btn);

        switch (key)
        {
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
        }

        var title = key;
        var subtitle = "Coming in a later milestone";
        var body = key switch
        {
            "Dashboard" => "Dashboard metrics and quick actions will arrive in a later milestone.",
            "Inventory Adjustment" => "Inventory Adjustment will be implemented in Milestone 14.",
            "Inventory History" => "Inventory History will be implemented in Milestone 13.",
            "Reports" => "Reports will be implemented in Milestone 16.",
            "Delegations" => "Delegation management will be implemented in Milestone 15.",
            _ => "This area is reserved for a future milestone."
        };

        if (key == "Dashboard")
            subtitle = "Welcome to NovaTech IMS.";

        ShowPlaceholder(title, subtitle, body);
        lblMessageStatus.Text = $"Opened: {title}";
    }

    private void SetActiveNav(Button btn)
    {
        if (_activeNavButton != null)
        {
            _activeNavButton.BackColor = UiTheme.SidebarBackground;
            _activeNavButton.ForeColor = UiTheme.SidebarText;
            _activeNavButton.Font = UiTheme.SidebarItem;
            _activeNavButton.FlatAppearance.BorderSize = 0;
        }

        _activeNavButton = btn;
        btn.BackColor = Color.FromArgb(40, 255, 255, 255);
        btn.ForeColor = UiTheme.SidebarTextActive;
        btn.Font = UiTheme.SidebarItemActive;
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
        lblBreadcrumb.Text = $"Home › {title}";

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
