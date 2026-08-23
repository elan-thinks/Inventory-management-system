# UI Designer Conversion

**Branch:** `version1.3`

## Status

| Form | Status |
|------|--------|
| LoginForm, MainForm | Already Designer-based |
| Category / Supplier / Customer / Product | **Converted** |
| Stock-In / Stock-Out / Adjustment / History | **Converted** |
| UserListForm, UserEditForm | **Converted** |
| DelegationList/Edit | In progress |
| Dashboard, Reports | In progress |

## Pattern (Designer-safe)

1. `InitializeComponent()` — **BCL only** (no `UiTheme`)
2. `ApplyRuntimeStyling()` — theme at runtime
3. Services/DB never run in Designer

## Verify Users

```powershell
git fetch origin
git reset --hard origin/version1.3
# Rebuild → View Designer on UserListForm / UserEditForm → F5
```
