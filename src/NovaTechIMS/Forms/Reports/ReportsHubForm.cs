using System;
using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Reports;

/// <summary>Reports hub (SCR-016) — M19 card layout; generation unchanged.</summary>
public class ReportsHubForm : Form
{
    private readonly ReportService _service = new();

    private FlowLayoutPanel cards = null!;
    private Panel filterPanel = null!;
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
    private Label lblSelected = null!;

    private int _selectedReport;

    public ReportsHubForm()
    {
        BuildUi();
        Load += (_, _) =>
        {
            LoadProducts();
            SelectReport(0);
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

        cards = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 220,
            Padding = new Padding(8),
            AutoScroll = true,
            WrapContents = true
        };

        AddCard(0, "Current Inventory", "All active products with quantity and stock status.");
        AddCard(1, "Low Stock", "Products at or below minimum stock level.");
        AddCard(2, "Out of Stock", "Products with zero quantity on hand.");
        AddCard(3, "Stock-In", "Goods received over a date range.");
        AddCard(4, "Stock-Out", "Goods issued over a date range.");
        AddCard(5, "Transaction History", "Full movement log with optional filters.");

        filterPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 140,
            Padding = new Padding(16),
            BackColor = UiTheme.Surface
        };

        lblSelected = new Label
        {
            Text = "Selected: Current Inventory",
            Font = UiTheme.SectionTitle,
            Location = new Point(16, 12),
            AutoSize = true
        };
        filterPanel.Controls.Add(lblSelected);

        lblFrom = new Label { Text = "From", Font = UiTheme.Label, Location = new Point(16, 48), AutoSize = true };
        dtpFrom = new DateTimePicker
        {
            Location = new Point(60, 44),
            Width = 130,
            Format = DateTimePickerFormat.Short,
            Value = DateTime.Today.AddDays(-30),
            Font = UiTheme.Body
        };
        lblTo = new Label { Text = "To", Font = UiTheme.Label, Location = new Point(210, 48), AutoSize = true };
        dtpTo = new DateTimePicker
        {
            Location = new Point(240, 44),
            Width = 130,
            Format = DateTimePickerFormat.Short,
            Value = DateTime.Today,
            Font = UiTheme.Body
        };

        lblType = new Label { Text = "Type", Font = UiTheme.Label, Location = new Point(16, 84), AutoSize = true };
        cboType = new ComboBox
        {
            Location = new Point(60, 80),
            Width = 150,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = UiTheme.Body
        };
        cboType.Items.AddRange(new object[] { "All", "Stock-In", "Stock-Out", "Adjustment" });
        cboType.SelectedIndex = 0;

        lblProduct = new Label { Text = "Product", Font = UiTheme.Label, Location = new Point(230, 84), AutoSize = true };
        cboProduct = new ComboBox
        {
            Location = new Point(290, 80),
            Width = 220,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = UiTheme.Body
        };

        btnRun = new Button
        {
            Text = "Generate",
            Location = new Point(540, 76),
            Size = new Size(120, 34),
            FlatStyle = FlatStyle.Flat,
            Font = UiTheme.Button,
            BackColor = UiTheme.Primary,
            ForeColor = Color.White,
            Cursor = Cursors.Hand
        };
        btnRun.FlatAppearance.BorderSize = 0;
        btnRun.Click += BtnRun_Click;

        lblError = new Label
        {
            ForeColor = UiTheme.Error,
            Font = UiTheme.Label,
            Location = new Point(16, 112),
            Size = new Size(640, 24),
            Visible = false
        };

        filterPanel.Controls.Add(lblFrom);
        filterPanel.Controls.Add(dtpFrom);
        filterPanel.Controls.Add(lblTo);
        filterPanel.Controls.Add(dtpTo);
        filterPanel.Controls.Add(lblType);
        filterPanel.Controls.Add(cboType);
        filterPanel.Controls.Add(lblProduct);
        filterPanel.Controls.Add(cboProduct);
        filterPanel.Controls.Add(btnRun);
        filterPanel.Controls.Add(lblError);

        var lblHint = new Label
        {
            Dock = DockStyle.Bottom,
            Height = 36,
            Text = "  Reports open in a separate window. Use Print… on the viewer if needed. Export is out of MVP scope.",
            Font = UiTheme.Label,
            ForeColor = UiTheme.TextMuted,
            TextAlign = ContentAlignment.MiddleLeft
        };

        Controls.Add(lblHint);
        Controls.Add(filterPanel);
        Controls.Add(cards);
    }

    private void AddCard(int index, string title, string description)
    {
        var card = new Panel
        {
            Width = 200,
            Height = 100,
            Margin = new Padding(8),
            BackColor = UiTheme.Surface,
            BorderStyle = BorderStyle.FixedSingle,
            Cursor = Cursors.Hand,
            Tag = index
        };

        var t = new Label
        {
            Text = title,
            Font = UiTheme.SectionTitle,
            ForeColor = UiTheme.Primary,
            Location = new Point(12, 12),
            AutoSize = true
        };
        var d = new Label
        {
            Text = description,
            Font = UiTheme.Label,
            ForeColor = UiTheme.TextMuted,
            Location = new Point(12, 40),
            Size = new Size(176, 48)
        };

        void Select(object? s, EventArgs e) => SelectReport(index);
        card.Click += Select;
        t.Click += Select;
        d.Click += Select;

        card.Controls.Add(t);
        card.Controls.Add(d);
        cards.Controls.Add(card);
    }

    private void SelectReport(int index)
    {
        _selectedReport = index;
        var names = new[]
        {
            "Current Inventory", "Low Stock", "Out of Stock",
            "Stock-In", "Stock-Out", "Transaction History"
        };
        lblSelected.Text = "Selected: " + names[index];

        foreach (Control c in cards.Controls)
        {
            if (c is Panel p && p.Tag is int i)
                p.BackColor = i == index ? UiTheme.InfoTint : UiTheme.Surface;
        }

        bool needsDates = index is 3 or 4 or 5;
        bool needsTypeProduct = index == 5;

        lblFrom.Visible = dtpFrom.Visible = needsDates;
        lblTo.Visible = dtpTo.Visible = needsDates;
        lblType.Visible = cboType.Visible = needsTypeProduct;
        lblProduct.Visible = cboProduct.Visible = needsTypeProduct;
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
        catch { /* run surfaces errors */ }
    }

    private void BtnRun_Click(object? sender, EventArgs e)
    {
        lblError.Visible = false;
        btnRun.Enabled = false;

        try
        {
            string title;
            object data;

            switch (_selectedReport)
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
            lblError.Text = ErrorPresenter.ToUserMessage(DbExceptionMapper.Map(ex));
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
