using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Categories;

/// <summary>
/// Category create/edit dialog — BCL-only in Designer (no UiTheme) so View Designer is stable.
/// </summary>
partial class CategoryEditForm
{
    private System.ComponentModel.IContainer components = null;

    private Label lblName;
    private TextBox txtName;
    private Label lblDescription;
    private TextBox txtDescription;
    private CheckBox chkActive;
    private Label lblError;
    private Button btnSave;
    private Button btnCancel;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        lblName = new Label();
        txtName = new TextBox();
        lblDescription = new Label();
        txtDescription = new TextBox();
        chkActive = new CheckBox();
        lblError = new Label();
        btnSave = new Button();
        btnCancel = new Button();
        SuspendLayout();

        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(440, 320);
        BackColor = Color.White;
        Name = "CategoryEditForm";
        Text = "Category";

        lblName.AutoSize = true;
        lblName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblName.ForeColor = Color.FromArgb(31, 41, 51);
        lblName.Location = new Point(24, 24);
        lblName.Name = "lblName";
        lblName.Text = "Name *";

        txtName.BorderStyle = BorderStyle.FixedSingle;
        txtName.Location = new Point(24, 48);
        txtName.MaxLength = 100;
        txtName.Name = "txtName";
        txtName.Size = new Size(392, 25);
        txtName.TabIndex = 0;

        lblDescription.AutoSize = true;
        lblDescription.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblDescription.ForeColor = Color.FromArgb(31, 41, 51);
        lblDescription.Location = new Point(24, 88);
        lblDescription.Name = "lblDescription";
        lblDescription.Text = "Description";

        txtDescription.BorderStyle = BorderStyle.FixedSingle;
        txtDescription.Location = new Point(24, 112);
        txtDescription.MaxLength = 500;
        txtDescription.Multiline = true;
        txtDescription.Name = "txtDescription";
        txtDescription.ScrollBars = ScrollBars.Vertical;
        txtDescription.Size = new Size(392, 80);
        txtDescription.TabIndex = 1;

        chkActive.AutoSize = true;
        chkActive.Checked = true;
        chkActive.CheckState = CheckState.Checked;
        chkActive.Font = new Font("Segoe UI", 10F);
        chkActive.ForeColor = Color.FromArgb(31, 41, 51);
        chkActive.Location = new Point(24, 204);
        chkActive.Name = "chkActive";
        chkActive.TabIndex = 2;
        chkActive.Text = "Active";
        chkActive.UseVisualStyleBackColor = true;

        lblError.ForeColor = Color.FromArgb(192, 57, 43);
        lblError.Font = new Font("Segoe UI", 8.5F);
        lblError.Location = new Point(24, 236);
        lblError.Name = "lblError";
        lblError.Size = new Size(392, 36);
        lblError.Visible = false;

        btnSave.BackColor = Color.FromArgb(30, 75, 143);
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(212, 276);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(100, 32);
        btnSave.TabIndex = 3;
        btnSave.Text = "Save";
        btnSave.UseVisualStyleBackColor = false;
        btnSave.Click += BtnSave_Click;

        btnCancel.BackColor = Color.White;
        btnCancel.FlatAppearance.BorderColor = Color.FromArgb(215, 220, 225);
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.Font = new Font("Segoe UI", 10F);
        btnCancel.ForeColor = Color.FromArgb(31, 41, 51);
        btnCancel.Location = new Point(316, 276);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(100, 32);
        btnCancel.TabIndex = 4;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = false;
        btnCancel.Click += BtnCancel_Click;

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

        ResumeLayout(false);
        PerformLayout();
    }
}
