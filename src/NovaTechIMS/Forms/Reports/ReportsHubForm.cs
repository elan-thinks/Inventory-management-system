using System;
using System.Windows.Forms;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Reports;

/// <summary>Reports hub — Designer-based (partial).</summary>
public partial class ReportsHubForm : Form
{
    private readonly ReportService _service = new();

    public ReportsHubForm()
    {
        InitializeComponent();
        ApplyRuntimeStyling();
        Load += (_, _) =>
        {
            LoadProducts();
            UpdateFilterVisibility();
        };
    }

    private void ApplyRuntimeStyling()
    {
        Font = UiTheme.Body;
        BackColor = UiTheme.Background;
        card.BackColor = UiTheme.Surface;
        UiTheme.ApplyRoundedRegion(card, UiTheme.RadiusLg);
        lblSelect.Font = UiTheme.Label;
        lblSelect.ForeColor = UiTheme.Text;
        foreach (var lbl in new[] { lblFrom, lblTo, lblType, lblProduct })
        {
            lbl.Font = UiTheme.Label;
            lbl.ForeColor = UiTheme.TextMuted;
        }
        lblError.Font = UiTheme.Label;
        lblError.ForeColor = UiTheme.Error;
        lblHint.Font = UiTheme.Label;
        lblHint.ForeColor = UiTheme.TextMuted;
        UiTheme.StyleComboBox(cboReport);
        UiTheme.StyleComboBox(cboType);
        UiTheme.StyleComboBox(cboProduct);
        UiTheme.StyleDatePicker(dtpFrom);
        UiTheme.StyleDatePicker(dtpTo);
        UiTheme.StyleButton(btnRun, UiTheme.ButtonKind.Primary);
        dtpFrom.Value = DateTime.Today.AddDays(-30);
        dtpTo.Value = DateTime.Today;
    }

    private void CboReport_SelectedIndexChanged(object? sender, EventArgs e) => UpdateFilterVisibility();

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
        catch { }
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
