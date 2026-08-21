# 2. Design Tokens

Every value below is reproduced exactly from `product-requirements/07-Design-System.md`, made complete (badge tints, hover/pressed math, component dimensions) and expressed in a machine-readable form for direct use in a WinForms theme/constants file. These are also the literal values used in every HTML mockup's embedded stylesheet, so the mockups and this document never disagree.

## 2.1 Color Tokens

```json
{
  "color": {
    "primary":            "#1E4B8F",
    "primaryHover":       "#163A6E",
    "secondary":          "#3B7A8F",
    "background":         "#F4F6F8",
    "surface":            "#FFFFFF",
    "text":               "#1F2933",
    "textMuted":          "#6B7785",
    "success":            "#2E8B57",
    "successTint":        "#E4F3EA",
    "warning":            "#C77700",
    "warningTint":        "#FBEBD9",
    "error":              "#C0392B",
    "errorTint":          "#FBEAE8",
    "info":               "#2E6E9E",
    "infoTint":           "#E7F0F7",
    "border":             "#D7DCE1",
    "borderFocus":        "#1E4B8F",
    "disabledBackground": "#ECEEF1",
    "disabledText":       "#9AA5B1",
    "rowHover":           "#EAF1F8",
    "rowSelected":        "#D6E4F5",
    "sidebarBackground":  "#16324F",
    "sidebarText":        "#C7D3E0",
    "sidebarTextActive":  "#FFFFFF",
    "sidebarAccent":      "#5B9BD5"
  }
}
```

| Token | Hex | Usage |
|---|---|---|
| Primary | `#1E4B8F` | Primary buttons, active nav item accent, key headers, links |
| Primary Hover/Pressed | `#163A6E` | Button hover/pressed states |
| Secondary | `#3B7A8F` | Secondary accents, form-section eyebrow labels |
| Background | `#F4F6F8` | App/window background |
| Surface | `#FFFFFF` | Cards, panels, dialogs, grid background |
| Text (Primary) | `#1F2933` | Body text, field values |
| Text (Muted) | `#6B7785` | Captions, helper text, disabled labels |
| Success | `#2E8B57` | In Stock, success messages, positive confirmations |
| Success Tint | `#E4F3EA` | Success badge/banner fill |
| Warning | `#C77700` | Low Stock, warning banners |
| Warning Tint | `#FBEBD9` | Warning badge/banner fill (also the Low-Stock pill fill referenced in doc 07 §7.7) |
| Error | `#C0392B` | Out of Stock, validation errors, destructive actions |
| Error Tint | `#FBEAE8` | Error badge/banner fill |
| Info | `#2E6E9E` | Informational messages/tips (e.g. "New quantity will be: X") |
| Info Tint | `#E7F0F7` | Info banner fill |
| Border | `#D7DCE1` | Field borders, table gridlines, panel dividers |
| Border (Focus) | `#1E4B8F`, 2px | Focused input outline |
| Disabled Background | `#ECEEF1` | Disabled fields/buttons |
| Disabled Text | `#9AA5B1` | Disabled field/button text |
| Row Hover | `#EAF1F8` | DataGridView row hover tint |
| Row Selected | `#D6E4F5` | DataGridView selected row fill (+2px left accent bar in Primary) |
| Sidebar Background | `#16324F` | The one deliberately dark surface in the app — sidebar only, for wayfinding contrast against the light content area |
| Sidebar Text | `#C7D3E0` / `#FFFFFF` active | Nav item label |
| Sidebar Accent | `#5B9BD5` | Active nav item's left accent bar |

All text/background pairings meet WCAG AA for normal text (verified against both `#F4F6F8` and `#FFFFFF`).

## 2.2 Typography Tokens

```json
{
  "typography": {
    "fontFamily": "Segoe UI, Tahoma, Arial, sans-serif",
    "screenTitle":   { "size": 20, "weight": 600 },
    "sectionTitle":  { "size": 15, "weight": 600 },
    "body":          { "size": 12, "weight": 400 },
    "label":         { "size": 11, "weight": 400 },
    "buttonText":    { "size": 12, "weight": 600 },
    "tableHeader":   { "size": 11,  "weight": 600 },
    "tableCell":     { "size": 11.5,"weight": 400 },
    "metricNumber":  { "size": 24, "weight": 600 },
    "helperText":    { "size": 11, "weight": 400 },
    "errorText":     { "size": 11, "weight": 400 }
  }
}
```

