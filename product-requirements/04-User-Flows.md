# 4. UX Flows

Each flow follows: **START → USER ACTION → SYSTEM RESPONSE → NEXT ACTION → SUCCESS / ERROR**

## 4.1 Login (SCR-001)
- **START:** Application launches; Login screen shown.
- **USER ACTION:** Enters username + password → clicks Login.
- **SYSTEM RESPONSE:** Validates non-empty fields → authenticates against stored credentials → checks IsActive.
- **NEXT ACTION:** On success, loads role, applies role-based UI, opens Dashboard.
- **SUCCESS:** Dashboard displayed, StatusStrip shows user name/role.
- **ERROR:** "Invalid username or password." Fields remain, password cleared, focus returns to username. Deactivated account → same generic message (does not reveal deactivation status, per ER-001 principle of not revealing which detail is wrong).

## 4.2 Add Product (SCR-003 → SCR-005)

**ROLE:** Administrator only.
- **START:** User on Product List clicks "Add Product."
- **USER ACTION:** Fills Name, Category (ComboBox), Default Supplier (ComboBox), Description, Purchase/Selling Price, Minimum Stock Level → clicks Save.
- **SYSTEM RESPONSE:** Validates required fields, numeric ranges (VAL-002,003,009,012), Category/Supplier selection made. QuantityOnHand is **not shown as an input** — informational "starts at 0" note shown instead.
- **NEXT ACTION:** On valid save, product created with QuantityOnHand = 0, no transaction created (BR-026).
- **SUCCESS:** Confirmation message ("Product added successfully"), dialog closes, Product List refreshes and highlights the new row.
- **ERROR:** Inline field messages + focus moves to first invalid field; save blocked; dialog remains open with entered data preserved.

## 4.3 Edit Product (SCR-003/004 → SCR-005)

**ROLE:** Administrator only.
- **START:** User selects a product, clicks Edit.
- **USER ACTION:** Changes any editable field (Name, Category, Supplier, Description, Prices, Minimum Stock Level, Active status) → Save. QuantityOnHand field is visible but **read-only/disabled**.
- **SYSTEM RESPONSE:** Same validation as Add; re-checks uniqueness constraints where relevant.
- **NEXT ACTION:** Valid change saved, ModifiedDate/ModifiedByUserID updated.
- **SUCCESS:** "Product updated successfully"; list/detail refreshes with new values.
- **ERROR:** Same inline pattern as Add Product.

## 4.4 Deactivate / Delete Product (SCR-003/004)

**ROLE:** Administrator only.
- **START:** User selects a product, clicks Delete.
- **USER ACTION:** Confirms intent in the confirmation dialog.
- **SYSTEM RESPONSE:** System checks for related InventoryTransaction records (BR-018).
- **NEXT ACTION:**
  - **If zero related transactions:** hard delete proceeds after confirmation.
  - **If related transactions exist:** delete is blocked; a Blocked-Delete dialog explains why and offers "Mark as Inactive" instead.
- **SUCCESS:** Either "Product deleted" or "Product marked Inactive," list refreshes, row removed or shown greyed-out/Inactive-tagged.
- **ERROR:** N/A beyond the blocked-delete explanation, which is itself the correct/expected outcome, not a failure state.

## 4.5 Add Category (SCR-006 → SCR-007)

**ROLE:** Administrator only.
- **START:** Admin on Category List clicks "Add Category."
- **USER ACTION:** Enters Name (required, unique), Description (optional) → Save.
- **SYSTEM RESPONSE:** Validates required, max length, uniqueness (BR-007, VAL-004, VAL-022).
- **NEXT ACTION:** Category created, IsActive = true by default.
- **SUCCESS:** "Category added successfully"; list refreshes.
- **ERROR:** "A category with this name already exists" (duplicate) or standard required-field message.

## 4.6 Add Supplier (SCR-008 → SCR-009)

**ROLE:** Administrator only.
- **START:** Admin on Supplier List clicks "Add Supplier."
- **USER ACTION:** Enters Name (required), Contact Person, Phone, Email, Address (all optional except Name) → Save.
- **SYSTEM RESPONSE:** Validates required Name, email format (VAL-013) and phone format (VAL-014) if provided.
- **NEXT ACTION:** Supplier created.
- **SUCCESS:** "Supplier added successfully"; list refreshes; supplier now selectable in Product and Stock-In ComboBoxes.
- **ERROR:** Field-specific validation messages (e.g., "Enter a valid email address").

## 4.7 Add Customer (SCR-010 → SCR-011)

**ROLE:** Administrator only.
- **START:** Admin on Customer List clicks "Add Customer."
- **USER ACTION:** Enters Name (required), Phone, Email, Address (optional) → Save.
- **SYSTEM RESPONSE:** Same validation pattern as Supplier.
- **NEXT ACTION:** Customer created.
- **SUCCESS:** "Customer added successfully"; list refreshes; customer now selectable (optionally) in Stock-Out.
- **ERROR:** Field-specific validation messages.

