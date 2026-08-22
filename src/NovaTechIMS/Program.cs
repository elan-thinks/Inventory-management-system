using System;
using System.Windows.Forms;

namespace NovaTechIMS;

/// <summary>
/// Application entry point.
/// Milestone 0: launches a minimal placeholder form to prove the project builds and runs.
/// Later milestones replace this with LoginForm (see technical-design/15-Implementation-Milestones.md).
/// </summary>
internal static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Milestone 0 only: show a simple verification form.
        // Milestone 1 will introduce LoginForm → MainForm shell.
        Application.Run(new Forms.PlaceholderForm());
    }
}
