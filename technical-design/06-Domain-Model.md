# 06 — Domain Model Design

**Document:** C# Models & Enums  
**Version:** 1.0  

---

## 1. Principles

- Models are plain C# classes (POCOs).
- They carry data and simple derived properties only.
- Business rules live in Services, not inside models.
- Database-specific concerns (connection, commands) do not appear in models.
- Enums are used for fixed sets of values (Role, TransactionType, etc.).

---

## 2. Enums

```csharp
public enum UserRole
{
    Administrator,
    InventoryStaff
}

public enum TransactionType
{
    StockIn,
    StockOut,
    Adjustment
}

public enum StockStatus
{
    OutOfStock,   // QuantityOnHand == 0
    LowStock,     // 0 < QuantityOnHand <= MinimumStockLevel
    InStock       // QuantityOnHand > MinimumStockLevel
}

public enum DelegationStatus
{
    Active,
    Expired,
    Revoked
}

public enum DelegatableResponsibility
{
    StockIn,
    StockOut,
    ReportAccess
}
```

---

## 3. Model Classes

### 3.1 User

| Property | Type | Notes |
|----------|------|-------|
| UserID | int | |
| Username | string | |
| PasswordHash | string | never displayed |
| PasswordSalt | string | or combined |
| FullName | string | |
| Role | UserRole | |
| IsActive | bool | |
| CreatedDate | DateTime | |
| LastLoginDate | DateTime? | |

No password plain-text property.

### 3.2 Category

| Property | Type |
|----------|------|
| CategoryID | int |
| CategoryName | string |
| Description | string |
| IsActive | bool |
| CreatedDate | DateTime |
| ModifiedDate | DateTime? |

### 3.3 Supplier

| Property | Type |
|----------|------|
| SupplierID | int |
| SupplierName | string |
| ContactPerson | string |
| Phone | string |
| Email | string |
| Address | string |
| IsActive | bool |
| CreatedDate | DateTime |
| ModifiedDate | DateTime? |

### 3.4 Customer

| Property | Type |
|----------|------|
| CustomerID | int |
| CustomerName | string |
| Phone | string |
| Email | string |
| Address | string |
| IsActive | bool |
| CreatedDate | DateTime |
| ModifiedDate | DateTime? |

### 3.5 Product

| Property | Type | Notes |
|----------|------|-------|
| ProductID | int | |
| ProductName | string | |
| CategoryID | int | |
| CategoryName | string | convenience for lists (not stored) |
| SupplierID | int | **default** supplier |
| SupplierName | string | convenience |
| Description | string | |
| PurchasePrice | decimal | |
| SellingPrice | decimal | |
| QuantityOnHand | int | never set directly from Product edit UI |
| MinimumStockLevel | int | |
| IsActive | bool | |
| CreatedDate | DateTime | |
| ModifiedDate | DateTime? | |
| CreatedByUserID | int? | |
| ModifiedByUserID | int? | |
| StockStatus | StockStatus | **calculated** property |

```csharp
public StockStatus StockStatus
{
    get
    {
        if (QuantityOnHand <= 0) return StockStatus.OutOfStock;
        if (QuantityOnHand <= MinimumStockLevel) return StockStatus.LowStock;
        return StockStatus.InStock;
    }
}
```

### 3.6 InventoryTransaction

| Property | Type | Notes |
|----------|------|-------|
| TransactionID | int | |
| TransactionType | TransactionType | |
| ProductID | int | |
| ProductName | string | convenience |
| Quantity | int | |
| TransactionDate | DateTime | |
| UnitPrice | decimal? | |
| SupplierID | int? | actual supplier for Stock-In |
| SupplierName | string | |
| CustomerID | int? | optional for Stock-Out |
| CustomerName | string | |
| PreviousQuantity | int? | Adjustment |
| NewQuantity | int? | Adjustment |
| Difference | int? | Adjustment |
| Reason | string | mandatory for Adjustment |
| Notes | string | |
| UserID | int | |
| UserFullName | string | convenience |
| CreatedDateTime | DateTime | |

### 3.7 Delegation

| Property | Type | Notes |
|----------|------|-------|
| DelegationID | int | |
| DelegatedByUserID | int | |
| DelegatedByName | string | convenience |
| DelegatedToUserID | int | |
| DelegatedToName | string | convenience |
| Responsibility | DelegatableResponsibility | |
| StartDate | DateTime | date only |
| EndDate | DateTime | date only |
| Reason | string | |
| Status | DelegationStatus | |
| CreatedDateTime | DateTime | |
| RevokedByUserID | int? | |
| RevokedByName | string | |
| RevokedDateTime | DateTime? | |

Helper method (can live on the model or in a service):

```csharp
public bool IsCurrentlyValid(DateTime asOfDate)
{
    return Status == DelegationStatus.Active
        && asOfDate.Date >= StartDate.Date
        && asOfDate.Date <= EndDate.Date;
}
```

---

## 4. Relationships in Code

Models may hold navigation-style convenience properties (CategoryName, SupplierName) that are populated by JOIN queries in the data layer.  
They do not hold live collections of child objects unless needed for a specific screen; lists are usually loaded separately.

---

## 5. Validation Responsibility

- Models may expose simple property-level checks if desired (e.g., `IsValidForSave()`), but authoritative validation lives in the Service layer.
- Required-field and range rules are re-checked in services before any database call.

---

## 6. CurrentUser Context

A lightweight class used throughout the application after login:

```csharp
public class CurrentUser
{
    public int UserID { get; set; }
    public string Username { get; set; }
    public string FullName { get; set; }
    public UserRole Role { get; set; }
    public List<DelegatableResponsibility> EffectivePermissions { get; set; }
}
```

`EffectivePermissions` is calculated once at login (and refreshed if needed) by combining permanent role rights with currently valid delegations.