Font family is Segoe UI everywhere — the Windows 10/11 default, falling back to Tahoma then Arial — guaranteeing native rendering with no embedded fonts. Minimum text size anywhere in the application is 11px (WCAG readability floor per `07-Design-System.md §7.9`).

## 2.3 Spacing Scale

```json
{ "spacing": { "1": 4, "2": 8, "3": 12, "4": 16, "6": 24, "8": 32 } }
```

| Value | Used for |
|---|---|
| 4px | Icon-to-text gaps inside badges/buttons |
| 8px | Label-to-input gap |
| 12px | Compact toolbar gaps, table cell horizontal padding |
| 16px | Between fields within a group; dialog body padding |
| 24px | Between field groups; between major page sections |
| 32px | Outer content padding on list/dashboard screens |

## 2.4 Radius Tokens

```json
{ "radius": { "input": 4, "button": 4, "card": 6, "dialog": 6, "badgePill": 11 } }
```

Deliberately modest — desktop software, not a mobile consumer app. Badge pills use a fully-rounded (11px on a ~22px-tall pill) shape since that is the one place a "pill" reads as a status indicator rather than decoration.

## 2.5 Border Tokens

| Token | Value |
|---|---|
| Standard border width | 1px, `#D7DCE1` |
| Focus border width | 2px, `#1E4B8F` |
| Error border width | 2px, `#C0392B` |
| Selected-row accent bar | 2–3px left border, `#1E4B8F` (rendered as `box-shadow: inset 3px 0 0` in the HTML mockups; a colored `Panel` strip or custom cell painting in WinForms) |

## 2.6 Shadow Policy

**No drop shadows anywhere in the application.** Depth is communicated only through the `Surface` (`#FFFFFF`) vs. `Background` (`#F4F6F8`) contrast and 1px borders — this is both the brief's explicit direction (`09 §10`: "No drop shadows") and the only depth technique that reproduces exactly in WinForms without custom painting. The one exception is the modal dialog backdrop overlay (`rgba(20,30,40,0.42)`), which is standard modal-dialog behavior in both web and WinForms (a semi-transparent owner-form overlay is achievable via a borderless topmost form or simply relies on the OS's native modal-dialog dimming, depending on the developer's chosen technique) and is not a "shadow" in the decorative sense.

## 2.7 Component Dimensions

| Component | Dimension |
|---|---|
| Sidebar width | 220px fixed |
| Header height | ~64px (14px vertical padding + 20px title line height + breadcrumb) |
| StatusStrip height | ~28px |
| Standard button height | ~34px (9px vertical padding + 12px text + border) |
| Standard input height | ~34px |
| Table row height | ~38px (9px vertical padding × 2 + 11.5px text + border) |
| Table header row height | ~36px |
| Modal dialog width (standard) | 560px |
| Modal dialog width (small / confirmation) | 440px |
| Modal dialog width (Product — most fields) | 620px |
| Minimum application window | 1024×768 |
| Icon sizes | 16px inline/table, 20px buttons, 18–24px sidebar nav, 28px+ dashboard tiles/dialog icons |

## 2.8 Icon Set

One coherent outline icon set: 1.75px stroke, round line caps/joins, 24×24 viewBox, rendered via `currentColor` so every icon inherits its container's color (status color, muted text color, etc.) rather than being hard-coded. Consistent with a Feather/Lucide-style outline aesthetic per `07-Design-System.md §7.8`. The full icon inventory used across the application (dashboard, products, categories, suppliers, customers, stock-in, stock-out, adjustment, history, reports, users, logout, search, edit, delete, view, save/check, cancel/close, filter, print, calendar, lock, chevrons, info, alert-triangle, check-circle, x-circle) is documented with usage context in `03-Component-Library.md §3.12`.