## 4.8 Receive Stock / Stock-In (SCR-012)
- **START:** User opens Stock In.
- **USER ACTION:** Selects an active Product; selects the actual Supplier for this receipt (may differ from the product's default supplier, BR-028); enters Quantity (positive integer), Date (defaults to today, cannot be future), Unit Price, optional Notes → Save.
- **SYSTEM RESPONSE:** Validates all fields (VAL-009,010,015); confirms product is active (BR-016).
- **NEXT ACTION:** On valid save, user is shown a confirmation prompt (this increases stock, an operational action) before commit.
- **SUCCESS:** Append-only transaction created; QuantityOnHand increases by Quantity; Stock Status recalculated immediately; success message shows new on-hand quantity; form resets for the next entry (staff often enter several receipts in a row).
- **ERROR:** Validation errors block save inline; attempting an inactive product shows "This product is inactive and cannot receive stock."

## 4.9 Issue Stock / Stock-Out (SCR-013)
- **START:** User opens Stock Out.
- **USER ACTION:** Selects an active Product (available quantity is shown immediately); optionally selects a Customer; enters Quantity, Date, Selling Price, optional Notes → Save.
- **SYSTEM RESPONSE:** Validates fields; checks Quantity ≤ current QuantityOnHand (BR-011, VAL-020) in real time as the quantity is entered, before Save is even pressed where practical.
- **NEXT ACTION:** Confirmation prompt before commit (stock-reducing action).
- **SUCCESS:** Transaction created; QuantityOnHand decreases; Stock Status recalculated; success message; form resets.
- **ERROR:** "Insufficient stock. Available: {X}." Save blocked; quantity field highlighted; user can reduce quantity or cancel.

## 4.10 Adjust Inventory (SCR-014, Administrator only)
- **START:** Administrator opens Inventory Adjustment.
- **USER ACTION:** Selects an active Product; system displays current (Previous) Quantity read-only; Administrator enters New Quantity and mandatory Reason → Save.
- **SYSTEM RESPONSE:** Validates New Quantity ≥ 0 (integer), Reason is non-empty (BR-013, VAL-018); computes and displays Difference before commit.
- **NEXT ACTION:** Confirmation prompt clearly stating the before/after quantities and the reason, since this directly overrides stock (high-stakes action).
- **SUCCESS:** Append-only Adjustment transaction recorded with Previous Quantity, New Quantity, Difference, Reason, User, Date; QuantityOnHand set to New Quantity; success message.
- **ERROR:** Standard validation messaging; Reason left empty is specifically called out ("A reason is required for inventory adjustments.").

## 4.11 Search Inventory (SCR-003)
- **START:** User on Product List.
- **USER ACTION:** Types in the search box (Name or Product ID) and/or selects Category / Supplier / Stock Status filters.
- **SYSTEM RESPONSE:** Grid updates live (debounced) without restarting the application (FR-SRCH-005); result count shown.
- **NEXT ACTION:** User can refine further or click "Clear Filters."
- **SUCCESS:** Matching rows shown; if none match, empty-state message shown (see document 06).
- **ERROR:** N/A — search never "fails," it only returns zero results, which is an empty state, not an error.

## 4.12 View Inventory History (SCR-015)
- **START:** User opens Inventory History.
- **USER ACTION:** Optionally sets Date Range, Transaction Type, and/or Product filters → Apply.
- **SYSTEM RESPONSE:** Displays matching immutable transaction records; no row offers Edit/Delete (BR-030 enforced at the UI level — action column is absent entirely for this grid).
- **NEXT ACTION:** User may Print or adjust filters.
- **SUCCESS:** Filtered, read-only grid; Print produces a formatted output via standard Windows printing.
- **ERROR:** N/A — same empty-state pattern as search if no records match.

## 4.13 Generate Report (SCR-016 → SCR-017)
- **START:** User opens Reports.
- **USER ACTION:** Selects a report type (Current Inventory, Low Stock, Out-of-Stock, Stock-In, Stock-Out, Transaction History) and sets the filters that report supports (date range, category, status, product, supplier/customer as applicable) → Generate.
- **SYSTEM RESPONSE:** Builds the report from current application data (FR-RPT-001).
- **NEXT ACTION:** Report Viewer opens showing results; user may Print or return to adjust filters.
- **SUCCESS:** Report displayed with correct columns per report type (see document 06 for per-report column specs); Print available.
- **ERROR:** No results → "No records found for the selected filters" shown in the viewer, not an error state.

## 4.14 Manage Users (SCR-018 → SCR-019, Administrator only)
- **START:** Administrator opens User Management.
- **USER ACTION:** Clicks Add (or selects an existing user and clicks Edit/Deactivate); enters Username, Password (create only), Full Name, Role → Save.
- **SYSTEM RESPONSE:** Validates required fields, username uniqueness (BR-019), password minimum length (VAL-017); on Deactivate, checks the target isn't the Administrator's own currently logged-in account (BR-020).
- **NEXT ACTION:** Confirmation prompt for Deactivate specifically (destructive-adjacent action).
- **SUCCESS:** "User created/updated/deactivated successfully"; list refreshes.
- **ERROR:** "This username is already in use." / "You cannot deactivate your own account while logged in." — Deactivate button is disabled on the Administrator's own row so this error is prevented rather than merely handled.
