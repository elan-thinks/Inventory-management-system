# 03 - Project Structure

**Document:** Visual Studio Solution & Folder Layout  
**Version:** 1.0  

---

## Decision

**Single Visual Studio solution containing one WinForms project.**

```
NovaTechIMS.sln
└── NovaTechIMS/
    ├── Program.cs
    ├── Forms/          (Login, Main, Products, Categories, Suppliers, Customers,
    │                    Inventory, Reports, Users, Delegations)
    ├── Models/         (+ Enums/)
    ├── Services/
    ├── Data/
    ├── Security/
    ├── Utilities/
    ├── Properties/
    └── NovaTechIMS.csproj
```

## Folder responsibilities

| Folder | May reference |
|--------|---------------|
| Forms | Services, Models |
| Models | Nothing else |
| Services | Data, Models, Security, Utilities |
| Data | Models |
| Security | System only |
| Utilities | Nothing |

## Naming

Forms end with `Form`; Services with `Service`; data classes with `Repository` (or DataAccess); enums in Models/Enums.

## Entry point

```csharp
ApplicationConfiguration.Initialize();
Application.Run(new LoginForm()); // Milestone 0 uses PlaceholderForm
```

## Connection string

In App.config / appsettings; read by DbConnectionFactory (Milestone 4).
