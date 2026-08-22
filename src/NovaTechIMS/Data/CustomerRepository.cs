using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;
using NovaTechIMS.Models;

namespace NovaTechIMS.Data;

/// <summary>
/// ADO.NET data access for Customer (Milestone 7).
/// Parameterized SQL only; maps readers manually.
/// </summary>
public class CustomerRepository
{
    public IReadOnlyList<CustomerListRow> GetList(string? search, bool? isActiveFilter)
    {
        var list = new List<CustomerListRow>();

        var whereParts = new List<string>();
        if (!string.IsNullOrWhiteSpace(search))
        {
            whereParts.Add(
                "(c.\"CustomerName\" ILIKE '%' || @search || '%' OR COALESCE(c.\"Phone\", '') ILIKE '%' || @search || '%')");
        }
        if (isActiveFilter is not null)
            whereParts.Add("c.\"IsActive\" = @activeFilter");

        var whereSql = whereParts.Count == 0
            ? string.Empty
            : "WHERE " + string.Join(" AND ", whereParts);

        var sql = $"""
            SELECT c."CustomerID",
                   c."CustomerName",
                   c."Phone",
                   c."Email",
                   c."Address",
                   c."IsActive",
                   c."CreatedDate",
                   c."ModifiedDate",
                   (SELECT COUNT(*)::int
                    FROM "InventoryTransaction" t
                    WHERE t."CustomerID" = c."CustomerID") AS "TransactionCount"
            FROM "Customer" c
            {whereSql}
            ORDER BY c."CustomerName"
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

    public Customer? GetById(int customerId)
    {
        const string sql = """
            SELECT "CustomerID", "CustomerName", "Phone", "Email", "Address",
                   "IsActive", "CreatedDate", "ModifiedDate"
            FROM "Customer"
            WHERE "CustomerID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = customerId });

        using var reader = cmd.ExecuteReader();
        if (!reader.Read())
            return null;

        return MapCustomer(reader);
    }

    public int CountStockOutTransactions(int customerId)
    {
        const string sql = """
            SELECT COUNT(*)::int
            FROM "InventoryTransaction"
            WHERE "CustomerID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = customerId });
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public int Insert(Customer customer)
    {
        const string sql = """
            INSERT INTO "Customer"
                ("CustomerName", "Phone", "Email", "Address", "IsActive", "CreatedDate")
            VALUES
                (@name, @phone, @email, @address, @isActive, @created)
            RETURNING "CustomerID"
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        AddWriteParameters(cmd, customer);
        cmd.Parameters.Add(new NpgsqlParameter("created", NpgsqlDbType.TimestampTz)
        {
            Value = customer.CreatedDate
        });

        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public void Update(Customer customer)
    {
        const string sql = """
            UPDATE "Customer"
            SET "CustomerName" = @name,
                "Phone" = @phone,
                "Email" = @email,
                "Address" = @address,
                "IsActive" = @isActive,
                "ModifiedDate" = @modified
            WHERE "CustomerID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = customer.CustomerID });
        AddWriteParameters(cmd, customer);
        cmd.Parameters.Add(new NpgsqlParameter("modified", NpgsqlDbType.TimestampTz)
        {
            Value = customer.ModifiedDate ?? DateTime.UtcNow
        });

        if (cmd.ExecuteNonQuery() == 0)
            throw new InvalidOperationException($"Customer {customer.CustomerID} was not updated.");
    }

    public void SoftDeactivate(int customerId)
    {
        const string sql = """
            UPDATE "Customer"
            SET "IsActive" = FALSE,
                "ModifiedDate" = @modified
            WHERE "CustomerID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = customerId });
        cmd.Parameters.Add(new NpgsqlParameter("modified", NpgsqlDbType.TimestampTz) { Value = DateTime.UtcNow });
        cmd.ExecuteNonQuery();
    }

    public bool TryHardDelete(int customerId)
    {
        const string sql = """
            DELETE FROM "Customer" WHERE "CustomerID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = customerId });

        try
        {
            return cmd.ExecuteNonQuery() > 0;
        }
        catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.ForeignKeyViolation)
        {
            return false;
        }
    }

    private static void AddWriteParameters(NpgsqlCommand cmd, Customer customer)
    {
        cmd.Parameters.Add(new NpgsqlParameter("name", NpgsqlDbType.Varchar)
        {
            Value = customer.CustomerName.Trim()
        });
        cmd.Parameters.Add(new NpgsqlParameter("phone", NpgsqlDbType.Varchar)
        {
            Value = string.IsNullOrWhiteSpace(customer.Phone)
                ? DBNull.Value
                : customer.Phone.Trim()
        });
        cmd.Parameters.Add(new NpgsqlParameter("email", NpgsqlDbType.Varchar)
        {
            Value = string.IsNullOrWhiteSpace(customer.Email)
                ? DBNull.Value
                : customer.Email.Trim()
        });
        cmd.Parameters.Add(new NpgsqlParameter("address", NpgsqlDbType.Varchar)
        {
            Value = string.IsNullOrWhiteSpace(customer.Address)
                ? DBNull.Value
                : customer.Address.Trim()
        });
        cmd.Parameters.Add(new NpgsqlParameter("isActive", NpgsqlDbType.Boolean)
        {
            Value = customer.IsActive
        });
    }

    private static Customer MapCustomer(NpgsqlDataReader reader)
    {
        return new Customer
        {
            CustomerID = reader.GetInt32(reader.GetOrdinal("CustomerID")),
            CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
            Phone = ReadNullableString(reader, "Phone"),
            Email = ReadNullableString(reader, "Email"),
            Address = ReadNullableString(reader, "Address"),
            IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
            ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate"))
                ? null
                : reader.GetDateTime(reader.GetOrdinal("ModifiedDate"))
        };
    }

    private static CustomerListRow MapListRow(NpgsqlDataReader reader)
    {
        return new CustomerListRow
        {
            CustomerID = reader.GetInt32(reader.GetOrdinal("CustomerID")),
            CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
            Phone = ReadNullableString(reader, "Phone"),
            Email = ReadNullableString(reader, "Email"),
            Address = ReadNullableString(reader, "Address"),
            IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
            ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate"))
                ? null
                : reader.GetDateTime(reader.GetOrdinal("ModifiedDate")),
            TransactionCount = reader.GetInt32(reader.GetOrdinal("TransactionCount"))
        };
    }

    private static string? ReadNullableString(NpgsqlDataReader reader, string column)
    {
        var ord = reader.GetOrdinal(column);
        return reader.IsDBNull(ord) ? null : reader.GetString(ord);
    }
}

/// <summary>List row for the Customer grid.</summary>
public class CustomerListRow
{
    public int CustomerID { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public int TransactionCount { get; set; }
    public string Status => IsActive ? "Active" : "Inactive";
}
