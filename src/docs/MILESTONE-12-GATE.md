# Milestone 12 Gate — Stock-Out

**Status:** COMPLETE  

**Branch:** `version1.2`

---

## Objective

Record Stock-Out: append-only transaction + decrease `QuantityOnHand`, with insufficient-stock rejection.

---

## What was implemented

| Item | Location |
|------|----------|
| `InsertStockOut` | `Data/InventoryTransactionRepository.cs` |
| `StockOutService` | `Services/StockOutService.cs` |
| Stock-Out form | `Forms/Inventory/StockOutForm.cs` |
| MainForm | **Stock Out** hosts form |

---

## Behaviour

| Requirement | Behaviour |
|-------------|-----------|
| FR-SOUT-002 | Product, optional customer, qty, date, selling price, user, notes |
| FR-SOUT-003 | Quantity ≥ 1 |
| FR-SOUT-004 / BR-011 | Reject when qty &gt; QuantityOnHand |
| FR-SOUT-005 | QuantityOnHand decreased |
| FR-SOUT-006 | Append-only `StockOut` row |
| FR-SOUT-007 | Inactive products blocked |
| BR-029 | Customer optional |

---

## Manual verification

1. Stock-In some quantity first.
2. **Stock Out** → product, qty ≤ on hand → success; on-hand decreases.
3. Qty &gt; on hand → *Insufficient stock* message.
4. Customer can be left blank.

---

## Next milestone

**Milestone 13 — Inventory History (read-only)**
