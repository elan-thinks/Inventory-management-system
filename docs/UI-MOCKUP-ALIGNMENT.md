# UI mockup alignment

**Branch:** `ui/mockup-alignment` (from `version1.3`)  
**Mockups:** `UI/update-ui/UI-mock-updated/UI-mock-updated/*.html`  
**Theme:** **Light** (approved tokens in `UiTheme.cs` — matches HTML mockups)

## Goal

Make the running WinForms app match the HTML mockups as closely as practical, while keeping **every form openable in Visual Studio View Designer** (controls live in `*.Designer.cs`, not only runtime `BuildUi()`).

## Progress

| Screen | Mockup file | Status |
|--------|-------------|--------|
| Dashboard | `dashboard.html` | **In progress** — metric icons, QA icons, Qty +/−, mockup labels |
| Login | `login.html` | Pending |
| Products list | `product-list.html` | Pending |
| Product form | `product-form.html` | Pending |
| Categories | `category-management.html` | Pending |
| Suppliers | `supplier-management.html` | Pending |
| Customers | `customer-management.html` | Pending |
| Stock In | `stock-in.html` | Pending |
| Stock Out | `stock-out.html` | Pending |
| Adjustment | `inventory-adjustment.html` | Pending |
| History | `inventory-history.html` | Pending |
| Reports | `reports.html` | Pending |
| Users | `user-management.html` | Pending |
| Delegations | `delegation-management.html` | Pending |
| Shell (MainForm) | shared chrome in mockups | Pending (brand, header search optional) |

## Dashboard gap analysis (mockup vs previous app)

| Mockup | Before | After (this branch) |
|--------|--------|---------------------|
| Icons on metric captions | Text only | `PictureBox` on each tile (designer-visible) + Resources at runtime |
| "Total Inventory Quantity" | "Total Inventory Qty" | Full mockup label |
| Quick Actions with icons | Text only | Icons from Resources |
| Primary Stock-In + outlined others | Similar | Primary filled; secondary primary-colored text |
| Qty +40 / −6 colors | Plain numbers | + green / − red by type |
| Columns Date·Type·Product·Qty·User | Order varied | Fixed mockup order |
| View all > | Present | Kept |

## Designer rule

- Layout structure, labels, panels, buttons, grids → **`*.Designer.cs`**
- Data load, permissions, icon *images* from disk → **`*.cs` runtime only**
- Never hide the whole UI behind a runtime-only `BuildUi()` for screens already converted

## How to try

```powershell
git fetch origin
git checkout ui/mockup-alignment
# open src/NovaTechIMS.sln — right-click DashboardForm → View Designer
dotnet build src/NovaTechIMS.sln
dotnet run --project src/NovaTechIMS/NovaTechIMS.csproj
```

Log in as `admin` / `Admin@123` to see full Quick Actions (Adjustment + Users).
