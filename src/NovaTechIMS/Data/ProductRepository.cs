using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;
using NovaTechIMS.Models;
using NovaTechIMS.Models.Enums;

namespace NovaTechIMS.Data;

/// <summary>
/// ADO.NET data access for Product (Milestone 8).
/// QuantityOnHand is never written from product edit paths — only INSERT default 0.
/// </summary>
public class ProductRepository
{
    public IReadOnlyList<ProductListRow> GetList(
        string? search,
        int? categoryId,
        int? supplierId,
        StockStatus? stockStatus,
        bool? isActiveFilter)
    {
        var list = new List<ProductListRow>();
        var whereParts = new List<string>();

        if (!string.IsNullOrWhiteSpace(search))
        {
            whereParts.Add(
                "(p.\"ProductName\" ILIKE '%' || @search || '%' OR CAST(p.\"ProductID\" AS TEXT) ILIKE '%' || @search || '%')");
        }

        if (categoryId is not null)
            whereParts.Add("p.\"CategoryID\" = @categoryId");

        if (supplierId is not null)
            whereParts.Add("p.\"SupplierID\" = @supplierId");

        if (isActiveFilter is not null)
            whereParts.Add("p.\"IsActive\" = @activeFilter");

        if (stockStatus is not null)
        {
            whereParts.Add(stockStatus.Value switch
            {
                StockStatus.OutOfStock => "p.\"QuantityOnHand\" = 0",
                StockStatus.LowStock =>
                    "p.\"QuantityOnHand\" > 0 AND p.\"QuantityOnHand\" <= p.\"MinimumStockLevel\"",
                StockStatus.InStock =>
                    "p.\"QuantityOnHand\" > p.\"MinimumStockLevel\"",
                _ => "TRUE"
            });
        }

        var whereSql = whereParts.Count == 0
            ? string.Empty
            : "WHERE " + string.Join(" AND ", whereParts);

        var sql = $"""
            SELECT p."ProductID",
                   p."ProductName",
                   p."CategoryID",
                   c."CategoryName",
                   p."SupplierID",
                   s."SupplierName",
                   p."Description",
                   p."PurchasePrice",
                   p."SellingPrice",
                   p."QuantityOnHand",
                   p."MinimumStockLevel",
                   p."IsActive",
                   p."CreatedDate",
                   p."ModifiedDate",
                   (SELECT COUNT(*)::int
                    FROM "InventoryTransaction" t
                    WHERE t."ProductID" = p."ProductID") AS "TransactionCount"
            FROM "Product" p
            INNER JOIN "Category" c ON c."CategoryID" = p."CategoryID"
            INNER JOIN "Supplier" s ON s."SupplierID" = p."SupplierID"
            {whereSql}
            ORDER BY p."ProductName"
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

        if (categoryId is not null)
        {
            cmd.Parameters.Add(new NpgsqlParameter("categoryId", NpgsqlDbType.Integer)
            {
                Value = categoryId.Value
            });
        }

        if (supplierId is not null)
        {
            cmd.Parameters.Add(new NpgsqlParameter("supplierId", NpgsqlDbType.Integer)
            {
                Value = supplierId.Value
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

    public Product? GetById(int productId)
    {
        const string sql = """
            SELECT "ProductID", "ProductName", "CategoryID", "SupplierID", "Description",
                   "PurchasePrice", "SellingPrice", "QuantityOnHand", "MinimumStockLevel",
                   "IsActive", "CreatedDate", "ModifiedDate",
                   "CreatedByUserID", "ModifiedByUserID"
            FROM "Product"
            WHERE "ProductID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = productId });

        using var reader = cmd.ExecuteReader();
        if (!reader.Read())
            return null;

        return MapProduct(reader);
    }

