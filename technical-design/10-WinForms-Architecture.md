# 10 — WinForms Architecture

**Document:** Presentation Layer Design  
**Version:** 1.0  

---

## 1. Overall Shell

- **LoginForm** — first form shown (Application.Run).
- After success → **MainForm** becomes the application shell.
- MainForm contains:
  - MenuStrip or sidebar navigation (per Visual Design).
  - StatusStrip (current user name, role, date).
  - Content area that hosts child forms or UserControls.
- Preferred pattern for this project: **MainForm as MDI container or single-content panel** that opens child forms (Show / ShowDialog).

Navigation is driven by the approved Information Architecture (product-requirements/02).

---

## 2. Form Catalogue (Screen → Form mapping)

| Screen ID | Form Class | Notes |
|-----------|------------|-------|
| SCR-001 | LoginForm | Standalone |
| SCR-002 | DashboardForm / DashboardControl | Hosted in MainForm |
| SCR-003 | ProductListForm | |
| SCR-004 | ProductDetailForm | |
| SCR-005 | ProductEditForm | Modal or non-modal |
| SCR-006 | CategoryListForm | |
| SCR-007 | CategoryEditForm | |
| SCR-008 | SupplierListForm | |
| SCR-009 | SupplierEditForm | |
| SCR-010 | CustomerListForm | |
| SCR-011 | CustomerEditForm | |
| SCR-012 | StockInForm | |
| SCR-013 | StockOutForm | |
| SCR-014 | InventoryAdjustmentForm | Admin only |
| SCR-015 | TransactionHistoryForm | Read-only |
| SCR-016 | ReportsHubForm | |
| SCR-017 | ReportViewerForm | |
| SCR-018 | UserListForm | Admin only |
| SCR-019 | UserEditForm | Admin only |
| SCR-020 | DelegationManagementForm | Admin only |

Shared dialogs: ConfirmationDialog pattern (MessageBox is acceptable), Blocked-Delete explanation.

---

## 3. Control Mapping (approved Visual Design → WinForms)

| UI Concept | WinForms Control |
|------------|------------------|
| Text entry | TextBox |
| Password | TextBox (PasswordChar) |
| Numeric quantity / price | NumericUpDown or masked TextBox |
| Date | DateTimePicker |
| Selection lists | ComboBox |
| Tabular data | DataGridView |
| Primary / secondary actions | Button |
| Grouping | GroupBox / Panel |
| Navigation | MenuStrip, ToolStrip, or custom sidebar Panel with Buttons |
| Status | StatusStrip + ToolStripStatusLabel |
| Messages | MessageBox / Label for inline errors |
| Read-only detail | Labels or read-only TextBoxes |

DataGridView is used for all list screens and for transaction history / report results.

---

## 4. Navigation & Form Lifecycle

- MainForm keeps a reference to CurrentUser.
- Menu / sidebar click → open the corresponding form (new instance or bring existing to front).
- Child forms receive CurrentUser (constructor or property).
- After successful Save on an edit form → close form and refresh the list form if it is open.
- FormClosing can prompt when there are unsaved changes (optional for MVP).
- Logout: clear CurrentUser, close MainForm, show LoginForm.

---

## 5. Event Handling Patterns

- Form.Load → populate ComboBoxes, load initial grid data.
- Button.Click → call service → handle success / exception.
- DataGridView.SelectionChanged or CellDoubleClick → open detail / edit.
- TextBox.TextChanged (optional) → live filter.
- Validating events for field-level checks (supplement service validation).

Keep event handlers thin: gather data → call service → update UI.

---

## 6. Passing Data

- Constructor injection of IDs or models for Edit/Detail forms.
- Or public properties set before Show/ShowDialog.
- Avoid static global state beyond CurrentUser.

---

## 7. Permission-Aware UI

On Form.Load (or when CurrentUser is set):

```csharp
bool canManage = AuthorizationService.HasPermission(CurrentUser, Permission.ManageProducts);
btnAdd.Visible = canManage;
btnEdit.Visible = canManage;
btnDelete.Visible = canManage;
```

Same pattern for every restricted control.  
This is UX only; services still enforce.

---

## 8. Avoiding Giant Forms

- Extract grid-loading and search logic into private methods.
- For complex screens, consider a small presenter/helper class, but pure code-behind is acceptable for this course.
- Do not put SQL or business rules inside the Form class.

---

## 9. Consistency Rules

- Save / Cancel buttons in the same relative location on every edit form.
- Primary action button is the AcceptButton.
- Escape cancels where appropriate.
- After delete/deactivate → refresh grid and show confirmation MessageBox.
