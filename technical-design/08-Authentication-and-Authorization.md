# 08 - Authentication and Authorization

**Document:** AuthN / AuthZ Design  
**Version:** 1.0  

---

## 1. Authentication (ADR-004)

### Login Process
1. User enters Username + Password on LoginForm (SCR-001).
2. AuthService.Authenticate: load active user, verify password hash, build CurrentUser with effective permissions.
3. On failure: generic "Invalid username or password".
4. On success: open MainForm.

### Password Handling
- Never store plain text (FR-AUTH-007).
- Use PBKDF2 (`Rfc2898DeriveBytes`) with random salt.
- PasswordHasher in Security/.

### Logout
Clear CurrentUser, close MainForm, show LoginForm.

---

## 2. Authorization Model (ADR-005)

### Permanent Roles

| Role | Base Permissions |
|------|------------------|
| Administrator | Everything |
| InventoryStaff | View master data, Stock-In, Stock-Out, selected Reports, Dashboard. No Adjustment, User Mgmt, Delegation Mgmt, master-data CUD |

### Effective Permissions Formula

```
EffectivePermissions(user) =
    PermanentRolePermissions(user.Role)
    U
    CurrentlyValidDelegations(user)
```

A delegation is valid when Status=Active, StartDate <= Today <= EndDate, not revoked.

### Permission Checking

`AuthorizationService.HasPermission(CurrentUser, permission)` called at the start of every sensitive service method.
UI may hide buttons, but **service is the authoritative gate**.

### Non-Delegatable
ManageUsers, PerformAdjustment, ManageDelegations, master-data deletion/deactivation.

---

## 3. CurrentUser

Created after login; cleared on logout. Holds UserID, Username, FullName, Role, EffectivePermissions.
