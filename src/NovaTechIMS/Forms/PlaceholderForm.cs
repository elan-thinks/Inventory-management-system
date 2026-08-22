using System;
using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Forms;

/// <summary>
/// Temporary form used only for Milestone 0 verification.
/// Confirms the WinForms project builds, starts, and displays a window.
/// This form will be removed or replaced when Milestone 1 (application shell) begins.
/// </summary>
public class PlaceholderForm : Form
{
    public PlaceholderForm()
    {
        Text = "NovaTech IMS — Milestone 0";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(520, 280);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = true;

        var title = new Label
        {
            Text = "NovaTech Electronics\nInventory Management System",
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
            Text = "Technical Design v1.0  |  Branch: version1.2",
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
