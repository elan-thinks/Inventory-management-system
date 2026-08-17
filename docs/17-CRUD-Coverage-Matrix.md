# 17. CRUD Coverage Matrix

| Entity | Create | Read (List) | Read (Detail) | Update | Delete | Search | Filter | Notes / Restrictions |
|---|---|---|---|---|---|---|---|---|
| User | Yes (Admin) | Yes (Admin) | Yes | Yes (Admin) | Deactivate (Admin) | Yes | Role/Status | Basic User Management is MVP; cannot deactivate self |
| Category | Yes | Yes | Yes | Yes | Conditional | Yes | Status | Hard delete blocked if products exist |
| Supplier | Yes | Yes | Yes | Yes | Conditional / Soft | Yes | Status | Hard delete blocked if referenced |
| Customer | Yes | Yes | Yes | Yes | Conditional / Soft | Yes | Status | Hard delete blocked if referenced by Stock-Out |
| Product | Yes | Yes | Yes | Yes | Conditional / Soft | Yes | Category, Supplier, Status | QuantityOnHand is not directly editable; hard delete blocked if transactions exist |
| Stock-In Transaction | Yes | Yes | Yes | No | No | Yes | Date, Product, Supplier | Append-only; actual supplier recorded |
| Stock-Out Transaction | Yes | Yes | Yes | No | No | Yes | Date, Product, Customer | Append-only; Customer optional |
| Inventory Adjustment | Yes (Admin) | Yes | Yes | No | No | Yes | Date, Product | Append-only |

**Legend**
- **Yes** = Required in MVP
- **Conditional** = Allowed only when business rules permit
- **Soft / Deactivate** = Preserve historical integrity rather than physical deletion
- **No** = Intentionally unsupported after creation
