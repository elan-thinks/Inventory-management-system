# Requirements Audit

## Project

**System:** Inventory Management System  
**Technology:** C# / .NET / Windows Forms  
**Repository:** Inventory-management-system  
**SRS Source:** Grok-generated SRS  
**Audit Status:** Complete

---

# 1. Purpose

This document records the review and correction of the initial System Requirements Specification.

The audit identified ambiguities and scope issues and converted them into explicit requirements decisions before the SRS baseline is used for Product/UI/UX design.

---

# 2. Final Verdict

**Status: SRS v1.0 — APPROVED WITH FINAL TRACEABILITY GATE**

The initial Grok-generated SRS was strong but required seven corrections. All seven have been incorporated into the affected documents.

---

# 3. Resolved Corrections

## RA-001 — Product Initial Quantity
**Status:** ✅ Resolved

A newly created Product starts with `QuantityOnHand = 0`. Product creation does not create an inventory movement. Stock enters through Stock-In or authorized Adjustment.

**Affected:** 08, 09, 10, 19, 22

## RA-002 — Product Supplier vs Transaction Supplier
**Status:** ✅ Resolved

Product.SupplierID represents the default/preferred supplier. Stock-In SupplierID represents the actual supplier for that receipt and may differ.

**Affected:** 08, 09, 10, 14, 19

## RA-003 — Customer on Stock-Out
**Status:** ✅ Resolved

CustomerID is optional on Stock-Out transactions.

**Affected:** 08, 09, 10, 14, 19

## RA-004 — User Management
**Status:** ✅ Resolved

Basic User Management is part of MVP. Administrators can create, edit, deactivate, and assign roles. An Administrator cannot deactivate their own current account.

**Affected:** 08, 10, 17, 19, 22

## RA-005 — Concrete Validation Limits
**Status:** ✅ Resolved

Concrete maximum lengths and numeric/date constraints are defined in the Validation Requirements document.

**Affected:** 10, 11

## RA-006 — Reporting Scope
**Status:** ✅ Resolved

MVP reports are viewable and printable. PDF/Excel/CSV export is deferred to future enhancements.

**Affected:** 14, 22, 23

## RA-007 — Traceability Completeness
**Status:** ✅ Core Matrix Resolved

The traceability document now maps core objectives to concrete functional requirements, business rules, use cases, and data requirements. The final use-case-ID consistency check remains the final baseline gate.

**Affected:** 20, 24

---

# 4. General Quality Checklist

## Completeness
- [x] Core business functions defined
- [x] Major entities defined
- [x] CRUD requirements defined
- [x] Inventory workflows defined
- [x] Authentication defined
- [x] Authorization defined
- [x] Validation defined
- [x] Error handling defined
- [x] Reporting defined

## Consistency
- [x] Product quantity rules are consistent
- [x] Supplier relationships are consistent
- [x] Customer requirements are consistent
- [x] User-management requirements are consistent
- [x] Delete rules are consistent
- [x] Stock status rules are consistent

## C# / WinForms Learning Value
- [x] Multiple forms required
- [x] CRUD operations required
- [x] DataGridView usage required
- [x] Form events required
- [x] Input validation required
- [x] MessageBox/error handling required
- [x] Classes/models required
- [x] Database interaction required
- [x] Search/filter functionality required
- [x] Navigation between forms required

## Scope Control
- [x] No unnecessary enterprise architecture
- [x] No cloud dependency
- [x] No mobile application
- [x] No AI requirement
- [x] No barcode requirement for MVP
- [x] No advanced analytics requirement
- [x] MVP remains implementable by a university student

---

# 5. Final Gate

Before the requirements baseline is frozen:

- [x] Seven audit corrections incorporated
- [x] Business rules updated
- [x] Functional requirements updated
- [x] Data requirements updated
- [x] Validation requirements updated
- [x] Reporting requirements updated
- [x] CRUD matrix updated
- [x] Workflows updated
- [x] MVP updated
- [x] Future scope updated
- [x] Quality review updated
- [x] Traceability matrix updated
- [ ] Final use-case IDs verified against traceability

After the final use-case-ID verification passes:

**SRS v1.0 — REQUIREMENTS BASELINE APPROVED**
