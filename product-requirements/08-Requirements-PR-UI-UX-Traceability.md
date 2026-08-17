# 8. Product, UI & UX Requirements + Traceability

## 8.1 Product Requirements (PR-)

| ID | Requirement | Traces to SRS |
|---|---|---|
| PR-001 | The system shall provide a persistent primary navigation mechanism (sidebar) visible on every authenticated screen. | NFR-002, §16.4 |
| PR-002 | The system shall visually distinguish In Stock, Low Stock, and Out of Stock products using icon, label, and color together. | BR-015, FR-STK-001–002 |
| PR-003 | The system shall provide consistent CRUD interaction patterns across Product, Category, Supplier, and Customer. | FR-PROD/CAT/SUP/CUS, §17 |
| PR-004 | The system shall restrict Inventory Adjustment and User Management to the Administrator role at the UI level. | BR-014, §6.2 |
| PR-005 | The system shall prevent direct editing of Quantity on Hand outside Stock-In, Stock-Out, and Inventory Adjustment. | BR-027, VAL-011 |
| PR-006 | The system shall block hard deletion of any master-data record referenced by existing transactions and offer deactivation instead. | BR-008, BR-009, BR-010, BR-018 |
| PR-007 | The system shall present Inventory Transaction History as read-only, with no edit or delete controls of any kind. | BR-030, NFR-012 |
| PR-008 | The system shall require confirmation before Delete, Deactivate, Stock-Out, and Inventory Adjustment actions. | C-008, §16.4 |
| PR-009 | The system shall provide the six MVP reports (Current Inventory, Low Stock, Out-of-Stock, Stock-In, Stock-Out, Transaction History) as screen-viewable and printable. | FR-RPT-001–003 |
| PR-010 | The system shall provide search and/or filter controls on every list screen containing more than a handful of records. | FR-SRCH-001–005 |
| PR-011 | The system shall reduce all primary staff tasks (Stock-In, Stock-Out, product search) to no more than two clicks from the Dashboard. | NFR-002 |
| PR-012 | The system shall hide or disable navigation items and controls the current user's role does not permit. | §6.2.3, ER-011 |
| PR-013 | The system shall present all validation and error messages in plain, non-technical language. | NFR-003, §12 principles |
| PR-014 | The system shall never expose raw stack traces, SQL errors, or technical exception detail to the end user. | ER-007, ER-010, NFR-006/007 |
| PR-015 | The system shall preserve entered form data when a Save attempt fails validation, so the user does not need to re-enter it. | Design philosophy #6 |
| PR-016 | The system shall expose Product, Category, Supplier, and Customer master-data mutation controls only to Administrator; Inventory/Store Staff shall have view/search access only. | UX-DEC-001, SRS §6.2.2 interpreted conservatively |

## 8.2 UI Requirements (UI-)

| ID | Requirement |
|---|---|
| UI-001 | Every required field displays a red asterisk immediately after its label. |
| UI-002 | Every Add/Edit dialog places Cancel (secondary) left and Save (primary) right, in a fixed bottom action bar. |
| UI-003 | Every DataGridView master-data list places the "+ Add {Entity}" button top-right of the screen header for Administrator; Staff do not see this mutation action. |
| UI-004 | Every list-screen action column uses the same icon order: View (where applicable), Edit, Delete — rightmost column for Administrator; Staff master-data lists expose View only. |
| UI-005 | Stock status renders as a pill badge with icon, label, and status color on every screen where it appears (Product List, Details, Dashboard, Stock-Out, Reports). |
| UI-006 | The sidebar highlights the current section with a background fill and left accent bar. |
| UI-007 | The StatusStrip displays the logged-in user's name, role, and current date on every authenticated screen. |
| UI-008 | Disabled controls use the Disabled Background/Disabled Text tokens and never respond to hover. |
| UI-009 | Field-level validation errors render directly beneath the offending field in the Error color, with the field border also switching to the Error color. |
| UI-010 | Confirmation dialogs display the specific consequence of the action in plain language, never a bare "Are you sure?" |
| UI-011 | DataGridView column headers are sortable by click on every list and history screen. |
| UI-012 | Every modal dialog supports Esc to cancel/close. |
| UI-013 | Empty-state messaging always includes a next-step action where one exists (e.g., "Add your first product"). |
| UI-014 | Administrator-only master-data mutation controls are visible only to Administrator; Staff do not see Add/Edit/Delete/Deactivate controls for Products, Categories, Suppliers, or Customers. |

