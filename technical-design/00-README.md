# Technical Design Package — NovaTech Electronics Inventory Management System

**Version:** Technical Design v1.0 (database engine amendment v1.1)  
**Status:** APPROVED — implemented on branch `version1.3`  
**Date:** August 2026

## Stack (as implemented)

| Layer | Technology |
|-------|------------|
| UI | Windows Forms · .NET 8 (`net8.0-windows`) |
| Language | C# |
| Database | **PostgreSQL** |
| Data access | **Npgsql** (ADO.NET-style; no Entity Framework) |
| Tests | xUnit (`NovaTechIMS.Tests`) |

See `project-management/DATABASE-ENGINE-AMENDMENT.md` and `technical-design/04-Database-Design.md` (ADR-002).

## Architecture

- Layered: **Forms → Services → Data Access → PostgreSQL**
- Hybrid WinForms: designer layouts + code-behind behaviour
- Educational transparency of SQL (no EF)

## Documents

Documents **00–17** under `technical-design/` remain the design blueprint.  
Milestone gates and source: `src/`.  
Schema / seed scripts: `database/`.

## Implementation status

Milestones **0–19** are implemented on **`version1.3`**.  
Automated tests: see `docs/HOW-TO-RUN-TESTS.md`.
