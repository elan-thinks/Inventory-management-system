# 02 - System Architecture

**Document:** System Architecture  
**Version:** 1.0  

---

## 1. Chosen Architecture Style

**Simple Layered Architecture**

```
Presentation (WinForms Forms)
        |
        v
Business / Services
        |
        v
Data Access (ADO.NET)
        |
        v
Database (SQL Server LocalDB)

Models/Enums shared by Services and Data; used by Forms for binding
```

### Why this style?

- Educational, clear responsibilities
- Easy to unit-test services without UI
- Transparent SQL (no hidden ORM magic)
- Forms never contain business rules or SQL

### Rejected alternatives

Monolithic Form with all SQL; full Repository+UoW+DI; Entity Framework Core; multi-project ceremony.

---

## 2. Layer Responsibilities

| Layer | Responsibility |
|-------|----------------|
| Presentation | Display, input, events; call services; show messages; permission-based visibility only |
| Services | Business rules, authorization, orchestration, transactions |
| Data Access | Connections, parameterized SQL, mapping, participate in transactions |
| Models | POCOs + enums; no DB/UI knowledge |
| Database | FKs, UNIQUE, CHECK constraints |

---

## 3. Dependency Rules

**Allowed:** Forms->Services, Forms->Models, Services->Data, Services->Models, Data->Models  
**Forbidden:** Forms->Data/DB, Data->Services/Forms, Models->anything else, circular deps

---

## 4. Data / Error / Authorization Flow

Happy path: Form gathers data -> Service validates + authorizes -> begins SqlTransaction -> Data inserts history + updates quantity -> Commit -> Form confirms.

Errors: typed exceptions bubble to Form; user-friendly messages only.

Authorization: HasPermission at start of every sensitive service method (role U valid delegations).

---

## ADR-001

Simple layered architecture with folders inside one WinForms project.
