# 12. Error Handling Requirements

| ID      | Situation                              | Expected System Behaviour |
|---------|----------------------------------------|---------------------------|
| ER-001  | Invalid login credentials               | Display “Invalid username or password” (or similar). Do not reveal which field is wrong. Remain on login form. |
| ER-002  | Required field left empty              | Prevent save. Show specific validation message. |
| ER-003  | Numeric field contains non-numeric data| Prevent save. Show message that a valid number is required. |
| ER-004  | Attempt to Stock-Out more than available stock | Prevent save. Show “Insufficient stock. Available: X”. |
| ER-005  | Attempt to delete a record that has related transactions | Prevent delete. Show clear explanatory message. |
| ER-006  | Attempt to delete a category that contains products | Prevent delete. Advise user to reassign products first. |
| ER-007  | Database connection failure            | Display a user-friendly message (“Unable to connect to the database. Please contact the administrator.”). Log technical details if possible. Do not crash. |
| ER-008  | Record not found (e.g. concurrent delete) | Display “The selected record no longer exists.” Refresh the list. |
| ER-009  | Duplicate key / unique constraint violation | Display a friendly message indicating the value already exists. |
| ER-010  | Unexpected exception                   | Catch at application level where possible. Show a generic “An unexpected error occurred” message. Log the exception details for the developer. |
| ER-011  | User attempts an action without permission | Hide or disable the control; if somehow triggered, show “You do not have permission to perform this action.” |
| ER-012  | Attempt to use an inactive product in a new transaction | Prevent the transaction and inform the user. |

**Principles**
- Never show raw stack traces or SQL errors to the end user.
- Prefer prevention (disable buttons, validate early) over recovery.
- All error messages must be in plain language appropriate for non-technical staff.
