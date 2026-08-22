# Database scripts — NovaTech IMS

**Engine:** SQL Server LocalDB (or full SQL Server Express / Developer)  
**Milestone:** 3 — schema only (application connection is Milestone 4)

## Files

| Script | Purpose |
|--------|---------|
| `01-CreateDatabase.sql` | Creates database `NovaTechIMS` (idempotent) |
| `02-CreateTables.sql` | Tables, FKs, CHECKs, indexes |
| `03-SeedOptional.sql` | Optional sample Category/Supplier/Customer only (no users yet) |

## How to run (Visual Studio / SSMS)

1. Install **SQL Server LocalDB** (VS 2022 workload “Data storage and processing” or SQL Express).
2. Open **SQL Server Object Explorer** in Visual Studio, or **SSMS**.
3. Connect to `(localdb)\MSSQLLocalDB`.
4. Run `01-CreateDatabase.sql`, then `02-CreateTables.sql`.
5. Optionally run `03-SeedOptional.sql`.

## Connection string (for Milestone 4 — do not use in the app yet)

```
Server=(localdb)\MSSQLLocalDB;Database=NovaTechIMS;Trusted_Connection=True;TrustServerCertificate=True;
```

## Notes

- No application code connects to the database in Milestone 3.
- Seed does **not** create login users (password hashing arrives in Milestone 9).
- InventoryTransaction is append-only by design (no UPDATE/DELETE from the app).
