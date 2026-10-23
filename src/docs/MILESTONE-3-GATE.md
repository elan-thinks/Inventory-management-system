# Milestone 3 Gate — Database Schema (PostgreSQL)

**Status:** COMPLETE (engine amended to PostgreSQL)  

**Branch:** `version1.2`  

---

## Objective

Create the physical schema matching Technical Design §04 and SRS Data Requirements, targeting **PostgreSQL**.

No application connection, Npgsql, or CRUD yet (Milestone 4+).

---

## Engine amendment

| OLD | NEW |
|-----|-----|
| SQL Server LocalDB | **PostgreSQL** |

See `project-management/DATABASE-ENGINE-AMENDMENT.md`.

---

## Scripts

| Script | Purpose |
|--------|---------|
| `database/01-CreateDatabase.sql` | `CREATE DATABASE "NovaTechIMS"` |
| `database/02-CreateTables.sql` | 7 tables, FKs, CHECKs, indexes |
| `database/03-SeedOptional.sql` | Sample Category / Supplier / Customer |
| `database/README.md` | Run instructions |

---

## How to verify

1. Ensure local PostgreSQL is running.
2. Create DB: run `01` (or create `NovaTechIMS` in pgAdmin).
3. Connect to `NovaTechIMS`, run `02-CreateTables.sql`.
4. Optional: run `03-SeedOptional.sql`.
5. Confirm seven tables via `information_schema.tables`.
6. WinForms app still builds and runs (no DB code in app).

---

## Next milestone

**Milestone 4 — Connection + data access skeleton** (Npgsql + PostgreSQL)
