# 11. Validation Requirements

| ID       | Field / Situation                    | Rule                                                                 | System Behaviour on Failure |
|----------|--------------------------------------|----------------------------------------------------------------------|-----------------------------|
| VAL-001  | Required text fields                 | Must not be null, empty, or whitespace only                          | Show validation message; prevent save |
| VAL-002  | Product Name                         | Required, max length reasonable (e.g. 100–200 chars)                 | Block save + message |
| VAL-003  | Purchase Price / Selling Price       | Must be numeric and ≥ 0                                              | Block save + message |
| VAL-004  | Quantity (Stock-In / Stock-Out)      | Must be integer ≥ 1                                                  | Block save + message |
| VAL-005  | Quantity on Hand (display)           | Must never become negative                                           | Enforced by business rules |
| VAL-006  | Minimum Stock Level                  | Must be integer ≥ 0                                                  | Block save + message |
| VAL-007  | Email                                | If provided, must match basic email pattern                          | Block save + message |
| VAL-008  | Phone                                | If provided, allow digits, spaces, +, -, ()                          | Block save + message |
| VAL-009  | Date fields                          | Must be valid date; preferably not far in the future                 | Block save + message |
| VAL-010  | Username                             | Required, unique, reasonable length                                  | Block save + message |
| VAL-011  | Password                             | Required on create; minimum length (e.g. 6)                          | Block save + message |
| VAL-012  | Category Name                        | Required and unique                                                  | Block save + message |
| VAL-013  | Stock-Out quantity                   | Must be ≤ current QuantityOnHand                                     | Block save + clear message “Insufficient stock” |
| VAL-014  | Adjustment Reason                     | Mandatory when performing adjustment                                 | Block save + message |
| VAL-015  | ComboBox selections (Category, Supplier, etc.) | A valid selection must be made                               | Block save + message |
| VAL-016  | Duplicate prevention                 | System shall check uniqueness where required before insert           | Block save + message |

**General Behaviour**
- Validation occurs before any database write.
- Error messages shall be clear, specific, and displayed via MessageBox or inline labels.
- The focus should preferably move to the first invalid field.
