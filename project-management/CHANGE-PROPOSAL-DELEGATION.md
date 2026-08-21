# Change Proposal — Delegation

## Status

🟡 Proposed for SRS v1.1

## Change ID

CR-001 — Controlled Permission Delegation

## Reason

The project requires a small, explicit delegation capability so an Administrator can temporarily assign selected operational responsibilities to an eligible Inventory/Store Staff user without changing the user's permanent role.

## Scope

Delegation is limited to approved operational responsibilities. It must not become unrestricted role impersonation or a mechanism for granting Administrator privileges.

### Proposed delegatable responsibilities

- Stock-In
- Stock-Out
- Approved report access where applicable

### Non-delegatable responsibilities

- User Management
- Administrator-only security/authorization management
- Inventory Adjustment
- Master-data deletion/deactivation
- Any responsibility the delegator is not authorized to perform

## Proposed Delegation Data

- DelegationID
- DelegatedByUserID
- DelegatedToUserID
- DelegatedResponsibility
- StartDate
- EndDate
- Reason
- Status
- CreatedDateTime
- RevokedByUserID (optional)
- RevokedDateTime (optional)

## Business Rules

1. Only an Administrator may create or revoke a delegation.
2. A delegation recipient must be an eligible active user.
3. A delegation cannot grant a responsibility beyond the delegator's own authority.
4. Delegation is valid only during its configured date range.
5. Expired delegations must not grant effective permission.
6. Delegation does not change the recipient's permanent role.
7. Delegation records are retained as history and are not silently deleted.
8. A delegation must have a clear responsibility and reason.
9. The system must prevent overlapping/conflicting delegations where they would produce ambiguous authorization.
10. Delegation actions must be attributable to the Administrator who created or revoked them.

## UX Impact

The approved Product/UX package will require a Delegation Management screen available to Administrators, plus clear effective-permission behavior for the recipient. Staff should not receive access to delegation management.

## Educational Value

The feature demonstrates role-based authorization, CRUD, validation, date-range logic, business rules, audit/history, and WinForms UI handling without introducing enterprise-scale workflow complexity.

## Approval Gate

This proposal must be incorporated into SRS v1.1 before the downstream Product/UX and Visual Design baselines are amended.
