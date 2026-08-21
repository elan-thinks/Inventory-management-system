# 1. Visual Design Overview

## 1.1 Visual Personality

NovaTech IMS should look and feel like **a well-made piece of equipment, not a marketing site or a consumer app.** It is opened dozens of times a day by a store owner and two or three staff who need to answer, at a glance, "what do we have, what's running low, what just happened." Every visual decision in this package serves that goal directly.

The application is:

- **Modern** — clean type, sensible spacing, clear hierarchy — executed entirely within real WinForms constraints, not web aesthetics borrowed for the sake of it.
- **Professional** — appropriate for an owner making purchasing decisions, not a hobbyist tool.
- **Clean** — no visual noise competing with data; chrome stays quiet, data stays legible.
- **Trustworthy** — numbers are always current, status is never ambiguous, destructive actions are never silent.
- **Calm** — no urgency-manufacturing patterns (no red pulsing badges, no artificial scarcity styling); alerts inform, they don't alarm.
- **Organized** — every entity has one predictable home and one predictable interaction pattern.
- **Efficient** — the most frequent tasks (Stock-In, Stock-Out, Search) are one or two clicks from the Dashboard and are optimized for repeat use within a shift.
- **Premium but not flashy** — understated confidence: a controlled palette, modest 4–6px radii, flat surfaces, no gradients, no glassmorphism, no drop shadows.

This is directly inherited from `product-requirements/01-Product-Vision-and-UX-Strategy.md §1.1–1.2` and the Claude Visual Design Brief in `product-requirements/09 §10` — this package does not deviate from that direction; it executes it in full detail across every screen and component.

## 1.2 Design Principles (Governing this Package)

1. **Clarity over decoration.** Every visual element earns its place by helping the user read data or complete a task faster.
2. **Data readability over visual effects.** The DataGridView content is the product. Its surrounding chrome (headers, filters, toolbars) stays quiet and out of the way.
3. **Consistency across forms.** A user who has used the Product Add/Edit form already knows the Category, Supplier, Customer, and User forms — same field grouping, same label style, same button order.
4. **Clear navigation.** The user always knows where they are (sidebar highlight + screen title), what's next to them (visible sidebar), and how to get back (sidebar is always present — no dead ends).
5. **Immediate feedback.** Every save, delete, or stock movement produces a visible, same-screen confirmation.
6. **Prevent errors before they happen.** Disable what can't be used yet; validate live where it's meaningful (Stock-Out quantity vs. available stock); never let a user submit something that's guaranteed to fail.
7. **Destructive actions require confirmation**, worded specifically — never a bare "Are you sure?"
8. **Important inventory information is visually prominent.** Stock status is a badge, not a buried table cell.
9. **Minimize unnecessary clicks.** Frequent tasks reach their screen in one or two clicks from the Dashboard, and repeat-entry screens (Stock-In/Stock-Out) reset in place rather than forcing renavigation.
10. **Realistic for WinForms.** Every visual pattern in this package is achievable with standard WinForms controls — TextBox, Button, Label, ComboBox, DataGridView, DateTimePicker, CheckBox, Panel/GroupBox, MenuStrip/ToolStrip, StatusStrip, MessageBox, ErrorProvider.

## 1.3 What Was Deliberately Avoided, and Why

Per SRS constraints C-001–C-009, the WinForms non-goals in SRS §16 and §23, and design philosophy #10 above, this package does not use:

| Avoided | Why |
|---|---|
| Animations/transitions beyond native WinForms behavior | Not reproducible without WPF/third-party libraries; adds nothing to task completion |
| Card-stacked "consumer dashboard" layouts | Reads as a hobby/startup app, not the trustworthy business tool NovaTech needs |
| Large decorative illustrations | Pure decoration; competes with data for attention |
| Mobile gestures (swipe, pull-to-refresh) | Desktop-only application (SRS §23); no touch target assumed |
| Web-only patterns (infinite scroll, hover-reveal menus) | Not WinForms-realistic; hover-reveal also fails keyboard-only navigation |
| Excessive gradients or shadow effects | Not reproducible with flat WinForms panel fills; reads as dated "glossy" web design, not calm/professional |
| Heavy rounding / pill-everything | Consumer-mobile visual language; 4–6px radius keeps the desktop-software register |
| Pagination on grids | SRS targets hundreds–low-thousands of records (NFR-004, NFR-016, A-005); standard scrolling is simpler and sufficient |
| Dark mode / advanced theming | Not in SRS MVP scope (§23); one consistent light theme keeps implementation and QA effort proportional to the brief |
| Anything requiring WPF, third-party UI libraries, or custom-drawn controls | Breaks the "realistic for WinForms" governing principle outright |

## 1.4 User Roles (No Change From Approved Spec)

Only the two roles defined in SRS §6.2 and PRD `01 §1.3` exist in this visual design. No new role, permission tier, or admin sub-level has been introduced.

- **Administrator** — full master-data CRUD, Stock-In/Out, Inventory Adjustment, User Management, all reports.
- **Inventory / Store Staff** — view/search master data, Stock-In/Out, Inventory History, an approved subset of reports.

Role differences are expressed visually by **hiding, not merely disabling**, controls the current role cannot use (UX-DEC-001; PR-012; UI-014) — see `04-Application-Shell.md §4.5` and `03-Component-Library.md §3.11`.

## 1.5 Relationship to the Approved Design System Brief

`product-requirements/07-Design-System.md` and the Claude Visual Design Brief in `product-requirements/09 §10` already specify exact colors, type sizes, spacing, radii, and component behavior. This package treats those values as final and non-negotiable, and its job is to:

1. Apply them **completely and identically** across all 19 screens (no drift screen to screen).
2. Resolve every visual detail the brief left implicit (e.g., exact badge tint values, hover/pressed states, empty-state iconography) in a way that is consistent with the brief's stated direction.
3. Produce **working mockups**, not just written specification, so the visual result can be reviewed before a single line of WinForms code is written.

See `02-Design-Tokens.md` for the literal values.
