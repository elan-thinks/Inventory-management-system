# UI Designer Conversion

**Branch:** `version1.3`

## Status

| Form | Status |
|------|--------|
| LoginForm, MainForm | Already Designer-based |
| CategoryList/Edit | **Converted** |
| SupplierList/Edit | **Converted** |
| CustomerList/Edit | **Converted** |
| ProductList/Edit | **Converted** |
| StockInForm, StockOutForm | **Converted** |
| AdjustmentForm, InventoryHistoryForm | **Converted** |
| Users, Delegations, Dashboard, Reports | Pending |

## Pattern (Designer-safe)

1. `InitializeComponent()` uses **only BCL** types (`Font`, `Color`, sizes) — **no UiTheme**
2. `ApplyRuntimeStyling()` applies UiTheme at runtime
3. Lookups/data load never run in Designer
4. Services/DB unchanged

## Inventory batch

- Stock-In / Stock-Out: form panel + recent grid
- Adjustment: product, prev/new qty, reason
- History: filter toolbar + grid

## Verify

```powershell
git fetch origin
git reset --hard origin/version1.3
# Rebuild → View Designer on each Inventory form → F5
```
