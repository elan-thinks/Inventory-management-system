using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;

namespace NovaTechIMS.Data;

/// <summary>
/// Append-only inventory transactions (Milestones 11–13).
/// </summary>
public class InventoryTransactionRepository
{
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
            var (currentQty, isActive) = LockProduct(conn, tx, productId);
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

            UpdateQuantity(conn, tx, productId, newQty);
            tx.Commit();
            return txnId;
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    public int InsertStockOut(
        int productId,
        int quantity,
        DateTime transactionDate,
        decimal unitPrice,
        int? customerId,
        string? notes,
        int userId)
    {
        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var tx = conn.BeginTransaction();

        try
        {
            var (currentQty, isActive) = LockProduct(conn, tx, productId);
            if (!isActive)
                throw new InvalidOperationException("Product is inactive.");

            if (quantity > currentQty)
                throw new InvalidOperationException("Insufficient stock.");

            var newQty = currentQty - quantity;

            const string insertSql = """
                INSERT INTO "InventoryTransaction"
                    ("TransactionType", "ProductID", "Quantity", "TransactionDate", "UnitPrice",
                     "CustomerID", "Notes", "UserID", "CreatedDateTime",
                     "PreviousQuantity", "NewQuantity", "Difference")
                VALUES
                    ('StockOut', @productId, @quantity, @txnDate, @unitPrice,
                     @customerId, @notes, @userId, @created,
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
                insertCmd.Parameters.Add(new NpgsqlParameter("customerId", NpgsqlDbType.Integer)
                {
                    Value = customerId.HasValue ? customerId.Value : DBNull.Value
                });
                insertCmd.Parameters.Add(new NpgsqlParameter("notes", NpgsqlDbType.Varchar)
                {
                    Value = string.IsNullOrWhiteSpace(notes) ? DBNull.Value : notes.Trim()
                });
                insertCmd.Parameters.Add(new NpgsqlParameter("userId", NpgsqlDbType.Integer) { Value = userId });
                insertCmd.Parameters.Add(new NpgsqlParameter("created", NpgsqlDbType.TimestampTz) { Value = DateTime.UtcNow });
                insertCmd.Parameters.Add(new NpgsqlParameter("prevQty", NpgsqlDbType.Integer) { Value = currentQty });
                insertCmd.Parameters.Add(new NpgsqlParameter("newQty", NpgsqlDbType.Integer) { Value = newQty });
                insertCmd.Parameters.Add(new NpgsqlParameter("diff", NpgsqlDbType.Integer) { Value = -quantity });
                txnId = Convert.ToInt32(insertCmd.ExecuteScalar());
            }

            UpdateQuantity(conn, tx, productId, newQty);
            tx.Commit();
            return txnId;
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    /// <summary>
    /// Read-only history with optional filters (FR-SRCH-003). No update/delete.
    /// </summary>
    public IReadOnlyList<TransactionHistoryRow> GetHistory(
        DateTime? dateFrom,
        DateTime? dateTo,
        string? transactionType,
        int? productId,
        int limit = 500)
    {
        var list = new List<TransactionHistoryRow>();
        var where = new List<string>();

        if (dateFrom is not null)
            where.Add("t.\"TransactionDate\" >= @dateFrom");
        if (dateTo is not null)
            where.Add("t.\"TransactionDate\" <= @dateTo");
        if (!string.IsNullOrWhiteSpace(transactionType))
            where.Add("t.\"TransactionType\" = @txnType");
        if (productId is not null && productId > 0)
            where.Add("t.\"ProductID\" = @productId");

        var whereSql = where.Count == 0 ? string.Empty : "WHERE " + string.Join(" AND ", where);

        var sql = $"""
            SELECT t."TransactionID",
                   t."TransactionType",
                   t."ProductID",
                   p."ProductName",
                   t."Quantity",
                   t."TransactionDate",
                   t."UnitPrice",
                   t."SupplierID",
                   s."SupplierName",
                   t."CustomerID",
                   c."CustomerName",
                   t."PreviousQuantity",
                   t."NewQuantity",
                   t."Difference",
                   t."Reason",
                   t."Notes",
                   t."UserID",
                   u."FullName" AS "UserFullName",
                   t."CreatedDateTime"
            FROM "InventoryTransaction" t
            INNER JOIN "Product" p ON p."ProductID" = t."ProductID"
            LEFT JOIN "Supplier" s ON s."SupplierID" = t."SupplierID"
            LEFT JOIN "Customer" c ON c."CustomerID" = t."CustomerID"
            INNER JOIN "User" u ON u."UserID" = t."UserID"
            {whereSql}
            ORDER BY t."CreatedDateTime" DESC, t."TransactionID" DESC
            LIMIT @limit
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);

        if (dateFrom is not null)
            cmd.Parameters.Add(new NpgsqlParameter("dateFrom", NpgsqlDbType.Date) { Value = dateFrom.Value.Date });
        if (dateTo is not null)
            cmd.Parameters.Add(new NpgsqlParameter("dateTo", NpgsqlDbType.Date) { Value = dateTo.Value.Date });
        if (!string.IsNullOrWhiteSpace(transactionType))
            cmd.Parameters.Add(new NpgsqlParameter("txnType", NpgsqlDbType.Varchar) { Value = transactionType });
        if (productId is not null && productId > 0)
            cmd.Parameters.Add(new NpgsqlParameter("productId", NpgsqlDbType.Integer) { Value = productId.Value });

        cmd.Parameters.Add(new NpgsqlParameter("limit", NpgsqlDbType.Integer) { Value = limit });

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            list.Add(MapHistoryRow(reader));

        return list;
    }

