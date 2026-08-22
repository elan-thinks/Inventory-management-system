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

        // UI-thread exceptions
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += OnUiThreadException;

        // Non-UI thread exceptions
        AppDomain.CurrentDomain.UnhandledException += OnDomainUnhandledException;

        Application.Run(new LoginForm());
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
                // last resort — ignore
            }
        }
    }
}
