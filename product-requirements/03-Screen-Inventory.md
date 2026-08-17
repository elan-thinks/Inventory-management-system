# 3. Screen Inventory

The product contains 19 screens covering the approved SRS v1.0 MVP.

| ID | Screen | Roles | Key access |
|---|---|---|---|
| SCR-001 | Login | All | Authenticate |
| SCR-002 | Dashboard | Admin, Staff | View metrics and quick actions |
| SCR-003 | Product List | Admin, Staff | View/search; Admin manages |
| SCR-004 | Product Details | Admin, Staff | View; Admin manages |
| SCR-005 | Product Add/Edit | Admin only | Create/update product master data |
| SCR-006 | Category List | Admin, Staff | View/search; Admin manages |
| SCR-007 | Category Add/Edit | Admin only | Create/update category |
| SCR-008 | Supplier List | Admin, Staff | View/search; Admin manages |
| SCR-009 | Supplier Add/Edit | Admin only | Create/update supplier |
| SCR-010 | Customer List | Admin, Staff | View/search; Admin manages |
| SCR-011 | Customer Add/Edit | Admin only | Create/update customer |
| SCR-012 | Stock-In | Admin, Staff | Record goods received |
| SCR-013 | Stock-Out | Admin, Staff | Record goods issued; customer optional |
| SCR-014 | Inventory Adjustment | Admin only | Correct quantity with mandatory reason |
| SCR-015 | Inventory Transaction History | Admin, Staff | Read-only, filterable history |
| SCR-016 | Reports Hub | Admin, Staff | MVP reports according to role |
| SCR-017 | Report Viewer | Admin, Staff | View/print reports according to role |
| SCR-018 | User Management List | Admin only | Manage users |
| SCR-019 | User Add/Edit | Admin only | Create/edit/deactivate users |

## Master-data permission rule

Inventory/Store Staff are view/search-only for Products, Categories, Suppliers, and Customers. Add, Edit, Delete, and Deactivate actions are Administrator-only. This is the approved resolution of the SRS ambiguity and is recorded as UX-DEC-001.

## Inventory permissions

Both Admin and Staff can use Stock-In and Stock-Out. Only Admin can use Inventory Adjustment. Transaction History is read-only for both roles.
