using System;
using System.Windows.Forms;
using NovaTechIMS.Forms;

namespace NovaTechIMS;

/// <summary>
/// Application entry point.
/// Milestone 9: LoginForm requires real authentication before MainForm.
/// </summary>
internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new LoginForm());
    }
}
