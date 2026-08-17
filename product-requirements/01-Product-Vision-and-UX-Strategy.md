# 1. Product Vision & UX Strategy

## Product Vision

The Inventory Management System (IMS) is a calm, trustworthy operational tool for NovaTech Electronics. It helps users answer: what do we have, what is running low, and what just happened?

## UX Principles

- Clarity over decoration
- Data readability over visual effects
- Consistent interaction patterns
- Fast access to frequent inventory tasks
- Immediate, understandable feedback
- Error prevention before submission
- Destructive actions require confirmation
- Status is never communicated by color alone
- Practical desktop behavior for WinForms

## User Roles

### Administrator

Full operational and master-data management access, including Products, Categories, Suppliers, Customers, Inventory Adjustments, User Management, Stock-In, Stock-Out, history, and reports.

### Inventory / Store Staff

Staff may view and search Products, Categories, Suppliers, and Customers. Master-data create/edit/delete/deactivate actions are Administrator-only. Staff may perform Stock-In and Stock-Out, view transaction history and available reports, but cannot perform Inventory Adjustments or User Management.

Restricted controls are hidden or disabled according to the approved SRS role-visibility rules.

## Product Experience Goals

- A staff member can reach Stock-In, Stock-Out, and product search quickly.
- Inventory status is immediately understandable.
- Forms prevent invalid inventory operations before submission.
- Tables are optimized for scanning and common desktop workflows.
- The application feels professional without relying on web-only visual effects.
