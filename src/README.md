# NovaTech IMS — Source

**Solution:** `NovaTechIMS.sln`  
**Project:** `NovaTechIMS` (Windows Forms, .NET 8)  
**Branch:** `version1.2`  
**Technical Design:** `/technical-design/` (v1.0 APPROVED)

---

## Milestone 0 status

| Check | Status |
|-------|--------|
| Solution opens | Ready (open in Visual Studio 2022 on Windows) |
| Project builds | Ready on Windows with .NET 8 SDK |
| WinForms starts | Placeholder form launches |
| Git structure matches TD | Yes |
| Business logic | **None** (correct for M0) |

---

## How to open and run (Windows)

1. Install **Visual Studio 2022** with workload: .NET desktop development
2. Install **.NET 8 SDK** if not already present.
3. Open `src/NovaTechIMS.sln`.
4. Set configuration to **Debug | Any CPU**.
5. Press **F5**.
6. You should see the Milestone 0 placeholder window.

---

## Folder map (matches Technical Design §03)

```
NovaTechIMS/
├── Forms/           Presentation (WinForms)
├── Models/          Domain classes + Enums/
├── Services/        Business logic
├── Data/            ADO.NET data access
├── Security/        Password hashing helpers
├── Utilities/       Helpers, custom exceptions
├── Properties/
├── Program.cs
└── NovaTechIMS.csproj
```

---

## Target framework

- **net8.0-windows** with UseWindowsForms

## Next milestone

**Milestone 1 — Basic WinForms application shell**  
See `technical-design/15-Implementation-Milestones.md`.
