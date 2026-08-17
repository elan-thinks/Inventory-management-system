# 13. Audit and History Requirements

## 13.1 Inventory Transaction History (Mandatory)

Every Stock-In, Stock-Out, and Inventory Adjustment **shall** be permanently recorded with:

- Transaction ID
- Transaction Type
- Product
- Quantity (and previous/new quantity for adjustments)
- Date and time
- User who performed the action
- Related Supplier or Customer (where applicable)
- Unit price (where applicable)
- Reason / Notes

These records form the definitive history of stock movements and **shall not be editable or deletable** by normal users.

## 13.2 Master Data Audit (Recommended)

Where practical, the system should record:

- CreatedDate / CreatedBy
- ModifiedDate / ModifiedBy

on Product, Category, Supplier, Customer, and User entities.

Full old-value / new-value change logging is **not required** for Version 1.0 (to keep complexity manageable).

## 13.3 Login History (Optional)

Recording last login date/time per user is useful but not mandatory.

## 13.4 Principles

- Inventory movements are append-only.
- Corrections are made by creating a new Adjustment transaction, never by editing history.
- The combination of Product.QuantityOnHand + full transaction history must always be reconcilable.
