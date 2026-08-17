# 10. Data Requirements (Logical)

No SQL is provided. This section defines the logical entities, attributes, and relationships required by the system.

---

## 10.1 Entity: User (DR-USR)

**Purpose**: Store login credentials and role information.

| Attribute          | Required | Notes |
|--------------------|----------|-------|
| UserID             | Yes      | Unique identifier (system-generated) |
| Username           | Yes      | Unique |
| PasswordHash       | Yes      | Never store plain text |
| FullName           | Yes      | |
| Role               | Yes      | Administrator / InventoryStaff |
| IsActive           | Yes      | Boolean |
| CreatedDate        | Yes      | |
| LastLoginDate      | No       | |

---

## 10.2 Entity: Category (DR-CAT)

**Purpose**: Group products.

| Attribute          | Required | Notes |
|--------------------|----------|-------|
| CategoryID         | Yes      | Unique |
| CategoryName       | Yes      | Unique |
| Description        | No       | |
| IsActive           | Yes      | |
| CreatedDate        | Yes      | |
| ModifiedDate       | No       | |

**Relationships**: One Category has many Products.

---

## 10.3 Entity: Supplier (DR-SUP)

**Purpose**: Record product suppliers.

| Attribute          | Required | Notes |
|--------------------|----------|-------|
| SupplierID         | Yes      | Unique |
| SupplierName       | Yes      | |
| ContactPerson      | No       | |
| Phone              | No       | |
| Email              | No       | |
| Address            | No       | |
| IsActive           | Yes      | |
| CreatedDate        | Yes      | |
| ModifiedDate       | No       | |

**Relationships**: One Supplier supplies many Products; appears on many Stock-In transactions.

---

## 10.4 Entity: Customer (DR-CUS)

**Purpose**: Record customers who receive stock (sales).

| Attribute          | Required | Notes |
|--------------------|----------|-------|
| CustomerID         | Yes      | Unique |
| CustomerName       | Yes      | |
| Phone              | No       | |
| Email              | No       | |
| Address            | No       | |
| IsActive           | Yes      | |
| CreatedDate        | Yes      | |
| ModifiedDate       | No       | |

**Relationships**: One Customer can have many Stock-Out transactions.

---

## 10.5 Entity: Product (DR-PROD)

**Purpose**: Core inventory item.

| Attribute              | Required | Notes |
|------------------------|----------|-------|
| ProductID              | Yes      | Unique (system-generated) |
| ProductName            | Yes      | |
| CategoryID             | Yes      | FK to Category |
| SupplierID             | Yes      | FK to Supplier |
| Description            | No       | |
| PurchasePrice          | Yes      | ≥ 0 |
| SellingPrice           | Yes      | ≥ 0 |
| QuantityOnHand         | Yes      | ≥ 0, maintained by transactions |
| MinimumStockLevel      | Yes      | ≥ 0 |
| IsActive               | Yes      | |
| CreatedDate            | Yes      | |
| ModifiedDate           | No       | |
| CreatedByUserID        | No       | |
| ModifiedByUserID       | No       | |

**Relationships**:
- Belongs to one Category
- Linked to one primary Supplier
- Has many Inventory Transactions

**Derived**: Stock Status (In Stock / Low Stock / Out of Stock) calculated from QuantityOnHand and MinimumStockLevel.

---

## 10.6 Entity: InventoryTransaction (DR-TXN)

**Purpose**: Immutable record of every stock movement.

| Attribute              | Required | Notes |
|------------------------|----------|-------|
| TransactionID          | Yes      | Unique |
| TransactionType        | Yes      | StockIn / StockOut / Adjustment |
| ProductID              | Yes      | FK |
| Quantity               | Yes      | Positive for In/Out; signed or absolute for Adjustment |
| TransactionDate        | Yes      | |
| UnitPrice              | No       | Purchase price for In, Selling price for Out |
| SupplierID             | No       | Required for StockIn |
| CustomerID             | No       | Optional/Required for StockOut |
| PreviousQuantity       | No       | Used for Adjustment |
| NewQuantity            | No       | Used for Adjustment |
| Reason                 | No       | Mandatory for Adjustment |
| Notes                  | No       | |
| UserID                 | Yes      | Who performed the action |
| CreatedDateTime        | Yes      | |

**Relationships**:
- Belongs to one Product
- Optionally references Supplier or Customer
- References the User who created it

**Important Constraint**: Once created, a transaction record should not be editable or deletable (append-only for audit integrity). Corrections are done via new Adjustment transactions.

---

## 10.7 Key Relationships Summary

```
User 1 ─── * InventoryTransaction
Category 1 ─── * Product
Supplier 1 ─── * Product
Supplier 1 ─── * InventoryTransaction (StockIn)
Customer 1 ─── * InventoryTransaction (StockOut)
Product 1 ─── * InventoryTransaction
```

## 10.8 Important Data Constraints

- Referential integrity must be maintained.
- QuantityOnHand on Product is the single source of current stock and is updated only through controlled transaction operations.
- Soft-delete (IsActive = false) is preferred for master data that has historical references.
