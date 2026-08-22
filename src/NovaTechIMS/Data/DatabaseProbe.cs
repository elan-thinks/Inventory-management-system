using System;
using Npgsql;

namespace NovaTechIMS.Data;

/// <summary>
/// Minimal connectivity check for Milestone 4 verification.
/// Not used by business features.
/// </summary>
public static class DatabaseProbe
{
    /// <summary>
    /// Opens a connection and runs SELECT 1. Returns true on success.
    /// </summary>
    public static bool TryConnect(out string message)
    {
        try
        {
            using var conn = DbConnectionFactory.CreateConnection();
            conn.Open();
            using var cmd = new NpgsqlCommand("SELECT 1", conn);
            var result = cmd.ExecuteScalar();
            message = result is int or long
                ? "PostgreSQL connection OK (SELECT 1 succeeded)."
                : "PostgreSQL connection opened.";
            return true;
        }
        catch (Exception ex)
        {
            message = "PostgreSQL connection failed: " + ex.Message;
            return false;
        }
    }
}
