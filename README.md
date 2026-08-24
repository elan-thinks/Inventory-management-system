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
| UI designer-friendly layouts | Complete — hybrid Designer + code-behind |

**Theme:** Light UI (`UiTheme` — background `#F4F6F8`, primary `#1E4B8F`).

---

## Prerequisites

- Windows 10/11
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 (WinForms workload) **or** `dotnet` CLI
- [PostgreSQL](https://www.postgresql.org/download/) running locally
- Database scripts applied from `/database/` (see that folder’s order)

Connection string: `src/NovaTechIMS/App.config` (adjust host, database, user, password for your machine).

---

## Clone and run the app

```powershell
git clone https://github.com/elan-thinks/Inventory-management-system.git
cd Inventory-management-system
git fetch origin
git checkout version1.3

cd src
dotnet build NovaTechIMS.sln
dotnet run --project NovaTechIMS\NovaTechIMS.csproj
```

Or open **`src/NovaTechIMS.sln`** in Visual Studio → set **NovaTechIMS** as startup project → **F5**.

Login uses seeded users from the database scripts (typically administrator / staff accounts defined in seed SQL).

---

## Run automated tests

Tests live in **`src/NovaTechIMS.Tests/`** (xUnit). They exercise models, security, services, and utilities against the main project.

### CLI (recommended)

From the repo root or `src`:

```powershell
cd src
dotnet test NovaTechIMS.Tests\NovaTechIMS.Tests.csproj
```

Run with more detail:

```powershell
dotnet test NovaTechIMS.Tests\NovaTechIMS.Tests.csproj --verbosity normal
```

Filter by name (example):

```powershell
dotnet test NovaTechIMS.Tests\NovaTechIMS.Tests.csproj --filter "FullyQualifiedName~Password"
```

Expected (Milestone 18 verification): **build success**, **20 passed**, **0 failed**, **0 skipped**.

### Visual Studio

1. Open `src/NovaTechIMS.sln`
2. **Test → Test Explorer**
3. **Run All**

### What the tests cover

| Folder under `NovaTechIMS.Tests` | Focus |
|----------------------------------|--------|
| `Models/` | Domain / enum behaviour |
| `Security/` | Auth / permissions helpers |
| `Services/` | Business rules (unit-level) |
| `Utilities/` | Helpers (e.g. mapping, presentation) |

**Note:** Automated suite is primarily unit-level. Full UI and PostgreSQL failure scenarios are in `docs/MANUAL-TEST-CHECKLIST-M18.md`.

---

## How the WinForms UI is built (hybrid)

| Layer | Role |
|-------|------|
| `*.Designer.cs` | Layout (panels, grids, buttons) — **View Designer (Shift+F7)** |
| `*.cs` code-behind | Data load, save, filters, permissions |
| `Services` / data access | Business rules + SQL via Npgsql |
| `MainForm` | Shell: sidebar, header, hosts one page at a time |

- **Designer** = structure; **runtime** = data, role-based nav, theme polish.
- Do not rename/delete controls the code uses.
- Details: `docs/UI-DESIGNER-CONVERSION.md`, `src/DESIGNER-NOTES.md`.

---

## Repository layout

```
/
├── README.md                 ← this file
├── docs/                     SRS + checklists + designer conversion notes
├── product-requirements/     Product + UX
├── UI/                       Visual design tokens & specs
├── project-management/       Audits & amendments (e.g. PostgreSQL)
├── technical-design/         Architecture blueprint
├── database/                 PostgreSQL schema + seed scripts
└── src/
    ├── NovaTechIMS.sln
    ├── README.md
    ├── DESIGNER-NOTES.md
    ├── MILESTONE-*-GATE.md   Acceptance gates (M0–M19)
    ├── NovaTechIMS/          WinForms app (net8.0-windows)
    └── NovaTechIMS.Tests/    xUnit test project
```

---

## Further reading

| Doc | Purpose |
|-----|---------|
| `src/README.md` | Implementation folder quick start |
| `docs/HOW-TO-RUN-TESTS.md` | Detailed test commands |
| `docs/MANUAL-TEST-CHECKLIST-M18.md` | Manual integration checklist |
| `docs/FINAL-RELEASE-CHECKLIST.md` | Release gate |
| `technical-design/00-README.md` | Tech design index |
| `project-management/DATABASE-ENGINE-AMENDMENT.md` | Why PostgreSQL (not LocalDB) |
