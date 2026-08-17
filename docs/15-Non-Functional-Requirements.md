# 15. Non-Functional Requirements

| ID       | Category          | Requirement |
|----------|-------------------|-------------|
| NFR-001  | Usability         | A new user with basic computer skills shall be able to perform common tasks (login, search product, record stock-out) after a short demonstration. |
| NFR-002  | Usability         | All primary actions shall be reachable within a small number of clicks from the Dashboard. |
| NFR-003  | Usability         | Error and validation messages shall be in clear, non-technical language. |
| NFR-004  | Performance       | Product list (up to a few thousand records) shall load and display in under 3 seconds on typical student hardware. |
| NFR-005  | Performance       | Search and filter operations shall return results in under 2 seconds for the expected data volume. |
| NFR-006  | Reliability        | The application shall not crash on common user errors (empty fields, invalid numbers, etc.). |
| NFR-007  | Reliability        | Database failures shall be handled gracefully with a user-friendly message. |
| NFR-008  | Maintainability   | Code (when written) shall follow consistent naming and structure so that another student can understand it. |
| NFR-009  | Security          | Passwords shall be stored using a one-way hash, never plain text. |
| NFR-010  | Security          | Users can only access functions permitted by their role. |
| NFR-011  | Data Integrity    | Referential integrity and business rules shall prevent orphan records and negative stock. |
| NFR-012  | Data Integrity    | Inventory transactions are append-only. |
| NFR-013  | Availability      | The system is a local desktop application; availability depends on the host PC being operational. |
| NFR-014  | Compatibility     | The application shall run on Windows 10 and Windows 11. |
| NFR-015  | Error Recovery    | After a handled error the user shall be able to continue working without restarting the application. |
| NFR-016  | Scalability       | Designed for a single store with hundreds to low thousands of products and transactions — not enterprise scale. |
