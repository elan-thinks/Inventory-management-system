# Milestone 18 Gate — Testing

**Status:** COMPLETE — **Automated suite VERIFIED & APPROVED**  

**Branch:** `version1.2`

---

## Objective

Establish unit tests for pure logic and a documented manual checklist covering **14+ mandatory negative cases** (Technical Design §14).

---

## Local Verification

Verified on developer machine:

| Item | Result |
|------|--------|
| Command | `dotnet test NovaTechIMS.Tests\NovaTechIMS.Tests.csproj` |
| Build | PASS |
| Total tests | **20** |
| Passed | **20** |
| Failed | **0** |
| Skipped | **0** |
| Duration | ~5.5 seconds |
| .NET runtime | 8.0.30 |
| Verified by | Developer |

```
Build:        SUCCESS
Tests:        20 / 20
Failed:       0
Skipped:      0
```

**Automated testing portion: CLOSED.**  
**Manual integration checklist** (`docs/MANUAL-TEST-CHECKLIST-M18.md`) remains recommended before formal release.

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

## Next milestone

**Milestone 19 — Final UI polish + integration**
