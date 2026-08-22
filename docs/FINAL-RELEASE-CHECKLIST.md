# Final System Audit & Release Checklist

**Branch:** `version1.2`  
**Application version badge:** v1.0  

Scope: compare **approved** SRS → Product/UX → Visual Design → Technical Design → Implementation → Tests.  
**Rule for Milestone 19:** fix deviations from approved design; do **not** invent a new look-and-feel.

---

## 1. Requirements coverage (high level)

| Area | Status |
|------|--------|
| Auth (login, logout, hashed passwords) | Implemented |
| Roles Admin / Inventory Staff | Implemented |
| Category / Supplier / Customer / Product CRUD | Implemented |
| Soft-delete / FK-blocked hard delete | Implemented |
| Stock-In / Stock-Out / Adjustment | Implemented |
| Append-only inventory history | Implemented |
| Delegations (create/revoke/list) | Implemented (requires `database/15-delegation.sql`) |
| Reports (6 types + print) | Implemented |
| Dashboard (SCR-002 metrics + activity + quick actions) | Implemented (M19) |
| Validation / friendly errors | Implemented (M17) |
| Automated unit tests | **20/20 verified** (M18) |

---

## 2. Design tokens

`UiTheme` maps to `UI/02-Design-Tokens.md` (Primary `#1E4B8F`, sidebar `#16324F`, fonts Segoe UI, min 1024×768).

---

## 3. Known intentional gaps (MVP)

| Item | Note |
|------|------|
| Pixel-perfect mockup parity | WinForms approximation of HTML mockups |
| Tile click → pre-filtered Product list | Optional polish in design; not required for FR-DASH core |
| Export reports | Explicitly out of MVP |
| Forgot password | Explicitly out of MVP |
| DI / mocked service unit tests | Architecture uses concrete repositories |
| Manual checklist M18 | Execute before formal hand-in |

---

## 4. Pre-release steps

1. `git pull origin version1.2`
2. Apply `database/15-delegation.sql` if not already applied
3. `dotnet test` → expect 20 pass
4. Walk `docs/MANUAL-TEST-CHECKLIST-M18.md` (especially negative cases)
5. Login admin + staff; smoke each sidebar area
6. Confirm Dashboard loads metrics without error

---

## 5. Build

```powershell
cd src
dotnet build NovaTechIMS\NovaTechIMS.csproj -c Release
dotnet test NovaTechIMS.Tests\NovaTechIMS.Tests.csproj
```
