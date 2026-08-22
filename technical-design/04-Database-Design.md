# 04 — Database Design

**Document:** Physical Database Design  
**Version:** 1.0  
**Engine:** SQL Server LocalDB (recommended)  

---

## 1. Database Engine Decision

**Recommended: SQL Server LocalDB**

| Criterion | LocalDB | SQLite | Full SQL Server |
|-----------|---------|--------|-----------------|
| WinForms / Visual Studio integration | Excellent | Good | Excellent |
| Full T-SQL + transactions | Yes | Limited | Yes |
| Referential integrity & CHECK | Yes | Yes | Yes |
| Installation on student machines | Very simple (comes with VS) | Trivial | Heavier |
| Learning value for SQL | High | Medium | High |
| Deployment for demo | Attach .mdf or use LocalDB | Single file | Requires instance |

**Decision (ADR-002):** SQL Server LocalDB.  
It gives real SQL Server behaviour, proper transactions, and is the most common choice in university Windows Programming labs.

---

## 2. Naming Conventions

- Tables: PascalCase plural or singular consistent — we use **singular** entity names for clarity with models (`User`, `Product`, `InventoryTransaction`).
- Columns: PascalCase matching C# properties.
- Primary keys: `<Entity>ID` (INT IDENTITY).
- Foreign keys: same name as referenced PK.
- Indexes: `IX_<Table>_<Columns>`.

---

## 3. Entity / Table Definitions

### 3.1 User

| Column | Type | Nullable | Constraints / Notes |
|--------|------|----------|---------------------|
| UserID | INT | No | PK, IDENTITY(1,1) |
| Username | NVARCHAR(50) | No | UNIQUE |
| PasswordHash | NVARCHAR(256) | No | Never plain text |
| PasswordSalt | NVARCHAR(128) | No | Or combined hash storage |
| FullName | NVARCHAR(150) | No | |
| Role | NVARCHAR(30) | No | 'Administrator' or 'InventoryStaff' (CHECK) |
| IsActive | BIT | No | DEFAULT 1 |
| CreatedDate | DATETIME2 | No | DEFAULT SYSUTCDATETIME() |
| LastLoginDate | DATETIME2 | Yes | |

**Indexes:** UNIQUE on Username.

### 3.2 Category

| Column | Type | Nullable | Constraints |
|--------|------|----------|-------------|
| CategoryID | INT | No | PK, IDENTITY |
| CategoryName | NVARCHAR(100) | No | UNIQUE |
| Description | NVARCHAR(500) | Yes | |
| IsActive | BIT | No | DEFAULT 1 |
| CreatedDate | DATETIME2 | No | DEFAULT SYSUTCDATETIME() |
| ModifiedDate | DATETIME2 | Yes | |

### 3.3 Supplier

| Column | Type | Nullable | Constraints |
|--------|------|----------|-------------|
| SupplierID | INT | No | PK, IDENTITY |
| SupplierName | NVARCHAR(150) | No | |
| ContactPerson | NVARCHAR(100) | Yes | |
| Phone | NVARCHAR(25) | Yes | |
| Email | NVARCHAR(254) | Yes | |
| Address | NVARCHAR(250) | Yes | |
| IsActive | BIT | No | DEFAULT 1 |
| CreatedDate | DATETIME2 | No | DEFAULT SYSUTCDATETIME() |
| ModifiedDate | DATETIME2 | Yes | |

**Index:** IX_Supplier_SupplierName

### 3.4 Customer

| Column | Type | Nullable | Constraints |
|--------|------|----------|-------------|
| CustomerID | INT | No | PK, IDENTITY |
| CustomerName | NVARCHAR(150) | No | |
| Phone | NVARCHAR(25) | Yes | |
| Email | NVARCHAR(254) | Yes | |
| Address | NVARCHAR(250) | Yes | |
| IsActive | BIT | No | DEFAULT 1 |
| CreatedDate | DATETIME2 | No | DEFAULT SYSUTCDATETIME() |
| ModifiedDate | DATETIME2 | Yes | |

**Index:** IX_Customer_CustomerName, IX_Customer_Phone

### 3.5 Product

| Column | Type | Nullable | Constraints / Notes |
|--------|------|----------|---------------------|
| ProductID | INT | No | PK, IDENTITY |
| ProductName | NVARCHAR(100) | No | |
| CategoryID | INT | No | FK → Category |
| SupplierID | INT | No | FK → Supplier (**default/preferred** supplier) |
| Description | NVARCHAR(500) | Yes | |
| PurchasePrice | DECIMAL(18,2) | No | ≥ 0 |
| SellingPrice | DECIMAL(18,2) | No | ≥ 0 |
| QuantityOnHand | INT | No | ≥ 0; DEFAULT 0; **single source of truth** |
| MinimumStockLevel | INT | No | ≥ 0 |
| IsActive | BIT | No | DEFAULT 1 |
| CreatedDate | DATETIME2 | No | DEFAULT SYSUTCDATETIME() |
| ModifiedDate | DATETIME2 | Yes | |
| CreatedByUserID | INT | Yes | FK → User |
| ModifiedByUserID | INT | Yes | FK → User |

**CHECK:** QuantityOnHand >= 0, PurchasePrice >= 0, SellingPrice >= 0, MinimumStockLevel >= 0  
**Indexes:** IX_Product_CategoryID, IX_Product_SupplierID, IX_Product_ProductName

