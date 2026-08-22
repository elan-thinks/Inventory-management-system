# 04 - Database Design

**Document:** Physical Database Design  
**Version:** 1.1 (engine amendment)  
**Engine:** PostgreSQL  

---

## ADR-002 - Database Engine (amended)

**Previous decision:** SQL Server LocalDB  

**Current decision: PostgreSQL**

| Criterion | PostgreSQL |
|-----------|------------|
| Free / open source | Yes |
| PK, FK, CHECK, UNIQUE, transactions | Yes |
| Partial unique indexes | Yes (active delegation BR-039) |
| C# support | Npgsql (Milestone 4+) |
| Avoid extra install | Uses existing developer environment |

**Reason:** PostgreSQL is already available; LocalDB would be redundant. All approved data requirements remain satisfied. Functional requirements unchanged.

**Alternatives:** SQL Server LocalDB (original), SQLite.

---

## Type mapping

| Concept | PostgreSQL |
|---------|------------|
| Identity PK | SERIAL |
| String | VARCHAR(n) |
| Boolean | BOOLEAN |
| Money | NUMERIC(18,2) |
| Date | DATE |
| Timestamp | TIMESTAMPTZ |

## Tables

User, Category, Supplier, Customer, Product, InventoryTransaction (append-only by app rule), Delegation.

Logical attributes and relationships unchanged from SRS / TD v1.0. FKs ON DELETE NO ACTION. Soft-delete preferred for master data.

Product.SupplierID = default supplier; InventoryTransaction.SupplierID on Stock-In = actual supplier (BR-028).

Scripts: `database/01-CreateDatabase.sql`, `02-CreateTables.sql`, `03-SeedOptional.sql`.
