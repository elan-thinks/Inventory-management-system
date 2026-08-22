# 05 — Data Access Design

**Document:** Data Access Layer  
**Version:** 1.0  

---

## 1. Technology Choice

**ADO.NET with System.Data.SqlClient (or Microsoft.Data.SqlClient)**

**ADR-003 — Data Access Technology**

- **Context:** Course requires demonstration of database interaction and SQL.
- **Decision:** Raw ADO.NET (SqlConnection, SqlCommand, SqlParameter, SqlDataReader, SqlTransaction).
- **Alternatives considered:** Entity Framework Core, Dapper.
- **Why ADO.NET:** Maximum transparency; students write and see the SQL; excellent for teaching parameterized queries, transactions, and reader mapping. EF would hide too much for this educational goal.

---

## 2. Connection Management

- Connection string stored in configuration (App.config / appsettings).
- `DbConnectionFactory.CreateConnection()` returns a new `SqlConnection`.
- Connections are opened as late as possible and closed/disposed as early as possible (`using` statements).
- No long-lived connection held by the UI or by services across user actions.
- Connection lifetime = one service operation (or one explicit transaction spanning a few commands).

```csharp
// Pattern used everywhere
using (var conn = DbConnectionFactory.CreateConnection())
{
    conn.Open();
    // ...
}
```

---

## 3. Commands & Parameters

- Every SQL statement uses **parameters**. No string concatenation of user input.
- Prefer stored procedures only if the instructor requires them; otherwise parameterized text commands are sufficient and clearer for learning.
- Naming: `@ProductID`, `@Quantity`, etc.

Example pattern:

```csharp
using (var cmd = new SqlCommand(sql, conn, transaction))
{
    cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;
    cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;
    // ...
}
```

---

## 4. Transactions

Inventory operations that change QuantityOnHand **must** run inside a `SqlTransaction`:

1. Begin transaction.
2. Insert InventoryTransaction row.
3. Update Product.QuantityOnHand (with conditional WHERE for Stock-Out).
4. Commit.
5. On any exception → Rollback.

The service layer owns the transaction scope; repositories accept an optional `SqlTransaction` parameter so they participate cleanly.

```csharp
using (var conn = ...)
{
    conn.Open();
    using (var tx = conn.BeginTransaction())
    {
        try
        {
            // repository calls with tx
            tx.Commit();
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }
}
```

---

## 5. CRUD Patterns per Entity

| Entity | Create | Read | Update | Delete / Soft |
|--------|--------|------|--------|---------------|
| Category, Supplier, Customer, Product, User | INSERT | SELECT by ID / list with filters | UPDATE | Soft (IsActive=0) or conditional hard delete |
| InventoryTransaction | INSERT only | SELECT with filters | **Forbidden** | **Forbidden** |
| Delegation | INSERT | SELECT | Status change to Revoked only | No hard delete |

Repositories expose methods such as:

- `GetById(int id)`
- `GetAll(filters)`
- `Insert(entity, transaction?)`
- `Update(entity)`
- `Deactivate(int id)`
- `ExistsWithName(...)` for uniqueness checks

---

## 6. Mapping

- Manual mapping from `SqlDataReader` to model objects (simple, educational).
- Helper methods such as `MapProduct(SqlDataReader r)` keep mapping in one place.
- No ORM magic.

---

## 7. Error Handling at Data Layer

- Catch `SqlException`.
- Translate unique-constraint violations into a domain-friendly message or rethrow as a custom `DuplicateRecordException`.
- All other database errors bubble up; the service layer decides the user message.
- Always dispose connections/commands even on error (`using`).

---

## 8. SQL Injection Prevention

- **Mandatory** use of parameters for every value that originates from the user or from untrusted input.
- Never build SQL with string interpolation of user text.
- Dynamic ORDER BY or column names, if ever needed, are restricted to a white-list.

---

## 9. Stock-Out Quantity Safety

```sql
UPDATE Product
SET QuantityOnHand = QuantityOnHand - @Quantity,
    ModifiedDate = SYSUTCDATETIME()
WHERE ProductID = @ProductID
  AND QuantityOnHand >= @Quantity
  AND IsActive = 1;

-- Check @@ROWCOUNT; if 0 → insufficient stock or concurrent change
```

This, inside a transaction with the history insert, gives the required protection for this project.

---

## 10. Disposal & Resource Discipline

- Every `SqlConnection`, `SqlCommand`, `SqlDataReader`, `SqlTransaction` is created inside a `using` block or explicitly disposed.
- Readers are closed before the connection is returned or reused.

---

## 11. Testing the Data Layer

- Integration tests against LocalDB (or a dedicated test database).
- Verify insert → read round-trip.
- Verify transaction rollback leaves QuantityOnHand unchanged.
- Verify unique constraint behaviour.
