# 2. Project Overview

## 2.1 Project Title

University Windows Programming Project – Inventory Management System

## 2.2 System Name

**Inventory Management System (IMS)**

## 2.3 Project Purpose

To design and (later) implement a realistic, educational desktop Inventory Management System using C#, .NET, Windows Forms, and a relational database.  

The project must demonstrate solid understanding of:

- C# language fundamentals
- Object-oriented programming (classes, objects, properties, methods, encapsulation)
- Collections
- Exception handling
- Input validation
- Event-driven programming with WinForms
- Forms and navigation
- Full CRUD operations
- Database interaction
- Searching and filtering
- Basic reporting
- Sound software engineering practices

## 2.4 Business Context (Summary)

The system is designed for **NovaTech Electronics**, a small-to-medium retail business that purchases electronic gadgets, computer accessories, and related products from suppliers, stores them in a single warehouse/store location, and sells them to walk-in and regular customers.

Inventory accuracy is critical because stock-outs lead to lost sales and overstock ties up capital.

## 2.5 Problem Statement

Currently the business relies on spreadsheets, paper records, and informal verbal updates. This leads to:

- Inaccurate stock counts
- Difficulty identifying low-stock items in time
- No reliable history of stock movements
- Time-consuming manual searches
- Risk of selling items that are actually out of stock
- Poor visibility for the owner/manager

## 2.6 Proposed Solution

A single-user / small-team Windows desktop application that allows authorised staff to:

- Maintain product, category, supplier, and customer master data
- Record stock-in (purchases/receipts) and stock-out (sales/issues)
- Automatically update current stock quantities
- Detect and highlight low-stock and out-of-stock products
- Search and filter inventory and transactions
- View a useful dashboard
- Generate basic inventory and transaction reports
- Maintain a complete, auditable history of stock movements

## 2.7 Target Users

- Store Owner / Administrator
- Inventory / Store Staff

## 2.8 Intended Environment

- Windows desktop application (Windows 10/11)
- Local relational database (e.g., SQL Server LocalDB, SQLite, or SQL Server Express)
- Single installation or small local network (no multi-branch or cloud requirements)

## 2.9 Project Assumptions

See document `07-Assumptions-and-Constraints.md`.

## 2.10 Project Constraints

See document `07-Assumptions-and-Constraints.md`.
