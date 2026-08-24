-- NovaTech IMS — Demo seed data (10 each + users)
-- Database: NovaTechIMS (PostgreSQL)
-- Run AFTER 01-CreateDatabase.sql and 02-CreateTables.sql
-- Safe to re-run: skips rows that already exist by unique username / category name / product name.
--
-- App logins after this script:
--   admin / Admin@123          (Administrator)
--   staff01..staff10 / Staff@123  (InventoryStaff)
--
-- Password hashes: PBKDF2-SHA256, 100000 iterations, 16-byte salt, 32-byte key (matches PasswordHasher.cs)

BEGIN;

-- ---------------------------------------------------------------------------
-- Users: admin + 10 staff
-- ---------------------------------------------------------------------------
INSERT INTO "User" ("Username", "PasswordHash", "PasswordSalt", "FullName", "Role", "IsActive")
SELECT v.username, v.hash, v.salt, v.fullname, v.role, TRUE
FROM (VALUES
    ('admin',   '+eXu9hD2jdaTzji5GIerQnBRERb3Fp2LxvMFTl6XMmo=', 'x6xxxseMU1csd3lasVcO0g==', 'System Administrator', 'Administrator'),
    ('staff01', 'f7i7FkACL5csnM4hDqIdXJfm+AfpGgwg4GfPR4jvnGA=', 'FQQwskyBaWFtXc1qlh58fw==', 'Abebe Kebede',        'InventoryStaff'),
    ('staff02', 'G6iaN3hAaotGeFeAi3cgtkJ3kjLVnGQRAW9JB17vBic=', '5g/k2lWxsgRymvDaBa7g/A==', 'Tigist Hailu',         'InventoryStaff'),
    ('staff03', 'X5WrXeaEbaTRsvwm2x+0ujY8aRzillLwJCQ0o82WW4c=', '0ev7Avzp8Qhg5jRbp+tzcg==', 'Dawit Mekonnen',       'InventoryStaff'),
    ('staff04', 'JSUc6KjnCmE4lsRwVU1/lPzqIiM6X35G7XLyJ6lpoT4=', 'Wx9PSQK6ZGZoO6ns+sh4zA==', 'Sara Tesfaye',         'InventoryStaff'),
    ('staff05', 'G5Y6lgGVlPNej2bbYrXOgrLsVYK34iRLWacPWmIjmNY=', 'FmAUUINC3BnVeiKO5MxeVQ==', 'Yonas Alemu',          'InventoryStaff'),
    ('staff06', 'J0jfw2B89gkP8dqzDFwTfW2EL9EiuepAIxW1CiH3Vr8=', 'D+0EmdoGV1wa+sm6/+xPow==', 'Hanna Girma',          'InventoryStaff'),
    ('staff07', 'u0xfPJV525hrwpbHoX7UfUIgRx2Xf/WQo4bHHUl43Ws=', '89cG1XfsUBP6ybIgI5MEAQ==', 'Bereket Solomon',      'InventoryStaff'),
    ('staff08', 'hwJLfBofTwa7HAtGuVl4ab2nad7uzUuCeu5rbdLk8CI=', 'BPz6J+9u+aeUtivlEuJlgg==', 'Meron Assefa',         'InventoryStaff'),
    ('staff09', 'G8ahryoaQQqTwe7TsuPq8y80L3g4RPi03ggOxoeMkgg=', '3ZVRcBTlxYNpTcVLJx6R7A==', 'Kidus Bekele',         'InventoryStaff'),
    ('staff10', 'AMjkflZFqvDqbj7f1VDarF99ONnx1FFNJIEPxTSQ7kM=', 'BcezhWUN9fed/UimSaqbUg==', 'Liya Desta',           'InventoryStaff')
) AS v(username, hash, salt, fullname, role)
WHERE NOT EXISTS (
    SELECT 1 FROM "User" u WHERE u."Username" = v.username
);

