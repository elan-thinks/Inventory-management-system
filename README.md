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
| **M20 Full system integration testing** | **In progress** — `src/MILESTONE-20-GATE.md` |

**Theme:** Light UI (`UiTheme` — background `#F4F6F8`, primary `#1E4B8F`).

---

## Prerequisites

- Windows 10/11
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 (WinForms workload) **or** `dotnet` CLI
- [PostgreSQL](https://www.postgresql.org/download/) running locally
- Database scripts applied from `/database/`

Connection string: `src/NovaTechIMS/App.config`.

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

Or open **`src/NovaTechIMS.sln`** → startup project **NovaTechIMS** → **F5**.

---

## Run automated tests

```powershell
cd src
dotnet test NovaTechIMS.Tests\NovaTechIMS.Tests.csproj
```

Expected: **20 passed**, **0 failed**, **0 skipped**.  
Details: **`docs/HOW-TO-RUN-TESTS.md`**.

---

## Full system integration testing (Milestone 20)

After M0–M19, verify the product end-to-end:

1. Gate: **`src/MILESTONE-20-GATE.md`**
2. Master checklist: **`docs/FULL-SYSTEM-INTEGRATION-TEST-M20.md`**
3. Deeper negatives (optional): **`docs/MANUAL-TEST-CHECKLIST-M18.md`**

Mark Pass/Fail on the machine with a real PostgreSQL instance, Admin + Staff accounts, and a full stock journey (create master data → Stock-In → Stock-Out → History → Reports).

---

## How the WinForms UI is built (hybrid)

| Layer | Role |
|-------|------|
| `*.Designer.cs` | Layout — View Designer (Shift+F7) |
| `*.cs` code-behind | Data, save, filters, permissions |
| Services / Npgsql | Business rules + SQL |
| MainForm | Shell hosts one page at a time |

See `docs/UI-DESIGNER-CONVERSION.md`, `src/DESIGNER-NOTES.md`.

---

## Repository layout

```
/
├── README.md
├── docs/                     SRS, HOW-TO-RUN-TESTS, M20 integration checklist
├── product-requirements/
├── UI/
├── project-management/
├── technical-design/
├── database/
└── src/
    ├── NovaTechIMS.sln
    ├── MILESTONE-0 … 20-GATE.md
    ├── NovaTechIMS/
    └── NovaTechIMS.Tests/
```

---

## Further reading

| Doc | Purpose |
|-----|---------|
| `docs/HOW-TO-RUN-TESTS.md` | Automated test commands |
| `docs/FULL-SYSTEM-INTEGRATION-TEST-M20.md` | M20 end-to-end checklist |
| `docs/FINAL-RELEASE-CHECKLIST.md` | Release sign-off |
| `technical-design/00-README.md` | Architecture index |
