# 6. CRUD, Inventory, Validation & Error UX Patterns

## 6.1 CRUD UX (Consistent Across All Entities)

**Role boundary:** CRUD patterns describe the interaction model, but permissions are role-specific. Administrator performs master-data Create/Update/Delete/Deactivate. Inventory/Store Staff are view/search-only for Products, Categories, Suppliers, and Customers. Both roles may perform the approved operational Stock-In/Stock-Out actions; Inventory Adjustment is Administrator-only.

**CREATE**
1. Open form (modal dialog for master data; dedicated screen for Stock-In/Out/Adjustment because those are used repeatedly in a session).
2. Enter information into grouped fields, required fields marked with a red asterisk.
3. Validate on blur (per field) and again on Save (whole form).
4. Save persists the record.
5. Success feedback: a transient status message (see 6.9) plus dialog close / form reset.
6. List updates immediately — new row visible, ideally scrolled into view/highlighted briefly.

**READ**
- List view: DataGridView with search + filters always visible above the grid.
- Detail view: read-only field layout, used where SRS explicitly separates "view details" from the list (Product only — other master-data entities show details inline via the Administrator's Edit form, while Staff remain view/search-only).
- Search and filtering are always paired, never search-only or filter-only.

**UPDATE**
- Edit opens the same form layout as Create, pre-filled; this interaction is available to Administrator only for master data.
- Fields that must never be user-edited directly (QuantityOnHand) are shown disabled/read-only with an explanatory caption rather than hidden — hiding a field the user expects to see (because it appears on the detail view) causes confusion; showing it disabled teaches the user *why* it can't be changed here.
- Same validation, same Save/Cancel pattern as Create.
- Success/confirmation feedback identical in style to Create.

**DELETE / DEACTIVATE**
1. System determines whether hard deletion is permitted (per entity-specific business rule: BR-008 Category, BR-009 Supplier, BR-010 Customer, BR-018 Product).
2. **If permitted:** standard confirmation dialog ("Delete {entity name}? This cannot be undone.") → Yes commits.
3. **If blocked:** a dedicated explanation dialog states *why* ("This supplier is used by 3 products and cannot be deleted.") and offers "Mark as Inactive" as the resolving action in the same dialog, so the user isn't dead-ended.
4. Success feedback follows either path ("Deleted" or "Marked Inactive").
5. Delete/Deactivate controls are exposed only to Administrator; Staff do not receive master-data mutation controls.

## 6.2 Global CRUD Consistency Rules

- All Add/Edit forms place Cancel (left) and Save (right, primary style) at the bottom of the dialog, in that consistent order, across every entity.
- All required fields use the same visual indicator: a red asterisk after the label.
- All list screens place the primary "+ Add {Entity}" action top-right of the header for Administrator; Staff do not see this mutation action on master-data lists.
- All list-screen action columns use the same icon set and column position (rightmost): View (if applicable) / Edit / Delete, in that order for Administrator; Staff see View only where applicable.
- All destructive actions (Delete, Deactivate) use the same confirmation-dialog visual pattern (see 6.7).
- All status indicators (Active/Inactive, In Stock/Low/Out) use the same Icon + Label + Color system (see 6.3, document 07).
- All error messages follow the same three-tier hierarchy (see 6.6).
- Button terminology is fixed system-wide: **Save** (never "Submit" or "OK" on data-entry forms), **Cancel** (never "Close" on a form with unsaved data), **Delete** (never "Remove"), **Print** (never "Export" — export doesn't exist in MVP).

## 6.3 Inventory-Specific UX: Stock Status

Per BR-015 / FR-STK-001, status is calculated, never entered:

| Status | Condition | Icon | Color | Label |
|---|---|---|---|---|
| **In Stock** | Quantity > Minimum Level | ✓ (check circle) | Success green | "In Stock" |
| **Low Stock** | 0 < Quantity ≤ Minimum Level | ⚠ (warning triangle) | Warning amber | "Low Stock" |
| **Out of Stock** | Quantity = 0 | ✕ (circle with line/cross) | Error red | "Out of Stock" |

**Rule: Icon + Label + Color together, always.** Color alone never carries meaning (accessibility requirement, also directly requested in the design brief). This status treatment appears identically on: Product List rows, Product Details, Dashboard tiles, Stock-Out product selector, and all relevant reports.

Visual hierarchy: status is placed as a compact badge (pill shape, icon-left) rather than a plain table cell, so it's scannable at a glance down a long product list.

## 6.4 Form Design (Reusable Pattern)

**Standard CRUD form anatomy:**
1. **Form title** — states the action and entity ("Add Product" / "Edit Product").
2. **Field area** — grouped logically (e.g., Identity / Category & Supplier / Pricing), using GroupBox or Panel-with-header sections when a form has more than ~6 fields (Product, User, Stock-In, Stock-Out, Adjustment); simple flat layout for short forms (Category, Supplier, Customer).
3. **Required-field indicators** — red asterisk directly after the label text.
4. **Validation messages** — appear directly beneath the offending field, in the error color, replacing any helper text there.
5. **Cancel** — left of Save, secondary button style; discards changes (confirms discard only if the form is dirty).
6. **Save** — right, primary button style; disabled until the form has no known validation errors is *not* required (users should be able to attempt Save and get told what's wrong) — Save is always clickable, but a second, low-friction layer of live per-field validation prevents most errors from ever reaching Save.

**Reset/Clear:** Only present on the repeated-entry screens (Stock-In, Stock-Out) as "Clear Form," since those are used many times per session and a quick reset is genuinely useful there; not present on master-data Add/Edit dialogs (a Cancel + reopen achieves the same with less UI clutter).

**Close (X):** Available on all modal dialogs, behaves identically to Cancel.

## 6.5 Data Grid Design (DataGridView Standard Pattern)

- **Column hierarchy:** Identity columns (ID, Name) leftmost and widest/fill; supporting columns (Category, Supplier, dates) fixed-width in the middle; Status always as a distinct badge column; Actions always rightmost, fixed-width, icon-only with tooltips.
- **Column visibility:** Every grid shows only the columns relevant to that entity's list purpose (kept close to SRS report column definitions — no speculative extra columns).
- **Row selection:** Single-row selection (SelectionMode = FullRowSelect), used to enable row-context actions and to feed detail views.
- **Sorting:** Click column header to sort ascending/descending on sortable columns (Name, Date, Quantity, Status); default sort is Name ascending for master data, Date descending (most recent first) for transaction grids.
- **Filtering:** Handled by the filter bar above the grid, not by in-column filter dropdowns (keeps the interaction model simple and consistent, appropriate for WinForms and the expected non-technical staff audience).
- **Empty state:** See 6.8.
- **Loading state:** A lightweight "Loading…" overlay or label replaces the grid body briefly on first load/refresh; the grid does not flash empty before showing "no records" during a real load.
- **Action columns:** Administrator master-data lists show View/Edit/Delete where applicable. Staff master-data lists show View only. Each icon has a tooltip. Restricted mutation controls are not shown to Staff, consistent with the approved UX-DEC-001 role-visibility decision.
- **Pagination:** **Not used.** SRS explicitly targets hundreds to low thousands of records (NFR-004, NFR-016, A-005) — standard DataGridView scrolling is sufficient and simpler; adding pagination would be unnecessary complexity per the design philosophy.
- **Status/zebra styling:** Alternating row shading (subtle) for readability on long lists; selected row uses a clear highlight; hover state (mouse-over row tint) aids scanning with a mouse, though it is secondary to keyboard navigation.

## 6.6 Search & Filter UX

- **Search field behavior:** Live filtering with a short debounce (~300ms) as the user types — grid updates without a separate "Search" button click, satisfying FR-SRCH-005 ("update displayed data without restarting the application").
- **Filter controls:** ComboBoxes for categorical filters (Category, Supplier, Status, Transaction Type); DateTimePicker pairs for date-range filters. All filters combine with AND logic with the search box.
- **Clear filters:** A single "Clear Filters" link/button appears once any filter or search text is active, resetting the view to the unfiltered list in one click.
- **Reset behavior:** Navigating away and back to a list screen resets filters to default (no filter persistence across navigation, to keep behavior predictable and avoid "why is this filtered?" confusion for a small-business, non-technical audience).
- **No-results state:** See 6.8.

## 6.7 Validation UX

Validation happens at three points, per SRS §11 general behavior:
1. **During input** — live checks where meaningful (numeric fields reject non-numeric keystrokes; Stock-Out quantity checks against available stock as it's typed).
2. **Before submission** — full-form validation runs on Save; first invalid field receives focus.
3. **At the business-rule level** — server/data-layer checks (uniqueness, referential rules) run on Save and can still surface an error even if client-side checks passed (e.g., a duplicate category name created in another session moments earlier).

**Presentation:** Inline messages beneath each field (not MessageBox-only, per SRS §15 "Do not rely only on MessageBoxes") — an ErrorProvider-style icon + tooltip pattern is acceptable and recommended alongside the inline text for WinForms. MessageBox is reserved for whole-form/business-rule failures that don't map to one field (e.g., "Unable to connect to the database").

**Coverage (from SRS §11, VAL-001–022):**
- Required-field emptiness → "This field is required."
- Numeric fields → "Enter a valid number." / range-specific messages (e.g., "Price must be zero or greater.")
- Negative or zero quantity where ≥1 is required → "Enter a quantity of at least 1."
- Insufficient stock → "Insufficient stock. Available: {X}."
- Invalid email → "Enter a valid email address."
- Invalid phone → "Phone number can only contain digits, spaces, +, -, and parentheses."
- Invalid/future date → "Date cannot be in the future."
- Duplicate values (username, category name) → "{Value} is already in use."
- Required ComboBox selection missing → "Please make a selection."

## 6.8 Empty States

Every list/table defines what's empty, why, and what to do next:

| Screen | Empty (no data at all) | Empty (filtered / searched, no matches) |
|---|---|---|
| Product List | "No products yet. Add your first product to get started." + Add button for Admin | "No products match your search or filters." + Clear Filters |
| Category / Supplier / Customer List | "No {entity} yet. Add your first {entity} to get started." + Add button for Admin | "No {entity} match your search." + Clear Filters |
| Inventory History | "No stock movements recorded yet." | "No transactions match your filters." + Clear Filters |
| Dashboard Recent Activity | "No recent activity yet — record your first Stock-In to get started." + link to Stock-In | — |
| Reports | "No records found for the selected filters." (report body area) | — |
| Low-Stock views | "Nothing is low on stock right now." (a positive, reassuring empty state — this is good news, styled neutrally/positively, not as a warning) | — |

## 6.9 Feedback States

| Type | When | Presentation |
|---|---|---|
| **Success** | Save, Delete, Deactivate, Stock-In/Out, Adjustment completed | Transient status message (StatusStrip or toast-style banner), green accent, auto-dismisses after a few seconds; also reflected immediately in the affected list/grid |
| **Warning** | Low-stock condition, blocked-delete explanation, approaching a risky action | Amber accent; persistent until acknowledged where it precedes a decision (e.g., blocked-delete dialog), transient where purely informational (e.g., dashboard tile) |
| **Error** | Validation failure, business-rule rejection, system/database error | Red accent; inline for field-level, MessageBox for form/system-level; does not auto-dismiss — user must correct or acknowledge |
| **Information** | Neutral status (e.g., "New quantity on hand will be: 42") | Neutral/blue accent, inline, non-blocking |

Feedback style is identical across every module — the same colors, icon set, and placement conventions apply whether the action is on Products, Stock-In, or User Management.

## 6.10 Error UX by Category

| Category | Example | Presentation |
|---|---|---|
| **Validation Errors** | Empty required field, invalid number | Inline, field-level, non-blocking to the rest of the form |
| **Business Rule Errors** | Insufficient stock, blocked delete, duplicate name | Inline (stock/duplicate) or dedicated explanation dialog (blocked delete) |
| **Database Errors** | Connection failure | MessageBox: "Unable to connect to the database. Please contact the administrator." Technical detail logged, never shown to the user (ER-007). |
| **Unexpected Errors** | Unhandled exception | MessageBox: "An unexpected error occurred. Please try again." Technical detail logged only (ER-010). Application does not crash (NFR-006). |
| **Permission Errors** | Restricted action somehow triggered | MessageBox: "You do not have permission to perform this action." (ER-011) — prevention (disable/hide the control) is always preferred over this reactive message. |

**Principle:** Stack traces, SQL error text, and exception messages are never shown to end users, under any circumstance (SRS §12 principles, NFR-006/007).

## 6.11 Confirmation Dialogs

Required before: Delete, Deactivate (Product/Category/Supplier/Customer/User), Stock-Out (reduces stock — operationally significant), Inventory Adjustment (directly overrides stock), Logout when unsaved work exists on an open form.

**Not required for:** Stock-In (a purely additive, low-risk, high-frequency action — requiring confirmation here would violate "minimize unnecessary clicks" for the single most-repeated task in the app; the live "New quantity will be: X" preview before Save already gives the user a chance to catch mistakes), ordinary navigation, Save on Add/Edit forms (the Save action itself is the confirmation).

**Dialog pattern:** Title states the action; body states the specific consequence in plain language (never just "Are you sure?"); two buttons only (Cancel left/secondary, confirming action right/primary, using the specific verb — "Delete," "Deactivate," "Issue Stock," "Save Adjustment" — never a generic "OK"). Destructive confirmations use the danger/red button style for the confirming action.
