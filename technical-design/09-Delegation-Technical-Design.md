# 09 — Delegation Technical Design

**Document:** Delegation (SRS v1.1)  
**Version:** 1.0  
**Related:** FR-DEL-001…014, BR-031…039, DR-DEL, SCR-020, VAL-023…029  

---

## 1. Conceptual Model

Delegation is a **temporary, explicit grant** of one approved operational responsibility from an Administrator to an eligible active Inventory/Store Staff user.

It does **not**:

- change the recipient’s permanent role,
- grant Administrator privileges,
- allow Inventory Adjustment, User Management, or master-data deletion/deactivation.

Effective permission for a user is:

```
Permanent role permissions  ∪  currently valid delegations
```

---

## 2. Entity (already defined in Database Design)

See table `Delegation` in 04-Database-Design.md.

Key fields: DelegatedByUserID, DelegatedToUserID, Responsibility, StartDate, EndDate, Reason, Status, RevokedByUserID, RevokedDateTime.

---

## 3. Allowed Responsibilities

```csharp
public enum DelegatableResponsibility
{
    StockIn,
    StockOut,
    ReportAccess
}
```

Anything else is rejected at creation time.

---

## 4. Status Lifecycle

```
Created → Active
Active  → Expired   (when current date > EndDate)  — automatic evaluation
Active  → Revoked   (explicit Administrator action)
```

Expired and Revoked records are retained forever (history).  
No hard delete of delegation rows.

---

## 5. DelegationService Methods

| Method | Actor | Behaviour |
|--------|-------|-----------|
| CreateDelegation | Administrator only | Validate eligibility, dates, responsibility, conflicts, authority → INSERT Status=Active |
| RevokeDelegation | Administrator only | Set Status=Revoked, RevokedByUserID, RevokedDateTime |
| GetDelegations | Administrator | List with filters (status, recipient, responsibility, date range) |
| GetActiveDelegationsForUser | System / Auth | Used when building CurrentUser |
| IsValid | System | Status==Active && Start ≤ Today ≤ End |

### Create Validation Checklist (must all pass)

1. Current user is Administrator.
2. Recipient is active and Role = InventoryStaff.
3. Responsibility is in the DelegatableResponsibility set.
4. StartDate and EndDate supplied; EndDate ≥ StartDate.
5. Reason non-empty (max 250).
6. No overlapping Active delegation already exists for the same (Recipient, Responsibility) whose date range intersects the new range.
7. Delegator (Administrator) is allowed to perform the responsibility (always true for the three allowed ones).

### Revoke Validation

1. Current user is Administrator.
2. Target delegation exists and Status = Active.

---

## 6. Effective Permission Calculation

Performed at login (and optionally refreshed):

```csharp
List<DelegatableResponsibility> CalculateEffectivePermissions(User user)
{
    var perms = new List<DelegatableResponsibility>();

    // Permanent role
    if (user.Role == UserRole.Administrator)
    {
        // Administrators already have everything; delegations are irrelevant for them
        return allPermissions;
    }

    // Base Staff permissions
    perms.Add(DelegatableResponsibility.StockIn);   // if Staff permanently have Stock-In
    perms.Add(DelegatableResponsibility.StockOut);
    // (exact base set follows the approved Staff permissions)

    // Add currently valid delegations
    var today = DateTime.Today;
    foreach (var d in GetActiveDelegationsForUser(user.UserID))
    {
        if (d.IsCurrentlyValid(today))
            perms.Add(d.Responsibility);
    }

    return perms.Distinct().ToList();
}
```

Note: According to the SRS, Inventory/Store Staff already possess Stock-In and Stock-Out as permanent permissions.  
Delegation is useful for temporarily granting ReportAccess or for future extension; the design still supports granting any of the three allowed responsibilities.

AuthorizationService.HasPermission simply checks whether the required permission appears in CurrentUser.EffectivePermissions (or is implied by Administrator role).

---

## 7. UI Mapping (SCR-020)

- Form: `DelegationManagementForm`
- Access: Administrator only (menu item hidden for Staff).
- Grid columns as specified in Visual Design v1.1 amendment.
- Actions: View (read-only details), Revoke (Active only), + New Delegation.
- No Edit of an existing delegation — revoke + create new preserves history.
- Create dialog: ComboBox of eligible Staff, ComboBox of responsibilities, DateTimePickers, multiline Reason, preview text, Create / Cancel.

---

## 8. History & Audit

- Every creation records DelegatedByUserID + CreatedDateTime.
- Every revocation records RevokedByUserID + RevokedDateTime.
- Status changes are the only mutations; the original row is never deleted.
- Reports or the grid can show Active / Expired / Revoked with visual badges.

---

## 9. Edge Cases Explicitly Handled

| Case | Handling |
|------|----------|
| Recipient deactivated after delegation created | Delegation remains in history; IsCurrentlyValid still evaluates dates/status; login of deactivated user is already blocked |
| Overlapping dates for same responsibility | Rejected at create |
| EndDate in the past at create time | Rejected (or immediately Expired — prefer reject) |
| Administrator tries to delegate Adjustment | Rejected (not in enum) |
| Staff opens Delegation Management | AuthorizationException / UI hidden |
| Expired delegation | Grants nothing |

---

## 10. Architecture Decision Record

**ADR-006 — Delegation Permission Model**

- **Context:** SRS v1.1 requires temporary operational grants without role change.
- **Decision:** Separate Delegation entity + runtime union of permanent role and valid delegations.
- **Consequences:** Clear audit trail, no role mutation, simple to reason about, fully testable.