    public int CountTransactions(int productId)
    {
        const string sql = """
            SELECT COUNT(*)::int FROM "InventoryTransaction" WHERE "ProductID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = productId });
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public int Insert(Product product)
    {
        // QuantityOnHand always 0 on create (BR-026). No inventory transaction is created.
        const string sql = """
            INSERT INTO "Product"
                ("ProductName", "CategoryID", "SupplierID", "Description",
                 "PurchasePrice", "SellingPrice", "QuantityOnHand", "MinimumStockLevel",
                 "IsActive", "CreatedDate", "CreatedByUserID")
            VALUES
                (@name, @categoryId, @supplierId, @description,
                 @purchase, @selling, 0, @minStock,
                 @isActive, @created, @createdBy)
            RETURNING "ProductID"
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("name", NpgsqlDbType.Varchar) { Value = product.ProductName.Trim() });
        cmd.Parameters.Add(new NpgsqlParameter("categoryId", NpgsqlDbType.Integer) { Value = product.CategoryID });
        cmd.Parameters.Add(new NpgsqlParameter("supplierId", NpgsqlDbType.Integer) { Value = product.SupplierID });
        cmd.Parameters.Add(new NpgsqlParameter("description", NpgsqlDbType.Varchar)
        {
            Value = string.IsNullOrWhiteSpace(product.Description)
                ? DBNull.Value
                : product.Description.Trim()
        });
        cmd.Parameters.Add(new NpgsqlParameter("purchase", NpgsqlDbType.Numeric) { Value = product.PurchasePrice });
        cmd.Parameters.Add(new NpgsqlParameter("selling", NpgsqlDbType.Numeric) { Value = product.SellingPrice });
        cmd.Parameters.Add(new NpgsqlParameter("minStock", NpgsqlDbType.Integer) { Value = product.MinimumStockLevel });
        cmd.Parameters.Add(new NpgsqlParameter("isActive", NpgsqlDbType.Boolean) { Value = product.IsActive });
        cmd.Parameters.Add(new NpgsqlParameter("created", NpgsqlDbType.TimestampTz) { Value = product.CreatedDate });
        cmd.Parameters.Add(new NpgsqlParameter("createdBy", NpgsqlDbType.Integer)
        {
            Value = product.CreatedByUserID.HasValue ? product.CreatedByUserID.Value : DBNull.Value
        });

        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    /// <summary>
    /// Updates product master data. Does <b>not</b> touch QuantityOnHand (BR-027).
    /// </summary>
    public void Update(Product product)
    {
        const string sql = """
            UPDATE "Product"
            SET "ProductName" = @name,
                "CategoryID" = @categoryId,
                "SupplierID" = @supplierId,
                "Description" = @description,
                "PurchasePrice" = @purchase,
                "SellingPrice" = @selling,
                "MinimumStockLevel" = @minStock,
                "IsActive" = @isActive,
                "ModifiedDate" = @modified,
                "ModifiedByUserID" = @modifiedBy
            WHERE "ProductID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = product.ProductID });
        cmd.Parameters.Add(new NpgsqlParameter("name", NpgsqlDbType.Varchar) { Value = product.ProductName.Trim() });
        cmd.Parameters.Add(new NpgsqlParameter("categoryId", NpgsqlDbType.Integer) { Value = product.CategoryID });
        cmd.Parameters.Add(new NpgsqlParameter("supplierId", NpgsqlDbType.Integer) { Value = product.SupplierID });
        cmd.Parameters.Add(new NpgsqlParameter("description", NpgsqlDbType.Varchar)
        {
            Value = string.IsNullOrWhiteSpace(product.Description)
                ? DBNull.Value
                : product.Description.Trim()
        });
        cmd.Parameters.Add(new NpgsqlParameter("purchase", NpgsqlDbType.Numeric) { Value = product.PurchasePrice });
        cmd.Parameters.Add(new NpgsqlParameter("selling", NpgsqlDbType.Numeric) { Value = product.SellingPrice });
        cmd.Parameters.Add(new NpgsqlParameter("minStock", NpgsqlDbType.Integer) { Value = product.MinimumStockLevel });
        cmd.Parameters.Add(new NpgsqlParameter("isActive", NpgsqlDbType.Boolean) { Value = product.IsActive });
        cmd.Parameters.Add(new NpgsqlParameter("modified", NpgsqlDbType.TimestampTz)
        {
            Value = product.ModifiedDate ?? DateTime.UtcNow
        });
        cmd.Parameters.Add(new NpgsqlParameter("modifiedBy", NpgsqlDbType.Integer)
        {
            Value = product.ModifiedByUserID.HasValue ? product.ModifiedByUserID.Value : DBNull.Value
        });

        if (cmd.ExecuteNonQuery() == 0)
            throw new InvalidOperationException($"Product {product.ProductID} was not updated.");
    }

    public void SoftDeactivate(int productId)
    {
        const string sql = """
            UPDATE "Product"
            SET "IsActive" = FALSE,
                "ModifiedDate" = @modified
            WHERE "ProductID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = productId });
        cmd.Parameters.Add(new NpgsqlParameter("modified", NpgsqlDbType.TimestampTz) { Value = DateTime.UtcNow });
        cmd.ExecuteNonQuery();
    }

