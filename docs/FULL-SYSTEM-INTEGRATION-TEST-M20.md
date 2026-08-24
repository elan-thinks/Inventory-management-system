# Full System Integration Test — Milestone 20

**Branch:** `version1.3`  
**Purpose:** End-to-end verification of NovaTech IMS as one coherent system.  
**Environment:** Dedicated PostgreSQL database (not shared production).  

Mark each row: ✅ Pass / ❌ Fail / ⏭ N/A — add notes if Fail.

**Pre-flight**

```powershell
git checkout version1.3
git pull origin version1.3
cd src
dotnet build NovaTechIMS.sln
dotnet test NovaTechIMS.Tests\NovaTechIMS.Tests.csproj
```

| Pre-check | Result |
|-----------|--------|
| Build SUCCESS | |
| Tests 20/20 | |
| PostgreSQL up | |
| Seed users work | |

---

## 1. Authentication & session (Critical)

| # | Steps | Expected | Result |
|---|-------|----------|--------|
| 1.1 | Launch app | Login form, light theme | |
| 1.2 | Empty credentials → Sign In | Validation message; stay on login | |
| 1.3 | Wrong password | Generic invalid message; no stack/SQL | |
| 1.4 | Valid Admin login | MainForm; full nav for Admin | |
| 1.5 | Sign Out | Login again; session cleared | |
| 1.6 | Valid Staff login | MainForm; limited nav | |
| 1.7 | Inactive user (if seeded) | Cannot log in | |

---

## 2. Shell & navigation (High)

| # | Steps | Expected | Result |
|---|-------|----------|--------|
| 2.1 | Admin: open every nav item | Screen loads; no crash | |
| 2.2 | Staff: Users / Delegations / Adjustment | Hidden or blocked | |
| 2.3 | Breadcrumb / title update | Matches current screen | |
| 2.4 | Status strip shows user + date | Correct | |

---

## 3. Master data journey (Critical)

Execute as **Admin** in order. Prefer unique names with a test prefix (e.g. `M20-`).

| # | Steps | Expected | Result |
|---|-------|----------|--------|
| 3.1 | Categories: Add → appears in grid | Create works | |
| 3.2 | Suppliers: Add → appears | Create works | |
| 3.3 | Customers: Add → appears | Create works | |
| 3.4 | Products: Add using category + supplier | Qty on hand = 0 | |
| 3.5 | Edit product name | Save reflects in list | |
| 3.6 | Search / status filters on each list | Filters work | |
| 3.7 | Duplicate category name | Business/validation error | |
| 3.8 | Delete category that has products | Blocked; offer inactive path if designed | |

---

## 4. Inventory operations chain (Critical)

Use the product created in §3 (or a known seed product).

| # | Steps | Expected | Result |
|---|-------|----------|--------|
| 4.1 | Stock-In: valid qty + supplier | On-hand increases; success message | |
| 4.2 | Inventory History | Stock-In row present | |
| 4.3 | Stock-Out: qty ≤ on-hand | On-hand decreases | |
| 4.4 | Stock-Out: qty > on-hand | Insufficient stock; no change | |
| 4.5 | Adjustment (Admin): set new qty + reason | On-hand matches new qty | |
| 4.6 | History shows Adjustment | Type/diff coherent | |
| 4.7 | Products list qty matches movements | Consistency | |

---

## 5. History & reports (High)

| # | Steps | Expected | Result |
|---|-------|----------|--------|
| 5.1 | History: date range includes test txns | Rows visible | |
| 5.2 | History: filter by type | Filter works | |
| 5.3 | History: no Edit/Delete columns | Read-only | |
| 5.4 | Reports: Current Inventory | Grid opens | |
| 5.5 | Reports: Low Stock / Out-of-Stock | Runs without crash | |
| 5.6 | Reports: Stock-In / Stock-Out / Txn History (dates) | Runs; back to hub works | |
| 5.7 | Print dialog (History or Report) | Opens; cancel OK | |

---

## 6. Users & delegations (High)

| # | Steps | Expected | Result |
|---|-------|----------|--------|
| 6.1 | Users: list loads | Admin only | |
| 6.2 | Add staff user (or edit existing) | Save OK | |
| 6.3 | Self-deactivate blocked (if applicable) | Cannot lock self out | |
| 6.4 | Delegation: create valid Active | Appears Active | |
| 6.5 | Overlapping Active same user+responsibility | Rejected | |
| 6.6 | Revoke Active | Status Revoked; Staff loses grant | |
| 6.7 | Staff with Stock-In delegation | Sees Stock-In while Active | |

---

## 7. Authorization regression (Critical)

| # | Steps | Expected | Result |
|---|-------|----------|--------|
| 7.1 | Staff: no User Management | Hidden | |
| 7.2 | Staff: no Adjustment | Hidden | |
| 7.3 | Staff: can Stock-In/Out if permitted | Works | |
| 7.4 | Staff: cannot manage master data CUD if policy forbids | Blocked | |

---

## 8. Error & resilience (High)

| # | Steps | Expected | Result |
|---|-------|----------|--------|
| 8.1 | Stop PostgreSQL → login | Friendly connection message | |
| 8.2 | Restart PostgreSQL → login again | Success | |
| 8.3 | Invalid report date range (From > To) | Validation; no crash | |

---

## 9. UI regression smoke (Medium)

| # | Steps | Expected | Result |
|---|-------|----------|--------|
| 9.1 | Light theme on Login + Main | Not dark/black theme | |
| 9.2 | Dashboard loads metrics or empty-safe | No unhandled exception | |
| 9.3 | Open form in View Designer (spot-check 2 forms) | Designer opens | |

---

## 10. Sign-off

| Item | Value |
|------|--------|
| Tester | |
| Date | |
| Machine / OS | |
| Critical fails | |
| High fails | |
| Blockers | |
| Decision | **PASS** / **FAIL** / **PASS WITH DEVIATIONS** |
| Notes | |

Update `src/MILESTONE-20-GATE.md` when this checklist is finished.

Related: `MANUAL-TEST-CHECKLIST-M18.md` (deeper negatives), `HOW-TO-RUN-TESTS.md`, `FINAL-RELEASE-CHECKLIST.md`.
