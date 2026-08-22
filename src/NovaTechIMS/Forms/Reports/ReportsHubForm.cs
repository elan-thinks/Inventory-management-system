using System;
using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Reports;

/// <summary>
/// Reports hub (SCR-016): choose report type, filters, run viewer (SCR-017).
/// </summary>
public class ReportsHubForm : Form
{
    private readonly ReportService _service = new();

    private ComboBox cboReport = null!;
    private DateTimePicker dtpFrom = null!;
    private DateTimePicker dtpTo = null!;
    private ComboBox cboType = null!;
    private ComboBox cboProduct = null!;
    private Label lblFrom = null!;
    private Label lblTo = null!;
    private Label lblType = null!;
    private Label lblProduct = null!;
    private Button btnRun = null!;
    private Label lblError = null!;
    private Label lblHint = null!;

    public ReportsHubForm()
    {
        BuildUi();
        Load += (_, _) =>
        {
            LoadProducts();
            UpdateFilterVisibility();
        };
    }

    private void BuildUi()
    {
        AutoScaleMode = AutoScaleMode.Font;
        Font = UiTheme.Body;
        BackColor = UiTheme.Background;
        FormBorderStyle = FormBorderStyle.None;
        Dock = DockStyle.Fill;
        TopLevel = false;

        var card = new Panel
        {
            Location = new Point(24, 24),
            Size = new Size(560, 320),
            BackColor = UiTheme.Surface,
            Padding = new Padding(20)
        };

        int y = 16;
        const int lx = 20;

        card.Controls.Add(new Label
        {
            Text = "Select report",
            Font = UiTheme.Label,
            Location = new Point(lx, y),
            AutoSize = true
        });
        y += 20;

        cboReport = new ComboBox
        {
            Location = new Point(lx, y),
            Width = 400,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = UiTheme.Body
        };
        cboReport.Items.AddRange(new object[]
        {
            "Current Inventory",
            "Low Stock",
            "Out of Stock",
            "Stock-In (date range)",
            "Stock-Out (date range)",
            "Inventory Transaction History"
        });
        cboReport.SelectedIndex = 0;
        cboReport.SelectedIndexChanged += (_, _) => UpdateFilterVisibility();
        card.Controls.Add(cboReport);
        y += 40;

        lblFrom = new Label { Text = "From", Font = UiTheme.Label, Location = new Point(lx, y), AutoSize = true };
        card.Controls.Add(lblFrom);
        dtpFrom = new DateTimePicker
        {
            Location = new Point(lx + 50, y - 2),
            Width = 130,
            Format = DateTimePickerFormat.Short,
            Value = DateTime.Today.AddDays(-30),
            Font = UiTheme.Body
        };
        card.Controls.Add(dtpFrom);

        lblTo = new Label { Text = "To", Font = UiTheme.Label, Location = new Point(lx + 200, y), AutoSize = true };
        card.Controls.Add(lblTo);
        dtpTo = new DateTimePicker
        {
            Location = new Point(lx + 230, y - 2),
            Width = 130,
            Format = DateTimePickerFormat.Short,
            Value = DateTime.Today,
            Font = UiTheme.Body
        };
        card.Controls.Add(dtpTo);
        y += 36;

        lblType = new Label { Text = "Type", Font = UiTheme.Label, Location = new Point(lx, y), AutoSize = true };
        card.Controls.Add(lblType);
        cboType = new ComboBox
        {
            Location = new Point(lx + 50, y - 2),
            Width = 150,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = UiTheme.Body
        };
        cboType.Items.AddRange(new object[] { "All", "Stock-In", "Stock-Out", "Adjustment" });
        cboType.SelectedIndex = 0;
        card.Controls.Add(cboType);
        y += 36;

        lblProduct = new Label { Text = "Product", Font = UiTheme.Label, Location = new Point(lx, y), AutoSize = true };
        card.Controls.Add(lblProduct);
        cboProduct = new ComboBox
        {
            Location = new Point(lx + 60, y - 2),
            Width = 280,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = UiTheme.Body
        };
        card.Controls.Add(cboProduct);
        y += 44;

        btnRun = new Button
        {
            Text = "Run report",
            Location = new Point(lx, y),
            Size = new Size(130, 34),
            FlatStyle = FlatStyle.Flat,
            Font = UiTheme.Button,
            BackColor = UiTheme.Primary,
            ForeColor = Color.White,
            Cursor = Cursors.Hand
        };
        btnRun.FlatAppearance.BorderSize = 0;
        btnRun.Click += BtnRun_Click;
        card.Controls.Add(btnRun);

        lblError = new Label
        {
            ForeColor = UiTheme.Error,
            Font = UiTheme.Label,
            Location = new Point(lx + 150, y),
            Size = new Size(320, 40),
            Visible = false
        };
        card.Controls.Add(lblError);

        lblHint = new Label
        {
            Dock = DockStyle.Bottom,
            Height = 40,
            Text = "  Reports open in a separate window. Use Print… on the viewer if needed. Export is out of MVP scope.",
            Font = UiTheme.Label,
            ForeColor = UiTheme.TextMuted,
            TextAlign = ContentAlignment.MiddleLeft
        };

        Controls.Add(card);
        Controls.Add(lblHint);
    }

