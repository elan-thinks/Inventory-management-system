# 03 — Project Structure

**Document:** Visual Studio Solution & Folder Layout  
**Version:** 1.0  

---

## 1. Decision

**Single Visual Studio solution containing one WinForms project.**

Rationale:
- University projects benefit from simplicity.
- Students can still organise code into folders that mirror layers.
- Avoids the complexity of multiple class-library projects, project references, and interface assemblies that add little educational value here.

---

## 2. Solution Layout

```
NovaTechIMS.sln
│
└── NovaTechIMS/                          ← WinForms project (.NET Framework or .NET 6/8 Windows)
    │
    ├── Program.cs                        ← Application entry point
    │
    ├── Forms/                            ← All WinForms
    │   ├── LoginForm.cs
    │   ├── MainForm.cs                   ← Shell / navigation host
    │   ├── DashboardForm.cs              ← or UserControl hosted in MainForm
    │   ├── Products/
    │   │   ├── ProductListForm.cs
    │   │   ├── ProductDetailForm.cs
    │   │   └── ProductEditForm.cs
    │   ├── Categories/
    │   ├── Suppliers/
    │   ├── Customers/
    │   ├── Inventory/
    │   │   ├── StockInForm.cs
    │   │   ├── StockOutForm.cs
    │   │   ├── InventoryAdjustmentForm.cs
    │   │   └── TransactionHistoryForm.cs
    │   ├── Reports/
    │   │   ├── ReportsHubForm.cs
    │   │   └── ReportViewerForm.cs
    │   ├── Users/
    │   │   ├── UserListForm.cs
    │   │   └── UserEditForm.cs
    │   └── Delegations/
    │       └── DelegationManagementForm.cs   ← SCR-020
    │
    ├── Models/                           ← Plain C# domain classes + enums
    │   ├── User.cs
    │   ├── Category.cs
    │   ├── Supplier.cs
    │   ├── Customer.cs
    │   ├── Product.cs
    │   ├── InventoryTransaction.cs
    │   ├── Delegation.cs
    │   ├── Enums/
    │   │   ├── UserRole.cs
    │   │   ├── TransactionType.cs
    │   │   ├── StockStatus.cs
    │   │   ├── DelegationStatus.cs
    │   │   └── DelegatableResponsibility.cs
    │   └── ...
    │
    ├── Services/                         ← Business logic
    │   ├── AuthService.cs
    │   ├── AuthorizationService.cs
    │   ├── ProductService.cs
    │   ├── CategoryService.cs
    │   ├── SupplierService.cs
    │   ├── CustomerService.cs
    │   ├── StockService.cs               ← Stock-In, Stock-Out, Adjustment
    │   ├── DelegationService.cs
    │   ├── ReportService.cs
    │   ├── UserService.cs
    │   └── DashboardService.cs
    │
    ├── Data/                             ← ADO.NET data access
    │   ├── DbConnectionFactory.cs
    │   ├── ProductRepository.cs          ← or *DataAccess.cs naming
    │   ├── CategoryRepository.cs
    │   ├── SupplierRepository.cs
    │   ├── CustomerRepository.cs
    │   ├── InventoryTransactionRepository.cs
    │   ├── UserRepository.cs
    │   ├── DelegationRepository.cs
    │   └── ...
    │
    ├── Security/                         ← Password hashing helpers
    │   └── PasswordHasher.cs
    │
    ├── Utilities/                        ← Shared helpers
    │   ├── ValidationHelper.cs
    │   ├── DateHelper.cs
    │   └── ExceptionTypes.cs             ← custom exceptions
    │
    ├── Properties/
    │   └── Resources, Settings, etc.
    │
    └── App.config (or appsettings)       ← connection string
```

---

## 3. Folder Responsibilities

| Folder | Responsibility | May reference |
|--------|----------------|---------------|
| Forms | UI only | Services, Models |
| Models | Domain data shapes + enums | Nothing else |
| Services | Business rules, orchestration, authorization | Data, Models, Security, Utilities |
| Data | SQL execution & mapping | Models |
| Security | Hashing / verification | Nothing (or System.Security) |
| Utilities | Pure helpers, custom exceptions | Nothing |

---

## 4. Naming Conventions (Project Level)

- Forms end with `Form` (LoginForm, ProductListForm).
- Services end with `Service`.
- Data-access classes end with `Repository` (or `DataAccess` if preferred by the instructor).
- Enums live in `Models/Enums`.
- Custom exceptions live in `Utilities` or a dedicated `Exceptions` folder.

---

## 5. Why Not Multiple Projects?

A multi-project solution (UI / Business / Data / Models) is perfectly valid in industry.  
For this university assignment it adds:

- project-reference management,
- build-order concerns,
- extra ceremony that does not teach additional C# or WinForms concepts.

The folder structure above already enforces the same dependency direction.  
If an instructor later requires multiple projects, the folders map 1:1 onto projects.

---

## 6. Entry Point

`Program.cs` simply:

```csharp
ApplicationConfiguration.Initialize();
Application.Run(new LoginForm());
```

After successful login, LoginForm opens MainForm (or Application.Run(new MainForm())) and closes itself.

---

## 7. Configuration

Connection string lives in `App.config` (or `appsettings.json` for modern .NET):

```xml
<connectionStrings>
  <add name="NovaTechIMS"
       connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NovaTechIMS.mdf;Integrated Security=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

`DbConnectionFactory` reads this string once.

---

## 8. Architecture Decision Record

**ADR — Project Structure**

- **Context:** Need a structure that supports layered design without overwhelming a student.
- **Decision:** Single WinForms project + logical folders.
- **Consequences:** Fast to create, easy to navigate, still teaches separation of concerns.
