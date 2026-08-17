# 9. Business Rules Catalogue

| ID     | Rule Statement |
|--------|----------------|
| BR-001 | Product Name is mandatory and must not be empty or whitespace only. |
| BR-002 | Purchase Price and Selling Price must be greater than or equal to zero. |
| BR-003 | Quantity on Hand must never be negative. |
| BR-004 | Minimum Stock Level must be greater than or equal to zero. |
| BR-005 | A product must belong to exactly one Category. |
| BR-006 | A product must be linked to exactly one primary Supplier. |
| BR-007 | Category Name must be unique. |
| BR-008 | A Category that still contains one or more products cannot be deleted. |
| BR-009 | A Supplier that is referenced by any Product or Stock-In transaction cannot be hard-deleted; it may be marked Inactive. |
| BR-010 | A Customer that has related Stock-Out transactions cannot be hard-deleted; it may be marked Inactive. |
| BR-011 | Stock-Out quantity cannot exceed the current Quantity on Hand of the product. |
| BR-012 | Stock-In and Stock-Out quantities must be positive integers (≥ 1). |
| BR-013 | Inventory Adjustment Reason is mandatory. |
| BR-014 | Only users with the Administrator role may perform Inventory Adjustments and manage user accounts. |
| BR-015 | Stock status is derived automatically:<br>• Out of Stock = Quantity = 0<br>• Low Stock = 0 < Quantity ≤ Minimum Stock Level<br>• In Stock = Quantity > Minimum Stock Level |
| BR-016 | Inactive products cannot be used in new Stock-In or Stock-Out transactions. |
| BR-017 | Every inventory movement (Stock-In, Stock-Out, Adjustment) must record the User who performed it and the Date/Time. |
| BR-018 | Product deletion is permitted only when the product has zero related inventory transactions; otherwise the user must be informed and the delete blocked. |
| BR-019 | Username must be unique across all user accounts. |
| BR-020 | A user cannot delete or deactivate their own currently logged-in account. |
| BR-021 | Dates used in transactions cannot be future dates beyond a reasonable tolerance (or system date is used by default). |
| BR-022 | Email addresses, when provided, must follow a basic valid email format. |
| BR-023 | Phone numbers, when provided, should contain only digits and common phone characters. |
| BR-024 | All monetary values are stored and displayed with a fixed number of decimal places (typically 2). |
| BR-025 | The system shall not allow a Stock-Out that would result in negative stock under any circumstances. |
