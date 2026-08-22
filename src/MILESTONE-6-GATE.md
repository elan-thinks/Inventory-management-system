# Milestone 6 Gate — Supplier CRUD

**Status:** COMPLETE  

**Branch:** `version1.2`  

**Database:** PostgreSQL (via Npgsql)

---

## Objective

Implement full Supplier create / read / update / delete (soft-delete when products or inventory transactions reference the supplier) end-to-end: Data → Service → WinForms UI, wired into the MainForm shell.

---

## What was implemented

| Item | Location |
|------|----------|
| `SupplierRepository` + list row | `Data/SupplierRepository.cs` |
| `SupplierService` (FR-SUP, BR-009, VAL-005/006/013/014) | `Services/SupplierService.cs` |
| Supplier list (SCR-008) | `Forms/Suppliers/SupplierListForm.cs` |
| Supplier add/edit (SCR-009) | `Forms/Suppliers/SupplierEditForm.cs` |
| MainForm navigation | `Forms/MainForm.cs` — Suppliers hosts list form |

---

## Behaviour

- **List:** search by name (FR-SUP-004), filter Active/Inactive, columns ID / Name / Contact / Phone / Email / Status, refresh.
- **Add / Edit:** Name required (max 150); Contact (max 100); Phone (optional, format-validated); Email (optional, format-validated); Address (optional, max 300); Active checkbox on edit.
- **Delete:** hard delete only when zero products **and** zero inventory transactions; otherwise offer **Mark Inactive** (BR-009).
- **SQL:** parameterized only with explicit `NpgsqlDbType`; no EF.

---

## Intentionally NOT implemented

- Role-based hiding of Add/Edit/Delete (Milestone 10)
- Real authentication (Milestone 9)
- Customer / Product CRUD (Milestones 7–8)

---

## Manual verification

1. PostgreSQL `NovaTechIMS` schema applied; connection works.
2. Optional: `database/03-SeedOptional.sql` for sample suppliers.
3. F5 → login → **Suppliers** in sidebar.
4. Add, edit, search, filter, delete unreferenced supplier, deactivate when referenced.

---

## Next milestone

**Milestone 7 — Customer CRUD**