-- ---------------------------------------------------------------------------
-- Categories (10)
-- ---------------------------------------------------------------------------
INSERT INTO "Category" ("CategoryName", "Description", "IsActive")
SELECT v.name, v.descr, TRUE
FROM (VALUES
    ('Laptops',           'Notebook computers and ultrabooks'),
    ('Desktops',          'Desktop PCs and workstations'),
    ('Monitors',          'LCD and LED displays'),
    ('Keyboards & Mice',  'Input devices'),
    ('Storage',           'HDD, SSD, and external drives'),
    ('Networking',        'Routers, switches, and adapters'),
    ('Cables & Adapters', 'Power and data cables'),
    ('Audio',             'Headsets, speakers, microphones'),
    ('Power',             'UPS, batteries, and adapters'),
    ('Accessories',       'Bags, stands, and peripherals')
) AS v(name, descr)
WHERE NOT EXISTS (
    SELECT 1 FROM "Category" c WHERE c."CategoryName" = v.name
);

-- ---------------------------------------------------------------------------
-- Suppliers (10)
-- ---------------------------------------------------------------------------
INSERT INTO "Supplier" ("SupplierName", "ContactPerson", "Phone", "Email", "Address", "IsActive")
SELECT v.name, v.contact, v.phone, v.email, v.addr, TRUE
FROM (VALUES
    ('Addis Tech Wholesale',  'Amanuel Tadesse', '+251-11-100-1001', 'sales@addistech.example',     'Bole, Addis Ababa'),
    ('East Africa Parts',     'Meron Hailu',     '+251-11-100-1002', 'orders@eaparts.example',      'Kirkos, Addis Ababa'),
    ('Horizon Electronics',   'Samuel Girma',    '+251-11-100-1003', 'info@horizonelec.example',    'CMC, Addis Ababa'),
    ('Nile Components',       'Betty Kebede',    '+251-11-100-1004', 'supply@nilecomp.example',     'Piassa, Addis Ababa'),
    ('Summit IT Distributors','Yared Mekonnen',  '+251-11-100-1005', 'desk@summitit.example',       'Mexico Sq., Addis Ababa'),
    ('Blue Nile Gadgets',     'Hana Alemu',      '+251-11-100-1006', 'contact@bluenile.example',    'Sarbet, Addis Ababa'),
    ('Abyssinia Peripherals', 'Daniel Assefa',   '+251-11-100-1007', 'sales@abysperi.example',      'Gerji, Addis Ababa'),
    ('Rift Valley Supply',    'Selam Bekele',    '+251-11-100-1008', 'rv@riftvalley.example',       'Kazanchis, Addis Ababa'),
    ('Capital Computer Co.',  'Michael Desta',   '+251-11-100-1009', 'cc@capitalpc.example',        'Megenagna, Addis Ababa'),
    ('Unity Office Tech',     'Rahel Solomon',   '+251-11-100-1010', 'unity@officetech.example',    'Lebu, Addis Ababa')
) AS v(name, contact, phone, email, addr)
WHERE NOT EXISTS (
    SELECT 1 FROM "Supplier" s WHERE s."SupplierName" = v.name
);

-- ---------------------------------------------------------------------------
-- Customers (10)
-- ---------------------------------------------------------------------------
INSERT INTO "Customer" ("CustomerName", "Phone", "Email", "Address", "IsActive")
SELECT v.name, v.phone, v.email, v.addr, TRUE
FROM (VALUES
    ('Addis Retail PLC',       '+251-11-555-0101', 'purchasing@addisretail.example',  'Bole Atlas, Addis Ababa'),
    ('Campus Store',           '+251-11-555-0102', 'store@campus.example',            'Sidist Kilo, Addis Ababa'),
    ('Horizon Trading',        '+251-11-555-0103', 'buy@horizontrade.example',        'Kazanchis, Addis Ababa'),
    ('Green Valley Schools',   '+251-11-555-0104', 'it@greenvalley.example',          'CMC, Addis Ababa'),
    ('Metro Clinic Network',   '+251-11-555-0105', 'procurement@metroclinic.example', 'Sarbet, Addis Ababa'),
    ('Blue Sky Cafe Chain',    '+251-11-555-0106', 'ops@bluesky.example',             'Piassa, Addis Ababa'),
    ('Unity Bank Branch Ops',  '+251-11-555-0107', 'itops@unitybank.example',         'Mexico, Addis Ababa'),
    ('Sunrise Logistics',      '+251-11-555-0108', 'fleet@sunrise.example',           'Kaliti, Addis Ababa'),
    ('Abyssinia Media Group',  '+251-11-555-0109', 'tech@abysmedia.example',          'Gerji, Addis Ababa'),
    ('Nile Engineering PLC',   '+251-11-555-0110', 'office@nileeng.example',          'Lebu, Addis Ababa')
) AS v(name, phone, email, addr)
WHERE NOT EXISTS (
    SELECT 1 FROM "Customer" c WHERE c."CustomerName" = v.name
);

