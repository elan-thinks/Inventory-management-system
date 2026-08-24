# Final release checklist — NovaTech IMS

**Branch:** `version1.3`  
**Stack:** .NET 8 · WinForms · PostgreSQL · Npgsql  
**Release status:** **READY FOR FORMAL SIGN-OFF** (2026-08-24)

---

## Build & automated tests

- [x] `dotnet build src/NovaTechIMS.sln` succeeds
- [x] `dotnet test src/NovaTechIMS.Tests/NovaTechIMS.Tests.csproj` — **20 passed**, 0 failed (re-run on delivery machine for final evidence)
- [x] Startup project is **NovaTechIMS**

## Database

- [x] PostgreSQL running
- [x] Scripts in `/database/` applied (`02` schema, `04` demo seed recommended)
- [x] `App.config` connection string matches local instance
- [x] Seed users can sign in (`admin` / `Admin@123`; `staff01`…`staff10` / `Staff@123`)

## Core flows (manual) — covered by M20

- [x] Login / logout
- [x] Dashboard
- [x] Products / Categories / Suppliers / Customers CRUD
- [x] Stock-In, Stock-Out, Adjustment
- [x] Inventory History (read-only)
- [x] Reports hub
- [x] User management (Admin)
- [x] Delegations (Admin)
- [x] Staff limited navigation

## UI / designer

- [x] Light theme at runtime
- [x] Major forms designer-compatible (`docs/UI-DESIGNER-CONVERSION.md`)

## Docs (cleaned)

- [x] Root `README.md` → `version1.3`
- [x] `MILESTONE-18-GATE.md` → `version1.3`
- [x] `MILESTONE-19-GATE.md` → `version1.3`, status **COMPLETE**
- [x] `MILESTONE-20-GATE.md` → **COMPLETE — ALL PASS**
- [x] `docs/HOW-TO-RUN-TESTS.md` present

## Sign-off

| Item | Result |
|------|--------|
| Date | 2026-08-24 |
| Build | PASS |
| Automated tests | 20/20 (confirm with final local run) |
| Integration (M20) | ALL PASS |
| Documentation | Cleaned to `version1.3` |
| Tester | Developer |
| **Release decision** | **APPROVED FOR DELIVERY** |

Re-run once for evidence:

```powershell
git pull origin version1.3
cd src
dotnet build NovaTechIMS.sln
dotnet test NovaTechIMS.Tests\NovaTechIMS.Tests.csproj
```
