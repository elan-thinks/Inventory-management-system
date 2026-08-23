using System;
using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Models.Enums;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Delegations;

/// <summary>Delegation Management list (SCR-020, Milestone 15).</summary>
public class DelegationListForm : Form
{
    private readonly DelegationService _service = new();

    private ComboBox cboStatus = null!;
    private ComboBox cboResponsibility = null!;
    private Button btnNew = null!;
    private Button btnRefresh = null!;
    private DataGridView grid = null!;
    private Label lblCount = null!;
    private Label lblEmpty = null!;

    public DelegationListForm()
    {
        BuildUi();
        Load += (_, _) => ReloadGrid();
    }

    private void BuildUi()
    {
        AutoScaleMode = AutoScaleMode.Font;
        Font = UiTheme.Body;
        BackColor = UiTheme.Background;
        FormBorderStyle = FormBorderStyle.None;
        Dock = DockStyle.Fill;
        TopLevel = false;

        var toolbar = new Panel { Dock = DockStyle.Top, Height = 48, BackColor = UiTheme.Background };

        cboStatus = UiTheme.StyleComboBox(new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Width = 130,
            Location = new Point(0, 8)
        });
        cboStatus.Items.AddRange(new object[] { "All statuses", "Active", "Expired", "Revoked" });
        cboStatus.SelectedIndex = 0;
        cboStatus.SelectedIndexChanged += (_, _) => ReloadGrid();

