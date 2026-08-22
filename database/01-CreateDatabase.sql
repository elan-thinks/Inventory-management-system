-- NovaTech IMS — Milestone 3
-- Creates the application database if it does not exist.
-- Target: SQL Server LocalDB / Express / Developer

IF DB_ID(N'NovaTechIMS') IS NULL
BEGIN
    CREATE DATABASE NovaTechIMS;
    PRINT 'Database NovaTechIMS created.';
END
ELSE
BEGIN
    PRINT 'Database NovaTechIMS already exists.';
END
GO
