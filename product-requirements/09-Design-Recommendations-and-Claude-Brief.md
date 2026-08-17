# 9. Design Recommendations Requiring Requirements Approval

These are potential improvements identified while designing the UX/UI spec. **None are included in the MVP design.** Each requires a separate requirements decision before it can be added to SRS scope.

## 9.1 Resolved — Staff Master-Data Create/Edit Rights

**Decision:** Inventory/Store Staff are view/search-only for Products, Categories, Suppliers, and Customers. Administrators own master-data creation, editing, and deactivation/deletion.

**Decision ID:** UX-DEC-001

**Reason:** The approved SRS lists Staff permissions as viewing Products, Categories, Suppliers, and Customers while explicitly restricting Staff from deleting them. To avoid silently expanding privileges, the MVP adopts the conservative interpretation: master-data management is Administrator-only.

**Affected screens:** SCR-003/005, SCR-006/007, SCR-008/009, SCR-010/011.

**Status:** Resolved before visual-design handoff.

## 9.2 Persist Search/Filter State Across Navigation

- **Proposed feature:** Remember a user's active filters on Product List / Inventory History when they navigate away and back within the same session.
- **Reason:** Would reduce repeated filter re-entry for staff working a specific category or date range across a shift.
- **Affected area:** SCR-003, SCR-015.
- **Implementation impact:** Low-medium — requires session-scoped state, not persisted storage.
- **MVP or Future:** Future — not required by any SRS requirement; current reset-on-navigation behavior is simpler and fully compliant.

## 9.3 Dashboard Metric Drill-Down Beyond FR-DASH-002/003

- **Proposed feature:** Clickable dashboard tiles that deep-link into pre-filtered lists (already assumed as a sensible interaction in document 05 §5.2).
- **Reason:** FR-DASH only requires the metrics and recent activity to be *shown*; it does not require them to be interactive/navigable.
- **Affected area:** SCR-002.
- **Implementation impact:** Low — reuses existing filtered-list screens.
- **MVP or Future:** Recommend confirming as in-scope MVP polish, since it directly serves NFR-002 without adding new functionality — but it is technically beyond the literal FR-DASH wording and is flagged here for that reason.

## 9.4 Keyboard Shortcuts (Ctrl+S, Ctrl+N, Alt-mnemonics)

- **Proposed feature:** Documented in 7.9 as an accessibility/efficiency recommendation.
- **Reason:** Not required by any NFR or FR, but low-cost and aligned with NFR-001/002.
- **Affected area:** All forms and list screens.
- **Implementation impact:** Low — native WinForms KeyPreview/mnemonic support.
- **MVP or Future:** Optional MVP polish; safe to defer to Future without any functional loss.

## 9.5 "Reset Password" Action Separate from Edit-User Password Field

- **Proposed feature:** SCR-019 assumes editing a user does not show an always-visible password field, instead offering a distinct "Reset Password" action.
- **Reason:** SRS DR-USR and FR-USR do not specify this UX detail — they only require a password credential to exist.
- **Affected area:** SCR-019.
- **Implementation impact:** Low.
- **MVP or Future:** Recommend as MVP UX polish; functionally equivalent to an always-visible optional password field, so it does not expand scope, but is flagged since it's an interpretive detail beyond the literal DR-USR entity definition.

---

# 10. Claude Visual Design Brief

*Use this section as the direct creative brief for producing visual mockups/design exploration in Claude, based on the specification in documents 01–09.*

**Product:** Inventory Management System for NovaTech Electronics — a small electronics/gadgets retailer. C#/.NET/WinForms desktop application.

**Visual personality:** Professional, calm, trustworthy, and efficient. This is a business tool a store owner and staff will use dozens of times a day — it should feel like a well-made piece of equipment, not a marketing site. Understated confidence, not flashy. Think "well-organized ledger," not "startup dashboard."

