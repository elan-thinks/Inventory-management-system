# 05 - Data Access Design

**Document:** Data Access Layer  
**Version:** 1.0  

---

## 1. Technology Choice

**ADO.NET with System.Data.SqlClient (or Microsoft.Data.SqlClient)**

**ADR-003 - Data Access Technology**

- **Context:** Course requires demonstration of database interaction and SQL.
- **Decision:** Raw ADO.NET (SqlConnection, SqlCommand, SqlParameter, SqlDataReader, SqlTransaction).
- **Alternatives considered:** Entity Framework Core, Dapper.
- **Why ADO.NET:** Maximum transparency; students write and see the SQL; excellent for teaching parameterized queries, transactions, and reader mapping.

---

## 2. Connection Management

- Connection string in App.config / appsettings.
- `DbConnectionFactory.CreateConnection()` returns a new `SqlConnection`.
- Open late, close early (`using`).
- Connection lifetime = one service operation (or one explicit transaction).

## 3. Commands & Parameters

- Every SQL statement uses **parameters**. No string concatenation of user input.
- Naming: `@ProductID`, `@Quantity`, etc.

## 4. Transactions

Inventory operations that change QuantityOnHand **must** run inside a `SqlTransaction`:
1. Begin transaction
2. Insert InventoryTransaction row
3. Update Product.QuantityOnHand (conditional WHERE for Stock-Out)
4. Commit / Rollback on exception

Service layer owns the transaction scope; repositories accept optional `SqlTransaction`.

## 5. CRUD Patterns

| Entity | Create | Read | Update | Delete |
|--------|--------|------|--------|--------|
| Category, Supplier, Customer, Product, User | INSERT | SELECT | UPDATE | Soft (IsActive=0) |
| InventoryTransaction | INSERT only | SELECT | **Forbidden** | **Forbidden** |
| Delegation | INSERT | SELECT | Status to Revoked only | No hard delete |

## 6. Mapping

Manual mapping from `SqlDataReader` to models. No ORM.

## 7. SQL Injection Prevention

**Mandatory** parameterized queries for all user input.

## 8. Stock-Out Quantity Safety

```sql
UPDATE Product
SET QuantityOnHand = QuantityOnHand - @Quantity
WHERE ProductID = @ProductID
  AND QuantityOnHand >= @Quantity
  AND IsActive = 1;
-- Check @@ROWCOUNT; if 0 -> insufficient stock
```

## 9. Disposal

Every SqlConnection, SqlCommand, SqlDataReader, SqlTransaction in `using` blocks.
