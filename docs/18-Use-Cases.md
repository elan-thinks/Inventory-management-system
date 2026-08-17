# 18. Use Case Catalogue

## UC-001 — User Login

- **Actor**: Any registered user
- **Goal**: Gain access to the system according to role
- **Preconditions**: Application is running; user has valid credentials
- **Trigger**: User launches the application or returns to login screen
- **Main Flow**:
  1. System displays Login form
  2. User enters username and password
  3. User clicks Login
  4. System validates credentials
  5. System loads user role and opens Dashboard
- **Alternative Flows**: None significant
- **Exception Flows**:
  - Invalid credentials → show error, remain on Login form
  - Database unavailable → show friendly error
- **Postconditions**: User is authenticated; role-based UI is presented
- **Related Requirements**: FR-AUTH-*, NFR-009, NFR-010

---

## UC-002 — User Logout

- **Actor**: Logged-in user
- **Goal**: End the current session securely
- **Preconditions**: User is logged in
- **Trigger**: User selects Logout
- **Main Flow**:
  1. System closes all open functional forms
  2. System clears session information
  3. System returns to Login form
- **Postconditions**: No authenticated session remains

---

## UC-003 — View Dashboard

- **Actor**: Logged-in user
- **Goal**: See key inventory metrics at a glance
- **Preconditions**: Successful login
- **Main Flow**:
  1. System displays total products, categories, suppliers, total stock quantity, low-stock count, out-of-stock count, and recent transactions
  2. User may click navigation items to open modules
- **Related Requirements**: FR-DASH-*

---

## UC-004 — Create Product

- **Actor**: Administrator or authorised Staff
- **Goal**: Add a new product to the catalogue
- **Preconditions**: User has permission; at least one Category and one Supplier exist
- **Trigger**: User selects “Add Product”
- **Main Flow**:
  1. System displays Product entry form
  2. User fills required fields (Name, Category, Supplier, Prices, Min Level, etc.)
  3. User clicks Save
  4. System validates all fields
  5. System creates the product with QuantityOnHand = 0 (or user-specified initial qty if allowed)
  6. System confirms success and refreshes product list
- **Exception Flows**: Validation failure → show messages, remain on form
- **Postconditions**: New product exists and is visible in lists
- **Related Requirements**: FR-PROD-001 to 004, VAL-*, BR-001 to 006

---

## UC-005 — Update Product

- **Actor**: Authorised user
- **Goal**: Modify product details
- **Main Flow**: Select product → Edit → change fields → Save → validation → update record
- **Exception Flows**: Validation errors; concurrent modification (simple handling)
- **Related Requirements**: FR-PROD-007

---

## UC-006 — Delete Product

- **Actor**: Administrator
- **Goal**: Remove a product that is no longer needed
- **Preconditions**: Product has no related inventory transactions
- **Main Flow**:
  1. User selects product and chooses Delete
  2. System asks for confirmation
  3. System checks for related transactions
  4. If none, product is deleted (or marked inactive)
  5. List is refreshed
- **Exception Flow**: Related transactions exist → inform user, abort delete
- **Related Requirements**: FR-PROD-008, FR-PROD-009, BR-018

---

## UC-007 — Search / Filter Products

- **Actor**: Any logged-in user
- **Goal**: Locate products quickly
- **Main Flow**: Enter search text and/or select filters → System updates DataGridView in real time or on button click
- **Related Requirements**: FR-SRCH-001, FR-SRCH-002, FR-PROD-010, FR-PROD-011

---

## UC-008 — Create Category / Supplier / Customer

Similar structure to Create Product.  
Key differences: uniqueness of Category Name; simpler attribute sets.

---

## UC-009 — Record Stock-In

- **Actor**: Authorised user
- **Goal**: Increase stock of a product after receiving goods
- **Preconditions**: Product exists and is Active; Supplier exists
- **Main Flow**:
  1. User opens Stock-In form
  2. Selects Product and Supplier
  3. Enters Quantity, Date, Purchase Price, optional Notes
  4. Clicks Save
  5. System validates (quantity > 0, etc.)
  6. System creates InventoryTransaction (Type = StockIn)
  7. System increases Product.QuantityOnHand
  8. System confirms success
- **Exception Flows**: Validation failure; inactive product
- **Postconditions**: Stock increased; permanent transaction record exists
- **Related Requirements**: FR-SIN-*, BR-012, BR-017

---

## UC-010 — Record Stock-Out

- **Actor**: Authorised user
- **Goal**: Decrease stock when goods are sold or issued
- **Preconditions**: Product has sufficient QuantityOnHand
- **Main Flow**: Similar to Stock-In, with Customer selection and Selling Price
- **Key Exception**: Insufficient stock → reject with clear message
- **Related Requirements**: FR-SOUT-*, BR-011, BR-025

---

## UC-011 — Perform Inventory Adjustment

- **Actor**: Administrator
- **Goal**: Correct the recorded quantity (e.g., after stock count)
- **Main Flow**:
  1. Select Product
  2. System shows current quantity
  3. User enters new quantity and mandatory Reason
  4. System calculates difference
  5. Creates Adjustment transaction
  6. Updates Product.QuantityOnHand
- **Related Requirements**: FR-ADJ-*, BR-013, BR-014

---

## UC-012 — View Low-Stock / Out-of-Stock Products

- **Actor**: Any user
- **Goal**: Identify products needing attention
- **Main Flow**: Open dedicated view or apply filter on product list
- **Related Requirements**: FR-STK-*, FR-DASH-006/007

---

## UC-013 — View Transaction History

- **Actor**: Any user (with possible role limits)
- **Goal**: Inspect past stock movements
- **Main Flow**: Open history form → apply optional filters (date, type, product) → view results in grid
- **Related Requirements**: FR-SRCH-003, FR-RPT-006

---

## UC-014 — Generate Inventory Report

- **Actor**: Authorised user
- **Goal**: Produce a printable/viewable report
- **Main Flow**: Select report type → optional filters → Generate → view / print
- **Related Requirements**: FR-RPT-*

---

## UC-015 — Manage Users (Administrator)

- **Actor**: Administrator
- **Goal**: Create or deactivate user accounts
- **Main Flow**: Standard CRUD for users with role assignment
- **Exception**: Cannot deactivate own account
- **Related Requirements**: FR-USR-*, BR-019, BR-020
