# Product UX Audit

## Status

🟢 APPROVED WITH CORRECTION — permission model resolved.

## Decision UX-DEC-001 — Inventory/Store Staff Master-Data Permissions

The approved SRS is interpreted conservatively for the MVP:

- Inventory/Store Staff may view and search Products, Categories, Suppliers, and Customers.
- Administrator is responsible for creating, editing, and deactivating/deleting master data.
- Inventory/Store Staff may perform Stock-In and Stock-Out.
- Inventory/Store Staff may not perform Inventory Adjustments.
- Only Administrators may manage users.
- Inventory/Store Staff may view reports and inventory history.

### Rationale

This resolves the SRS ambiguity without silently expanding Staff privileges. It also preserves a clear role-based authorization model and keeps destructive/master-data operations under Administrator control.

## Audit Checklist

- [x] SRS v1.0 used as source of truth
- [x] All major UX flows reviewed
- [x] CRUD UX reviewed
- [x] Inventory status UX reviewed
- [x] Append-only transaction history respected
- [x] Product initial quantity rule respected
- [x] Default supplier vs actual Stock-In supplier distinguished
- [x] Optional Stock-Out customer respected
- [x] MVP reporting scope respected
- [x] Staff permission ambiguity resolved
- [x] Unsupported features kept outside MVP

## Approval

Product Requirements + UX/UI Specification may proceed to visual design after the permission decision above is applied consistently to the Claude documents.
