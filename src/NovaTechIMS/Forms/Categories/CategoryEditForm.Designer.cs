using System.Drawing;
using System.Windows.Forms;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Forms.Categories;

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
        Font = UiTheme.Body;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(440, 320);
        BackColor = UiTheme.Surface;
        Name = "CategoryEditForm";
        Text = "Category";

        lblName.Text = "Name *";
        lblName.Font = UiTheme.LabelSemibold;
        lblName.ForeColor = UiTheme.Text;
        lblName.Location = new Point(24, 24);
        lblName.AutoSize = true;
        lblName.Name = "lblName";

        txtName.Location = new Point(24, 48);
        txtName.Width = 392;
        txtName.MaxLength = 100;
        txtName.Name = "txtName";
        txtName.TabIndex = 0;

        lblDescription.Text = "Description";
        lblDescription.Font = UiTheme.LabelSemibold;
        lblDescription.ForeColor = UiTheme.Text;
        lblDescription.Location = new Point(24, 88);
        lblDescription.AutoSize = true;
        lblDescription.Name = "lblDescription";

        txtDescription.Location = new Point(24, 112);
        txtDescription.Size = new Size(392, 80);
        txtDescription.Multiline = true;
        txtDescription.MaxLength = 500;
        txtDescription.ScrollBars = ScrollBars.Vertical;
        txtDescription.Name = "txtDescription";
        txtDescription.TabIndex = 1;

        chkActive.Text = "Active";
        chkActive.Font = UiTheme.Body;
        chkActive.ForeColor = UiTheme.Text;
        chkActive.Location = new Point(24, 204);
        chkActive.AutoSize = true;
        chkActive.Checked = true;
        chkActive.Name = "chkActive";
        chkActive.TabIndex = 2;

        lblError.ForeColor = UiTheme.Error;
        lblError.Font = UiTheme.ErrorText;
        lblError.Location = new Point(24, 236);
        lblError.Size = new Size(392, 36);
        lblError.Visible = false;
        lblError.Name = "lblError";

        btnSave.Text = "Save";
        btnSave.Size = new Size(100, 32);
        btnSave.Location = new Point(212, 276);
        btnSave.Name = "btnSave";
        btnSave.TabIndex = 3;
        btnSave.Click += BtnSave_Click;

        btnCancel.Text = "Cancel";
        btnCancel.Size = new Size(100, 32);
        btnCancel.Location = new Point(316, 276);
        btnCancel.Name = "btnCancel";
        btnCancel.TabIndex = 4;
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
