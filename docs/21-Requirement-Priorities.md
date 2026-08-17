# 21. Requirement Priorities (MoSCoW)

## Must Have (Version 1.0 / MVP)

- Authentication (login/logout, role)
- Dashboard with core metrics
- Full CRUD for Product, Category, Supplier, Customer
- Stock-In and Stock-Out with automatic quantity update
- Stock status calculation (In / Low / Out)
- Search and basic filtering on products
- Transaction history recording and viewing
- Core validation and business rules (especially no negative stock)
- Basic reports: Current Inventory, Low Stock, Transaction History
- WinForms multi-form navigation and event handling
- Data persistence in a relational database

## Should Have

- Inventory Adjustment with reason
- Out-of-Stock dedicated report/view
- Date-range filtering on transaction reports
- User management by Administrator
- Soft-delete / Active flags for master data
- Created/Modified audit fields on master data

## Could Have

- Inline ErrorProvider validation
- Export reports to CSV / simple file
- Last login tracking
- More advanced dashboard charts (still WinForms compatible)
- Print preview polish

## Won’t Have (This Version)

- Barcode scanning
- Multi-warehouse
- Automatic purchasing
- Cloud sync
- Mobile app
- AI / forecasting
- Advanced BI
- Multi-currency
- Serial/lot tracking
