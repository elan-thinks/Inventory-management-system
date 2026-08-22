# Milestone 8 Gate — Product CRUD

**Status:** COMPLETE  

**Branch:** `version1.2`  

**Database:** PostgreSQL (via Npgsql)

---

## Objective

Implement Product create / read / update / delete with:

- **QuantityOnHand starts at 0** on create (BR-026)
- **QuantityOnHand is never edited** from product screens (BR-027 / VAL-011)
- Calculated stock status (BR-015)
- Search and filters (FR-PROD-007, FR-SRCH)
- Soft-delete when inventory transactions exist (BR-018)

---

## What was implemented

| Item | Location |
|------|----------|
| `ProductRepository` + list row | `Data/ProductRepository.cs` |
| `ProductService` | `Services/ProductService.cs` |
| Product list (SCR-003) | `Forms/Products/ProductListForm.cs` |
| Product add/edit (SCR-005) | `Forms/Products/ProductEditForm.cs` |
| MainForm | `Forms/MainForm.cs` — Products hosts list form |

---

## Behaviour

- **List:** search by name or ID; filters Category, Supplier, Stock status (In/Low/Out), Active/Inactive.
- **Columns:** ID, Name, Category, Supplier, Qty, Min, Stock status, Selling price, Status, Edit/Delete.
- **Add:** name, category, default supplier, description, prices ≥ 0, min stock ≥ 0; qty shown as **0 (starts at zero)** and is not editable.
- **Edit:** same fields; qty is read-only current value; Active checkbox.
- **Delete:** hard delete only with zero inventory transactions; otherwise **Mark Inactive**.
- **INSERT** forces `QuantityOnHand = 0`; **UPDATE** never sets quantity.

---

## Prerequisites to test Add Product

At least one **active Category** and one **active Supplier** (seed script or create via their screens).

---

## Intentionally NOT implemented

- Separate Product Details form (SCR-004) — edit covers update path for M8
- Stock-In / Stock-Out / Adjustment (later milestones change quantity)
- Role-based UI (Milestone 10)
- Authentication (Milestone 9)

---

## Next milestone

**Milestone 9 — Authentication (hashed passwords)**
