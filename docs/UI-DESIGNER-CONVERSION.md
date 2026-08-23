# UI Designer Conversion

**Branch:** `version1.3`

## Status

| Form | Status |
|------|--------|
| LoginForm, MainForm | Already Designer-based |
| CategoryListForm, CategoryEditForm | **Converted** |
| SupplierListForm, SupplierEditForm | **Converted** |
| CustomerListForm, CustomerEditForm | **Converted** |
| ProductListForm, ProductEditForm | **Converted** |
| Inventory, Users, Delegations, Dashboard, Reports | Pending |

## Pattern

1. `public partial class XForm : Form`
2. Controls in `XForm.Designer.cs` → `InitializeComponent()`
3. `XForm.resx` present
4. Constructor: `InitializeComponent()` → `ApplyRuntimeStyling()` → lookups/data
5. No DB/services in `InitializeComponent()`
6. Business logic unchanged

## Products batch

- ProductListForm: toolbar filters (search, category, supplier, stock, status) + grid
- ProductEditForm: all fields; qty display-only; lookups filled at runtime
- Removed duplicate `btnSave.Click` subscription
- Services/DB not modified

## Verify

```powershell
git pull origin version1.3
# Rebuild → View Designer on Product forms → F5 CRUD
```
