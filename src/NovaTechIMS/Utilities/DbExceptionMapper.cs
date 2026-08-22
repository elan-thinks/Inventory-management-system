using System;
using Npgsql;

namespace NovaTechIMS.Utilities;

/// <summary>
/// Translates PostgreSQL errors into application exceptions (no raw SQL to UI).
/// </summary>
public static class DbExceptionMapper
{
    public static Exception Map(Exception ex)
    {
        if (ex is AppException)
            return ex;

        if (ex is PostgresException pg)
            return MapPostgres(pg);

        if (ex.InnerException is PostgresException innerPg)
            return MapPostgres(innerPg);

        // Connection failures often surface as NpgsqlException
        if (ex is NpgsqlException)
        {
            return new DataAccessException(
                "Unable to reach the database. Check that PostgreSQL is running and the connection string is correct.",
                ex);
        }

        return new DataAccessException(
            "A database error occurred. Please try again.",
            ex);
    }

    private static AppException MapPostgres(PostgresException pg)
    {
        return pg.SqlState switch
        {
            PostgresErrorCodes.UniqueViolation =>
                new DuplicateRecordException("A record with the same unique value already exists."),

            PostgresErrorCodes.ForeignKeyViolation =>
                new BusinessRuleException(
                    "This record is still referenced by other data and cannot be changed or deleted."),

            PostgresErrorCodes.NotNullViolation =>
                new ValidationException("A required field was missing. Please complete all required fields."),

            PostgresErrorCodes.CheckViolation =>
                new ValidationException("One or more values are outside the allowed range."),

            PostgresErrorCodes.InvalidPassword or
            PostgresErrorCodes.InvalidAuthorizationSpecification =>
                new DataAccessException("Database login failed. Check the connection credentials."),

            // 57P01 admin_shutdown, 57P03 cannot_connect_now, etc.
            "57P01" or "57P03" or "08006" or "08001" or "08004" =>
                new DataAccessException(
                    "Unable to reach the database. Check that PostgreSQL is running and the connection string is correct."),

            _ => new DataAccessException("A database error occurred. Please try again.")
        };
    }
}