    public bool TryHardDelete(int productId)
    {
        const string sql = """
            DELETE FROM "Product" WHERE "ProductID" = @id
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = productId });

        try
        {
            return cmd.ExecuteNonQuery() > 0;
        }
        catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.ForeignKeyViolation)
        {
            return false;
        }
    }

    private static Product MapProduct(NpgsqlDataReader reader)
    {
        return new Product
        {
            ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
            ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
            CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
            SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID")),
            Description = reader.IsDBNull(reader.GetOrdinal("Description"))
                ? null
                : reader.GetString(reader.GetOrdinal("Description")),
            PurchasePrice = reader.GetDecimal(reader.GetOrdinal("PurchasePrice")),
            SellingPrice = reader.GetDecimal(reader.GetOrdinal("SellingPrice")),
            QuantityOnHand = reader.GetInt32(reader.GetOrdinal("QuantityOnHand")),
            MinimumStockLevel = reader.GetInt32(reader.GetOrdinal("MinimumStockLevel")),
            IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
            ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate"))
                ? null
                : reader.GetDateTime(reader.GetOrdinal("ModifiedDate")),
            CreatedByUserID = reader.IsDBNull(reader.GetOrdinal("CreatedByUserID"))
                ? null
                : reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
            ModifiedByUserID = reader.IsDBNull(reader.GetOrdinal("ModifiedByUserID"))
                ? null
                : reader.GetInt32(reader.GetOrdinal("ModifiedByUserID"))
        };
    }

    private static ProductListRow MapListRow(NpgsqlDataReader reader)
    {
        var qty = reader.GetInt32(reader.GetOrdinal("QuantityOnHand"));
        var min = reader.GetInt32(reader.GetOrdinal("MinimumStockLevel"));

        return new ProductListRow
        {
            ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
            ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
            CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
            CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
            SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID")),
            SupplierName = reader.GetString(reader.GetOrdinal("SupplierName")),
            Description = reader.IsDBNull(reader.GetOrdinal("Description"))
                ? null
                : reader.GetString(reader.GetOrdinal("Description")),
            PurchasePrice = reader.GetDecimal(reader.GetOrdinal("PurchasePrice")),
            SellingPrice = reader.GetDecimal(reader.GetOrdinal("SellingPrice")),
            QuantityOnHand = qty,
            MinimumStockLevel = min,
            IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
            ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate"))
                ? null
                : reader.GetDateTime(reader.GetOrdinal("ModifiedDate")),
            TransactionCount = reader.GetInt32(reader.GetOrdinal("TransactionCount"))
        };
    }
}

/// <summary>List row for the Product grid (joined names + derived status).</summary>
public class ProductListRow
{
    public int ProductID { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int CategoryID { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int SupplierID { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal SellingPrice { get; set; }
    public int QuantityOnHand { get; set; }
    public int MinimumStockLevel { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public int TransactionCount { get; set; }

    public string StatusLabel => IsActive ? "Active" : "Inactive";

    public string StockStatusLabel
    {
        get
        {
            if (QuantityOnHand <= 0) return "Out of Stock";
            if (QuantityOnHand <= MinimumStockLevel) return "Low Stock";
            return "In Stock";
        }
    }
}
