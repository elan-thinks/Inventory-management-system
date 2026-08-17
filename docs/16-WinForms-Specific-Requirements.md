# 16. WinForms-Specific Requirements

These requirements ensure the eventual implementation naturally exercises Windows Forms and event-driven programming.

## 16.1 Forms Required (Minimum)

- Login Form
- Main / Dashboard Form (MDI parent or navigation hub)
- Product List Form
- Product Add/Edit Form (or combined)
- Category List + Add/Edit
- Supplier List + Add/Edit
- Customer List + Add/Edit
- Stock-In Form
- Stock-Out Form
- Inventory Adjustment Form
- Transaction History / Search Form
- Report Viewer Form(s)
- User Management Form (Administrator)

## 16.2 Controls That Must Be Used Meaningfully

| Control            | Typical Usage |
|--------------------|---------------|
| TextBox            | All data entry fields |
| Button             | Save, Cancel, Search, Delete, Navigate, Print |
| Label              | Field captions, status messages, dashboard metrics |
| ComboBox           | Category, Supplier, Customer, Status, Transaction Type selection |
| DataGridView       | Product lists, transaction lists, report results |
| DateTimePicker     | Transaction dates, report date ranges |
| CheckBox           | Active/Inactive flags (or RadioButtons) |
| Panel / GroupBox   | Logical grouping of fields |
| MenuStrip / ToolStrip | Main navigation (optional but recommended) |
| StatusStrip        | Current user, date, simple messages |
| MessageBox         | Validation errors, confirmations, information |
| ErrorProvider (optional) | Inline validation feedback |

## 16.3 Event-Driven Behaviour

- Button Click events for all primary actions
- Form Load events to populate ComboBoxes and initial data
- TextChanged / KeyDown events for live search where appropriate
- CellClick or SelectionChanged on DataGridViews to load details
- FormClosing events for confirmation if needed
- Validating / Validated events for field-level validation

## 16.4 Navigation & UX Expectations

- Clear way to move between modules without losing context unnecessarily
- Consistent button placement (e.g., Save / Cancel at bottom)
- Confirmation dialog before Delete or other destructive actions
- After successful Save, either close the form or clear fields and refresh the list
- Disabled controls for actions the current user is not permitted to perform

## 16.5 Explicit Non-Goals for This Phase

- No visual design (colours, fonts, icons, layout pixel-perfection)
- No custom-drawn controls
- No WPF or third-party UI libraries required
