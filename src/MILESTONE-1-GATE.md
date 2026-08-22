# Milestone 1 Gate — Basic WinForms Application Shell

**Status:** COMPLETE  

**Branch:** `version1.2`  

---

## Objective

Replace the Milestone 0 placeholder with a LoginForm UI shell and a MainForm application shell (sidebar, header, content, status strip) that matches the approved Visual Design and Information Architecture. Prove WinForms navigation without any business or data layer.

---

## What was implemented

| Item | Detail |
|------|--------|
| LoginForm | Username, password, Sign In, Exit; shell-level empty-field check only |
| MainForm | 220px sidebar, header (breadcrumb + title), content region, StatusStrip |
| Navigation | Dashboard, Catalog group, Inventory Operations, Reports, Administration |
| Placeholders | Each nav item shows “Coming in a later milestone” content |
| Theme | `Utilities/UiTheme.cs` from UI/02-Design-Tokens.md |
| Sign Out | Confirms and returns to LoginForm |
| Entry | `Program.cs` runs `LoginForm` |

---

## Files / classes added or modified

**Added**

- `src/NovaTechIMS/Forms/LoginForm.cs`
- `src/NovaTechIMS/Forms/LoginForm.Designer.cs`
- `src/NovaTechIMS/Forms/MainForm.cs`
- `src/NovaTechIMS/Forms/MainForm.Designer.cs`
- `src/NovaTechIMS/Utilities/UiTheme.cs`
- `src/MILESTONE-1-GATE.md`

**Modified**

- `src/NovaTechIMS/Program.cs` — starts LoginForm

**Unchanged / still present**

- `Forms/PlaceholderForm.cs` (Milestone 0 artifact; no longer launched)

---

## WinForms concepts demonstrated

- Forms and partial Designer files
- Panels, FlowLayoutPanel, StatusStrip, ToolStripStatusLabel
- Dock / layout for resize
- Events (Click, Load)
- AcceptButton / tab order / AccessibleName
- Modal ShowDialog for MainForm from LoginForm
- MessageBox for logout confirm
- No business logic in event handlers beyond UI state

---

## Manual verification checklist

| Check | Expected |
|-------|----------|
| Solution builds | No errors |
| App launches | LoginForm centered |
| Empty Sign In | Error label shown; stay on login |
| Non-empty Sign In | MainForm opens with sidebar |
| Nav clicks | Content title/body update |
| Resize | Sidebar fixed; content fills |
| Sign Out → Yes | Returns to LoginForm |
| No DB / SQL | None in this milestone |

---

## Intentionally NOT implemented

- Database, SQL, LocalDB, ADO.NET, EF
- Authentication, password hashing, User entities
- Authorization / role enforcement beyond demo label
- CRUD for any entity
- Stock-In / Out / Adjustment / History
- Delegation logic
- Reports
- Real Dashboard data
- Services / repositories

---

## Known limitations

- Active nav styling is approximate (no painted left accent bar yet).
- Role-based hiding of Adjustment / User Management is deferred (Milestone 10).
- PlaceholderForm remains in the tree but is unused.

---

## Next milestone

**Milestone 2 — Models and Enums**
