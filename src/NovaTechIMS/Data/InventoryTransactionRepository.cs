using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;
using NovaTechIMS.Models;
using NovaTechIMS.Models.Enums;

namespace NovaTechIMS.Data;

/// <summary>
/// Append-only inventory transactions (Milestone 11+).
/// Stock-In inserts a row and updates Product.QuantityOnHand in one DB transaction.
/// </summary>
public class InventoryTransactionRepository
{
    /// <summary>
    /// Records Stock-In and increases QuantityOnHand. Returns new TransactionID.
    /// </summary>
    public int InsertStockIn(
        int productId,
        int quantity,
        DateTime transactionDate,
        decimal unitPrice,
        int supplierId,
        string? notes,
        int userId)
    {
        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var tx = conn.BeginTransaction();

        try
        {
            // Lock product row and verify active + current qty
            const string lockSql = """
                SELECT "QuantityOnHand", "IsActive"
                FROM "Product"
                WHERE "ProductID" = @productId
                FOR UPDATE
                """;

            int currentQty;
            bool isActive;
            using (var lockCmd = new NpgsqlCommand(lockSql, conn, tx))
            {
                lockCmd.Parameters.Add(new NpgsqlParameter("productId", NpgsqlDbType.Integer) { Value = productId });
                using var reader = lockCmd.ExecuteReader();
                if (!reader.Read())
                    throw new InvalidOperationException("Product not found.");

                currentQty = reader.GetInt32(0);
                isActive = reader.GetBoolean(1);
            }

            if (!isActive)
                throw new InvalidOperationException("Product is inactive.");

            var newQty = currentQty + quantity;

            const string insertSql = """
                INSERT INTO "InventoryTransaction"
                    ("TransactionType", "ProductID", "Quantity", "TransactionDate", "UnitPrice",
                     "SupplierID", "Notes", "UserID", "CreatedDateTime",
                     "PreviousQuantity", "NewQuantity", "Difference")
                VALUES
                    ('StockIn', @productId, @quantity, @txnDate, @unitPrice,
                     @supplierId, @notes, @userId, @created,
                     @prevQty, @newQty, @diff)
                RETURNING "TransactionID"
                """;

            int txnId;
            using (var insertCmd = new NpgsqlCommand(insertSql, conn, tx))
            {
                insertCmd.Parameters.Add(new NpgsqlParameter("productId", NpgsqlDbType.Integer) { Value = productId });
                insertCmd.Parameters.Add(new NpgsqlParameter("quantity", NpgsqlDbType.Integer) { Value = quantity });
                insertCmd.Parameters.Add(new NpgsqlParameter("txnDate", NpgsqlDbType.Date) { Value = transactionDate.Date });
                insertCmd.Parameters.Add(new NpgsqlParameter("unitPrice", NpgsqlDbType.Numeric) { Value = unitPrice });
                insertCmd.Parameters.Add(new NpgsqlParameter("supplierId", NpgsqlDbType.Integer) { Value = supplierId });
                insertCmd.Parameters.Add(new NpgsqlParameter("notes", NpgsqlDbType.Varchar)
                {
                    Value = string.IsNullOrWhiteSpace(notes) ? DBNull.Value : notes.Trim()
                });
                insertCmd.Parameters.Add(new NpgsqlParameter("userId", NpgsqlDbType.Integer) { Value = userId });
                insertCmd.Parameters.Add(new NpgsqlParameter("created", NpgsqlDbType.TimestampTz) { Value = DateTime.UtcNow });
                insertCmd.Parameters.Add(new NpgsqlParameter("prevQty", NpgsqlDbType.Integer) { Value = currentQty });
                insertCmd.Parameters.Add(new NpgsqlParameter("newQty", NpgsqlDbType.Integer) { Value = newQty });
                insertCmd.Parameters.Add(new NpgsqlParameter("diff", NpgsqlDbType.Integer) { Value = quantity });

                txnId = Convert.ToInt32(insertCmd.ExecuteScalar());
            }

            const string updateSql = """
                UPDATE "Product"
                SET "QuantityOnHand" = @newQty,
                    "ModifiedDate" = @modified
                WHERE "ProductID" = @productId
                """;

            using (var updateCmd = new NpgsqlCommand(updateSql, conn, tx))
            {
                updateCmd.Parameters.Add(new NpgsqlParameter("newQty", NpgsqlDbType.Integer) { Value = newQty });
                updateCmd.Parameters.Add(new NpgsqlParameter("modified", NpgsqlDbType.TimestampTz) { Value = DateTime.UtcNow });
                updateCmd.Parameters.Add(new NpgsqlParameter("productId", NpgsqlDbType.Integer) { Value = productId });
                updateCmd.ExecuteNonQuery();
            }

            tx.Commit();
            return txnId;
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    public IReadOnlyList<StockInListRow> GetRecentStockIns(int limit = 50)
    {
        var list = new List<StockInListRow>();

        const string sql = """
            SELECT t."TransactionID",
                   t."ProductID",
                   p."ProductName",
                   t."Quantity",
                   t."TransactionDate",
                   t."UnitPrice",
                   t."SupplierID",
                   s."SupplierName",
                   t."Notes",
                   t."UserID",
                   u."FullName" AS "UserFullName",
                   t."CreatedDateTime",
                   t."PreviousQuantity",
                   t."NewQuantity"
            FROM "InventoryTransaction" t
            INNER JOIN "Product" p ON p."ProductID" = t."ProductID"
            LEFT JOIN "Supplier" s ON s."SupplierID" = t."SupplierID"
            INNER JOIN "User" u ON u."UserID" = t."UserID"
            WHERE t."TransactionType" = 'StockIn'
            ORDER BY t."CreatedDateTime" DESC
            LIMIT @limit
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("limit", NpgsqlDbType.Integer) { Value = limit });

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new StockInListRow
            {
                TransactionID = reader.GetInt32(reader.GetOrdinal("TransactionID")),
                ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                TransactionDate = reader.GetDateTime(reader.GetOrdinal("TransactionDate")),
                UnitPrice = reader.IsDBNull(reader.GetOrdinal("UnitPrice"))
                    ? null
                    : reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                SupplierID = reader.IsDBNull(reader.GetOrdinal("SupplierID"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("SupplierID")),
                SupplierName = reader.IsDBNull(reader.GetOrdinal("SupplierName"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("SupplierName")),
                Notes = reader.IsDBNull(reader.GetOrdinal("Notes"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("Notes")),
                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                UserFullName = reader.GetString(reader.GetOrdinal("UserFullName")),
                CreatedDateTime = reader.GetDateTime(reader.GetOrdinal("CreatedDateTime")),
                PreviousQuantity = reader.IsDBNull(reader.GetOrdinal("PreviousQuantity"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("PreviousQuantity")),
                NewQuantity = reader.IsDBNull(reader.GetOrdinal("NewQuantity"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("NewQuantity"))
            });
        }

        return list;
    }
}

public class StockInListRow
{
    public int TransactionID { get; set; }
    public int ProductID { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal? UnitPrice { get; set; }
    public int? SupplierID { get; set; }
    public string? SupplierName { get; set; }
    public string? Notes { get; set; }
    public int UserID { get; set; }
    public string UserFullName { get; set; } = string.Empty;
    public DateTime CreatedDateTime { get; set; }
    public int? PreviousQuantity { get; set; }
    public int? NewQuantity { get; set; }
}
