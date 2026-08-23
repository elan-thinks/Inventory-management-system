using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Inventory;

/// <summary>
/// Inventory Adjustment (FR-ADJ, Milestone 14). Administrator only.
/// </summary>
public class AdjustmentForm : Form
{
    private readonly AdjustmentService _service = new();

    private ComboBox cboProduct = null!;
    private Label lblPreviousQty = null!;
    private NumericUpDown nudNewQty = null!;
    private Label lblDifference = null!;
    private DateTimePicker dtpDate = null!;
    private TextBox txtReason = null!;
    private TextBox txtNotes = null!;
    private Label lblError = null!;
    private Button btnSave = null!;
    private DataGridView grid = null!;

    private int _previousQty;

    public AdjustmentForm()
    {
        BuildUi();
        Load += (_, _) =>
        {
            LoadProducts();
            ReloadRecent();
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

        var formPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 300,
            BackColor = UiTheme.Surface,
            Padding = new Padding(16)
        };

        const int lx = 16;
        const int fw = 300;
        var y = 12;

        void L(string t)
        {
            formPanel.Controls.Add(new Label
            {
                Text = t,
                Font = UiTheme.Label,
                Location = new Point(lx, y),
                AutoSize = true
            });
            y += 18;
        }

        L("Product *");
        cboProduct = new ComboBox
        {
            FlatStyle = FlatStyle.Flat,
            Location = new Point(lx, y),
            Width = fw,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = UiTheme.Body
        };
        cboProduct.SelectedIndexChanged += CboProduct_SelectedIndexChanged;
        formPanel.Controls.Add(cboProduct);
        y += 32;

        L("Previous quantity (system)");
        lblPreviousQty = new Label
        {
            Text = "—",
            Font = UiTheme.Body,
            ForeColor = UiTheme.TextMuted,
            Location = new Point(lx, y),
            AutoSize = true
        };
        formPanel.Controls.Add(lblPreviousQty);
        y += 28;

        L("New quantity *");
        nudNewQty = new NumericUpDown
        {
            Location = new Point(lx, y),
            Width = 120,
            Minimum = 0,
            Maximum = 10_000_000,
            Value = 0,
            Font = UiTheme.Body
        };
        nudNewQty.ValueChanged += (_, _) => UpdateDifference();
        formPanel.Controls.Add(nudNewQty);
        y += 32;

        L("Difference (New − Previous)");
        lblDifference = new Label
        {
            Text = "—",
            Font = UiTheme.Body,
            Location = new Point(lx, y),
            AutoSize = true
        };
        formPanel.Controls.Add(lblDifference);
        y += 28;

        var xR = lx + fw + 40;
        var yR = 12;

        formPanel.Controls.Add(new Label
        {
            Text = "Transaction date *",
            Font = UiTheme.Label,
            Location = new Point(xR, yR),
            AutoSize = true
        });
        yR += 18;
        dtpDate = new DateTimePicker
        {
            Location = new Point(xR, yR),
            Width = 200,
            Format = DateTimePickerFormat.Short,
            MaxDate = DateTime.Today,
            Value = DateTime.Today,
            Font = UiTheme.Body
        };
        formPanel.Controls.Add(dtpDate);
        yR += 36;

        formPanel.Controls.Add(new Label
        {
            Text = "Reason *",
            Font = UiTheme.Label,
            Location = new Point(xR, yR),
            AutoSize = true
        });
        yR += 18;
        txtReason = new TextBox
        {
            BorderStyle = BorderStyle.FixedSingle,
            Location = new Point(xR, yR),
            Width = 280,
            MaxLength = 250,
            Font = UiTheme.Body
        };
        formPanel.Controls.Add(txtReason);
        yR += 32;

        formPanel.Controls.Add(new Label
        {
            Text = "Notes",
            Font = UiTheme.Label,
            Location = new Point(xR, yR),
            AutoSize = true
        });
        yR += 18;
        txtNotes = new TextBox
        {
            BorderStyle = BorderStyle.FixedSingle,
            Location = new Point(xR, yR),
            Width = 280,
            Height = 48,
            Multiline = true,
            MaxLength = 500,
            Font = UiTheme.Body,
            ScrollBars = ScrollBars.Vertical
        };
        formPanel.Controls.Add(txtNotes);

        lblError = new Label
        {
            ForeColor = UiTheme.Error,
            Font = UiTheme.Label,
            Location = new Point(lx, y),
            Size = new Size(520, 28),
            Visible = false
        };
        formPanel.Controls.Add(lblError);

        btnSave = UiTheme.StyleButton(new Button
        {
            Text = "Record Adjustment",
            Size = new Size(160, 34),
            Location = new Point(xR + 120, y)
        }, UiTheme.ButtonKind.Primary);
        btnSave.Click += BtnSave_Click;
        formPanel.Controls.Add(btnSave);

        var lblHint = new Label
        {
            Dock = DockStyle.Top,
            Height = 28,
            Text = "  Recent adjustments",
            Font = UiTheme.Label,
            ForeColor = UiTheme.TextMuted,
            TextAlign = ContentAlignment.MiddleLeft
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

        Controls.Add(grid);
        Controls.Add(lblHint);
        Controls.Add(formPanel);
    }

    private void LoadProducts()
    {
        try
        {
            cboProduct.Items.Clear();
            cboProduct.Items.Add(new LookupItem(0, "— Select product —"));
            foreach (var p in _service.GetActiveProducts())
                cboProduct.Items.Add(new LookupItem(p.ProductID, $"{p.ProductName} (ID {p.ProductID})"));
            cboProduct.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            lblError.Text = "Could not load products. " + ex.Message;
            lblError.Visible = true;
        }
    }

    private void CboProduct_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cboProduct.SelectedItem is not LookupItem item || item.Value <= 0)
        {
            _previousQty = 0;
            lblPreviousQty.Text = "—";
            UpdateDifference();
            return;
        }

