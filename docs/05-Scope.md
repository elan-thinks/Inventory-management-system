# 5. System Scope

## 5.1 In Scope (Version 1.0 / MVP)

The first complete version of the system **shall** support:

### Master Data
- Product management (full CRUD + search + filter)
- Category management (full CRUD)
- Supplier management (full CRUD)
- Customer management (full CRUD)

### Inventory Operations
- Stock-In transactions (receipt of goods from suppliers)
- Stock-Out transactions (sales / issues to customers)
- Inventory adjustments (quantity corrections with reason)
- Automatic update of current stock quantity
- Low-stock and out-of-stock detection and display

### User Interface & Navigation
- Login / Logout
- Role-based access (Administrator vs Inventory Staff)
- Main dashboard with key metrics
- Multiple forms with clear navigation
- DataGridView-based listing screens
- Search and filter capabilities on major screens
- Confirmation dialogs for destructive actions
- Validation feedback via message boxes / labels

### Reporting
- Current Inventory Report
- Low Stock Report
- Out-of-Stock Report
- Stock-In Report (with date filter)
- Stock-Out Report (with date filter)
- Inventory Transaction History Report

### Technical / Educational
- Local relational database
- Full demonstration of CRUD
- Input validation
- Exception handling
- Event-driven WinForms programming
- Meaningful relationships between entities

## 5.2 Out of Scope

The following are **intentionally excluded** from Version 1.0:

- Multi-branch / multi-warehouse inventory
- Barcode scanning or RFID
- Automatic reorder / purchase order generation
- Integration with accounting, POS, or e-commerce systems
- Cloud synchronisation or multi-device concurrent editing beyond basic local use
- Advanced analytics, forecasting, or machine learning
- Customer loyalty / points systems
- Supplier portals or external APIs
- Mobile applications or web interfaces
- Complex permission matrices beyond two roles
- Soft-delete with complex recovery workflows (simple delete rules are in scope)
- Batch import/export of large product lists (beyond basic reporting)
- Multi-currency support
- Tax calculation engines
- Serial number / lot tracking at individual item level

## 5.3 Scope Control Statement

Any feature request that introduces enterprise architecture, distributed systems, cloud infrastructure, or advanced AI capabilities is considered out of scope for this university project and must be deferred to Future Enhancements.
