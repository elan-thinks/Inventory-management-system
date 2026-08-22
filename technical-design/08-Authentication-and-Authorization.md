# 08 — Authentication and Authorization

**Document:** AuthN / AuthZ Design  
**Version:** 1.0  

---

## 1. Authentication (ADR-004)

### 1.1 Login Process

1. User enters Username + Password on LoginForm (SCR-001).
2. AuthService.Authenticate(username, password):
   - Load user by username (must be IsActive = true).
   - Verify password against stored hash + salt.
   - On failure → clear password field, show generic “Invalid username or password”.
   - On success → update LastLoginDate, build CurrentUser (including effective permissions), return it.
3. LoginForm opens MainForm and closes itself.
4. Only one active session per application instance (FR-AUTH-008) — simply the single CurrentUser static/singleton for the process.

### 1.2 Password Handling

- Passwords are **never** stored in plain text (FR-AUTH-007).
- Use `Rfc2898DeriveBytes` (PBKDF2) with a random salt and sufficient iterations (e.g., 100 000).
- Store both salt and hash (or a combined encoded string).
- PasswordHasher class lives in Security/.

### 1.3 Logout

- Clear CurrentUser.
- Close MainForm.
- Show LoginForm again.

### 1.4 Failed Login

- No account lockout required for MVP (keep simple).
- Message is intentionally non-specific to avoid username enumeration.

---

## 2. Authorization Model (ADR-005)

### 2.1 Permanent Roles

| Role | Base Permissions |
|------|------------------|
| Administrator | Everything (Products CUD, Categories CUD, Suppliers CUD, Customers CUD, Stock-In, Stock-Out, Adjustment, User Management, Delegation Management, all Reports, Dashboard) |
| InventoryStaff | View master data, Stock-In, Stock-Out, view selected Reports, Dashboard. **No** master-data CUD, **No** Adjustment, **No** User Management, **No** Delegation Management |

### 2.2 Effective Permissions Formula

```
EffectivePermissions(user) =
    PermanentRolePermissions(user.Role)
    ∪
    CurrentlyValidDelegations(user)
```

A delegation is currently valid when:

- Status = Active
- StartDate ≤ Today ≤ EndDate
- Not revoked

### 2.3 Permission Checking

`AuthorizationService.HasPermission(CurrentUser user, Permission permission)`

- Returns true if the permanent role grants it **or** a valid delegation grants it.
- Called at the **start of every sensitive service method**.
- UI also uses it (or a simpler role check) to hide/disable buttons, but the service is the real gate.

### 2.4 Permission Enumeration (example)

```csharp
public enum Permission
{
    ManageProducts,
    ManageCategories,
    ManageSuppliers,
    ManageCustomers,
    RecordStockIn,
    RecordStockOut,
    PerformAdjustment,
    ManageUsers,
    ManageDelegations,
    ViewReports,
    ViewDashboard
    // ...
}
```

Mapping:

- Administrator → all permissions.
- InventoryStaff → RecordStockIn, RecordStockOut, ViewReports (subset), ViewDashboard, view-only master data.
- Delegation can add RecordStockIn, RecordStockOut, or ReportAccess only.

### 2.5 Non-Delegatable Permissions

Explicitly blocked from delegation (BR-036, FR-DEL-005/008):

- ManageUsers
- PerformAdjustment
- ManageDelegations
- Master-data deletion / deactivation
- Any pure Administrator privilege

---

## 3. UI vs Service Enforcement

| Layer | Responsibility |
|-------|----------------|
| UI | Hide or disable buttons/menus the user should not see (good UX) |
| Service | Reject the operation with AuthorizationException even if the UI was bypassed |

Security **must not** depend only on hiding buttons.

---

## 4. CurrentUser Lifetime

- Created after successful login.
- Held in a simple static/singleton or passed explicitly (prefer explicit for testability, static acceptable for this project).
- Cleared on logout.
- Forms and services receive it as a parameter or via a small AppContext helper.

---

## 5. Architecture Decision Records

**ADR-004 — Authentication Approach**  
PBKDF2 hashing + simple session object. No ASP.NET Identity or external providers.

**ADR-005 — Authorization Approach**  
Role-based + explicit time-bounded delegations evaluated at runtime. Enforcement in service layer.
