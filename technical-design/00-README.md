# Technical Design Package — NovaTech Electronics Inventory Management System

**Version:** Technical Design v1.0 (engine amendment v1.1 for database)  
**Status:** APPROVED  
**Date:** August 2026  

Implementation technology for the database is **PostgreSQL** (amended from SQL Server LocalDB). See `project-management/DATABASE-ENGINE-AMENDMENT.md` and `technical-design/04-Database-Design.md` (ADR-002).

- Layered WinForms architecture (Forms → Services → Data Access → Database)
- PostgreSQL for relational integrity (amended from LocalDB — see project-management/DATABASE-ENGINE-AMENDMENT.md).
- ADO.NET-style access via **Npgsql** (Milestone 4+; not implemented yet)
- No Entity Framework; educational transparency of SQL

Documents 00–17 under `technical-design/` remain the blueprint. Milestone 3 scripts live in `database/`.
