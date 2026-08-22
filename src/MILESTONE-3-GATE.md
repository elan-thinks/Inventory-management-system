# Milestone 3 Gate — Database Schema (LocalDB)

**Status:** COMPLETE  

**Branch:** `version1.2`  

---

## Objective

Create the physical SQL Server schema that matches Technical Design §04 and SRS Data Requirements. No application connection, ADO.NET, or CRUD yet (Milestone 4+).

---

## What was implemented

| Script | Purpose |
|--------|---------|
| `database/01-CreateDatabase.sql` | Creates `NovaTechIMS` database |
| `database/02-CreateTables.sql` | All tables, FKs, CHECKs, indexes |
| `database/03-SeedOptional.sql` | Sample Category / Supplier / Customer only |
| `database/README.md` | How to run scripts |

### Tables

`User`, `Category`, `Supplier`, `Customer`, `Product`, `InventoryTransaction`, `Delegation`

### Key constraints

- Unique Username, CategoryName
- Role / TransactionType / Responsibility / Status CHECKs
- Prices ≥ 0, QuantityOnHand ≥ 0, MinStock ≥ 0
- Product FKs to Category + default Supplier
- Transaction FKs; SupplierID on txn = actual Stock-In supplier
- Delegation EndDate ≥ StartDate; unique Active (ToUser, Responsibility)
- ON DELETE NO ACTION (soft-delete preferred)

---

## Intentionally NOT implemented

- App.config / connection string usage in code
- DbConnectionFactory / ADO.NET
- Repositories / services
- User seed with password hashes

---

## How to verify locally

1. Connect to `(localdb)\MSSQLLocalDB`
2. Run `01`, then `02`, optionally `03`
3. Confirm seven tables exist
4. Application still builds and runs (shell unchanged)

---

## Next milestone

**Milestone 4 — Connection + data access skeleton**
