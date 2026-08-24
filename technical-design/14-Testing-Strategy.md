# 14 - Testing Strategy

**Version:** 1.1 (aligned with `version1.3` implementation)  
**Database:** PostgreSQL

| Level | Focus | Where |
|-------|--------|--------|
| Unit | Models, security helpers, services, utilities | `src/NovaTechIMS.Tests/` (xUnit) |
| Integration | Real PostgreSQL (manual / optional) | Manual checklist |
| Manual UI | Forms, navigation, validation messages | `docs/MANUAL-TEST-CHECKLIST-M18.md` |

## Automated tests

```powershell
cd src
dotnet test NovaTechIMS.Tests\NovaTechIMS.Tests.csproj
```

**Milestone 18 verification target:** 20 passed, 0 failed, 0 skipped.

Full command reference: **`docs/HOW-TO-RUN-TESTS.md`**.

## Mandatory negative cases (manual / service-level)

Examples: insufficient stock, overlapping Active delegations, inactive product on stock movement, unauthorized adjustment, auth failure. See original testing checklist and M18 manual list.

## Test database

Use a dedicated PostgreSQL database or schema for destructive experiments — not a shared production `NovaTechIMS` instance.
