-- NovaTech IMS — Milestone 3 (PostgreSQL)
-- Connect to database NovaTechIMS, then run this script.

BEGIN;

CREATE TABLE IF NOT EXISTS "User" (
    "UserID"            SERIAL          PRIMARY KEY,
    "Username"          VARCHAR(50)     NOT NULL,
    "PasswordHash"      VARCHAR(256)    NOT NULL,
    "PasswordSalt"      VARCHAR(256)    NOT NULL,
    "FullName"          VARCHAR(100)    NOT NULL,
    "Role"              VARCHAR(30)     NOT NULL,
    "IsActive"          BOOLEAN         NOT NULL DEFAULT TRUE,
    "CreatedDate"       TIMESTAMPTZ     NOT NULL DEFAULT (NOW() AT TIME ZONE),
    "LastLoginDate"     TIMESTAMPTZ     NULL,
    CONSTRAINT "UQ_User_Username" UNIQUE ("Username"),
    CONSTRAINT "CK_User_Role" CHECK ("Role" IN ('Administrator', 'InventoryStaff')),
    CONSTRAINT "CK_User_Username_NotEmpty" CHECK (LENGTH(TRIM("Username")) > 0),
    CONSTRAINT "CK_User_FullName_NotEmpty" CHECK (LENGTH(TRIM("FullName")) > 0)
);

CREATE TABLE IF NOT EXISTS "Category" (
    "CategoryID"        SERIAL          PRIMARY KEY,
    "CategoryName"      VARCHAR(100)    NOT NULL,
    "Description"       VARCHAR(500)    NULL,
    "IsActive"          BOOLEAN         NOT NULL DEFAULT TRUE,
    "CreatedDate"       TIMESTAMPTZ     NOT NULL DEFAULT (NOW() AT TIME ZONE),
    "ModifiedDate"      TIMESTAMPTZ     NULL,
    CONSTRAINT "UQ_Category_Name" UNIQUE ("CategoryName"),
    CONSTRAINT "CK_Category_Name_NotEmpty" CHECK (LENGTH(TRIM("CategoryName")) > 0)
);

CREATE TABLE IF NOT EXISTS "Supplier" (
    "SupplierID"        SERIAL          PRIMARY KEY,
    "SupplierName"      VARCHAR(150)    NOT NULL,
    "ContactPerson"     VARCHAR(100)    NULL,
    "Phone"             VARCHAR(30)     NULL,
    "Email"             VARCHAR(100)    NULL,
    "Address"           VARCHAR(300)    NULL,
    "IsActive"          BOOLEAN         NOT NULL DEFAULT TRUE,
    "CreatedDate"       TIMESTAMPTZ     NOT NULL DEFAULT (NOW() AT TIME ZONE),
    "ModifiedDate"      TIMESTAMPTZ     NULL,
    CONSTRAINT "CK_Supplier_Name_NotEmpty" CHECK (LENGTH(TRIM("SupplierName")) > 0)
);

CREATE TABLE IF NOT EXISTS "Customer" (
    "CustomerID"        SERIAL          PRIMARY KEY,
    "CustomerName"      VARCHAR(150)    NOT NULL,
    "Phone"             VARCHAR(30)     NULL,
    "Email"             VARCHAR(100)    NULL,
    "Address"           VARCHAR(300)    NULL,
    "IsActive"          BOOLEAN         NOT NULL DEFAULT TRUE,
    "CreatedDate"       TIMESTAMPTZ     NOT NULL DEFAULT (NOW() AT TIME ZONE),
    "ModifiedDate"      TIMESTAMPTZ     NULL,
    CONSTRAINT "CK_Customer_Name_NotEmpty" CHECK (LENGTH(TRIM("CustomerName")) > 0)
);

CREATE TABLE IF NOT EXISTS "Product" (
    "ProductID"             SERIAL          PRIMARY KEY,
    "ProductName"           VARCHAR(150)    NOT NULL,
    "CategoryID"            INT             NOT NULL,
    "SupplierID"            INT             NOT NULL,
    "Description"           VARCHAR(500)    NULL,
    "PurchasePrice"         NUMERIC(18,2)   NOT NULL,
    "SellingPrice"          NUMERIC(18,2)   NOT NULL,
    "QuantityOnHand"        INT             NOT NULL DEFAULT 0,
    "MinimumStockLevel"     INT             NOT NULL DEFAULT 0,
    "IsActive"              BOOLEAN         NOT NULL DEFAULT TRUE,
    "CreatedDate"           TIMESTAMPTZ     NOT NULL DEFAULT (NOW() AT TIME ZONE),
    "ModifiedDate"          TIMESTAMPTZ     NULL,
    "CreatedByUserID"       INT             NULL,
    "ModifiedByUserID"      INT             NULL,
    CONSTRAINT "CK_Product_Name_NotEmpty" CHECK (LENGTH(TRIM("ProductName")) > 0),
    CONSTRAINT "CK_Product_PurchasePrice" CHECK ("PurchasePrice" >= 0),
    CONSTRAINT "CK_Product_SellingPrice" CHECK ("SellingPrice" >= 0),
    CONSTRAINT "CK_Product_QuantityOnHand" CHECK ("QuantityOnHand" >= 0),
    CONSTRAINT "CK_Product_MinStock" CHECK ("MinimumStockLevel" >= 0),
    CONSTRAINT "FK_Product_Category" FOREIGN KEY ("CategoryID") REFERENCES "Category" ("CategoryID") ON DELETE NO ACTION,
    CONSTRAINT "FK_Product_Supplier" FOREIGN KEY ("SupplierID") REFERENCES "Supplier" ("SupplierID") ON DELETE NO ACTION,
    CONSTRAINT "FK_Product_CreatedBy" FOREIGN KEY ("CreatedByUserID") REFERENCES "User" ("UserID") ON DELETE NO ACTION,
    CONSTRAINT "FK_Product_ModifiedBy" FOREIGN KEY ("ModifiedByUserID") REFERENCES "User" ("UserID") ON DELETE NO ACTION
);

