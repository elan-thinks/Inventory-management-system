# 8. Functional Requirements

## 8.14 Delegation (FR-DEL)

| ID | Requirement |
|---|---|
| FR-DEL-001 | The system shall allow an Administrator to create a delegation for an eligible active user and one approved operational responsibility. |
| FR-DEL-002 | The system shall allow Administrators to view current and historical delegation records. |
| FR-DEL-003 | The system shall allow an Administrator to revoke an active delegation and record the revocation actor and timestamp. |
| FR-DEL-004 | The system shall grant the delegated responsibility only when the delegation is active and the current date falls within its configured start and end dates. |
| FR-DEL-005 | The system shall automatically treat a delegation as expired after its end date and shall not grant the delegated responsibility after expiry. |
| FR-DEL-006 | Creating a delegation shall not modify the recipient's permanent role. |
| FR-DEL-007 | The system shall prevent an Administrator from delegating a responsibility that the Administrator is not authorized to perform. |
| FR-DEL-008 | The system shall prevent delegation of User Management, Administrator-only security/authorization management, Inventory Adjustment, master-data deletion/deactivation, and other explicitly non-delegatable responsibilities. |
| FR-DEL-009 | The system shall allow delegation only to an eligible active user. |
| FR-DEL-010 | The system shall require a valid date range where StartDate is not after EndDate and shall prevent invalid or contradictory delegation periods. |
| FR-DEL-011 | The system shall require a reason when creating a delegation. |
| FR-DEL-012 | The system shall prevent overlapping or conflicting delegations where they would create ambiguous effective authorization for the same responsibility and recipient. |
| FR-DEL-013 | Delegation records shall be retained as authorization history; revocation shall change the delegation state rather than silently deleting its history. |
| FR-DEL-014 | The system shall record who created and, when applicable, who revoked each delegation. |

## 8.15 Delegation Authorization Model

The effective authorization of a user shall be determined from the user's permanent role permissions plus currently valid delegations. A delegation shall never change the user's permanent role or grant Administrator privileges.

> **SRS v1.1 reconciliation note:** FR-DEL-001 through FR-DEL-014 are the authoritative delegation functional requirements. The dedicated `docs/26-Delegation-Requirements-v1.1.md` provides the detailed amendment and remains consistent with this section.
