# NovaTech Electronics — Inventory Management System

**Repository:** https://github.com/elan-thinks/Inventory-management-system  
**Active branch:** `version1.3`  
**Stack:** C# · .NET 8 · Windows Forms · PostgreSQL · Npgsql · xUnit  
**Release:** **Approved for delivery** — see `docs/RELEASE-STATUS.md`

---

## Status summary

| Area | Status |
|------|--------|
| Implementation (M0–M19) | Complete |
| Automated tests | 20 xUnit tests |
| Full system integration (M20) | Complete — ALL PASS |
| Milestone documentation | Aligned to `version1.3` |
| Formal release checklist | `docs/FINAL-RELEASE-CHECKLIST.md` |

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

## Final verification (build + tests)

```powershell
cd src
dotnet build NovaTechIMS.sln
dotnet test NovaTechIMS.Tests\NovaTechIMS.Tests.csproj
```

Expected: **20 passed · 0 failed · 0 skipped**.

Details: `docs/HOW-TO-RUN-TESTS.md` · `docs/RELEASE-STATUS.md`

---

## Milestone gates

All gates under `src/MILESTONE-*-GATE.md` (0–20). Latest:

- **M18** — Automated testing · COMPLETE · branch `version1.3`
- **M19** — UI polish · COMPLETE · branch `version1.3`
- **M20** — Full system integration · COMPLETE — ALL PASS
