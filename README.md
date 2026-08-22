# NovaTech Electronics — Inventory Management System

**Repository:** https://github.com/elan-thinks/Inventory-management-system  
**Branch:** `version1.2`  
**Target:** C# · .NET 8 · Windows Forms · SQL Server LocalDB

---

## Current status (version1.2)

| Phase | Status |
|-------|--------|
| Requirements (SRS v1.1) | Complete — `/docs/` |
| Product / UX | Complete — `/product-requirements/` |
| Visual Design | Complete — `/UI/` |
| Technical Design v1.0 | In progress on this branch — `/technical-design/` |
| **Milestone 0 — Environment & Solution Setup** | **COMPLETE** — `/src/` |
| Milestone 1+ | Not started |

---

## Open the solution (Windows + VS Code / Visual Studio)

```bash
git clone https://github.com/elan-thinks/Inventory-management-system.git
cd Inventory-management-system
git fetch origin
git checkout version1.2
```

Then open **`src/NovaTechIMS.sln`** in Visual Studio 2022 and press **F5**.

You should see the Milestone 0 placeholder window.

In **VS Code**: File → Open Folder → this repo → checkout `version1.2` → open `src/NovaTechIMS.sln` or the `src` folder.

---

## Repository layout

```
/
├── docs/                    SRS v1.1
├── product-requirements/    Product + UX v1.1
├── UI/                      Visual Design v1.1
├── project-management/      Audits & change proposals
├── technical-design/        Technical Design v1.0 (blueprint)
└── src/                     Implementation (Milestone 0+)
    ├── NovaTechIMS.sln
    ├── README.md
    ├── MILESTONE-0-GATE.md
    └── NovaTechIMS/         WinForms project (net8.0-windows)
```

---

## Next milestone

**Milestone 1 — Basic WinForms application shell**  
LoginForm (UI only) + MainForm navigation skeleton.

See `technical-design/` and `src/MILESTONE-0-GATE.md`.
