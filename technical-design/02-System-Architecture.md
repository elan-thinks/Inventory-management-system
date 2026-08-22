# 02 — System Architecture

**Document:** System Architecture  
**Version:** 1.0  

---

## 1. Chosen Architecture Style

**Simple Layered Architecture**

```
┌─────────────────────────────────────┐
│  Presentation Layer (WinForms)      │  Forms, controls, event handlers,
│  NovaTechIMS.Forms / UI             │  CurrentUser context, display only
└─────────────────┬───────────────────┘
                  │ calls
┌─────────────────▼───────────────────┐
│  Business / Service Layer           │  Validation, business rules,
│  NovaTechIMS.Services               │  authorization, orchestration
└─────────────────┬───────────────────┘
                  │ calls
┌─────────────────▼───────────────────┐
│  Data Access Layer                  │  ADO.NET, SQL, transactions,
│  NovaTechIMS.Data                   │  mapping readers → models
└─────────────────┬───────────────────┘
                  │
┌─────────────────▼───────────────────┐
│  Database (SQL Server LocalDB)      │  Tables, FKs, constraints
└─────────────────────────────────────┘

Models / Enums (NovaTechIMS.Models) are shared (read by Services & Data; used by Forms for binding)
```

### Why this style?

- Matches the educational goals of a Windows Programming course.
- Every layer has a single, explainable responsibility.
- Easy to unit-test services without the UI.
- No hidden frameworks; students see the SQL and the C# that executes it.
- Sufficient separation so that Forms do not contain business rules or raw SQL.

### Alternatives considered and rejected

| Alternative | Reason rejected |
|-------------|-----------------|
| Single giant Form with all SQL | Unmaintainable, teaches bad habits |
| Full Repository + Unit of Work + DI container | Over-engineering for this scope |
| Entity Framework Core | Hides SQL; less transparent for learning ADO.NET concepts required by many courses |
| Multi-project solution with interfaces everywhere | Unnecessary ceremony |

---

## 2. Layer Responsibilities

### 2.1 Presentation (WinForms)

- Display data and collect user input.
- Raise events (button clicks, form load, selection changed).
- Call service methods.
- Show MessageBox / inline validation messages.
- Enable/disable or hide controls according to CurrentUser effective permissions (UX only).
- Never contain business rules that decide whether an operation is allowed.
- Never contain SQL.

### 2.2 Business / Service Layer

- Own all business rules (BR-001 … BR-039).
- Perform authorization checks (permanent role + valid delegations).
- Coordinate multi-step operations (especially stock movements) inside transactions.
- Validate domain rules that cannot be expressed only in the UI.
- Throw clear, typed exceptions (ValidationException, BusinessRuleException, AuthorizationException).
- Return models or simple result objects to the UI.

### 2.3 Data Access Layer

- Open/close connections.
- Execute parameterized commands.
- Map SqlDataReader rows to model objects.
- Participate in SqlTransaction when requested by a service.
- Contain no business rules beyond basic null checks required for safe mapping.

### 2.4 Models

- Plain C# classes representing domain entities.
- Properties, constructors, simple calculated properties (e.g., StockStatus).
- No database connection knowledge.
- No UI knowledge.

### 2.5 Database

- Enforce referential integrity, uniqueness, and non-null constraints.
- Act as the final safety net (CHECK constraints for QuantityOnHand ≥ 0, etc.).

---

## 3. Dependency Rules

**Allowed**

- Forms → Services
- Forms → Models (for binding / display)
- Services → Data Access
- Services → Models
- Data Access → Models
- Data Access → Database (via ADO.NET)

**Forbidden**

- Forms → Data Access (direct SQL from UI)
- Forms → Database
- Data Access → Services
- Data Access → Forms
- Models → Services / Data / Forms
- Circular dependencies between any layers

---

## 4. Data Flow (Happy Path)

1. User action on Form (e.g., Save Stock-In).
2. Form gathers values into a model or parameter object.
3. Form calls `StockService.RecordStockIn(...)`.
4. Service validates inputs and authorization.
5. Service begins a SqlTransaction.
6. Service asks Data Access to insert InventoryTransaction and update Product.QuantityOnHand.
7. Data Access executes parameterized commands inside the transaction.
8. Service commits.
9. Service returns success; Form shows confirmation and refreshes.

### Error flow

Any failure (validation, insufficient stock, authorization, database) throws a typed exception.  
Service rolls back the transaction if one was started.  
Form catches the exception and displays a user-friendly message.  
Technical details are logged (or Debug.WriteLine in this educational context) but never shown to the end user as a stack trace.

### Authorization flow

1. At login, CurrentUser is populated with Role + list of currently valid Delegations.
2. Before any sensitive service method runs, the service calls `AuthorizationService.HasPermission(CurrentUser, requiredPermission)`.
3. HasPermission evaluates: permanent role grants **OR** a valid active, non-expired, non-revoked delegation grants the permission.
4. If false → AuthorizationException.

UI may also hide buttons, but the service is the authoritative gate.

---

## 5. Cross-Cutting Concerns

| Concern | Handled by |
|---------|------------|
| Validation | UI (immediate feedback) + Service (authoritative) + Database (constraints) |
| Authorization | Service layer (mandatory) + UI (visibility) |
| Transactions | Service initiates; Data Access participates |
| Audit / attribution | Service always passes CurrentUser.UserID into history records |
| Error messages | Service produces domain messages; Form displays them |

---

## 6. Scalability & Concurrency Note

This is a single-user / low-concurrency desktop application typical of a university lab.  

Race conditions on QuantityOnHand are mitigated by:

- Performing the quantity check and the update inside the same database transaction.
- Using a conditional UPDATE (`WHERE QuantityOnHand >= @Qty`) for Stock-Out so that concurrent changes are detected.

No distributed locking or optimistic concurrency tokens beyond this are required.

---

## 7. Architecture Decision Record

**ADR-001 — Architecture Style**

- **Context:** Need a clear structure for a C# WinForms student project that still separates UI from business rules and data access.
- **Decision:** Simple layered architecture with folders inside one project.
- **Alternatives:** Monolithic Form, full Clean Architecture multi-project, Onion, etc.
- **Consequences:** Easy to teach and to implement; sufficient separation; no ceremony.
