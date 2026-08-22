-- NovaTech IMS — Milestone 3 (PostgreSQL, optional)
-- Sample master-data only. No users (password hashing = Milestone 9).

BEGIN;

INSERT INTO "Category" ("CategoryName", "Description", "IsActive")
SELECT v.name, v.descr, TRUE
FROM (VALUES
    ('Electronics', 'Consumer and office electronics'),
    ('Accessories', 'Cables, cases, and peripherals'),
    ('Components',  'Internal parts and modules')
) AS v(name, descr)
WHERE NOT EXISTS (SELECT 1 FROM "Category" LIMIT 1);

INSERT INTO "Supplier" ("SupplierName", "ContactPerson", "Phone", "Email", "IsActive")
SELECT v.name, v.contact, v.phone, v.email, TRUE
FROM (VALUES
    ('Nova Supply Co.', 'A. Bekele', '+251-11-000-0001', 'sales@novasupply.example'),
    ('East Africa Parts', 'M. Hailu', '+251-11-000-0002', 'orders@eaparts.example')
) AS v(name, contact, phone, email)
WHERE NOT EXISTS (SELECT 1 FROM "Supplier" LIMIT 1);

INSERT INTO "Customer" ("CustomerName", "Phone", "Email", "IsActive")
SELECT v.name, v.phone, v.email, TRUE
FROM (VALUES
    ('Addis Retail PLC', '+251-11-555-0100', 'purchasing@addisretail.example'),
    ('Campus Store', '+251-11-555-0200', NULL)
) AS v(name, phone, email)
WHERE NOT EXISTS (SELECT 1 FROM "Customer" LIMIT 1);

COMMIT;

SELECT 'Optional seed finished. Products/users/transactions intentionally not seeded.' AS message;
