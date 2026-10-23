# Milestone 14 Gate — Inventory Adjustment

**Status:** COMPLETE  

**Branch:** `version1.2`

---

## Objective

Administrator-only inventory adjustment: set `QuantityOnHand` to a new value with mandatory reason and append-only history (FR-ADJ).

---

## What was implemented

| Item | Location |
|------|----------|
| `InsertAdjustment` | `Data/InventoryTransactionRepository.cs` |
| `AdjustmentService` | `Services/AdjustmentService.cs` |
| Adjustment form | `Forms/Inventory/AdjustmentForm.cs` |
| MainForm | **Inventory Adjustment** (Admin only in nav) |

---

## Behaviour

| Requirement | Behaviour |
|-------------|-----------|
| FR-ADJ-001 | `Permissions.PerformAdjustment` — Administrator only |
| FR-ADJ-002 | Previous, New, Difference, Reason, Date, User recorded |
| FR-ADJ-003 | QuantityOnHand set to New Quantity |
| FR-ADJ-004 | Append-only `Adjustment` transaction |
| FR-ADJ-005 | Reason mandatory |

Difference = New − Previous (can be negative).

---

## Manual verification

1. Login as **admin** → **Inventory Adjustment** visible.
2. Select product → previous qty shown → enter new qty + reason → save.
3. Products list reflects new quantity.
4. **Inventory History** → filter Type = Adjustment.
5. Login as **staff** → Adjustment menu **hidden**.

---

## Next milestone

**Milestone 15 — Delegation management**
