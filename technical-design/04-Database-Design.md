# 04 - Database Design

**Document:** Physical Database Design  
**Version:** 1.0  
**Engine:** SQL Server LocalDB (recommended)  

---

## ADR-002 - Database Engine

SQL Server LocalDB: full T-SQL, transactions, FKs, easy VS integration, common in university labs.

---

## Tables

### User
UserID PK IDENTITY, Username UNIQUE NVARCHAR(50), PasswordHash, PasswordSalt, FullName, Role (Administrator|InventoryStaff), IsActive BIT DEFAULT 1, CreatedDate, LastLoginDate?

### Category
CategoryID PK, CategoryName UNIQUE, Description?, IsActive, CreatedDate, ModifiedDate?

### Supplier
SupplierID PK, SupplierName, ContactPerson?, Phone?, Email?, Address?, IsActive, dates

### Customer
CustomerID PK, CustomerName, Phone?, Email?, Address?, IsActive, dates

### Product
ProductID PK, ProductName, CategoryID FK, SupplierID FK (**default** supplier), Description?, PurchasePrice DECIMAL(18,2)>=0, SellingPrice>=0, QuantityOnHand INT>=0 DEFAULT 0, MinimumStockLevel>=0, IsActive, Created/Modified, CreatedByUserID?, ModifiedByUserID?

**BR-028:** Product.SupplierID = default; InventoryTransaction.SupplierID on Stock-In = actual supplier (may differ).

### InventoryTransaction (append-only)
TransactionID PK, TransactionType (StockIn|StockOut|Adjustment), ProductID FK, Quantity, TransactionDate, UnitPrice?, SupplierID? (required for StockIn), CustomerID? (optional StockOut), PreviousQuantity?, NewQuantity?, Difference?, Reason? (required Adjustment), Notes?, UserID FK, CreatedDateTime

No UPDATE/DELETE of rows after insert.

### Delegation
DelegationID PK, DelegatedByUserID FK, DelegatedToUserID FK, Responsibility (StockIn|StockOut|ReportAccess), StartDate, EndDate (>=Start), Reason, Status (Active|Expired|Revoked), CreatedDateTime, RevokedByUserID?, RevokedDateTime?

Prevent overlapping Active (DelegatedToUserID, Responsibility).

---

## Relationships

User 1-* InventoryTransaction / Delegation  
Category 1-* Product  
Supplier 1-* Product (default) and 1-* InventoryTransaction (actual Stock-In)  
Customer 1-* InventoryTransaction (optional Stock-Out)  
Product 1-* InventoryTransaction

FKs: ON DELETE NO ACTION. Prefer soft-delete for master data referenced by history.

## Integrity

QuantityOnHand only changed via transactional Stock-In/Out/Adjustment. CHECK QuantityOnHand >= 0.
