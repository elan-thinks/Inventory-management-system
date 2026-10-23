using System;
using System.Windows.Forms;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Suppliers;

/// <summary>SCR-009 Supplier Add/Edit modal — soft dialog chrome.</summary>
public partial class SupplierEditForm : Form
{
    private readonly SupplierService _service = new();
    private readonly int? _supplierId;

    public SupplierEditForm(int? supplierId = null)
    {
        _supplierId = supplierId;
        InitializeComponent();

        if (!DesignTime.IsActive)
            ApplyRuntimeStyling();

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

    private void ApplyRuntimeStyling() => DialogChrome.Apply(this);

    private void BtnCancel_Click(object? sender, EventArgs e) => DialogResult = DialogResult.Cancel;

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
            AppDialog.Warning(this, ex.Message, "Supplier");
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
