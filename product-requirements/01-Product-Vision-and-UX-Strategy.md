# 1. Product Vision & UX Strategy

## 1.1 Product Vision

The Inventory Management System (IMS) is a **calm, trustworthy operational tool** for NovaTech Electronics — not a consumer app. It exists so a small store's owner and staff can always answer, with confidence, three questions in seconds: *"What do we have? What's running low? What just happened?"*

The product should feel:

- **Professional** — appropriate for a business owner making purchasing decisions
- **Clean** — no visual noise competing with data
- **Trustworthy** — numbers are always current and never ambiguous
- **Efficient** — staff can complete a Stock-In or Stock-Out in seconds, many times a day
- **Calm** — no urgency-manufacturing UI patterns; alerts are informative, not alarming
- **Organized** — every entity (Product, Category, Supplier, Customer, Transaction) has a predictable, consistent home
- **Modern** — clean typography, sensible spacing, and clear hierarchy, executed within real WinForms constraints
- **Practical** — every screen exists because an SRS requirement needs it, nothing more

## 1.2 Design Philosophy (Governing Principles)

1. **Clarity over decoration.** Every visual element must help the user read data or complete a task faster.
2. **Data readability over visual effects.** DataGridView content is the product; chrome around it stays quiet.
3. **Consistency across forms.** A user who learns the Product form already knows the Category, Supplier, and Customer forms.
4. **Clear navigation.** The user always knows where they are, what's next to them, and how to get back.
5. **Immediate feedback.** Every action (save, delete, stock movement) produces visible confirmation within the same screen.
6. **Prevent errors before they happen.** Disable what can't be used; validate before submission is even attempted where practical.
7. **Destructive actions require confirmation.** No silent deletes, no silent stock corrections.
8. **Important inventory information is visually prominent.** Stock status is never buried in a dense text row.
9. **Minimize unnecessary clicks.** Frequent tasks (Stock-In, Stock-Out, Search) are reachable in 1–2 clicks from the Dashboard.
10. **Realistic for WinForms.** Every pattern specified here is achievable with standard WinForms controls: TextBox, Button, Label, ComboBox, DataGridView, DateTimePicker, CheckBox, Panel/GroupBox, MenuStrip/ToolStrip, StatusStrip, MessageBox, and optionally ErrorProvider.

### Explicitly avoided (per SRS constraints C-001–C-009 and WinForms non-goals):

- Animations and transitions beyond simple, native WinForms behavior
- Overly card-heavy "dashboard app" layouts
- Large decorative illustrations
- Mobile gestures (swipe, pull-to-refresh)
- Web-only interaction patterns (infinite scroll, hover-reveal menus)
- Excessive gradients or shadow effects
- Anything requiring WPF, third-party UI libraries, or custom-drawn controls

## 1.3 User Roles

Only the two roles defined in SRS §6.2 exist. No additional roles are introduced.

### 1.3.1 Administrator

**Sees:** Every screen and module in the application, including User Management and Inventory Adjustment.

**Can do:**
- Full CRUD on Products, Categories, Suppliers, Customers (subject to BR-008, BR-009, BR-010, BR-018 deletion rules)
- Stock-In, Stock-Out
- Inventory Adjustments (Administrator-only, BR-014, FR-ADJ-001)
- Create/edit/deactivate users and assign roles (FR-USR-001–006)
- View all reports, dashboard, and full transaction history

**Cannot do:**
- Deactivate their own currently logged-in account (BR-020)
- Edit or delete historical inventory transactions (BR-030, append-only)
- Directly edit QuantityOnHand outside Stock-In/Stock-Out/Adjustment (BR-027, VAL-011)

**How restrictions are communicated:** Not applicable to most functions — Administrator restrictions are the two hard rules above, enforced by disabling the relevant control (e.g., "Deactivate" button disabled on the Administrator's own user row) with a tooltip/status message explaining why.

### 1.3.2 Inventory / Store Staff

**Sees:** Dashboard, Products (view/search), Categories/Suppliers/Customers (view/search only), Stock-In, Stock-Out, selected reports (Current Inventory, Low Stock, Transaction History), and Inventory History.

**Can do:**
- View and search Products, Categories, Suppliers, Customers
- Create Stock-In and Stock-Out transactions
- View dashboard and permitted reports
- Search and filter inventory and transaction history

**Cannot do:**
- Create, edit, delete, or deactivate Products, Categories, Suppliers, or Customers
- Perform Inventory Adjustments (Administrator-only, BR-014)
- Manage user accounts
- Change system-critical settings

**How restrictions are communicated:** Master-data mutation controls (Add, Edit, Delete/Deactivate) and other Administrator-only controls are hidden or disabled for Inventory/Store Staff rather than shown-then-blocked, per the approved UX-DEC-001 decision and SRS role-visibility rules. If a restricted action is somehow triggered, the system shows: *"You do not have permission to perform this action."*

### 1.3.3 Role Assignment Rule

- Each user has exactly one role, assigned at creation and changeable only by an Administrator.
- Role-based visibility is enforced at the UI level on every screen — this is a first-class design requirement, not an afterthought (see UI-Requirements in document 08).
