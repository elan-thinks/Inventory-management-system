using System;
using System.Windows.Forms;
using NovaTechIMS.Data;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Customers;

/// <summary>
/// SCR-010 Customer List — designer owns layout; this file is data + filters only.
/// </summary>
public partial class CustomerListForm : Form
{
    private CustomerService? _service;
    private CustomerService Service => _service ??= new CustomerService();

    public CustomerListForm()
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
        BackColor = UiTheme.Background;
        toolbar.BackColor = UiTheme.Background;
        lblCount.BackColor = UiTheme.Background;
        lblCount.ForeColor = UiTheme.TextMuted;
        lblEmpty.ForeColor = UiTheme.TextMuted;
        lblEmpty.BackColor = UiTheme.Background;

        if (lblSearch is not null) lblSearch.ForeColor = UiTheme.TextMuted;
        if (lblStatus is not null) lblStatus.ForeColor = UiTheme.TextMuted;

        UiTheme.StyleTextBox(txtSearch);
        UiTheme.StyleComboBox(cboStatus);
        UiTheme.StyleButton(btnClearFilters, UiTheme.ButtonKind.Ghost);
        UiTheme.StyleButton(btnRefresh, UiTheme.ButtonKind.Secondary);
        UiTheme.StyleButton(btnAdd, UiTheme.ButtonKind.Primary);
        UiTheme.ApplyGridTheme(grid);
        UiTheme.WireStatusBadgeColumn(grid, nameof(CustomerListRow.Status), value => (value?.ToString() ?? "") switch
        {
            "Active" => ("Active", UiTheme.BadgeTone.Success, '\u2713'),
            _ => ("Inactive", UiTheme.BadgeTone.Muted, '\u2715')
        });

        btnClearFilters.Visible = false;
        Toolbar_Resize(toolbar, EventArgs.Empty);
    }

    private void Toolbar_Resize(object? sender, EventArgs e)
    {
        if (toolbar is null || btnAdd is null || btnRefresh is null) return;
        btnAdd.Left = Math.Max(8, toolbar.ClientSize.Width - btnAdd.Width - 8);
        btnRefresh.Left = btnAdd.Left - btnRefresh.Width - 8;
    }

    private void TxtSearch_TextChanged(object? sender, EventArgs e)
    {
        if (DesignTime.IsActive) return;
        ReloadGrid();
    }

    private void CboStatus_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (DesignTime.IsActive) return;
        ReloadGrid();
    }

    private void BtnRefresh_Click(object? sender, EventArgs e) => ReloadGrid();

    private void BtnClearFilters_Click(object? sender, EventArgs e)
    {
        txtSearch.Clear();
        if (cboStatus.Items.Count > 0)
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

            var rows = Service.GetList(search, activeFilter);

            grid.DataSource = null;
            grid.Columns.Clear();

            if (rows.Count == 0)
            {
                grid.Visible = false;
                lblEmpty.Visible = true;
                lblEmpty.Text = search is not null || activeFilter is not null
                    ? "No customers match your search/filters."
                    : "No customers yet. Add your first customer to get started.";
                lblCount.Text = "0 customers";
                return;
            }

            lblEmpty.Visible = false;
            grid.Visible = true;
            grid.DataSource = rows;
            ConfigureColumns();
            lblCount.Text = rows.Count == 1 ? "1 customer" : $"{rows.Count} customers";
        }
        catch (Exception ex)
        {
            grid.Visible = false;
            lblEmpty.Visible = true;
            var mapped = DbExceptionMapper.Map(ex);
            lblEmpty.Text = "Unable to load customers.\n" + ErrorPresenter.Classify(mapped).Message;
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

        Show(nameof(CustomerListRow.CustomerID), "ID", 0.4f);
        Show(nameof(CustomerListRow.CustomerName), "Name", 1.5f);
        Show(nameof(CustomerListRow.Phone), "Phone", 1.0f);
        Show(nameof(CustomerListRow.Email), "Email", 1.3f);
        Show(nameof(CustomerListRow.Status), "Status", 0.6f);

        if (grid.Columns.Contains("colEdit"))
            grid.Columns.Remove("colEdit");
        if (grid.Columns.Contains("colDelete"))
            grid.Columns.Remove("colDelete");

        grid.Columns.Add(AppIcons.CreateEditColumn(0.4f));
        grid.Columns.Add(AppIcons.CreateDeleteColumn(0.4f));
        AppIcons.FillActionIcons(grid);
    }

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        using var dlg = new CustomerEditForm();
        if (dlg.ShowDialog(FindForm()) == DialogResult.OK)
            ReloadGrid();
    }

    private void Grid_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
            return;

        var col = grid.Columns[e.ColumnIndex];
        if (grid.Rows[e.RowIndex].DataBoundItem is not CustomerListRow row)
            return;

        if (col.Name == "colEdit")
        {
            using var dlg = new CustomerEditForm(row.CustomerID);
            if (dlg.ShowDialog(FindForm()) == DialogResult.OK)
                ReloadGrid();
            return;
        }

        if (col.Name == "colDelete")
            HandleDelete(row);
    }

    private void HandleDelete(CustomerListRow row)
    {
        var confirm = MessageBox.Show(
            FindForm(),
            $"Delete customer \"{row.CustomerName}\"?",
            "Delete Customer",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes)
            return;

        try
        {
            Service.DeleteOrThrowIfReferenced(row.CustomerID);
            MessageBox.Show(FindForm(), "Customer deleted.", "Customers",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReloadGrid();
        }
        catch (BusinessRuleException ex)
        {
            var result = MessageBox.Show(
                FindForm(),
                ex.Message + "\n\nMark this customer Inactive instead?",
                "Cannot Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    Service.Deactivate(row.CustomerID);
                    MessageBox.Show(FindForm(), "Customer marked Inactive.", "Customers",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReloadGrid();
                }
                catch (Exception deactivateEx)
                {
                    MessageBox.Show(FindForm(), deactivateEx.Message, "Customers",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        catch (AppException ex)
        {
            MessageBox.Show(FindForm(), ex.Message, "Customers",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show(FindForm(), "Delete failed: " + ex.Message, "Customers",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public void RefreshList() => ReloadGrid();
}
