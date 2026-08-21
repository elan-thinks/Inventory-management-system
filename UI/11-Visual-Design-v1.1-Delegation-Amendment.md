# Visual Design v1.1 — Delegation Amendment

## Status

**Approved amendment pending final visual review.**

## 1. Purpose

This document extends the v1.0 visual design to support the approved SRS v1.1 delegation capability and Product/UX v1.1 changes.

It does not redesign the application. It adds only the visual layer required by the approved delegation requirements.

## 2. New Screen

### SCR-020 — Delegation Management

**Access:** Administrator only.

**Purpose:** Create, inspect, and revoke controlled temporary operational delegations.

### Layout

1. Standard NovaTech application shell.
2. Page header: `Delegation Management`.
3. Compact explanatory line: `Temporary operational responsibilities assigned to eligible users.`
4. Toolbar with search, status filter, responsibility filter, and `+ New Delegation`.
5. DataGridView with delegation records.
6. Status legend/summary.

### Grid columns

- Delegation ID
- Delegated To
- Responsibility
- Start Date
- End Date
- Status
- Delegated By
- Created Date
- Actions

Actions are contextual:

- View
- Revoke — Active delegations only

There is **no Edit action** for an existing delegation. A correction requires the existing delegation to be revoked and a new one created, preserving history.

## 3. Delegation Status Visuals

Use the existing badge system and never rely on color alone:

| Status | Visual treatment | Meaning |
|---|---|---|
| Active | success badge + check icon | Currently grants the delegated responsibility |
| Expired | muted badge + clock icon | End date has passed; grants nothing |
| Revoked | error/neutral badge + revoke icon | Explicitly ended before expiry; grants nothing |

The status label must always be visible as text.

## 4. Create Delegation

Use the existing modal/form pattern from the v1.0 component library.

### Fields

- Delegated To — required ComboBox; eligible active Staff users only
- Responsibility — required ComboBox; only approved delegatable responsibilities
- Start Date — required date
- End Date — required date
- Reason — required multiline text

### Informational preview

Before saving, show:

`This delegation grants [responsibility] to [user] from [start] through [end].`

Also show:

`The user's permanent role will not change.`

### Primary action

`Create Delegation`

### Secondary action

`Cancel`

## 5. Validation States

Use the existing inline validation treatment.

Required checks include:

- recipient selected
- recipient eligible and active
- responsibility selected
- responsibility is delegatable
- start date supplied
- end date supplied
- end date is not before start date
- reason supplied
- delegation does not violate overlapping/conflicting delegation rules
- delegator has authority to grant the responsibility

Error messages should identify the correction without exposing internal authorization details.

## 6. Delegation Details

The details view is read-only.

Group information as:

### Delegation

- Delegation ID
- Responsibility
- Status

### Recipient

- User
- Permanent Role

### Validity

- Start Date
- End Date

### Accountability

- Delegated By
- Created Date/Time
- Reason
- Revoked By, when applicable
- Revoked Date/Time, when applicable

### Effective Permission indicator

For an Active delegation, display a small info panel:

`Effective permission: [responsibility]`

with supporting text:

`This is a temporary delegation. The user's permanent role remains unchanged.`

For Expired/Revoked delegations, display:

`Effective permission: None — delegation is no longer active.`

## 7. Revoke Flow

Only Active delegations expose `Revoke`.

The confirmation dialog must show:

- recipient
- responsibility
- current validity period
- reason for the original delegation

The dialog asks for confirmation before revocation.

After confirmation:

1. Delegation becomes Revoked.
2. Revocation attribution is recorded.
3. The list refreshes.
4. A success StatusStrip message confirms revocation.
5. The record remains visible in history.

No hard-delete control is shown.

## 8. Empty State

When no delegations exist:

**Title:** `No delegations yet`

**Message:** `Create a temporary operational delegation when a staff member needs additional approved responsibility.`

**Action:** `New Delegation` — Administrator only.

When filters/search return no results:

`No delegations match your search or filters.`

with `Clear Filters`.

## 9. Error States

Database/load failure uses the existing ER-007 visual pattern:

- clear error icon
- concise message
- Retry action
- shell remains usable

If a delegation becomes invalid between opening and saving, preserve entered data and show the relevant business-rule validation.

## 10. Effective-Permission UX

The UI must distinguish **role** from **delegated responsibility**.

Example:

`Role: Inventory/Store Staff`

`Temporary delegation: Reports`

Do not relabel the user as Administrator and do not expose Administrator navigation merely because a delegation exists.

Only currently valid delegations affect effective permissions. Expired and revoked delegations must not grant access.

## 11. Application Shell Changes

Administrator navigation gains one item:

`Delegation Management`

Place it in the Administration group near User Management.

Staff navigation does not contain the item.

No new shell color, typography, spacing, or icon system is introduced.

## 12. Accessibility

- Status uses icon + text + color.
- All form fields have visible labels.
- Required fields use both asterisk and validation text.
- Focus order follows the visual order.
- Revoke is visually distinct as a destructive action.
- Keyboard navigation must reach the grid actions.
- Modal dialogs trap focus until closed.

## 13. WinForms Mapping

Recommended controls:

- `DataGridView` for delegation list
- `ComboBox` for recipient and responsibility
- `DateTimePicker` for start/end dates
- `TextBox` with `Multiline=true` for reason
- `Button` for Create/Cancel/Revoke/View
- `Panel`/`GroupBox` for sections
- existing StatusStrip/toast pattern for feedback
- existing reusable confirmation dialog pattern for revoke

No web-only interaction is required.

## 14. Traceability

| Visual element | Source |
|---|---|
| SCR-020 Delegation Management | Product/UX v1.1 screen inventory |
| Create delegation | FR-DEL requirements |
| Active/Expired/Revoked states | SRS v1.1 delegation business rules |
| Start/end dates | SRS v1.1 delegation data/validation requirements |
| Eligible Staff recipient | SRS v1.1 authorization rules |
| No Edit / no hard delete | Delegation history rules |
| Revoke flow | Delegation functional requirements |
| Effective permission indicator | Product/UX v1.1 delegation UX |
| Admin-only navigation | UX-DEC-001 + delegation authorization rules |

## 15. Design Constraint

This amendment is intentionally narrow. It does not introduce approval chains, notifications, multi-level delegation, delegation templates, bulk delegation, or unrestricted role impersonation.
