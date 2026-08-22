# 09 - Delegation Technical Design

**Document:** Delegation (SRS v1.1)  
**Version:** 1.0  
**Related:** FR-DEL, BR-031–039, SCR-020  

---

## Concept

Temporary explicit grant of one approved operational responsibility from Administrator to eligible active InventoryStaff. Does **not** change permanent role; does **not** grant Admin privileges, Adjustment, User Mgmt, or master-data delete/deactivate.

```
Effective permission = permanent role permissions U currently valid delegations
```

## Allowed responsibilities

StockIn, StockOut, ReportAccess only.

## Lifecycle

Created -> Active; Active -> Expired (past EndDate) or Revoked. History retained; no hard delete.

## DelegationService

- CreateDelegation (Admin): validate eligibility, dates, responsibility, conflicts, authority -> INSERT Active
- RevokeDelegation (Admin): Status=Revoked + actor/time
- List / GetActiveForUser / IsValid

## Create validation

1. Current user is Administrator
2. Recipient active InventoryStaff
3. Responsibility in allowed set
4. EndDate >= StartDate; reason non-empty
5. No overlapping Active same recipient+responsibility

## UI (SCR-020)

DelegationManagementForm: grid, filters, New, View, Revoke (Active only). No Edit of existing rows.

## ADR-006

Separate Delegation entity + runtime union of role and valid delegations.
