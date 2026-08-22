using System;
using System.Collections.Generic;
using NovaTechIMS.Data;
using NovaTechIMS.Models;
using NovaTechIMS.Models.Enums;
using NovaTechIMS.Security;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Services;

/// <summary>
/// User management (FR-USR). Administrator only (BR-014, Permissions.ManageUsers).
/// </summary>
public class UserService
{
    private readonly UserRepository _repository = new();

    public IReadOnlyList<UserListRow> GetList(string? search = null, bool? isActiveFilter = null)
    {
        AuthorizationService.RequirePermission(Permissions.ManageUsers);
        return _repository.GetList(search, isActiveFilter);
    }

    public User GetById(int userId)
    {
        AuthorizationService.RequirePermission(Permissions.ManageUsers);
        return _repository.GetById(userId)
            ?? throw new NotFoundException("The selected user no longer exists.");
    }

    public User Create(string username, string password, string fullName, UserRole role)
    {
        AuthorizationService.RequirePermission(Permissions.ManageUsers);
        ValidateUsername(username);
        ValidatePassword(password);
        ValidateFullName(fullName);

        if (_repository.GetByUsername(username.Trim()) is not null)
            throw new DuplicateRecordException("A user with this username already exists.");

        var (hash, salt) = PasswordHasher.HashPassword(password);
        var user = new User
        {
            Username = username.Trim(),
            PasswordHash = hash,
            PasswordSalt = salt,
            FullName = fullName.Trim(),
            Role = role,
            IsActive = true,
            CreatedDate = DateTime.UtcNow
        };

        user.UserID = _repository.Insert(user);
        return user;
    }

    public void Update(int userId, string fullName, UserRole role, bool isActive, string? newPassword)
    {
        AuthorizationService.RequirePermission(Permissions.ManageUsers);

        var existing = GetById(userId);
        var current = SessionContext.Current;

        // BR-020: cannot deactivate own account
        if (current is not null && current.UserID == userId && !isActive)
            throw new BusinessRuleException("You cannot deactivate your own currently logged-in account.");

        ValidateFullName(fullName);

        if (!string.IsNullOrEmpty(newPassword))
            ValidatePassword(newPassword);

        existing.FullName = fullName.Trim();
        existing.Role = role;
        existing.IsActive = isActive;

        if (!string.IsNullOrEmpty(newPassword))
        {
            var (hash, salt) = PasswordHasher.HashPassword(newPassword);
            existing.PasswordHash = hash;
            existing.PasswordSalt = salt;
            _repository.UpdateWithPassword(existing);
        }
        else
        {
            _repository.UpdateProfile(existing);
        }
    }

    public void Deactivate(int userId)
    {
        AuthorizationService.RequirePermission(Permissions.ManageUsers);

        var current = SessionContext.Current;
        if (current is not null && current.UserID == userId)
            throw new BusinessRuleException("You cannot deactivate your own currently logged-in account.");

        _ = GetById(userId);
        _repository.SoftDeactivate(userId);
    }

    private static void ValidateUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ValidationException("Username is required.");
        if (username.Trim().Length > 50)
            throw new ValidationException("Username cannot exceed 50 characters.");
    }

    private static void ValidatePassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            throw new ValidationException("Password is required.");
        if (password.Length < 6)
            throw new ValidationException("Password must be at least 6 characters.");
    }

    private static void ValidateFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ValidationException("Full name is required.");
        if (fullName.Trim().Length > 100)
            throw new ValidationException("Full name cannot exceed 100 characters.");
    }
}
