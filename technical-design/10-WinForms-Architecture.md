# 10 - WinForms Architecture

**Document:** Presentation Layer Design  
**Version:** 1.0  

---

## 1. Overall Shell

- LoginForm first (Application.Run).
- After success: MainForm as application shell with navigation + StatusStrip + content area.

## 2. Form Catalogue (Screen -> Form)

| Screen | Form |
|--------|------|
| SCR-001 | LoginForm |
| SCR-002 | DashboardForm |
| SCR-003-005 | ProductListForm, ProductDetailForm, ProductEditForm |
| SCR-006-007 | CategoryListForm, CategoryEditForm |
| SCR-008-009 | SupplierListForm, SupplierEditForm |
| SCR-010-011 | CustomerListForm, CustomerEditForm |
| SCR-012 | StockInForm |
| SCR-013 | StockOutForm |
| SCR-014 | InventoryAdjustmentForm |
| SCR-015 | TransactionHistoryForm |
| SCR-016-017 | ReportsHubForm, ReportViewerForm |
| SCR-018-019 | UserListForm, UserEditForm |
| SCR-020 | DelegationManagementForm |

## 3. Control Mapping

TextBox, ComboBox, NumericUpDown, DateTimePicker, DataGridView, Button, Label, Panel, GroupBox, MenuStrip/ToolStrip, StatusStrip, MessageBox.

## 4. Navigation

MainForm holds CurrentUser. Menu/sidebar opens child forms. After Save: refresh list. Logout: clear session, return to Login.

## 5. Event Handling

Thin handlers: gather input -> call service -> update UI. Form.Load populates ComboBoxes and grids.

## 6. Permission-Aware UI

On Load, show/hide/disable controls using AuthorizationService. Service still enforces.

## 7. Avoid Giant Forms

No SQL or business rules inside Form classes.
