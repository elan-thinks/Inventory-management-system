# Database scripts — NovaTech IMS

**Engine:** PostgreSQL  
**Milestone:** 3 — schema only (application connection is Milestone 4)

## Amendment

| Item | Value |
|------|--------|
| **Previous engine** | SQL Server LocalDB |
| **Current engine** | PostgreSQL |
| **Reason** | Already available in developer environment; avoids second server install; full relational features. |

## Files

| Script | Purpose |
|--------|---------|
| `01-CreateDatabase.sql` | Creates database `NovaTechIMS` |
| `02-CreateTables.sql` | Tables, FKs, CHECKs, indexes |
| `03-SeedOptional.sql` | Optional sample Category / Supplier / Customer |

## How to run (psql)

```bash
psql -U postgres -f database/01-CreateDatabase.sql
psql -U postgres -d NovaTechIMS -f database/02-CreateTables.sql
psql -U postgres -d NovaTechIMS -f database/03-SeedOptional.sql
```

Or use **pgAdmin**: create DB `NovaTechIMS`, then run `02` (and optional `03`) on that database.

## Verify

```sql
SELECT table_name FROM information_schema.tables
WHERE table_schema = 'public' ORDER BY table_name;
```

Expect: Category, Customer, Delegation, InventoryTransaction, Product, Supplier, User.

## Connection string (Milestone 4 only — not used in app yet)

```
Host=localhost;Port=5432;Database=NovaTechIMS;Username=postgres;Password=YOUR_PASSWORD
```

Provider later: **Npgsql** (not added in Milestone 3).
