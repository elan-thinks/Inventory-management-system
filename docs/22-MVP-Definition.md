# 22. MVP Definition

The Minimum Viable Product is the smallest complete system that still fully demonstrates the educational goals of the course.

## MVP Must Include

1. **Login / Logout** with at least two roles (Administrator, Inventory Staff)
2. **Dashboard** showing key counts and recent activity
3. **Product Management** — full CRUD + search + filter by category/status
4. **Category, Supplier, Customer Management** — full CRUD with appropriate delete restrictions
5. **Stock-In** — creates transaction and increases quantity
6. **Stock-Out** — creates transaction, enforces available stock, decreases quantity
7. **Automatic Stock Status** (In Stock / Low Stock / Out of Stock)
8. **Transaction History** view with basic filtering
9. **At least three reports**: Current Inventory, Low Stock, Transaction History
10. **Solid validation** and clear error messages
11. **Multiple WinForms forms** with navigation and event-driven behaviour
12. **Relational database** with proper relationships

## Explicitly Deferred from MVP (but documented)

- Inventory Adjustment (can be added early if time permits)
- Full user management UI (hard-coded admin user is acceptable for very first demo)
- Advanced report filters
- Soft-delete polish

## Success Criteria for MVP

A student (or instructor) can:

- Log in
- Add categories, suppliers, customers, and products
- Receive stock and sell stock
- See quantities and status update correctly
- Find low-stock items
- View history and generate a basic report
- Observe that invalid actions are blocked with clear messages

…all through a coherent Windows Forms application.
