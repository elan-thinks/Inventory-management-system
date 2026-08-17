# 17. CRUD Coverage Matrix

| Entity                | Create | Read (List) | Read (Detail) | Update | Delete | Search | Filter | Notes / Restrictions |
|-----------------------|--------|-------------|---------------|--------|--------|--------|--------|----------------------|
| User                  | Yes (Admin) | Yes (Admin) | Yes | Yes (Admin) | Soft (Admin) | Yes | — | Cannot delete self |
| Category              | Yes | Yes | Yes | Yes | Conditional | Yes | — | Block if products exist |
| Supplier              | Yes | Yes | Yes | Yes | Conditional / Soft | Yes | — | Block if referenced |
| Customer              | Yes | Yes | Yes | Yes | Conditional / Soft | Yes | — | Block if transactions exist |
| Product               | Yes | Yes | Yes | Yes | Conditional | Yes | Yes (Category, Supplier, Status) | Block if transactions exist |
| Stock-In Transaction  | Yes | Yes | Yes | No | No | Yes | Yes (Date, Product, Supplier) | Append-only |
| Stock-Out Transaction | Yes | Yes | Yes | No | No | Yes | Yes (Date, Product, Customer) | Append-only |
| Inventory Adjustment  | Yes (Admin) | Yes | Yes | No | No | Yes | Yes | Append-only |

**Legend**
- **Yes** = Required in Version 1.0
- **Conditional** = Allowed only when business rules permit
- **Soft** = Preferred to mark Inactive rather than physical delete
- **No** = Intentionally not supported (audit integrity)

This matrix guarantees that the finished application demonstrates full CRUD knowledge across multiple related entities plus controlled transaction recording.
