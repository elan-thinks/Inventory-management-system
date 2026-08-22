# 01 - Technical Design Overview

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

- Enterprise distributed systems, microservices, CQRS, event sourcing
- Cloud deployment, Docker, Kubernetes
- Heavy dependency injection containers
- WPF or third-party UI frameworks
- Features outside the approved SRS scope

---

## 4. Summary of Key Technical Decisions

| Decision Area | Choice |
|---------------|--------|
| Architecture | Simple layered (Presentation -> Services -> Data Access -> Database) |
| Solution shape | Single WinForms project with logical folders |
| Database engine | SQL Server LocalDB |
| Data access | ADO.NET (parameterized) |
| Authentication | Username + salted hash (PBKDF2) |
| Authorization | Role + effective permissions (permanent role union valid delegations) |
| Inventory quantity | Single source of truth on Product.QuantityOnHand |
| History | Append-only InventoryTransaction table |

---

## 5. High-Level System Behaviour

1. User launches application -> Login form.
2. Successful authentication loads User + permanent Role + currently valid Delegations -> CurrentUser context.
3. Main shell appears according to effective permissions.
4. All operations go through service classes that validate, authorize, apply business rules, and use transactions for quantity changes.
5. Inventory history is never edited or deleted.
6. Logout clears CurrentUser and returns to Login.

---

## 6. Success Criteria

- Every SRS requirement has a technical destination.
- SCR-020 Delegation Management is fully specified.
- Effective permissions = permanent role + currently valid delegations.
- Stock quantity can never go negative through normal operations.
- InventoryTransaction is append-only.
