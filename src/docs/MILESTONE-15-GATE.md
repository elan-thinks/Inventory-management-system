# Milestone 15 Gate — Delegation management

**Status:** COMPLETE  

**Branch:** `version1.2`

---

## Objective

Administrator creates time-bounded delegations of Stock-In, Stock-Out, or Report Access to active Inventory Staff. Revoke preserves history. Login unions valid delegations into `EffectivePermissions` (ADR-006).

---

## Prerequisites

Run once against PostgreSQL:

```bash
psql -d <your_db> -f database/15-delegation.sql
```

---

## What was implemented

| Item | Location |
|------|----------|
| Schema | `database/15-delegation.sql` |
| `DelegationRepository` | `Data/DelegationRepository.cs` |
| `DelegationService` | `Services/DelegationService.cs` |
| List + create UI | `Forms/Delegations/` |
| Auth union | `AuthService` + `AuthorizationService.BuildEffectivePermissions(role, userId)` |

---

## Rules enforced

- Admin only create/revoke (`ManageDelegations`)
- Recipient = active Inventory Staff
- Responsibility ∈ { StockIn, StockOut, ReportAccess }
- EndDate ≥ StartDate; reason required
- No overlapping Active same user + responsibility
- Revoke → Status=Revoked + actor/time (no hard delete)
- Past EndDate → Expired (lazy update on list/login)

---

## Next milestone

**Milestone 16 — Reports**
