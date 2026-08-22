# 12 — Audit and History Design

**Document:** Audit / History  
**Version:** 1.0  

---

## 1. Inventory Transaction History

- Table: `InventoryTransaction`
- Nature: **append-only / immutable** after insert.
- Every Stock-In, Stock-Out, and Adjustment creates exactly one row.
- Columns capture who (UserID), when (CreatedDateTime + TransactionDate), what (Product, Quantity, Type), and supporting context (Supplier / Customer / Reason / Previous & New quantities).
- No application method allows UPDATE or DELETE of these rows.
- Corrections are performed by issuing a new compensating transaction or an authorized Adjustment.

## 2. Delegation History

- Table: `Delegation`
- Creation is recorded with DelegatedByUserID + CreatedDateTime.
- Revocation changes Status to Revoked and fills RevokedByUserID + RevokedDateTime.
- Rows are never hard-deleted.
- Grid and detail views can show the full lifecycle.

## 3. Master-Data Attribution

- Product (and optionally other entities) store CreatedByUserID / ModifiedByUserID.
- Soft-delete (IsActive = false) preserves the record so historical transactions remain meaningful.

## 4. User Attribution Rule

Every inventory movement and every delegation action is attributed to the **actual logged-in user** who performed it (CurrentUser.UserID).  
Delegation does not impersonate; the Staff member who records a Stock-In under a delegated permission is still the UserID on the transaction.

## 5. Mutable vs Immutable Summary

| Record Type | Mutable? | Notes |
|-------------|----------|-------|
| InventoryTransaction | No | Append-only |
| Delegation (core data) | Status + revocation fields only | History preserved |
| Product / Category / etc. | Yes (except QuantityOnHand) | Soft-delete preferred |
| User | Yes (except self-deactivate) | Soft-delete preferred |

## 6. Reporting & Viewing

- TransactionHistoryForm (SCR-015) is the primary read-only viewer with date, type, and product filters.
- Reports reuse the same underlying queries.
