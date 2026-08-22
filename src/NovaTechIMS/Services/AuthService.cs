using System;
using NovaTechIMS.Data;
using NovaTechIMS.Models;
using NovaTechIMS.Models.Enums;
using NovaTechIMS.Security;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Services;

/// <summary>
/// Authentication + effective permissions including valid delegations (M15).
/// </summary>
public class AuthService
{
    private readonly UserRepository _users = new();

    public CurrentUser Authenticate(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
            throw new AuthenticationException("Invalid username or password.");

        EnsureBootstrapAdmin();

        var user = _users.GetByUsername(username.Trim());

        if (user is null || !user.IsActive)
            throw new AuthenticationException("Invalid username or password.");

        if (!PasswordHasher.Verify(password, user.PasswordHash, user.PasswordSalt))
            throw new AuthenticationException("Invalid username or password.");

        var now = DateTime.UtcNow;
        _users.UpdateLastLogin(user.UserID, now);

        return new CurrentUser
        {
            UserID = user.UserID,
            Username = user.Username,
            FullName = user.FullName,
            Role = user.Role,
            EffectivePermissions = AuthorizationService.BuildEffectivePermissions(user.Role, user.UserID)
        };
    }

    public void EnsureBootstrapAdmin()
    {
        if (_users.CountUsers() > 0)
            return;

        var (hash, salt) = PasswordHasher.HashPassword("Admin@123");
        var admin = new User
        {
            Username = "admin",
            PasswordHash = hash,
            PasswordSalt = salt,
            FullName = "System Administrator",
            Role = UserRole.Administrator,
            IsActive = true,
            CreatedDate = DateTime.UtcNow
        };
        _users.Insert(admin);
    }
}
