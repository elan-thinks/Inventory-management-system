using System;
using System.Collections.Generic;
using System.Linq;
using NovaTechIMS.Models;
using NovaTechIMS.Models.Enums;
using NovaTechIMS.Security;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Services;

/// <summary>
/// Role-based authorization (ADR-005/006). Effective = role U valid delegations.
/// </summary>
public class AuthorizationService
{
    public static bool HasPermission(CurrentUser? user, string permission)
    {
        if (user is null || string.IsNullOrWhiteSpace(permission))
            return false;

        return user.EffectivePermissions.Contains(permission, StringComparer.Ordinal);
    }

    public static void RequirePermission(CurrentUser? user, string permission)
    {
        if (!HasPermission(user, permission))
        {
            throw new UnauthorizedException(
                "You do not have permission to perform this action.");
        }
    }

    public static void RequirePermission(string permission)
        => RequirePermission(SessionContext.Current, permission);

    public static bool HasPermission(string permission)
        => HasPermission(SessionContext.Current, permission);

    /// <summary>
    /// Permanent role permissions only (no delegations).
    /// </summary>
    public static List<string> BuildEffectivePermissions(UserRole role)
    {
        return role switch
        {
            UserRole.Administrator => AdministratorPermissions().ToList(),
            UserRole.InventoryStaff => StaffPermissions().ToList(),
            _ => StaffPermissions().ToList()
        };
    }

    /// <summary>
    /// Role permissions union currently valid delegations (Milestone 15).
    /// </summary>
    public static List<string> BuildEffectivePermissions(UserRole role, int userId)
    {
        var set = new HashSet<string>(BuildEffectivePermissions(role), StringComparer.Ordinal);

        if (role != UserRole.Administrator)
        {
            foreach (var key in DelegationService.GetDelegatedPermissionKeys(userId))
                set.Add(key);
        }

        return set.ToList();
    }

    private static IEnumerable<string> AdministratorPermissions()
    {
        yield return Permissions.ViewDashboard;
        yield return Permissions.ViewProducts;
        yield return Permissions.ManageProducts;
        yield return Permissions.DeleteProducts;
        yield return Permissions.ViewCategories;
        yield return Permissions.ManageCategories;
        yield return Permissions.DeleteCategories;
        yield return Permissions.ViewSuppliers;
        yield return Permissions.ManageSuppliers;
        yield return Permissions.DeleteSuppliers;
        yield return Permissions.ViewCustomers;
        yield return Permissions.ManageCustomers;
        yield return Permissions.DeleteCustomers;
        yield return Permissions.StockIn;
        yield return Permissions.StockOut;
        yield return Permissions.ViewInventoryHistory;
        yield return Permissions.PerformAdjustment;
        yield return Permissions.ViewReports;
        yield return Permissions.ManageUsers;
        yield return Permissions.ManageDelegations;
    }

    /// <summary>
    /// Staff base: view master data + history + dashboard.
    /// Stock-In / Stock-Out / Reports may also come from permanent role (SRS) and/or delegation.
    /// </summary>
    private static IEnumerable<string> StaffPermissions()
    {
        yield return Permissions.ViewDashboard;
        yield return Permissions.ViewProducts;
        yield return Permissions.ViewCategories;
        yield return Permissions.ViewSuppliers;
        yield return Permissions.ViewCustomers;
        yield return Permissions.StockIn;
        yield return Permissions.StockOut;
        yield return Permissions.ViewInventoryHistory;
        yield return Permissions.ViewReports;
    }
}
