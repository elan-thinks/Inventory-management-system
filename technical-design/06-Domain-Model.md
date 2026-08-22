# 06 - Domain Model Design

**Document:** C# Models & Enums  
**Version:** 1.0  

---

## Enums

```csharp
UserRole { Administrator, InventoryStaff }
TransactionType { StockIn, StockOut, Adjustment }
StockStatus { OutOfStock, LowStock, InStock }
DelegationStatus { Active, Expired, Revoked }
DelegatableResponsibility { StockIn, StockOut, ReportAccess }
```

## Models (key properties)

**User:** UserID, Username, PasswordHash, PasswordSalt, FullName, Role, IsActive, CreatedDate, LastLoginDate?

**Category:** CategoryID, CategoryName, Description?, IsActive, CreatedDate, ModifiedDate?

**Supplier:** SupplierID, SupplierName, ContactPerson?, Phone?, Email?, Address?, IsActive, dates

**Customer:** CustomerID, CustomerName, Phone?, Email?, Address?, IsActive, dates

**Product:** ProductID, ProductName, CategoryID, SupplierID (default), Description?, PurchasePrice, SellingPrice, QuantityOnHand, MinimumStockLevel, IsActive, Created/Modified + by user IDs.  
**StockStatus** calculated: 0 -> OutOfStock; <= Min -> LowStock; else InStock.

**InventoryTransaction:** TransactionID, Type, ProductID, Quantity, TransactionDate, UnitPrice?, SupplierID? (actual for Stock-In), CustomerID? (optional Stock-Out), Previous/New/Difference/Reason for Adjustment, Notes?, UserID, CreatedDateTime

**Delegation:** DelegationID, DelegatedByUserID, DelegatedToUserID, Responsibility, StartDate, EndDate, Reason, Status, CreatedDateTime, RevokedByUserID?, RevokedDateTime?

**CurrentUser:** UserID, Username, FullName, Role, EffectivePermissions list

Models are POCOs. Business rules live in Services.
