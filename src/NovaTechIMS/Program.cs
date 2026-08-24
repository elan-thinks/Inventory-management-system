using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using NovaTechIMS.Forms;
using NovaTechIMS.Utilities;

namespace NovaTechIMS;

/// <summary>
/// Application entry point with global exception handlers (Milestone 17).
/// </summary>
internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += OnUiThreadException;
        AppDomain.CurrentDomain.UnhandledException += OnDomainUnhandledException;

        try
        {
            Application.Run(new LoginForm());
        }
        catch (Exception ex)
        {
            // Constructors / first paint can fail before the message loop — always show something.
            Debug.WriteLine("[Startup] " + ex);
            try
            {
                MessageBox.Show(
                    "NovaTech IMS failed to start:\n\n" + ex.Message + "\n\n" + ex.GetType().FullName,
                    "Startup error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch
            {
                // ignore
            }
        }
    }

    private static void OnUiThreadException(object sender, ThreadExceptionEventArgs e)
    {
        Debug.WriteLine("[UI] Unhandled: " + e.Exception);
        ErrorPresenter.Show(null, e.Exception, "Unexpected error");
    }

    private static void OnDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is Exception ex)
        {
            Debug.WriteLine("[Domain] Unhandled: " + ex);
            try
            {
                ErrorPresenter.Show(null, ex, "Unexpected error");
            }
            catch
            {
                // last resort
            }
        }
    }
}
