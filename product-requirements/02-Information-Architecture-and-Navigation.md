# 2. Information Architecture & Navigation

## 2.1 Information Architecture

| Section | Purpose | Navigation Location | Roles | Child Screens / Actions |
|---|---|---|---|---|
| **Login** | Authenticate user, load role | Entry point of app | All (pre-auth) | — |
| **Dashboard** | Orientation hub: key metrics, recent activity, quick navigation | Sidebar: top item, default post-login screen | Admin, Staff (limited) | Links into Products, Stock-In, Stock-Out, Reports |
| **Products** | Manage product master data | Sidebar | Admin, Staff (view/search only) | Product List → Add/Edit → Details |
| **Categories** | Group products | Sidebar (under "Catalog" group) | Admin (full), Staff (view) | Category List → Add/Edit |
| **Suppliers** | Record suppliers | Sidebar (under "Catalog" group) | Admin (full), Staff (view) | Supplier List → Add/Edit |
| **Customers** | Record customers | Sidebar (under "Catalog" group) | Admin (full), Staff (view) | Customer List → Add/Edit |
| **Stock In** | Record goods received | Sidebar (under "Inventory Operations" group) | Admin, Staff | Stock-In Form |
| **Stock Out** | Record goods issued | Sidebar (under "Inventory Operations" group) | Admin, Staff | Stock-Out Form |
| **Inventory Adjustment** | Correct stock quantity with reason | Sidebar (under "Inventory Operations" group) | Admin only | Adjustment Form |
| **Inventory History** | Read-only transaction log | Sidebar (under "Inventory Operations" group) | Admin, Staff | Filterable transaction list |
| **Reports** | View/print MVP reports | Sidebar | Admin, Staff (subset: Current Inventory, Low Stock, Transaction History) | Report picker → Report Viewer |
| **User Management** | Manage accounts and roles | Sidebar (bottom, Administrator-only) | Admin only | User List → Add/Edit |

**Permission decision UX-DEC-001:** Inventory/Store Staff are view/search-only for Products, Categories, Suppliers, and Customers. Administrator manages master-data creation, editing, and deactivation/deletion.

Notes:
- "Catalog" and "Inventory Operations" are logical groupings within the sidebar, not separate top-level sections — this keeps the navigation tree shallow (SRS explicitly avoids complex, deep hierarchies for a small business tool).
- Settings/system configuration is **not** included as a section because the SRS does not define system-wide settings beyond user management (C-004, out-of-scope items in §5.2). See Design Recommendations document if this is desired later.

## 2.2 Navigation Model

**Chosen strategy: Persistent left sidebar navigation with a fixed top StatusStrip/header, single main-content region, and modal/child forms for Add/Edit and confirmation.**

### Why this pattern:

- A persistent sidebar keeps every module one click away at all times — directly satisfies NFR-002 ("all primary actions reachable within a small number of clicks from the Dashboard") and the "minimize unnecessary clicks" design principle.
- It scales naturally to WinForms: the sidebar is a fixed Panel with Buttons/LinkLabels or a styled ListBox; the content region swaps UserControls or opens child forms — no MDI complexity required.
- It always communicates **where the user is** (highlighted sidebar item + page header), **where they can go** (all other sidebar items remain visible and enabled/disabled per role), and **how to return** (sidebar is always present; there's no "drill-down and get lost" state).
- Role-based visibility (hiding/disabling User Management and Inventory Adjustment for Staff) is trivial to implement as sidebar item visibility toggles at login.

### Navigation components:

| Element | Behavior |
|---|---|
| **Sidebar** (left, fixed width ~220px) | Lists all IA sections the current role can access. Current section is visually highlighted (background fill + left accent bar). Icon + label per item. |
| **Header bar** (top) | Shows current screen title, breadcrumb-style subtitle where relevant (e.g. "Products › Edit Product"), and a global search box for Product quick-search. |
| **StatusStrip** (bottom) | Shows logged-in user's name and role, current date, and transient status messages (e.g. "Product saved successfully"). |
| **Content region** | Hosts the active screen. List screens use full width; Add/Edit forms open as modal child dialogs (see 2.3) so the underlying list remains visually present and context isn't lost. |
| **Logout** | Fixed control in the header or sidebar footer. If unsaved changes exist on an open form, a confirmation dialog is shown before logging out. |

### What is deliberately NOT used:

- **Tabbed content** for top-level navigation (reserved only for grouping related fields within a single Add/Edit form, e.g., Stock-In "Details" vs "Notes," if a form grows large — not required for MVP forms, which are short).
- **MDI (Multiple Document Interface)** parent/child windows — avoided because it re-introduces window-management complexity the sidebar model already solves more simply.
- **Menu-bar-only navigation** (classic MenuStrip as the sole nav) — retained only as a supplementary File/Help-style menu (e.g., Logout, About), not as primary navigation, since a persistent sidebar is faster to scan for non-technical staff.

## 2.3 Desktop Window Strategy

| Aspect | Specification |
|---|---|
| **Minimum window size** | 1024×768 (typical low-end store PC / student development target). Application should be usable, not cramped, at this size. |
| **Main window** | Resizable, maximizable; opens maximized by default on 1366×768+ displays. Sidebar has a fixed width; content region and DataGridViews resize/anchor to fill available space. |
| **Resizing behavior** | DataGridView columns use a mix of fixed-width (ID, Status, action buttons) and fill-weighted (Name, Description) sizing so tables remain usable at both minimum and larger sizes. Forms do not need to support extreme narrow widths (no mobile responsiveness per SRS §23). |
| **Add/Edit dialogs** | Modal child forms, fixed or minimum size, centered on the main window. Not resizable (fields are static in number), except where a grid within the dialog benefits from resizing (rare in MVP). |
| **Confirmation dialogs** | Standard MessageBox-style modal, small fixed size, centered on parent. |
| **Report Viewer** | Resizable window (reports benefit from more screen space); opens maximized or large by default. |
| **Maximize/minimize** | Standard Windows behavior on the main window; modal dialogs do not offer maximize/minimize (only close). |
| **Multiple monitors** | No special handling required; standard Windows placement behavior is sufficient (A-001, single-PC assumption). |

## 2.4 Responsive Behavior (Desktop, Not Mobile)

This is a desktop-only application (SRS §23 explicitly excludes mobile responsiveness). "Responsive" here means:

- The main window and its DataGridViews adapt gracefully across common desktop resolutions (1024×768 through 1920×1080+).
- Sidebar remains fixed-width; content region uses Dock/Anchor properties to fill remaining space.
- DataGridView columns: ID-style and Status columns fixed-width; Name/Description-style columns fill remaining space proportionally; action columns (icons/buttons) fixed-width, right-aligned.
- Add/Edit dialogs do not need to reflow at different sizes — they use a static, generous field layout designed for their fixed size.
