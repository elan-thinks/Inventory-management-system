# NovaTech Electronics — Inventory Management System
## Visual Design Package (v1.1)

**Status:** Approved visual-design baseline derived from SRS v1.1 and Product Requirements + UX/UI Specification v1.1. The v1.0 visual package remains the base design; this v1.1 amendment adds the approved delegation capability without changing the existing visual language.

**Scope:** Visual design only — colors, typography, spacing, component appearance, screen layout, interaction states, and HTML/CSS visual mockups for the eventual C# WinForms build. No C#, SQL, WinForms Designer output, or implementation code.

**Source of truth:**
- `docs/` — approved SRS v1.1 and delegation amendment
- `product-requirements/` — approved Product/UX v1.1
- `UI/` — v1.0 visual system plus the v1.1 delegation amendment in this package

## 1. Version 1.1 Change

SRS v1.1 introduced controlled permission delegation. Product/UX v1.1 added the corresponding Delegation Management capability. This visual-design amendment adds the missing visual layer:

- **SCR-020 — Delegation Management**
- Administrator-only navigation and screen access
- Delegation list and status presentation
- Create Delegation form
- Delegation details/read-only view
- Revoke Delegation confirmation
- Active, Expired, and Revoked states
- Effective-permission indication without changing the user's permanent role
- Delegation empty, validation, error, and success states
- Updated traceability and final audit

No existing screen, business rule, permanent role, or permission is changed by this amendment.

## 2. Document Map

| # | File | Contents |
|---|---|---|
| 00 | `00-README.md` | Package scope, source of truth, version history |
| 01 | `01-Visual-Design-Overview.md` | Visual personality and design principles |
| 02 | `02-Design-Tokens.md` | Color, typography, spacing, radius, border, shadow tokens |
| 03 | `03-Component-Library.md` | Component visual states |
| 04 | `04-Application-Shell.md` | Sidebar, header, StatusStrip, content region |
| 05 | `05-Screen-Designs.md` | Base v1.0 screen designs, SCR-001–019 |
| 06 | `06-Dialogs-and-States.md` | Shared dialogs and states |
| 07 | `07-Accessibility.md` | Accessibility guidance |
| 08 | `08-WinForms-Implementation-Guidelines.md` | WinForms implementation mapping |
| 09 | `09-Visual-Design-Traceability.md` | Base visual traceability |
| 10 | `10-Final-Visual-Design-Audit.md` | Base v1.0 audit |
| 11 | `11-Visual-Design-v1.1-Delegation-Amendment.md` | **New v1.1 delegation visual specification** |
| 12 | `12-Visual-Design-v1.1-Audit.md` | **New v1.1 amendment audit and approval gate** |
| — | `UI-mock/*.html` | Rendered visual mockups |

## 3. Screen Baseline

The approved Product/UX v1.1 baseline contains **20 screens (SCR-001–020)**. The original 19-screen visual specification remains intact as the base; SCR-020 is supplied by the v1.1 amendment.

### Mockup coverage

The original package contains 13 shared HTML mockups for the 19-screen base. The v1.1 amendment adds:

| File | Screen |
|---|---|
| `UI-mock/delegation-management.html` | SCR-020 Delegation Management |

The separate `UI/update-ui/` staging area may contain source ZIPs or intermediate artifacts. The canonical visual mockup is the file under `UI/UI-mock/`.

## 4. Role and Permission Rule

The UI must reflect **effective permissions**, not role mutation:

`Effective Permission = Permanent Role Permission + Currently Valid Delegation`

A Staff user receiving a valid delegation does **not** become an Administrator. Delegation Management remains Administrator-only. Administrator-only capabilities such as User Management, Inventory Adjustment, and security/authorization management remain unavailable unless explicitly allowed by the SRS delegation rules.

Where a user lacks authority, inappropriate destructive/admin controls should not be rendered merely as disabled controls.

## 5. Non-Goals

- No new business rules beyond SRS v1.1
- No new roles
- No unrestricted role impersonation
- No delegation of Administrator privileges
- No changes to existing color or typography tokens
- No enterprise workflow engine
- No implementation code

## 6. Review / Implementation Order

1. Review v1.0 visual system.
2. Review `11-Visual-Design-v1.1-Delegation-Amendment.md`.
3. Open `UI-mock/delegation-management.html` in a browser.
4. Verify `12-Visual-Design-v1.1-Audit.md` is approved.
5. Only then freeze the UI baseline for C# WinForms implementation.
