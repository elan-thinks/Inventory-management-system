# Designer notes — NovaTech IMS (`version1.3`)

## Hybrid UI

This project is **not** pure drag-and-drop and **not** pure code UI.

- **`.Designer.cs`** — layout you edit in **View Designer (Shift+F7)**
- **`.cs`** — behaviour: load data, save, filters, permissions, theme

Runtime will always differ slightly from the designer: live data, role-based menu, `UiTheme` polish.

## Design-time safety

`Utilities/DesignTime.cs` detects the Visual Studio designer host so constructors skip services and database work.

## MainForm sidebar

- Designer: sample nav buttons for visibility
- Runtime: `BuildNavigation()` clears and rebuilds from `AuthorizationService` / permissions

To change **labels or order of real menu items**, edit `MainForm.cs` (`BuildNavigation` / `AddNavItem`), not only the sample designer buttons.

## Preview one form alone

Temporarily in `Program.cs`:

```csharp
Application.Run(new NovaTechIMS.Forms.Categories.CategoryListForm());
```

Restore `Application.Run(new LoginForm());` when finished.

## Theme

Light tokens in `Utilities/UiTheme.cs` (background `#F4F6F8`, primary `#1E4B8F`).
