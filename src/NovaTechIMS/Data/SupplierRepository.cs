using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;
using NovaTechIMS.Models;

namespace NovaTechIMS.Data;

/// <summary>
/// ADO.NET data access for Supplier (Milestone 6).
/// Parameterized SQL only; maps readers manually.
/// </summary>
public class SupplierRepository
{
    public IReadOnlyList<SupplierListRow> GetList(string? searchName, bool? isActiveFilter)
    {
        var list = new List<SupplierListRow>();

        var whereParts = new List<string>();
        if (!string.IsNullOrWhiteSpace(searchName))
            whereParts.Add("s.\"SupplierName\" ILIKE '%' || @search || '%'");
        if (isActiveFilter is not null)
            whereParts.Add("s.\"IsActive\" = @activeFilter");

        var whereSql = whereParts.Count == 0
            ? string.Empty
            : "WHERE " + string.Join(" AND ", whereParts);

        var sql = $"""
            SELECT s."SupplierID",
                   s."SupplierName",
                   s."ContactPerson",
                   s."Phone",
                   s."Email",
                   s."Address",
                   s."IsActive",
                   s."CreatedDate",
                   s."ModifiedDate",
                   (SELECT COUNT(*)::int FROM "Product" p WHERE p."SupplierID" = s."SupplierID") AS "ProductCount",
                   (SELECT COUNT(*)::int FROM "InventoryTransaction" t WHERE t."SupplierID" = s."SupplierID") AS "TransactionCount"
            FROM "Supplier" s
            {whereSql}
            ORDER BY s."SupplierName"
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);

        if (!string.IsNullOrWhiteSpace(searchName))
        {
            cmd.Parameters.Add(new NpgsqlParameter("search", NpgsqlDbType.Text)
            {
                Value = searchName.Trim()
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

    public Supplier? GetById(int supplierId)
    {
        const string sql = """
            SELECT "SupplierID", "SupplierName", "ContactPerson", "Phone", "Email",
                   "Address", "IsActive", "CreatedDate", "ModifiedDate"
            FROM "Supplier"
            WHERE "SupplierID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = supplierId });

        using var reader = cmd.ExecuteReader();
        if (!reader.Read())
            return null;

        return MapSupplier(reader);
    }

    public int CountProducts(int supplierId)
    {
        const string sql = """
            SELECT COUNT(*)::int FROM "Product" WHERE "SupplierID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = supplierId });
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public int CountTransactions(int supplierId)
    {
        const string sql = """
            SELECT COUNT(*)::int FROM "InventoryTransaction" WHERE "SupplierID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = supplierId });
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public int Insert(Supplier supplier)
    {
        const string sql = """
            INSERT INTO "Supplier"
                ("SupplierName", "ContactPerson", "Phone", "Email", "Address", "IsActive", "CreatedDate")
            VALUES
                (@name, @contact, @phone, @email, @address, @isActive, @created)
            RETURNING "SupplierID"
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        AddWriteParameters(cmd, supplier);
        cmd.Parameters.Add(new NpgsqlParameter("created", NpgsqlDbType.TimestampTz) { Value = supplier.CreatedDate });

        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public void Update(Supplier supplier)
    {
        const string sql = """
            UPDATE "Supplier"
            SET "SupplierName" = @name,
                "ContactPerson" = @contact,
                "Phone" = @phone,
                "Email" = @email,
                "Address" = @address,
                "IsActive" = @isActive,
                "ModifiedDate" = @modified
            WHERE "SupplierID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = supplier.SupplierID });
        AddWriteParameters(cmd, supplier);
        cmd.Parameters.Add(new NpgsqlParameter("modified", NpgsqlDbType.TimestampTz)
        {
            Value = supplier.ModifiedDate ?? DateTime.UtcNow
        });

        if (cmd.ExecuteNonQuery() == 0)
            throw new InvalidOperationException($"Supplier {supplier.SupplierID} was not updated.");
    }

    public void SoftDeactivate(int supplierId)
    {
        const string sql = """
            UPDATE "Supplier"
            SET "IsActive" = FALSE,
                "ModifiedDate" = @modified
            WHERE "SupplierID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = supplierId });
        cmd.Parameters.Add(new NpgsqlParameter("modified", NpgsqlDbType.TimestampTz) { Value = DateTime.UtcNow });
        cmd.ExecuteNonQuery();
    }

    public bool TryHardDelete(int supplierId)
    {
        const string sql = """
            DELETE FROM "Supplier" WHERE "SupplierID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = supplierId });

        try
        {
            return cmd.ExecuteNonQuery() > 0;
        }
        catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.ForeignKeyViolation)
        {
            return false;
        }
    }

    private static void AddWriteParameters(NpgsqlCommand cmd, Supplier supplier)
    {
        cmd.Parameters.Add(new NpgsqlParameter("name", NpgsqlDbType.Varchar)
        {
            Value = supplier.SupplierName.Trim()
        });
        cmd.Parameters.Add(new NpgsqlParameter("contact", NpgsqlDbType.Varchar)
        {
            Value = string.IsNullOrWhiteSpace(supplier.ContactPerson)
                ? DBNull.Value
                : supplier.ContactPerson.Trim()
        });
        cmd.Parameters.Add(new NpgsqlParameter("phone", NpgsqlDbType.Varchar)
        {
            Value = string.IsNullOrWhiteSpace(supplier.Phone)
                ? DBNull.Value
                : supplier.Phone.Trim()
        });
        cmd.Parameters.Add(new NpgsqlParameter("email", NpgsqlDbType.Varchar)
        {
            Value = string.IsNullOrWhiteSpace(supplier.Email)
                ? DBNull.Value
                : supplier.Email.Trim()
        });
        cmd.Parameters.Add(new NpgsqlParameter("address", NpgsqlDbType.Varchar)
        {
            Value = string.IsNullOrWhiteSpace(supplier.Address)
                ? DBNull.Value
                : supplier.Address.Trim()
        });
        cmd.Parameters.Add(new NpgsqlParameter("isActive", NpgsqlDbType.Boolean)
        {
            Value = supplier.IsActive
        });
    }

    private static Supplier MapSupplier(NpgsqlDataReader reader)
    {
        return new Supplier
        {
            SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID")),
            SupplierName = reader.GetString(reader.GetOrdinal("SupplierName")),
            ContactPerson = ReadNullableString(reader, "ContactPerson"),
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

    private static SupplierListRow MapListRow(NpgsqlDataReader reader)
    {
        return new SupplierListRow
        {
            SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID")),
            SupplierName = reader.GetString(reader.GetOrdinal("SupplierName")),
            ContactPerson = ReadNullableString(reader, "ContactPerson"),
            Phone = ReadNullableString(reader, "Phone"),
            Email = ReadNullableString(reader, "Email"),
            Address = ReadNullableString(reader, "Address"),
            IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
            ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate"))
                ? null
                : reader.GetDateTime(reader.GetOrdinal("ModifiedDate")),
            ProductCount = reader.GetInt32(reader.GetOrdinal("ProductCount")),
            TransactionCount = reader.GetInt32(reader.GetOrdinal("TransactionCount"))
        };
    }

    private static string? ReadNullableString(NpgsqlDataReader reader, string column)
    {
        var ord = reader.GetOrdinal(column);
        return reader.IsDBNull(ord) ? null : reader.GetString(ord);
    }
}

/// <summary>List row for the Supplier grid (includes reference counts).</summary>
public class SupplierListRow
{
    public int SupplierID { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public string? ContactPerson { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public int ProductCount { get; set; }
    public int TransactionCount { get; set; }
    public string Status => IsActive ? "Active" : "Inactive";
}
