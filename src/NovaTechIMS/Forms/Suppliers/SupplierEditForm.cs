using System;
using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Suppliers;

/// <summary>
/// SCR-009 Supplier Add/Edit modal (Milestone 6).
/// </summary>
public class SupplierEditForm : Form
{
    private readonly SupplierService _service = new();
    private readonly int? _supplierId;

    private TextBox txtName = null!;
    private TextBox txtContact = null!;
    private TextBox txtPhone = null!;
    private TextBox txtEmail = null!;
    private TextBox txtAddress = null!;
    private CheckBox chkActive = null!;
    private Label lblError = null!;
    private Button btnSave = null!;
    private Button btnCancel = null!;

    public SupplierEditForm(int? supplierId = null)
    {
        _supplierId = supplierId;
        BuildUi();

        if (_supplierId is int id)
        {
            Text = "Edit Supplier";
            LoadExisting(id);
        }
        else
        {
            Text = "Add Supplier";
            chkActive.Checked = true;
            chkActive.Visible = false;
            chkActive.Enabled = false;
        }
    }

    private void BuildUi()
    {
        AutoScaleMode = AutoScaleMode.Font;
        Font = UiTheme.Body;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(460, 420);
        BackColor = UiTheme.Surface;

        int y = 20;
        const int labelX = 24;
        const int fieldX = 24;
        const int fieldW = 412;

        void AddLabel(string text)
        {
            Controls.Add(new Label
            {
                Text = text,
                Font = UiTheme.Label,
                ForeColor = UiTheme.Text,
                Location = new Point(labelX, y),
                AutoSize = true
            });
            y += 20;
        }

        TextBox AddTextBox(int maxLen, bool multiline = false)
        {
            var tb = new TextBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(fieldX, y),
                Width = fieldW,
                MaxLength = maxLen,
                Font = UiTheme.Body
            };
            if (multiline)
            {
                tb.Multiline = true;
                tb.Height = 56;
                tb.ScrollBars = ScrollBars.Vertical;
                y += 64;
            }
            else
            {
                y += 32;
            }
            Controls.Add(tb);
            return tb;
        }

        AddLabel("Name *");
        txtName = AddTextBox(150);

        AddLabel("Contact person");
        txtContact = AddTextBox(100);

        AddLabel("Phone");
        txtPhone = AddTextBox(30);

        AddLabel("Email");
        txtEmail = AddTextBox(100);

        AddLabel("Address");
        txtAddress = AddTextBox(300, multiline: true);

        chkActive = new CheckBox
        {
            Text = "Active",
            Font = UiTheme.Body,
            ForeColor = UiTheme.Text,
            Location = new Point(fieldX, y),
            AutoSize = true,
            Checked = true
        };
        Controls.Add(chkActive);
        y += 28;

        lblError = new Label
        {
            ForeColor = UiTheme.Error,
            Font = UiTheme.Label,
            Location = new Point(fieldX, y),
            Size = new Size(fieldW, 36),
            Visible = false
        };
        Controls.Add(lblError);
        y += 40;

        btnSave = UiTheme.StyleButton(new Button
        {
            Text = "Save",
            Size = new Size(100, 32),
            Location = new Point(232, y)
        }, UiTheme.ButtonKind.Primary);
        btnSave.Click += BtnSave_Click;
btnSave.Click += BtnSave_Click;

        btnCancel = UiTheme.StyleButton(new Button
        {
            Text = "Cancel",
            Size = new Size(100, 32),
            Location = new Point(336, y)
        }, UiTheme.ButtonKind.Secondary);
        btnCancel.Click += (_, _) => DialogResult = DialogResult.Cancel;

        AcceptButton = btnSave;
        CancelButton = btnCancel;
        Controls.Add(btnSave);
        Controls.Add(btnCancel);

        ClientSize = new Size(460, y + 52);
    }

    private void LoadExisting(int id)
    {
        try
        {
            var s = _service.GetById(id);
            txtName.Text = s.SupplierName;
            txtContact.Text = s.ContactPerson ?? string.Empty;
            txtPhone.Text = s.Phone ?? string.Empty;
            txtEmail.Text = s.Email ?? string.Empty;
            txtAddress.Text = s.Address ?? string.Empty;
            chkActive.Checked = s.IsActive;
            chkActive.Visible = true;
            chkActive.Enabled = true;
        }
        catch (AppException ex)
        {
            MessageBox.Show(this, ex.Message, "Supplier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            DialogResult = DialogResult.Cancel;
        }
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        lblError.Visible = false;
        btnSave.Enabled = false;

        try
        {
            if (_supplierId is int id)
            {
                _service.Update(
                    id,
                    txtName.Text,
                    txtContact.Text,
                    txtPhone.Text,
                    txtEmail.Text,
                    txtAddress.Text,
                    chkActive.Checked);
            }
            else
            {
                _service.Create(
                    txtName.Text,
                    txtContact.Text,
                    txtPhone.Text,
                    txtEmail.Text,
                    txtAddress.Text);
            }

            DialogResult = DialogResult.OK;
        }
        catch (ValidationException ex)
        {
            ShowError(ex.Message);
            txtName.Focus();
        }
        catch (AppException ex)
        {
            ShowError(ex.Message);
        }
        catch (Exception ex)
        {
            ShowError("Could not save supplier. " + ex.Message);
        }
        finally
        {
            btnSave.Enabled = true;
        }
    }

    private void ShowError(string message)
    {
        lblError.Text = message;
        lblError.Visible = true;
    }
}
