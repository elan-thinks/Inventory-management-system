using System;
using System.Collections.Generic;
using System.Linq;
using NovaTechIMS.Data;
using NovaTechIMS.Models;
using NovaTechIMS.Models.Enums;
using NovaTechIMS.Security;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Services;

/// <summary>
/// Delegation management (FR-DEL, BR-031–039, ADR-006).
/// </summary>
public class DelegationService
{
    private readonly DelegationRepository _repo = new();
    private readonly UserRepository _users = new();

    public IReadOnlyList<DelegationListRow> GetList(
        int? toUserId = null,
        string? status = null,
        string? responsibility = null)
    {
        AuthorizationService.RequirePermission(Permissions.ManageDelegations);
        _repo.ExpirePastDue(DateTime.Today);
        return _repo.GetList(toUserId, status, responsibility);
    }

    public IReadOnlyList<UserListRow> GetEligibleRecipients()
    {
        AuthorizationService.RequirePermission(Permissions.ManageDelegations);
        return _users.GetList(null, isActiveFilter: true)
            .Where(u => u.Role == UserRole.InventoryStaff)
            .ToList();
    }

    public int Create(
        int delegatedToUserId,
        DelegatableResponsibility responsibility,
        DateTime startDate,
        DateTime endDate,
        string reason)
    {
        AuthorizationService.RequirePermission(Permissions.ManageDelegations);

        var current = SessionContext.Current
            ?? throw new UnauthorizedException("You must be signed in.");

        if (current.Role != UserRole.Administrator)
            throw new UnauthorizedException("Only an Administrator can create delegations.");

        var recipient = _users.GetById(delegatedToUserId)
            ?? throw new NotFoundException("The selected user no longer exists.");

        if (!recipient.IsActive)
            throw new BusinessRuleException("Delegation can only be granted to an active user.");

        if (recipient.Role != UserRole.InventoryStaff)
            throw new BusinessRuleException("Delegation can only be granted to Inventory Staff (not Administrators).");

        if (startDate.Date > endDate.Date)
            throw new ValidationException("Start date cannot be after end date.");

        if (string.IsNullOrWhiteSpace(reason))
            throw new ValidationException("Reason is required when creating a delegation.");

        if (reason.Trim().Length > 500)
            throw new ValidationException("Reason cannot exceed 500 characters.");

        // Non-delegatable responsibilities are not in the enum; still guard.
        if (responsibility is not (DelegatableResponsibility.StockIn
            or DelegatableResponsibility.StockOut
            or DelegatableResponsibility.ReportAccess))
        {
            throw new BusinessRuleException("That responsibility cannot be delegated.");
        }

        if (_repo.HasOverlappingActive(delegatedToUserId, responsibility, startDate, endDate))
        {
            throw new BusinessRuleException(
                "An active overlapping delegation already exists for this user and responsibility.");
        }

        var d = new Delegation
        {
            DelegatedByUserID = current.UserID,
            DelegatedToUserID = delegatedToUserId,
            Responsibility = responsibility,
            StartDate = startDate.Date,
            EndDate = endDate.Date,
            Reason = reason.Trim(),
            Status = DelegationStatus.Active,
            CreatedDateTime = DateTime.UtcNow
        };

        return _repo.Insert(d);
    }

    public void Revoke(int delegationId)
    {
        AuthorizationService.RequirePermission(Permissions.ManageDelegations);

        var current = SessionContext.Current
            ?? throw new UnauthorizedException("You must be signed in.");

        var existing = _repo.GetById(delegationId)
            ?? throw new NotFoundException("The selected delegation no longer exists.");

        if (existing.Status != DelegationStatus.Active)
            throw new BusinessRuleException("Only an active delegation can be revoked.");

        _repo.Revoke(delegationId, current.UserID, DateTime.UtcNow);
    }

    /// <summary>Used at login to union delegated permissions into EffectivePermissions.</summary>
    public static IReadOnlyList<string> GetDelegatedPermissionKeys(int userId)
    {
        var repo = new DelegationRepository();
        repo.ExpirePastDue(DateTime.Today);
        var responsibilities = repo.GetValidResponsibilities(userId, DateTime.Today);

        var keys = new List<string>();
        foreach (var r in responsibilities)
        {
            switch (r)
            {
                case DelegatableResponsibility.StockIn:
                    keys.Add(Permissions.StockIn);
                    break;
                case DelegatableResponsibility.StockOut:
                    keys.Add(Permissions.StockOut);
                    break;
                case DelegatableResponsibility.ReportAccess:
                    keys.Add(Permissions.ViewReports);
                    break;
            }
        }

        return keys;
    }
}
