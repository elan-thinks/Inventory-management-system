# NovaTech Electronics — Inventory Management System
## Visual Design Package (v1.0)

**Status:** Derived from approved SRS v1.0 and approved Product Requirements + UX/UI Specification v1.0 (`product-requirements/` package). Both are the source of truth; this package changes nothing they define.
**Scope of this package:** Visual design only — colors, typography, spacing, component appearance, screen layout, and HTML/CSS visual mockups standing in for the eventual C# WinForms build. No C#, no SQL, no WinForms Designer output, no new functionality, no new screens, no changed business rules or permissions.
**Audience:** C# WinForms developer implementing the application; NovaTech stakeholders reviewing the visual direction before build.

---

## 1. What This Package Is

This package turns the approved Product Requirements + UX/UI Specification (`product-requirements/01`–`10`) into a complete, reproducible **visual design system** and a full set of **visual mockups** for every screen the specification defines. Every screen, color, spacing value, and component state documented here traces back to an entry in the PRD/UX package, which in turn traces back to the SRS. Nothing here originates a new requirement.

Where the PRD package already specified an exact value (colors, fonts, spacing, component states — see `product-requirements/07-Design-System.md` and `09-Design-Recommendations-and-Claude-Brief.md §10`), this package **implements that specification precisely** rather than re-inventing it. This is a deliberate choice: the brief already made the design decisions; this package's job is to execute them completely and consistently across all 19 screens, and to produce artifacts (tokens, component specs, mockups) a WinForms developer can build directly from.

## 2. Document Map

| # | File | Contents |
|---|------|----------|
| 00 | `00-README.md` | This file |
| 01 | `01-Visual-Design-Overview.md` | Visual personality, design principles, what was avoided and why |
| 02 | `02-Design-Tokens.md` | Machine-readable color, typography, spacing, radius, border, and shadow tokens |
| 03 | `03-Component-Library.md` | Every component's visual spec across all interaction states |
| 04 | `04-Application-Shell.md` | Sidebar, header, StatusStrip, content region — the reusable shell |
| 05 | `05-Screen-Designs.md` | All 19 screens (SCR-001–019), each traced to SRS → PR → UX → UI → Screen |
| 06 | `06-Dialogs-and-States.md` | Confirmation dialogs, blocked-delete dialogs, empty/error/success states |
| 07 | `07-Accessibility.md` | Keyboard navigation, contrast, color-independence, focus, labeling |
| 08 | `08-WinForms-Implementation-Guidelines.md` | How every visual decision maps to real WinForms controls |
| 09 | `09-Visual-Design-Traceability.md` | SRS → PR → UX → UI → Screen → Visual Design matrix |
| 10 | `10-Final-Visual-Design-Audit.md` | Final coverage checklist and verdict |
| — | `mockups/*.html` | 13 rendered visual mockups (see §4 below) |

## 3. Source of Truth

- **SRS v1.0** (`docs/01`–`25`) — functional requirements, business rules, data requirements, validation, error handling, use cases.
- **Product Requirements + UX/UI Specification v1.0** (`product-requirements/00`–`10`) — the direct input to this package. Screen inventory (`03-Screen-Inventory.md`), screen specifications (`05-Screen-Specifications.md`), CRUD/inventory/validation/error UX (`06-CRUD-Inventory-Validation-Error-UX.md`), and the design system (`07-Design-System.md`) are followed exactly.

No requirement IDs (FR-, BR-, DR-, VAL-, ER-, UC-, WF-, RPT-, NFR-, PR-, UI-, UX-, SCR-) have been invented in this package. Every ID referenced already exists in the approved input documents.

## 4. Visual Mockups

13 interactive HTML mockups are provided in `mockups/`, covering every screen the brief called out for mockups:

| File | Screen(s) |
|---|---|
| `login.html` | SCR-001 Login |
| `dashboard.html` | SCR-002 Dashboard |
| `product-list.html` | SCR-003 Product List |
| `product-form.html` | SCR-005 Product Add/Edit (modal, over Product List) |
| `category-management.html` | SCR-006/007 Category List + Add/Edit + Blocked-Delete dialog |
| `supplier-management.html` | SCR-008/009 Supplier List + Add/Edit |
| `customer-management.html` | SCR-010/011 Customer List |
| `stock-in.html` | SCR-012 Stock-In (with confirmation dialog) |
| `stock-out.html` | SCR-013 Stock-Out (insufficient-stock error state) |
| `inventory-adjustment.html` | SCR-014 Inventory Adjustment (with confirmation dialog) |
| `inventory-history.html` | SCR-015 Inventory Transaction History (read-only) |
| `reports.html` | SCR-016/017 Reports Hub + Report Viewer |
| `user-management.html` | SCR-018/019 User Management |

**All 13 mockups share one embedded design-token stylesheet** (Segoe UI, the exact hex palette from `07-Design-System.md`, 4/8/12/16/24/32px spacing, 4px/6px radius) so they read as one consistent application, not 13 separate ones. Product Details (SCR-004), the remaining Add/Edit dialogs (Category, Supplier, Customer, User), and the Blocked-Delete/confirmation dialog pattern are demonstrated inline within the list-screen mockups above rather than as separate files, since they are the same reusable modal pattern applied to different entities (see `03-Component-Library.md §3.9` and `06-Dialogs-and-States.md`).

**Role simulation:** every mockup includes a working "Administrator / Inventory Staff" switcher in the top-right header. Selecting "Inventory Staff" hides every Administrator-only control (Add/Edit/Delete buttons, Inventory Adjustment and User Management navigation, the deactivate-self restriction) live in the browser, so the role-based UI difference required by UX-DEC-001 can be inspected directly rather than only read about. This switcher is a mockup convenience only — it is not a feature to build; role visibility in the real application is determined once at login (see `04-Application-Shell.md §4.5`).

## 5. Non-Goals of This Package

- No C#, SQL, or WinForms Designer (.designer.cs) code
- No database schema
- No new screens, fields, business rules, or permissions beyond `product-requirements/01`–`10`
- No resolution of open items in `product-requirements/09-Design-Recommendations-and-Claude-Brief.md` — those remain logged as pending requirements decisions, not silently designed in (see `09-Visual-Design-Traceability.md §9.4`)

## 6. How to Use This Package

1. **Stakeholder review:** open the `mockups/*.html` files in a browser to review the visual direction before implementation begins.
2. **WinForms developer:** start with `02-Design-Tokens.md` (exact values), `03-Component-Library.md` (every control's states), and `08-WinForms-Implementation-Guidelines.md` (how each token/component maps to a real WinForms control). Use `05-Screen-Designs.md` for per-screen layout and `06-Dialogs-and-States.md` for shared dialog/empty/error patterns.
3. **Traceability check:** `09-Visual-Design-Traceability.md` and `10-Final-Visual-Design-Audit.md` confirm nothing was added, changed, or skipped relative to the approved SRS and PRD.
