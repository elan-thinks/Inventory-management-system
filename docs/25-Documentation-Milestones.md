# 25. Recommended Documentation Milestones

The following progressive milestones move from basic understanding to a fully validated requirements baseline. They are designed so that foundational work is never skipped.

| Milestone | Title                              | Purpose                                      | Documents to Create / Update                  | Requirements Covered                     | Dependencies          | Completion Criteria |
|-----------|------------------------------------|----------------------------------------------|-----------------------------------------------|------------------------------------------|-----------------------|---------------------|
| M1        | Project Understanding              | Establish shared understanding of the problem | 02-Project-Overview, 03-Business-Context     | Context, problem, solution               | None                  | Stakeholders agree on business scenario |
| M2        | Objectives & Scope                 | Define success and boundaries                | 04-Objectives, 05-Scope, 07-Assumptions-and-Constraints | OBJ-*, Scope, Assumptions, Constraints | M1                    | In-scope / out-of-scope signed off |
| M3        | Stakeholders & Roles               | Clarify who does what                        | 06-Stakeholders-and-Roles                    | Roles & permissions                      | M2                    | Role matrix approved |
| M4        | Functional Requirements            | Capture all “what the system shall do”       | 08-Functional-Requirements                   | All FR-*                                 | M3                    | Every major module has FR statements |
| M5        | Business Rules & Validation        | Make behaviour precise and enforceable       | 09-Business-Rules, 11-Validation-Requirements | BR-*, VAL-*                              | M4                    | Rules are testable and non-contradictory |
| M6        | Data Requirements                  | Define the information the system must hold  | 10-Data-Requirements                         | DR-*                                     | M4, M5                | All entities, attributes, relationships defined |
| M7        | Use Cases & Workflows              | Describe dynamic behaviour                   | 18-Use-Cases, 19-System-Workflows            | UC-*, WF-*                               | M4                    | Major user journeys documented |
| M8        | Error Handling, Audit, Reporting   | Complete behavioural picture                 | 12-Error-Handling, 13-Audit-and-History, 14-Reporting | ER-*, Audit, RPT-*                       | M4–M7                 | Error and report requirements clear |
| M9        | Non-Functional & WinForms          | Address quality and platform constraints     | 15-Non-Functional, 16-WinForms-Specific      | NFR-*, WinForms expectations             | M4                    | NFRs measurable; WinForms needs explicit |
| M10       | Traceability, Priority, MVP        | Ensure completeness and focus                | 17-CRUD-Matrix, 20-Traceability, 21-Priorities, 22-MVP | Matrix, prioritisation, MVP definition   | All previous          | MVP clearly bounded; traceability exists |
| M11       | Quality Review & Finalisation      | Independent check of the whole SRS           | 24-Requirement-Quality-Review, 01-Document-Control | Full audit                               | M10                   | Self-audit completed; version 1.0 declared |

**Notes**
- Each milestone should produce reviewable Markdown files.
- Later milestones may refine earlier documents (requirements evolution is normal).
- After M11 the SRS is considered a stable baseline for design and implementation.
