using System;
using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Utilities;

/// <summary>
/// Soft iOS-style message and confirm dialogs (replaces harsh MessageBox chrome).
/// </summary>
internal static class AppDialog
{
    public static void Info(IWin32Window? owner, string message, string title = "IMS")
        => Show(owner, message, title, MessageBoxIcon.Information, MessageBoxButtons.OK);

    public static void Warning(IWin32Window? owner, string message, string title = "IMS")
        => Show(owner, message, title, MessageBoxIcon.Warning, MessageBoxButtons.OK);

    public static void Error(IWin32Window? owner, string message, string title = "IMS")
        => Show(owner, message, title, MessageBoxIcon.Error, MessageBoxButtons.OK);

    public static bool Confirm(IWin32Window? owner, string message, string title = "Confirm")
        => Show(owner, message, title, MessageBoxIcon.Question, MessageBoxButtons.OKCancel) == DialogResult.OK;

    public static DialogResult Show(
        IWin32Window? owner,
        string message,
        string title,
        MessageBoxIcon icon,
        MessageBoxButtons buttons)
    {
        using var dlg = new SoftMessageForm(message, title, icon, buttons);
        return owner is null ? dlg.ShowDialog() : dlg.ShowDialog(owner);
    }

    private sealed class SoftMessageForm : Form
    {
        public SoftMessageForm(string message, string title, MessageBoxIcon icon, MessageBoxButtons buttons)
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Font = UiTheme.Body;
            BackColor = UiTheme.Surface;
            Text = title;
            AutoScaleMode = AutoScaleMode.Font;
            Padding = new Padding(0);

            var accent = icon switch
            {
                MessageBoxIcon.Error => UiTheme.Error,
                MessageBoxIcon.Warning => UiTheme.Warning,
                MessageBoxIcon.Question => UiTheme.Primary,
                _ => UiTheme.Primary
            };

            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 6,
                BackColor = accent
            };

            var lblTitle = new Label
            {
                Text = title,
                Font = UiTheme.SectionTitle,
                ForeColor = UiTheme.Text,
                AutoSize = true,
                Location = new Point(24, 24)
            };

            var lblBody = new Label
            {
                Text = message,
                Font = UiTheme.Body,
                ForeColor = UiTheme.TextSecondary,
                AutoSize = false,
                Location = new Point(24, 56),
                MaximumSize = new Size(400, 0),
                Size = new Size(400, 80)
            };

            // Measure body height
            using (var g = CreateGraphics())
            {
                var sz = g.MeasureString(message, UiTheme.Body, 400);
                lblBody.Height = Math.Max(48, (int)Math.Ceiling(sz.Height) + 8);
            }

            var actions = new Panel
            {
                Height = 64,
                Dock = DockStyle.Bottom,
                BackColor = UiTheme.Surface,
                Padding = new Padding(24, 12, 24, 16)
            };

            Button? btnOk = null;
            Button? btnCancel = null;

            if (buttons is MessageBoxButtons.OK or MessageBoxButtons.OKCancel)
            {
                btnOk = UiTheme.CreateButton(
                    buttons == MessageBoxButtons.OKCancel ? "Confirm" : "OK",
                    UiTheme.ButtonKind.Primary,
                    110,
                    UiTheme.ButtonHeight);
                btnOk.DialogResult = DialogResult.OK;
                btnOk.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            }

            if (buttons == MessageBoxButtons.OKCancel)
            {
                btnCancel = UiTheme.CreateButton("Cancel", UiTheme.ButtonKind.Secondary, 100, UiTheme.ButtonHeight);
                btnCancel.DialogResult = DialogResult.Cancel;
                btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            }

            actions.Resize += (_, _) =>
            {
                if (btnOk is null) return;
                btnOk.Left = actions.ClientSize.Width - btnOk.Width - 24;
                btnOk.Top = 12;
                if (btnCancel is not null)
                {
                    btnCancel.Left = btnOk.Left - btnCancel.Width - 10;
                    btnCancel.Top = 12;
                }
            };

            if (btnCancel is not null) actions.Controls.Add(btnCancel);
            if (btnOk is not null) actions.Controls.Add(btnOk);

            ClientSize = new Size(448, 24 + 32 + lblBody.Height + 16 + 64 + 6);

            Controls.Add(lblBody);
            Controls.Add(lblTitle);
            Controls.Add(actions);
            Controls.Add(header);

            if (btnOk is not null) AcceptButton = btnOk;
            if (btnCancel is not null) CancelButton = btnCancel;
            else if (btnOk is not null) CancelButton = btnOk;

            // Force layout once
            actions.PerformLayout();
            if (btnOk is not null)
            {
                btnOk.Left = ClientSize.Width - btnOk.Width - 24;
                btnOk.Top = 12;
                if (btnCancel is not null)
                {
                    btnCancel.Left = btnOk.Left - btnCancel.Width - 10;
                    btnCancel.Top = 12;
                }
            }
        }
    }
}
