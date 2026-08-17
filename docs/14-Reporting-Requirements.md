# 14. Reporting Requirements

All MVP reports are screen-viewable and printable. Export is not required for MVP and is treated as future scope.

## 14.1 Current Inventory Report (RPT-001)
- **Purpose:** Show every active product with current stock position.
- **Columns:** Product ID, Product Name, Category, Default Supplier, Quantity On Hand, Minimum Level, Stock Status, Purchase Price, Selling Price, Stock Value.
- **Filtering:** Category and Stock Status.
- **Sorting:** Product Name or Category.

## 14.2 Low Stock Report (RPT-002)
- **Purpose:** List products requiring attention.
- **Content:** Products where Stock Status = Low Stock.
- **Columns:** Product ID, Name, Category, Current Quantity, Minimum Level, Default Supplier, Supplier Contact.

## 14.3 Out-of-Stock Report (RPT-003)
- **Purpose:** List products with QuantityOnHand = 0.
- **Columns:** Product ID, Name, Category, Default Supplier, Minimum Level.

## 14.4 Stock-In Report (RPT-004)
- **Purpose:** Show goods received during a period.
- **Filtering:** Date range, optional Product or actual Supplier.
- **Columns:** Transaction ID, Date, Product, Supplier, Quantity, Unit Price, User, Notes.

## 14.5 Stock-Out Report (RPT-005)
- **Purpose:** Show goods issued during a period.
- **Filtering:** Date range, optional Product or Customer.
- **Columns:** Transaction ID, Date, Product, Customer when present, Quantity, Unit Price, User, Notes.

## 14.6 Inventory Transaction History Report (RPT-006)
- **Purpose:** Show complete stock movement history.
- **Filtering:** Date range, Transaction Type, Product.
- **Columns:** Relevant InventoryTransaction fields.

## 14.7 Common MVP Requirements

- Reports shall use current application data.
- Reports shall be viewable on screen.
- Reports shall be printable using standard Windows printing facilities.
- Empty results shall display a clear “No records found” message.
- Report generation shall remain responsive for the expected university-project data volume.

## 14.8 Future Reporting

Export to formats such as PDF or Excel is not required for MVP and may be considered after the complete MVP is stable.
