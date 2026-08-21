# 4. Main Application Shell

The shell is the one layout every authenticated screen shares. It is built once and reused, not recreated per screen — this is what makes the 19-screen application read as one product rather than 19 disconnected ones. See any file in `mockups/` except `login.html` for the shell rendered live.

## 4.1 Structure

```
┌───────────────┬──────────────────────────────────────────────────────────┐
│               │  Header: breadcrumb · screen title      search · role    │
│   Sidebar     ├──────────────────────────────────────────────────────────┤
│   220px       │                                                          │
│   fixed       │                    Content region                       │
│               │                    (fills remaining space)               │
│               │                                                          │
│               ├──────────────────────────────────────────────────────────┤
│               │  StatusStrip: user · role · date          status message│
└───────────────┴──────────────────────────────────────────────────────────┘
```

Login (SCR-001) is the sole exception — it has no sidebar or shell, since there is nothing to navigate to before authentication (`05-Screen-Specifications.md §5.1`).

## 4.2 Sidebar

- **Width:** 220px fixed, does not resize with the window; `sidebarBackground` (`#16324F`) fill — the one deliberately dark surface in the app, chosen so the navigation column reads as a distinct, always-findable landmark against the light content area.
- **Brand mark:** app icon + "NovaTech IMS" wordmark, top, 18px padding, bottom-bordered.
- **Groups:** Dashboard (ungrouped, top) → **Catalog** (Products, Categories, Suppliers, Customers) → **Inventory Operations** (Stock In, Stock Out, Inventory Adjustment, Inventory History) → **Reports** → **Administration** (User Management), matching `product-requirements/02-Information-Architecture-and-Navigation.md §2.1` exactly. Group labels are uppercase, 10.5px, muted sidebar-text color, non-interactive.
- **Nav item:** icon (18px) + label, 9px vertical padding, 4px radius, 3px transparent left border. Hover: 6% white overlay. **Active:** 12% white overlay fill, white text (weight 600), 3px `sidebarAccent` (`#5B9BD5`) left border — this satisfies UI-006 ("the sidebar highlights the current section with a background fill and left accent bar").
- **Footer:** Log Out control, icon + label, top-bordered, bottom of sidebar. If unsaved changes exist on an open form, logging out shows a confirmation dialog first (per `02 §2.2`, "What is deliberately NOT used" table and `06-CRUD…§6.11`).
- **Role visibility:** Inventory Adjustment and User Management (and its group label) carry an admin-only flag and are **not rendered** for Inventory/Store Staff — not shown-then-disabled. This is the direct visual implementation of UX-DEC-001 and PR-012/UI-014.

## 4.3 Header Bar

- `surface` fill, 1px bottom `border`, 14px vertical / 24px horizontal padding.
- **Left:** optional breadcrumb (11px, muted — e.g., "Products › Add Product") directly above the screen title (20px/600, `screenTitle` token).
- **Right:** global product-search box (fixed ~230px width, search icon + "Search products…" placeholder) — reachable from every screen, satisfying FR-SRCH quick-search access — followed by the current-role indicator.
- Screen-specific primary actions (e.g., "+ Add Product") render in this same right-hand region, immediately left of the search box, so the single primary action per screen is always in a predictable, consistent location (UI-003).

## 4.4 StatusStrip (Footer)

- `#EEF1F4` fill, 1px top `border`, 6px vertical / 24px horizontal padding, 10.5px text.
- **Left:** logged-in user's full name (bold) + role label + current date with a calendar icon — present on every authenticated screen (UI-007).
- **Right:** transient status messages (e.g., "Product saved successfully," green/`success` colored with a check-circle icon) — auto-dismissing after a few seconds per `06-CRUD…§6.9`. Left empty when there is nothing to report.

## 4.5 Role-Based Shell Behavior

Role visibility is resolved **once, at login**, from the authenticated user's role — not re-evaluated per click and not a runtime "attempt, then get blocked" pattern, per UX-006 and PR-012. Concretely, at login the application:

1. Hides the "Inventory Adjustment" and "Administration › User Management" sidebar items entirely for Staff.
2. Hides "+ Add," "Edit," and "Delete/Deactivate" controls on every master-data list and swaps the action column to View-only for Staff.
3. Shows the same Dashboard metric set to both roles (per FR-DASH-002 — the SRS does not define a reduced metric set for Staff; role differences are about navigation/mutation access, not dashboard content) but restricts the Quick Actions panel to the actions that role can actually perform.
4. Continues to show the StatusStrip role label so the logged-in user always has a plain-language confirmation of which mode they're in.

**Note on the mockups specifically:** each HTML mockup includes a live Administrator/Inventory Staff switcher in the header purely so this behavior can be inspected in a browser without needing 19×2 separate files. In the real application this is not a runtime toggle — it is resolved once at authentication and does not appear as a UI control at all.

## 4.6 Content Region

Fills all remaining space after the fixed sidebar; 32px padding on list/dashboard screens. List screens use full width; Add/Edit for master data opens as a modal child dialog over the list (context is never lost); Stock-In/Out/Adjustment are dedicated full-width-but-narrow (content-max-width ~760px) screens rather than modals, since they're used repeatedly through a shift and benefit from a stable, larger target area (`02 §2.2`, "Content region" row).

## 4.7 What the Shell Deliberately Does Not Do

- No tabbed top-level navigation (tabs are reserved only for grouping fields within a single large Add/Edit form if one ever needs it — not required by any MVP form, which are all short).
- No MDI parent/child windows.
- No menu-bar-only navigation (a supplementary File/Help-style MenuStrip may exist for Logout/About, but never as primary navigation).

These exclusions are carried over unchanged from `product-requirements/02-Information-Architecture-and-Navigation.md §2.2`.
