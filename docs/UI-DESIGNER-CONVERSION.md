# UI Designer Conversion

**Branch:** `version1.3`  
**Goal:** Every user-facing Form opens in Visual Studio **View Designer** with real controls.

---

## Status

| Form | Status |
|------|--------|
| LoginForm, MainForm | Already Designer-based |
| CategoryListForm, CategoryEditForm | **Converted** |
| SupplierListForm, SupplierEditForm | **Converted** |
| CustomerListForm, CustomerEditForm | **Converted** |
| Products, Inventory, Users, Delegations, Dashboard, Reports | Pending |

---

## Pattern

1. `public partial class XForm : Form`
2. Controls in `XForm.Designer.cs` → `InitializeComponent()`
3. `XForm.resx` present
4. Constructor: `InitializeComponent()` → `ApplyRuntimeStyling()` → data/events
5. No DB/services in `InitializeComponent()`
6. Business logic unchanged (existing Services)

---

## Batch: Suppliers + Customers (this session)

**Converted:**
- SupplierListForm (.cs + .Designer.cs + .resx)
- SupplierEditForm (.cs + .Designer.cs + .resx)
- CustomerListForm (.cs + .Designer.cs + .resx)
- CustomerEditForm (.cs + .Designer.cs + .resx)

**Also fixed:** duplicate `btnSave.Click += BtnSave_Click` that existed in original Edit `BuildUi` methods (single subscription now).

**Not modified:** Services, repositories, database, authorization, tests.

---

## Local verification (Windows + VS)

```powershell
git pull origin version1.3
# Open src/NovaTechIMS.sln
# Build → Rebuild Solution
# Right-click SupplierListForm.cs / CustomerEditForm.cs → View Designer
# F5 → test Suppliers + Customers CRUD
dotnet build src/NovaTechIMS.sln
dotnet test src/NovaTechIMS.Tests/NovaTechIMS.Tests.csproj
```

Designer success = controls visible and selectable on the design surface (not an empty form).
