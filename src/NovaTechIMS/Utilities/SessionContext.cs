using NovaTechIMS.Models;

namespace NovaTechIMS.Utilities;

/// <summary>
/// In-process session for the single WinForms application instance (FR-AUTH-008).
/// Set after successful login; cleared on logout.
/// </summary>
public static class SessionContext
{
    public static CurrentUser? Current { get; private set; }

    public static bool IsAuthenticated => Current is not null;

    public static void SignIn(CurrentUser user)
    {
        Current = user;
    }

    public static void SignOut()
    {
        Current = null;
    }
}
