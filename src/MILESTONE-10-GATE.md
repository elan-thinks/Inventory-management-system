# Milestone 10 Gate — Role-based authorization

**Status:** COMPLETE  

**Branch:** `version1.2`

---

## Objective

Enforce permanent role permissions (ADR-005): service layer is authoritative; UI hides unauthorized actions.

---

## What was implemented

| Item | Location |
|------|----------|
| Permission keys | `Security/Permissions.cs` |
| `AuthorizationService` | `Services/AuthorizationService.cs` |
| Effective permissions on login | `AuthService` |
| `UnauthorizedException` | `Utilities/AppExceptions.cs` |
| Service gates (CRUD) | Category / Supplier / Customer / Product / User services |
| User Management (FR-USR) | `UserService` + list/edit forms |
| Nav visibility | `MainForm` — Admin-only: Adjustment, Users, Delegations |

---

## Roles

| Role | Can do |
|------|--------|
| **Administrator** | Everything |
| **Inventory Staff** | View master data, Stock-In/Out (later), history, reports, dashboard — **no** master-data CUD, adjustment, user/delegation mgmt |

---

## How to test

1. Login as `admin` / `Admin@123`.
2. **User Management** → **+ Add User**: username `staff1`, password `Staff@123`, role **Inventory Staff**.
3. Logout → login as `staff1`.
4. Catalog screens are **view-only** (no Add/Edit/Delete if UI applied; service still blocks CUD).
5. **User Management**, **Delegations**, **Inventory Adjustment** hidden or blocked.

---

## Intentionally deferred

- Delegation merge into effective permissions (**Milestone 15**)
- Stock-In/Out service gates when those modules land (M11–12)

---

## Next milestone

**Milestone 11 — Stock-In**
