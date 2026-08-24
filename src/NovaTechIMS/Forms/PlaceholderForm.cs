using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms;

/// <summary>
/// Temporary form used only for Milestone 0 verification.
/// Confirms the WinForms project builds, starts, and displays a window.
/// </summary>
public class PlaceholderForm : Form
{
    public PlaceholderForm()
    {
        Text = "Inventory Management System (IMS)";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(520, 280);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = true;

        var title = new Label
        {
            Text = "Inventory Management System\n(IMS)",
            Font = new Font("Segoe UI", 14F, FontStyle.Bold),
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Top,
            Height = 80
        };

        var status = new Label
        {
            Text = "Milestone 0 — Environment & Solution Setup\n\n" +
                   "Solution opens  ✓\n" +
                   "Project builds  ✓\n" +
                   "WinForms starts ✓\n\n" +
                   "No business logic yet. Ready for Milestone 1.",
            Font = new Font("Segoe UI", 10F),
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Fill
        };

        var footer = new Label
        {
            Text = "Inventory Management System (IMS)  |  Branch: version1.3",
            Font = new Font("Segoe UI", 8F),
            ForeColor = Color.Gray,
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Bottom,
            Height = 28
        };

        Controls.Add(status);
        Controls.Add(footer);
        Controls.Add(title);
    }
}
