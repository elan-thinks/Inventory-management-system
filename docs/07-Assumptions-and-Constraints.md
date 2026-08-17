# 7. Assumptions and Constraints

## 7.1 Assumptions

| ID     | Assumption |
|--------|------------|
| A-001  | The system will be used on a single Windows PC or a small number of PCs sharing a local database. |
| A-002  | Users are literate in basic computer use and will receive short training. |
| A-003  | All products are physical goods that can be counted as whole units (no fractional stock). |
| A-004  | Prices are in a single currency. |
| A-005  | The business has a limited number of suppliers and customers (hundreds, not tens of thousands). |
| A-006  | Network connectivity is reliable when a shared database is used; offline mode is not required. |
| A-007  | The student has access to a suitable development environment (Visual Studio, .NET, database engine). |
| A-008  | User authentication is sufficient with username + password stored securely in the database (hashed). |
| A-009  | One product is supplied by one primary supplier for simplicity (many-to-many can be a future enhancement). |
| A-010  | Stock quantity is always an integer ≥ 0. |

## 7.2 Constraints

| ID     | Constraint |
|--------|------------|
| C-001  | The application must be a Windows Forms desktop application written in C#. |
| C-002  | A relational database must be used for persistence. |
| C-003  | The solution must be implementable by a university student within a typical course timeframe. |
| C-004  | No external paid services, cloud platforms, or third-party commercial libraries beyond standard .NET are required. |
| C-005  | The UI must use standard WinForms controls (no WPF, no web technologies). |
| C-006  | The system must remain understandable without enterprise architecture patterns. |
| C-007  | All monetary values and quantities must be validated to prevent negative or invalid data. |
| C-008  | Destructive actions (delete) must require user confirmation. |
| C-009  | The final deliverable of the course is a working application + documentation; this SRS is the requirements baseline only. |
