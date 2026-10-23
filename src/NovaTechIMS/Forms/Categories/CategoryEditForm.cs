using System;
using System.Windows.Forms;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Categories;

/// <summary>
/// SCR-007 Category Add/Edit modal — soft dialog chrome.
/// </summary>
public partial class CategoryEditForm : Form
{
    private readonly CategoryService _service = new();
    private readonly int? _categoryId;

    public CategoryEditForm(int? categoryId = null)
    {
        _categoryId = categoryId;
        InitializeComponent();

        if (!DesignTime.IsActive)
            ApplyRuntimeStyling();

        if (_categoryId is int id)
        {
            Text = "Edit Category";
            LoadExisting(id);
        }
        else
        {
            Text = "Add Category";
            chkActive.Checked = true;
            chkActive.Enabled = false;
            chkActive.Visible = false;
        }
    }

    private void ApplyRuntimeStyling()
    {
        DialogChrome.Apply(this);
        actionsBar.BackColor = UiTheme.Background;
    }

    private void BtnCancel_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void LoadExisting(int id)
    {
        try
        {
            var cat = _service.GetById(id);
            txtName.Text = cat.CategoryName;
            txtDescription.Text = cat.Description ?? string.Empty;
            chkActive.Checked = cat.IsActive;
            chkActive.Visible = true;
            chkActive.Enabled = true;
        }
        catch (AppException ex)
        {
            AppDialog.Warning(this, ex.Message, "Category");
            DialogResult = DialogResult.Cancel;
        }
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        lblError.Visible = false;
        btnSave.Enabled = false;

        try
        {
            if (_categoryId is int id)
                _service.Update(id, txtName.Text, txtDescription.Text, chkActive.Checked);
            else
                _service.Create(txtName.Text, txtDescription.Text);

            DialogResult = DialogResult.OK;
        }
        catch (ValidationException ex)
        {
            ShowError(ex.Message);
            txtName.Focus();
        }
        catch (DuplicateRecordException ex)
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
            ShowError("Could not save category. " + ex.Message);
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
