# 5. Screen Designs

All 19 screens from `product-requirements/03-Screen-Inventory.md` are covered below. Per each screen: Purpose, Layout, Visual Hierarchy, Components + Placement, Primary/Secondary Actions, Data Presentation, Empty/Error/Success States, Permission Differences, and Resizing Behavior. Where a screen shares its pattern with another (list screens; Add/Edit dialogs), the pattern is documented once in full and referenced afterward — the same approach `05-Screen-Specifications.md` itself uses — to keep 19 screens consistent rather than independently reinvented.

---

## 5.1 SCR-001 — Login

**Purpose:** Authenticate and load role. **Mockup:** `mockups/login.html`.

- **Layout:** No shell. Centered 380px card on `background`, vertically and horizontally centered in the viewport.
- **Visual hierarchy:** App mark + name (top) → error banner (only when present) → Username → Password → Login button → build/version footnote (smallest, least prominent element on screen).
- **Components:** Username text field (user icon), Password field (lock icon, masked), Login primary button (full card width), inline error banner.
- **Component placement:** Single column, top-to-bottom, generous 32px card padding so the form doesn't feel cramped despite being the smallest screen in the app.
- **Primary action:** Login (Enter key in the Password field also triggers it).
- **Secondary actions:** None — intentionally minimal per the design philosophy; no "forgot password" self-service flow exists in MVP scope (not in SRS).
- **Data presentation:** N/A (no data grid on this screen).
- **Empty state:** Normal state *is* the empty state — blank fields, focus on Username.
- **Error state:** "Invalid username or password." in the error banner (icon + text, `errorTint` background); password field clears; focus returns to Username. A deactivated account shows the identical generic message — it never reveals which detail was wrong (ER-001 principle).
- **Success state:** Immediate navigation to Dashboard; no confirmation screen needed (arriving at the Dashboard *is* the confirmation).
- **Permission differences:** None — pre-authentication, all users see the identical screen.
- **Resizing behavior:** Card stays fixed-size and centered regardless of window size; background fills any remaining space. Not resizable/maximizable by the user (Login is the one screen where the shell's normal resizing rules don't apply, since there's no content that benefits from more space).

---

## 5.2 SCR-002 — Dashboard

**Purpose:** Orientation hub — "what do we have, what's running low, what just happened." **Mockup:** `mockups/dashboard.html`.

- **Layout:** Header ("Dashboard") → six-tile metric row → two-column region (Recent Activity, wider, left; Quick Actions, narrower, right).
- **Visual hierarchy:** Metric numbers (24px/600, the largest text on the screen) draw the eye first; Recent Activity table is scannable but secondary; Quick Actions are visually prominent (button-styled, not link-styled) because they're the highest-frequency next action.
- **Components:** 6 metric tiles (Total Active Products, Total Categories, Total Suppliers, Total Inventory Quantity, Low-Stock Count, Out-of-Stock Count — the low/out tiles use warning/error colored numbers), Recent Activity grid (Date, Type badge, Product, Quantity with sign-colored styling, User), Quick Actions button stack.
- **Component placement:** Metrics span the full content width in a 6-column row; below, a 1.6:1 two-column split.
- **Primary action:** Contextual — most users land here and immediately choose a Quick Action.
- **Secondary actions:** Clicking a metric tile navigates to the relevant filtered list (e.g., Low-Stock tile → Product List pre-filtered to Low Stock) — flagged in `product-requirements/09 §9.3` as sensible-but-technically-beyond-literal-FR-DASH-wording; included here per that document's recommendation to treat it as in-scope MVP polish. Clicking a Recent Activity row navigates to Inventory History pre-filtered to that date.
- **Data presentation:** Metrics as large numbers with small icon+label captions; Recent Activity as a compact read-only grid (no action column).
- **Empty state:** Brand-new installation — metric tiles show 0; Recent Activity shows "No recent activity yet — record your first Stock-In to get started." with a direct button into Stock-In.
- **Error state:** If metrics can't be computed (database issue), the tile area shows "Unable to load dashboard data" (ER-007 pattern); the rest of the shell remains usable.
- **Success state:** N/A (read-only orientation screen — no save action of its own).
- **Permission differences:** Both roles see the identical metric set and Recent Activity (FR-DASH-002 defines no reduced Staff metric set). Quick Actions differs: Staff see Stock-In, Stock-Out, Search Products only; Administrator additionally sees Inventory Adjustment and Manage Users.
- **Resizing behavior:** Metric row stays 6-across down to 1024px width (tiles compress rather than wrap); two-column region below reflows to stacked single-column only below the 1024px minimum (out of scope, since 1024px is the floor).

---

## 5.3 SCR-003 — Product List *(reference pattern for all master-data list screens)*

**Purpose:** Browse, search, filter products. **Mockup:** `mockups/product-list.html`.

- **Layout:** Header ("Products" + "+ Add Product," Administrator only) → toolbar (search + Category/Supplier/Stock-Status filters + Clear Filters) → DataGridView → footer (row count, sort indicator).
- **Visual hierarchy:** Grid is the dominant element; toolbar is compact and sits quietly above it; Status column uses the pill-badge treatment so stock condition is scannable down the entire list without reading every row.
- **Components:** Search field, 3 filter ComboBoxes, Clear Filters link, DataGridView (Product ID, Name, Category, Default Supplier, Qty on Hand, Min Level, Status badge, Selling Price, Actions), result-count label.
- **Component placement:** Standard list-screen anatomy — see §3.9 for the shared grid spec.
- **Primary action:** "+ Add Product" (Administrator only), top-right of header.
- **Secondary actions:** View/Edit/Delete row actions (Administrator); View only (Staff); Search/Filter/Clear Filters.
- **Data presentation:** One row per product; numeric columns right-aligned; Status as icon+label+color pill; inactive products shown with muted name text and an "Inactive" badge in place of the stock-status badge.
- **Empty state:** No products at all → "No products yet. Add your first product to get started." + Add button (Admin only). No search/filter matches → "No products match your search or filters." + Clear Filters.
- **Error state:** ER-007/ER-010 friendly message with a retry option; grid area only, shell remains usable.
- **Success state:** After Add/Edit/Delete, the list refreshes and the affected row is briefly highlighted/scrolled into view; a StatusStrip success message confirms the action.
- **Permission differences:** Administrator — Add/Edit/Delete visible. Staff — Search/Filter/View only; mutation controls are not rendered (not disabled-and-visible).
- **Resizing behavior:** ID/Status/Actions columns fixed-width; Name/Category/Supplier fill-weighted; grid remains usable at 1024×768 and scales cleanly to larger displays.

---

## 5.4 SCR-004 — Product Details

**Purpose:** Full read view of one product, including calculated status.

- **Layout:** Header (product name + status badge) → two-column read-only field area (left: identity/category/supplier/pricing; right: stock info) → action row (Edit, Delete/Deactivate, Back).
- **Visual hierarchy:** Status badge sits directly beside the product name at the top — the single most important fact about a product is visible before any other field.
- **Components:** Read-only labeled fields (not inputs — plain label/value pairs) for every Product attribute; status badge; Edit/Delete/Back buttons.
- **Component placement:** Two-column layout balances the number of fields without an overly long single column.
- **Primary action:** Edit (Administrator only).
- **Secondary actions:** Delete/Deactivate (Administrator only), Back to Product List.
- **Data presentation:** Plain label/value pairs, not a form — this screen never accepts input.
- **Empty state:** N/A (always represents exactly one existing product).
- **Error state:** "The selected record no longer exists" (ER-008) with a Back action, covering the case where the product was deleted by another session between list and detail load.
- **Success state:** N/A (read-only; success feedback belongs to the Add/Edit and Delete flows that lead here).
- **Permission differences:** Administrator sees Edit/Delete/Deactivate; Staff sees the read-only view and Back only.
- **Resizing behavior:** Two-column field area reflows gracefully; not a grid, so no column-width concerns.

---

## 5.5 SCR-005 — Product Add/Edit *(reference pattern for the CRUD form/dialog itself)*

**Purpose:** Create/update product master data. **Mockup:** `mockups/product-form.html` (shown as a modal over the Product List, Administrator only).

- **Layout:** Modal dialog, 620px. Header (title states action) → grouped field sections (Identity; Category & Supplier; Pricing; Stock Settings) → footer (Cancel left, Save right).
- **Visual hierarchy:** Section eyebrow labels (12.5px uppercase, `secondary` color) break a 9-field form into four digestible groups rather than one long list.
- **Components:** Product Name (required), Description (optional, char counter), Category ComboBox (required, active only), Default Supplier ComboBox (required, active only), Purchase/Selling Price numeric fields (required), Minimum Stock Level (required), Quantity on Hand (**visible but disabled**, "0 (starts at zero)" on Add / current value on Edit, with an explanatory caption), Active checkbox (Edit only).
- **Component placement:** One field or field-pair per row inside each section; required fields carry a red asterisk directly after the label.
- **Primary action:** Save.
- **Secondary actions:** Cancel (discards changes; confirms discard only if the form is dirty).
- **Data presentation:** N/A (a form, not a grid).
- **Empty state:** Add mode — blank form, QuantityOnHand pre-showing "0 (starts at zero)."
- **Error state:** Inline message directly beneath each invalid field (red text + red 2px border), focus moves to the first invalid field, Save remains enabled so the user can immediately retry, and all entered data is preserved (PR-015, UX-004) — the mockup demonstrates a live example on the Selling Price field.
- **Success state:** Dialog closes; parent list refreshes with the new/updated row highlighted; StatusStrip confirms ("Product added successfully" / "Product updated successfully").
- **Permission differences:** Administrator only — Staff never see this dialog.
- **Resizing behavior:** Fixed-size, non-resizable (field count is static), centered on the main window.

---

## 5.6 SCR-006 / SCR-007 — Category List & Add/Edit

**Purpose:** Group products. **Mockup:** `mockups/category-management.html` (includes the Blocked-Delete dialog).

Follows §5.3/§5.5 exactly, scoped to Category, with these differences:

- **Grid columns:** Category ID, Name, Description, Product Count, Status, Actions.
- **Filters:** Status (Active/Inactive) only — no category-specific sub-filter applies.
- **Add/Edit fields:** Name (required, unique, max 100), Description (optional, max 500), Active status — a short, flat (non-sectioned) form since it has fewer than 6 fields.
- **Delete behavior:** If Product Count > 0, hard delete is blocked (BR-008); the Blocked-Delete dialog (§3.11B, §6.6) explains the specific count and offers "Mark as Inactive" as the resolving action in the same dialog.
- **Permission differences:** Identical pattern to Products — Administrator full CRUD, Staff view/search only.
- **Empty/Error/Success states:** Same catalogue as §5.3/§5.5, entity name substituted.

---

## 5.7 SCR-008 / SCR-009 — Supplier List & Add/Edit

**Purpose:** Record suppliers. **Mockup:** `mockups/supplier-management.html` (includes the Add Supplier dialog).

Follows §5.3/§5.5, scoped to Supplier:

- **Grid columns:** Supplier ID, Name, Contact Person, Phone, Email, Status, Actions.
- **Search:** Name only.
- **Add/Edit fields:** Name (required, max 150), Contact Person (optional, max 100), Phone (optional, format-validated), Email (optional, format-validated), Address (optional, max 250).
- **Delete behavior:** Blocked if referenced by any Product or InventoryTransaction (BR-009); same Blocked-Delete → Mark Inactive pattern as Category.
- **Permission/state catalogue:** Identical pattern to §5.3/§5.5.

---

## 5.8 SCR-010 / SCR-011 — Customer List & Add/Edit

**Purpose:** Record customers. **Mockup:** `mockups/customer-management.html`.

Follows §5.3/§5.5, scoped to Customer:

- **Grid columns:** Customer ID, Name, Phone, Email, Status, Actions.
- **Search:** Name or Phone.
- **Add/Edit fields:** Name (required, max 150), Phone (optional, format-validated), Email (optional, format-validated), Address (optional, max 250).
- **Delete behavior:** Blocked if referenced by any Stock-Out transaction (BR-010); same Blocked-Delete → Mark Inactive pattern.
- **Permission/state catalogue:** Identical pattern to §5.3/§5.5. Note the "Walk-in Customer" convention shown in the mockup for anonymous/no-customer Stock-Out transactions.

---

## 5.9 SCR-012 — Stock-In

**Purpose:** Record goods received. **Mockup:** `mockups/stock-in.html`.

- **Layout:** Dedicated screen (not a modal — used repeatedly through a shift), single narrow column (~760px), grouped: Product & Supplier → Movement Details → Notes → action row.
- **Visual hierarchy:** The live "New quantity on hand will be: X" info banner is the visual anchor of the Movement Details group — it's the number the user is there to change, shown as a preview before commit.
- **Components:** Product ComboBox (active only, type-to-search, shows current on-hand once selected), Actual Supplier ComboBox (defaults to the product's default supplier but changeable per BR-028), Quantity (numeric, positive integer), Date (DateTimePicker, today default, no future dates), Unit Price (numeric), Notes (optional, char counter), "Record Stock-In" primary button, "Clear Form" secondary button.
- **Component placement:** Three-field row (Quantity/Date/Unit Price) keeps Movement Details compact; the info banner sits directly below that row.
- **Primary action:** Record Stock-In → confirmation dialog (§3.11B) stating quantity, product, supplier, and the resulting new on-hand quantity → commits on Yes.
- **Secondary actions:** Clear Form (repeat-entry convenience, unique to Stock-In/Out).
- **Data presentation:** N/A (form).
- **Empty state:** Blank form on screen entry/after reset, focus returns to Product for rapid repeated entry.
- **Error state:** Standard inline validation; if the selected product becomes inactive mid-session, save is blocked with "This product is now inactive."
- **Success state:** Confirmation dialog → commit → form clears, refocuses Product, StatusStrip toast confirms, new on-hand quantity stated in the toast.
- **Permission differences:** Both Administrator and Staff have full access — this is a shared operational task, not a master-data mutation.
- **Resizing behavior:** Content column stays at its comfortable reading width and centers within the available content region; does not stretch full-width on large displays (long form rows would hurt scanability).

---

## 5.10 SCR-013 — Stock-Out

**Purpose:** Record goods issued; make insufficient stock impossible to miss. **Mockup:** `mockups/stock-out.html` (shown in its insufficient-stock error state).

- **Layout:** Same structural pattern as Stock-In (§5.9). Groups: Product & Customer → Movement Details → Notes.
- **Visual hierarchy:** The Available-Quantity banner sits immediately below the Product selector, in the same visual weight class as an error banner even when stock is sufficient (info-colored when fine, error-colored the instant it's insufficient) — this is deliberate: the user should register "how much is available" as part of selecting the product, before they've even typed a quantity.
- **Components:** Product ComboBox (active only, shows Available Quantity immediately below on selection), Customer ComboBox (**optional**, "No customer selected" default, no asterisk), Quantity (numeric, live-validated against availability), Date, Selling Price (pre-filled from the product's current price, editable), Notes, "Record Stock-Out" primary button, "Clear Form" secondary.
- **Component placement:** Identical grouping rhythm to Stock-In for muscle-memory consistency between the two most-repeated screens in the app.
- **Primary action:** Record Stock-Out → confirmation dialog stating quantity, product, customer (or "no customer"), and remaining stock → commits on Yes.
- **Secondary actions:** Clear Form.
- **Data presentation:** N/A (form).
- **Empty state:** Blank form on entry/after reset.
- **Error state (the screen's defining state):** The instant entered quantity exceeds availability — Quantity field switches to the 2px error border, an inline message reads "Insufficient stock. Available: {X}." directly beneath it, the Available-Quantity banner switches to error coloring, and the primary Save button visibly disables (`disabledBackground`/`disabledText`, no hover response) *before* the user attempts to submit. All three signals (field, banner, button) change together so the state can't be missed even at a glance. If the selected product is already at zero, this state is the default the moment it's chosen — see the mockup.
- **Success state:** Confirmation dialog → commit → form clears, StatusStrip toast states remaining stock.
- **Permission differences:** Both roles have full access, same as Stock-In.
- **Resizing behavior:** Same as Stock-In.

---

## 5.11 SCR-014 — Inventory Adjustment (Administrator only)

**Purpose:** Correct stock quantity with a mandatory reason — the single highest-risk action in the system. **Mockup:** `mockups/inventory-adjustment.html`.

- **Layout:** Dedicated screen, single narrow column. A persistent warning banner ("Administrator-only action…") sits above the form as a constant reminder of the screen's stakes, in addition to the sidebar item being hidden entirely for Staff.
- **Visual hierarchy:** Previous Quantity → New Quantity → Difference is laid out as an explicit left-to-right flow with chevron separators, so the before/after relationship reads instantly rather than requiring the user to mentally subtract two disconnected fields.
- **Components:** Product ComboBox (active only), Previous Quantity (read-only, auto-populated), New Quantity (required numeric), Difference (read-only, computed live, colored green for a positive difference / red for negative / neutral for zero), Reason (required multiline, max 250, example placeholder text), "Save Adjustment" primary button.
- **Component placement:** Quantity Correction is the visual centerpiece; Reason sits below as its own section since it's mandatory and easy to under-weight visually if placed as "just another field."
- **Primary action:** Save Adjustment → confirmation dialog (danger-colored icon roundel, reflecting this being the one confirmation in the app that uses the Danger button style for a non-delete action) stating the specific before/after quantities and the reason, with an explicit "cannot be undone" line → commits on Yes.
- **Secondary actions:** Cancel.
- **Data presentation:** N/A (form).
- **Empty state:** Blank form; Difference shows "0" (neutral) until a product and new quantity are set.
- **Error state:** "A reason is required for inventory adjustments." shown inline if Reason is left empty; Save is disabled until it's populated (in addition to standard field validation).
- **Success state:** Confirmation → commit → append-only Adjustment transaction recorded (Previous, New, Difference, Reason, User, Date all captured) → StatusStrip confirms → user is taken to Inventory History (SCR-015) so the just-recorded adjustment is immediately visible, per `04-User-Flows.md §4.10`.
- **Permission differences:** Administrator only. This screen and its sidebar entry are **entirely hidden**, not merely disabled, for Inventory/Store Staff — enforced at both the navigation layer (§4.2) and, defensively, at this screen's own entry point.
- **Resizing behavior:** Same narrow-column behavior as Stock-In/Out.

---

## 5.12 SCR-015 — Inventory Transaction History

**Purpose:** Read-only, filterable log of every stock movement. **Mockup:** `mockups/inventory-history.html`.

- **Layout:** A persistent info banner ("Read-only record…") sits above the filter toolbar as a standing reminder, in addition to the structural fact that this is the only grid in the application with **no Actions column at all**. Header → banner → filter toolbar (Date Range, Type, Product) + Print → DataGridView → (no footer actions beyond row count).
- **Visual hierarchy:** Quantity column uses the same sign-colored convention as the Dashboard's Recent Activity (green "+" for Stock-In, red "−" for Stock-Out/negative Adjustments) so movement direction is scannable without reading the Type column.
- **Components:** Date Range DateTimePicker pair, Transaction Type ComboBox (All/Stock-In/Stock-Out/Adjustment), Product ComboBox (type-to-search, optional), Print button, DataGridView (Transaction ID, Date, Type badge, Product, Quantity with signed styling, Supplier/Customer — contextual to type, User, Notes).
- **Component placement:** Standard list-screen anatomy minus the Actions column and minus any "+ Add" affordance — there is nothing to create here.
- **Primary action:** Apply filters (implicit — grid updates live/on Apply per the filter model).
- **Secondary actions:** Print (opens a standard Windows print dialog using the current filtered view); Clear Filters.
- **Data presentation:** One row per immutable transaction, most-recent-first by default.
- **Empty state:** No transactions at all → "No stock movements recorded yet." No filter matches → "No transactions match your filters." + Clear Filters.
- **Error state:** ER-007/ER-010 pattern, same as other grids.
- **Success state:** N/A — this screen has no save action; it is the destination other screens redirect to after a successful Stock-In/Out/Adjustment.
- **Permission differences:** Both roles view identically — the append-only nature of this data (BR-030) is a role-independent, absolute rule, not a Staff-specific restriction. **No row, for any role, ever offers Edit or Delete.**
- **Resizing behavior:** Standard grid resizing rules (§3.9); Notes column is the fill-weighted column here since it's the most variable-length content.

---

## 5.13 SCR-016 / SCR-017 — Reports Hub & Report Viewer

**Purpose:** Select and view/print the six MVP reports. **Mockup:** `mockups/reports.html` (Hub and Viewer shown together on one scrollable page for review purposes; in the real application SCR-017 is a separate, larger, resizable window per `02 §2.3`).

- **Layout (Hub):** A 3-column grid of report-type cards (icon + name + one-line description), each admin-only-flagged where applicable; selecting a card reveals that report's specific filters inline, followed by "Generate."
- **Layout (Viewer):** Header (report title + active-filter summary) → tabular report body → footer (Print, Back to Reports — filters preserved so the user can adjust and regenerate without starting over).
- **Visual hierarchy:** Report cards use the same card component as Dashboard tiles/panels for visual family resemblance; the currently-selected report card is highlighted with a `primary`-tinted border and background.
- **Components:** 6 report-type cards (Current Inventory, Low Stock, Out-of-Stock, Stock-In, Stock-Out, Transaction History — filters and columns exactly as specified in `05-Screen-Specifications.md §5.13`'s per-report table), Generate button, Report Viewer grid, Print button, Back button.
- **Component placement:** Cards above, viewer below (or as a full-screen replacement in the real windowed implementation).
- **Primary action:** Generate (Hub); Print (Viewer).
- **Secondary actions:** Back to Reports (Viewer, preserves filters).
- **Data presentation:** Report-type-specific columns exactly as defined in the PRD's per-report table — no speculative extra columns added here.
- **Empty state:** "No records found for the selected filters." in the viewer body — explicitly not styled as an error.
- **Error state:** ER-007/ER-010 pattern for a data-load failure.
- **Success state:** N/A beyond the populated report itself.
- **Permission differences:** Administrator sees all 6 report cards; Staff sees only Current Inventory, Low Stock, and Transaction History (the three FR-RPT/§6.2.2-approved reports for that role) — the other three cards are admin-only-flagged and not rendered for Staff.
- **Resizing behavior:** The Report Viewer is the one window in the application explicitly called out as resizable and opening large/maximized by default (`02 §2.3`), since reports benefit from more visible rows/columns than the standard content region.

---

## 5.14 SCR-018 / SCR-019 — User Management List & Add/Edit (Administrator only)

**Purpose:** Manage accounts and roles. **Mockup:** `mockups/user-management.html`.

- **Layout (List):** Header ("+ Add User") → search bar → DataGridView (Username, Full Name, Role badge, Status, Actions) → footer.
- **Layout (Add/Edit):** Modal dialog, same reference pattern as §5.5, flat (no section grouping needed at 4–5 fields).
- **Visual hierarchy:** Role is rendered as a distinct badge for Administrator (info-tinted, since it's a privilege signal worth calling out) versus a plain tag pill for Inventory Staff — administrators should be able to spot who else has elevated access at a glance down the list.
- **Components:** Username (required, unique, max 50, **disabled on Edit** — usernames are permanent to protect transaction-history attribution), Password + Confirm Password (Create only, min 6 chars) / a separate "Reset Password" ghost-button action (Edit only, per `09 §9.5`), Full Name (required), Role ComboBox (Administrator / Inventory Staff), Active checkbox (Edit).
- **Component placement:** Standard modal pattern.
- **Primary action:** Save (list header "+ Add User"; dialog "Save").
- **Secondary actions:** Edit and Deactivate row actions.
- **Data presentation:** One row per user account.
- **Empty state:** Not realistically reachable (at least one Administrator must always exist to have logged in), so no empty-state message is designed for this screen.
- **Error state:** "This username is already in use." (duplicate, BR-019); standard required-field messaging (VAL-016, VAL-017 for password length).
- **Success state:** "User created/updated/deactivated successfully"; list refreshes.
- **Permission differences:** Administrator only — this entire module (sidebar entry included) is hidden, not disabled, for Staff.
- **Special behavior:** On the Administrator's own row, the Deactivate icon is rendered in the disabled visual state with an explanatory tooltip ("You cannot deactivate your own account while logged in") — **prevented at the control level** rather than attempted-then-blocked (BR-020), exactly as shown in the mockup.
- **Resizing behavior:** Standard list/modal resizing rules, same as other master-data screens.

---

## 5.15 Shared / Reusable Modal Components (Not Separately Numbered Screens)

Per `03-Screen-Inventory.md §3.1`, these are modal components referenced across the screens above rather than distinct navigable screens. Their full visual spec is in `03-Component-Library.md §3.11` and `06-Dialogs-and-States.md`:

- **Confirmation Dialog** — generic pattern for Delete, Deactivate, Stock-Out, Adjustment, Logout-with-unsaved-changes.
- **Blocked-Delete Explanation Dialog** — BR-008/009/010/018, shown in `mockups/category-management.html`.
- **Session/Permission Error Dialog** — ER-011, "You do not have permission to perform this action."
