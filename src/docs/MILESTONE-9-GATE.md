# Milestone 9 Gate — Authentication (hashed passwords)

**Status:** COMPLETE  

**Branch:** `version1.2`  

**Database:** PostgreSQL (via Npgsql)

---

## Objective

Replace demo login with real credential checks against the `User` table using **PBKDF2** hashes (never plain text).

---

## What was implemented

| Item | Location |
|------|----------|
| `PasswordHasher` (PBKDF2-SHA256, 100k iterations, random salt) | `Security/PasswordHasher.cs` |
| `UserRepository` | `Data/UserRepository.cs` |
| `AuthService` (login + bootstrap admin) | `Services/AuthService.cs` |
| `SessionContext` | `Utilities/SessionContext.cs` |
| `AuthenticationException` | `Utilities/AppExceptions.cs` |
| LoginForm → AuthService | `Forms/LoginForm.cs` |
| MainForm receives `CurrentUser` | `Forms/MainForm.cs` |

---

## Behaviour (FR-AUTH)

| ID | Behaviour |
|----|-----------|
| FR-AUTH-001 | Username + password required |
| FR-AUTH-002 | Credentials checked against stored hash + salt |
| FR-AUTH-003 | Generic error: *Invalid username or password* |
| FR-AUTH-004 | MainForm only after successful login |
| FR-AUTH-005 | Role loaded into `CurrentUser` (UI gating = M10) |
| FR-AUTH-006 | Logout clears session and returns to Login |
| FR-AUTH-007 | Passwords stored only as hash + salt |
| FR-AUTH-008 | Single in-process session via `SessionContext` |
| FR-USR-006 | Inactive users cannot log in |

---

## Default administrator (first run)

If the `User` table is **empty**, the first login attempt creates:

| Field | Value |
|-------|-------|
| Username | `admin` |
| Password | `Admin@123` |
| Role | Administrator |
| Full name | System Administrator |

Change this password after first login when User Management is available (M10).

---

## Manual verification

1. Ensure PostgreSQL schema is applied and connection string works.
2. F5 → Login.
3. Wrong password → *Invalid username or password*.
4. `admin` / `Admin@123` → MainForm with real full name and role.
5. Logout → back to Login; session cleared.

---

## Intentionally NOT implemented

- Role-based UI / service permission gates (**Milestone 10**)
- User Management CRUD screens (started with auth; full admin UI with M10)
- Account lockout after failed attempts (optional future)

---

## Next milestone

**Milestone 10 — Role-based authorization**
