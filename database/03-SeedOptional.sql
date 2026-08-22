-- NovaTech IMS — Milestone 3 (optional)
-- Sample master-data rows only. No users (password hashing = Milestone 9).
-- Safe to re-run: inserts only when tables are empty.

USE NovaTechIMS;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Category)
BEGIN
    INSERT INTO dbo.Category (CategoryName, Description, IsActive)
    VALUES
        (N'Electronics', N'Consumer and office electronics', 1),
        (N'Accessories', N'Cables, cases, and peripherals', 1),
        (N'Components',  N'Internal parts and modules', 1);
    PRINT 'Sample categories inserted.';
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Supplier)
BEGIN
    INSERT INTO dbo.Supplier (SupplierName, ContactPerson, Phone, Email, IsActive)
    VALUES
        (N'Nova Supply Co.', N'A. Bekele', N'+251-11-000-0001', N'sales@novasupply.example', 1),
        (N'East Africa Parts', N'M. Hailu', N'+251-11-000-0002', N'orders@eaparts.example', 1);
    PRINT 'Sample suppliers inserted.';
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Customer)
BEGIN
    INSERT INTO dbo.Customer (CustomerName, Phone, Email, IsActive)
    VALUES
        (N'Addis Retail PLC', N'+251-11-555-0100', N'purchasing@addisretail.example', 1),
        (N'Campus Store', N'+251-11-555-0200', NULL, 1);
    PRINT 'Sample customers inserted.';
END
GO

PRINT 'Optional seed finished. Products/users/transactions intentionally not seeded.';
GO
