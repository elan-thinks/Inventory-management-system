# 3. Screen Inventory

19 screens fully cover the SRS v1.0 MVP. This count maps directly onto the "Forms Required (Minimum)" list in SRS §16.1, splitting each "List + Add/Edit" bullet into two screens for clarity, and adding a Product Details view and a Reports Hub (report-type selector) ahead of the Report Viewer.

| Screen ID | Screen Name | Purpose | Primary User | Secondary User(s) | Entry Point | Exit / Navigation | Main Actions | Related Requirements | Related Use Cases |
|---|---|---|---|---|---|---|---|---|---|
| SCR-001 | Login | Authenticate and load role | All users (pre-auth) | — | Application launch | On success → Dashboard | Enter credentials, Login | FR-AUTH-001–008, FR-USR-006 | UC-001 |
| SCR-002 | Dashboard | Orientation hub: metrics, recent activity, quick nav | Admin, Staff | — | Post-login default; sidebar "Dashboard" | Sidebar to any module | View metrics, click into recent transaction, navigate | FR-DASH-001–003 | UC-003 |
| SCR-003 | Product List | Browse, search, filter products | Admin, Staff | — | Sidebar "Products" | Admin: Add → SCR-005; row → SCR-004; Delete confirm dialog | Admin: Add, Edit, Delete/Deactivate, View; Staff: Search, Filter, View | FR-PROD-004,007,008; FR-SRCH-001–002 | UC-005, UC-008 |
| SCR-004 | Product Details | View full detail of one product incl. calculated status | Admin, Staff | — | Row select / "View" on SCR-003 | Admin Edit → SCR-005; Back → SCR-003 | View fields; Admin Edit/Delete/Deactivate | FR-PROD-004, FR-STK-002 | UC-005 |
| SCR-005 | Product Add/Edit | Create or update product master data | Admin only | — | "Add" on SCR-003; "Edit" on SCR-004 | Save → SCR-003; Cancel → prior screen | Enter/change fields, Save, Cancel | FR-PROD-001–003,005,009; BR-001–006, BR-026–027; VAL-002,003,009,011,012 | UC-004, UC-006 |
| SCR-006 | Category List | Browse, search categories | Admin, Staff (view) | — | Sidebar "Categories" | Admin: Add/Edit/Delete; Staff: view/search | Search, View | FR-CAT-001–004 | UC-009 |
| SCR-007 | Category Add/Edit | Create/update category | Admin | Staff (view only, controls disabled) | "Add"/"Edit" on SCR-006 | Save/Cancel → SCR-006 | Enter fields, Save, Cancel | FR-CAT-001–004; BR-007–008; VAL-004 | UC-009 |
| SCR-008 | Supplier List | Browse, search suppliers | Admin, Staff (view) | — | Sidebar "Suppliers" | Admin: Add/Edit/Delete; Staff: view/search | Search, View | FR-SUP-001–004 | UC-010 |
| SCR-009 | Supplier Add/Edit | Create/update supplier | Admin | Staff (view only) | "Add"/"Edit" on SCR-008 | Save/Cancel → SCR-008 | Enter fields, Save, Cancel | FR-SUP-001–004; BR-009; VAL-005,006,013,014 | UC-010 |
| SCR-010 | Customer List | Browse, search customers | Admin, Staff (view) | — | Sidebar "Customers" | Admin: Add/Edit/Delete; Staff: view/search | Search, View | FR-CUS-001–004 | UC-011 |
| SCR-011 | Customer Add/Edit | Create/update customer | Admin | Staff (view only) | "Add"/"Edit" on SCR-010 | Save/Cancel → SCR-010 | Enter fields, Save, Cancel | FR-CUS-001–004; BR-010; VAL-007,008,013,014 | UC-011 |
| SCR-012 | Stock-In | Record goods received from a supplier | Admin, Staff | — | Sidebar "Stock In"; Dashboard quick action | Save → confirmation → SCR-012 (reset) or SCR-015 | Select product/supplier, enter qty/price/date/notes, Save | FR-SIN-001–006; BR-012,016–017,021,027–030; VAL-009,010,015,019 | UC-012 |
| SCR-013 | Stock-Out | Record goods issued to a customer (optional) | Admin, Staff | — | Sidebar "Stock Out"; Dashboard quick action | Save → confirmation → SCR-013 (reset) or SCR-015 | Select product, optional customer, enter qty/price/date/notes, Save | FR-SOUT-001–007; BR-011–012,016–017,021,025,027,029–030; VAL-009,010,015,019,020 | UC-013 |
| SCR-014 | Inventory Adjustment | Correct stock quantity with mandatory reason | Admin only | — | Sidebar "Inventory Adjustment" (hidden for Staff) | Save → confirmation → SCR-015 | Select product, view previous qty, enter new qty + reason, Save | FR-ADJ-001–005; BR-013–014,017,027,030; VAL-012,018 | UC-014 |
| SCR-015 | Inventory Transaction History | Read-only, filterable log of all stock movements | Admin, Staff | — | Sidebar "Inventory History"; post-save redirect from SCR-012/013/014 | Filter, Print; no edit/delete | Filter by date/type/product, view, print | FR-SRCH-003; FR-RPT-001–003; BR-030 | UC-016 |
| SCR-016 | Reports Hub | Choose a report type and its filters | Admin, Staff (subset) | — | Sidebar "Reports" | Generate → SCR-017 | Select report, set filters, Generate | FR-RPT-001–003 | UC-017 |
| SCR-017 | Report Viewer | Display and print a generated report | Admin, Staff (subset) | — | "Generate" on SCR-016 | Print; Back → SCR-016 | View, Print, adjust filters, Back | FR-RPT-001–003 | UC-017 |
| SCR-018 | User Management List | Browse and search user accounts | Admin only | — | Sidebar "User Management" (hidden for Staff) | Add → SCR-019; row → SCR-019 (edit) | Search, Add, Edit, Deactivate | FR-USR-001–006 | UC-018 |
| SCR-019 | User Add/Edit | Create/update user account and role | Admin only | — | "Add"/"Edit" on SCR-018 | Save/Cancel → SCR-018 | Enter fields, assign role, Save, Cancel | FR-USR-001–006; BR-019–020; VAL-016,017 | UC-018 |

## 3.1 Shared / Reusable Screens (Not Separately Numbered)

These are not distinct navigable screens but reusable modal components referenced across the table above and detailed in document 06:

- **Confirmation Dialog** (generic Yes/No pattern for Delete, Deactivate, Stock-Out, Adjustment, Logout-with-unsaved-changes)
- **Blocked-Delete Explanation Dialog** (BR-008, BR-009, BR-010, BR-018 — explains why deletion is blocked and offers Deactivate instead)
- **Session/Permission Error Dialog** (ER-011)

## 3.2 Master-data Permission Rule

Inventory/Store Staff are view/search-only for Products, Categories, Suppliers, and Customers. Add, Edit, Delete, and Deactivate actions are Administrator-only. This is the approved resolution of the SRS ambiguity and is recorded as UX-DEC-001.
