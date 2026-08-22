# Milestone 7 Gate — Customer CRUD

**Status:** COMPLETE  

**Branch:** `version1.2`  

**Database:** PostgreSQL (via Npgsql)

---

## Objective

Implement full Customer create / read / update / delete (soft-delete when Stock-Out transactions reference the customer) end-to-end: Data → Service → WinForms UI, wired into the MainForm shell.

---

## What was implemented

| Item | Location |
|------|----------|
| `CustomerRepository` + list row | `Data/CustomerRepository.cs` |
| `CustomerService` (FR-CUS, BR-010, VAL-007/008/013/014) | `Services/CustomerService.cs` |
| Customer list (SCR-010) | `Forms/Customers/CustomerListForm.cs` |
| Customer add/edit (SCR-011) | `Forms/Customers/CustomerEditForm.cs` |
| MainForm navigation | `Forms/MainForm.cs` — Customers hosts list form |

---

## Behaviour

- **List:** search by **name or phone** (FR-CUS-004), filter Active/Inactive, columns ID / Name / Phone / Email / Status, refresh.
- **Add / Edit:** Name required (max 150); Phone (optional, format-validated); Email (optional, format-validated); Address (optional, max 250 per VAL-008); Active checkbox on edit.
- **Delete:** hard delete only when zero Stock-Out transactions reference the customer; otherwise offer **Mark Inactive** (BR-010).
- **SQL:** parameterized only with explicit `NpgsqlDbType`; no EF.

---

## Intentionally NOT implemented

- Role-based hiding of Add/Edit/Delete (Milestone 10)
- Real authentication (Milestone 9)
- Product CRUD (Milestone 8)

---

## Manual verification

1. PostgreSQL `NovaTechIMS` schema applied; connection works.
2. Optional: `database/03-SeedOptional.sql` for sample customers.
3. F5 → login → **Customers** in sidebar.
4. Add, edit, search by name/phone, filter, delete unreferenced customer, deactivate when referenced by Stock-Out.

---

## Next milestone

**Milestone 8 — Product CRUD** (Qty starts at 0; not directly editable)
