using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;
using NovaTechIMS.Models;
using NovaTechIMS.Models.Enums;

namespace NovaTechIMS.Data;

/// <summary>
/// ADO.NET data access for User (authentication + user management).
/// </summary>
public class UserRepository
{
    public IReadOnlyList<UserListRow> GetList(string? search, bool? isActiveFilter)
    {
        var list = new List<UserListRow>();
        var whereParts = new List<string>();

        if (!string.IsNullOrWhiteSpace(search))
        {
            whereParts.Add(
                "(u.\"Username\" ILIKE '%' || @search || '%' OR u.\"FullName\" ILIKE '%' || @search || '%')");
        }

        if (isActiveFilter is not null)
            whereParts.Add("u.\"IsActive\" = @activeFilter");

        var whereSql = whereParts.Count == 0
            ? string.Empty
            : "WHERE " + string.Join(" AND ", whereParts);

        var sql = $"""
            SELECT u."UserID", u."Username", u."FullName", u."Role", u."IsActive",
                   u."CreatedDate", u."LastLoginDate"
            FROM "User" u
            {whereSql}
            ORDER BY u."Username"
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);

        if (!string.IsNullOrWhiteSpace(search))
        {
            cmd.Parameters.Add(new NpgsqlParameter("search", NpgsqlDbType.Text)
            {
                Value = search.Trim()
            });
        }

        if (isActiveFilter is not null)
        {
            cmd.Parameters.Add(new NpgsqlParameter("activeFilter", NpgsqlDbType.Boolean)
            {
                Value = isActiveFilter.Value
            });
        }

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            list.Add(MapListRow(reader));

        return list;
    }

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
        const string sql = "SELECT COUNT(*)::int FROM \"User\"";

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

    public void UpdateProfile(User user)
    {
        const string sql = """
            UPDATE "User"
            SET "FullName" = @fullName,
                "Role" = @role,
                "IsActive" = @isActive
            WHERE "UserID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = user.UserID });
        cmd.Parameters.Add(new NpgsqlParameter("fullName", NpgsqlDbType.Varchar) { Value = user.FullName.Trim() });
        cmd.Parameters.Add(new NpgsqlParameter("role", NpgsqlDbType.Varchar) { Value = RoleToDb(user.Role) });
        cmd.Parameters.Add(new NpgsqlParameter("isActive", NpgsqlDbType.Boolean) { Value = user.IsActive });

        if (cmd.ExecuteNonQuery() == 0)
            throw new InvalidOperationException($"User {user.UserID} was not updated.");
    }

    public void UpdateWithPassword(User user)
    {
        const string sql = """
            UPDATE "User"
            SET "FullName" = @fullName,
                "Role" = @role,
                "IsActive" = @isActive,
                "PasswordHash" = @hash,
                "PasswordSalt" = @salt
            WHERE "UserID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = user.UserID });
        cmd.Parameters.Add(new NpgsqlParameter("fullName", NpgsqlDbType.Varchar) { Value = user.FullName.Trim() });
        cmd.Parameters.Add(new NpgsqlParameter("role", NpgsqlDbType.Varchar) { Value = RoleToDb(user.Role) });
        cmd.Parameters.Add(new NpgsqlParameter("isActive", NpgsqlDbType.Boolean) { Value = user.IsActive });
        cmd.Parameters.Add(new NpgsqlParameter("hash", NpgsqlDbType.Varchar) { Value = user.PasswordHash });
        cmd.Parameters.Add(new NpgsqlParameter("salt", NpgsqlDbType.Varchar) { Value = user.PasswordSalt });

        if (cmd.ExecuteNonQuery() == 0)
            throw new InvalidOperationException($"User {user.UserID} was not updated.");
    }

    public void SoftDeactivate(int userId)
    {
        const string sql = """
            UPDATE "User" SET "IsActive" = FALSE WHERE "UserID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = userId });
        cmd.ExecuteNonQuery();
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

    private static UserListRow MapListRow(NpgsqlDataReader reader)
    {
        return new UserListRow
        {
            UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
            Username = reader.GetString(reader.GetOrdinal("Username")),
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
        => role == "Administrator" ? UserRole.Administrator : UserRole.InventoryStaff;

    private static string RoleToDb(UserRole role)
        => role == UserRole.Administrator ? "Administrator" : "InventoryStaff";
}

/// <summary>List row for User Management grid (no password fields).</summary>
public class UserListRow
{
    public int UserID { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastLoginDate { get; set; }

    public string RoleLabel => Role == UserRole.Administrator ? "Administrator" : "Inventory Staff";
    public string Status => IsActive ? "Active" : "Inactive";
}
