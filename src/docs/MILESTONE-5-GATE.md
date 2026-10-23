# Milestone 5 Gate — Category CRUD

**Status:** COMPLETE  

**Branch:** `version1.2`  

**Database:** PostgreSQL (via Npgsql)

---

## Objective

Implement full Category create / read / update / delete (soft-delete when products exist) end-to-end: Data → Service → WinForms UI, wired into the MainForm shell.

---

## What was implemented

| Item | Location |
|------|----------|
| Exception hierarchy | `Utilities/AppExceptions.cs` |
| `CategoryRepository` + list row | `Data/CategoryRepository.cs` |
| `CategoryService` (FR-CAT, BR-007/008, VAL-004) | `Services/CategoryService.cs` |
| Category list (SCR-006) | `Forms/Categories/CategoryListForm.cs` |
| Category add/edit (SCR-007) | `Forms/Categories/CategoryEditForm.cs` |
| MainForm navigation | `Forms/MainForm.cs` — Categories hosts list form |

---

## Behaviour

- **List:** search by name, filter Active/Inactive, product count column, refresh.
- **Add / Edit:** name required (max 100), unique; description optional (max 500); Active checkbox on edit.
- **Delete:** hard delete when zero products; if products exist → offer **Mark Inactive** (BR-008).
- **SQL:** parameterized only; manual reader mapping; no EF.

---

## Intentionally NOT implemented

- Role-based hiding of Add/Edit/Delete (Milestone 10)
- Real authentication (Milestone 9)
- Supplier / Customer / Product CRUD (Milestones 6–8)

---

## Manual verification

1. PostgreSQL `NovaTechIMS` schema applied (M3); connection works (M4).
2. Optional: run `database/03-SeedOptional.sql` for sample categories.
3. F5 → login with any non-empty credentials → **Categories** in sidebar.
4. Add, edit, search, filter, delete empty category, deactivate when products exist.

---

## Next milestone

**Milestone 6 — Supplier CRUD**