## 8.3 UX Requirements (UX-)

| ID | Requirement |
|---|---|
| UX-001 | A user shall be able to locate any product by name or ID in under 5 seconds using the Product List search. |
| UX-002 | A user shall be able to complete a Stock-In entry in under 30 seconds for a familiar product. |
| UX-003 | A user attempting an invalid Stock-Out shall be informed of insufficient stock before, or immediately upon, clicking Save — never after a silent failure. |
| UX-004 | A user shall never lose entered form data due to a validation error. |
| UX-005 | A user shall be able to tell, without reading fine print, whether a product is In Stock, Low Stock, or Out of Stock, from the product list alone. |
| UX-006 | A user attempting a role-restricted action shall be prevented from attempting it in the first place (hidden/disabled control) rather than told after the fact, wherever practical. |
| UX-007 | A user who cancels an Add/Edit dialog shall return to exactly the list/filter state they left. |
| UX-008 | A user completing a repeated task (Stock-In/Stock-Out) shall be able to immediately begin the next entry without extra navigation. |
| UX-009 | A user blocked from deleting a record shall be offered a clear alternative action (deactivate) in the same interaction, not a dead end. |
| UX-010 | A user shall always be able to determine their current location in the application from the sidebar and screen header alone. |
| UX-011 | Inventory/Store Staff shall be able to view and search master data without being exposed to unauthorized mutation controls. |

## 8.4 Traceability

**SRS Requirement → Product Requirement → UX Requirement → UI Requirement → Screen**

| SRS Source | Product Requirement | UX Requirement | UI Requirement | Screen(s) |
|---|---|---|---|---|
| BR-015, FR-STK-001–002 | PR-002 | UX-005 | UI-005 | SCR-002, 003, 004, 013, 016/017 |
| BR-014, §6.2 | PR-004 | UX-006 | UI-006 | SCR-014, 018, 019 |
| BR-027, VAL-011 | PR-005 | UX-004 | UI-009 | SCR-004, 005 |
| BR-008/009/010/018 | PR-006 | UX-009 | UI-010 | SCR-003, 006, 008, 010 |
| BR-030, NFR-012 | PR-007 | UX-010 | UI-004 (absence of action icons) | SCR-015 |
| C-008 | PR-008 | UX-003 | UI-010 | SCR-003/005/006–011/012/013/014/018 |
| FR-RPT-001–003 | PR-009 | UX-001 (adjacent) | UI-011 | SCR-016, 017 |
| FR-SRCH-001–005 | PR-010 | UX-001 | UI-003 | SCR-003, 006, 008, 010, 015 |
| NFR-002 | PR-011 | UX-002, UX-008 | UI-006 | SCR-002, 012, 013 |
| §6.2.3, ER-011 | PR-012 | UX-006 | UI-006, UI-008 | All role-restricted screens |
| NFR-003, §12 | PR-013 | UX-004 | UI-009 | All Add/Edit screens |
| ER-007/010, NFR-006/007 | PR-014 | — | — | All screens (global error handling) |
| Design philosophy #6 | PR-015 | UX-004, UX-007 | UI-009 | All Add/Edit screens |
| UX-DEC-001 | PR-016 | UX-011 | UI-014 | SCR-003/004/005, SCR-006/007, SCR-008/009, SCR-010/011 |

## 8.5 MVP vs. Future Separation

All screens, flows, and requirements documented in this package (documents 01–08) are **MVP**, matching SRS §22 exactly: Login/Logout, Dashboard, full CRUD for Product/Category/Supplier/Customer by Administrator, view/search master data for Inventory/Store Staff, Stock-In, Stock-Out, Administrator-only Inventory Adjustment, automatic stock status, Transaction History with search/filter, the six required reports (view + print, no export), and basic User Management.

**Explicitly not designed here (Future, per SRS §23):** report export (PDF/Excel/CSV), barcode/QR scanning, automatic purchase-order suggestions, multi-branch/warehouse, cloud sync or concurrent multi-user editing, mobile companion app, accounting/e-commerce integrations, analytics/forecasting, customer loyalty/pricing rules, batch import, serial/lot tracking, email/SMS notifications, permissions beyond the two defined roles, full field-level audit history, dark mode/advanced theming, multi-language support.

No screen, component, or interaction in documents 01–08 introduces any of the above. Anything that *would* improve the product but isn't covered by the current SRS is logged separately in document 09, not silently designed in.
