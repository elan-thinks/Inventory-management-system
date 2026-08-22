# Milestone 2 Gate — Models and Enums

**Status:** COMPLETE  

**Branch:** `version1.2`  

---

## Objective

Introduce the domain model layer as plain C# classes and enums that match SRS Data Requirements and Technical Design §06. No database, no services, no UI changes required.

---

## What was implemented

### Enums (`Models/Enums/`)

| Enum | Values |
|------|--------|
| `UserRole` | Administrator, InventoryStaff |
| `TransactionType` | StockIn, StockOut, Adjustment |
| `StockStatus` | OutOfStock, LowStock, InStock (derived) |
| `DelegationStatus` | Active, Expired, Revoked |
| `DelegatableResponsibility` | StockIn, StockOut, ReportAccess |

### Models (`Models/`)

| Class | Notes |
|-------|--------|
| `User` | Includes PasswordHash + PasswordSalt (hashing later) |
| `Category` | Soft-delete via IsActive |
| `Supplier` | Default supplier for products |
| `Customer` | Optional on Stock-Out |
| `Product` | Qty starts at 0; `StockStatus` computed property |
| `InventoryTransaction` | Append-only shape; adjustment fields optional |
| `Delegation` | Time-bounded grant; does not change permanent role |
| `CurrentUser` | Session snapshot; EffectivePermissions list for later auth |

---

## Intentionally NOT implemented

- Database / SQL / LocalDB
- Data access / repositories
- Services / business rules enforcement
- Authentication or password hashing
- Binding models to forms

---

## Manual verification

| Check | Expected |
|-------|----------|
| Solution builds | No errors |
| App still runs | LoginForm → MainForm unchanged |
| Models folder | Enums + POCOs present |

---

## Next milestone

**Milestone 3 — Database schema (SQL Server LocalDB)**
