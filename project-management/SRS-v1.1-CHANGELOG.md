# SRS v1.1 Change Log

## Baseline Change

**Change ID:** CR-001  
**Feature:** Controlled Permission Delegation  
**Status:** Incorporated into SRS v1.1

## Documents Updated

- `docs/05-Scope.md`
- `docs/08-Functional-Requirements.md`
- `docs/09-Business-Rules.md`
- `docs/10-Data-Requirements.md`
- `docs/11-Validation-Requirements.md`
- `docs/18-Use-Cases.md`
- `docs/20-Requirement-Traceability.md`

## New Requirement Set

- Functional Requirements: `FR-DEL-001` through `FR-DEL-011`
- Business Rules: `BR-031` through `BR-039`
- Data Entity: `DR-DEL`
- Validation Requirements: `VAL-023` through `VAL-029`
- Use Case: `UC-019 — Manage Delegations`
- Objective: `OBJ-013 — Provide controlled temporary delegation`

## Delegation Boundary

Delegation is intentionally narrow and educationally appropriate for the WinForms project.

### Allowed

- Stock-In
- Stock-Out
- Approved report access where applicable

### Not Allowed

- Administrator privileges
- User Management
- Inventory Adjustment
- Master-data deletion/deactivation

## Authorization Rules

- Only Administrators create/revoke delegations.
- Recipient must be an active eligible Inventory/Store Staff user.
- Delegator cannot delegate beyond their own authority.
- Delegation does not change the recipient's permanent role.
- Start/end dates control validity.
- Expired or revoked delegations provide no effective permission.
- Conflicting overlapping delegations are prevented.
- Delegation history is retained with attribution.

## Downstream Impact Gate

The Product Requirements + UX/UI specification must be updated to include Delegation Management and effective-permission states before Visual Design v1.0 is approved.
