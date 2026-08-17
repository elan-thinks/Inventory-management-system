# 5. Screen-by-Screen Specification

Format per screen: **Screen Info → Layout → Components → Actions → States.**
List screens (Product, Category, Supplier, Customer) share one pattern, documented once in 5.3 and referenced by 5.6–5.8. Add/Edit screens share a pattern documented once in 5.5 and referenced likewise.

---

## 5.1 Login — SCR-001

**Purpose:** Authenticate; load role. **Role:** All (pre-auth). **Requirements:** FR-AUTH-001–008, FR-USR-006.

**Layout:** Centered card on a plain background — Header (app name/logo), Field area, Action area. No sidebar (nothing to navigate to before auth).

**Components:**
- Username TextBox (required indicator)
- Password TextBox (masked, required indicator)
- "Login" Button (primary)
- Inline error message area (below fields, hidden until needed)
- Optional: app version/build label, small and unobtrusive, bottom corner

**Actions:**
| Action | Trigger | Result | Validation | Feedback |
|---|---|---|---|---|
| Login | Click "Login" or Enter key in Password field | Authenticate → Dashboard | Both fields required | Inline error on failure; navigation on success |

**States:** Normal (empty fields, focus on Username) · Loading (brief "Signing in…" state on Login button while credentials are checked — button disabled during check) · Error (ER-001 message shown, password field cleared) · Disabled (Login button disabled until both fields have content).

---

## 5.2 Dashboard — SCR-002

**Purpose:** Orientation hub. **Role:** Admin, Staff (same layout; both see full metric set per FR-DASH-002, since the SRS does not define a reduced metric set for Staff — role differences apply to navigation access, not dashboard content). **Requirements:** FR-DASH-001–003.

**Layout:** Header ("Dashboard," current date) → Metrics row (summary tiles) → two-column lower region: Recent Activity (left, wider) + Quick Actions (right, narrower).

