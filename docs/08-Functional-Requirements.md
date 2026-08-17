# 8. Functional Requirements

All functional requirements are written in “The system shall …” form and are intended to be testable.

## 8.1 Authentication & Session (FR-AUTH)

| ID | Requirement |
|---|---|
| FR-AUTH-001 | The system shall require a user to enter a username and password to log in. |
| FR-AUTH-002 | The system shall authenticate credentials against stored user records. |
| FR-AUTH-003 | The system shall display a clear error message when credentials are incorrect. |
| FR-AUTH-004 | The system shall prevent access to functional screens until successful login. |
| FR-AUTH-005 | The system shall load the user's role after login and apply role-based permissions. |
| FR-AUTH-006 | The system shall provide Logout that returns the user to Login and clears the current session. |
| FR-AUTH-007 | The system shall not store passwords in plain text. |
| FR-AUTH-008 | The system shall allow only one active session per application instance. |

## 8.2 Dashboard (FR-DASH)

| ID | Requirement |
|---|---|
| FR-DASH-001 | After successful login the system shall display a Dashboard form. |
| FR-DASH-002 | The Dashboard shall show total active products, categories, suppliers, current inventory quantity, low-stock products, and out-of-stock products. |
| FR-DASH-003 | The Dashboard shall list recent inventory transactions and provide navigation to main modules. |

## 8.3 Product Management (FR-PROD)

| ID | Requirement |
|---|---|
| FR-PROD-001 | The system shall allow authorised users to create a product. |
| FR-PROD-002 | A product shall contain Product ID, Name, Category, Primary/Default Supplier, Description (optional), Purchase Price, Selling Price, Minimum Stock Level, and Status. QuantityOnHand shall not be entered during product creation and shall start at 0. |
| FR-PROD-003 | The system shall prevent creation with empty required fields or invalid numeric values. |
| FR-PROD-004 | The system shall allow authorised users to view product lists and details. |
| FR-PROD-005 | The system shall allow authorised users to update editable product information, but not directly edit QuantityOnHand. |
| FR-PROD-006 | The system shall allow deletion only when the product has no related inventory transactions; otherwise deletion shall be blocked and the product may be marked Inactive. |
| FR-PROD-007 | The system shall support product search by name or Product ID and filtering by Category, Supplier, and Stock Status. |
| FR-PROD-008 | The system shall display calculated stock status for each product. |
| FR-PROD-009 | The system shall record product creation and update user/timestamp information where applicable. |

## 8.4 Category Management (FR-CAT)

| ID | Requirement |
|---|---|
| FR-CAT-001 | The system shall allow authorised users to create, view, update, and delete categories subject to business rules. |
| FR-CAT-002 | A category shall contain Category ID, Name, Description (optional), and Status. |
| FR-CAT-003 | Category names shall be unique. |
| FR-CAT-004 | A category containing products shall not be hard-deleted and may instead be marked Inactive. |

## 8.5 Supplier Management (FR-SUP)

| ID | Requirement |
|---|---|
| FR-SUP-001 | The system shall allow authorised users to create, view, update, and delete suppliers subject to business rules. |
| FR-SUP-002 | A supplier shall contain Supplier ID, Name, Contact Person, Phone, Email, Address, and Status. |
| FR-SUP-003 | The system shall prevent hard deletion of a supplier referenced by products or inventory transactions; it may be marked Inactive instead. |
| FR-SUP-004 | The system shall support supplier search by name. |

## 8.6 Customer Management (FR-CUS)

| ID | Requirement |
|---|---|
| FR-CUS-001 | The system shall allow authorised users to create, view, update, and delete customers subject to business rules. |
| FR-CUS-002 | A customer shall contain Customer ID, Name, Phone, Email (optional), Address (optional), and Status. |
| FR-CUS-003 | The system shall prevent hard deletion of a customer referenced by Stock-Out transactions; it may be marked Inactive instead. |
| FR-CUS-004 | The system shall support customer search by name or phone. |

## 8.7 Stock-In (FR-SIN)

