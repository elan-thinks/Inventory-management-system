# 01 - Technical Design Overview

**Version:** 1.0 (+ database engine amendment)  

## Technology stack

| Layer | Choice |
|-------|--------|
| UI | C# Windows Forms, .NET 8 |
| Language | C# |
| Database engine | **PostgreSQL** |
| Data access | ADO.NET patterns via **Npgsql** (from Milestone 4) |
| Architecture | Simple layered |

Database engine was amended from SQL Server LocalDB to PostgreSQL because PostgreSQL is already available in the developer environment and fully supports approved requirements. See ADR-002 in `04-Database-Design.md` and `project-management/DATABASE-ENGINE-AMENDMENT.md`.

Functional requirements, Product/UX, and UI design are unchanged.
