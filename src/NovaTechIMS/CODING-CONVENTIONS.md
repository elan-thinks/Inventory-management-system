# Coding Conventions — NovaTech IMS

**Applies from Milestone 0 onward.**  
Aligned with Technical Design v1.0 (`technical-design/`).

---

## 1. Naming

| Element | Convention | Example |
|---------|------------|---------|
| Namespaces | PascalCase, match folder | `NovaTechIMS.Services` |
| Classes / Forms | PascalCase | `ProductService`, `LoginForm` |
| Methods | PascalCase | `RecordStockIn` |
| Properties | PascalCase | `QuantityOnHand` |
| Private fields | `_camelCase` | `_currentUser` |
| Local variables | camelCase | `productId` |
| Enums | PascalCase | `UserRole.Administrator` |

## 2. File organization

- One primary public type per file.
- File name = type name.
- Forms under `Forms/...`.
- No business rules inside Form classes.
- No SQL inside Form or Model classes.

## 3. Exception handling

- Use custom exception types in later milestones.
- Catch specific exceptions in Forms; show user-friendly messages.
- Never show stack traces to end users.
- Always dispose connections/commands (`using`).

## 4. SQL

- Parameterized queries only.
- Transactions for any operation that changes `QuantityOnHand`.

## 5. WinForms

- Keep event handlers thin: gather input → call service → update UI.
- Disable/hide controls according to permissions (UX only); services still enforce.

## 6. Milestone discipline

Implement only what the current milestone requires.