    private void LoadProducts()
    {
        try
        {
            cboProduct.Items.Clear();
            cboProduct.Items.Add(new LookupItem(0, "All products"));
            foreach (var p in _service.GetProductsForFilter())
                cboProduct.Items.Add(new LookupItem(p.ProductID, p.ProductName));
            cboProduct.SelectedIndex = 0;
        }
        catch
        {
            // run will surface errors
        }
    }

    private void UpdateFilterVisibility()
    {
        var idx = cboReport.SelectedIndex;
        bool needsDates = idx is 3 or 4 or 5;
        bool needsTypeProduct = idx == 5;

        lblFrom.Visible = dtpFrom.Visible = needsDates;
        lblTo.Visible = dtpTo.Visible = needsDates;
        lblType.Visible = cboType.Visible = needsTypeProduct;
        lblProduct.Visible = cboProduct.Visible = needsTypeProduct;
    }

    private void BtnRun_Click(object? sender, EventArgs e)
    {
        lblError.Visible = false;
        btnRun.Enabled = false;

        try
        {
            string title;
            object data;

            switch (cboReport.SelectedIndex)
            {
                case 0:
                    title = "Current Inventory";
                    data = _service.GetCurrentInventory();
                    break;
                case 1:
                    title = "Low Stock";
                    data = _service.GetLowStock();
                    break;
                case 2:
                    title = "Out of Stock";
                    data = _service.GetOutOfStock();
                    break;
                case 3:
                    title = $"Stock-In ({dtpFrom.Value:yyyy-MM-dd} – {dtpTo.Value:yyyy-MM-dd})";
                    data = _service.GetStockInReport(dtpFrom.Value, dtpTo.Value);
                    break;
                case 4:
                    title = $"Stock-Out ({dtpFrom.Value:yyyy-MM-dd} – {dtpTo.Value:yyyy-MM-dd})";
                    data = _service.GetStockOutReport(dtpFrom.Value, dtpTo.Value);
                    break;
                default:
                {
                    int? productId = null;
                    if (cboProduct.SelectedItem is LookupItem item && item.Value > 0)
                        productId = item.Value;
                    var type = cboType.SelectedItem?.ToString();
                    title = $"Transaction History ({dtpFrom.Value:yyyy-MM-dd} – {dtpTo.Value:yyyy-MM-dd})";
                    data = _service.GetTransactionHistoryReport(
                        dtpFrom.Value, dtpTo.Value, type, productId);
                    break;
                }
            }

            using var viewer = new ReportViewerForm(title, data);
            viewer.ShowDialog(FindForm());
        }
        catch (AppException ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }
        catch (Exception ex)
        {
            lblError.Text = "Could not run report. " + ex.Message;
            lblError.Visible = true;
        }
        finally
        {
            btnRun.Enabled = true;
        }
    }

    private sealed class LookupItem
    {
        public int Value { get; }
        public string Text { get; }
        public LookupItem(int value, string text) { Value = value; Text = text; }
        public override string ToString() => Text;
    }
}
