# Milestone 20 Gate — Full System Integration Testing

**Status:** IN PROGRESS  
**Branch:** `version1.3`  
**Depends on:** M18 (automated tests) · M19 (UI polish) · live PostgreSQL

---

## Objective

Prove the **entire** NovaTech IMS works as one product end-to-end:

- Login → shell → every major module → data consistency → roles → failure paths
- Not unit tests alone, not single-screen checks alone
- **System-level** flows across UI + services + PostgreSQL

---

## Scope

| In scope | Out of scope |
|----------|----------------|
| Full happy-path journeys (Admin + Staff) | New features / redesign |
| Cross-module data consistency (stock → history → reports) | Performance benchmarking |
| Negative / security cases from M18 checklist | Changing architecture |
| Regression after designer UI work | Rewriting SRS |
| Build + automated suite still green | |

---

## Prerequisites (before testing)

1. Branch: `version1.3`
2. PostgreSQL running; `/database/` scripts applied
3. `App.config` connection string correct
4. Build succeeds:

```powershell
cd src
dotnet build NovaTechIMS.sln
```

5. Automated tests still pass:

```powershell
dotnet test NovaTechIMS.Tests\NovaTechIMS.Tests.csproj
```

Expected: **20 passed / 0 failed / 0 skipped**

---

## Deliverables

| Deliverable | Location |
|-------------|----------|
| Master integration checklist | `docs/FULL-SYSTEM-INTEGRATION-TEST-M20.md` |
| This gate | `src/MILESTONE-20-GATE.md` |
| Evidence (optional) | Tester fills Pass/Fail + notes in the checklist |

---

## Acceptance criteria

Milestone 20 is **COMPLETE** only when:

1. Automated suite: **20/20** on the same machine used for integration tests  
2. Checklist `docs/FULL-SYSTEM-INTEGRATION-TEST-M20.md` executed  
3. All **Critical** and **High** rows marked **Pass** (or documented approved deviation)  
4. No open **blockers** (app crash, data corruption, auth bypass)  
5. Gate status below updated to **COMPLETE** with tester name + date  

---

## Local verification (fill when done)

| Item | Result |
|------|--------|
| Date | |
| Tester | |
| .NET version | |
| PostgreSQL version | |
| `dotnet test` | |
| Critical cases | |
| High cases | |
| Blockers remaining | None / list |
| Gate decision | COMPLETE / BLOCKED |

---

## Relationship to prior milestones

| Milestone | Role |
|-----------|------|
| M18 | Unit tests + initial manual negative list |
| M19 | UI polish / designer alignment |
| **M20** | **Full system integration** across modules and roles |

---

## Next after M20

- Formal release packaging / demo script (optional M21), or  
- Close project with `docs/FINAL-RELEASE-CHECKLIST.md` sign-off  
