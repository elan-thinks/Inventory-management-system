# Inventory Management System (IMS)

**Repository:** https://github.com/elan-thinks/Inventory-management-system  
**Active branch:** `version1.3`  
**Product name:** **Inventory Management System (IMS)**  
**Stack:** C# · .NET 8 · Windows Forms · PostgreSQL · Npgsql · xUnit  
**Release:** Approved for delivery — `docs/RELEASE-STATUS.md`

> **Note:** The C# project/namespace remains `NovaTechIMS` for build stability. User-facing titles and branding use **Inventory Management System (IMS)**.

---

## Status summary

| Area | Status |
|------|--------|
| Implementation (M0–M19) | Complete |
| Automated tests | 20 xUnit tests |
| Full system integration (M20) | Complete — ALL PASS |
| Milestone documentation | Aligned to `version1.3` |

---

## Run the app

```powershell
git clone https://github.com/elan-thinks/Inventory-management-system.git
cd Inventory-management-system
git checkout version1.3
cd src
dotnet build NovaTechIMS.sln
dotnet run --project NovaTechIMS\NovaTechIMS.csproj
```

Requires PostgreSQL + scripts under `/database/` (use `04-SeedDemoData.sql` for demo data).

### Demo logins

| Username | Password | Role |
|----------|----------|------|
| `admin` | `Admin@123` | Administrator |
| `staff01` … `staff10` | `Staff@123` | Inventory Staff |

---

## Final verification

```powershell
cd src
dotnet build NovaTechIMS.sln
dotnet test NovaTechIMS.Tests\NovaTechIMS.Tests.csproj
```

Expected: **20 passed · 0 failed · 0 skipped**.
