using System;
using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Models.Enums;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Delegations;

/// <summary>Delegation Management list — designer owns layout.</summary>
public partial class DelegationListForm : Form
{
    private DelegationService? _service;
    private DelegationService Service => _service ??= new DelegationService();

    public DelegationListForm()
    {
        InitializeComponent();

        if (DesignTime.IsActive)
            return;

        grid.Columns.Clear();
        grid.ColumnCount = 0;

        ApplyRuntimeStyling();
        Load += (_, _) => ReloadGrid();
    }

    private void ApplyRuntimeStyling()
    {
        Font = UiTheme.Body;
        BackColor = UiTheme.Background;
        toolbar.BackColor = UiTheme.Background;
        lblEmpty.Font = UiTheme.Body;
        lblEmpty.ForeColor = UiTheme.TextMuted;
        lblCount.Font = UiTheme.Label;
        lblCount.ForeColor = UiTheme.TextMuted;
        if (lblStatus is not null) lblStatus.ForeColor = UiTheme.TextMuted;
        if (lblResp is not null) lblResp.ForeColor = UiTheme.TextMuted;
        UiTheme.StyleComboBox(cboStatus);
        UiTheme.StyleComboBox(cboResponsibility);
        UiTheme.StyleButton(btnRefresh, UiTheme.ButtonKind.Secondary);
        UiTheme.StyleButton(btnNew, UiTheme.ButtonKind.Primary);
        UiTheme.ApplyGridTheme(grid);
        UiTheme.WireStatusBadgeColumn(grid, nameof(DelegationListRow.StatusLabel), value => (value?.ToString() ?? "") switch
        {
            "Active" => ("Active", UiTheme.BadgeTone.Success, '\u2713'),
            "Expired" => ("Expired", UiTheme.BadgeTone.Muted, 'c'),
            _ => ("Revoked", UiTheme.BadgeTone.Error, '\u2715')
        });
        Toolbar_Resize(toolbar, EventArgs.Empty);
    }

    private void Toolbar_Resize(object? sender, EventArgs e)
    {
        if (toolbar is null || btnNew is null || btnRefresh is null) return;
        btnNew.Left = Math.Max(8, toolbar.ClientSize.Width - btnNew.Width - 8);
        btnRefresh.Left = btnNew.Left - btnRefresh.Width - 8;
    }

    private void CboFilter_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (DesignTime.IsActive) return;
        ReloadGrid();
    }

    private void BtnRefresh_Click(object? sender, EventArgs e) => ReloadGrid();

    private void BtnNew_Click(object? sender, EventArgs e)
    {
        using var dlg = new DelegationEditForm();
        if (dlg.ShowDialog(FindForm()) == DialogResult.OK)
            ReloadGrid();
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

            var rows = Service.GetList(null, status, resp);
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
            lblEmpty.Text = "Unable to load delegations.\n" + ErrorPresenter.Classify(DbExceptionMapper.Map(ex)).Message +
                "\n\nIf the Delegation table is missing, run database/15-delegation.sql.";
            lblCount.Text = "";
        }
    }

    private void Grid_MaskRevokeForNonActive(object? sender, DataGridViewCellPaintingEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
        if (grid.Columns[e.ColumnIndex].Name != "colRevoke") return;
        if (grid.Rows[e.RowIndex].DataBoundItem is not DelegationListRow row) return;
        if (row.Status == DelegationStatus.Active) return;

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
        if (row.Status != DelegationStatus.Active) return;

        var confirm = MessageBox.Show(
            FindForm(),
            $"This will immediately end {row.DelegatedToName}'s access to {row.ResponsibilityLabel}.",
            "Revoke Delegation?",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes) return;

        try
        {
            Service.Revoke(row.DelegationID);
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
