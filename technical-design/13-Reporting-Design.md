# 13 — Reporting Design

**Document:** Reports  
**Version:** 1.0  
**Scope:** Only the reports approved in FR-RPT  

---

## 1. Approved Reports

1. Current Inventory
2. Low Stock
3. Out-of-Stock
4. Stock-In (date-range filter)
5. Stock-Out (date-range filter)
6. Inventory Transaction History (date-range + type + product filters)

MVP: view on screen + print. Export is out of scope.

---

## 2. Technical Approach

- ReportService contains methods that return `List<T>` or DataTable of the required shape.
- ReportsHubForm (SCR-016) lets the user choose report type and set filters.
- ReportViewerForm (SCR-017) displays the result in a DataGridView (or a simple formatted panel) and offers Print (PrintDocument / PrintPreviewDialog is sufficient for a university project).

No Crystal Reports or external reporting engine is required.

---

## 3. Query Outline

- **Current Inventory** — SELECT from Product (active) with calculated StockStatus, Category, Supplier.
- **Low Stock / Out-of-Stock** — same with WHERE QuantityOnHand conditions.
- **Stock-In / Stock-Out** — SELECT from InventoryTransaction WHERE TransactionType = … AND TransactionDate BETWEEN @From AND @To.
- **Full History** — InventoryTransaction with optional filters.

All queries are parameterized and executed via the Data Access layer.

---

## 4. Authorization

- Administrator: all reports.
- InventoryStaff: Current Inventory, Low Stock, Out-of-Stock, Transaction History (and Stock-In/Out reports if granted by permanent role or ReportAccess delegation).

Service checks permission before running the query.
