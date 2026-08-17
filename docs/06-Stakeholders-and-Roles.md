# 6. Stakeholders and User Roles

## 6.1 Stakeholders

| Stakeholder              | Who They Are                          | Responsibilities                              | Needs from System                                      | Information They Provide                     | Information They Receive                     |
|--------------------------|---------------------------------------|-----------------------------------------------|--------------------------------------------------------|----------------------------------------------|----------------------------------------------|
| Store Owner / Manager    | Business owner                        | Overall business decisions, stock policy      | Accurate stock levels, low-stock alerts, reports       | Pricing strategy, minimum stock levels       | Dashboard metrics, all reports               |
| Inventory / Store Staff  | Sales assistants, stock clerks       | Daily stock movements, product maintenance    | Fast recording of stock-in/out, product search         | Transaction details, new product info        | Current stock, transaction confirmation      |
| System Administrator     | Usually the same as Owner             | User accounts, system configuration           | Ability to manage users and view all data              | User credentials, system settings            | Full system access                           |
| Course Instructor        | University assessor                   | Evaluate student work                         | Clear requirements that map to C# / WinForms skills    | Assessment criteria                          | Implemented system + documentation           |
| Student Developer        | The person building the system        | Design & implement according to SRS           | Unambiguous, testable requirements                     | —                                            | This entire SRS                              |

## 6.2 User Roles

Only two roles are defined to keep the system appropriate for a university project.

### 6.2.1 Administrator

**Permissions**
- Full access to all modules
- Manage products, categories, suppliers, customers
- Perform stock-in, stock-out, and inventory adjustments
- View all reports and dashboard
- Manage user accounts (create/edit/deactivate users) — optional but recommended
- View full transaction history and audit information

**Accessible Functions**
- Everything in the system

**Restrictions**
- None (within the scope of the application)

**Responsibilities**
- Maintain master data accuracy
- Set minimum stock levels
- Review reports and take purchasing decisions
- Manage staff access

### 6.2.2 Inventory / Store Staff

**Permissions**
- View products, categories, suppliers, customers
- Create and view stock-in and stock-out transactions
- Search and filter inventory
- View dashboard (limited metrics if desired)
- View basic reports (Current Inventory, Low Stock, Transaction History)

**Accessible Functions**
- Product viewing & search
- Stock-In
- Stock-Out
- Dashboard
- Selected reports

**Restrictions**
- Cannot delete products, categories, suppliers, or customers
- Cannot perform inventory adjustments (or only with restricted rights)
- Cannot manage user accounts
- Cannot change system-critical settings

**Responsibilities**
- Accurately record goods received and goods sold
- Keep product information up to date where permitted
- Alert manager when stock is low

### 6.2.3 Role Assignment Rule

- A user belongs to exactly one role.
- Role is assigned at user creation and can be changed only by an Administrator.
- The system shall enforce role-based visibility of menu items and buttons.
