# 4. System Objectives

## 4.1 Primary Objectives

| ID      | Objective                                                                 | Measurable Indicator |
|---------|---------------------------------------------------------------------------|----------------------|
| OBJ-001 | Enable authorised users to maintain accurate product master data          | Full CRUD for products with validation |
| OBJ-002 | Enable authorised users to maintain categories, suppliers, and customers  | Full CRUD for these entities |
| OBJ-003 | Accurately record all stock-in and stock-out transactions                 | Every movement updates quantity and is stored in history |
| OBJ-004 | Maintain correct current stock quantity for every product                 | Quantity always reflects sum of movements |
| OBJ-005 | Automatically identify low-stock and out-of-stock products                | Status calculated from quantity vs minimum level |
| OBJ-006 | Provide searchable and filterable views of products and transactions      | Users can locate records quickly by key fields |
| OBJ-007 | Provide a useful dashboard with key inventory metrics                     | Summary numbers and lists visible after login |
| OBJ-008 | Generate basic inventory and transaction reports                          | At least Current Inventory, Low Stock, and Transaction History reports |
| OBJ-009 | Enforce business rules and data integrity                                 | Invalid operations are rejected with clear messages |
| OBJ-010 | Demonstrate solid C# / WinForms / database skills appropriate for a university course | Multiple forms, event handling, CRUD, validation, relationships |

## 4.2 Secondary Objectives

| ID      | Objective                                                                 | Notes |
|---------|---------------------------------------------------------------------------|-------|
| OBJ-011 | Support inventory adjustments with reason and audit trail                 | Useful but not core to daily operations |
| OBJ-012 | Allow filtering of reports by date range and other criteria               | Improves usefulness of reports |
| OBJ-013 | Record basic audit information (who / when) on master data changes        | Nice-to-have for accountability |
| OBJ-014 | Provide a clean, consistent user experience across forms                  | Supports usability NFR |

## 4.3 Objectives That Are Explicitly Out of Scope

- Predictive analytics / AI-based demand forecasting
- Automatic purchase order generation
- Multi-warehouse or multi-branch inventory
- Barcode / RFID scanning
- Integration with accounting or e-commerce systems
- Mobile or web clients
- Advanced business intelligence dashboards
