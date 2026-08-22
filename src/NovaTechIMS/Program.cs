using System;
using System.Windows.Forms;
using NovaTechIMS.Forms;

namespace NovaTechIMS;

/// <summary>
/// Application entry point.
/// Milestone 1: LoginForm → MainForm shell (no authentication or database).
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