-- ---------------------------------------------------------------------------
-- Products (10) — linked to categories & suppliers by name
-- ---------------------------------------------------------------------------
INSERT INTO "Product" (
    "ProductName", "CategoryID", "SupplierID", "Description",
    "PurchasePrice", "SellingPrice", "QuantityOnHand", "MinimumStockLevel",
    "IsActive", "CreatedByUserID"
)
SELECT
    v.pname,
    c."CategoryID",
    s."SupplierID",
    v.descr,
    v.purchase,
    v.selling,
    v.qty,
    v.mins,
    TRUE,
    (SELECT u."UserID" FROM "User" u WHERE u."Username" = 'admin' LIMIT 1)
FROM (VALUES
    ('ProBook 15 Laptop',     'Laptops',           'Addis Tech Wholesale',  '15-inch business laptop',           18500.00, 22500.00, 12, 3),
    ('Office Desktop i5',     'Desktops',          'East Africa Parts',     'Core i5 office workstation',       14200.00, 17500.00,  8, 2),
    ('24-inch IPS Monitor',   'Monitors',          'Horizon Electronics',   'Full HD IPS display',               4800.00,  6200.00, 20, 5),
    ('Wireless Keyboard Set', 'Keyboards & Mice',  'Nile Components',       'Keyboard + mouse combo',            650.00,   950.00,  40, 10),
    ('1TB NVMe SSD',          'Storage',           'Summit IT Distributors','Internal solid-state drive',        3200.00,  4100.00, 25, 8),
    ('Gigabit Router AX',     'Networking',        'Blue Nile Gadgets',     'Wi-Fi 6 dual-band router',          2800.00,  3600.00, 15, 4),
    ('HDMI Cable 2m',         'Cables & Adapters', 'Abyssinia Peripherals', 'High-speed HDMI cable',              180.00,   280.00, 60, 15),
    ('USB Headset Pro',       'Audio',             'Rift Valley Supply',    'Noise-cancelling office headset',  1100.00,  1550.00, 18, 6),
    ('600VA UPS',             'Power',             'Capital Computer Co.',  'Line-interactive UPS',              3500.00,  4500.00, 10, 3),
    ('Laptop Stand Aluminum', 'Accessories',       'Unity Office Tech',     'Adjustable desk stand',              750.00,  1100.00, 30, 8)
) AS v(pname, cname, sname, descr, purchase, selling, qty, mins)
JOIN "Category" c ON c."CategoryName" = v.cname
JOIN "Supplier" s ON s."SupplierName" = v.sname
WHERE NOT EXISTS (
    SELECT 1 FROM "Product" p WHERE p."ProductName" = v.pname
);

COMMIT;

SELECT 'Demo seed complete.' AS message;
SELECT 'Users' AS entity, COUNT(*)::text AS cnt FROM "User"
UNION ALL SELECT 'Categories', COUNT(*)::text FROM "Category"
UNION ALL SELECT 'Suppliers', COUNT(*)::text FROM "Supplier"
UNION ALL SELECT 'Customers', COUNT(*)::text FROM "Customer"
UNION ALL SELECT 'Products', COUNT(*)::text FROM "Product";
