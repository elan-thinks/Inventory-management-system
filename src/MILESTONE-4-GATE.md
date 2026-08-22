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
| Connection string | `App.config` → name `NovaTechIMS` |
| Package Npgsql | `NovaTechIMS.csproj` |
| ConfigurationManager | package for App.config |
| `DbConnectionFactory` | `Data/DbConnectionFactory.cs` |
| `DatabaseProbe.TryConnect` | `Data/DatabaseProbe.cs` — SELECT 1 |

---

## Setup (required once)

1. Ensure database `NovaTechIMS` exists and Milestone 3 scripts ran.
2. Edit `src/NovaTechIMS/App.config`: set your real PostgreSQL password (replace `YOUR_PASSWORD`).
3. Rebuild solution (restores NuGet packages).

### Optional connectivity test

```csharp
if (NovaTechIMS.Data.DatabaseProbe.TryConnect(out var msg))
    MessageBox.Show(msg);
else
    MessageBox.Show(msg, "Database", MessageBoxButtons.OK, MessageBoxIcon.Warning);
```

Login shell does **not** require DB to open.

---

## Intentionally NOT implemented

- Category / Supplier / Customer / Product repositories
- Stock services
- Authentication against DB
- Full CRUD

---

## Next milestone

**Milestone 5 — Category CRUD**
