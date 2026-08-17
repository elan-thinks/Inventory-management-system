# Requirements Audit

## Project

**System:** Inventory Management System  
**Technology:** C# / .NET / Windows Forms  
**Repository:** Inventory-management-system  
**SRS Source:** Grok-generated SRS  
**Audit Status:** In Progress

---

# 1. Purpose

This document records the review of the initial System Requirements Specification.

The goal is to identify:

- missing requirements
- ambiguous requirements
- contradictions
- unnecessary complexity
- scope risks
- requirements requiring clarification
- requirements especially important for demonstrating C# and WinForms skills

The original Grok-generated SRS must not be silently modified. Every significant correction must be documented here before the requirements baseline is approved.

---

# 2. Current Verdict

**Status:** APPROVED WITH CORRECTIONS

The initial SRS provides a strong foundation but requires clarification and correction before becoming the official Version 1.0 requirements baseline.

---

# 3. Required Corrections

## RA-001 — Product Initial Quantity

**Status:** ⬜ Pending

### Problem

The Product entity contains `QuantityOnHand`, while inventory quantity is also changed through Stock-In, Stock-Out, and Inventory Adjustment.

This can create ambiguity about how a newly created product receives its initial quantity.

### Decision

A newly created product shall have:

`QuantityOnHand = 0`

Inventory shall be introduced through a Stock-In transaction or authorized Inventory Adjustment.

### Affected Documents

- `10-Data-Requirements.md`
- `09-Business-Rules.md`
- `08-Functional-Requirements.md`
- `19-System-Workflows.md`

### Verification

- [ ] Product creation does not add inventory
- [ ] New products start with zero quantity
- [ ] Stock-In increases quantity
- [ ] Stock-Out decreases quantity
- [ ] Adjustment follows its defined rules

---

## RA-002 — Product Supplier vs Transaction Supplier

**Status:** ⬜ Pending

### Problem

Products have a primary/default supplier while Stock-In transactions also record a supplier.

The distinction needs to be explicit.

### Decision

`Product.Supplier` represents the product's default/preferred supplier.

`StockIn.Supplier` represents the supplier from whom the particular stock shipment was actually received.

The Stock-In supplier may therefore differ from the product's default supplier.

### Affected Documents

- `10-Data-Requirements.md`
- `09-Business-Rules.md`
- `08-Functional-Requirements.md`

### Verification

- [ ] Product has a default supplier
- [ ] Stock-In records the actual supplier
- [ ] The two concepts are clearly distinguished

---

## RA-003 — Customer on Stock-Out

**Status:** ⬜ Pending

### Problem

The original SRS leaves customer requirements on Stock-Out somewhat ambiguous.

### Decision

Customer information is OPTIONAL for Stock-Out transactions.

A Stock-Out may occur without a registered customer.

### Affected Documents

- `08-Functional-Requirements.md`
- `09-Business-Rules.md`
- `10-Data-Requirements.md`
- `19-System-Workflows.md`

### Verification

- [ ] Stock-Out can be recorded without a customer
- [ ] Customer can be selected when applicable
- [ ] Inventory quantity is still updated correctly

---

## RA-004 — User Management

**Status:** ⬜ Pending

### Problem

Authentication and role-based access exist, but User Management is described as optional.

This creates inconsistency because users are required by the authentication and audit requirements.

### Decision

Basic User Management is part of MVP.

Administrators shall be able to:

- create users
- edit users
- deactivate users
- assign roles

Advanced account-management features remain out of scope.

### Affected Documents

- `08-Functional-Requirements.md`
- `10-Data-Requirements.md`
- `17-CRUD-Coverage-Matrix.md`
- `22-MVP-Definition.md`

### Verification

- [ ] Administrator can create users
- [ ] Administrator can edit users
- [ ] Administrator can deactivate users
- [ ] Administrator can assign roles
- [ ] Non-administrators cannot manage users

---

## RA-005 — Concrete Validation Limits

**Status:** ⬜ Pending

### Problem

Some validation requirements use vague wording such as "reasonable length."

### Decision

Concrete field length limits shall be defined in the requirements before implementation.

The exact limits will be reviewed and approved before the SRS is finalized.

### Affected Documents

- `10-Data-Requirements.md`
- `11-Validation-Requirements.md`

### Verification

- [ ] Required fields have defined maximum lengths
- [ ] Numeric fields have defined valid ranges
- [ ] Text fields have defined constraints
- [ ] Email/phone validation rules are explicit

---

## RA-006 — Reporting Scope

**Status:** ⬜ Pending

### Problem

Reporting requirements mention printing/exporting, potentially expanding the project unnecessarily.

### Decision

MVP reporting shall support:

- viewing reports
- printing reports

Export functionality is optional/future scope unless specifically approved later.

### Affected Documents

- `14-Reporting-Requirements.md`
- `22-MVP-Definition.md`
- `23-Future-Enhancements.md`

### Verification

- [ ] Required reports can be viewed
- [ ] Required reports can be printed
- [ ] Export is not required for MVP

---

## RA-007 — Traceability Completeness

**Status:** ⬜ Pending

### Problem

The current traceability document contains high-level mappings such as `FR-PROD-*` rather than a complete requirement-level traceability matrix.

### Decision

Before SRS v1.0 approval, the traceability matrix shall map important requirements to their related:

- objectives
- business rules
- use cases
- data requirements

### Affected Documents

- `20-Requirement-Traceability.md`

### Verification

- [ ] Major functional requirements are traceable
- [ ] Major business rules are traceable
- [ ] Major use cases are traceable
- [ ] Important data requirements are traceable
- [ ] No major requirement is orphaned

---

# 4. General Quality Checklist

## Completeness

- [ ] All core business functions defined
- [ ] All major entities defined
- [ ] CRUD requirements defined
- [ ] Inventory workflows defined
- [ ] Authentication defined
- [ ] Authorization defined
- [ ] Validation defined
- [ ] Error handling defined
- [ ] Reporting defined

## Consistency

- [ ] Product quantity rules are consistent
- [ ] Supplier relationships are consistent
- [ ] Customer requirements are consistent
- [ ] User-management requirements are consistent
- [ ] Delete rules are consistent
- [ ] Stock status rules are consistent

## C# / WinForms Learning Value

- [ ] Multiple forms required
- [ ] CRUD operations required
- [ ] DataGridView usage required
- [ ] Form events required
- [ ] Input validation required
- [ ] MessageBox/error handling required
- [ ] Classes/models required
- [ ] Database interaction required
- [ ] Search/filter functionality required
- [ ] Navigation between forms required

## Scope Control

- [ ] No unnecessary enterprise architecture
- [ ] No cloud dependency
- [ ] No mobile application
- [ ] No AI requirement
- [ ] No barcode requirement for MVP
- [ ] No advanced analytics requirement
- [ ] MVP remains implementable by a university student

---

# 5. Final Approval

The SRS may only be declared:

**SRS v1.0 — APPROVED**

when every blocking correction has been resolved and verified.

Current status:

**🟡 APPROVED WITH CORRECTIONS**