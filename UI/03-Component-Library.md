# 3. Component Library

Every component below is defined across its applicable states (normal, hover, focus, pressed, disabled, error) and references the token names from `02-Design-Tokens.md`. All components appear, rendered, in `mockups/*.html`.

## 3.1 Buttons

| Variant | Fill | Text | Border | Usage |
|---|---|---|---|---|
| Primary | `primary` | `#FFFFFF` | none | Save, Login, Generate, Record Stock-In/Out, the one primary action per screen |
| Secondary | `surface` | `primary` | 1px `primary` | Cancel, Back, Clear Form |
| Danger | `error` | `#FFFFFF` | none | The confirming button only, in Delete/Deactivate/high-risk-Adjustment confirmation dialogs |
| Ghost/Text | transparent | `primary` | none | Clear Filters, "Mark Inactive" inline actions, "View all" links |

**States:**

| State | Primary | Secondary | Danger |
|---|---|---|---|
| Normal | `primary` fill | white fill, `primary` border/text | `error` fill |
| Hover | `primaryHover` fill (~12% darker) | `#F1F6FC` fill | `#A5301F` fill (~12% darker) |
| Pressed | fill darkens further (no shadow/inset trick — flat darker fill only) | fill darkens further | fill darkens further |
| Focus (keyboard) | 2px `borderFocus` outline, offset 2px, in addition to the fill state | same | same |
| Disabled | `disabledBackground` fill, `disabledText` text, no hover response | same tokens | same tokens |

