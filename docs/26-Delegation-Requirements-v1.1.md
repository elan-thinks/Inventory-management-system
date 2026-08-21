# 26. Delegation Requirements — SRS v1.1 Amendment

## 26.1 Purpose

The Delegation feature allows an Administrator to temporarily assign selected operational responsibilities to an eligible Inventory/Store Staff user without changing the user's permanent role.

Delegation is an explicit authorization grant, not role impersonation.

## 26.2 Scope

### Delegatable

- Stock-In
- Stock-Out
- Approved report access where applicable

### Not Delegatable

- User Management
- Administrator-only security or authorization management
- Inventory Adjustment
- Master-data deletion/deactivation
- Any responsibility the delegating Administrator is not authorized to perform

## 26.3 Delegation Data Requirements

| Attribute | Required | Description |
|---|---|---|
| DelegationID | Yes | Unique identifier |
| DelegatedByUserID | Yes | Administrator creating the delegation |
| DelegatedToUserID | Yes | Eligible active recipient |
| DelegatedResponsibility | Yes | Specific approved responsibility |
| StartDate | Yes | First date on which delegation is effective |
| EndDate | Yes | Last date on which delegation is effective |
| Reason | Yes | Business reason for delegation |
| Status | Yes | Active / Expired / Revoked |
| CreatedDateTime | Yes | Creation timestamp |
| RevokedByUserID | No | Administrator who revoked it |
| RevokedDateTime | No | Revocation timestamp |

## 26.4 Functional Requirements

### FR-DEL-001 — Create Delegation

The system shall allow an Administrator to create a delegation for an eligible active user and one approved operational responsibility.

### FR-DEL-002 — View Delegations

The system shall allow Administrators to view current and historical delegation records.

### FR-DEL-003 — Revoke Delegation

The system shall allow an Administrator to revoke an active delegation and record the revocation actor and timestamp.

### FR-DEL-004 — Effective Permission

The system shall grant the delegated responsibility only when the delegation is active and the current date falls within its configured start and end dates.

### FR-DEL-005 — Expiry

The system shall automatically treat a delegation as expired after its end date and shall not grant the delegated responsibility after expiry.

### FR-DEL-006 — No Role Change

Creating a delegation shall not modify the recipient's permanent role.

### FR-DEL-007 — Authority Boundary

The system shall prevent an Administrator from delegating a responsibility that the Administrator is not authorized to perform.

### FR-DEL-008 — Restricted Responsibilities

The system shall prevent delegation of User Management, Administrator-only security/authorization management, Inventory Adjustment, master-data deletion/deactivation, and other explicitly non-delegatable responsibilities.

### FR-DEL-009 — Recipient Eligibility

The system shall allow delegation only to an eligible active user.

### FR-DEL-010 — Date Validation

The system shall require a valid date range where StartDate is not after EndDate and shall prevent invalid or contradictory delegation periods.

### FR-DEL-011 — Reason

The system shall require a reason when creating a delegation.

### FR-DEL-012 — Conflict Prevention

The system shall prevent overlapping or conflicting delegations where they would create ambiguous effective authorization for the same responsibility and recipient.

### FR-DEL-013 — History

Delegation records shall be retained as authorization history. Revocation shall change the delegation state rather than silently deleting its history.

### FR-DEL-014 — Auditability

The system shall record who created and, when applicable, who revoked each delegation.

## 26.5 Security and Business Rules

1. Only Administrators may create or revoke delegations.
2. Inventory/Store Staff cannot create, modify, or revoke delegations.
3. A delegation cannot grant Administrator privileges.
4. A delegation cannot exceed the authority of its delegator.
5. Expired or revoked delegations confer no effective permission.
6. Delegation does not change the recipient's role.
7. Effective permission must be evaluated from the permanent role plus currently valid delegations.
8. Every delegated operation remains attributable to the actual user who performed it.

## 26.6 Use Cases

### UC-019 — Create Delegation

**Actor:** Administrator

**Goal:** Temporarily assign an approved operational responsibility to an eligible user.

**Main flow:**
1. Administrator opens Delegation Management.
2. Administrator selects an eligible active user.
3. Administrator selects an approved responsibility.
4. Administrator enters start date, end date, and reason.
5. System validates eligibility, authority, dates, and conflicts.
6. Administrator confirms.
7. System creates the delegation and records the creator.

### UC-020 — Revoke Delegation

**Actor:** Administrator

**Goal:** End an active delegation before its scheduled expiry.

**Main flow:**
1. Administrator opens active delegations.
2. Administrator selects a delegation.
3. Administrator chooses Revoke.
4. System requests confirmation.
5. Administrator confirms.
6. System records the revocation actor/time and marks the delegation revoked.

### UC-021 — View Delegation History

**Actor:** Administrator

**Goal:** Review active, expired, and revoked delegations.

**Main flow:**
1. Administrator opens Delegation Management.
2. System displays delegation records.
3. Administrator filters or searches as supported.
4. System displays delegation details and status.

## 26.7 UI Requirements

- Delegation Management shall be available only to Administrators.
- The recipient selector shall show only eligible active users.
- The responsibility selector shall show only delegatable responsibilities.
- The UI shall clearly display start/end dates and current status.
- Expired and revoked delegations shall be visually distinguishable from active delegations.
- The UI shall not provide delegation controls to Staff.
- Delegation history shall be read-only except for the explicit Revoke action on active delegations.

## 26.8 Traceability Impact

This amendment introduces:

- FR-DEL-001 through FR-DEL-014
- UC-019 through UC-021
- A new Delegation Management screen in the downstream Product/UX baseline

The existing SRS requirements remain unchanged unless explicitly amended elsewhere.
