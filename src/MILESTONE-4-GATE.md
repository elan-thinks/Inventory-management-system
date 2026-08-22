# Milestone 4 Gate — Connection + Data Access Skeleton

**Status:** COMPLETE  

**Branch:** `version1.2`  

**Database:** PostgreSQL (via Npgsql)

---

## Objective

Wire the WinForms project to PostgreSQL with a minimal data-access skeleton. No entity CRUD yet (Milestones 5+).

---

## What was implemented

| Item | Location |
|------|----------|
| Connection string (no password) | `App.config` → name `NovaTechIMS` |
| Password | User env var **`NOVATECH_PG_PASSWORD`** |
| Optional full override | Env var **`NOVATECH_IMS_CONNECTION`** |
| Package Npgsql | `NovaTechIMS.csproj` |
| `DbConnectionFactory` | `Data/DbConnectionFactory.cs` |
| `DatabaseProbe.TryConnect` | `Data/DatabaseProbe.cs` |

---

## Setup

1. Database `NovaTechIMS` exists; Milestone 3 scripts applied.
2. Set user environment variable **NOVATECH_PG_PASSWORD** = your PostgreSQL password.
3. **Fully restart Visual Studio** (required to pick up env vars).
4. Rebuild solution.

### Environment variable setup (Windows)

1. Windows search → **environment variables** → **Edit the system environment variables**.
2. **Environment Variables…**
3. Under **User variables** → **New…**
   - Name: `NOVATECH_PG_PASSWORD`
   - Value: *your postgres password*
4. OK all dialogs.
5. Close Visual Studio completely and open it again.

Optional: `NOVATECH_IMS_CONNECTION` = full connection string (overrides App.config).

---

## Optional connectivity test

```csharp
if (NovaTechIMS.Data.DatabaseProbe.TryConnect(out var msg))
    MessageBox.Show(msg);
else
    MessageBox.Show(msg, "Database", MessageBoxButtons.OK, MessageBoxIcon.Warning);
```

---

## Next milestone

**Milestone 5 — Category CRUD**
