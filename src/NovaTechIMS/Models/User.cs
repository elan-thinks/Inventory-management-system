using System;
using NovaTechIMS.Models.Enums;

namespace NovaTechIMS.Models;

/// <summary>Application user account (logical entity DR-USR).</summary>
public class User
{
    public int UserID { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; }
    public DateTime? LastLoginDate { get; set; }
}
