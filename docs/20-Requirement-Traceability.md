# 20. Requirement Traceability

This matrix connects major business objectives to concrete functional requirements, business rules, use cases, and data requirements.

| Objective | Functional Requirements | Business Rules | Use Cases | Data Requirements |
|---|---|---|---|---|
| OBJ-001 Maintain product data | FR-PROD-001–009 | BR-001–006, BR-016, BR-018, BR-026–027 | UC-004–007 | DR-PROD, DR-CAT, DR-SUP |
| OBJ-002 Maintain categories | FR-CAT-001–004 | BR-007–008 | UC-009 | DR-CAT, DR-PROD |
| OBJ-003 Maintain suppliers | FR-SUP-001–004 | BR-009, BR-028 | UC-010 | DR-SUP, DR-PROD, DR-TXN |
| OBJ-004 Maintain customers | FR-CUS-001–004 | BR-010, BR-029 | UC-011 | DR-CUS, DR-TXN |
| OBJ-005 Authenticate and authorize users | FR-AUTH-001–008, FR-USR-001–006 | BR-014, BR-019–020 | UC-001, UC-002, UC-018 | DR-USR |
| OBJ-006 Record Stock-In | FR-SIN-001–006 | BR-012, BR-016–017, BR-021, BR-027–030 | UC-012 | DR-TXN, DR-PROD, DR-SUP, DR-USR |
| OBJ-007 Record Stock-Out | FR-SOUT-001–007 | BR-011–012, BR-016–017, BR-021, BR-025, BR-027, BR-029–030 | UC-013 | DR-TXN, DR-PROD, DR-CUS, DR-USR |
| OBJ-008 Correct inventory quantities | FR-ADJ-001–005 | BR-013–014, BR-017, BR-027, BR-030 | UC-014 | DR-TXN, DR-PROD, DR-USR |
| OBJ-009 Maintain accurate stock status | FR-STK-001–004 | BR-003–004, BR-015, BR-025–027 | UC-015 | DR-PROD, DR-TXN |
| OBJ-010 Search and filter information | FR-SRCH-001–005 | — | UC-008, UC-016 | Relevant entities |
| OBJ-011 Provide reports | FR-RPT-001–003 | — | UC-017 | DR-PROD, DR-TXN, DR-CAT, DR-SUP, DR-CUS |
| OBJ-012 Demonstrate C# / WinForms skills | WinForms requirements + CRUD/event requirements | Validation and business rules | All UI-driven use cases | All relevant entities |
| OBJ-013 Provide controlled temporary delegation | FR-DEL-001–011 | BR-031–039 | UC-019 | DR-DEL, DR-USR |

## Traceability Verification

- [x] Major objectives have concrete functional requirements.
- [x] Core functional requirements reference relevant business rules.
- [x] Core workflows are represented by explicit use-case IDs.
- [x] Delegation requirements are represented by explicit FR-DEL, BR-031–039, UC-019, and DR-DEL references.
- [x] Core data entities are connected to functionality.
- [x] Use-case IDs match the finalized Use Case Catalogue.
