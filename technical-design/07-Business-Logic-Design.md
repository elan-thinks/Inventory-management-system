# 07 — Business Logic Design

**Document:** Services & Business Rules Implementation  
**Version:** 1.0  

---

## 1. Service Layer Responsibilities

Every service:

1. Receives input from a Form.
2. Validates (UI validation is helpful but not sufficient).
3. Checks authorization via AuthorizationService.
4. Applies business rules.
5. Calls one or more repository methods (often inside a transaction).
6. Returns a result or throws a typed exception.

Services never open UI dialogs and never contain SQL strings (SQL lives in Data).

---

## 2. Core Services

| Service | Primary Responsibilities |
|---------|--------------------------|
| AuthService | Login, logout, password verification, load CurrentUser |
| AuthorizationService | HasPermission, CalculateEffectivePermissions |
| CategoryService | CRUD + uniqueness + soft-delete rules |
| SupplierService | CRUD + soft-delete rules |
| CustomerService | CRUD + soft-delete rules |
| ProductService | CRUD (QuantityOnHand never directly written), search/filter, stock status |
| StockService | Stock-In, Stock-Out, Inventory Adjustment (all transactional) |
| DelegationService | Create, Revoke, List, validity checks |
| UserService | Admin user management |
| ReportService | Query + filter for approved reports |
| DashboardService | Aggregate counts for SCR-002 |

---

## 3. Inventory Architecture (Critical)

### 3.1 Quantity Ownership

- `Product.QuantityOnHand` is the **single source of truth**.
- It starts at 0 when a product is created (BR-026).
- It is changed **only** by StockService methods.

### 3.2 Stock-In

1. Validate product is active.
2. Validate quantity ≥ 1.
3. Validate actual supplier is active (or at least exists).
4. Begin transaction.
5. INSERT InventoryTransaction (Type = StockIn, SupplierID = actual supplier).
6. UPDATE Product SET QuantityOnHand = QuantityOnHand + @Qty.
7. Commit.
8. Return new transaction / updated product.

### 3.3 Stock-Out

1. Validate product is active.
2. Validate quantity ≥ 1.
3. Validate quantity ≤ current QuantityOnHand (also re-checked in SQL).
4. Customer is optional.
5. Begin transaction.
6. INSERT InventoryTransaction (Type = StockOut, CustomerID optional).
7. UPDATE Product … WHERE QuantityOnHand >= @Qty (conditional).
8. If rows affected = 0 → rollback and throw InsufficientStockException.
9. Commit.

### 3.4 Inventory Adjustment (Admin only)

1. Authorization: Administrator only (not delegatable).
2. Reason mandatory.
3. Capture PreviousQuantity, NewQuantity, Difference.
4. Begin transaction.
5. INSERT InventoryTransaction (Type = Adjustment, Previous/New/Difference/Reason).
6. UPDATE Product SET QuantityOnHand = @NewQuantity (must still be ≥ 0).
7. Commit.

### 3.5 History

- InventoryTransaction is append-only (BR-030).
- No Update or Delete methods are exposed on the repository for normal use.
- Corrections are performed by a new Stock-In, Stock-Out, or Adjustment.

### 3.6 Concurrency Note

The conditional UPDATE on Stock-Out plus a single transaction is sufficient for this educational desktop system. No additional optimistic concurrency token is required.

---

## 4. Key Business Rules → Service Logic

| Rule ID | Implementation Location |
|---------|-------------------------|
| BR-003, BR-011, BR-025 | StockService.RecordStockOut + SQL CHECK / conditional UPDATE |
| BR-012 | StockService validation (quantity ≥ 1) |
| BR-013 | StockService.AdjustInventory (Reason required) |
| BR-014 | AuthorizationService + StockService / UserService |
| BR-015 | Product.StockStatus calculated property |
| BR-016 | StockService rejects inactive products |
| BR-018 | ProductService.DeleteOrDeactivate checks for existing transactions |
| BR-026 | ProductService.Create sets QuantityOnHand = 0; no transaction created |
| BR-027 | Only StockService may change QuantityOnHand |
| BR-028 | Product.SupplierID vs InventoryTransaction.SupplierID kept distinct |
| BR-029 | CustomerID nullable on Stock-Out |
| BR-030 | No update/delete on InventoryTransaction |
| BR-031–039 | DelegationService + AuthorizationService |

---

## 5. Soft-Delete / Deactivation Pattern

When a hard delete is blocked (category has products, supplier referenced, product has transactions, customer has stock-outs):

1. Service throws or returns a result indicating “blocked”.
2. UI offers “Deactivate instead”.
3. Service sets IsActive = false.
4. Inactive master data cannot be used in new Stock-In / Stock-Out.

---

## 6. Exception Types (Utilities)

- `ValidationException` — field or format problems
- `BusinessRuleException` — e.g., insufficient stock, delete blocked
- `AuthorizationException` — permission denied
- `DuplicateRecordException` — unique constraint
- `NotFoundException` — record no longer exists

Services throw these; Forms catch and display friendly text.

---

## 7. Example Service Method Signature (illustrative)

```csharp
public InventoryTransaction RecordStockOut(
    int productId,
    int quantity,
    int? customerId,
    decimal? unitPrice,
    DateTime transactionDate,
    string notes,
    CurrentUser currentUser);
```

Inside: authorize → validate → transaction → insert + conditional update → commit.
