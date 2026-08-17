# 11. Validation Requirements

| ID | Field / Situation | Rule | System Behaviour on Failure |
|---|---|---|---|
| VAL-001 | Required text fields | Must not be null, empty, or whitespace only | Block save + clear message |
| VAL-002 | Product Name | Required; maximum 100 characters | Block save + message |
| VAL-003 | Product Description | Optional; maximum 500 characters | Block save + message |
| VAL-004 | Category Name | Required; maximum 100 characters; unique | Block save + message |
| VAL-005 | Supplier Name | Required; maximum 150 characters | Block save + message |
| VAL-006 | Supplier Contact Person | Optional; maximum 100 characters | Block save + message |
| VAL-007 | Customer Name | Required; maximum 150 characters | Block save + message |
| VAL-008 | Address fields | Optional; maximum 250 characters | Block save + message |
| VAL-009 | Purchase Price / Selling Price | Numeric and >= 0; maximum two decimal places | Block save + message |
| VAL-010 | Quantity (Stock-In / Stock-Out) | Integer >= 1 | Block save + message |
| VAL-011 | QuantityOnHand | Integer >= 0; cannot be directly edited from Product management | Block save + message / prevent edit |
| VAL-012 | Minimum Stock Level | Integer >= 0 | Block save + message |
| VAL-013 | Email | If provided, must match a basic valid email pattern; maximum 254 characters | Block save + message |
| VAL-014 | Phone | If provided, only digits, spaces, +, -, and parentheses; maximum 25 characters | Block save + message |
| VAL-015 | Transaction Date | Valid date and not later than current system date/time | Block save + message |
| VAL-016 | Username | Required; unique; maximum 50 characters | Block save + message |
| VAL-017 | Password | Required on account creation; minimum 6 characters | Block save + message |
| VAL-018 | Adjustment Reason | Required; maximum 250 characters | Block save + message |
| VAL-019 | Notes | Optional; maximum 500 characters | Block save + message |
| VAL-020 | Stock-Out quantity | Must not exceed current QuantityOnHand | Block save + clear “Insufficient stock” message |
| VAL-021 | ComboBox selections | A valid required selection must be made | Block save + message |
| VAL-022 | Duplicate prevention | System shall check uniqueness where required before insert/update | Block save + message |

## General Behaviour

- Validation occurs before any database write.
- Error messages shall be clear, specific, and understandable to the user.
- The first invalid field should receive focus where practical.
- Validation shall be applied consistently to both create and update operations.