Button label terminology is fixed system-wide (never varies by screen): **Save** (never Submit/OK), **Cancel** (never Close on a form with unsaved data), **Delete** (never Remove), **Print** (never Export — export doesn't exist in MVP), and the confirming button in a confirmation dialog always uses the specific verb for that action ("Delete," "Deactivate," "Issue Stock," "Save Adjustment") — never a bare "OK."

## 3.2 Text Fields

| State | Border | Fill | Notes |
|---|---|---|---|
| Normal | 1px `border` | `surface` | |
| Focus | 2px `borderFocus` | `surface` | Padding reduces by 1px to compensate for the thicker border so field height never shifts |
| Error | 2px `error` | `surface` | Always paired with an inline message directly beneath the field |
| Disabled | 1px `border` | `disabledBackground` | Text uses `disabledText`; used for QuantityOnHand, Username-on-Edit, Previous Quantity, etc. |
| Hover (non-focused) | 1px `border` (unchanged) | `surface` | Text fields do not change on hover — only focus is visually distinct, to keep the interaction model simple and match native WinForms TextBox behavior |

Labels are always persistent (never placeholder-only). Placeholder text, where used, is example/guidance text inside an otherwise-labeled field (e.g., "e.g. stock count correction, damaged goods, theft" inside the Adjustment Reason field) — it disappears on typing and is never the field's only identifier.

## 3.3 Password Fields

Same visual spec as Text Fields (§3.2), masked character rendering (WinForms `PasswordChar`). Login's password field clears entirely on an authentication error (per SRS ER-001 pattern) rather than retaining the failed value. User creation shows Password + Confirm Password as a pair; User edit replaces the always-visible password field with a separate "Reset Password" ghost-button action (per `product-requirements/09 §9.5`).

## 3.4 Numeric Inputs

Same visual spec as Text Fields. Behaviorally: reject non-numeric keystrokes at entry time (WinForms `KeyPress` filtering) rather than only validating after the fact — this is the "prevent errors before they happen" principle applied to the input level. Quantity fields on Stock-Out show live validation against available stock as digits are typed (VAL-020) — the field switches to the Error state the instant the typed quantity exceeds what's available, before Save is even attempted (see `mockups/stock-out.html` for the rendered example).

## 3.5 ComboBoxes

Visually identical to Text Fields with a trailing chevron-down icon (muted text color, 14px, vertically centered, right-inset 10px). Dropdown list rows use `body` typography on `surface` background; hover uses `rowHover`; selected uses `rowSelected`. Populated only with **active** records (e.g., Product's Category/Supplier ComboBoxes list only active Categories/Suppliers) per the entity CRUD rules. Where a ComboBox supports type-to-search (Product selector on Stock-In/Out/Adjustment), typed text filters the dropdown list live rather than requiring exact-match selection.

## 3.6 DateTimePickers

Visually a Text Field with a trailing calendar icon (muted text color, 14px). Displayed value uses `dd MMM yyyy` format (e.g., "19 Aug 2026") for unambiguous scanning by non-technical staff. Defaults to today's date on Stock-In/Stock-Out; future dates are rejected at the control level (WinForms `MaxDate = DateTime.Today`) rather than only via a post-submit validation message, consistent with "prevent errors before they happen."

## 3.7 Checkboxes

Standard WinForms CheckBox visual: 16×16px box, 1px `border`, `primary` fill with a white check glyph when checked, `borderFocus` outline ring when focused via keyboard. Used for: Active status on Edit forms (Product, Category, Supplier, Customer, User).

## 3.8 Radio Buttons

Not used anywhere in the MVP screen set — every choice in the approved specification is either a single toggle (CheckBox) or a selection from more than two options (ComboBox). No screen in `03-Screen-Inventory.md` requires a mutually-exclusive 2–4-option inline choice, so no radio-button pattern is defined. If a future requirement introduces one, it should use the same border/fill/focus token behavior as Checkboxes (circular instead of square).

## 3.9 DataGridView (Table)

See `02-Design-Tokens.md §2.7` for dimensions. Full behavioral spec:

| Element | Spec |
|---|---|
| Header | `background` fill, `text` color, Table Header typography (11px/600), 1px bottom border in `border`; sortable columns show a subtle sort-direction chevron once active; click anywhere on the header cell toggles sort |
| Row (odd) | `surface` (`#FFFFFF`) fill |
| Row (even) | `#FAFBFC` fill (subtle zebra) |
| Row hover | `rowHover` fill |
| Row selected | `rowSelected` fill + 2–3px left accent bar in `primary` (selection is never color-fill-only, consistent with the "not color alone" rule extended to interaction states) |
| Numeric/currency columns | Right-aligned, tabular-number rendering so digits line up vertically down the column |
| Status column | Rendered as a pill badge (§3.10), never plain colored text and never a whole colored row |
| Action column | Always rightmost, fixed-width, icon-only with tooltips, order: View (if applicable) → Edit → Delete for Administrator; View only for Staff on master-data grids; **entirely absent** on Inventory History (append-only, BR-030) |
| Alignment | Identity columns (ID, Name) leftmost and fill-weighted; supporting columns (Category, Supplier, dates) fixed-width; Status and Actions fixed-width |
| Column resizing | ID/Status/Actions fixed-width; Name/Description fill-weighted — grid remains usable from 1024×768 up |
| Empty state | See `06-Dialogs-and-States.md §6.4` |
| Loading state | A lightweight "Loading…" label/overlay replaces the grid body briefly; the grid never flashes an empty/no-records state during a real load |
| Pagination | Not used — standard scrolling only (see `01-Visual-Design-Overview.md §1.3`) |

## 3.10 Status / Badge Indicators

The single most important recurring component in the application. Per BR-015 and the "never color alone" accessibility rule, every status is rendered as **icon + label + color**, together, as a pill badge — never as plain colored text, a colored cell background, or a colored row.

| Status | Icon | Label | Color | Badge fill |
|---|---|---|---|---|
| In Stock | check-circle | "In Stock" | `success` | `successTint` |
| Low Stock | alert-triangle | "Low Stock" | `warning` | `warningTint` |
| Out of Stock | x-circle | "Out of Stock" | `error` | `errorTint` |
| Active | check-circle | "Active" | `success` | `successTint` |
| Inactive | x-circle | "Inactive" | `disabledText` | `disabledBackground` |

Rendered identically everywhere status appears: Product List, Product Details, Dashboard tiles, Stock-Out product selector, Current Inventory / Low Stock / Out-of-Stock reports (BR-015, FR-STK-001–002; PR-002, UI-005).

## 3.11 Dialogs

Two dialog shapes cover every use in the specification:

**A. Add/Edit dialog** (modal, 560–620px wide): title states action + entity; grouped fields with section eyebrows in `secondary` color (used once a form exceeds ~6 fields — Product, User, Stock-In, Stock-Out, Adjustment; simple flat layout otherwise for Category/Supplier/Customer); Cancel (secondary, left) and Save (primary, right) fixed at the bottom; Esc closes/cancels; close (×) top-right behaves identically to Cancel. See `mockups/product-form.html`, `supplier-management.html`, `user-management.html`.

**B. Confirmation dialog** (modal, 440px, `modal-sm`): a colored icon roundel (warn/danger/info per the action's risk level) beside a bold single-sentence statement of the specific consequence — never a bare "Are you sure?" — optionally followed by a compact before/after "facts" block for high-stakes actions (Stock-In/Out preview, Adjustment before/after/reason). Two buttons only: Cancel (secondary, left), confirming action (primary or danger per risk level, right) labeled with the specific verb. See `mockups/stock-in.html`, `inventory-adjustment.html`, `category-management.html` (Blocked-Delete variant).

Modal dialogs never offer maximize/minimize — close (Esc / × / Cancel) only. They are centered on the main window and are not resizable (field count is static), except where noted in `08-WinForms-Implementation-Guidelines.md`.

## 3.12 Iconography Reference

| Icon | Used for |
|---|---|
| grid | Dashboard nav |
| box | Products nav, app logomark, "product" concept generally |
| tag | Categories nav |
| truck | Suppliers nav |
| users | Customers nav |
| arrow-down-circle | Stock In nav, Stock-In transaction type badge |
| arrow-up-circle | Stock Out nav, Stock-Out transaction type badge |
| sliders | Inventory Adjustment nav, Adjustment transaction type badge |
| clock | Inventory History nav |
| bar-chart | Reports nav |
| user | User Management nav, login username field, StatusStrip user identity |
| log-out | Sidebar footer logout control |
| search | Search inputs, global product search |
| edit (pencil) | Edit row action |
| trash | Delete row action |
| eye | View row action |
| plus | "+ Add {Entity}" primary actions |
| check-circle / alert-triangle / x-circle | Status badges (§3.10), success/warning/error banners and toasts |
| info | Informational banners/toasts |
| filter | (reserved; filter bars in this spec use labeled ComboBoxes rather than a filter icon-button, per `06-CRUD…§6.6` — icon retained in the library for any future filter-affordance need) |
| printer | Print actions (Inventory History, Reports) |
| calendar | DateTimePicker affordance |
| lock | Login password field, "read-only" banner on Inventory History |
| chevron-down | ComboBox affordance, sortable-column hint |
| chevron-right | Breadcrumbs, "view all" links, Adjustment before→after flow |
| x | Modal close, Clear Filters |
| check | Success confirmations (compact) |

## 3.13 Search Bars

A single reusable component: search icon (16px, muted) + text input, no visible border box beyond the shared 1px/`border` field style, placeholder text stating exactly what's searchable ("Search by name or ID…", "Search by name or phone…"). Always paired with filters where filters exist for that screen (never search-only or filter-only, per `06-CRUD…§6.1`). Live-filters the grid with a ~300ms debounce; no separate "Search" button.

## 3.14 Filters

ComboBoxes for categorical filters (Category, Supplier, Stock Status, Transaction Type), DateTimePicker pairs for date ranges. Combine with the search box using AND logic. A "Clear Filters" ghost-button/link appears only once a filter or search term is active, and resets to the unfiltered default view in one click.

## 3.15 Toolbars

The horizontal region directly above a grid, holding Search + Filters + Clear Filters (list screens) or Filters + Print (Inventory History, Report Viewer). Consistent 12px gap between elements, wraps to a second line gracefully at the 1024px minimum width rather than truncating or hiding a control.

## 3.16 Cards / Panels

1px `border`, `surface` fill, `radius.card` (6px), no shadow. Used for: Dashboard metric tiles, Dashboard Recent Activity / Quick Actions panels, Stock-In/Out/Adjustment form containers, Report cards on the Reports Hub. A `panel-header` region (14/16px padding, bottom border) is used when a card needs its own title distinct from the screen title.

## 3.17 Headers (Screen Header)

See `04-Application-Shell.md §4.3` for the full application header (breadcrumb + screen title + global search + role context). Within a card, a lighter-weight `panel-header` (§3.16) is used for sub-titles (e.g., "Current Inventory Report" inside the Report Viewer card).

## 3.18 Empty States

See `06-Dialogs-and-States.md §6.4` for the full empty-state catalogue (icon, message, and next-step action per screen).
