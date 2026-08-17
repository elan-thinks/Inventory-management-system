# 18. Use Case Catalogue

## UC-001 — User Login
**Actor:** Registered user  
**Goal:** Gain access according to role.  
**Preconditions:** Application is running; account exists and is active.  
**Trigger:** Application starts or Login is displayed.

**Main Flow:**
1. System displays Login.
2. User enters username and password.
3. User selects Login.
4. System validates credentials and account status.
5. System loads the role and opens Dashboard.

**Exceptions:** Invalid credentials or database failure → clear error; remain on Login.  
**Postcondition:** Authenticated session exists.  
**Requirements:** FR-AUTH-001–008, FR-USR-006.

---

## UC-002 — User Logout
**Actor:** Logged-in user  
**Goal:** End the current session.  
**Main Flow:** Select Logout → close functional forms → clear session → return to Login.  
**Postcondition:** No authenticated session remains.  
**Requirements:** FR-AUTH-006.

---

## UC-003 — View Dashboard
**Actor:** Logged-in user  
**Goal:** View key inventory metrics and navigate modules.  
**Main Flow:** System displays active product/category/supplier counts, inventory quantity, low/out-of-stock counts, and recent transactions.  
**Requirements:** FR-DASH-001–003.

---

## UC-004 — Create Product
**Actor:** Authorized user  
**Preconditions:** At least one active Category and one active default Supplier exist.  
**Main Flow:** Open Product form → enter fields → validate → save Product with QuantityOnHand = 0 → refresh list.  
**Exceptions:** Invalid or duplicate data → block save and show validation.  
**Postcondition:** Product exists with zero initial stock and no inventory transaction.  
**Requirements:** FR-PROD-001–003, BR-001–006, BR-026–027.

---

## UC-005 — View Products
**Actor:** Logged-in user  
**Main Flow:** Open Product List → view list → select product for details.  
**Requirements:** FR-PROD-004, FR-PROD-008.

---

## UC-006 — Update Product
**Actor:** Authorized user  
**Main Flow:** Select product → Edit → change allowed fields → validate → save. QuantityOnHand is never directly edited here.  
**Requirements:** FR-PROD-005, FR-PROD-009.

---

## UC-007 — Delete / Deactivate Product
**Actor:** Authorized user  
**Main Flow:** Select Product → Delete → confirm → system checks transactions → hard-delete only if no transactions; otherwise block and offer deactivation.  
**Requirements:** FR-PROD-006, BR-018.

---

## UC-008 — Search / Filter Products
**Actor:** Logged-in user  
**Main Flow:** Enter Product Name/ID and/or select Category, Supplier, or Status → displayed results update → clear filters to restore list.  
**Requirements:** FR-PROD-007, FR-SRCH-001–002.

---

## UC-009 — Manage Categories
**Actor:** Authorized user  
**Main Flow:** Create, view, update, search, and conditionally delete/deactivate Category.  
**Exceptions:** Duplicate name or products linked → block invalid operation.  
**Requirements:** FR-CAT-001–004, BR-007–008.

---

## UC-010 — Manage Suppliers
**Actor:** Authorized user  
**Main Flow:** Create, view, update, search, and conditionally delete/deactivate Supplier.  
**Requirements:** FR-SUP-001–004, BR-009, BR-028.

---

## UC-011 — Manage Customers
**Actor:** Authorized user  
**Main Flow:** Create, view, update, search, and conditionally delete/deactivate Customer.  
**Requirements:** FR-CUS-001–004, BR-010, BR-029.

---

## UC-012 — Record Stock-In
**Actor:** Authorized user  
**Preconditions:** Product is active; actual Supplier exists.  
**Main Flow:** Select Product and actual Supplier → enter positive Quantity, Date, Unit Price, Notes → validate → create append-only Stock-In transaction → increase QuantityOnHand → update status.  
**Requirements:** FR-SIN-001–006, BR-012, BR-016–017, BR-021, BR-027–030.

---

## UC-013 — Record Stock-Out
**Actor:** Authorized user  
**Preconditions:** Product is active.  
**Main Flow:** Select Product → optionally select Customer → enter Quantity, Date, Selling Price, Notes → validate stock → create append-only transaction → decrease QuantityOnHand → update status.  
**Exception:** Requested quantity exceeds stock → reject operation.  
**Requirements:** FR-SOUT-001–007, BR-011–012, BR-016–017, BR-021, BR-025, BR-027, BR-029–030.

---

## UC-014 — Perform Inventory Adjustment
**Actor:** Administrator  
**Main Flow:** Select Product → system shows Previous Quantity → enter New Quantity and Reason → validate → create append-only Adjustment → update QuantityOnHand.  
**Requirements:** FR-ADJ-001–005, BR-013–014, BR-017, BR-027, BR-030.

---

## UC-015 — View Stock Status
**Actor:** Logged-in user  
**Main Flow:** Open Product List/dashboard → system displays calculated In Stock, Low Stock, or Out of Stock status → filter as required.  
**Requirements:** FR-STK-001–004, BR-015.

---

## UC-016 — View Transaction History
**Actor:** Logged-in user  
**Main Flow:** Open History → optionally filter by date, type, and Product → view immutable records → print if required.  
**Requirements:** FR-SRCH-003, FR-RPT-001–003.

---

## UC-017 — Generate Inventory Report
**Actor:** Authorized user  
**Main Flow:** Select report → choose supported filters → Generate → view report → Print. Export is not part of MVP.  
**Requirements:** FR-RPT-001–003.

---

## UC-018 — Manage Users
**Actor:** Administrator  
**Main Flow:** Open User Management → create/edit/deactivate account → assign role → validate → save.  
**Exceptions:** Duplicate username or self-deactivation → block operation.  
**Requirements:** FR-USR-001–006, BR-019–020.
