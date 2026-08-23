using System;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Suppliers;

/// <summary>
/// SCR-008 Supplier List — Designer-based (partial).
/// </summary>
public partial class SupplierListForm : Form
{
    private readonly SupplierService _service = new();

    public SupplierListForm()
    {
        InitializeComponent();
        ApplyRuntimeStyling();
        Load += (_, _) => ReloadGrid();
    }

    private void ApplyRuntimeStyling()
    {
        UiTheme.StyleTextBox(txtSearch);
        UiTheme.StyleComboBox(cboStatus);
        UiTheme.StyleButton(btnClearFilters, UiTheme.ButtonKind.Ghost);
        UiTheme.StyleButton(btnRefresh, UiTheme.ButtonKind.Secondary);
        UiTheme.StyleButton(btnAdd, UiTheme.ButtonKind.Primary);
        UiTheme.ApplyGridTheme(grid);
        UiTheme.WireStatusBadgeColumn(grid, "Status", value => (value?.ToString() ?? "") switch
        {
            "Active" => ("Active", UiTheme.BadgeTone.Success, '\u2713'),
            _ => ("Inactive", UiTheme.BadgeTone.Muted, '\u2715')
        });
        lblEmpty.Text = "No suppliers yet. Add your first supplier to get started.";
        Toolbar_Resize(toolbar, EventArgs.Empty);
    }

    private void Toolbar_Resize(object? sender, EventArgs e)
    {
        btnAdd.Left = toolbar.ClientSize.Width - btnAdd.Width;
        btnRefresh.Left = btnAdd.Left - btnRefresh.Width - 8;
    }

    private void TxtSearch_TextChanged(object? sender, EventArgs e) => ReloadGrid();
    private void CboStatus_SelectedIndexChanged(object? sender, EventArgs e) => ReloadGrid();
    private void BtnRefresh_Click(object? sender, EventArgs e) => ReloadGrid();

    private void BtnClearFilters_Click(object? sender, EventArgs e)
    {
        txtSearch.Clear();
        cboStatus.SelectedIndex = 0;
        ReloadGrid();
    }

    private void ReloadGrid()
    {
        try
        {
            string? search = string.IsNullOrWhiteSpace(txtSearch.Text) ? null : txtSearch.Text;
            bool? activeFilter = cboStatus.SelectedIndex switch
            {
                1 => true,
                2 => false,
                _ => null
            };

            btnClearFilters.Visible = search is not null || activeFilter is not null;

            var rows = _service.GetList(search, activeFilter);

            grid.DataSource = null;
            grid.Columns.Clear();

            if (rows.Count == 0)
            {
                grid.Visible = false;
                lblEmpty.Visible = true;
                lblEmpty.Text = search is not null || activeFilter is not null
                    ? "No suppliers match your search/filters."
                    : "No suppliers yet. Add your first supplier to get started.";
                lblCount.Text = "0 suppliers";
                return;
            }

            lblEmpty.Visible = false;
            grid.Visible = true;
            grid.DataSource = rows;
            ConfigureColumns();
            lblCount.Text = rows.Count == 1 ? "1 supplier" : $"{rows.Count} suppliers";
        }
        catch (Exception ex)
        {
            grid.Visible = false;
            lblEmpty.Visible = true;
            lblEmpty.Text = "Unable to load suppliers. Check the database connection.\n" + ex.Message;
            lblCount.Text = "";
        }
    }

    private void ConfigureColumns()
    {
        foreach (DataGridViewColumn col in grid.Columns)
            col.Visible = false;

        void Show(string name, string header, float fillWeight = 1f)
        {
            if (grid.Columns[name] is not DataGridViewColumn c)
                return;
            c.Visible = true;
            c.HeaderText = header;
            c.FillWeight = fillWeight;
        }

        Show(nameof(SupplierListRow.SupplierID), "ID", 0.4f);
        Show(nameof(SupplierListRow.SupplierName), "Name", 1.4f);
        Show(nameof(SupplierListRow.ContactPerson), "Contact", 1.0f);
        Show(nameof(SupplierListRow.Phone), "Phone", 0.9f);
        Show(nameof(SupplierListRow.Email), "Email", 1.2f);
        Show(nameof(SupplierListRow.Status), "Status", 0.6f);

        if (grid.Columns.Contains("colEdit"))
            grid.Columns.Remove("colEdit");
        if (grid.Columns.Contains("colDelete"))
            grid.Columns.Remove("colDelete");

        grid.Columns.Add(new DataGridViewButtonColumn
        {
            Name = "colEdit",
            HeaderText = "",
            Text = "Edit",
            UseColumnTextForButtonValue = true,
            FillWeight = 0.45f,
            FlatStyle = FlatStyle.Flat,
            DefaultCellStyle = { ForeColor = UiTheme.Primary, Font = UiTheme.LabelSemibold }
        });
        grid.Columns.Add(new DataGridViewButtonColumn
        {
            Name = "colDelete",
            HeaderText = "",
            Text = "Delete",
            UseColumnTextForButtonValue = true,
            FillWeight = 0.5f,
            FlatStyle = FlatStyle.Flat,
            DefaultCellStyle = { ForeColor = UiTheme.Error, Font = UiTheme.LabelSemibold }
        });
    }

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        using var dlg = new SupplierEditForm();
        if (dlg.ShowDialog(FindForm()) == DialogResult.OK)
            ReloadGrid();
    }

    private void Grid_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
            return;

        var col = grid.Columns[e.ColumnIndex];
        if (grid.Rows[e.RowIndex].DataBoundItem is not SupplierListRow row)
            return;

        if (col.Name == "colEdit")
        {
            using var dlg = new SupplierEditForm(row.SupplierID);
            if (dlg.ShowDialog(FindForm()) == DialogResult.OK)
                ReloadGrid();
            return;
        }

        if (col.Name == "colDelete")
            HandleDelete(row);
    }

    private void HandleDelete(SupplierListRow row)
    {
        var confirm = MessageBox.Show(
            FindForm(),
            $"Delete supplier \"{row.SupplierName}\"?",
            "Delete Supplier",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes)
            return;

        try
        {
            _service.DeleteOrThrowIfReferenced(row.SupplierID);
            MessageBox.Show(FindForm(), "Supplier deleted.", "Suppliers",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReloadGrid();
        }
        catch (BusinessRuleException ex)
        {
            var result = MessageBox.Show(
                FindForm(),
                ex.Message + "\n\nMark this supplier Inactive instead?",
                "Cannot Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _service.Deactivate(row.SupplierID);
                    MessageBox.Show(FindForm(), "Supplier marked Inactive.", "Suppliers",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReloadGrid();
                }
                catch (Exception deactivateEx)
                {
                    MessageBox.Show(FindForm(), deactivateEx.Message, "Suppliers",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        catch (AppException ex)
        {
            MessageBox.Show(FindForm(), ex.Message, "Suppliers",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show(FindForm(), "Delete failed: " + ex.Message, "Suppliers",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public void RefreshList() => ReloadGrid();
}