**Components:**
- **Summary metric tiles** (Label + large number, no unnecessary icons or illustrations): Total Active Products, Total Categories, Total Suppliers, Total Current Inventory Quantity, Low-Stock Product Count, Out-of-Stock Product Count. Each low-stock/out-of-stock tile uses status color + icon + label per the "not color alone" rule (see document 07).
- **Recent Activity list** (DataGridView or simple list, last N transactions): Date, Type, Product, Quantity, User. Read-only; clicking a row can navigate to Inventory History pre-filtered to that transaction's date.
- **Quick Actions panel:** Buttons for the tasks staff repeat most — "Record Stock-In," "Record Stock-Out," "Search Products." (Adjustment/User Management omitted here for Staff since those modules aren't theirs; shown for Administrator.)

**Actions:** Click a metric tile → navigates to the relevant filtered list (e.g., Low-Stock tile → Product List filtered to Low Stock). Click Quick Action → opens that screen directly.

**States:** Normal (populated) · Loading (tiles show a neutral placeholder, e.g. "—," while counts are computed on load) · Empty (a brand-new installation with zero products: tiles show 0, Recent Activity shows "No recent activity yet — record your first Stock-In to get started," with a direct link/button into Stock-In) · Error (metrics unavailable due to a database issue → tile area shows "Unable to load dashboard data" per ER-007, rest of app remains usable).

---

## 5.3 Product List — SCR-003 *(reference pattern for all master-data list screens)*

**Purpose:** Browse, search, filter products. **Role:** Admin, Staff (view/search). **Requirements:** FR-PROD-004,007,008; FR-SRCH-001–002; FR-STK-002.

**Permission:** Administrator can Add/Edit/Delete/Deactivate. Inventory/Store Staff can Search, Filter, and View only.

**Layout:** Header ("Products," "+ Add Product" button top-right) → Search & Filter bar → DataGridView (main content) → Footer/status area (row count).

**Components:**
- Search TextBox (placeholder: "Search by name or ID…")
- Filter ComboBoxes: Category, Supplier, Stock Status (All/In Stock/Low Stock/Out of Stock)
- "Clear Filters" link/button (appears only when a filter is active)
- DataGridView columns: Product ID, Name, Category, Default Supplier, Qty on Hand, Min Level, Status (icon+label+color), Selling Price, Actions (View/Edit/Delete icons for Administrator; View only for Staff)
- Row count / result summary label

**Actions:**
| Action | Trigger | Result | Validation | Feedback |
|---|---|---|---|---|
| Add Product | Admin clicks "+ Add Product" | Opens SCR-005 (Add mode) | Admin-only | Staff do not see this action |
| View | Click row / "View" icon | Opens SCR-004 | — | — |
| Edit | Admin clicks "Edit" icon | Opens SCR-005 (Edit mode) | Admin-only | Staff do not see this action |
| Delete | Admin clicks "Delete" icon | Confirmation dialog → delete or blocked-delete dialog | Checks related transactions (BR-018) | Success/blocked message |
| Search/Filter | Type / select | Grid updates live | — | Result count updates |

**States:** Normal (populated grid) · Empty — no products at all ("No products yet. Add your first product to get started." + Add button for Admin) · Empty — no search/filter results ("No products match your search/filters." + "Clear Filters" link) · Loading (grid shows a lightweight loading indicator on open/refresh) · Error (ER-007/ER-010 friendly message, retry option).

---

## 5.4 Product Details — SCR-004

**Purpose:** Full read view of one product. **Role:** Admin, Staff. **Requirements:** FR-PROD-004, FR-STK-002.

**Layout:** Header (Product Name, Status badge) → two-column field area (left: identity/category/supplier/pricing; right: stock info) → Action area (Edit, Delete/Deactivate, Back).

**Components:** Read-only labeled fields for all Product attributes (Name, Category, Default Supplier, Description, Purchase Price, Selling Price, Quantity on Hand, Minimum Stock Level, Status badge, Active/Inactive, Created/Modified metadata where available). No editable controls on this screen — it is a pure detail view; editing happens on SCR-005.

**Actions:** Admin Edit → SCR-005. Admin Delete/Deactivate → confirmation flow (see 5.3). Back → SCR-003.

**States:** Normal · Not Found (ER-008: "The selected record no longer exists," with a Back action — covers the case where the product was deleted by another session between list and detail load) · Inactive (visual treatment: muted/greyed header, "Inactive" badge instead of stock-status badge).

---

## 5.5 Product Add/Edit — SCR-005 *(reference pattern for all master-data Add/Edit dialogs)*

**Purpose:** Create/update product. **Role:** Admin only. **Requirements:** FR-PROD-001–003,005,009; BR-001–006, BR-026–027; VAL-002,003,009,011,012.

**Layout:** Modal dialog. Header (Title: "Add Product" / "Edit Product") → Field area (grouped: Identity, Category & Supplier, Pricing, Stock Settings) → Action area, bottom-right: Cancel, Save.

**Components:**
- Product Name — required TextBox, red asterisk, max 100 chars
- Category — required ComboBox, populated from active Categories
- Default Supplier — required ComboBox, populated from active Suppliers
- Description — optional multiline TextBox, max 500 chars, char counter
- Purchase Price / Selling Price — required numeric TextBoxes (or NumericUpDown-style masked input), ≥ 0, 2 decimals
- Minimum Stock Level — required numeric TextBox, integer ≥ 0
- Quantity on Hand — **visible but disabled/read-only**, showing "0 (starts at zero)" on Add, or the current value on Edit, with a caption: "Adjust stock using Stock-In, Stock-Out, or Inventory Adjustment."
- Active status — CheckBox (Edit mode only; Add mode defaults to Active)
- Inline validation message area per field

**Actions:**
| Action | Trigger | Result | Validation | Feedback |
|---|---|---|---|---|
| Save | Click "Save" | Validates → persists → closes dialog | All VAL rules for this entity | Success toast/status message, list refresh |
| Cancel | Click "Cancel" or dialog close (X) | Discards changes, closes dialog | If dirty, confirm discard | — |

**States:** Normal (Add: blank form; Edit: pre-filled) · Validating (inline, per-field, on blur and on Save attempt) · Error (field-level red outline + message; Save button remains enabled so the user can retry, focus jumps to first invalid field) · Saving (Save button shows brief busy state, disabled to prevent double-submit) · Success (dialog closes, confirmation shown on the parent screen).

---

## 5.6 Category List & Add/Edit — SCR-006 / SCR-007

Follows the SCR-003 / SCR-005 pattern exactly, scoped to Category. **Requirements:** FR-CAT-001–004; BR-007–008; VAL-004.

**Permission:** Administrator can Add/Edit/Delete/Deactivate. Inventory/Store Staff can View/Search only.

**Differences from the reference pattern:**
- Grid columns: Category ID, Name, Description, Product Count, Status, Actions.
- No stock-status filter (not applicable); filter is Status (Active/Inactive) only.
- Add/Edit fields: Name (required, unique, max 100), Description (optional, max 500), Active status.
- Delete: if Product Count > 0, hard delete is blocked (BR-008); Blocked-Delete dialog explains "This category contains N product(s). Reassign or remove those products first, or mark this category Inactive." with a direct "Mark Inactive" action.

---

## 5.7 Supplier List & Add/Edit — SCR-008 / SCR-009

Follows the reference pattern, scoped to Supplier. **Requirements:** FR-SUP-001–004; BR-009, BR-028; VAL-005,006,013,014.

**Permission:** Administrator can Add/Edit/Delete/Deactivate. Inventory/Store Staff can View/Search only.

**Differences:**
- Grid columns: Supplier ID, Name, Contact Person, Phone, Email, Status, Actions.
- Search: Name only (FR-SUP-004); no Category/Status-type filter grid beyond Active/Inactive.
- Add/Edit fields: Name (required, max 150), Contact Person (optional, max 100), Phone (optional, format-validated), Email (optional, format-validated), Address (optional, max 250).
- Delete: blocked if referenced by any Product or InventoryTransaction (BR-009); Blocked-Delete dialog offers "Mark Inactive."

---

## 5.8 Customer List & Add/Edit — SCR-010 / SCR-011

Follows the reference pattern, scoped to Customer. **Requirements:** FR-CUS-001–004; BR-010, BR-029; VAL-007,008,013,014.

**Permission:** Administrator can Add/Edit/Delete/Deactivate. Inventory/Store Staff can View/Search only.

**Differences:**
- Grid columns: Customer ID, Name, Phone, Email, Status, Actions.
- Search: Name or Phone (FR-CUS-004).
- Add/Edit fields: Name (required, max 150), Phone (optional, format-validated), Email (optional, format-validated), Address (optional, max 250).
- Delete: blocked if referenced by any Stock-Out transaction (BR-010); Blocked-Delete dialog offers "Mark Inactive."

---

## 5.9 Stock-In — SCR-012

**Purpose:** Record goods received. **Role:** Admin, Staff. **Requirements:** FR-SIN-001–006; BR-012,016–017,021,027–030; VAL-009,010,015,019.

**Layout:** Single-column form (not a modal — a dedicated screen, since staff use it repeatedly through a shift), grouped: Product & Supplier, Movement Details, Notes. Action area bottom.

**Components:**
- Product — required ComboBox with type-to-search, shows only Active products, displays current on-hand quantity next to the selection once chosen
- Actual Supplier — required ComboBox (defaults to the product's default supplier but is changeable, per BR-028), all active suppliers listed
- Quantity — required numeric TextBox, positive integer, live "New on-hand will be: X" preview as it's typed
- Date — DateTimePicker, defaults to today, cannot select a future date
- Unit Price (purchase price for this receipt) — required numeric, ≥ 0, 2 decimals
- Notes — optional multiline TextBox, max 500 chars
- "Record Stock-In" primary Button; "Clear Form" secondary action

**Actions:** Record Stock-In → confirmation dialog ("Add {qty} × {product} from {supplier}? New quantity on hand: {X}.") → Yes commits, No cancels. On success, form clears and refocuses Product field for rapid repeated entry; a toast/status message confirms.

**States:** Normal · Product Inactive Selected (should not occur since only active products are listed, but if data changes mid-session, save is blocked with "This product is now inactive.") · Insufficient input (Save disabled until required fields are valid) · Error (standard validation messaging) · Success (confirmation + form reset).

---

## 5.10 Stock-Out — SCR-013

**Purpose:** Record goods issued. **Role:** Admin, Staff. **Requirements:** FR-SOUT-001–007; BR-011–012,016–017,021,025,027,029–030; VAL-009,010,015,019,020.

**Layout:** Same structural pattern as Stock-In. Groups: Product & Customer, Movement Details, Notes.

**Components:**
- Product — required ComboBox, Active only, shows **Available Quantity** prominently next to selection (e.g., a small stat: "Available: 42")
- Customer — optional ComboBox ("No customer selected" default; explicitly optional, no asterisk)
- Quantity — required numeric TextBox; live validation against available quantity as it's typed (VAL-020) — field turns to an error state and Save disables the instant entered quantity exceeds availability, before the user even attempts Save
- Date — DateTimePicker, defaults to today, no future dates
- Selling Price — required numeric, ≥ 0, 2 decimals (pre-filled from the product's current Selling Price, editable)
- Notes — optional
- "Record Stock-Out" primary Button; "Clear Form" secondary

**Actions:** Record Stock-Out → confirmation dialog ("Issue {qty} × {product} to {customer or \"no customer\"}? Remaining stock: {X}.") → commits on Yes.

**States:** Normal · Insufficient Stock ("Insufficient stock. Available: {X}." — inline under Quantity field, Save disabled, red field outline) · Product Inactive (blocked with message) · Error/Success as Stock-In.

---

## 5.11 Inventory Adjustment — SCR-014 (Administrator only)

**Purpose:** Correct stock quantity with reason. **Role:** Admin only. **Requirements:** FR-ADJ-001–005; BR-013–014,017,027,030; VAL-012,018.

**Layout:** Single-column form. Groups: Product, Quantity Correction, Reason.

**Components:**
- Product — required ComboBox, Active only
- Previous Quantity — read-only label, auto-populated on product selection
- New Quantity — required numeric TextBox, integer ≥ 0
- Difference — read-only, computed live ("+5" green / "−3" red / "0" neutral) as New Quantity is typed
- Reason — required multiline TextBox, max 250 chars, explicit placeholder text (e.g., "e.g., stock count correction, damaged goods, theft")
- "Save Adjustment" primary Button

**Actions:** Save Adjustment → confirmation dialog explicitly stating the before/after quantities and reason (this is the single highest-risk action in the system, since it directly overrides stock without a supplier/customer transaction context) → commits on Yes.

**States:** Normal · Missing Reason (Save disabled, inline "A reason is required.") · Error/Success standard pattern. This screen and its sidebar entry are **entirely hidden**, not merely disabled, for Inventory/Store Staff.

---

## 5.12 Inventory Transaction History — SCR-015

**Purpose:** Read-only, filterable log. **Role:** Admin, Staff. **Requirements:** FR-SRCH-003; FR-RPT-001–003; BR-030.

**Layout:** Header → Filter bar → DataGridView → Footer (Print).

**Components:**
- Filter bar: Date Range (From/To DateTimePickers), Transaction Type ComboBox (All/Stock-In/Stock-Out/Adjustment), Product ComboBox (type-to-search, optional)
- DataGridView columns: Transaction ID, Date, Type (icon+label), Product, Quantity (with +/− sign styled by type), Supplier/Customer (contextual), User, Notes
- **No Edit/Delete actions anywhere on this grid or its rows** — this is the UI-level enforcement of append-only history (BR-030)
- "Print" Button

**Actions:** Apply filters → grid updates. Print → standard print dialog with the current filtered view.

**States:** Normal · Empty — no transactions yet ("No stock movements recorded yet.") · Empty — no filter matches ("No transactions match your filters." + Clear Filters) · Loading · Error (ER-007/010 pattern).

---

## 5.13 Reports Hub & Report Viewer — SCR-016 / SCR-017

**Purpose:** Select and view/print MVP reports. **Role:** Admin (all 6 reports), Staff (Current Inventory, Low Stock, Transaction History only — the three reports FR-RPT and §6.2.2 make available to Staff). **Requirements:** FR-RPT-001–003.

**SCR-016 Layout:** A simple list/grid of report cards or a ComboBox + description, one entry per report type available to the current role, each showing its supported filters inline once selected, and a "Generate" button.

**Per-report filters (as defined in SRS §14):**
| Report | Filters | Key Columns |
|---|---|---|
| Current Inventory | Category, Stock Status | Product ID, Name, Category, Default Supplier, Qty on Hand, Min Level, Status, Purchase Price, Selling Price, Stock Value |
| Low Stock | (implicit: Status = Low Stock) | Product ID, Name, Category, Current Qty, Min Level, Default Supplier, Supplier Contact |
| Out-of-Stock | (implicit: Qty = 0) | Product ID, Name, Category, Default Supplier, Min Level |
| Stock-In | Date Range, Product, Supplier | Transaction ID, Date, Product, Supplier, Quantity, Unit Price, User, Notes |
| Stock-Out | Date Range, Product, Customer | Transaction ID, Date, Product, Customer, Quantity, Unit Price, User, Notes |
| Transaction History | Date Range, Type, Product | Relevant InventoryTransaction fields (as SCR-015) |

**SCR-017 Layout:** Header (report title + active filters summary) → tabular report body (DataGridView or print-oriented grid) → footer (Print, Back to Reports).

**Actions:** Generate (SCR-016 → SCR-017), Print (standard Windows print), Back (returns to SCR-016 with filters preserved so the user can adjust and regenerate).

**States:** Normal (populated table) · Empty ("No records found for the selected filters.") · Loading (brief, for larger date ranges) · Error (ER-007/010 pattern).

---

## 5.14 User Management List & Add/Edit — SCR-018 / SCR-019 (Administrator only)

**Purpose:** Manage accounts and roles. **Role:** Admin only. **Requirements:** FR-USR-001–006; BR-019–020; VAL-016,017.

**SCR-018 Layout:** Header ("+ Add User") → Search bar → DataGridView (Username, Full Name, Role, Status, Last Login if tracked, Actions) → footer.

**SCR-018 special behavior:** On the Administrator's own row, the "Deactivate" action is **disabled** with a tooltip: "You cannot deactivate your own account while logged in" (BR-020, prevented rather than merely blocked-after-the-fact).

**SCR-019 (Add/Edit) Components:**
- Username — required TextBox, unique, max 50, disabled on Edit (usernames are not changed once created, to protect transaction-history attribution integrity)
- Password — required on Create only (min 6 chars, masked TextBox + confirm field); on Edit, a separate "Reset Password" action/link is offered instead of an always-visible password field
- Full Name — required TextBox
- Role — required ComboBox (Administrator / Inventory Staff)
- Active status — CheckBox (Edit mode)

**Actions:** Save → validates uniqueness (BR-019) and required fields → success message → list refresh. Deactivate (list row) → confirmation → account deactivated, cannot log in (FR-USR-006), row shown greyed/Inactive-tagged.

**States:** Normal · Duplicate Username Error ("This username is already in use.") · Missing Fields · Success · Self-Deactivation Blocked (prevented via disabled control, per above, not a runtime error state).
