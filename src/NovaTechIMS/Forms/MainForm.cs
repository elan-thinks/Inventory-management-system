using System;
using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Forms.Categories;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms;

/// <summary>
/// Application shell (sidebar + header + content + status strip).
/// Milestone 5: Categories opens CategoryListForm in the content area.
/// Other nav items remain placeholders until their milestones.
/// </summary>
public partial class MainForm : Form
{
    private readonly string _displayName;
    private readonly string _roleLabel;
    private Button? _activeNavButton;
    private Form? _hostedContent;

    public MainForm(string displayName, string roleLabel)
    {
        _displayName = displayName;
        _roleLabel = roleLabel;
        InitializeComponent();
        BuildNavigation();
        ShowPlaceholder("Dashboard", "Welcome to NovaTech IMS.",
            "Dashboard metrics and quick actions will arrive in a later milestone.");
    }

    private void MainForm_Load(object? sender, EventArgs e)
    {
        lblUserStatus.Text = $"{_displayName}  ·  {_roleLabel}";
        lblDateStatus.Text = DateTime.Now.ToString("dddd, dd MMM yyyy");
        lblMessageStatus.Text = "Ready";
        MinimumSize = new Size(UiTheme.MinWindowWidth, UiTheme.MinWindowHeight);
        lblRoleBadge.Text = "Milestone 5";
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
        AddNavItem("Stock In");
        AddNavItem("Stock Out");
        AddNavItem("Inventory Adjustment");
        AddNavItem("Inventory History");
        AddGroupLabel("REPORTS");
        AddNavItem("Reports");
        AddGroupLabel("ADMINISTRATION");
        AddNavItem("User Management");
        AddNavItem("Delegations");
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

        if (key == "Categories")
        {
            ShowCategories();
            lblMessageStatus.Text = "Opened: Categories";
            return;
        }

        var title = key;
        var subtitle = "Coming in a later milestone";
        var body = key switch
        {
            "Dashboard" => "Dashboard metrics and quick actions will arrive in a later milestone.",
            "Products" => "Product list and product forms will be implemented in Milestone 8.",
            "Suppliers" => "Supplier management will be implemented in Milestone 6.",
            "Customers" => "Customer management will be implemented in Milestone 7.",
            "Stock In" => "Stock-In will be implemented in Milestone 11.",
            "Stock Out" => "Stock-Out will be implemented in Milestone 12.",
            "Inventory Adjustment" => "Inventory Adjustment will be implemented in Milestone 14.",
            "Inventory History" => "Inventory History will be implemented in Milestone 13.",
            "Reports" => "Reports will be implemented in Milestone 16.",
            "User Management" => "User management will be implemented with authentication (Milestone 9).",
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

    private void ShowCategories()
    {
        ClearHostedContent();

        lblPlaceholderTitle.Visible = false;
        lblPlaceholderBody.Visible = false;

        lblScreenTitle.Text = "Categories";
        lblBreadcrumb.Text = "Home › Categories";

        var listForm = new CategoryListForm();
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
        if (_hostedContent is null)
            return;

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
            Close();
    }
}
