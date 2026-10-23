using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms.Categories;

/// <summary>
/// Category create/edit dialog — soft layout (Designer-safe BCL only).
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
    private Panel actionsBar;
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
        actionsBar = new Panel();
        btnSave = new Button();
        btnCancel = new Button();
        actionsBar.SuspendLayout();
        SuspendLayout();

        AutoScaleMode = AutoScaleMode.Font;
        Font = new Font("Segoe UI", 10F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(460, 360);
        BackColor = Color.White;
        Name = "CategoryEditForm";
        Text = "Category";
        Padding = new Padding(0);

        lblName.AutoSize = true;
        lblName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblName.ForeColor = Color.FromArgb(26, 29, 33);
        lblName.Location = new Point(28, 24);
        lblName.Name = "lblName";
        lblName.Text = "Name *";

        txtName.BorderStyle = BorderStyle.FixedSingle;
        txtName.Location = new Point(28, 48);
        txtName.MaxLength = 100;
        txtName.Name = "txtName";
        txtName.Size = new Size(404, 28);
        txtName.TabIndex = 0;

        lblDescription.AutoSize = true;
        lblDescription.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblDescription.ForeColor = Color.FromArgb(26, 29, 33);
        lblDescription.Location = new Point(28, 92);
        lblDescription.Name = "lblDescription";
        lblDescription.Text = "Description";

        txtDescription.BorderStyle = BorderStyle.FixedSingle;
        txtDescription.Location = new Point(28, 116);
        txtDescription.MaxLength = 500;
        txtDescription.Multiline = true;
        txtDescription.Name = "txtDescription";
        txtDescription.ScrollBars = ScrollBars.Vertical;
        txtDescription.Size = new Size(404, 100);
        txtDescription.TabIndex = 1;

        chkActive.AutoSize = true;
        chkActive.Checked = true;
        chkActive.CheckState = CheckState.Checked;
        chkActive.Font = new Font("Segoe UI", 10F);
        chkActive.ForeColor = Color.FromArgb(26, 29, 33);
        chkActive.Location = new Point(28, 228);
        chkActive.Name = "chkActive";
        chkActive.TabIndex = 2;
        chkActive.Text = "Active";
        chkActive.UseVisualStyleBackColor = true;

        lblError.ForeColor = Color.FromArgb(209, 67, 67);
        lblError.Font = new Font("Segoe UI", 8.5F);
        lblError.Location = new Point(28, 260);
        lblError.Name = "lblError";
        lblError.Size = new Size(404, 32);
        lblError.Visible = false;

        actionsBar.Dock = DockStyle.Bottom;
        actionsBar.Height = 64;
        actionsBar.BackColor = Color.FromArgb(248, 249, 251);
        actionsBar.Name = "actionsBar";

        btnCancel.BackColor = Color.White;
        btnCancel.FlatAppearance.BorderColor = Color.FromArgb(232, 235, 238);
        btnCancel.FlatAppearance.BorderSize = 1;
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnCancel.ForeColor = Color.FromArgb(26, 29, 33);
        btnCancel.Location = new Point(228, 14);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(100, 38);
        btnCancel.TabIndex = 4;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = false;
        btnCancel.Click += BtnCancel_Click;

        btnSave.BackColor = Color.FromArgb(15, 118, 70);
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(336, 14);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(100, 38);
        btnSave.TabIndex = 3;
        btnSave.Text = "Save";
        btnSave.UseVisualStyleBackColor = false;
        btnSave.Click += BtnSave_Click;

        actionsBar.Controls.Add(btnCancel);
        actionsBar.Controls.Add(btnSave);

        AcceptButton = btnSave;
        CancelButton = btnCancel;

        Controls.Add(lblName);
        Controls.Add(txtName);
        Controls.Add(lblDescription);
        Controls.Add(txtDescription);
        Controls.Add(chkActive);
        Controls.Add(lblError);
        Controls.Add(actionsBar);

        actionsBar.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }
}
