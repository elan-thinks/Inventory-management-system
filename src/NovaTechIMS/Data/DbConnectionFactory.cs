using System;
using System.Configuration;
using Npgsql;

namespace NovaTechIMS.Data;

/// <summary>
/// Creates PostgreSQL connections (Milestone 4).
/// Password from environment variable NOVATECH_PG_PASSWORD
/// (or full string from NOVATECH_IMS_CONNECTION).
/// </summary>
public static class DbConnectionFactory
{
    public const string ConnectionStringName = "NovaTechIMS";
    public const string PasswordEnvironmentVariable = "NOVATECH_PG_PASSWORD";
    public const string FullConnectionEnvironmentVariable = "NOVATECH_IMS_CONNECTION";

    public static NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(BuildConnectionString());
    }

    public static string BuildConnectionString()
    {
        var fullOverride = Environment.GetEnvironmentVariable(
            FullConnectionEnvironmentVariable,
            EnvironmentVariableTarget.Process)
            ?? Environment.GetEnvironmentVariable(
                FullConnectionEnvironmentVariable,
                EnvironmentVariableTarget.User)
            ?? Environment.GetEnvironmentVariable(
                FullConnectionEnvironmentVariable,
                EnvironmentVariableTarget.Machine);

        if (!string.IsNullOrWhiteSpace(fullOverride))
            return fullOverride.Trim();

        var settings = ConfigurationManager.ConnectionStrings[ConnectionStringName]
            ?? throw new InvalidOperationException(
                $"Connection string '{ConnectionStringName}' was not found in App.config.");

        if (string.IsNullOrWhiteSpace(settings.ConnectionString))
            throw new InvalidOperationException(
                $"Connection string '{ConnectionStringName}' is empty in App.config.");

        var builder = new NpgsqlConnectionStringBuilder(settings.ConnectionString);

        if (string.IsNullOrEmpty(builder.Password))
        {
            var password = Environment.GetEnvironmentVariable(
                    PasswordEnvironmentVariable,
                    EnvironmentVariableTarget.Process)
                ?? Environment.GetEnvironmentVariable(
                    PasswordEnvironmentVariable,
                    EnvironmentVariableTarget.User)
                ?? Environment.GetEnvironmentVariable(
                    PasswordEnvironmentVariable,
                    EnvironmentVariableTarget.Machine);

            if (string.IsNullOrEmpty(password))
            {
                throw new InvalidOperationException(
                    $"PostgreSQL password not set. Create a user environment variable " +
                    $"named '{PasswordEnvironmentVariable}' with your postgres password, " +
                    $"then restart Visual Studio. " +
                    $"(Alternatively set '{FullConnectionEnvironmentVariable}' to a full connection string.)");
            }

            builder.Password = password;
        }

        return builder.ConnectionString;
    }

    public static string GetConnectionString() => BuildConnectionString();
}
