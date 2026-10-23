using System.Drawing;
using System.Windows.Forms;

namespace NovaTechIMS.Utilities;

/// <summary>
/// Shared chrome for small edit/create popup dialogs — soft, consistent, iOS-like.
/// Call from runtime (not Designer) so View Designer stays BCL-only.
/// </summary>
internal static class DialogChrome
{
    public static void Apply(Form dialog)
    {
        dialog.BackColor = UiTheme.Surface;
        dialog.Font = UiTheme.Body;
        dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
        dialog.MaximizeBox = false;
        dialog.MinimizeBox = false;
        dialog.ShowInTaskbar = false;
        dialog.StartPosition = FormStartPosition.CenterParent;

        StyleRecursive(dialog);
    }

    private static void StyleRecursive(Control root)
    {
        foreach (Control c in root.Controls)
        {
            switch (c)
            {
                case Label lbl when lbl.Name.StartsWith("lbl") && lbl.Name != "lblError" && !lbl.Name.Contains("Hint") && !lbl.Name.Contains("Qty"):
                    if (lbl.Font.Bold || lbl.Text.Contains('*') || lbl.Text.Length < 40)
                    {
                        lbl.ForeColor = UiTheme.Text;
                        if (!lbl.Font.Bold)
                            lbl.Font = UiTheme.LabelSemibold;
                    }
                    else
                    {
                        lbl.ForeColor = UiTheme.TextMuted;
                    }
                    break;

                case Label err when err.Name == "lblError":
                    err.ForeColor = UiTheme.Error;
                    err.Font = UiTheme.ErrorText;
                    break;

                case TextBox tb:
                    UiTheme.StyleTextBox(tb);
                    break;

                case ComboBox cb:
                    UiTheme.StyleComboBox(cb);
                    cb.FlatStyle = FlatStyle.Standard;
                    break;

                case CheckBox chk:
                    chk.ForeColor = UiTheme.Text;
                    chk.Font = UiTheme.Body;
                    break;

                case Button btn when btn.Name is "btnSave" or "btnOk" or "btnConfirm":
                    UiTheme.StyleButton(btn, UiTheme.ButtonKind.Primary);
                    btn.Height = UiTheme.ButtonHeight;
                    break;

                case Button btn when btn.Name is "btnCancel" or "btnClose":
                    UiTheme.StyleButton(btn, UiTheme.ButtonKind.Secondary);
                    btn.Height = UiTheme.ButtonHeight;
                    break;
            }

            if (c.HasChildren)
                StyleRecursive(c);
        }
    }
}
