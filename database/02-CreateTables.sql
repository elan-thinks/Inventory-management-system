-- NovaTech IMS — Milestone 3
-- Schema: tables, constraints, indexes
-- Run against database NovaTechIMS after 01-CreateDatabase.sql

USE NovaTechIMS;
GO

/* ========== [User] ========== */
IF OBJECT_ID(N'dbo.[User]', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.[User]
    (
        UserID          INT             NOT NULL IDENTITY(1,1),
        Username        NVARCHAR(50)    NOT NULL,
        PasswordHash    NVARCHAR(256)   NOT NULL,
        PasswordSalt    NVARCHAR(256)   NOT NULL,
        FullName        NVARCHAR(100)   NOT NULL,
        Role            NVARCHAR(30)    NOT NULL,
        IsActive        BIT             NOT NULL CONSTRAINT DF_User_IsActive DEFAULT (1),
        CreatedDate     DATETIME2(0)    NOT NULL CONSTRAINT DF_User_CreatedDate DEFAULT (SYSUTCDATETIME()),
        LastLoginDate   DATETIME2(0)    NULL,

        CONSTRAINT PK_User PRIMARY KEY (UserID),
        CONSTRAINT UQ_User_Username UNIQUE (Username),
        CONSTRAINT CK_User_Role CHECK (Role IN (N'Administrator', N'InventoryStaff')),
        CONSTRAINT CK_User_Username_NotEmpty CHECK (LEN(LTRIM(RTRIM(Username))) > 0),
        CONSTRAINT CK_User_FullName_NotEmpty CHECK (LEN(LTRIM(RTRIM(FullName))) > 0)
    );
END
GO

/* ========== Category ========== */
IF OBJECT_ID(N'dbo.Category', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Category
    (
        CategoryID      INT             NOT NULL IDENTITY(1,1),
        CategoryName    NVARCHAR(100)   NOT NULL,
        Description     NVARCHAR(500)   NULL,
        IsActive        BIT             NOT NULL CONSTRAINT DF_Category_IsActive DEFAULT (1),
        CreatedDate     DATETIME2(0)    NOT NULL CONSTRAINT DF_Category_CreatedDate DEFAULT (SYSUTCDATETIME()),
        ModifiedDate    DATETIME2(0)    NULL,

        CONSTRAINT PK_Category PRIMARY KEY (CategoryID),
        CONSTRAINT UQ_Category_Name UNIQUE (CategoryName),
        CONSTRAINT CK_Category_Name_NotEmpty CHECK (LEN(LTRIM(RTRIM(CategoryName))) > 0)
    );
END
GO

/* ========== Supplier ========== */
IF OBJECT_ID(N'dbo.Supplier', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Supplier
    (
        SupplierID      INT             NOT NULL IDENTITY(1,1),
        SupplierName    NVARCHAR(150)   NOT NULL,
        ContactPerson   NVARCHAR(100)   NULL,
        Phone           NVARCHAR(30)    NULL,
        Email           NVARCHAR(100)   NULL,
        Address         NVARCHAR(300)   NULL,
        IsActive        BIT             NOT NULL CONSTRAINT DF_Supplier_IsActive DEFAULT (1),
        CreatedDate     DATETIME2(0)    NOT NULL CONSTRAINT DF_Supplier_CreatedDate DEFAULT (SYSUTCDATETIME()),
        ModifiedDate    DATETIME2(0)    NULL,

        CONSTRAINT PK_Supplier PRIMARY KEY (SupplierID),
        CONSTRAINT CK_Supplier_Name_NotEmpty CHECK (LEN(LTRIM(RTRIM(SupplierName))) > 0)
    );
END
GO

/* ========== Customer ========== */
IF OBJECT_ID(N'dbo.Customer', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Customer
    (
        CustomerID      INT             NOT NULL IDENTITY(1,1),
        CustomerName    NVARCHAR(150)   NOT NULL,
        Phone           NVARCHAR(30)    NULL,
        Email           NVARCHAR(100)   NULL,
        Address         NVARCHAR(300)   NULL,
        IsActive        BIT             NOT NULL CONSTRAINT DF_Customer_IsActive DEFAULT (1),
        CreatedDate     DATETIME2(0)    NOT NULL CONSTRAINT DF_Customer_CreatedDate DEFAULT (SYSUTCDATETIME()),
        ModifiedDate    DATETIME2(0)    NULL,

        CONSTRAINT PK_Customer PRIMARY KEY (CustomerID),
        CONSTRAINT CK_Customer_Name_NotEmpty CHECK (LEN(LTRIM(RTRIM(CustomerName))) > 0)
    );
END
GO

/* ========== Product ========== */
IF OBJECT_ID(N'dbo.Product', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Product
    (
        ProductID           INT             NOT NULL IDENTITY(1,1),
        ProductName         NVARCHAR(150)   NOT NULL,
        CategoryID          INT             NOT NULL,
        SupplierID          INT             NOT NULL,
        Description         NVARCHAR(500)   NULL,
        PurchasePrice       DECIMAL(18,2)   NOT NULL,
        SellingPrice        DECIMAL(18,2)   NOT NULL,
        QuantityOnHand      INT             NOT NULL CONSTRAINT DF_Product_Qty DEFAULT (0),
        MinimumStockLevel   INT             NOT NULL CONSTRAINT DF_Product_MinStock DEFAULT (0),
        IsActive            BIT             NOT NULL CONSTRAINT DF_Product_IsActive DEFAULT (1),
        CreatedDate         DATETIME2(0)    NOT NULL CONSTRAINT DF_Product_CreatedDate DEFAULT (SYSUTCDATETIME()),
        ModifiedDate        DATETIME2(0)    NULL,
        CreatedByUserID     INT             NULL,
        ModifiedByUserID    INT             NULL,

        CONSTRAINT PK_Product PRIMARY KEY (ProductID),
        CONSTRAINT CK_Product_Name_NotEmpty CHECK (LEN(LTRIM(RTRIM(ProductName))) > 0),
        CONSTRAINT CK_Product_PurchasePrice CHECK (PurchasePrice >= 0),
        CONSTRAINT CK_Product_SellingPrice CHECK (SellingPrice >= 0),
        CONSTRAINT CK_Product_QuantityOnHand CHECK (QuantityOnHand >= 0),
        CONSTRAINT CK_Product_MinStock CHECK (MinimumStockLevel >= 0),

        CONSTRAINT FK_Product_Category FOREIGN KEY (CategoryID)
            REFERENCES dbo.Category (CategoryID) ON DELETE NO ACTION,
        CONSTRAINT FK_Product_Supplier FOREIGN KEY (SupplierID)
            REFERENCES dbo.Supplier (SupplierID) ON DELETE NO ACTION,
        CONSTRAINT FK_Product_CreatedBy FOREIGN KEY (CreatedByUserID)
            REFERENCES dbo.[User] (UserID) ON DELETE NO ACTION,
        CONSTRAINT FK_Product_ModifiedBy FOREIGN KEY (ModifiedByUserID)
            REFERENCES dbo.[User] (UserID) ON DELETE NO ACTION
    );

    CREATE INDEX IX_Product_CategoryID ON dbo.Product (CategoryID);
    CREATE INDEX IX_Product_SupplierID ON dbo.Product (SupplierID);
    CREATE INDEX IX_Product_IsActive ON dbo.Product (IsActive);
END
GO

/* ========== InventoryTransaction (append-only) ========== */
IF OBJECT_ID(N'dbo.InventoryTransaction', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.InventoryTransaction
    (
        TransactionID       INT             NOT NULL IDENTITY(1,1),
        TransactionType     NVARCHAR(20)    NOT NULL,
        ProductID           INT             NOT NULL,
        Quantity            INT             NOT NULL,
        TransactionDate     DATE            NOT NULL,
        UnitPrice           DECIMAL(18,2)   NULL,
        SupplierID          INT             NULL,
        CustomerID          INT             NULL,
        PreviousQuantity    INT             NULL,
        NewQuantity         INT             NULL,
        Difference          INT             NULL,
        Reason              NVARCHAR(300)   NULL,
        Notes               NVARCHAR(500)   NULL,
        UserID              INT             NOT NULL,
        CreatedDateTime     DATETIME2(0)    NOT NULL CONSTRAINT DF_Txn_Created DEFAULT (SYSUTCDATETIME()),

        CONSTRAINT PK_InventoryTransaction PRIMARY KEY (TransactionID),
        CONSTRAINT CK_Txn_Type CHECK (TransactionType IN (N'StockIn', N'StockOut', N'Adjustment')),
        CONSTRAINT CK_Txn_Quantity CHECK (Quantity >= 0),
        CONSTRAINT CK_Txn_UnitPrice CHECK (UnitPrice IS NULL OR UnitPrice >= 0),

        CONSTRAINT FK_Txn_Product FOREIGN KEY (ProductID)
            REFERENCES dbo.Product (ProductID) ON DELETE NO ACTION,
        CONSTRAINT FK_Txn_Supplier FOREIGN KEY (SupplierID)
            REFERENCES dbo.Supplier (SupplierID) ON DELETE NO ACTION,
        CONSTRAINT FK_Txn_Customer FOREIGN KEY (CustomerID)
            REFERENCES dbo.Customer (CustomerID) ON DELETE NO ACTION,
        CONSTRAINT FK_Txn_User FOREIGN KEY (UserID)
            REFERENCES dbo.[User] (UserID) ON DELETE NO ACTION
    );

    CREATE INDEX IX_Txn_ProductID ON dbo.InventoryTransaction (ProductID);
    CREATE INDEX IX_Txn_TransactionDate ON dbo.InventoryTransaction (TransactionDate);
    CREATE INDEX IX_Txn_Type ON dbo.InventoryTransaction (TransactionType);
    CREATE INDEX IX_Txn_UserID ON dbo.InventoryTransaction (UserID);
END
GO

/* ========== Delegation ========== */
IF OBJECT_ID(N'dbo.Delegation', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Delegation
    (
        DelegationID        INT             NOT NULL IDENTITY(1,1),
        DelegatedByUserID   INT             NOT NULL,
        DelegatedToUserID   INT             NOT NULL,
        Responsibility      NVARCHAR(30)    NOT NULL,
        StartDate           DATE            NOT NULL,
        EndDate             DATE            NOT NULL,
        Reason              NVARCHAR(300)   NOT NULL,
        Status              NVARCHAR(20)    NOT NULL,
        CreatedDateTime     DATETIME2(0)    NOT NULL CONSTRAINT DF_Delegation_Created DEFAULT (SYSUTCDATETIME()),
        RevokedByUserID     INT             NULL,
        RevokedDateTime     DATETIME2(0)    NULL,

        CONSTRAINT PK_Delegation PRIMARY KEY (DelegationID),
        CONSTRAINT CK_Delegation_Responsibility CHECK (
            Responsibility IN (N'StockIn', N'StockOut', N'ReportAccess')),
        CONSTRAINT CK_Delegation_Status CHECK (
            Status IN (N'Active', N'Expired', N'Revoked')),
        CONSTRAINT CK_Delegation_Dates CHECK (EndDate >= StartDate),
        CONSTRAINT CK_Delegation_Reason_NotEmpty CHECK (LEN(LTRIM(RTRIM(Reason))) > 0),
        CONSTRAINT CK_Delegation_NotSelf CHECK (DelegatedByUserID <> DelegatedToUserID),

        CONSTRAINT FK_Delegation_By FOREIGN KEY (DelegatedByUserID)
            REFERENCES dbo.[User] (UserID) ON DELETE NO ACTION,
        CONSTRAINT FK_Delegation_To FOREIGN KEY (DelegatedToUserID)
            REFERENCES dbo.[User] (UserID) ON DELETE NO ACTION,
        CONSTRAINT FK_Delegation_RevokedBy FOREIGN KEY (RevokedByUserID)
            REFERENCES dbo.[User] (UserID) ON DELETE NO ACTION
    );

    CREATE INDEX IX_Delegation_ToUser ON dbo.Delegation (DelegatedToUserID, Status);
    CREATE INDEX IX_Delegation_Dates ON dbo.Delegation (StartDate, EndDate);

    CREATE UNIQUE INDEX UQ_Delegation_Active_Recipient_Responsibility
        ON dbo.Delegation (DelegatedToUserID, Responsibility)
        WHERE Status = N'Active';
END
GO

PRINT 'NovaTechIMS schema created successfully.';
GO
