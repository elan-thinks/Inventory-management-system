using System;
using System.Windows.Forms;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Customers;

/// <summary>SCR-011 Customer Add/Edit modal — soft dialog chrome.</summary>
public partial class CustomerEditForm : Form
{
    private readonly CustomerService _service = new();
    private readonly int? _customerId;

    public CustomerEditForm(int? customerId = null)
    {
        _customerId = customerId;
        InitializeComponent();

        if (!DesignTime.IsActive)
            ApplyRuntimeStyling();

        if (_customerId is int id)
        {
            Text = "Edit Customer";
            LoadExisting(id);
        }
        else
        {
            Text = "Add Customer";
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
            var c = _service.GetById(id);
            txtName.Text = c.CustomerName;
            txtPhone.Text = c.Phone ?? string.Empty;
            txtEmail.Text = c.Email ?? string.Empty;
            txtAddress.Text = c.Address ?? string.Empty;
            chkActive.Checked = c.IsActive;
            chkActive.Visible = true;
            chkActive.Enabled = true;
        }
        catch (AppException ex)
        {
            AppDialog.Warning(this, ex.Message, "Customer");
            DialogResult = DialogResult.Cancel;
        }
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        lblError.Visible = false;
        btnSave.Enabled = false;

        try
        {
            if (_customerId is int id)
            {
                _service.Update(
                    id,
                    txtName.Text,
                    txtPhone.Text,
                    txtEmail.Text,
                    txtAddress.Text,
                    chkActive.Checked);
            }
            else
            {
                _service.Create(
                    txtName.Text,
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
            ShowError("Could not save customer. " + ex.Message);
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
