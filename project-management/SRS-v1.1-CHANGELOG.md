# SRS v1.1 Change Log

## Baseline Change

**Change ID:** CR-001  
**Feature:** Controlled Permission Delegation  
**Status:** Incorporated into SRS v1.1

## Documents Updated

- `docs/01-Document-Control.md`
- `docs/05-Scope.md`
- `docs/08-Functional-Requirements.md`
- `docs/09-Business-Rules.md`
- `docs/10-Data-Requirements.md`
- `docs/11-Validation-Requirements.md`
- `docs/18-Use-Cases.md`
- `docs/20-Requirement-Traceability.md`
- `docs/26-Delegation-Requirements-v1.1.md`
- `project-management/SRS-v1.1-AUDIT.md`

## Authoritative Delegation Requirement Set

- Functional Requirements: `FR-DEL-001` through `FR-DEL-014`
- Business Rules: `BR-031` through `BR-039`
- Data Entity: `DR-DEL`
- Validation Requirements: `VAL-023` through `VAL-029`
- Use Cases: `UC-019` through `UC-021`
- Objective: controlled temporary delegation as defined by the amended requirements and traceability documents

## Delegation Boundary

Delegation is intentionally narrow and educationally appropriate for the WinForms project.

### Allowed

- Stock-In
- Stock-Out
- Approved report access where applicable

### Not Allowed

- Administrator privileges
- User Management
- Administrator-only security/authorization management
- Inventory Adjustment
- Master-data deletion/deactivation
- Any responsibility the delegator is not authorized to perform

## Authorization Rules

- Only Administrators create/revoke delegations.
- Recipient must be an eligible active Inventory/Store Staff user.
- Delegator cannot delegate beyond their own authority.
- Delegation does not change the recipient's permanent role.
- Start/end dates control validity.
- Expired or revoked delegations provide no effective permission.
- Conflicting overlapping delegations are prevented.
- Delegation history is retained with attribution.
- Creation and revocation are attributable to the responsible Administrator.

## Downstream Impact Gate

The Product/UX and Visual Design baselines must include Delegation Management and effective-permission behavior before technical implementation begins.

## Reconciliation Note

An earlier draft of this changelog referenced only FR-DEL-001 through FR-DEL-011 and UC-019. That was incomplete. The authoritative SRS v1.1 baseline is now reconciled to FR-DEL-001 through FR-DEL-014 and UC-019 through UC-021, matching `docs/26-Delegation-Requirements-v1.1.md`.
