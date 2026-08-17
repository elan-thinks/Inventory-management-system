# Inventory Management System — System Requirements Specification (SRS)

**Project**: Inventory Management System  
**Repository**: https://github.com/elan-thinks/Inventory-management-system  
**Target Technology**: C# · .NET · Windows Forms (WinForms) · Relational Database  
**Document Type**: Master System Requirements Specification  
**Audience**: University Software Engineering / Windows Programming students and instructors  
**Status**: Complete (Requirements Phase Only — No Implementation Code)

---

## Purpose of This Repository

This repository contains the **complete System Requirements Specification** for a university-level Inventory Management System.  

It defines **what** the system must do — not how it will be coded.

No C# source code, SQL scripts, WinForms designer code, or UI designs are included.

The documentation is intentionally structured so that a student can:

1. Understand the business problem
2. Understand the complete set of functional and non-functional requirements
3. Implement a clean, well-structured WinForms application that demonstrates:
   - C# fundamentals and OOP
   - Classes, properties, methods, collections, encapsulation
   - Exception handling and input validation
   - Event-driven programming with WinForms
   - Full CRUD operations
   - Database interaction
   - Searching, filtering, and basic reporting

---

## Document Structure

All requirements documents live inside the `docs/` folder.

| File | Content |
|------|---------|
| `01-Document-Control.md` | Version history, authors, status |
| `02-Project-Overview.md` | Title, purpose, business context, problem & solution |
| `03-Business-Context.md` | Detailed business scenario |
| `04-Objectives.md` | Primary and secondary objectives |
| `05-Scope.md` | In-scope and out-of-scope items |
| `06-Stakeholders-and-Roles.md` | Stakeholders + User roles & permissions |
| `07-Assumptions-and-Constraints.md` | Project assumptions and constraints |
| `08-Functional-Requirements.md` | Complete functional requirements (all modules) |
| `09-Business-Rules.md` | Business rule catalogue |
| `10-Data-Requirements.md` | Logical data model / entities |
| `11-Validation-Requirements.md` | Input validation rules |
| `12-Error-Handling-Requirements.md` | Expected system behaviour on errors |
| `13-Audit-and-History.md` | Audit / transaction history requirements |
| `14-Reporting-Requirements.md` | Basic reports |
| `15-Non-Functional-Requirements.md` | Usability, performance, security, etc. |
| `16-WinForms-Specific-Requirements.md` | Forms, controls, event-driven expectations |
| `17-CRUD-Coverage-Matrix.md` | CRUD matrix for all entities |
| `18-Use-Cases.md` | Complete use-case catalogue |
| `19-System-Workflows.md` | Major business workflows |
| `20-Requirement-Traceability.md` | Traceability structure |
| `21-Requirement-Priorities.md` | MoSCoW prioritisation |
| `22-MVP-Definition.md` | Minimum Viable Product definition |
| `23-Future-Enhancements.md` | Intentionally excluded future features |
| `24-Requirement-Quality-Review.md` | Self-audit of the SRS |
| `25-Documentation-Milestones.md` | Recommended progressive documentation plan |

---

## How to Use This Documentation

1. Start with **Project Overview** and **Business Context**.
2. Read **Objectives** and **Scope** to understand boundaries.
3. Study **Functional Requirements**, **Business Rules**, and **Data Requirements**.
4. Use **Use Cases** and **Workflows** to understand behaviour.
5. Refer to **Validation**, **Error Handling**, and **WinForms** sections when implementing.
6. Follow the **MVP Definition** for the first complete version.

---

## Important Boundaries (Repeated for Clarity)

- **No application code** is provided or should be expected in this phase.
- The system is intentionally scoped for a **university Windows Programming / C# course**.
- Enterprise patterns (microservices, CQRS, cloud, ML, etc.) are explicitly out of scope.
- All requirements are written to be **testable** and **implementable**.

---

## Next Steps After Requirements

After this SRS is approved:

1. Database design (logical → physical)
2. Class / object design
3. UI/UX and screen design (WinForms)
4. Implementation
5. Testing against the requirements in this SRS

---

**Prepared as a complete, implementation-ready requirements baseline.**
