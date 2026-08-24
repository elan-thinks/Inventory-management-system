# UI Designer Conversion — COMPLETE

**Branch:** `version1.3`  
**Theme:** Light mode (`UiTheme`)

## Status

| Form | Designer layout |
|------|-----------------|
| LoginForm | Yes |
| MainForm (shell + sample nav) | Yes |
| DashboardForm | Yes |
| Product / Category / Supplier / Customer list + edit | Yes |
| StockIn / StockOut / Adjustment / InventoryHistory | Yes |
| UserList / UserEdit | Yes |
| DelegationList / DelegationEdit | Yes |
| ReportsHubForm | Yes (sample cards; full cards at runtime) |

## Pattern (Designer-safe)

1. `InitializeComponent()` uses **BCL only** (`System.Drawing` / `System.Windows.Forms`) — avoid `UiTheme` in Designer files where it breaks the host.
2. `ApplyRuntimeStyling()` applies light theme, grid theme, and button styles at **runtime**.
3. Services / DB are **not** constructed at design time (`DesignTime.IsActive` guards).
4. Sample grid columns / combo items help View Designer; runtime clears and binds real data.
5. MainForm **navigation** is rebuilt at runtime from permissions (`BuildNavigation`); designer shows sample items only.

## Editing safely

| Do | Don’t |
|----|--------|
| Move / resize / restyle | Rename controls used in code-behind |
| Add decorative labels | Delete `grid`, `btnSave`, `cboProduct`, etc. |
| Rebuild after edits | Expect Designer to show live DB data |

## Verify

```powershell
git pull origin version1.3
cd src
dotnet build NovaTechIMS.sln
# Visual Studio: open a Form → Shift+F7
# F5 → full app with login
```

## Related

- `src/DESIGNER-NOTES.md`
- Root `README.md` — hybrid UI explanation
