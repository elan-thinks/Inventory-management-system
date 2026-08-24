using System;
using System.Windows.Forms;
using NovaTechIMS.Forms;
using NovaTechIMS.Utilities;

namespace NovaTechIMS;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        try
        {
            Application.Run(new LoginForm());
        }
        catch (Exception ex)
        {
            try
            {
                MessageBox.Show(
                    "Inventory Management System (IMS) failed to start:\n\n" + ex.Message + "\n\n" + ex.GetType().FullName,
                    "IMS — Startup Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch
            {
                // last resort
            }
        }
    }
}
