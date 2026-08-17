# 7. Design System

A desktop-realistic design system for a professional electronics-retail inventory tool. Every value here is chosen to be reproducible in standard WinForms (solid fills, simple borders, system fonts — no gradients or shadow effects).

## 7.1 Color System

| Token | Hex | Usage |
|---|---|---|
| Primary | `#1E4B8F` | Primary buttons, active nav item, key headers, links |
| Primary Hover/Pressed | `#163A6E` | Button hover/pressed states |
| Secondary | `#3B7A8F` | Secondary accents, secondary buttons' text/border |
| Background | `#F4F6F8` | App/window background |
| Surface | `#FFFFFF` | Cards, panels, dialogs, grid background |
| Text (Primary) | `#1F2933` | Body text, field values |
| Text (Muted) | `#6B7785` | Captions, helper text, disabled labels |
| Success | `#2E8B57` | In Stock, success messages, positive confirmations |
| Warning | `#C77700` | Low Stock, warning banners |
| Error | `#C0392B` | Out of Stock, validation errors, destructive actions |
| Info | `#2E6E9E` | Informational messages/tips |
| Border | `#D7DCE1` | Field borders, table gridlines, panel dividers |
| Border (Focus) | `#1E4B8F` | Focused input outline |
| Disabled Background | `#ECEEF1` | Disabled fields/buttons |
| Disabled Text | `#9AA5B1` | Disabled field/button text |
| Row Hover | `#EAF1F8` | DataGridView row hover tint |
| Row Selected | `#D6E4F5` | DataGridView selected row |

Rationale: a controlled, low-saturation blue/teal professional palette (electronics-retail-appropriate, not consumer-flashy), with clearly distinct, colorblind-considerate status colors always paired with icon + label (per 6.3) so the palette choice never becomes an accessibility gap.

## 7.2 Typography

| Token | Font | Size | Weight | Usage |
|---|---|---|---|---|
| Font Family | Segoe UI (Windows default; falls back to Tahoma/Arial) | — | — | All UI text — guarantees native rendering on Windows 10/11 without embedding |
| H1 / Screen Title | Segoe UI | 20px (Semibold) | 600 | Screen headers ("Products," "Dashboard") |
| H2 / Section Title | Segoe UI | 15px (Semibold) | 600 | Form section headers, dialog titles |
| Body | Segoe UI | 12px (Regular) | 400 | Field values, general text |
| Label | Segoe UI | 11px (Regular) | 400 | Field captions, above/beside inputs |
| Button Text | Segoe UI | 12px (Semibold) | 600 | All button labels |
| Table Header | Segoe UI | 11px (Semibold) | 600 | DataGridView column headers |
| Table Cell | Segoe UI | 11.5px (Regular) | 400 | DataGridView row content |
| Metric / Tile Number | Segoe UI | 24px (Semibold) | 600 | Dashboard summary numbers |

## 7.3 Spacing Scale

`4 / 8 / 12 / 16 / 24 / 32` px. Used consistently: 8px between a label and its input; 16px between fields within a group; 24px between field groups; 32px around dialog/panel edges (content padding).

## 7.4 Borders

- **Border radius:** 4px on inputs and buttons, 6px on cards/panels/dialogs. Deliberately modest — not the heavily rounded style of consumer mobile apps.
- **Border thickness:** 1px standard for inputs, panels, and table gridlines; 2px only for the focus outline on the active input.
- **Input borders:** `#D7DCE1` default, `#1E4B8F` on focus, `#C0392B` on validation error.
- **Card/panel borders:** 1px `#D7DCE1`, `#FFFFFF` fill, no drop shadow (flat, WinForms-realistic).

## 7.5 Buttons

| Variant | Fill | Text | Border | Usage |
|---|---|---|---|---|
| Primary | `#1E4B8F` | White | none | Save, Login, Generate, primary confirming actions |
| Secondary | `#FFFFFF` | `#1E4B8F` | 1px `#1E4B8F` | Cancel, Back, secondary actions |
| Danger | `#C0392B` | White | none | Delete, Deactivate (confirming button only) |
| Ghost/Text | transparent | `#1E4B8F` | none | Clear Filters, low-emphasis links, "Mark Inactive" inline actions |

**States:** Normal (as above) · Hover (fill darkens ~12%, e.g. Primary Hover `#163A6E`) · Pressed (fill darkens further, subtle inset feel via slightly darker fill — no shadow tricks) · Disabled (Disabled Background/Disabled Text tokens, no hover response).

## 7.6 Inputs

| State | Border | Fill | Notes |
|---|---|---|---|
| Normal | `#D7DCE1` | `#FFFFFF` | |
| Focus | `#1E4B8F`, 2px | `#FFFFFF` | Clear, visible keyboard focus ring |
| Error | `#C0392B`, 2px | `#FFFFFF` | Paired with inline message below |
| Disabled | `#D7DCE1` | `#ECEEF1` | Text uses Disabled Text token; used for QuantityOnHand, Username-on-Edit, etc. |

## 7.7 Tables (DataGridView)

- **Header:** `#F4F6F8` fill, `#1F2933` text, Table Header type, 1px bottom border `#D7DCE1`.
- **Row:** `#FFFFFF` (odd) / `#FAFBFC` (even, subtle zebra), `#1F2933` text.
- **Row hover:** `#EAF1F8`.
- **Row selected:** `#D6E4F5`, with a 2px left accent bar in Primary color for extra clarity beyond color alone.
- **Status column:** rendered as a pill badge (icon + label + status color background at ~15% opacity equivalent using a light tint fill, e.g. Low Stock badge = `#FBEBD9` fill with `#C77700` text/icon).

## 7.8 Icons

- **Style:** single coherent outline icon set (e.g., a library consistent with a "Feather/Lucide"-style outline aesthetic) — simple, 1.5–2px stroke, no filled/flat-color icon mixing.
- **Usage:** navigation items, status badges (check/warning/cross per 6.3), action-column icons (eye/pencil/trash), dashboard tile icons (kept minimal — icons support the label, they don't replace it).
- **Sizing:** 16px for inline/table icons, 20px for buttons, 24px for sidebar navigation, 28–32px for dashboard tile icons.

## 7.9 Accessibility

- **Keyboard navigation:** Full Tab-order support on every form, following visual left-to-right, top-to-bottom field order; Enter submits the primary action on single-purpose dialogs (e.g., Login); Esc cancels/closes modal dialogs.
- **Visible focus:** every interactive control shows the Focus border/outline defined in 7.6 — never suppressed.
- **Labels:** every input has a persistent, visible label (not placeholder-only text) so context isn't lost once a field is filled.
- **Contrast:** all text/background pairings in 7.1 meet at least WCAG AA contrast for normal text (verified against `#F4F6F8`/`#FFFFFF` backgrounds).
- **Color independence:** status, validation, and feedback states are always Icon + Label + Color together (per 6.3, 6.9) — never color alone.
- **Keyboard shortcuts:** Alt-mnemonics on menu items (e.g., Alt+P for Products) and common actions (Ctrl+S to Save on open forms, Ctrl+N for "Add New" on list screens) as a WinForms-native, low-effort accessibility and efficiency win.
- **Typography readability:** minimum 11px body/table text, Segoe UI at native ClearType rendering, no condensed or decorative fonts.

## 7.10 Desktop Behavior Summary

(See document 02 §2.3 for full window-strategy detail.) Design tokens above apply uniformly across the main window, all list screens, all modal Add/Edit dialogs, the Report Viewer, and confirmation dialogs — there is exactly one visual language, not a separate "dialog theme."
