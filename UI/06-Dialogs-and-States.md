# 6. Dialogs and States

## 6.1 Confirmation Dialogs

**Required before:** Delete, Deactivate (Product/Category/Supplier/Customer/User), Stock-Out, Inventory Adjustment, Logout when unsaved work exists on an open form.

**Not required for:** Stock-In (purely additive, low-risk, highest-frequency task — the live "new quantity will be" preview already gives the user a chance to catch a mistake before Save; requiring confirmation here would violate the "minimize unnecessary clicks" principle for the single most-repeated task in the app), ordinary navigation, or Save on standard Add/Edit forms (the Save action itself is the confirmation).

**Visual pattern** (see `03-Component-Library.md §3.11B`): 440px modal, colored icon roundel (warning-tint for blocked-delete/logout-with-unsaved-work, danger-tint for the confirming step of an actual delete/deactivate/adjustment, info-tint for Stock-In/Stock-Out previews) beside a bold plain-language statement of the specific consequence — never a bare "Are you sure?" — with an optional compact facts block for before/after numbers. Two buttons only, Cancel left/secondary, confirming action right, labeled with the specific verb ("Delete," "Deactivate," "Issue Stock," "Save Adjustment"), using the Danger button style only when the confirming action is itself destructive (Delete/Deactivate) or overrides recorded stock (Adjustment).