**Color direction:** A controlled blue/teal professional palette (Primary `#1E4B8F`, Secondary `#3B7A8F`) on a light neutral background (`#F4F6F8`/`#FFFFFF` surfaces). Status colors (Success `#2E8B57`, Warning `#C77700`, Error `#C0392B`) are used sparingly and always paired with an icon and text label — never color alone. No gradients, no glassmorphism, no dark mode for MVP. Full palette in document 07 §7.1.

**Typography:** Segoe UI throughout (native Windows font — nothing custom or web-fontlike). Clear size hierarchy: 20px screen titles, 15px section headers, 12px body/buttons, 11–11.5px table content. Generous but not oversized line height for scanability in dense grids. Full scale in document 07 §7.2.

**Spacing:** Consistent 4/8/12/16/24/32px scale. Forms breathe (16–24px between field groups) but grids stay dense and efficient (compact row height) since scanning many rows quickly is a core task.

**Component style:** Flat, 1px-bordered panels and inputs, 4px radius on controls and 6px on cards/dialogs — modest rounding, not pill-everything. No drop shadows; depth communicated through subtle background/border contrast only (`#FFFFFF` surfaces on `#F4F6F8` background). Buttons: solid Primary fill for the one primary action per screen, outlined Secondary for Cancel/Back, solid Danger red reserved strictly for the confirming button in destructive dialogs.

**Navigation style:** Persistent left sidebar (~220px), grouped into Dashboard / Catalog (Products, Categories, Suppliers, Customers) / Inventory Operations (Stock In, Stock Out, Inventory Adjustment, Inventory History) / Reports / User Management (Administrator only, bottom). Active item has a filled background + left accent bar. Top header shows screen title and a global product-search box. Bottom StatusStrip shows current user, role, and date.

**Dashboard style:** A row of clean metric tiles (label + large number, minimal icon), followed by a two-column layout: Recent Activity (a simple, readable list of the latest stock movements) beside Quick Actions (large, obvious buttons for Stock-In/Stock-Out/Search — the tasks staff repeat most). No charts required for MVP; if included, must remain simple bar/line, WinForms-compatible, and secondary to the numbers themselves.

**Table style:** DataGridView-realistic — header row in a light gray-blue tint, alternating subtle row shading, clear selected-row highlight with a left accent bar, status rendered as a small colored pill badge (icon+label) rather than plain colored text or a whole colored row. Action icons (view/edit/delete) right-aligned, consistent across every grid.

**Form style:** Single, consistent Add/Edit dialog pattern reused for every master-data entity — title, grouped fields with visible persistent labels (never placeholder-only), red-asterisk required indicators, inline validation messages under each field, Cancel/Save bottom-right. Stock-In, Stock-Out, and Inventory Adjustment use the same field-grouping and button conventions but live as full dedicated screens (not modals) since they're used repeatedly through a shift. For master-data screens, these Add/Edit controls are Administrator-only.

**Status indicators:** In Stock (green check), Low Stock (amber warning triangle), Out of Stock (red cross) — always icon + label + color together, rendered as a pill badge, used identically everywhere status appears (lists, details, dashboard, Stock-Out selector, reports).

**Icon style:** One coherent outline icon set, 1.5–2px stroke weight, no mixed icon styles. Icons support labels; they never stand alone as the only signifier of meaning or status.

**Accessibility:** Full keyboard tab order and visible focus rings on every interactive control; WCAG-AA-adequate contrast on all text/background pairs; no meaning conveyed by color alone; persistent field labels; minimum 11px text.

**Desktop behavior:** Minimum window size 1024×768, resizable and maximizable main window with a fixed-width sidebar and fill-anchored content/grids; modal, fixed-size Add/Edit dialogs centered on the main window; a larger, resizable Report Viewer window. No mobile patterns, no gestures, no responsive breakpoints beyond desktop resolutions.

**What to avoid entirely:** animation-heavy transitions, card-stacked "consumer dashboard" layouts, large illustrations, gradients, drop shadows, mobile UI conventions, and any visual complexity that a standard WinForms control set (TextBox, Button, Label, ComboBox, DataGridView, DateTimePicker, CheckBox, Panel/GroupBox, MenuStrip/ToolStrip, StatusStrip, MessageBox) could not realistically reproduce.
