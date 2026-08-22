# Current UI Gap Analysis

**Status:** AWAITING APPROVAL — analysis only; **no code changes** in this deliverable.  
**Date:** 2026-08-22  
**Branch:** `version1.2`  

## Purpose

Compare **actual running UI** evidence in `UI/current-ui/` (developer screenshots) against the **approved visual baseline** represented by:

| Source | Role |
|--------|------|
| `UI/update-ui/UI-mock-updated/UI-mock-updated/*.html` | Updated visual mockups (staging approved target) |
| `UI/UI-mock/*.html` | Canonical package mockups |
| `UI/02-Design-Tokens.md` | Color, type, spacing tokens |
| `UI/04-Application-Shell.md` | Shell / sidebar / header / StatusStrip |
| `UI/05-Screen-Designs.md` | SCR-001–019 layouts & UX |
| `UI/11-Visual-Design-v1.1-Delegation-Amendment.md` | SCR-020 |

**Out of scope for this document:** redesign proposals, implementation, new features beyond approved specs.

**Evidence set (`UI/current-ui/`):** Login, Dashboard, Products, Categories/Suppliers (list pattern), Customers, Stock In, Stock Out, Inventory Adjustment, Inventory History, Reports, User Management, Delegations (screenshots dated 2026-08-22).

---

## Severity definitions

| Level | Meaning |
|-------|---------|
| **Critical** | Blocks correct understanding of product state, shows incorrect product claims, or violates core shell/token identity |
| **High** | Clear deviation from approved screen structure or component pattern that users will notice every session |
| **Medium** | Spec’d interaction or visual treatment missing/wrong; workflow still usable |
| **Low** | Polish, density, naming, or WinForms default chrome vs mock |

---

## Cross-cutting (all shell screens)

| ID | Issue | Approved expectation | Current observation | Severity |
|----|--------|----------------------|---------------------|----------|
| X-01 | **Window title bar color** | App chrome aligned with primary brand (`#1E4B8F` family) / restrained professional shell | **Teal/green** system title bar on Login and MainForm | **Critical** |
| X-02 | **DataGridView selection** | Subtle selected row (surface + primary tint), readable text | Full-row **bright system blue** with white text (default WinForms) | **High** |
| X-03 | **Status / type presentation** | Pill badges for stock status, role, transaction type (component library) | Plain text cells (`Active`, `Low Stock`, `Stock-In`, `Administrator`) | **High** |
| X-04 | **Action column headers** | Human labels (`Edit`, `Delete`, `Revoke`) or icon-only actions | Literal property names **`colEdit`**, **`colDelete`**, **`colDeactivate`**, **`colRevoke`** visible in grids | **High** |
| X-05 | **Sidebar active item** | Accent treatment per shell spec (active state + optional accent) | Grey highlight block; functional but flatter than mock | **Medium** |
| X-06 | **List empty / sparse density** | Empty-state copy when no rows; comfortable padding | Large empty white grid body when few rows (functional but sparse vs mock density) | **Low** |
| X-07 | **Typography scale on metrics** | ~24px / 600 weight metric numbers (SCR-002) | Large bold numbers present; tile chrome thinner than mock cards | **Medium** |

---

## SCR-001 — Login

**Evidence:** `Screenshot 2026-08-22 193621.png`  
**Approved:** `login.html` + §5.1

| ID | Issue | Approved | Current | Severity |
|----|--------|----------|---------|----------|
| L-01 | **Footer product claim** | Version footnote only; no “demo login” messaging after auth is real | Text: *“Milestone 1 shell — any username/password opens the app.”* — **false** vs current auth | **Critical** |
| L-02 | **Primary action width / pairing** | Single full-width primary **Login** | Side-by-side **Sign In** + **Exit** | **High** |
| L-03 | **Field affordances** | User / lock icons in fields (mock) | Plain labels + textboxes | **Medium** |
| L-04 | **Error presentation** | Inline error banner with tint when credentials fail | Not shown in screenshot (needs verify on fail path); design requires banner not only label | **Medium** *(verify)* |
| L-05 | **Window behavior** | Login not maximizing as content shell | Dialog-style centered card — **aligned** in spirit | **Low** (OK) |

---

## SCR-002 — Dashboard

**Evidence:** `193712` / wider dashboard shot  
**Approved:** `dashboard.html` + §5.2

| ID | Issue | Approved | Current | Severity |
|----|--------|----------|---------|----------|
| D-01 | **Metric tile chrome** | Card tiles with icon + caption + large number | Thin bordered boxes; **no icons** | **Medium** |
| D-02 | **Low / Out emphasis** | Warning / error colored numbers | Low stock amber, Out red — **partially aligned** | **Low** (OK direction) |
| D-03 | **Recent activity type column** | Type **badge**; signed qty coloring | Plain “Stock-In” text; full-row blue selection | **High** (with X-02/X-03) |
| D-04 | **Quick actions** | Button stack; Staff vs Admin sets | Present; Admin shows Adjustment + Manage Users — **role idea aligned** | **Low** (OK) |
| D-05 | **Tile → filtered navigation** | Optional polish: Low-Stock tile → product list filtered | **Not observed** | **Medium** |
| D-06 | **Empty activity copy** | Specific empty message + path to Stock-In | Not visible when data exists; empty path unconfirmed in set | **Medium** *(verify)* |

