# NovaTech Electronics — Inventory Management System

**Repository:** https://github.com/elan-thinks/Inventory-management-system  
**Active branch:** `version1.3`  
**Stack:** C# · .NET 8 · Windows Forms · PostgreSQL · Npgsql · xUnit

Desktop inventory system for a university Windows Programming project: catalog, stock movements, adjustments, history, users, delegations, and reports.

---

## Current status (`version1.3`)

| Area | Status |
|------|--------|
| Requirements (SRS) | Complete — `/docs/` |
| Product / UX | Complete — `/product-requirements/` |
| Visual Design | Complete — `/UI/` |
| Technical Design | Complete — `/technical-design/` |
| Implementation (M0–M19) | **Complete** — `/src/` |
| Automated tests (xUnit) | **20 tests** — `src/NovaTechIMS.Tests/` |
| UI designer-friendly layouts | Complete |
| Demo seed data | `database/04-SeedDemoData.sql` |
| **M20 Full system integration testing** | **COMPLETE — ALL PASS** |

**Theme:** Light UI (`UiTheme`).

---

## Prerequisites

- Windows 10/11 · .NET 8 SDK · Visual Studio 2022 or `dotnet` CLI  
- PostgreSQL · scripts in `/database/` (include `04-SeedDemoData.sql` for demo data)

Connection string: `src/NovaTechIMS/App.config`.

---

## Clone and run

```powershell
git clone https://github.com/elan-thinks/Inventory-management-system.git
cd Inventory-management-system
git checkout version1.3
cd src
dotnet build NovaTechIMS.sln
dotnet run --project NovaTechIMS\NovaTechIMS.csproj
```

### Demo logins (after seed script)

| Username | Password | Role |
|----------|----------|------|
| `admin` | `Admin@123` | Administrator |
| `staff01` … `staff10` | `Staff@123` | Inventory Staff |

---

## Run automated tests

```powershell
cd src
dotnet test NovaTechIMS.Tests\NovaTechIMS.Tests.csproj
```

Details: `docs/HOW-TO-RUN-TESTS.md`.

---

## Integration testing (M20)

Gate: `src/MILESTONE-20-GATE.md` — **COMPLETE**.  
Checklist: `docs/FULL-SYSTEM-INTEGRATION-TEST-M20.md` — **ALL PASS**.

---

## Repository layout

```
/
├── README.md
├── docs/
├── product-requirements/
├── UI/
├── project-management/
├── technical-design/
├── database/          # schema + 04-SeedDemoData.sql
└── src/
    ├── MILESTONE-0 … 20-GATE.md
    ├── NovaTechIMS/
    └── NovaTechIMS.Tests/
```
