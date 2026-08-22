using System;
using NovaTechIMS.Models.Enums;

namespace NovaTechIMS.Models;

/// <summary>
/// Time-bounded grant of an operational responsibility (DR-DEL).
/// Does not change the recipient's permanent role.
/// </summary>
public class Delegation
{
    public int DelegationID { get; set; }
    public int DelegatedByUserID { get; set; }
    public int DelegatedToUserID { get; set; }
    public DelegatableResponsibility Responsibility { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DelegationStatus Status { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public int? RevokedByUserID { get; set; }
    public DateTime? RevokedDateTime { get; set; }
}
