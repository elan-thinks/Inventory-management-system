# 15 — Implementation Milestones

**Document:** Progressive Implementation Roadmap  
**Version:** 1.0  

Build order deliberately starts from simple C# / WinForms concepts and adds complexity only when prerequisites are solid.

---

## Milestone 0 — Environment & Solution Setup

- **Objective:** Working Visual Studio solution, LocalDB ready, empty WinForms project.
- **Prerequisites:** None.
- **Files:** NovaTechIMS.sln, project, App.config connection string.
- **C# concepts:** Solution/project structure.
- **Acceptance:** Project builds and runs an empty Form.
- **Checklist:** [ ] VS project created [ ] LocalDB connection tested [ ] Folders created per 03-Project-Structure.

---

## Milestone 1 — Basic WinForms Application Shell

- **Objective:** LoginForm (UI only) + MainForm shell with menu/sidebar placeholders.
- **Files:** LoginForm, MainForm, Program.cs.
- **C# / WinForms:** Forms, controls, events, Show/Close.
- **Acceptance:** Can open MainForm from a fake “Login” button and navigate between empty placeholder screens.
- **Checklist:** [ ] LoginForm layout [ ] MainForm navigation skeleton [ ] StatusStrip present.

---

## Milestone 2 — Models and Enums

- **Objective:** All domain classes and enums compiled.
- **Files:** Models/* , Enums/*.
- **C# concepts:** Classes, properties, constructors, enums, calculated properties (StockStatus).
- **Acceptance:** Models compile; StockStatus logic unit-tested.
- **Checklist:** [ ] All entities present [ ] Enums match SRS [ ] No database code inside models.

---

## Milestone 3 — Database Setup

- **Objective:** Physical schema created in LocalDB.
- **Files:** SQL script(s) under Data/Scripts or Database project.
- **Concepts:** CREATE TABLE, PK, FK, CHECK, UNIQUE, indexes.
- **Acceptance:** Schema matches 04-Database-Design; seed Administrator user exists.
- **Checklist:** [ ] All 7 tables [ ] FKs [ ] QuantityOnHand CHECK ≥ 0 [ ] Seed data.

---

## Milestone 4 — Database Connection & Data Access Skeleton

- **Objective:** DbConnectionFactory + one sample repository (e.g., Category) working.
- **Files:** Data/DbConnectionFactory.cs, CategoryRepository.cs.
- **Concepts:** SqlConnection, SqlCommand, parameters, SqlDataReader, using/disposal.
- **Acceptance:** Can insert and read a Category from a test method or temporary button.
- **Checklist:** [ ] Parameterized queries only [ ] Connection disposed [ ] Mapping helper exists.

---

## Milestone 5 — Category CRUD

- **Objective:** Full Category list + add/edit + soft-delete with uniqueness.
- **Files:** CategoryService, CategoryListForm, CategoryEditForm.
- **Concepts:** Service layer, basic validation, DataGridView binding.
- **Acceptance:** Create / read / update / deactivate Category; duplicate name rejected.
- **Checklist:** [ ] UI validation [ ] Service validation [ ] Unique name [ ] Soft-delete.

---

## Milestone 6 — Supplier CRUD

- Same pattern as Milestone 5.
- **Acceptance:** Full Supplier CRUD + search by name.

---

## Milestone 7 — Customer CRUD

- Same pattern.
- **Acceptance:** Full Customer CRUD + search by name/phone.

---

## Milestone 8 — Product CRUD

- **Objective:** Product list/detail/edit; QuantityOnHand = 0 on create; never directly editable; stock status displayed.
- **Special:** Default Supplier vs later actual supplier distinction already in model.
- **Acceptance:** Product CRUD; status calculation correct; cannot edit QuantityOnHand on Product form.
- **Checklist:** [ ] BR-026 [ ] StockStatus [ ] Category & Supplier ComboBoxes populated from active records.

---

## Milestone 9 — Authentication

- **Objective:** Real login with hashed passwords; CurrentUser populated.
- **Files:** PasswordHasher, AuthService, UserRepository, LoginForm wired.
- **Concepts:** Hashing, session object.
- **Acceptance:** Correct credentials open MainForm; wrong credentials show error; deactivated user cannot log in.
- **Checklist:** [ ] No plain-text passwords [ ] LastLoginDate updated [ ] Logout returns to Login.

---

## Milestone 10 — Role-Based Authorization

- **Objective:** Menu items and buttons respect permanent role; services reject unauthorized calls.
- **Files:** AuthorizationService, permission checks in every sensitive service method.
- **Acceptance:** Staff cannot open User Management / Adjustment / Delegation; service throws if called directly.
- **Checklist:** [ ] UI hidden [ ] Service enforced [ ] Administrator has full access.

---

## Milestone 11 — Stock-In

- **Objective:** Record Stock-In with actual supplier; QuantityOnHand increases; history row created inside transaction.
- **Files:** StockService.RecordStockIn, StockInForm, InventoryTransactionRepository.
- **Acceptance:** Happy path + inactive product rejected + quantity ≥ 1 enforced.
- **Checklist:** [ ] Transaction used [ ] Actual supplier stored [ ] History append-only.

---

## Milestone 12 — Stock-Out

- **Objective:** Record Stock-Out (optional customer); insufficient stock rejected; quantity decreases safely.
- **Acceptance:** All Stock-Out business rules; concurrent-style conditional UPDATE works.
- **Checklist:** [ ] InsufficientStockException [ ] Optional Customer [ ] History row.

---

## Milestone 13 — Inventory History

- **Objective:** Read-only TransactionHistoryForm with filters.
- **Acceptance:** Filters by date range, type, product; no edit/delete UI.

---

## Milestone 14 — Inventory Adjustment

- **Objective:** Admin-only adjustment with mandatory reason; Previous/New/Difference recorded.
- **Acceptance:** Only Administrator can execute; history + quantity updated atomically.

---

## Milestone 15 — Delegation (SCR-020)

- **Objective:** Full Delegation Management.
- **Files:** DelegationService, DelegationManagementForm, AuthorizationService updated for effective permissions.
- **Acceptance:** Create / revoke / list; effective permissions change for recipient inside date range; expired/revoked grant nothing; non-delegatable responsibilities rejected; overlapping rejected.
- **Checklist:** [ ] All FR-DEL [ ] All BR-031–039 [ ] History retained.

---

## Milestone 16 — Reports

- **Objective:** ReportsHub + ReportViewer for the six approved reports.
- **Acceptance:** Filters work; print works; permission respected.

---

## Milestone 17 — Validation & Error Handling Polish

- **Objective:** Consistent messages, all VAL rules covered, friendly errors.
- **Acceptance:** Negative cases produce clear messages; no stack traces shown.

---

## Milestone 18 — Testing

- **Objective:** Execute the testing strategy (especially the 14 mandatory negative cases).
- **Acceptance:** All listed tests pass; evidence recorded.

---

## Milestone 19 — Final UI Polish & Integration

- **Objective:** Align layout with Visual Design v1.1 as closely as practical; end-to-end flows; documentation of known limitations.
- **Acceptance:** Complete walkthrough of all SCR-001…SCR-020; system ready for demonstration.

---

## Dependency Graph (simplified)

```
0 → 1 → 2 → 3 → 4 → 5 → 6 → 7 → 8 → 9 → 10
                                      ↓
                                 11 → 12 → 13 → 14
                                      ↓
                                     15
                                      ↓
                                 16 → 17 → 18 → 19
```
