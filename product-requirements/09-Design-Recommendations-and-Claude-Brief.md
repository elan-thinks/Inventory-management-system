# 9. Design Recommendations and Claude Brief

## 9.1 Resolved — Staff Create/Edit Rights

Inventory/Store Staff are view/search-only for Products, Categories, Suppliers, and Customers. Administrator-only controls provide Create, Edit, Delete, and Deactivate actions for master data.

This resolves the ambiguity identified by Claude without expanding Staff privileges. See `project-management/PRODUCT-UX-AUDIT.md` and decision UX-DEC-001.

## 9.2 Design Recommendations Requiring Future Approval

Any future enhancement not supported by SRS v1.0 must be approved before entering MVP. Examples include persisted filter state, advanced analytics, export features, barcode scanning, cloud synchronization, or other workflow expansion.

## Claude Visual Design Brief

The application should feel professional, calm, trustworthy, efficient, and desktop-native. Prioritize readable data, consistent forms, clear navigation, restrained visual hierarchy, accessible status indicators, and WinForms-realistic controls. Avoid excessive animation, gradients, decorative effects, or mobile-style interaction patterns.

Use a coherent professional palette, Windows-available typography, consistent spacing, reusable button/input/table states, and icons from one coherent family. Inventory status must use icon + label + color rather than color alone.

The dashboard should prioritize current inventory, low/out-of-stock visibility, recent activity, and quick access to Stock-In, Stock-Out, and search. No advanced charts are required for MVP.
