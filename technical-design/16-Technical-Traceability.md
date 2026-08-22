# 16 — Technical Traceability Matrix

**Document:** Requirement → Technical Destination  
**Version:** 1.0  

This matrix links major SRS requirements to their technical homes.

---

## Authentication & Session

| SRS | Screen | Technical Component | DB | Milestone |
|-----|--------|---------------------|-----|----------|
| FR-AUTH-001…008 | SCR-001 | AuthService, LoginForm, PasswordHasher | User | 9 |
| FR-USR-006 | SCR-001 | AuthService (IsActive check) | User | 9 |

## Dashboard

| SRS | Screen | Component | Milestone |
|-----|--------|-----------|----------|
| FR-DASH-001…003 | SCR-002 | DashboardService, DashboardForm | 1 + 16 |

## Product / Category / Supplier / Customer

| SRS | Screens | Service | Milestone |
|-----|---------|---------|----------|
| FR-PROD-001…009 | SCR-003,004,005 | ProductService | 8 |
| FR-CAT-001…004 | SCR-006,007 | CategoryService | 5 |
| FR-SUP-001…004 | SCR-008,009 | SupplierService | 6 |
| FR-CUS-001…004 | SCR-010,011 | CustomerService | 7 |

## Stock-In / Stock-Out / Adjustment

| SRS | Screen | Service | Milestone |
|-----|--------|---------|----------|
| FR-SIN-001…006 | SCR-012 | StockService.RecordStockIn | 11 |
| FR-SOUT-001…007 | SCR-013 | StockService.RecordStockOut | 12 |
| FR-ADJ-001…005 | SCR-014 | StockService.AdjustInventory | 14 |

## Delegation (v1.1)

| SRS | Screen | Service / Form | Milestone |
|-----|--------|----------------|----------|
| FR-DEL-001…014 | SCR-020 | DelegationService, DelegationManagementForm, AuthorizationService | 15 |
| BR-031…039 | — | same | 15 |

## Coverage Statement

- Every functional requirement area in SRS v1.1 has a technical destination.
- SCR-020 is fully specified.
- Effective permissions are designed.
- Inventory quantity rules and append-only history are preserved.
