# View Design — all screens (version1.2)

**Purpose:** Open every approved UI design in a browser so you can review and refine layouts, copy, and states **before** further WinForms changes.

## Quick start

```powershell
git pull origin version1.2
```

Then open in Chrome or Edge:

```text
UI/VIEW-DESIGN.html
```

(Double-click the file, or right-click → Open with Live Server in VS Code.)

That page lists **every screen** with links to:

| Set | Folder | Use for |
|-----|--------|--------|
| **UPDATED** (preferred) | `UI/update-ui/UI-mock-updated/UI-mock-updated/` | Refinements & source of truth for polish |
| **BASELINE** | `UI/UI-mock/` | Compare older approved mocks |
| Specs | `UI/*.md` | Tokens, shell, screen rules, gap analysis |

## Screens included

1. Login  
2. Dashboard  
3. Product list  
4. Product form  
5. Categories  
6. Suppliers  
7. Customers  
8. Stock-In  
9. Stock-Out  
10. Inventory Adjustment  
11. Inventory History  
12. Reports  
13. User Management  
14. Delegation Management  

## How to refine

1. Open the **UPDATED** HTML for the screen you care about.  
2. Edit colors/layout/copy in that HTML (or note changes in a short list).  
3. Keep tokens aligned with `UI/02-Design-Tokens.md` when possible.  
4. Tell the implementer: *“Apply these refinements from View Design to WinForms on version1.2.”*  
5. Do **not** change business rules, auth, or inventory math in the mock HTML.

## Related docs

- `UI/02-Design-Tokens.md`  
- `UI/05-Screen-Designs.md`  
- `UI/11-Visual-Design-v1.1-Delegation-Amendment.md`  
- `UI/CURRENT-UI-GAP-ANALYSIS.md`  

## Note on Visual Studio “View Designer”

WinForms **Designer** only works for forms that use `*.Designer.cs` (e.g. `LoginForm`, `MainForm`).  
Most list/inventory screens are built in code (`BuildUi()`), so **visual design review is via this HTML gallery**, not the VS form designer surface.

Branch: **version1.2**
