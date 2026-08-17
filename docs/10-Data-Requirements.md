# 10. Data Requirements (Logical)

No SQL is provided. This section defines the logical entities, attributes, and relationships required by the system.

## 10.1 Entity: User (DR-USR)

**Purpose:** Store login credentials, identity, status, and role information.

| Attribute | Required | Notes |
|---|---|---|
| UserID | Yes | Unique, system-generated |
| Username | Yes | Unique |
| PasswordHash | Yes | Never store plain text |
| FullName | Yes | |
| Role | Yes | Administrator / InventoryStaff |
| IsActive | Yes | Boolean |
| CreatedDate | Yes | |
| LastLoginDate | No | |

## 10.2 Entity: Category (DR-CAT)

**Purpose:** Group products.

| Attribute | Required | Notes |
|---|---|---|
| CategoryID | Yes | Unique |
| CategoryName | Yes | Unique |
| Description | No | |
| IsActive | Yes | Boolean |
| CreatedDate | Yes | |
| ModifiedDate | No | |

Relationship: One Category has many Products.

## 10.3 Entity: Supplier (DR-SUP)

**Purpose:** Record suppliers.

| Attribute | Required | Notes |
|---|---|---|
| SupplierID | Yes | Unique |
| SupplierName | Yes | |
| ContactPerson | No | |
| Phone | No | |
| Email | No | |
| Address | No | |
| IsActive | Yes | Boolean |
| CreatedDate | Yes | |
| ModifiedDate | No | |

Relationship: One Supplier supplies many Products and may appear on many Stock-In transactions.

## 10.4 Entity: Customer (DR-CUS)

**Purpose:** Record customers when applicable to Stock-Out transactions.

| Attribute | Required | Notes |
|---|---|---|
| CustomerID | Yes | Unique |
| CustomerName | Yes | |
| Phone | No | |
| Email | No | |
| Address | No | |
| IsActive | Yes | Boolean |
| CreatedDate | Yes | |
| ModifiedDate | No | |

Relationship: One Customer can have many Stock-Out transactions; Stock-Out CustomerID is optional.

## 10.5 Entity: Product (DR-PROD)

**Purpose:** Core inventory item.

| Attribute | Required | Notes |
|---|---|---|
| ProductID | Yes | Unique, system-generated |
| ProductName | Yes | |
| CategoryID | Yes | FK to Category |
| SupplierID | Yes | FK to default/preferred Supplier |
| Description | No | |
| PurchasePrice | Yes | ≥ 0 |
| SellingPrice | Yes | ≥ 0 |
| QuantityOnHand | Yes | ≥ 0; starts at 0 and is maintained only by inventory operations |
| MinimumStockLevel | Yes | ≥ 0 |
| IsActive | Yes | Boolean |
| CreatedDate | Yes | |
| ModifiedDate | No | |
| CreatedByUserID | No | FK to User |
| ModifiedByUserID | No | FK to User |

Relationships:
- Belongs to one Category.
- Has one default/preferred Supplier.
- Has many Inventory Transactions.

Important rule: Product creation never creates a Stock-In or other inventory transaction. QuantityOnHand starts at 0.

Derived: Stock Status is calculated from QuantityOnHand and MinimumStockLevel.

## 10.6 Entity: InventoryTransaction (DR-TXN)

**Purpose:** Immutable record of every stock movement.

| Attribute | Required | Notes |
|---|---|---|
| TransactionID | Yes | Unique |
| TransactionType | Yes | StockIn / StockOut / Adjustment |
| ProductID | Yes | FK to Product |
| Quantity | Yes | Positive for Stock-In/Out; Adjustment stores the approved change according to the adjustment design |
| TransactionDate | Yes | Cannot be a future date |
| UnitPrice | No | Purchase price for Stock-In; selling price for Stock-Out |
| SupplierID | No | Required for Stock-In; represents actual supplier for that receipt |
| CustomerID | No | Optional for Stock-Out |
| PreviousQuantity | No | Used for Adjustment |
| NewQuantity | No | Used for Adjustment |
| Reason | No | Mandatory for Adjustment |
| Notes | No | |
| UserID | Yes | User who performed the action |
| CreatedDateTime | Yes | |

Relationships:
- Belongs to one Product.
- Optionally references Supplier and Customer as appropriate.
- References the User who created it.

Important constraint: InventoryTransaction records are append-only and cannot be edited or deleted after creation. Corrections are performed through a new appropriate transaction or authorized adjustment.

## 10.7 Key Relationships

```text
User 1 ─── * InventoryTransaction
Category 1 ─── * Product
Supplier 1 ─── * Product
Supplier 1 ─── * InventoryTransaction (StockIn)
Customer 1 ─── * InventoryTransaction (StockOut, optional)
Product 1 ─── * InventoryTransaction
```

## 10.8 Important Data Constraints

- Referential integrity shall be maintained.
- Product.QuantityOnHand is the single source of current stock.
- QuantityOnHand shall start at zero for every newly created Product.
- QuantityOnHand shall be changed only through Stock-In, Stock-Out, or authorized Inventory Adjustment operations.
- Historical master data referenced by transactions shall normally be deactivated rather than hard-deleted.
- User accounts required by historical transaction records shall not be hard-deleted when doing so would break audit history.