| ID | Requirement |
|---|---|
| FR-SIN-001 | The system shall allow authorised users to record Stock-In. |
| FR-SIN-002 | Stock-In shall capture Transaction ID, Product, Actual Supplier for the receipt, Quantity, Date, Purchase Price at receipt, User, and Notes (optional). |
| FR-SIN-003 | Quantity shall be a positive integer. |
| FR-SIN-004 | Successful Stock-In shall increase QuantityOnHand by the entered quantity. |
| FR-SIN-005 | Stock-In records shall be stored as append-only transaction history. |
| FR-SIN-006 | Stock-In shall not be allowed for inactive products. |

## 8.8 Stock-Out (FR-SOUT)

| ID | Requirement |
|---|---|
| FR-SOUT-001 | The system shall allow authorised users to record Stock-Out. |
| FR-SOUT-002 | Stock-Out shall capture Transaction ID, Product, Customer (optional), Quantity, Date, Selling Price, User, and Notes (optional). |
| FR-SOUT-003 | Quantity shall be a positive integer. |
| FR-SOUT-004 | The system shall reject Stock-Out when requested quantity exceeds QuantityOnHand. |
| FR-SOUT-005 | Successful Stock-Out shall decrease QuantityOnHand by the entered quantity. |
| FR-SOUT-006 | Stock-Out records shall be stored as append-only transaction history. |
| FR-SOUT-007 | Stock-Out shall not be allowed for inactive products. |

## 8.9 Inventory Adjustment (FR-ADJ)

| ID | Requirement |
|---|---|
| FR-ADJ-001 | Only an Administrator shall be allowed to perform inventory adjustments. |
| FR-ADJ-002 | An adjustment shall capture Transaction ID, Product, Previous Quantity, New Quantity, Difference, Reason, Date, and User. |
| FR-ADJ-003 | The system shall update QuantityOnHand to the approved New Quantity. |
| FR-ADJ-004 | The adjustment shall be recorded in append-only transaction history. |
| FR-ADJ-005 | Adjustment Reason shall be mandatory. |

## 8.10 Stock Status & Monitoring (FR-STK)

| ID | Requirement |
|---|---|
| FR-STK-001 | The system shall calculate status automatically: Out of Stock = QuantityOnHand 0; Low Stock = 0 < QuantityOnHand ≤ Minimum Stock Level; In Stock = QuantityOnHand > Minimum Stock Level. |
| FR-STK-002 | The calculated status shall appear on product lists and details. |
| FR-STK-003 | The system shall provide Low Stock and Out of Stock views or filters. |
| FR-STK-004 | Quantity changes through Stock-In, Stock-Out, or Adjustment shall be reflected immediately. |

## 8.11 Searching & Filtering (FR-SRCH)

| ID | Requirement |
|---|---|
| FR-SRCH-001 | Product lists shall support search by Product Name and Product ID. |
| FR-SRCH-002 | Product lists shall support filtering by Category, Supplier, and Stock Status. |
| FR-SRCH-003 | Transaction history shall support date range, transaction type, and Product filters. |
| FR-SRCH-004 | Supplier and Customer lists shall support name search; Customer lists shall also support phone search. |
| FR-SRCH-005 | Search/filter operations shall update displayed data without restarting the application. |

## 8.12 Reporting (FR-RPT)

| ID | Requirement |
|---|---|
| FR-RPT-001 | The system shall provide Current Inventory, Low Stock, Out-of-Stock, Stock-In, Stock-Out, and Inventory Transaction History reports. |
| FR-RPT-002 | Stock-In and Stock-Out reports shall support date-range filtering. |
| FR-RPT-003 | MVP reports shall be viewable on screen and printable. Export is not required for MVP. |

## 8.13 User Management (FR-USR)

| ID | Requirement |
|---|---|
| FR-USR-001 | An Administrator shall be able to create user accounts. |
| FR-USR-002 | An Administrator shall be able to edit user accounts. |
| FR-USR-003 | An Administrator shall be able to deactivate user accounts. |
| FR-USR-004 | An Administrator shall be able to assign Administrator or Inventory/Store Staff roles. |
| FR-USR-005 | Each user shall have a unique username, password credential, full name, and role. |
| FR-USR-006 | Deactivated users shall be unable to log in. |
