# Source — NovaTech IMS

**Branch:** `version1.3`  
**Solution:** `NovaTechIMS.sln`  
**Projects:** `NovaTechIMS` (WinForms app) · `NovaTechIMS.Tests` (xUnit)

## Build & run application

```powershell
cd src
dotnet build NovaTechIMS.sln
dotnet run --project NovaTechIMS\NovaTechIMS.csproj
```

Visual Studio: open the solution → startup project **NovaTechIMS** → **F5**.

Requires **PostgreSQL** and a valid connection string in `NovaTechIMS/App.config`.

## Run tests

```powershell
cd src
dotnet test NovaTechIMS.Tests\NovaTechIMS.Tests.csproj
```

Or **Test Explorer** in Visual Studio → Run All.

See also: `../docs/HOW-TO-RUN-TESTS.md`.

## Architecture (short)

```
Forms (UI)
  → Services (rules, auth, stock)
    → Data / Npgsql
      → PostgreSQL
```

- **Hybrid UI:** `.Designer.cs` for layout; `.cs` for behaviour and data.
- **MainForm** hosts feature forms in the content panel.
- **UiTheme** applies light styling at runtime.

## Milestone gates

`MILESTONE-0-GATE.md` … `MILESTONE-19-GATE.md` — acceptance criteria for each implementation step. M0–M19 are implemented on this branch.

## Designer notes

See `DESIGNER-NOTES.md` and `../docs/UI-DESIGNER-CONVERSION.md`.