    public IReadOnlyList<StockInListRow> GetRecentStockIns(int limit = 50)
        => GetRecentByType("StockIn", limit);

    public IReadOnlyList<StockOutListRow> GetRecentStockOuts(int limit = 50)
    {
        var list = new List<StockOutListRow>();

        const string sql = """
            SELECT t."TransactionID",
                   t."ProductID",
                   p."ProductName",
                   t."Quantity",
                   t."TransactionDate",
                   t."UnitPrice",
                   t."CustomerID",
                   c."CustomerName",
                   t."Notes",
                   t."UserID",
                   u."FullName" AS "UserFullName",
                   t."CreatedDateTime",
                   t."PreviousQuantity",
                   t."NewQuantity"
            FROM "InventoryTransaction" t
            INNER JOIN "Product" p ON p."ProductID" = t."ProductID"
            LEFT JOIN "Customer" c ON c."CustomerID" = t."CustomerID"
            INNER JOIN "User" u ON u."UserID" = t."UserID"
            WHERE t."TransactionType" = 'StockOut'
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
            list.Add(new StockOutListRow
            {
                TransactionID = reader.GetInt32(reader.GetOrdinal("TransactionID")),
                ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                TransactionDate = reader.GetDateTime(reader.GetOrdinal("TransactionDate")),
                UnitPrice = reader.IsDBNull(reader.GetOrdinal("UnitPrice"))
                    ? null
                    : reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                CustomerID = reader.IsDBNull(reader.GetOrdinal("CustomerID"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("CustomerID")),
                CustomerName = reader.IsDBNull(reader.GetOrdinal("CustomerName"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("CustomerName")),
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

    private IReadOnlyList<StockInListRow> GetRecentByType(string type, int limit)
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
            WHERE t."TransactionType" = @type
            ORDER BY t."CreatedDateTime" DESC
            LIMIT @limit
            """;

        using var conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.Add(new NpgsqlParameter("type", NpgsqlDbType.Varchar) { Value = type });
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

    private static TransactionHistoryRow MapHistoryRow(NpgsqlDataReader reader)
    {
        var typeDb = reader.GetString(reader.GetOrdinal("TransactionType"));
        return new TransactionHistoryRow
        {
            TransactionID = reader.GetInt32(reader.GetOrdinal("TransactionID")),
            TransactionType = typeDb,
            TypeLabel = typeDb switch
            {
                "StockIn" => "Stock-In",
                "StockOut" => "Stock-Out",
                "Adjustment" => "Adjustment",
                _ => typeDb
            },
            ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
            ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
            Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
            TransactionDate = reader.GetDateTime(reader.GetOrdinal("TransactionDate")),
            UnitPrice = reader.IsDBNull(reader.GetOrdinal("UnitPrice"))
                ? null
                : reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
            SupplierName = reader.IsDBNull(reader.GetOrdinal("SupplierName"))
                ? null
                : reader.GetString(reader.GetOrdinal("SupplierName")),
            CustomerName = reader.IsDBNull(reader.GetOrdinal("CustomerName"))
                ? null
                : reader.GetString(reader.GetOrdinal("CustomerName")),
            PreviousQuantity = reader.IsDBNull(reader.GetOrdinal("PreviousQuantity"))
                ? null
                : reader.GetInt32(reader.GetOrdinal("PreviousQuantity")),
            NewQuantity = reader.IsDBNull(reader.GetOrdinal("NewQuantity"))
                ? null
                : reader.GetInt32(reader.GetOrdinal("NewQuantity")),
            Difference = reader.IsDBNull(reader.GetOrdinal("Difference"))
                ? null
                : reader.GetInt32(reader.GetOrdinal("Difference")),
            Reason = reader.IsDBNull(reader.GetOrdinal("Reason"))
                ? null
                : reader.GetString(reader.GetOrdinal("Reason")),
            Notes = reader.IsDBNull(reader.GetOrdinal("Notes"))
                ? null
                : reader.GetString(reader.GetOrdinal("Notes")),
            UserFullName = reader.GetString(reader.GetOrdinal("UserFullName")),
            CreatedDateTime = reader.GetDateTime(reader.GetOrdinal("CreatedDateTime"))
        };
    }

    private static (int Qty, bool IsActive) LockProduct(NpgsqlConnection conn, NpgsqlTransaction tx, int productId)
    {
        const string lockSql = """
            SELECT "QuantityOnHand", "IsActive"
            FROM "Product"
            WHERE "ProductID" = @productId
            FOR UPDATE
            """;

        using var lockCmd = new NpgsqlCommand(lockSql, conn, tx);
        lockCmd.Parameters.Add(new NpgsqlParameter("productId", NpgsqlDbType.Integer) { Value = productId });
        using var reader = lockCmd.ExecuteReader();
        if (!reader.Read())
            throw new InvalidOperationException("Product not found.");

        return (reader.GetInt32(0), reader.GetBoolean(1));
    }

    private static void UpdateQuantity(NpgsqlConnection conn, NpgsqlTransaction tx, int productId, int newQty)
    {
        const string updateSql = """
            UPDATE "Product"
            SET "QuantityOnHand" = @newQty,
                "ModifiedDate" = @modified
            WHERE "ProductID" = @productId
            """;

        using var updateCmd = new NpgsqlCommand(updateSql, conn, tx);
        updateCmd.Parameters.Add(new NpgsqlParameter("newQty", NpgsqlDbType.Integer) { Value = newQty });
        updateCmd.Parameters.Add(new NpgsqlParameter("modified", NpgsqlDbType.TimestampTz) { Value = DateTime.UtcNow });
        updateCmd.Parameters.Add(new NpgsqlParameter("productId", NpgsqlDbType.Integer) { Value = productId });
        updateCmd.ExecuteNonQuery();
    }
}

public class TransactionHistoryRow
{
    public int TransactionID { get; set; }
    public string TransactionType { get; set; } = string.Empty;
    public string TypeLabel { get; set; } = string.Empty;
    public int ProductID { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal? UnitPrice { get; set; }
    public string? SupplierName { get; set; }
    public string? CustomerName { get; set; }
    public int? PreviousQuantity { get; set; }
    public int? NewQuantity { get; set; }
    public int? Difference { get; set; }
    public string? Reason { get; set; }
    public string? Notes { get; set; }
    public string UserFullName { get; set; } = string.Empty;
    public DateTime CreatedDateTime { get; set; }
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

public class StockOutListRow
{
    public int TransactionID { get; set; }
    public int ProductID { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal? UnitPrice { get; set; }
    public int? CustomerID { get; set; }
    public string? CustomerName { get; set; }
    public string? Notes { get; set; }
    public int UserID { get; set; }
    public string UserFullName { get; set; } = string.Empty;
    public DateTime CreatedDateTime { get; set; }
    public int? PreviousQuantity { get; set; }
    public int? NewQuantity { get; set; }
}