        try
        {
            var p = _service.GetProduct(item.Value);
            _previousQty = p.QuantityOnHand;
            lblPreviousQty.Text = _previousQty.ToString(CultureInfo.InvariantCulture);
            nudNewQty.Value = _previousQty;
            UpdateDifference();
        }
        catch
        {
            lblPreviousQty.Text = "—";
        }
    }

    private void UpdateDifference()
    {
        var diff = (int)nudNewQty.Value - _previousQty;
        lblDifference.Text = diff.ToString(CultureInfo.InvariantCulture);
        lblDifference.ForeColor = diff == 0
            ? UiTheme.TextMuted
            : (diff > 0 ? Color.DarkGreen : UiTheme.Error);
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        lblError.Visible = false;
        btnSave.Enabled = false;

        try
        {
            if (cboProduct.SelectedItem is not LookupItem prod || prod.Value <= 0)
                throw new ValidationException("Product is required.");

            var txnId = _service.Record(
                prod.Value,
                (int)nudNewQty.Value,
                dtpDate.Value.Date,
                txtReason.Text,
                txtNotes.Text);

            MessageBox.Show(
                FindForm(),
                $"Adjustment recorded successfully.\nTransaction ID: {txnId}",
                "Inventory Adjustment",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            txtReason.Clear();
            txtNotes.Clear();
            CboProduct_SelectedIndexChanged(null, EventArgs.Empty);
            ReloadRecent();
        }
        catch (AppException ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }
        catch (Exception ex)
        {
            lblError.Text = "Could not record adjustment. " + ex.Message;
            lblError.Visible = true;
        }
        finally
        {
            btnSave.Enabled = true;
        }
    }

    private void ReloadRecent()
    {
        try
        {
            var rows = _service.GetRecentAdjustments(30);
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

            Show(nameof(TransactionHistoryRow.TransactionID), "ID", 0.4f);
            Show(nameof(TransactionHistoryRow.TransactionDate), "Date", 0.7f);
            Show(nameof(TransactionHistoryRow.ProductName), "Product", 1.3f);
            Show(nameof(TransactionHistoryRow.PreviousQuantity), "Prev", 0.5f);
            Show(nameof(TransactionHistoryRow.NewQuantity), "New", 0.5f);
            Show(nameof(TransactionHistoryRow.Difference), "Diff", 0.5f);
            Show(nameof(TransactionHistoryRow.Reason), "Reason", 1.4f);
            Show(nameof(TransactionHistoryRow.UserFullName), "User", 1.0f);
        }
        catch { /* still usable */ }
    }

    private sealed class LookupItem
    {
        public int Value { get; }
        public string Text { get; }
        public LookupItem(int value, string text) { Value = value; Text = text; }
        public override string ToString() => Text;
    }
}
