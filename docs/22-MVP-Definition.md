# 22. MVP Definition

The MVP is the smallest complete system that still demonstrates the educational goals of the C# Windows Programming course.

## MVP Must Include

1. Login / Logout with Administrator and Inventory Staff roles.
2. Basic User Management by Administrators: create, edit, deactivate, and assign roles.
3. Dashboard showing key inventory counts and recent activity.
4. Product Management: full CRUD subject to deletion rules, search, and filtering.
5. Category Management: full CRUD subject to business rules.
6. Supplier Management: full CRUD subject to business rules.
7. Customer Management: full CRUD subject to business rules.
8. Stock-In: records actual supplier, creates append-only transaction, and increases QuantityOnHand.
9. Stock-Out: optionally records customer, creates append-only transaction, rejects insufficient stock, and decreases QuantityOnHand.
10. Inventory Adjustment: Administrator-only, reason required, append-only, and updates QuantityOnHand.
11. Automatic In Stock / Low Stock / Out of Stock status.
12. Transaction History with search/filtering.
13. Required reports: Current Inventory, Low Stock, Out-of-Stock, Stock-In, Stock-Out, and Transaction History; at minimum they are viewable and printable.
14. Validation and clear error handling.
15. Multiple WinForms forms, navigation, controls, and event-driven interactions.
16. Relational database with the defined relationships and referential integrity.

## Explicitly Deferred from MVP

- PDF/Excel/export functionality
- Barcode scanning
- Advanced analytics or forecasting
- Cloud synchronization
- Mobile application
- Multi-branch inventory
- Automated purchasing/reordering
- Enterprise reporting/BI

## MVP Success Criteria

A user can:

- Log in according to their role.
- Manage users if they are an Administrator.
- Add and manage categories, suppliers, customers, and products.
- Create a product with QuantityOnHand = 0.
- Receive stock and see QuantityOnHand increase.
- Issue stock and see QuantityOnHand decrease.
- Record authorized inventory adjustments.
- See stock status update automatically.
- Search and filter inventory and transaction history.
- View and print the required reports.
- Observe that invalid operations are blocked with clear validation/error messages.

All of the above must be achievable through the WinForms application using the defined relational data model.
