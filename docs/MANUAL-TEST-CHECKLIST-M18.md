# Manual Test Checklist — Milestone 18

**Purpose:** Integration / UI verification including **mandatory negative cases**.  
**Database:** Use a dedicated test database or schema — not production.

Mark each row: ✅ Pass / ❌ Fail / ⏭ N/A

---

## A. Authentication & session

| # | Case | Expected |
|---|------|----------|
| A1 | Empty username/password | Inline: enter username and password |
| A2 | Wrong password | *Invalid username or password* (no hash leak) |
| A3 | Unknown username | Same generic message as A2 |
| A4 | Inactive user | Cannot log in (generic message) |
| A5 | `admin` / `Admin@123` (first run) | MainForm opens |
| A6 | Logout | Returns to Login; session cleared |

---

## B. Authorization (negative)

| # | Case | Expected |
|---|------|----------|
| B1 | Staff opens app | No User Management, Delegations, Inventory Adjustment |
| B2 | Staff tries master-data CUD via service (if UI still shows buttons) | *You are not authorised…* or service block |
| B3 | Staff cannot perform Adjustment | Menu hidden; service rejects |
| B4 | Admin sees Adjustment / Users / Delegations | Visible and usable |

---

## C. Stock-Out negative cases

| # | Case | Expected |
|---|------|----------|
| C1 | Qty &gt; QuantityOnHand | *Insufficient stock. Available: n, requested: m.* |
| C2 | Qty = 0 or negative (UI min 1) | Cannot submit / validation |
| C3 | Inactive product | Stock-Out not allowed |
| C4 | Future transaction date | Validation error |
| C5 | Valid Stock-Out | Qty decreases; history row appears |

---

## D. Stock-In negative cases

| # | Case | Expected |
|---|------|----------|
| D1 | Inactive product | Blocked |
| D2 | Missing supplier | Validation |
| D3 | Qty &lt; 1 | Validation |
| D4 | Future date | Validation |
| D5 | Valid Stock-In | Qty increases |

---

## E. Adjustment (Admin)

| # | Case | Expected |
|---|------|----------|
| E1 | Empty reason | Validation — reason required |
| E2 | New qty = current qty | Validation — no change needed |
| E3 | New qty negative | Not allowed |
| E4 | Valid adjustment | Qty set; Adjustment in history |

---

## F. Delegation negative cases

| # | Case | Expected |
|---|------|----------|
| F1 | Recipient = Administrator | Rejected — staff only |
| F2 | Inactive recipient | Rejected |
| F3 | EndDate &lt; StartDate | Validation |
| F4 | Empty reason | Validation |
| F5 | Overlapping Active same user + responsibility | Business rule error |
| F6 | Revoke non-Active | Only Active can be revoked |
| F7 | Create valid delegation | Appears as Active |

---

## G. Master data rules

| # | Case | Expected |
|---|------|----------|
| G1 | Duplicate category name | Duplicate message |
| G2 | Delete category with products | Cannot hard-delete; offer Inactive |
| G3 | Delete product with transactions | Cannot hard-delete; offer Inactive |
| G4 | Product create | QuantityOnHand starts at 0; not editable on form |

---

## H. Reports & history

| # | Case | Expected |
|---|------|----------|
| H1 | Each of 6 report types runs | Grid opens |
| H2 | History From &gt; To | Validation |
| H3 | Print dialog opens | No crash |

---

## I. Infrastructure

| # | Case | Expected |
|---|------|----------|
| I1 | PostgreSQL stopped at login | Friendly connection message (no stack/SQL dump) |

---

## Automated unit tests

```powershell
cd src
dotnet test NovaTechIMS.Tests\NovaTechIMS.Tests.csproj
```

Covers: PasswordHasher, AuthorizationService role maps, ErrorPresenter, stock status labels.
