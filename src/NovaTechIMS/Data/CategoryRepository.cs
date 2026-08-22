using System;
using System.Collections.Generic;
using Npgsql;
using NovaTechIMS.Models;

namespace NovaTechIMS.Data;

/// <summary>
/// ADO.NET data access for Category (Milestone 5).
/// Parameterized SQL only; maps readers manually.
/// </summary>
public class CategoryRepository
{
    public IReadOnlyList<CategoryListRow> GetList(string? searchName, bool? isActiveFilter)
    {
        var list = new List<CategoryListRow>();

        const string sql = """
            SELECT c."CategoryID",
                   c."CategoryName",
                   c."Description",
                   c."IsActive",
                   c."CreatedDate",
                   c."ModifiedDate",
                   COUNT(p."ProductID")::int AS "ProductCount"
            FROM "Category" c
            LEFT JOIN "Product" p ON p."CategoryID" = c."CategoryID"
            WHERE (@search IS NULL OR c."CategoryName" ILIKE '%' || @search || '%')
              AND (@activeFilter IS NULL OR c."IsActive" = @activeFilter)
            GROUP BY c."CategoryID", c."CategoryName", c."Description",
                     c."IsActive", c."CreatedDate", c."ModifiedDate"
            ORDER BY c."CategoryName"
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);

        if (string.IsNullOrWhiteSpace(searchName))
            cmd.Parameters.AddWithValue("search", DBNull.Value);
        else
            cmd.Parameters.AddWithValue("search", searchName.Trim());

        if (isActiveFilter is null)
            cmd.Parameters.AddWithValue("activeFilter", DBNull.Value);
        else
            cmd.Parameters.AddWithValue("activeFilter", isActiveFilter.Value);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(MapListRow(reader));
        }

        return list;
    }

    public Category? GetById(int categoryId)
    {
        const string sql = """
            SELECT "CategoryID", "CategoryName", "Description", "IsActive",
                   "CreatedDate", "ModifiedDate"
            FROM "Category"
            WHERE "CategoryID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("id", categoryId);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read())
            return null;

        return MapCategory(reader);
    }

    public bool ExistsByName(string categoryName, int? excludeCategoryId = null)
    {
        const string sql = """
            SELECT 1
            FROM "Category"
            WHERE LOWER(TRIM("CategoryName")) = LOWER(TRIM(@name))
              AND (@excludeId IS NULL OR "CategoryID" <> @excludeId)
            LIMIT 1
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("name", categoryName);
        if (excludeCategoryId is null)
            cmd.Parameters.AddWithValue("excludeId", DBNull.Value);
        else
            cmd.Parameters.AddWithValue("excludeId", excludeCategoryId.Value);

        return cmd.ExecuteScalar() is not null;
    }

    public int CountProducts(int categoryId)
    {
        const string sql = """
            SELECT COUNT(*)::int
            FROM "Product"
            WHERE "CategoryID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("id", categoryId);
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public int Insert(Category category)
    {
        const string sql = """
            INSERT INTO "Category" ("CategoryName", "Description", "IsActive", "CreatedDate")
            VALUES (@name, @description, @isActive, @created)
            RETURNING "CategoryID"
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("name", category.CategoryName.Trim());
        cmd.Parameters.AddWithValue("description",
            (object?)category.Description?.Trim() ?? DBNull.Value);
        cmd.Parameters.AddWithValue("isActive", category.IsActive);
        cmd.Parameters.AddWithValue("created", category.CreatedDate);

        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public void Update(Category category)
    {
        const string sql = """
            UPDATE "Category"
            SET "CategoryName" = @name,
                "Description" = @description,
                "IsActive" = @isActive,
                "ModifiedDate" = @modified
            WHERE "CategoryID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("id", category.CategoryID);
        cmd.Parameters.AddWithValue("name", category.CategoryName.Trim());
        cmd.Parameters.AddWithValue("description",
            (object?)category.Description?.Trim() ?? DBNull.Value);
        cmd.Parameters.AddWithValue("isActive", category.IsActive);
        cmd.Parameters.AddWithValue("modified", category.ModifiedDate ?? DateTime.UtcNow);

        var rows = cmd.ExecuteNonQuery();
        if (rows == 0)
            throw new InvalidOperationException($"Category {category.CategoryID} was not updated.");
    }

    public void SoftDeactivate(int categoryId)
    {
        const string sql = """
            UPDATE "Category"
            SET "IsActive" = FALSE,
                "ModifiedDate" = @modified
            WHERE "CategoryID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("id", categoryId);
        cmd.Parameters.AddWithValue("modified", DateTime.UtcNow);
        cmd.ExecuteNonQuery();
    }

    public bool TryHardDelete(int categoryId)
    {
        const string sql = """
            DELETE FROM "Category"
            WHERE "CategoryID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("id", categoryId);

        try
        {
            return cmd.ExecuteNonQuery() > 0;
        }
        catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.ForeignKeyViolation)
        {
            return false;
        }
    }

    private static Category MapCategory(NpgsqlDataReader reader)
    {
        return new Category
        {
            CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
            CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
            Description = reader.IsDBNull(reader.GetOrdinal("Description"))
                ? null
                : reader.GetString(reader.GetOrdinal("Description")),
            IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
            ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate"))
                ? null
                : reader.GetDateTime(reader.GetOrdinal("ModifiedDate"))
        };
    }

    private static CategoryListRow MapListRow(NpgsqlDataReader reader)
    {
        return new CategoryListRow
        {
            CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
            CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
            Description = reader.IsDBNull(reader.GetOrdinal("Description"))
                ? null
                : reader.GetString(reader.GetOrdinal("Description")),
            IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
            ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate"))
                ? null
                : reader.GetDateTime(reader.GetOrdinal("ModifiedDate")),
            ProductCount = reader.GetInt32(reader.GetOrdinal("ProductCount"))
        };
    }
}

/// <summary>List row including product count for the Category grid.</summary>
public class CategoryListRow
{
    public int CategoryID { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public int ProductCount { get; set; }
    public string Status => IsActive ? "Active" : "Inactive";
}
