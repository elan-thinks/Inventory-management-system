# 20. Requirement Traceability

This matrix connects major business objectives to concrete functional requirements, business rules, use cases, and data requirements. It is intentionally requirement-level for the core MVP rather than using wildcard-only mappings.

| Objective | Functional Requirements | Business Rules | Use Cases | Data Requirements |
|---|---|---|---|---|
| OBJ-001 Maintain product data | FR-PROD-001–009 | BR-001–006, BR-016, BR-018, BR-026–028 | Product CRUD use cases | DR-PROD, DR-CAT, DR-SUP |
| OBJ-002 Maintain categories | FR-CAT-001–004 | BR-007, BR-008 | Category CRUD use cases | DR-CAT, DR-PROD |
| OBJ-003 Maintain suppliers | FR-SUP-001–004 | BR-009, BR-028 | Supplier CRUD use cases | DR-SUP, DR-PROD, DR-TXN |
| OBJ-004 Maintain customers | FR-CUS-001–004 | BR-010, BR-029 | Customer CRUD use cases | DR-CUS, DR-TXN |
| OBJ-005 Authenticate and authorize users | FR-AUTH-001–008, FR-USR-001–006 | BR-014, BR-019, BR-020 | Login, Logout, User Management use cases | DR-USR |
| OBJ-006 Record Stock-In | FR-SIN-001–006 | BR-012, BR-016, BR-017, BR-021, BR-027, BR-028, BR-030 | Stock-In use case | DR-TXN, DR-PROD, DR-SUP, DR-USR |
| OBJ-007 Record Stock-Out | FR-SOUT-001–007 | BR-011, BR-012, BR-016, BR-017, BR-021, BR-025, BR-027, BR-029, BR-030 | Stock-Out use case | DR-TXN, DR-PROD, DR-CUS, DR-USR |
| OBJ-008 Correct inventory quantities | FR-ADJ-001–005 | BR-013, BR-014, BR-017, BR-027, BR-030 | Inventory Adjustment use case | DR-TXN, DR-PROD, DR-USR |
| OBJ-009 Maintain accurate stock status | FR-STK-001–004 | BR-003, BR-004, BR-015, BR-025–027 | Stock monitoring use cases | DR-PROD, DR-TXN |
| OBJ-010 Search and filter information | FR-SRCH-001–005 | — | Search/filter use cases | Relevant list entities |
| OBJ-011 Provide reports | FR-RPT-001–003 | — | Reporting use cases | DR-PROD, DR-TXN, DR-CAT, DR-SUP, DR-CUS |
| OBJ-012 Demonstrate C# / WinForms skills | WinForms-specific requirements + all CRUD/event requirements | Validation and business rules | All UI-driven use cases | All relevant entities |

## Traceability Verification

- [x] Major objectives have concrete functional requirements.
- [x] Core functional requirements reference relevant business rules.
- [x] Core workflows are represented by use cases.
- [x] Core data entities are connected to their functionality.
- [ ] Final use-case IDs will be rechecked after the use-case document is finalized.
