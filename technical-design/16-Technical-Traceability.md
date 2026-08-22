# 16 — Technical Traceability Matrix

**Document:** Requirement → Technical Destination  
**Version:** 1.0  

This matrix links major SRS requirements to their technical homes.  
It is not exhaustive for every sub-bullet; it proves coverage of every functional area.

---

## Authentication & Session

| SRS | Product/UX | Screen | Technical Component | DB Entity | Milestone | Test |
|-----|------------|--------|---------------------|-----------|-----------|------|
| FR-AUTH-001…008 | Login flow | SCR-001 | AuthService, LoginForm, PasswordHasher | User | 9 | Login success/fail, logout |
| FR-USR-006 | — | SCR-001 | AuthService (IsActive check) | User | 9 | Deactivated user blocked |

## Dashboard

| SRS | Screen | Component | Milestone |
|-----|--------|-----------|-----------|
| FR-DASH-001…003 | SCR-002 | DashboardService, DashboardForm | 1 + 16 |

## Product

| SRS | Screen | Service / Form | DB | Milestone |
|-----|--------|----------------|-----|-----------|
| FR-PROD-001…009 | SCR-003,004,005 | ProductService, Product*Form | Product | 8 |
| BR-026,027 | — | ProductService.Create / StockService | Product | 8, 11 |

## Category / Supplier / Customer

| SRS | Screens | Service | Milestone |
|-----|---------|---------|-----------|
| FR-CAT-001…004 | SCR-006,007 | CategoryService | 5 |
| FR-SUP-001…004 | SCR-008,009 | SupplierService | 6 |
| FR-CUS-001…004 | SCR-010,011 | CustomerService | 7 |

## Stock-In / Stock-Out / Adjustment

| SRS | Screen | Service | DB | Milestone |
|-----|--------|---------|-----|-----------|
| FR-SIN-001…006 | SCR-012 | StockService.RecordStockIn | InventoryTransaction, Product | 11 |
| FR-SOUT-001…007 | SCR-013 | StockService.RecordStockOut | InventoryTransaction, Product | 12 |
| FR-ADJ-001…005 | SCR-014 | StockService.AdjustInventory | InventoryTransaction, Product | 14 |
| BR-011,025,030 | — | StockService + transaction | — | 12, 14 |

## Stock Status & Search

| SRS | Screen | Component | Milestone |
|-----|--------|-----------|-----------|
| FR-STK-001…004 | SCR-003,004 | Product.StockStatus, filters | 8 |
| FR-SRCH-001…005 | List forms, SCR-015 | Service filter methods | 5–13 |

## Reporting

| SRS | Screens | Service | Milestone |
|-----|---------|---------|-----------|
| FR-RPT-001…003 | SCR-016,017 | ReportService | 16 |

## User Management

| SRS | Screens | Service | Milestone |
|-----|---------|---------|-----------|
| FR-USR-001…006 | SCR-018,019 | UserService | 9–10 |

## Delegation (v1.1)

| SRS | Screen | Service / Form | DB | Milestone |
|-----|--------|----------------|-----|-----------|
| FR-DEL-001…014 | SCR-020 | DelegationService, DelegationManagementForm, AuthorizationService | Delegation | 15 |
| BR-031…039 | — | same | Delegation | 15 |
| VAL-023…029 | SCR-020 | DelegationService validation | — | 15 |

## Data & History

| SRS / DR | Technical Home |
|----------|----------------|
| DR-USR … DR-DEL | 04-Database-Design, 06-Domain-Model |
| BR-030 append-only | InventoryTransactionRepository (no Update/Delete) |
| Audit attribution | UserID on every transaction / delegation |

---

## Coverage Statement

- Every functional requirement area in SRS v1.1 has a technical destination.
- SCR-020 is fully specified.
- Effective permissions are designed.
- Inventory quantity rules and append-only history are preserved.
- No unsupported features were introduced.
