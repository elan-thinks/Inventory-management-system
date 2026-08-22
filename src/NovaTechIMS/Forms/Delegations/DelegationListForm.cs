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

        cboStatus = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Width = 130,
            Location = new Point(0, 8),
            Font = UiTheme.Body
        };
        cboStatus.Items.AddRange(new object[] { "All statuses", "Active", "Expired", "Revoked" });
        cboStatus.SelectedIndex = 0;
        cboStatus.SelectedIndexChanged += (_, _) => ReloadGrid();

        cboResponsibility = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Width = 140,
            Location = new Point(140, 8),
            Font = UiTheme.Body
        };
        cboResponsibility.Items.AddRange(new object[] { "All responsibilities", "Stock-In", "Stock-Out", "Report Access" });
        cboResponsibility.SelectedIndex = 0;
        cboResponsibility.SelectedIndexChanged += (_, _) => ReloadGrid();

        btnRefresh = new Button
        {
            Text = "Refresh",
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Size = new Size(88, 30),
            FlatStyle = FlatStyle.Flat,
            Font = UiTheme.Label,
            BackColor = UiTheme.Surface
        };
        btnRefresh.FlatAppearance.BorderColor = UiTheme.Border;
        btnRefresh.Click += (_, _) => ReloadGrid();

        btnNew = new Button
        {
            Text = "+ New Delegation",
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Size = new Size(140, 30),
            FlatStyle = FlatStyle.Flat,
            Font = UiTheme.Button,
            BackColor = UiTheme.Primary,
            ForeColor = Color.White,
            Cursor = Cursors.Hand
        };
        btnNew.FlatAppearance.BorderSize = 0;
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
        grid.CellContentClick += Grid_CellContentClick;

        lblEmpty = new Label
        {
            Text = "No delegations found.",
            Font = UiTheme.Body,
            ForeColor = UiTheme.TextMuted,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            Visible = false
        };

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
                FlatStyle = FlatStyle.Flat
            });

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

    private void Grid_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        if (grid.Columns[e.ColumnIndex].Name != "colRevoke") return;
        if (grid.Rows[e.RowIndex].DataBoundItem is not DelegationListRow row) return;

        if (row.Status != DelegationStatus.Active)
        {
            MessageBox.Show(FindForm(), "Only active delegations can be revoked.", "Delegations",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var confirm = MessageBox.Show(
            FindForm(),
            $"Revoke delegation of {row.ResponsibilityLabel} to {row.DelegatedToName}?",
            "Revoke Delegation",
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
