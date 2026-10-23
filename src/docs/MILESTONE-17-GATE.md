# Milestone 17 Gate — Validation + error polish

**Status:** COMPLETE  

**Branch:** `version1.2`

---

## Objective

Align exception hierarchy and UI messaging with Technical Design §11: three validation layers, no stack traces / raw SQL to users, consistent tone.

---

## What was implemented

| Item | Location |
|------|----------|
| Full exception hierarchy | `Utilities/AppExceptions.cs` |
| `InsufficientStockException` | used by Stock-Out |
| `ErrorPresenter` | `Utilities/ErrorPresenter.cs` |
| `DbExceptionMapper` | `Utilities/DbExceptionMapper.cs` |
| Global UI + domain handlers | `Program.cs` |
| Login / navigation safety | `LoginForm`, `MainForm` |

---

## Principles enforced

1. **UI** — empty required fields checked before service call where practical.  
2. **Service** — authoritative VAL / BR rules throw typed `AppException`s.  
3. **Database** — UNIQUE / FK / CHECK mapped to friendly messages.  
4. **Display** — `ErrorPresenter` / mapped messages only; technical detail via `Debug.WriteLine`.  
5. **AuthZ** — generic *You are not authorised…* (no internal permission names).

---

## Manual verification

1. Wrong login → generic invalid credentials (no DB dump).  
2. Stop PostgreSQL → connection-friendly message on login.  
3. Stock-Out qty &gt; on hand → *Insufficient stock. Available: n, requested: m.*  
4. Force a duplicate category name → duplicate-friendly message.

---

## Next milestone

**Milestone 18 — Testing (incl. negative cases)**
