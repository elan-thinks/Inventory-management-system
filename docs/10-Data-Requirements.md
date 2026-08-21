# 10. Data Requirements (Logical)

No SQL is provided. This section defines the logical entities, attributes, relationships, and constraints required by the system.

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

MVP User Management allows Administrators to create, edit, deactivate, and assign roles to users.

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

Relationship: One Supplier can be the default/preferred supplier for many Products and may appear on many Stock-In transactions.

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
| Quantity | Yes | Positive for Stock-In/Out; Adjustment records the approved change and resulting quantity |
| TransactionDate | Yes | Cannot be a future date |
| UnitPrice | No | Purchase price for Stock-In; selling price for Stock-Out |
| SupplierID | No | Required for Stock-In; represents actual supplier for that receipt |
| CustomerID | No | Optional for Stock-Out |
| PreviousQuantity | No | Used for Adjustment |
| NewQuantity | No | Used for Adjustment |
| Difference | No | Used for Adjustment |
| Reason | No | Mandatory for Adjustment |
| Notes | No | |
| UserID | Yes | User who performed the action |
| CreatedDateTime | Yes | |

Relationships:
- Belongs to one Product.
- Optionally references Supplier and Customer as appropriate.
- References the User who created it.

Important constraint: InventoryTransaction records are append-only and cannot be edited or deleted after creation. Corrections are performed through a new appropriate transaction or authorized adjustment.

## 10.7 Entity: Delegation (DR-DEL)

**Purpose:** Store a controlled, time-bounded delegation of an approved operational responsibility from an Administrator to an eligible Inventory/Store Staff user.

| Attribute | Required | Notes |
|---|---|---|
| DelegationID | Yes | Unique, system-generated |
| DelegatedByUserID | Yes | FK to User; must be Administrator |
| DelegatedToUserID | Yes | FK to User; must be eligible active Inventory/Store Staff |
| Responsibility | Yes | Approved responsibility such as StockIn, StockOut, or approved ReportAccess |
| StartDate | Yes | Delegation validity start |
| EndDate | Yes | Must not precede StartDate |
| Reason | Yes | Explanation for delegation |
| Status | Yes | Active / Expired / Revoked |
| CreatedDateTime | Yes | |
| RevokedByUserID | No | FK to User; Administrator who revoked it |
| RevokedDateTime | No | Set when revoked |

Relationships:
- DelegatedByUserID references User.
- DelegatedToUserID references User.
- RevokedByUserID optionally references User.

Important constraints:
- Delegation never changes the recipient's permanent role.
- Delegation cannot grant Administrator privileges or non-delegatable responsibilities.
- Delegation is effective only inside its date range and while active.
- Delegation records are retained as history.

## 10.8 Key Relationships

```text
User 1 ─── * InventoryTransaction
Category 1 ─── * Product
Supplier 1 ─── * Product (default/preferred)
Supplier 1 ─── * InventoryTransaction (actual Stock-In supplier)
Customer 1 ─── * InventoryTransaction (optional Stock-Out customer)
Product 1 ─── * InventoryTransaction
User 1 ─── * Delegation (delegator)
User 1 ─── * Delegation (recipient)
User 1 ─── * Delegation (revoker, optional)
```

## 10.9 Important Data Constraints

- Referential integrity shall be maintained.
- Product.QuantityOnHand is the single source of current stock.
- QuantityOnHand shall start at zero for every newly created Product.
- Product creation shall not create an InventoryTransaction.
- QuantityOnHand shall be changed only through Stock-In, Stock-Out, or authorized Inventory Adjustment operations.
- Product.SupplierID identifies the default/preferred supplier.
- InventoryTransaction.SupplierID on Stock-In identifies the actual supplier for that receipt and may differ from the Product default supplier.
- InventoryTransaction.CustomerID on Stock-Out is optional.
- InventoryTransaction records are append-only.
- Historical master data referenced by transactions shall normally be deactivated rather than hard-deleted.
- User accounts required by historical transaction or delegation records shall not be hard-deleted when doing so would break audit history.
- Delegation EndDate shall not precede StartDate.
- DelegatedToUserID must reference an eligible active Inventory/Store Staff account when the delegation is created.
- Delegation responsibility must be from the approved delegatable responsibility set.
