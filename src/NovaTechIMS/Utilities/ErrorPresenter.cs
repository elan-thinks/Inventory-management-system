using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace NovaTechIMS.Utilities;

/// <summary>
/// Consistent user-facing error display (Technical Design §11).
/// Never shows stack traces; logs technical detail via Debug.
/// </summary>
public static class ErrorPresenter
{
    public static void Show(IWin32Window? owner, Exception ex, string caption = "NovaTech IMS")
    {
        Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {ex}");

        var (message, icon) = Classify(ex);
        MessageBox.Show(owner, message, caption, MessageBoxButtons.OK, icon);
    }

    public static void Show(IWin32Window? owner, string message, string caption = "NovaTech IMS",
        MessageBoxIcon icon = MessageBoxIcon.Warning)
    {
        MessageBox.Show(owner, message, caption, MessageBoxButtons.OK, icon);
    }

    /// <summary>
    /// Maps an exception to a safe user message (no SQL / stack).
    /// </summary>
    public static (string Message, MessageBoxIcon Icon) Classify(Exception ex)
    {
        return ex switch
        {
            ValidationException v => (v.Message, MessageBoxIcon.Warning),
            InsufficientStockException s => (s.Message, MessageBoxIcon.Warning),
            BusinessRuleException b => (b.Message, MessageBoxIcon.Warning),
            DuplicateRecordException d => (d.Message, MessageBoxIcon.Warning),
            NotFoundException n => (n.Message, MessageBoxIcon.Information),
            AuthenticationException a => (a.Message, MessageBoxIcon.Warning),
            UnauthorizedException or AuthorizationException =>
                ("You are not authorised to perform this action.", MessageBoxIcon.Warning),
            DataAccessException da => (da.Message, MessageBoxIcon.Error),
            AppException app => (app.Message, MessageBoxIcon.Warning),
            _ => (
                "An unexpected error occurred. Please try again or contact your administrator.",
                MessageBoxIcon.Error)
        };
    }

    /// <summary>Returns a friendly message for inline labels (forms).</summary>
    public static string ToUserMessage(Exception ex)
        => Classify(ex).Message;
}