---

## SCR-003 — Product List

**Evidence:** Products grid screenshot  
**Approved:** `product-list.html` + §5.3

| ID | Issue | Approved | Current | Severity |
|----|--------|----------|---------|----------|
| P-01 | **Action header labels** | Edit / Delete (or icons) | **`colEdit` / `colDelete`** | **High** |
| P-02 | **Stock status** | Pill badge (In / Low / Out) | Plain “Low Stock” text | **High** |
| P-03 | **Active status** | Distinct inactive treatment | “Active” plain text | **Medium** |
| P-04 | **Row actions** | View / Edit / Delete (Admin); View only (Staff) | Edit + Delete only; **no View/detail** | **Medium** |
| P-05 | **Toolbar** | Search + filters + Clear Filters | Search + filters present; **Clear Filters** not evident as link | **Medium** |
| P-06 | **Header primary** | “+ Add Product” top-right | “+ Add” + Refresh — acceptable naming variance | **Low** |

---

## SCR-006–011 — Category / Supplier / Customer lists

**Evidence:** Customers (clear); Categories/Suppliers same list pattern as Products  
**Approved:** category/supplier/customer management mockups + §5.6–5.8

| ID | Issue | Approved | Current | Severity |
|----|--------|----------|---------|----------|
| M-01 | **Shared list pattern** | Same anatomy as product list | Search + status filter + Add + grid — **structurally close** | — |
| M-02 | **Action headers** | Edit / Delete | Likely same `col*` pattern if same control factory | **High** *(confirm on Category/Supplier shots)* |
| M-03 | **Status badges** | Active/Inactive pills | Plain text | **Medium** |
| M-04 | **Blocked-delete dialog** | Explains FK count + Mark Inactive | Not in screenshot set — **verify** on delete-with-refs | **High** *(verify)* |

---

## SCR-012 — Stock-In

**Evidence:** `193912`  
**Approved:** `stock-in.html` + §5.9

| ID | Issue | Approved | Current | Severity |
|----|--------|----------|---------|----------|
| SI-01 | **Layout** | Narrow ~760px dedicated form column | Wide form + large recent grid below — denser ops layout | **Medium** |
| SI-02 | **Preview banner** | “New quantity on hand will be: X” info banner | Only “Current quantity on hand —” static label | **High** |
| SI-03 | **Secondary action** | **Clear Form** | Missing in screenshot | **Medium** |
| SI-04 | **Confirm before commit** | Confirmation dialog stating qty/product/supplier/new on-hand | Not visible in evidence — **verify** | **High** *(verify)* |
| SI-05 | **Grouped sections** | Product & Supplier → Movement → Notes | Flat 2-column field placement | **Medium** |

---

## SCR-013 — Stock-Out

**Evidence:** `193933`  
**Approved:** `stock-out.html` + §5.10

| ID | Issue | Approved | Current | Severity |
|----|--------|----------|---------|----------|
| SO-01 | **Available-qty banner** | High-weight banner under product; error coloring when insufficient | “Current quantity on hand —” only; no live insufficient styling in shot | **High** |
| SO-02 | **Live insufficient UX** | Field border + message + disable Save **together** | Relies more on post-submit service message (M17) | **High** |
| SO-03 | **Clear Form** | Required secondary | Missing | **Medium** |
| SO-04 | **Confirm dialog** | Before commit | Unverified in screenshots | **High** *(verify)* |

---

## SCR-014 — Inventory Adjustment

**Evidence:** `193949`  
**Approved:** `inventory-adjustment.html` + §5.11

| ID | Issue | Approved | Current | Severity |
|----|--------|----------|---------|----------|
| AD-01 | **Admin warning banner** | Persistent “Administrator-only…” banner above form | **Absent** | **High** |
| AD-02 | **Prev → New → Diff flow** | Explicit left-to-right with chevrons; Diff color-coded | Flat labels; Diff shows `0` without emphasis | **High** |
| AD-03 | **Primary button copy** | “Save Adjustment” | “**Record**” | **Medium** |
| AD-04 | **Reason emphasis** | Mandatory multiline with weight | Single-line Reason + Notes — weaker hierarchy | **Medium** |
| AD-05 | **Post-success navigation** | To Inventory History with new row visible | Unverified | **Medium** *(verify)* |

---

## SCR-015 — Inventory History

**Evidence:** `194008`  
**Approved:** `inventory-history.html` + §5.12

