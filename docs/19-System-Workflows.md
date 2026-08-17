# 19. System Workflows

## WF-001 — User Login
1. Application starts and displays Login.
2. User enters username and password.
3. User selects Login.
4. System validates credentials and account status.
5. On success, system loads role and opens Dashboard.
6. On failure, system displays an error and remains on Login.

## WF-002 — Create Product
1. Authorized user opens Products → Add New.
2. System loads active Categories and default/preferred Suppliers.
3. User enters required product information.
4. User selects Save.
5. Validation runs.
6. If valid, product is saved with QuantityOnHand = 0; no inventory transaction is created.
7. List refreshes and success feedback is shown.

## WF-003 — Edit Product
1. User selects a product and chooses Edit.
2. Current editable values are displayed.
3. User changes allowed fields.
4. QuantityOnHand remains read-only.
5. Validation runs and valid changes are saved.

## WF-004 — Delete / Deactivate Product
1. User selects a product and chooses Delete.
2. Confirmation is requested.
3. System checks related inventory transactions.
4. If none exist, hard deletion may proceed.
5. If transactions exist, deletion is blocked and the product may be deactivated.

## WF-005 — Receive Stock (Stock-In)
1. User opens Stock-In.
2. User selects an active Product and the actual Supplier for the receipt.
3. User enters positive Quantity, valid non-future Date, receipt Unit Price, and optional Notes.
4. Validation runs.
5. System creates an append-only transaction.
6. System increases QuantityOnHand by Quantity.
7. Stock Status updates automatically.

## WF-006 — Issue Stock (Stock-Out)
1. User opens Stock-Out.
2. User selects an active Product.
3. User may select a Customer; Customer is optional.
4. User enters positive Quantity, valid Date, Selling Price, and optional Notes.
5. System checks available QuantityOnHand.
6. If sufficient, transaction is created and QuantityOnHand decreases.
7. If insufficient, operation is rejected and available quantity is shown.
8. Stock Status updates automatically.

## WF-007 — Inventory Adjustment
1. Administrator opens Inventory Adjustment.
2. Administrator selects an active Product.
3. System displays Previous Quantity.
4. Administrator enters approved New Quantity and mandatory Reason.
5. System validates that New Quantity is >= 0 and Date is not in the future.
6. System records Previous Quantity, New Quantity, Difference, Reason, User, and Date as an append-only transaction.
7. Product.QuantityOnHand becomes New Quantity.

## WF-008 — Search Inventory
1. User opens Product List.
2. User enters Product Name/ID and/or selects filters.
3. Grid updates without restarting the application.
4. User can clear filters.

## WF-009 — Identify Low-Stock Items
1. User opens Low Stock view/filter.
2. System displays products where 0 < QuantityOnHand <= MinimumStockLevel.
3. User can view details or open the Low Stock Report.

## WF-010 — View Transaction History
1. User opens Transaction History.
2. User optionally selects date range, transaction type, and Product.
3. User applies filters.
4. System displays matching immutable transaction records.
5. User may print the results.

## WF-011 — User Management
1. Administrator opens User Management.
2. Administrator creates or edits a user, or deactivates an existing user.
3. Administrator assigns Administrator or Inventory Staff role.
4. System validates username uniqueness and required fields.
5. Deactivated users cannot log in.
6. Administrator cannot deactivate their own currently logged-in account.

## WF-012 — Generate Report
1. User opens Reports.
2. User selects report type.
3. User sets supported filters.
4. System generates the report from current data.
5. Report is displayed on screen.
6. User may print it.
7. Export is not required for MVP.
