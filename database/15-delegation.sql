-- Milestone 15: Delegation table (run once if not already present)
CREATE TABLE IF NOT EXISTS "Delegation" (
    "DelegationID"      SERIAL PRIMARY KEY,
    "DelegatedByUserID" INTEGER NOT NULL REFERENCES "User" ("UserID"),
    "DelegatedToUserID" INTEGER NOT NULL REFERENCES "User" ("UserID"),
    "Responsibility"    VARCHAR(50)  NOT NULL,
    "StartDate"         DATE         NOT NULL,
    "EndDate"           DATE         NOT NULL,
    "Reason"            VARCHAR(500) NOT NULL,
    "Status"            VARCHAR(20)  NOT NULL DEFAULT 'Active',
    "CreatedDateTime"   TIMESTAMPTZ  NOT NULL DEFAULT NOW(),
    "RevokedByUserID"   INTEGER      NULL REFERENCES "User" ("UserID"),
    "RevokedDateTime"   TIMESTAMPTZ  NULL,
    CONSTRAINT "CK_Delegation_Dates" CHECK ("EndDate" >= "StartDate"),
    CONSTRAINT "CK_Delegation_Status" CHECK ("Status" IN ('Active', 'Expired', 'Revoked')),
    CONSTRAINT "CK_Delegation_Responsibility" CHECK (
        "Responsibility" IN ('StockIn', 'StockOut', 'ReportAccess')
    )
);

CREATE INDEX IF NOT EXISTS "IX_Delegation_ToUser"
    ON "Delegation" ("DelegatedToUserID", "Status");
