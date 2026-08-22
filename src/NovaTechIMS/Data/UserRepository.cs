using System;
using Npgsql;
using NpgsqlTypes;
using NovaTechIMS.Models;
using NovaTechIMS.Models.Enums;

namespace NovaTechIMS.Data;

/// <summary>
/// ADO.NET data access for User (Milestone 9 authentication).
/// </summary>
public class UserRepository
{
    public User? GetByUsername(string username)
    {
        const string sql = """
            SELECT "UserID", "Username", "PasswordHash", "PasswordSalt", "FullName",
                   "Role", "IsActive", "CreatedDate", "LastLoginDate"
            FROM "User"
            WHERE LOWER("Username") = LOWER(@username)
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("username", NpgsqlDbType.Varchar)
        {
            Value = username.Trim()
        });

        using var reader = cmd.ExecuteReader();
        if (!reader.Read())
            return null;

        return MapUser(reader);
    }

    public User? GetById(int userId)
    {
        const string sql = """
            SELECT "UserID", "Username", "PasswordHash", "PasswordSalt", "FullName",
                   "Role", "IsActive", "CreatedDate", "LastLoginDate"
            FROM "User"
            WHERE "UserID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = userId });

        using var reader = cmd.ExecuteReader();
        if (!reader.Read())
            return null;

        return MapUser(reader);
    }

    public int CountUsers()
    {
        const string sql = """SELECT COUNT(*)::int FROM "User"""";

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public int Insert(User user)
    {
        const string sql = """
            INSERT INTO "User"
                ("Username", "PasswordHash", "PasswordSalt", "FullName", "Role", "IsActive", "CreatedDate")
            VALUES
                (@username, @hash, @salt, @fullName, @role, @isActive, @created)
            RETURNING "UserID"
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("username", NpgsqlDbType.Varchar) { Value = user.Username.Trim() });
        cmd.Parameters.Add(new NpgsqlParameter("hash", NpgsqlDbType.Varchar) { Value = user.PasswordHash });
        cmd.Parameters.Add(new NpgsqlParameter("salt", NpgsqlDbType.Varchar) { Value = user.PasswordSalt });
        cmd.Parameters.Add(new NpgsqlParameter("fullName", NpgsqlDbType.Varchar) { Value = user.FullName.Trim() });
        cmd.Parameters.Add(new NpgsqlParameter("role", NpgsqlDbType.Varchar) { Value = RoleToDb(user.Role) });
        cmd.Parameters.Add(new NpgsqlParameter("isActive", NpgsqlDbType.Boolean) { Value = user.IsActive });
        cmd.Parameters.Add(new NpgsqlParameter("created", NpgsqlDbType.TimestampTz) { Value = user.CreatedDate });

        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public void UpdateLastLogin(int userId, DateTime lastLoginUtc)
    {
        const string sql = """
            UPDATE "User"
            SET "LastLoginDate" = @lastLogin
            WHERE "UserID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = userId });
        cmd.Parameters.Add(new NpgsqlParameter("lastLogin", NpgsqlDbType.TimestampTz) { Value = lastLoginUtc });
        cmd.ExecuteNonQuery();
    }

    private static User MapUser(NpgsqlDataReader reader)
    {
        return new User
        {
            UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
            Username = reader.GetString(reader.GetOrdinal("Username")),
            PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
            PasswordSalt = reader.GetString(reader.GetOrdinal("PasswordSalt")),
            FullName = reader.GetString(reader.GetOrdinal("FullName")),
            Role = ParseRole(reader.GetString(reader.GetOrdinal("Role"))),
            IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
            LastLoginDate = reader.IsDBNull(reader.GetOrdinal("LastLoginDate"))
                ? null
                : reader.GetDateTime(reader.GetOrdinal("LastLoginDate"))
        };
    }

    private static UserRole ParseRole(string role)
    {
        return role switch
        {
            "Administrator" => UserRole.Administrator,
            "InventoryStaff" => UserRole.InventoryStaff,
            _ => UserRole.InventoryStaff
        };
    }

    private static string RoleToDb(UserRole role)
        => role == UserRole.Administrator ? "Administrator" : "InventoryStaff";
}
