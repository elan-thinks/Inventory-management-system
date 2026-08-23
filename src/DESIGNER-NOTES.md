# WinForms View Designer — version1.3

## Status

Almost every screen already has:

- `SomethingForm.cs` (logic)
- `SomethingForm.Designer.cs` (`InitializeComponent` + controls)
- `SomethingForm.resx` (resources)

That is the structure Visual Studio needs for **View Designer**.

## Why the designer was empty / failed

1. **Constructor ran runtime code** (DB lookups, services, nav build).
2. **MainForm had no parameterless constructor** (designer requires one).
3. **Service fields** were created with `= new()` before the form finished loading.

## What we fixed

- `Utilities/DesignTime.cs` — detects design-time host
- Constructors return early when `DesignTime.IsActive` after `InitializeComponent()`
- Services are **lazy** (created only when used at runtime)
- `MainForm()` parameterless ctor for the designer

## How to open a form in the designer

1. `git pull origin version1.3`
2. Build the project (must succeed)
3. Solution Explorer → form `.cs` file → right-click → **View Designer**
   - Or open `.cs` and press **Shift+F7**

### Forms that should design well

| Form | Notes |
|------|--------|
| LoginForm | Standalone dialog |
| MainForm | Shell; design-time uses placeholder user |
| CategoryListForm / ProductListForm | Layout from Designer.cs |
| Other *ListForm / *EditForm / Stock* / Reports* | Already have Designer.cs |

### Still normal limitations

- Child forms use `FormBorderStyle.None` (look borderless in designer — OK).
- **Theme colors / icons** applied at runtime may differ slightly from design surface.
- **Grid columns / data** appear only when the app runs (bound at runtime).
- Report cards or dynamic nav buttons created in code may be incomplete on the design surface.

## If designer still fails

1. Close all designer tabs
2. **Build → Rebuild Solution**
3. Delete `bin` / `obj` under `src/NovaTechIMS`, rebuild
4. Open **LoginForm** first (simplest)
5. Check Error List for design-time exceptions

Do **not** edit only the `.cs` file expecting layout changes — layout lives in **`.Designer.cs`**. After drag-drop in the designer, Visual Studio updates that file.
