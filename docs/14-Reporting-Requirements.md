# 14. Reporting Requirements

All reports are basic, screen-viewable, and printable (or simple export). No enterprise BI tools.

## 14.1 Current Inventory Report (RPT-001)

- **Purpose**: Show every active product with current stock position.
- **Columns**: Product ID, Product Name, Category, Supplier, Quantity on Hand, Minimum Level, Stock Status, Purchase Price, Selling Price, Stock Value (Quantity × Purchase Price).
- **Filtering**: Optional by Category or Stock Status.
- **Sorting**: By Product Name or Category.

## 14.2 Low Stock Report (RPT-002)

- **Purpose**: List products that need attention for reordering.
- **Content**: All products where Stock Status = Low Stock.
- **Columns**: Product ID, Name, Category, Current Qty, Minimum Level, Supplier, Contact info (optional).

## 14.3 Out-of-Stock Report (RPT-003)

- **Purpose**: List products that are completely unavailable.
- **Content**: All products where Quantity on Hand = 0.

## 14.4 Stock-In Report (RPT-004)

- **Purpose**: Show goods received in a period.
- **Filtering**: Date range (From – To), optional Product or Supplier.
- **Columns**: Transaction ID, Date, Product, Supplier, Quantity, Unit Price, Total, User, Notes.

## 14.5 Stock-Out Report (RPT-005)

- **Purpose**: Show goods issued/sold in a period.
- **Filtering**: Date range, optional Product or Customer.
- **Columns**: Transaction ID, Date, Product, Customer, Quantity, Unit Price, Total, User, Notes.

## 14.6 Inventory Transaction History Report (RPT-006)

- **Purpose**: Complete movement history for audit or investigation.
- **Filtering**: Date range, Transaction Type, Product.
- **Columns**: All key fields from InventoryTransaction.

## 14.7 Common Report Requirements

- Reports shall be generated from current database data.
- User shall be able to print the report (standard Windows print dialog is acceptable).
- Empty result sets shall display a friendly “No records found” message.
- Report generation shall not lock the UI for an unreasonable time (for the expected data volume).
