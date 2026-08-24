# Database scripts — NovaTech IMS

**Engine:** PostgreSQL  
**Branch:** `version1.3`

## Files

| Script | Purpose |
|--------|---------|
| `01-CreateDatabase.sql` | Creates database `NovaTechIMS` |
| `02-CreateTables.sql` | Tables, FKs, CHECKs, indexes |
| `03-SeedOptional.sql` | Small optional Category / Supplier / Customer sample |
| **`04-SeedDemoData.sql`** | **Demo pack: 10 categories, 10 suppliers, 10 customers, 10 products, admin + 10 staff** |
| `15-delegation.sql` | Delegation-related support (if used) |

## How to run (psql)

```bash
psql -U postgres -f database/01-CreateDatabase.sql
psql -U postgres -d NovaTechIMS -f database/02-CreateTables.sql
psql -U postgres -d NovaTechIMS -f database/04-SeedDemoData.sql
```

Or **pgAdmin**: open database `NovaTechIMS`, run `02`, then `04`.

`04-SeedDemoData.sql` is **idempotent** for the demo names (skips existing username / category / product names).

## Demo logins (after `04-SeedDemoData.sql`)

| Username | Password | Role |
|----------|----------|------|
| `admin` | `Admin@123` | Administrator |
| `staff01` … `staff10` | `Staff@123` | InventoryStaff |

Hashes match app `PasswordHasher` (PBKDF2-SHA256, 100k iterations).

If you already had an empty `User` table and logged in once, bootstrap may have created `admin` already — the seed skips duplicate usernames.

## Connection string (app)

`src/NovaTechIMS/App.config` — example:

```
Host=localhost;Port=5432;Database=NovaTechIMS;Username=postgres;Password=YOUR_PASSWORD
```
