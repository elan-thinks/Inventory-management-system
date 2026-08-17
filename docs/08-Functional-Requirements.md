# 8. Functional Requirements

All functional requirements are written in “The system shall …” form and are intended to be testable.

---

## 8.1 Authentication & Session (FR-AUTH)

| ID       | Requirement |
|----------|-------------|
| FR-AUTH-001 | The system shall require a user to enter a username and password to log in. |
| FR-AUTH-002 | The system shall authenticate the credentials against the stored user records. |
| FR-AUTH-003 | The system shall display a clear error message when username or password is incorrect. |
| FR-AUTH-004 | The system shall prevent access to any functional screen until successful login. |
| FR-AUTH-005 | The system shall load the user’s role after successful login and apply role-based permissions. |
| FR-AUTH-006 | The system shall provide a Logout function that returns the user to the login screen and clears the current session. |
| FR-AUTH-007 | The system shall not store passwords in plain text. |
| FR-AUTH-008 | The system shall allow only one active session per application instance (simple desktop behaviour). |

---

## 8.2 Dashboard (FR-DASH)

| ID       | Requirement |
|----------|-------------|
| FR-DASH-001 | After successful login the system shall display a Dashboard form. |
| FR-DASH-002 | The Dashboard shall show the total number of active products. |
| FR-DASH-003 | The Dashboard shall show the total number of categories. |
| FR-DASH-004 | The Dashboard shall show the total number of suppliers. |
| FR-DASH-005 | The Dashboard shall show the total quantity of all products currently in stock. |
| FR-DASH-006 | The Dashboard shall show the number of products that are currently Low Stock. |
| FR-DASH-007 | The Dashboard shall show the number of products that are currently Out of Stock. |
| FR-DASH-008 | The Dashboard shall list the most recent inventory transactions (e.g., last 5–10). |
| FR-DASH-009 | The Dashboard shall provide navigation buttons/links to the main modules (Products, Stock In, Stock Out, Reports, etc.). |

---

## 8.3 Product Management (FR-PROD)

| ID       | Requirement |
|----------|-------------|
| FR-PROD-001 | The system shall allow authorised users to create a new product. |
| FR-PROD-002 | A product shall have at least: Product ID (system-generated), Name, Category, Supplier, Description (optional), Purchase Price, Selling Price, Quantity on Hand, Minimum Stock Level, Status (Active/Inactive). |
| FR-PROD-003 | The system shall prevent creation of a product with an empty required field. |
| FR-PROD-004 | The system shall prevent negative prices or quantities. |
| FR-PROD-005 | The system shall allow authorised users to view a list of all products. |
| FR-PROD-006 | The system shall allow authorised users to view the full details of a selected product. |
| FR-PROD-007 | The system shall allow authorised users to update an existing product. |
| FR-PROD-008 | The system shall allow authorised users to delete a product only when the product has no related inventory transactions (or according to the defined business rule). |
| FR-PROD-009 | When a product cannot be deleted because of related transactions, the system shall display a clear message explaining why. |
| FR-PROD-010 | The system shall allow users to search products by name or Product ID. |
| FR-PROD-011 | The system shall allow users to filter products by Category, Supplier, and Stock Status (In Stock / Low Stock / Out of Stock). |
| FR-PROD-012 | The system shall display the current stock status of each product based on Quantity vs Minimum Stock Level. |
| FR-PROD-013 | The system shall record the user and timestamp of product creation and last update (where feasible). |

---

## 8.4 Category Management (FR-CAT)

| ID       | Requirement |
|----------|-------------|
| FR-CAT-001 | The system shall allow authorised users to create a new category. |
| FR-CAT-002 | A category shall have at least: Category ID, Name, Description (optional), Status. |
| FR-CAT-003 | The system shall prevent duplicate category names. |
| FR-CAT-004 | The system shall allow viewing, updating, and deleting categories. |
| FR-CAT-005 | The system shall prevent deletion of a category that still contains products. |
| FR-CAT-006 | When deletion is blocked, the system shall inform the user that products must be reassigned or removed first. |

---

## 8.5 Supplier Management (FR-SUP)

| ID       | Requirement |
|----------|-------------|
| FR-SUP-001 | The system shall allow authorised users to create a new supplier. |
| FR-SUP-002 | A supplier shall have at least: Supplier ID, Name, Contact Person, Phone, Email, Address, Status. |
| FR-SUP-003 | The system shall allow viewing, updating, and deleting suppliers. |
| FR-SUP-004 | The system shall prevent deletion of a supplier that is linked to existing products or stock-in transactions (according to business rules). |
| FR-SUP-005 | The system shall support search of suppliers by name. |

---

## 8.6 Customer Management (FR-CUS)

| ID       | Requirement |
|----------|-------------|
| FR-CUS-001 | The system shall allow authorised users to create a new customer. |
| FR-CUS-002 | A customer shall have at least: Customer ID, Name, Phone, Email (optional), Address (optional), Status. |
| FR-CUS-003 | The system shall allow viewing, updating, and deleting customers. |
| FR-CUS-004 | The system shall prevent deletion of a customer that has related stock-out transactions (or mark as inactive instead). |
| FR-CUS-005 | The system shall support search of customers by name or phone. |

