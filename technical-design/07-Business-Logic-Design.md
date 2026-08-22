# 07 - Business Logic Design

**Document:** Services & Business Rules  
**Version:** 1.0  

---

## Services

AuthService, AuthorizationService, CategoryService, SupplierService, CustomerService, ProductService, StockService, DelegationService, UserService, ReportService, DashboardService.

## Inventory Architecture

- `Product.QuantityOnHand` is the single source of truth; starts at 0 on create (BR-026).
- Changed **only** by StockService.

### Stock-In
Validate active product, qty >= 1, actual supplier -> transaction: INSERT history + UPDATE qty +=.

### Stock-Out
Validate active product, qty >= 1, qty <= on hand, optional customer -> transaction: INSERT history + conditional UPDATE qty -=. If 0 rows -> InsufficientStockException.

### Adjustment (Admin only)
Mandatory reason; capture Previous/New/Difference; transaction: INSERT history + SET qty = New (>= 0).

### History
InventoryTransaction is append-only (BR-030). No update/delete methods.

## Soft-delete
When hard delete blocked: offer Deactivate (IsActive=false). Inactive products cannot be used in new stock movements.

## Exception types
ValidationException, BusinessRuleException, AuthorizationException, DuplicateRecordException, NotFoundException, InsufficientStockException.