| Action | Icon roundel | Confirming button style | Confirming label | Rendered in |
|---|---|---|---|---|
| Delete (unblocked) | danger | Danger | "Delete" | (pattern; not separately mocked — identical shape to Blocked-Delete's sibling state) |
| Stock-In | info | Primary | "Confirm Stock-In" | `mockups/stock-in.html` |
| Stock-Out | info/danger* | Primary/disabled* | "Confirm Stock-Out" | pattern (see §6.3 for the pre-Save error state that supersedes this dialog) |
| Inventory Adjustment | danger | Danger | "Save Adjustment" | `mockups/inventory-adjustment.html` |
| Deactivate User | danger | Danger | "Deactivate" | pattern, `mockups/user-management.html` |
| Logout with unsaved work | warning | Primary | "Log Out" | pattern |

\* Stock-Out's confirmation dialog is only ever reached once the quantity is valid — see §6.3, the insufficient-stock state disables Save entirely before a confirmation dialog would ever appear.

## 6.2 Blocked-Delete Explanation Dialog

Rendered live in `mockups/category-management.html`. Shown instead of the standard confirmation dialog whenever BR-008 (Category), BR-009 (Supplier), BR-010 (Customer), or BR-018 (Product) determines a hard delete would break referential integrity:

- Warning-tint icon roundel (this is an expected, non-error outcome, not a failure).
- Bold statement: "This {entity} can't be deleted yet."
- Plain-language explanation citing the specific blocking count: *"Electronics Accessories" contains 34 product(s). Reassign or remove those products first, or mark this category Inactive instead.*
- The resolving action ("Mark Inactive") is offered **in the same dialog** — the user is never dead-ended (UX-009) — styled as the Primary button since taking it is the recommended, low-risk path forward, not a destructive one.

## 6.3 Insufficient-Stock State (Stock-Out)

Rendered live in `mockups/stock-out.html`. This is not a dialog — it is a live, in-form state that pre-empts the confirmation dialog entirely:

1. The Available-Quantity banner beneath the Product selector switches from info-tint to error-tint the moment the selected product's availability is insufficient for what's about to be entered (or immediately, if the product is already at zero).
2. The Quantity field itself switches to the 2px error border with an inline message: "Insufficient stock. Available: {X}."
3. The Record Stock-Out primary button visibly disables (`disabledBackground`/`disabledText`).

All three signals change together, live, as the user types — before Save is ever attempted (VAL-020, UX-003). This is the single most safety-critical visual state in the application and is deliberately redundant (banner + field + button) rather than relying on any one signal alone.

## 6.4 Empty States

Every list/grid defines what's empty, why, and what to do next — never a bare blank grid.

| Screen | No data at all | Filtered/searched, no matches |
|---|---|---|
| Product List | "No products yet. Add your first product to get started." + Add button (Admin) | "No products match your search or filters." + Clear Filters |
| Category / Supplier / Customer List | "No {entity} yet. Add your first {entity} to get started." + Add button (Admin) | "No {entity} match your search." + Clear Filters |
| Inventory History | "No stock movements recorded yet." | "No transactions match your filters." + Clear Filters |
| Dashboard Recent Activity | "No recent activity yet — record your first Stock-In to get started." + link to Stock-In | — |
| Reports | "No records found for the selected filters." (report body area, not an error) | — |
| Low-Stock views specifically | "Nothing is low on stock right now." — styled neutrally/positively (this is good news), never as a warning despite living in a stock-status context | — |
| User Management | Not designed — unreachable (an Administrator must exist to view this screen) | "No users match your search." + Clear Filters |

Visual treatment: centered icon (muted `border` color, 32–40px), bold 13px message, 11.5px supporting sentence, and — wherever a next step exists — a button/link directly beneath, never merely descriptive text with no path forward (UI-013).

## 6.5 Loading States

A lightweight "Loading…" label/overlay replaces the grid body or metric-tile values briefly on first load or refresh. The grid or tile area never flashes an empty/zero state during a genuine load — that would be indistinguishable from a real empty state and would misinform the user (`06-CRUD…§6.5`).

## 6.6 Validation / Error UX

Validation happens at three points, all visually represented:

1. **During input** — numeric fields reject non-numeric keystrokes at the control level; Stock-Out quantity is checked live against availability (§6.3).
2. **Before submission** — full-form validation on Save; the first invalid field receives focus and its inline message appears.
3. **At the business-rule level** — server/data-layer checks (uniqueness, referential rules) can still surface an error on Save even after client-side checks passed, presented via MessageBox for whole-form/system-level failures that don't map to a single field (e.g., "Unable to connect to the database"), never for anything that does map to a field.

**Presentation:** inline message directly beneath the offending field, in `error` color, replacing any helper text that was there; the field border switches to the 2px error state simultaneously (UI-009). An ErrorProvider-style icon+tooltip may be layered alongside the inline text in the WinForms build (see `08-WinForms-Implementation-Guidelines.md`).

| Category | Example | Presentation |
|---|---|---|
| Required-field emptiness | Empty Product Name | "This field is required." — inline |
| Numeric range | Negative price | "Price must be zero or greater." — inline |
| Quantity floor | Zero/negative Stock-In quantity | "Enter a quantity of at least 1." — inline |
| Insufficient stock | Stock-Out over availability | "Insufficient stock. Available: {X}." — inline, §6.3 |
| Invalid email | Malformed supplier email | "Enter a valid email address." — inline |
| Invalid phone | Malformed phone | "Phone number can only contain digits, spaces, +, -, and parentheses." — inline |
| Future date | Stock-In dated tomorrow | Prevented at the control level (DateTimePicker MaxDate) rather than only messaged after the fact |
| Duplicate values | Existing username/category name | "{Value} is already in use." — inline, surfaced even if it only fails at Save time |
| Missing required selection | No Category chosen | "Please make a selection." — inline |
| Missing Adjustment reason | Empty Reason | "A reason is required for inventory adjustments." — inline, Save disabled until resolved |
| Session/permission | A restricted action is somehow triggered | "You do not have permission to perform this action." (ER-011, Session/Permission Error Dialog) |

## 6.7 Feedback States (Toasts / Banners)

| Type | When | Visual |
|---|---|---|
| Success | Save, Delete, Deactivate, Stock-In/Out, Adjustment completed | Transient StatusStrip message or toast, `success` colored, check-circle icon, auto-dismisses after a few seconds; also reflected immediately in the affected list/grid |
| Warning | Low-stock condition, blocked-delete explanation, approaching a risky action | `warning` colored, alert-triangle icon; persistent until acknowledged where it precedes a decision (blocked-delete dialog), transient where purely informational (dashboard tile) |
| Error | Validation failure, business-rule rejection, system/database error | `error` colored, x-circle icon; inline for field-level, MessageBox for form/system-level; does not auto-dismiss — the user must correct or acknowledge it |
| Information | Neutral status, e.g. "New quantity on hand will be: 168" | `info` colored, info icon; inline, non-blocking |

Feedback style is identical across every module — same colors, same icon set, same placement conventions whether the action is on Products, Stock-In, or User Management (`06-CRUD…§6.9`).

## 6.8 Success States by Flow

Beyond the generic toast pattern (§6.7), several flows have a specific, notable success behavior:

- **Add Product / any master-data Create:** dialog closes, list refreshes, new row briefly highlighted/scrolled into view.
- **Stock-In / Stock-Out:** form clears entirely and refocuses the Product field, so a staff member can immediately begin the next entry without extra navigation (UX-008) — the toast states the resulting on-hand/remaining quantity, not just "Saved."
- **Inventory Adjustment:** on commit, the user is taken directly to Inventory History (SCR-015) so the just-recorded, now-immutable adjustment is visible in context.
- **Delete → Blocked → Mark Inactive:** the row is not removed but re-rendered with the Inactive badge and muted styling, and the success message reads "Marked Inactive" rather than "Deleted," so the outcome the user actually got is stated accurately.
