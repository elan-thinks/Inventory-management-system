# 20. Requirement Traceability

High-level mapping (illustrative — full matrix can be expanded during implementation planning).

| Business Objective | Key Functional Requirements | Key Business Rules | Key Use Cases | Key Data Entities |
|--------------------|-----------------------------|--------------------|---------------|-------------------|
| OBJ-001 Maintain product data | FR-PROD-* | BR-001–006, BR-018 | UC-004, UC-005, UC-006 | Product, Category, Supplier |
| OBJ-002 Maintain categories, suppliers, customers | FR-CAT-*, FR-SUP-*, FR-CUS-* | BR-007–010 | UC-008 | Category, Supplier, Customer |
| OBJ-003 Record stock movements | FR-SIN-*, FR-SOUT-*, FR-ADJ-* | BR-011–014, BR-017, BR-025 | UC-009, UC-010, UC-011 | InventoryTransaction |
| OBJ-004 Accurate current stock | FR-STK-005 | BR-003, BR-015 | UC-009–011 | Product.QuantityOnHand |
| OBJ-005 Low/Out of stock detection | FR-STK-001–004, FR-DASH-006/007 | BR-015 | UC-012 | Product |
| OBJ-006 Search & filter | FR-SRCH-* | — | UC-007, UC-013 | All list entities |
| OBJ-007 Dashboard | FR-DASH-* | — | UC-003 | Aggregates |
| OBJ-008 Basic reports | FR-RPT-* | — | UC-014 | All |
| OBJ-009 Enforce rules & integrity | VAL-*, ER-*, many FR | All BR-* | All | All |
| OBJ-010 Demonstrate C# / WinForms skills | All + WinForms requirements | — | All | — |

This structure allows verification that every major objective is supported by concrete requirements, rules, behaviours, and data.
