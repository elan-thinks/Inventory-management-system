# 11 — Validation and Error Handling

**Document:** Validation Layers & Exception Strategy  
**Version:** 1.0  

---

## 1. Three Layers of Validation

| Layer | Purpose | Examples |
|-------|---------|----------|
| UI | Immediate feedback, reduce round-trips | Required field empty, non-numeric text |
| Business / Service | Authoritative domain rules | Insufficient stock, overlapping delegation, inactive product |
| Database | Final safety net | CHECK constraints, UNIQUE, FK, NOT NULL |

UI validation never replaces service validation.

---

## 2. Validation Rules Mapping (selected)

| VAL ID | Layer(s) | Implementation |
|--------|----------|----------------|
| VAL-001…008 | UI + Service | Required / length checks |
| VAL-009 | UI + Service + DB CHECK | Price ≥ 0, 2 decimals |
| VAL-010 | UI + Service | Quantity ≥ 1 |
| VAL-011 | Service + DB | QuantityOnHand never set from Product edit |
| VAL-012 | UI + Service + DB | Min stock ≥ 0 |
| VAL-013,014 | UI + Service | Email / phone format |
| VAL-015 | UI + Service | Transaction date ≤ today |
| VAL-016,017 | Service | Username unique, password min length |
| VAL-018 | Service | Adjustment reason required |
| VAL-020 | Service + SQL | Stock-Out ≤ QuantityOnHand |
| VAL-023…029 | Service | All delegation rules |

---

## 3. Error Flow

```
Database error / constraint violation
        ↓
Data Access (may wrap SqlException)
        ↓
Service (translates to ValidationException / BusinessRuleException / etc.)
        ↓
Form (catch, display friendly MessageBox or inline label)
```

- Never show stack traces or raw SQL errors to the user.
- Log technical detail with Debug.WriteLine or a simple file logger if desired.

---

## 4. Custom Exception Hierarchy

```csharp
public class AppException : Exception { ... }

public class ValidationException : AppException { ... }
public class BusinessRuleException : AppException { ... }
public class AuthorizationException : AppException { ... }
public class DuplicateRecordException : AppException { ... }
public class NotFoundException : AppException { ... }
public class InsufficientStockException : BusinessRuleException { ... }
```

Forms typically:

```csharp
try
{
    _stockService.RecordStockOut(...);
    MessageBox.Show("Stock-Out recorded successfully.");
    // refresh
}
catch (ValidationException ex)
{
    MessageBox.Show(ex.Message, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
}
catch (InsufficientStockException)
{
    MessageBox.Show("Insufficient stock for this product.", "Stock", ...);
}
catch (AuthorizationException)
{
    MessageBox.Show("You are not authorised to perform this action.", "Access Denied", ...);
}
catch (Exception)
{
    MessageBox.Show("An unexpected error occurred. Please try again or contact support.", "Error", ...);
}
```

---

## 5. User Message Principles

- Specific enough to correct the problem (“End date cannot be before start date”).
- Never reveal internal authorization logic beyond “You are not authorised…”.
- Consistent tone across the application.
