# 9. Business Rules Catalogue

| ID | Rule Statement |
|---|---|
| BR-001 | Product Name is mandatory and must not be empty or whitespace only. |
| BR-002 | Purchase Price and Selling Price must be greater than or equal to zero. |
| BR-003 | Quantity on Hand must never be negative. |
| BR-004 | Minimum Stock Level must be greater than or equal to zero. |
| BR-005 | A product must belong to exactly one Category. |
| BR-006 | A product must be linked to exactly one primary/default Supplier. |
| BR-007 | Category Name must be unique. |
| BR-008 | A Category that still contains one or more products cannot be hard-deleted; it may be marked Inactive. |
| BR-009 | A Supplier referenced by any Product or Inventory Transaction cannot be hard-deleted; it may be marked Inactive. |
| BR-010 | A Customer referenced by any Stock-Out transaction cannot be hard-deleted; it may be marked Inactive. |
| BR-011 | Stock-Out quantity cannot exceed the current Quantity on Hand of the product. |
| BR-012 | Stock-In and Stock-Out quantities must be positive integers (≥ 1). |
| BR-013 | Inventory Adjustment Reason is mandatory. |
| BR-014 | Only users with the Administrator role may perform Inventory Adjustments and manage user accounts. |
| BR-015 | Stock status is derived automatically: Out of Stock = Quantity = 0; Low Stock = 0 < Quantity ≤ Minimum Stock Level; In Stock = Quantity > Minimum Stock Level. |
| BR-016 | Inactive products cannot be used in new Stock-In or Stock-Out transactions. |
| BR-017 | Every inventory movement (Stock-In, Stock-Out, Adjustment) must record the User who performed it and the date/time. |
| BR-018 | A product may be hard-deleted only when it has zero related inventory transactions; otherwise deletion is blocked and the product may be marked Inactive. |
| BR-019 | Username must be unique across all user accounts. |
| BR-020 | A user cannot delete or deactivate their own currently logged-in account. |
| BR-021 | Transaction dates cannot be future dates; the system date/time is the default. |
| BR-022 | Email addresses, when provided, must follow a basic valid email format. |
| BR-023 | Phone numbers, when provided, may contain digits, spaces, +, -, and parentheses. |
| BR-024 | Monetary values use a fixed two-decimal representation. |
| BR-025 | The system shall not allow any Stock-Out operation that would result in negative stock. |
| BR-026 | A newly created Product always starts with QuantityOnHand = 0. Product creation does not create an inventory movement. |
| BR-027 | Inventory quantity changes are performed only through Stock-In, Stock-Out, or authorized Inventory Adjustment operations. |
| BR-028 | Product.SupplierID represents the product's default/preferred supplier. The SupplierID recorded on a Stock-In transaction represents the actual supplier for that receipt and may differ from the product's default supplier. |
| BR-029 | CustomerID on a Stock-Out transaction is optional. A Stock-Out may be recorded without a registered customer. |
| BR-030 | Inventory Transaction records are append-only. They cannot be edited or deleted after creation; corrections must be made through a new appropriate transaction or adjustment. |
