using System;
using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Models;
using NovaTechIMS.Services;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Categories;

/// <summary>
/// SCR-007 Category Add/Edit modal (Milestone 5).
/// </summary>
public class CategoryEditForm : Form
{
    private readonly CategoryService _service = new();
    private readonly int? _categoryId;

    private TextBox txtName = null!;
    private TextBox txtDescription = null!;
    private CheckBox chkActive = null!;
    private Label lblError = null!;
    private Button btnSave = null!;
    private Button btnCancel = null!;

    public CategoryEditForm(int? categoryId = null)
    {
        _categoryId = categoryId;
        BuildUi();

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

    private void BuildUi()
    {
        AutoScaleMode = AutoScaleMode.Font;
        Font = UiTheme.Body;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(440, 320);
        BackColor = UiTheme.Surface;

        var lblName = new Label
        {
            Text = "Name *",
            Font = UiTheme.Label,
            ForeColor = UiTheme.Text,
            Location = new Point(24, 24),
            AutoSize = true
        };

        txtName = new TextBox
        {
            Location = new Point(24, 48),
            Width = 392,
            MaxLength = 100,
            Font = UiTheme.Body
        };

        var lblDescription = new Label
        {
            Text = "Description",
            Font = UiTheme.Label,
            ForeColor = UiTheme.Text,
            Location = new Point(24, 88),
            AutoSize = true
        };

        txtDescription = new TextBox
        {
            Location = new Point(24, 112),
            Width = 392,
            Height = 80,
            Multiline = true,
            MaxLength = 500,
            ScrollBars = ScrollBars.Vertical,
            Font = UiTheme.Body
        };

        chkActive = new CheckBox
        {
            Text = "Active",
            Font = UiTheme.Body,
            ForeColor = UiTheme.Text,
            Location = new Point(24, 204),
            AutoSize = true,
            Checked = true
        };

        lblError = new Label
        {
            ForeColor = UiTheme.Error,
            Font = UiTheme.Label,
            Location = new Point(24, 236),
            Size = new Size(392, 36),
            Visible = false
        };

        btnSave = new Button
        {
            Text = "Save",
            Font = UiTheme.Button,
            BackColor = UiTheme.Primary,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Size = new Size(100, 32),
            Location = new Point(212, 276),
            Cursor = Cursors.Hand
        };
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.Click += BtnSave_Click;

        btnCancel = new Button
        {
            Text = "Cancel",
            Font = UiTheme.Body,
            BackColor = UiTheme.Surface,
            ForeColor = UiTheme.Text,
            FlatStyle = FlatStyle.Flat,
            Size = new Size(100, 32),
            Location = new Point(316, 276),
            Cursor = Cursors.Hand
        };
        btnCancel.FlatAppearance.BorderColor = UiTheme.Border;
        btnCancel.Click += (_, _) => DialogResult = DialogResult.Cancel;

        AcceptButton = btnSave;
        CancelButton = btnCancel;

        Controls.Add(lblName);
        Controls.Add(txtName);
        Controls.Add(lblDescription);
        Controls.Add(txtDescription);
        Controls.Add(chkActive);
        Controls.Add(lblError);
        Controls.Add(btnSave);
        Controls.Add(btnCancel);
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
            MessageBox.Show(this, ex.Message, "Category", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            {
                _service.Update(id, txtName.Text, txtDescription.Text, chkActive.Checked);
            }
            else
            {
                _service.Create(txtName.Text, txtDescription.Text);
            }

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
