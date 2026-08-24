# How to run tests — NovaTech IMS

**Branch:** `version1.3`  
**Test project:** `src/NovaTechIMS.Tests/NovaTechIMS.Tests.csproj`  
**Framework:** xUnit · .NET 8 (`net8.0-windows`)  
**References:** main app `NovaTechIMS`

---

## 1. Prerequisites

- .NET 8 SDK installed (`dotnet --version` shows 8.x)
- Solution builds: `dotnet build src/NovaTechIMS.sln`
- NuGet packages restore (automatic on `dotnet test`)

Most automated tests are **unit-level** and do **not** require PostgreSQL. Manual UI / DB scenarios are separate (see below).

---

## 2. Run all tests (CLI)

From repository root:

```powershell
cd src
dotnet test NovaTechIMS.Tests\NovaTechIMS.Tests.csproj
```

From `src` folder:

```powershell
dotnet test NovaTechIMS.Tests\NovaTechIMS.Tests.csproj
```

### Useful options

```powershell
# Verbose output
dotnet test NovaTechIMS.Tests\NovaTechIMS.Tests.csproj --verbosity normal

# No build (if already built)
dotnet test NovaTechIMS.Tests\NovaTechIMS.Tests.csproj --no-build

# Filter by fully qualified name fragment
dotnet test NovaTechIMS.Tests\NovaTechIMS.Tests.csproj --filter "FullyQualifiedName~Auth"

# Filter by class name
dotnet test NovaTechIMS.Tests\NovaTechIMS.Tests.csproj --filter "FullyQualifiedName~PasswordHasher"
```

---

## 3. Run tests in Visual Studio

1. Open `src/NovaTechIMS.sln`
2. Menu **Test → Test Explorer**
3. Click **Run All** (or run a single test)

Test source folders:

| Path | Content |
|------|---------|
| `NovaTechIMS.Tests/Models/` | Model / enum tests |
| `NovaTechIMS.Tests/Security/` | Auth / permission tests |
| `NovaTechIMS.Tests/Services/` | Service / business-rule tests |
| `NovaTechIMS.Tests/Utilities/` | Helper tests |

---

## 4. Expected result (Milestone 18)

Verified pattern:

```text
Build:       SUCCESS
Total tests: 20
Passed:      20
Failed:      0
Skipped:     0
```

If counts differ after local changes, investigate failing tests before declaring a release.

---

## 5. Manual / integration testing

Automated tests do **not** replace:

- Login / logout
- Role-based navigation
- Stock In / Out / Adjustment against real PostgreSQL
- Reports print path
- Connection-failure messaging

Use: **`docs/MANUAL-TEST-CHECKLIST-M18.md`**

---

## 6. Troubleshooting

| Symptom | What to try |
|---------|-------------|
| NuGet / restore errors | `dotnet restore src/NovaTechIMS.sln`; check internet / NuGet feed |
| Project won’t build | Ensure `net8.0-windows` SDK and Windows (WinForms) |
| 0 tests discovered | Confirm `xunit` + `Microsoft.NET.Test.Sdk` packages; rebuild |
| VS Test Explorer empty | Build solution; ensure test project is not unloaded |

---

## 7. Related docs

- `src/MILESTONE-18-GATE.md` — automated testing gate
- `docs/MANUAL-TEST-CHECKLIST-M18.md` — manual checklist
- `docs/FINAL-RELEASE-CHECKLIST.md` — release sign-off
- `technical-design/14-Testing-Strategy.md` — strategy overview
