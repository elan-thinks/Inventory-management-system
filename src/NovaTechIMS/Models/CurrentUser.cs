using System.Collections.Generic;
using NovaTechIMS.Models.Enums;

namespace NovaTechIMS.Models;

/// <summary>
/// Session snapshot after login (not a database entity).
/// EffectivePermissions will be filled when authorization is implemented (Milestone 10).
/// </summary>
public class CurrentUser
{
    public int UserID { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public UserRole Role { get; set; }

    /// <summary>
    /// Combined permanent role permissions plus currently valid delegations.
    /// Empty until auth milestones; shell uses demo labels only in Milestone 1–2.
    /// </summary>
    public List<string> EffectivePermissions { get; set; } = new();
}
