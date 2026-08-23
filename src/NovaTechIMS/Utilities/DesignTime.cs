using System.ComponentModel;
using NovaTechIMS.Models;
using NovaTechIMS.Models.Enums;

namespace NovaTechIMS.Utilities;

/// <summary>
/// Detect Visual Studio / design-time host so forms can call InitializeComponent
/// without constructing services, hitting PostgreSQL, or requiring runtime args.
/// </summary>
public static class DesignTime
{
    public static bool IsActive =>
        LicenseManager.UsageMode == LicenseUsageMode.Designtime;

    /// <summary>Dummy user so MainForm can be opened in the designer.</summary>
    public static CurrentUser PlaceholderUser { get; } = new()
    {
        UserID = 0,
        Username = "designer",
        FullName = "Design Time",
        Role = UserRole.Administrator,
        EffectivePermissions = new()
    };
}
