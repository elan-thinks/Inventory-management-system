using System;
using NovaTechIMS.Data;
using NovaTechIMS.Models;
using NovaTechIMS.Models.Enums;
using NovaTechIMS.Security;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Services;

/// <summary>
/// Authentication (FR-AUTH, ADR-004).
/// Milestone 9: login + hashed passwords. Authorization gates arrive in Milestone 10.
/// </summary>
public class AuthService
{
    private readonly UserRepository _users = new();

    /// <summary>
    /// Authenticates username/password against an active user record.
    /// On success builds <see cref="CurrentUser"/> and updates LastLoginDate.
    /// Always returns the same generic failure message (FR-AUTH-003).
    /// </summary>
    public CurrentUser Authenticate(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
            throw new AuthenticationException("Invalid username or password.");

        EnsureBootstrapAdmin();

        var user = _users.GetByUsername(username.Trim());

        // Generic failure for missing user, inactive, or bad password (FR-AUTH-003).
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
            EffectivePermissions = new() // filled in Milestone 10
        };
    }

    /// <summary>
    /// If the User table is empty, creates a default Administrator so first-run login works.
    /// Username: admin  Password: Admin@123
    /// </summary>
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
