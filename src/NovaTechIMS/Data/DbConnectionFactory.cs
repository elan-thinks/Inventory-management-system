using System;
using System.Configuration;
using Npgsql;

namespace NovaTechIMS.Data;

/// <summary>
/// Creates PostgreSQL connections (Milestone 4 skeleton).
/// Does not open the connection — caller opens/closes with using.
/// </summary>
public static class DbConnectionFactory
{
    public const string ConnectionStringName = "NovaTechIMS";

    /// <summary>
    /// Returns a new unopened <see cref="NpgsqlConnection"/>.
    /// </summary>
    public static NpgsqlConnection CreateConnection()
    {
        var settings = ConfigurationManager.ConnectionStrings[ConnectionStringName]
            ?? throw new InvalidOperationException(
                $"Connection string '{ConnectionStringName}' was not found in App.config.");

        if (string.IsNullOrWhiteSpace(settings.ConnectionString))
            throw new InvalidOperationException(
                $"Connection string '{ConnectionStringName}' is empty. Set Host/Database/Username/Password in App.config.");

        return new NpgsqlConnection(settings.ConnectionString);
    }

    /// <summary>Raw connection string for diagnostics (does not open a connection).</summary>
    public static string GetConnectionString()
    {
        var settings = ConfigurationManager.ConnectionStrings[ConnectionStringName];
        return settings?.ConnectionString
            ?? throw new InvalidOperationException(
                $"Connection string '{ConnectionStringName}' was not found in App.config.");
    }
}
