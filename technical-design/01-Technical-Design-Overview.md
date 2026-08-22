# 01 — Technical Design Overview

**Document:** Technical Design Overview  
**Version:** 1.0  
**Status:** Approved  

---

## 1. Project Identity

| Item | Value |
|------|-------|
| System Name | NovaTech Electronics Inventory Management System |
| Short Name | NovaTech IMS |
| Technology | C# · .NET (Windows Forms) · Relational Database |
| Course Context | University Windows Programming / Software Engineering |
| Requirements Baseline | SRS v1.1 + Product/UX v1.1 + Visual Design v1.1 |

---

## 2. Design Goals

1. Faithfully implement every approved functional requirement (including FR-DEL and SCR-020).
2. Demonstrate solid C# fundamentals: classes, properties, constructors, encapsulation, enums, collections, exception handling, events, validation.
3. Demonstrate clean WinForms usage: Forms, DataGridView, ComboBox, NumericUpDown, DateTimePicker, MessageBox, event handlers.
4. Demonstrate proper relational database interaction with parameterized SQL and transactions.
5. Keep architecture simple enough that a student can understand every layer and why it exists.
6. Enforce business rules and authorization in code, not only in the UI.
7. Preserve append-only inventory history and correct stock quantity management.

---

## 3. Explicit Non-Goals

- Enterprise distributed systems
- Microservices, CQRS, event sourcing
- Cloud deployment, Docker, Kubernetes
- Heavy dependency injection containers
- Repository + Unit-of-Work abstractions that add no educational value
- WPF, third-party UI frameworks, or custom-drawn controls
- Features outside the approved SRS scope

---

## 4. Summary of Key Technical Decisions

| Decision Area | Choice | Rationale |
|---------------|--------|-----------|
| Architecture | Simple layered (Presentation → Services → Data Access → Database) | Clear responsibilities, easy to teach, realistic for WinForms |
| Solution shape | Single WinForms project with logical folders | Avoids multi-project complexity for a course project |
| Database engine | SQL Server LocalDB | Full T-SQL, transactions, referential integrity, easy Visual Studio integration, free, common in university labs |
| Data access | ADO.NET (SqlConnection, SqlCommand, SqlParameter, SqlTransaction, SqlDataReader) | Transparent SQL learning; no hidden magic |
| Authentication | Username + salted hash (PBKDF2 or equivalent via Rfc2898DeriveBytes) | Satisfies FR-AUTH-007 without enterprise identity servers |
| Authorization | Role + effective permissions (permanent role ∪ valid delegations) | Implements SRS v1.1 delegation model correctly |
| Inventory quantity | Single source of truth on Product.QuantityOnHand; changes only inside transactional services | Prevents negative stock and race conditions at the level appropriate for this project |
| History | Append-only InventoryTransaction table | Satisfies BR-030 |

Full Architecture Decision Records appear in later documents and are summarised in the final audit.

---

## 5. High-Level System Behaviour

1. User launches application → Login form.
2. Successful authentication loads User + permanent Role + currently valid Delegations → CurrentUser context.
3. Main shell (Dashboard + navigation) appears according to effective permissions.
4. All master-data and inventory operations go through service classes that:
   - validate input,
   - check authorization,
   - execute business rules,
   - perform database work inside transactions where quantity is affected,
   - return clear results or throw typed business exceptions.
5. Inventory history is never edited or deleted.
6. Logout clears CurrentUser and returns to Login.

---

## 6. Success Criteria for the Technical Design

- Every SRS requirement has a technical destination (see 16-Technical-Traceability.md).
- SCR-020 Delegation Management is fully specified.
- Effective permissions = permanent role + currently valid delegations.
- Stock quantity can never go negative through normal operations.
- InventoryTransaction is append-only.
- A student following the milestones can implement the system without inventing architecture.

---

## 7. Document Ownership

This technical design package is the authoritative guide for implementation on branch `version1.1`.  
Requirements documents remain the source of truth for *what* the system must do; this package defines *how* it will be structured in C# WinForms.
