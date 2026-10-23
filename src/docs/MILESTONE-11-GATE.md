# Milestone 11 Gate — Stock-In

**Status:** COMPLETE  

**Branch:** `version1.2`

---

## Objective

Record Stock-In: append-only inventory transaction + increase `Product.QuantityOnHand` in one database transaction.

---

## What was implemented

| Item | Location |
|------|----------|
| `InventoryTransactionRepository.InsertStockIn` | `Data/InventoryTransactionRepository.cs` |
| `StockInService` (FR-SIN, BR-012/016/021/027/028) | `Services/StockInService.cs` |
| Stock-In form | `Forms/Inventory/StockInForm.cs` |
| MainForm | **Stock In** hosts form |

---

## Behaviour

| Requirement | Behaviour |
|-------------|-----------|
| FR-SIN-002 | Product, actual supplier, qty, date, purchase price, user, notes |
| FR-SIN-003 | Quantity ≥ 1 |
| FR-SIN-004 | QuantityOnHand increased by qty |
| FR-SIN-005 | Append-only `InventoryTransaction` (`StockIn`) |
| FR-SIN-006 | Inactive products blocked |
| BR-028 | Supplier on txn = actual receipt supplier (may differ from product default) |
| BR-021 | Date cannot be future |

Atomic: `FOR UPDATE` on product → insert txn → update qty → commit.

---

## Manual verification

1. Login → create product (qty 0) if needed.
2. **Stock In** → select product & supplier, qty, price, date → **Record Stock-In**.
3. Open **Products** → qty increased.
4. Recent Stock-In grid shows the new row.

---

## Next milestone

**Milestone 12 — Stock-Out**