| ID | Issue | Approved | Current | Severity |
|----|--------|----------|---------|----------|
| IH-01 | **Read-only banner** | Persistent “Read-only record…” info banner | **Absent** | **Medium** |
| IH-02 | **No Actions column** | Absolute — no edit/delete | **Aligned** (no actions) | **Low** (OK) |
| IH-03 | **Signed quantity styling** | Green + / red − | Plain qty | **Medium** |
| IH-04 | **Type badges** | Badge per type | Plain text | **Medium** |
| IH-05 | **Print** | Print on toolbar | **Not visible** | **Medium** |

---

## SCR-016 / SCR-017 — Reports

**Evidence:** `194028`  
**Approved:** `reports.html` + §5.13

| ID | Issue | Approved | Current | Severity |
|----|--------|----------|---------|----------|
| RP-01 | **Hub layout** | **3-column report-type cards** (icon + name + description) | Single **dropdown** “Select report” on a small card | **High** |
| RP-02 | **Staff card visibility** | Staff only allowed report cards rendered | Unverified in Admin-only screenshots | **Medium** *(verify)* |
| RP-03 | **Viewer** | Separate larger window + Print + Back preserving filters | Separate window claimed in footer note — Print path exists in code; chrome not in set | **Medium** |
| RP-04 | **Generate labeling** | “Generate” | “Run report” — copy variance | **Low** |

---

## SCR-018 / SCR-019 — User Management

**Evidence:** `194047`  
**Approved:** `user-management.html` + §5.14

| ID | Issue | Approved | Current | Severity |
|----|--------|----------|---------|----------|
| U-01 | **Column headers** | Edit / Deactivate | **`colEdit` / `colDeactivate`** | **High** |
| U-02 | **Role badge** | Admin distinct badge vs Staff pill | Plain text “Administrator” / “Inventory Staff” | **Medium** |
| U-03 | **Self-deactivate control** | Disabled + tooltip on own row | Deactivate shown on `admin` row same as others | **High** |
| U-04 | **Add User** | Header primary | “+ Add User” present — **aligned** | **Low** (OK) |

---

## SCR-020 — Delegations

**Evidence:** `194103`  
**Approved:** `UI/update-ui/.../delegation-management.html` + amendment §11

| ID | Issue | Approved | Current | Severity |
|----|--------|----------|---------|----------|
| DG-01 | **Status presentation** | Active / Expired / Revoked **status chips** | Plain “Active” | **High** |
| DG-02 | **Revoke column header** | Revoke | **`colRevoke`** | **High** |
| DG-03 | **Filter labels** | Full responsibility labels | Truncated “All responsibilit” | **Medium** |
| DG-04 | **Create entry** | “New Delegation” / form modal per amendment | “+ New” — acceptable; modal not in set | **Low** |
| DG-05 | **Details / read-only view** | View details without edit | Only list + Revoke in shot | **Medium** |

---

## Coverage gaps in evidence (not scored as product bugs)

These screens/states were **not** clearly present in `UI/current-ui/` and need capture before re-audit:

- Login **error** state  
- Category / Supplier grids close-up (confirm `col*` headers)  
- Product / Category **Add-Edit modals**  
- Stock-In/Out **confirmation** and **insufficient stock** live state  
- Staff-role sidebar (hidden Admin items)  
- Report **viewer** window  
- Delegation **create** modal  

---

## Priority rollup (for later fix planning — not implementing now)

### Critical
1. **L-01** — Remove false Milestone-1 “any password” login footnote  
2. **X-01** — Title bar / shell chrome color vs brand tokens  

### High
3. **X-04 / P-01 / U-01 / DG-02** — Fix grid action column **HeaderText** (`colEdit` → Edit, etc.)  
4. **X-02 / X-03** — Selection styling + status/type **badges**  
5. **SI-02 / SO-01 / SO-02** — Stock movement **preview / available** banners + live insufficient UX  
6. **AD-01 / AD-02** — Adjustment warning banner + Diff visual hierarchy  
7. **RP-01** — Reports hub **cards** vs single dropdown  
8. **U-03** — Self-deactivate disabled state  
9. **DG-01** — Delegation status chips  
10. Confirm **SI-04 / SO-04 / M-04** dialogs against design  

### Medium
- Login field icons / full-width primary  
- Dashboard tile icons & optional tile navigation  
- Clear Form on Stock-In/Out  
- History read-only banner, Print, signed qty  
- Product View action; Clear Filters  

### Low
- Copy variants (Run report vs Generate, Record vs Save Adjustment)  
- Grid empty whitespace density  

---

## Explicit non-findings (aligned enough)

- Sidebar **information architecture** (Catalog / Inventory / Reports / Administration) matches shell intent.  
- Admin-only items (**Adjustment**, **User Management**, **Delegations**) appear for Administrator in screenshots.  
- Inventory History has **no** edit/delete actions (correct).  
- Customer optional on Stock-Out is labeled.  
- StatusStrip shows user · role · date pattern.  

---

## Recommendation

**Do not implement until this document is approved.**  
After approval, fix in severity order (Critical → High → Medium), re-capture `UI/current-ui/`, and produce a short **re-audit delta** — still without inventing new design beyond `UI/update-ui` + approved `UI/` specs.

---

**End of analysis — waiting for approval.**