CREATE INDEX IF NOT EXISTS "IX_Product_CategoryID" ON "Product" ("CategoryID");
CREATE INDEX IF NOT EXISTS "IX_Product_SupplierID" ON "Product" ("SupplierID");
CREATE INDEX IF NOT EXISTS "IX_Product_IsActive" ON "Product" ("IsActive");

CREATE TABLE IF NOT EXISTS "InventoryTransaction" (
    "TransactionID"         SERIAL          PRIMARY KEY,
    "TransactionType"       VARCHAR(20)     NOT NULL,
    "ProductID"             INT             NOT NULL,
    "Quantity"              INT             NOT NULL,
    "TransactionDate"       DATE            NOT NULL,
    "UnitPrice"             NUMERIC(18,2)   NULL,
    "SupplierID"            INT             NULL,
    "CustomerID"            INT             NULL,
    "PreviousQuantity"      INT             NULL,
    "NewQuantity"           INT             NULL,
    "Difference"            INT             NULL,
    "Reason"                VARCHAR(300)    NULL,
    "Notes"                 VARCHAR(500)    NULL,
    "UserID"                INT             NOT NULL,
    "CreatedDateTime"       TIMESTAMPTZ     NOT NULL DEFAULT (NOW() AT TIME ZONE),
    CONSTRAINT "CK_Txn_Type" CHECK ("TransactionType" IN ('StockIn', 'StockOut', 'Adjustment')),
    CONSTRAINT "CK_Txn_Quantity" CHECK ("Quantity" >= 0),
    CONSTRAINT "CK_Txn_UnitPrice" CHECK ("UnitPrice" IS NULL OR "UnitPrice" >= 0),
    CONSTRAINT "FK_Txn_Product" FOREIGN KEY ("ProductID") REFERENCES "Product" ("ProductID") ON DELETE NO ACTION,
    CONSTRAINT "FK_Txn_Supplier" FOREIGN KEY ("SupplierID") REFERENCES "Supplier" ("SupplierID") ON DELETE NO ACTION,
    CONSTRAINT "FK_Txn_Customer" FOREIGN KEY ("CustomerID") REFERENCES "Customer" ("CustomerID") ON DELETE NO ACTION,
    CONSTRAINT "FK_Txn_User" FOREIGN KEY ("UserID") REFERENCES "User" ("UserID") ON DELETE NO ACTION
);

CREATE INDEX IF NOT EXISTS "IX_Txn_ProductID" ON "InventoryTransaction" ("ProductID");
CREATE INDEX IF NOT EXISTS "IX_Txn_TransactionDate" ON "InventoryTransaction" ("TransactionDate");
CREATE INDEX IF NOT EXISTS "IX_Txn_Type" ON "InventoryTransaction" ("TransactionType");
CREATE INDEX IF NOT EXISTS "IX_Txn_UserID" ON "InventoryTransaction" ("UserID");

CREATE TABLE IF NOT EXISTS "Delegation" (
    "DelegationID"          SERIAL          PRIMARY KEY,
    "DelegatedByUserID"     INT             NOT NULL,
    "DelegatedToUserID"     INT             NOT NULL,
    "Responsibility"        VARCHAR(30)     NOT NULL,
    "StartDate"             DATE            NOT NULL,
    "EndDate"               DATE            NOT NULL,
    "Reason"                VARCHAR(300)    NOT NULL,
    "Status"                VARCHAR(20)     NOT NULL,
    "CreatedDateTime"       TIMESTAMPTZ     NOT NULL DEFAULT (NOW() AT TIME ZONE),
    "RevokedByUserID"       INT             NULL,
    "RevokedDateTime"       TIMESTAMPTZ     NULL,
    CONSTRAINT "CK_Delegation_Responsibility" CHECK ("Responsibility" IN ('StockIn', 'StockOut', 'ReportAccess')),
    CONSTRAINT "CK_Delegation_Status" CHECK ("Status" IN ('Active', 'Expired', 'Revoked')),
    CONSTRAINT "CK_Delegation_Dates" CHECK ("EndDate" >= "StartDate"),
    CONSTRAINT "CK_Delegation_Reason_NotEmpty" CHECK (LENGTH(TRIM("Reason")) > 0),
    CONSTRAINT "CK_Delegation_NotSelf" CHECK ("DelegatedByUserID" <> "DelegatedToUserID"),
    CONSTRAINT "FK_Delegation_By" FOREIGN KEY ("DelegatedByUserID") REFERENCES "User" ("UserID") ON DELETE NO ACTION,
    CONSTRAINT "FK_Delegation_To" FOREIGN KEY ("DelegatedToUserID") REFERENCES "User" ("UserID") ON DELETE NO ACTION,
    CONSTRAINT "FK_Delegation_RevokedBy" FOREIGN KEY ("RevokedByUserID") REFERENCES "User" ("UserID") ON DELETE NO ACTION
);

CREATE INDEX IF NOT EXISTS "IX_Delegation_ToUser" ON "Delegation" ("DelegatedToUserID", "Status");
CREATE INDEX IF NOT EXISTS "IX_Delegation_Dates" ON "Delegation" ("StartDate", "EndDate");

CREATE UNIQUE INDEX IF NOT EXISTS "UQ_Delegation_Active_Recipient_Responsibility"
    ON "Delegation" ("DelegatedToUserID", "Responsibility")
    WHERE "Status" = 'Active';

COMMIT;

SELECT 'NovaTechIMS PostgreSQL schema created successfully.' AS message;
