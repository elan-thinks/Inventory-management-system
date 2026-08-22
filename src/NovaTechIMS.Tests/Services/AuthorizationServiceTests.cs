using NovaTechIMS.Models;
using NovaTechIMS.Models.Enums;
using NovaTechIMS.Security;
using NovaTechIMS.Services;
using Xunit;

namespace NovaTechIMS.Tests.Services;

public class AuthorizationServiceTests
{
    [Fact]
    public void Administrator_Has_All_Key_Permissions()
    {
        var perms = AuthorizationService.BuildEffectivePermissions(UserRole.Administrator);

        Assert.Contains(Permissions.ManageUsers, perms);
        Assert.Contains(Permissions.ManageDelegations, perms);
        Assert.Contains(Permissions.PerformAdjustment, perms);
        Assert.Contains(Permissions.DeleteProducts, perms);
        Assert.Contains(Permissions.StockIn, perms);
        Assert.Contains(Permissions.ViewReports, perms);
    }

    [Fact]
    public void Staff_Cannot_Manage_Users_Or_Adjust()
    {
        var perms = AuthorizationService.BuildEffectivePermissions(UserRole.InventoryStaff);

        Assert.DoesNotContain(Permissions.ManageUsers, perms);
        Assert.DoesNotContain(Permissions.ManageDelegations, perms);
        Assert.DoesNotContain(Permissions.PerformAdjustment, perms);
        Assert.DoesNotContain(Permissions.DeleteProducts, perms);
        Assert.DoesNotContain(Permissions.ManageProducts, perms);
    }

    [Fact]
    public void Staff_Can_View_Master_Data_And_Stock_Ops()
    {
        var perms = AuthorizationService.BuildEffectivePermissions(UserRole.InventoryStaff);

        Assert.Contains(Permissions.ViewProducts, perms);
        Assert.Contains(Permissions.ViewCategories, perms);
        Assert.Contains(Permissions.StockIn, perms);
        Assert.Contains(Permissions.StockOut, perms);
        Assert.Contains(Permissions.ViewInventoryHistory, perms);
        Assert.Contains(Permissions.ViewReports, perms);
    }

    [Fact]
    public void HasPermission_False_When_User_Null()
    {
        Assert.False(AuthorizationService.HasPermission((CurrentUser?)null, Permissions.StockIn));
    }

    [Fact]
    public void HasPermission_True_When_Listed()
    {
        var user = new CurrentUser
        {
            UserID = 1,
            Role = UserRole.InventoryStaff,
            EffectivePermissions = new() { Permissions.StockIn }
        };

        Assert.True(AuthorizationService.HasPermission(user, Permissions.StockIn));
        Assert.False(AuthorizationService.HasPermission(user, Permissions.PerformAdjustment));
    }
}
