using System;
using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Models;
using NovaTechIMS.Security;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms;

/// <summary>
/// SCR-002 Dashboard — metrics, recent activity, quick actions (approved UI §5.2).
/// Does not invent new features beyond FR-DASH / screen design.
/// </summary>
public class DashboardForm : Form
{
    private readonly DashboardService _service = new();
    private readonly CurrentUser _user;
    private readonly Action<string>? _navigate;

    private Label lblProducts = null!;
    private Label lblCategories = null!;
    private Label lblSuppliers = null!;
    private Label lblTotalQty = null!;
    private Label lblLow = null!;
    private Label lblOut = null!;
    private DataGridView grid = null!;
    private Label lblError = null!;
    private Label lblEmpty = null!;

    public DashboardForm(CurrentUser user, Action<string>? navigate = null)
    {
        _user = user;
        _navigate = navigate;
        BuildUi();
        Load += (_, _) => Reload();
    }

    private void BuildUi()
    {
        AutoScaleMode = AutoScaleMode.Font;
        Font = UiTheme.Body;
        BackColor = UiTheme.Background;
        FormBorderStyle = FormBorderStyle.None;
        Dock = DockStyle.Fill;
        TopLevel = false;

        var metricsPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 110,
            Padding = new Padding(0, 8, 0, 8),
            WrapContents = false,
            AutoScroll = true
        };

        metricsPanel.Controls.Add(MakeTile("Active products", out lblProducts, UiTheme.Text));
        metricsPanel.Controls.Add(MakeTile("Categories", out lblCategories, UiTheme.Text));
        metricsPanel.Controls.Add(MakeTile("Suppliers", out lblSuppliers, UiTheme.Text));
        metricsPanel.Controls.Add(MakeTile("Total qty on hand", out lblTotalQty, UiTheme.Text));
        metricsPanel.Controls.Add(MakeTile("Low stock", out lblLow, ColorTranslator.FromHtml("#B7791F")));
        metricsPanel.Controls.Add(MakeTile("Out of stock", out lblOut, UiTheme.Error));

        var bottom = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            Padding = new Padding(0, 8, 0, 0)
        };
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70f));
        bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30f));

        var activityPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 0, 8, 0) };
        var lblAct = new Label
        {
            Text = "Recent activity",
            Font = UiTheme.SectionTitle,
            ForeColor = UiTheme.Text,
            Dock = DockStyle.Top,
            Height = 28
        };

        grid = new DataGridView
        {
            Dock = DockStyle.Fill,
            BackgroundColor = UiTheme.Surface,
            BorderStyle = BorderStyle.FixedSingle,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,
            ReadOnly = true,
            MultiSelect = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            RowHeadersVisible = false,
            Font = UiTheme.Body,
            ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Font = UiTheme.Label,
                BackColor = UiTheme.StatusStripBackground,
                ForeColor = UiTheme.Text
            },
            EnableHeadersVisualStyles = false
        };

        lblEmpty = new Label
        {
            Text = "No recent activity yet — record your first Stock-In to get started.",
            Font = UiTheme.Body,
            ForeColor = UiTheme.TextMuted,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            Visible = false
        };

        activityPanel.Controls.Add(grid);
        activityPanel.Controls.Add(lblEmpty);
        activityPanel.Controls.Add(lblAct);

        var actionsPanel = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = UiTheme.Surface,
            Padding = new Padding(12),
            BorderStyle = BorderStyle.FixedSingle
        };
        var lblQa = new Label
        {
            Text = "Quick actions",
            Font = UiTheme.SectionTitle,
            Dock = DockStyle.Top,
            Height = 28
        };
        actionsPanel.Controls.Add(lblQa);

        int ay = 40;
        void AddAction(string text, string navKey)
        {
            var b = new Button
            {
                Text = text,
                Location = new Point(12, ay),
                Size = new Size(200, 34),
                FlatStyle = FlatStyle.Flat,
                Font = UiTheme.Button,
                BackColor = UiTheme.Primary,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Tag = navKey
            };
            b.FlatAppearance.BorderSize = 0;
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

        bottom.Controls.Add(activityPanel, 0, 0);
        bottom.Controls.Add(actionsPanel, 1, 0);

        lblError = new Label
        {
            Dock = DockStyle.Top,
            Height = 28,
            ForeColor = UiTheme.Error,
            Font = UiTheme.Label,
            Visible = false
        };

        Controls.Add(bottom);
        Controls.Add(lblError);
        Controls.Add(metricsPanel);
    }

    private static Panel MakeTile(string caption, out Label valueLabel, Color valueColor)
    {
        var tile = new Panel
        {
            Width = 140,
            Height = 88,
            Margin = new Padding(0, 0, 10, 0),
            BackColor = UiTheme.Surface,
            BorderStyle = BorderStyle.FixedSingle,
            Padding = new Padding(10)
        };

        valueLabel = new Label
        {
            Text = "—",
            Font = new Font("Segoe UI", 18f, FontStyle.Bold),
            ForeColor = valueColor,
            Location = new Point(10, 12),
            AutoSize = true
        };

        var cap = new Label
        {
            Text = caption,
            Font = UiTheme.Label,
            ForeColor = UiTheme.TextMuted,
            Location = new Point(10, 52),
            AutoSize = true
        };

        tile.Controls.Add(valueLabel);
        tile.Controls.Add(cap);
        return tile;
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