        cboResponsibility = UiTheme.StyleComboBox(new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Width = 140,
            Location = new Point(140, 8)
        });
        cboResponsibility.Items.AddRange(new object[] { "All responsibilities", "Stock-In", "Stock-Out", "Report Access" });
        cboResponsibility.SelectedIndex = 0;
        cboResponsibility.SelectedIndexChanged += (_, _) => ReloadGrid();

        btnRefresh = UiTheme.StyleButton(new Button
        {
            Text = "Refresh",
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Size = new Size(88, 30)
        }, UiTheme.ButtonKind.Secondary);
        btnRefresh.Click += (_, _) => ReloadGrid();

        btnNew = UiTheme.StyleButton(new Button
        {
            Text = "+ New Delegation",
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Size = new Size(140, 30)
        }, UiTheme.ButtonKind.Primary);
        btnNew.Click += (_, _) =>
        {
            using var dlg = new DelegationEditForm();
            if (dlg.ShowDialog(FindForm()) == DialogResult.OK)
                ReloadGrid();
        };

        toolbar.Controls.Add(cboStatus);
        toolbar.Controls.Add(cboResponsibility);
        toolbar.Controls.Add(btnRefresh);
        toolbar.Controls.Add(btnNew);
        toolbar.Resize += (_, _) =>
        {
            btnNew.Left = toolbar.ClientSize.Width - btnNew.Width;
            btnRefresh.Left = btnNew.Left - btnRefresh.Width - 8;
        };

        grid = new DataGridView
        {
            Dock = DockStyle.Fill,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            ReadOnly = true,
            MultiSelect = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        };
        UiTheme.ApplyGridTheme(grid);
        UiTheme.WireStatusBadgeColumn(grid, nameof(DelegationListRow.StatusLabel), value => (value?.ToString() ?? "") switch
        {
            "Active" => ("Active", UiTheme.BadgeTone.Success, '\u2713'),
            "Expired" => ("Expired", UiTheme.BadgeTone.Muted, 'c'),
            _ => ("Revoked", UiTheme.BadgeTone.Error, '\u2715')
        });
        grid.CellContentClick += Grid_CellContentClick;

        lblEmpty = UiTheme.CreateEmptyStateLabel(
            "No delegations yet. Create one to temporarily extend a Staff member's access.");

        lblCount = new Label
        {
            Dock = DockStyle.Bottom,
            Height = 28,
            Font = UiTheme.Label,
            ForeColor = UiTheme.TextMuted
        };

        Controls.Add(grid);
        Controls.Add(lblEmpty);
        Controls.Add(lblCount);
        Controls.Add(toolbar);
    }

    private void ReloadGrid()
    {
        try
        {
            string? status = cboStatus.SelectedIndex switch
            {
                1 => "Active",
                2 => "Expired",
                3 => "Revoked",
                _ => null
            };

            string? resp = cboResponsibility.SelectedIndex switch
            {
                1 => "StockIn",
                2 => "StockOut",
                3 => "ReportAccess",
                _ => null
            };

            var rows = _service.GetList(null, status, resp);
            grid.DataSource = null;
            grid.Columns.Clear();

            if (rows.Count == 0)
            {
                grid.Visible = false;
                lblEmpty.Visible = true;
                bool filtersOn = cboStatus.SelectedIndex != 0 || cboResponsibility.SelectedIndex != 0;
                lblEmpty.Text = filtersOn
                    ? "No delegations match your filters."
                    : "No delegations yet. Create one to temporarily extend a Staff member's access.";
                lblCount.Text = "0 delegations";
                return;
            }

            lblEmpty.Visible = false;
            grid.Visible = true;
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

            Show(nameof(DelegationListRow.DelegationID), "ID", 0.4f);
            Show(nameof(DelegationListRow.DelegatedToName), "To", 1.1f);
            Show(nameof(DelegationListRow.ResponsibilityLabel), "Responsibility", 1.0f);
            Show(nameof(DelegationListRow.StartDate), "Start", 0.7f);
            Show(nameof(DelegationListRow.EndDate), "End", 0.7f);
            Show(nameof(DelegationListRow.StatusLabel), "Status", 0.6f);
            Show(nameof(DelegationListRow.Reason), "Reason", 1.4f);
            Show(nameof(DelegationListRow.DelegatedByName), "By", 1.0f);

            grid.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "colRevoke",
                Text = "Revoke",
                UseColumnTextForButtonValue = true,
                FillWeight = 0.6f,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = { ForeColor = UiTheme.Error, Font = UiTheme.LabelSemibold }
            });
            grid.CellPainting -= Grid_MaskRevokeForNonActive;
            grid.CellPainting += Grid_MaskRevokeForNonActive;

            lblCount.Text = rows.Count == 1 ? "1 delegation" : $"{rows.Count} delegations";
        }
        catch (Exception ex)
        {
            grid.Visible = false;
            lblEmpty.Visible = true;
            lblEmpty.Text = "Unable to load delegations.\n" + ex.Message +
                "\n\nIf the Delegation table is missing, run database/15-delegation.sql.";
            lblCount.Text = "";
        }
    }

    private void Grid_MaskRevokeForNonActive(object? sender, DataGridViewCellPaintingEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
        if (grid.Columns[e.ColumnIndex].Name != "colRevoke") return;
        if (grid.Rows[e.RowIndex].DataBoundItem is not DelegationListRow row) return;
        if (row.Status == DelegationStatus.Active) return; // let the button paint normally

        // Expired/Revoked rows: no action control at all — an em-dash, not a disabled button.
        bool selected = grid.Rows[e.RowIndex].Selected;
        var bg = selected ? UiTheme.RowSelected : (e.RowIndex % 2 == 1 ? UiTheme.RowAlternate : UiTheme.Surface);
        using var brush = new SolidBrush(bg);
        e.Graphics?.FillRectangle(brush, e.CellBounds);
        using var textBrush = new SolidBrush(UiTheme.DisabledText);
        using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        e.Graphics?.DrawString("—", UiTheme.Body, textBrush, e.CellBounds, sf);
        e.Handled = true;
    }

    private void Grid_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        if (grid.Columns[e.ColumnIndex].Name != "colRevoke") return;
        if (grid.Rows[e.RowIndex].DataBoundItem is not DelegationListRow row) return;

        if (row.Status != DelegationStatus.Active)
            return; // no action control on this row — nothing to click

        var confirm = MessageBox.Show(
            FindForm(),
            $"This will immediately end {row.DelegatedToName}'s access to {row.ResponsibilityLabel}.",
            "Revoke Delegation?",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes) return;

        try
        {
            _service.Revoke(row.DelegationID);
            MessageBox.Show(FindForm(), "Delegation revoked.", "Delegations",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReloadGrid();
        }
        catch (AppException ex)
        {
            MessageBox.Show(FindForm(), ex.Message, "Delegations",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
