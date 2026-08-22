# Milestone 18 Gate — Testing

**Status:** COMPLETE  

**Branch:** `version1.2`

---

## Objective

Establish unit tests for pure logic and a documented manual checklist covering **14+ mandatory negative cases** (Technical Design §14).

---

## What was delivered

| Item | Location |
|------|----------|
| xUnit test project | `src/NovaTechIMS.Tests/` |
| PasswordHasher tests | `Security/PasswordHasherTests.cs` |
| Authorization tests | `Services/AuthorizationServiceTests.cs` |
| ErrorPresenter tests | `Utilities/ErrorPresenterTests.cs` |
| Stock status tests | `Models/ProductStockStatusTests.cs` |
| Manual checklist | `docs/MANUAL-TEST-CHECKLIST-M18.md` |
| Solution entry | `NovaTechIMS.sln` includes Tests project |

---

## Run automated tests

```powershell
cd src
dotnet test NovaTechIMS.Tests\NovaTechIMS.Tests.csproj
```

---

## Negative cases covered (manual)

Insufficient stock, inactive product on movements, unauthorized adjustment/staff restrictions, overlapping Active delegations, invalid delegation dates/recipients, duplicate names, delete with references, generic auth failures, DB down message, adjustment reason required, future dates, product qty not editable on create, report date validation.

---

## Intentionally limited

Full service unit tests with mocked repositories would require refactoring constructors for DI. Current architecture uses concrete `new Repository()` — integration tests against a dedicated PostgreSQL database remain the primary service-level approach (see checklist sections C–G).

---

## Next milestone

**Milestone 19 — Final UI polish + integration**
