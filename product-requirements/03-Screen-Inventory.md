# 3. Screen Inventory

19 screens fully cover the SRS v1.0 MVP. This count maps directly onto the "Forms Required (Minimum)" list in SRS §16.1, splitting each "List + Add/Edit" bullet into two screens for clarity, and adding a Product Details view and a Reports Hub (report-type selector) ahead of the Report Viewer.

| ID | Screen | Purpose | Role(s) | Entry | Exit / Navigation | Main Actions | Related SRS |
|---|---|---|---|---|---|---|---|
| SCR-001 | Login | Authenticate and load role | All pre-auth | App launch | Success → SCR-002 | Login | FR-AUTH-001–008 |
| SCR-002 | Dashboard | Orientation hub: metrics, recent activity, quick nav | Admin, Staff | Post-login; sidebar | Sidebar to accessible modules | View metrics, recent activity, quick actions | FR-DASH-001–003 |
| SCR-003 | Product List | Browse, search, filter products | Admin, Staff | Sidebar Products | Admin Add → SCR-005; row → SCR-004 | Admin: Add, Edit, Delete/Deactivate, View; Staff: Search, Filter, View | FR-PROD-004,007,008; FR-SRCH-001–002; FR-STK-002 |
| SCR-004 | Product Details | View full detail of one product incl. calculated status | Admin, Staff | Product row / View | Admin Edit → SCR-005; Back → SCR-003 | View; Admin Edit/Delete/Deactivate | FR-PROD-004; FR-STK-002 |
| SCR-005 | Product Add/Edit | Create or update product master data | Admin only | Admin Add/Edit from SCR-003/004 | Save/Cancel → SCR-003 | Enter/change fields, Save, Cancel | FR-PROD-001–003,005,009; BR-001–006, BR-026–027; VAL-002,003,009,011,012 |
| SCR-006 | Category List | Browse, search categories | Admin, Staff | Sidebar Categories | Admin Add/Edit → SCR-007 | Admin: Add, Edit, Delete/Deactivate; Staff: Search, View | FR-CAT-001–004 |
| SCR-007 | Category Add/Edit | Create/update category | Admin only | Admin Add/Edit from SCR-006 | Save/Cancel → SCR-006 | Enter fields, Save, Cancel | FR-CAT-001–004; BR-007–008; VAL-004 |
| SCR-008 | Supplier List | Browse, search suppliers | Admin, Staff | Sidebar Suppliers | Admin Add/Edit → SCR-009 | Admin: Add, Edit, Delete/Deactivate; Staff: Search, View | FR-SUP-001–004 |
| SCR-009 | Supplier Add/Edit | Create/update supplier | Admin only | Admin Add/Edit from SCR-008 | Save/Cancel → SCR-008 | Enter fields, Save, Cancel | FR-SUP-001–004; BR-009; VAL-005,006,013,014 |
| SCR-010 | Customer List | Browse, search customers | Admin, Staff | Sidebar Customers | Admin Add/Edit → SCR-011 | Admin: Add, Edit, Delete/Deactivate; Staff: Search, View | FR-CUS-001–004 |
| SCR-011 | Customer Add/Edit | Create/update customer | Admin only | Admin Add/Edit from SCR-010 | Save/Cancel → SCR-010 | Enter fields, Save, Cancel | FR-CUS-001–004; BR-010; VAL-007,008,013,014 |
| SCR-012 | Stock-In | Record goods received | Admin, Staff | Sidebar Stock In / Dashboard | Success → reset or history | Product/supplier selection, quantity, price, date, notes, Save | FR-SIN-001–006; BR-012,016–017,021,027–030; VAL-009,010,015,019 |
| SCR-013 | Stock-Out | Record goods issued; customer optional | Admin, Staff | Sidebar Stock Out / Dashboard | Success → reset or history | Product, optional customer, quantity, price, date, notes, Save | FR-SOUT-001–007; BR-011–012,016–017,021,025,027,029–030; VAL-009,010,015,019,020 |
| SCR-014 | Inventory Adjustment | Correct stock quantity with mandatory reason | Admin only | Sidebar Inventory Adjustment | Save → SCR-015 | Product, previous/new quantity, difference, reason, Save | FR-ADJ-001–005; BR-013–014,017,027,030; VAL-012,018 |
| SCR-015 | Inventory Transaction History | Read-only, filterable log of all stock movements | Admin, Staff | Sidebar Inventory History / post-transaction | Stay / Print | Filter, View, Print; no Edit/Delete | FR-SRCH-003; FR-RPT-001–003; BR-030 |
| SCR-016 | Reports Hub | Choose report type and filters | Admin, Staff | Sidebar Reports | Generate → SCR-017 | Select report, set filters, Generate | FR-RPT-001–003 |
| SCR-017 | Report Viewer | Display and print generated report | Admin, Staff | Generate from SCR-016 | Back → SCR-016 | View, Print | FR-RPT-001–003 |
| SCR-018 | User Management List | Browse and search user accounts | Admin only | Sidebar User Management | Add/Edit → SCR-019 | Search, Add, Edit, Deactivate | FR-USR-001–006 |
| SCR-019 | User Add/Edit | Create/update user account and role | Admin only | Add/Edit from SCR-018 | Save/Cancel → SCR-018 | Enter fields, assign role, Save, Cancel | FR-USR-001–006; BR-019–020; VAL-016,017 |

## Master-data permission rule

Inventory/Store Staff are view/search-only for Products, Categories, Suppliers, and Customers. Add, Edit, Delete, and Deactivate actions are Administrator-only. This is the approved resolution of the SRS ambiguity and is recorded as UX-DEC-001.

## Inventory permissions

Both Admin and Staff can use Stock-In and Stock-Out. Only Admin can use Inventory Adjustment. Transaction History is read-only for both roles.
