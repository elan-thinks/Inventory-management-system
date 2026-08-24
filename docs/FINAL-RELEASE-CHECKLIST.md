# Final release checklist — NovaTech IMS

**Branch:** `version1.3`  
**Stack:** .NET 8 · WinForms · PostgreSQL · Npgsql

Use this before marking a delivery complete.

## Build & automated tests

- [ ] `dotnet build src/NovaTechIMS.sln` succeeds
- [ ] `dotnet test src/NovaTechIMS.Tests/NovaTechIMS.Tests.csproj` — **20 passed**, 0 failed
- [ ] Startup project is **NovaTechIMS** (not the test project)

## Database

- [ ] PostgreSQL running
- [ ] Scripts in `/database/` applied in order
- [ ] `App.config` connection string matches local instance
- [ ] Seed users can sign in

## Core flows (manual)

- [ ] Login / logout
- [ ] Dashboard loads
- [ ] Products / Categories / Suppliers / Customers CRUD
- [ ] Stock-In, Stock-Out, Adjustment
- [ ] Inventory History (read-only; no edit/delete)
- [ ] Reports hub + at least one report
- [ ] User management (Admin)
- [ ] Delegations create / revoke (Admin)
- [ ] Staff role sees limited navigation

## UI / designer

- [ ] Light theme at runtime
- [ ] Key forms open in View Designer (Shift+F7) without designer crash

## Docs

- [ ] Root `README.md` matches branch and stack
- [ ] `docs/HOW-TO-RUN-TESTS.md` followed successfully on the delivery machine

## Sign-off

| Item | Result |
|------|--------|
| Date | |
| Build | |
| Tests | |
| Tester | |

Detailed manual cases: `MANUAL-TEST-CHECKLIST-M18.md`.