---

## 8.7 Stock-In (Inventory Receipt) (FR-SIN)

| ID       | Requirement |
|----------|-------------|
| FR-SIN-001 | The system shall allow authorised users to record a Stock-In transaction. |
| FR-SIN-002 | A Stock-In transaction shall capture: Transaction ID (system-generated), Product, Supplier, Quantity, Date, Purchase Price (at time of receipt), User who performed the transaction, Notes (optional). |
| FR-SIN-003 | Quantity must be a positive integer. |
| FR-SIN-004 | Upon successful save, the system shall increase the product’s Quantity on Hand by the entered quantity. |
| FR-SIN-005 | The system shall store the Stock-In record permanently as part of transaction history. |
| FR-SIN-006 | The system shall prevent Stock-In for an inactive product. |

---

## 8.8 Stock-Out (Inventory Issue / Sale) (FR-SOUT)

| ID       | Requirement |
|----------|-------------|
| FR-SOUT-001 | The system shall allow authorised users to record a Stock-Out transaction. |
| FR-SOUT-002 | A Stock-Out transaction shall capture: Transaction ID, Product, Customer (optional or required according to rule), Quantity, Date, Selling Price (at time of sale), User, Notes (optional). |
| FR-SOUT-003 | Quantity must be a positive integer. |
| FR-SOUT-004 | The system shall reject a Stock-Out if the requested quantity exceeds the current Quantity on Hand. |
| FR-SOUT-005 | Upon successful save, the system shall decrease the product’s Quantity on Hand by the entered quantity. |
| FR-SOUT-006 | The system shall store the Stock-Out record permanently. |
| FR-SOUT-007 | The system shall prevent Stock-Out for an inactive product. |

---

## 8.9 Inventory Adjustment (FR-ADJ)

| ID       | Requirement |
|----------|-------------|
| FR-ADJ-001 | The system shall allow authorised users (Administrator) to perform an inventory adjustment. |
| FR-ADJ-002 | An adjustment shall capture: Transaction ID, Product, Previous Quantity, New Quantity, Difference, Reason, Date, User. |
| FR-ADJ-003 | The system shall update the product’s Quantity on Hand to the new value. |
| FR-ADJ-004 | The adjustment shall be recorded in the transaction history for audit purposes. |
| FR-ADJ-005 | Reason shall be mandatory. |

---

## 8.10 Stock Status & Monitoring (FR-STK)

| ID       | Requirement |
|----------|-------------|
| FR-STK-001 | The system shall calculate stock status for every product as follows:<br>• **Out of Stock** when Quantity on Hand = 0<br>• **Low Stock** when 0 < Quantity on Hand ≤ Minimum Stock Level<br>• **In Stock** when Quantity on Hand > Minimum Stock Level |
| FR-STK-002 | The system shall display the calculated status on product lists and detail views. |
| FR-STK-003 | The system shall provide a dedicated view or filter for Low Stock products. |
| FR-STK-004 | The system shall provide a dedicated view or filter for Out of Stock products. |
| FR-STK-005 | Every change to Quantity on Hand (via Stock-In, Stock-Out, or Adjustment) shall be reflected immediately in the product record. |

---

## 8.11 Searching & Filtering (FR-SRCH)

| ID       | Requirement |
|----------|-------------|
| FR-SRCH-001 | Product list shall support search by Product Name and Product ID. |
| FR-SRCH-002 | Product list shall support filter by Category, Supplier, and Stock Status. |
| FR-SRCH-003 | Transaction history shall support filter by date range, transaction type (In / Out / Adjustment), and Product. |
| FR-SRCH-004 | Supplier and Customer lists shall support search by name. |
| FR-SRCH-005 | Search and filter operations shall update the displayed grid without requiring a full application restart. |

---

## 8.12 Reporting (FR-RPT)

See document `14-Reporting-Requirements.md` for detailed report definitions.  
High-level requirements:

| ID       | Requirement |
|----------|-------------|
| FR-RPT-001 | The system shall provide a Current Inventory Report. |
| FR-RPT-002 | The system shall provide a Low Stock Report. |
| FR-RPT-003 | The system shall provide an Out-of-Stock Report. |
| FR-RPT-004 | The system shall provide Stock-In and Stock-Out reports filterable by date range. |
| FR-RPT-005 | The system shall provide an Inventory Transaction History report. |
| FR-RPT-006 | Reports shall be viewable on screen and printable (or exportable to a simple format). |

---

## 8.13 User Management (Optional but Recommended) (FR-USR)

| ID       | Requirement |
|----------|-------------|
| FR-USR-001 | An Administrator shall be able to create, edit, and deactivate user accounts. |
| FR-USR-002 | Each user shall have a unique username, password, full name, and role. |
| FR-USR-003 | Deactivated users shall be unable to log in. |