**Important distinction (BR-028):**  
`Product.SupplierID` = default/preferred supplier.  
`InventoryTransaction.SupplierID` (on Stock-In) = actual supplier for that receipt. They may differ.

### 3.6 InventoryTransaction (append-only)

| Column | Type | Nullable | Constraints / Notes |
|--------|------|----------|---------------------|
| TransactionID | INT | No | PK, IDENTITY |
| TransactionType | NVARCHAR(20) | No | 'StockIn', 'StockOut', 'Adjustment' (CHECK) |
| ProductID | INT | No | FK → Product |
| Quantity | INT | No | For StockIn/StockOut: > 0; for Adjustment: absolute difference or signed as designed |
| TransactionDate | DATE | No | ≤ current date |
| UnitPrice | DECIMAL(18,2) | Yes | Purchase price (In) / Selling price (Out) |
| SupplierID | INT | Yes | **Required for StockIn**; actual supplier |
| CustomerID | INT | Yes | Optional for StockOut |
| PreviousQuantity | INT | Yes | Used by Adjustment |
| NewQuantity | INT | Yes | Used by Adjustment |
| Difference | INT | Yes | Used by Adjustment (New − Previous) |
| Reason | NVARCHAR(250) | Yes | Mandatory for Adjustment |
| Notes | NVARCHAR(500) | Yes | |
| UserID | INT | No | FK → User (who performed it) |
| CreatedDateTime | DATETIME2 | No | DEFAULT SYSUTCDATETIME() |

**No UPDATE or DELETE** of rows after insert (enforced by application; optional trigger can harden).  
**Indexes:** IX_InventoryTransaction_ProductID, IX_InventoryTransaction_TransactionDate, IX_InventoryTransaction_Type

### 3.7 Delegation

| Column | Type | Nullable | Constraints / Notes |
|--------|------|----------|---------------------|
| DelegationID | INT | No | PK, IDENTITY |
| DelegatedByUserID | INT | No | FK → User (must be Administrator) |
| DelegatedToUserID | INT | No | FK → User (must be active InventoryStaff) |
| Responsibility | NVARCHAR(50) | No | 'StockIn', 'StockOut', 'ReportAccess' (CHECK) |
| StartDate | DATE | No | |
| EndDate | DATE | No | ≥ StartDate |
| Reason | NVARCHAR(250) | No | |
| Status | NVARCHAR(20) | No | 'Active', 'Expired', 'Revoked' (CHECK) |
| CreatedDateTime | DATETIME2 | No | DEFAULT SYSUTCDATETIME() |
| RevokedByUserID | INT | Yes | FK → User |
| RevokedDateTime | DATETIME2 | Yes | |

**Indexes:** IX_Delegation_DelegatedToUserID, IX_Delegation_Status, IX_Delegation_Dates  
**Application rule:** Prevent overlapping Active delegations for same (DelegatedToUserID, Responsibility).

---

## 4. Relationships & Cardinalities

```
User 1 ──────── * InventoryTransaction
User 1 ──────── * Delegation (as DelegatedBy)
User 1 ──────── * Delegation (as DelegatedTo)
User 1 ──────── * Delegation (as RevokedBy, optional)

Category 1 ──── * Product
Supplier 1 ──── * Product          (default supplier)
Supplier 1 ──── * InventoryTransaction  (actual Stock-In supplier)
Customer 1 ──── * InventoryTransaction  (optional Stock-Out)
Product  1 ──── * InventoryTransaction
```

**Referential integrity:** All FKs ON DELETE NO ACTION / RESTRICT.  
Master data that is referenced is deactivated (IsActive = 0), never hard-deleted when history exists.

---

## 5. ERD Description (Textual)

- One Category has many Products.
- One Supplier is the default supplier for many Products and may also appear as the actual supplier on many Stock-In transactions.
- One Product has many InventoryTransactions.
- One User creates many InventoryTransactions and may create/receive/revoke many Delegations.
- InventoryTransaction optionally links to Customer (Stock-Out) and to Supplier (Stock-In).
- Delegation links two Users (delegator and recipient) and optionally a third (revoker).

---

## 6. Key Integrity Rules

1. `Product.QuantityOnHand` is the only place current stock lives.
2. Quantity changes happen only inside a transaction that also inserts an InventoryTransaction row.
3. `CHECK (QuantityOnHand >= 0)` on Product.
4. Stock-Out service uses conditional update so concurrent oversell is rejected.
5. InventoryTransaction rows are never updated or deleted by normal application code.
6. Delegation EndDate ≥ StartDate (CHECK constraint + application validation).
7. Role values restricted by CHECK or lookup.

---

## 7. Seed Data (Recommended for Development)

- One Administrator user (e.g., admin / hashed password).
- One InventoryStaff user.
- A few Categories, Suppliers, Customers, and Products with QuantityOnHand = 0.
- No sample transactions required for empty start.

---

## 8. Architecture Decision Record

**ADR-002 — Database Engine**

- **Context:** Need a relational engine that supports transactions, FKs, and is easy for students.
- **Decision:** SQL Server LocalDB.
- **Alternatives:** SQLite (simpler file but weaker T-SQL experience), full SQL Server Express.
- **Consequences:** Students learn real SQL Server dialect; Visual Studio tooling works out of the box.
