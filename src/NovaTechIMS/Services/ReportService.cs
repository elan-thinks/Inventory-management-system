using System;
using System.Collections.Generic;
using System.Linq;
using NovaTechIMS.Data;
using NovaTechIMS.Models.Enums;
using NovaTechIMS.Security;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Services;

/// <summary>
/// MVP reports (FR-RPT, technical-design/13-Reporting-Design).
/// View on screen + print; no export engine.
/// </summary>
public class ReportService
{
    private readonly ProductRepository _products = new();
    private readonly InventoryTransactionRepository _txns = new();

    public IReadOnlyList<InventoryReportRow> GetCurrentInventory()
    {
        RequireReports();
        return MapInventory(_products.GetList(null, null, null, null, isActiveFilter: true));
    }

    public IReadOnlyList<InventoryReportRow> GetLowStock()
    {
        RequireReports();
        return MapInventory(_products.GetList(null, null, null, StockStatus.LowStock, isActiveFilter: true));
    }

    public IReadOnlyList<InventoryReportRow> GetOutOfStock()
    {
        RequireReports();
        return MapInventory(_products.GetList(null, null, null, StockStatus.OutOfStock, isActiveFilter: true));
    }

    public IReadOnlyList<TransactionHistoryRow> GetStockInReport(DateTime from, DateTime to)
    {
        RequireReports();
        ValidateRange(from, to);
        return _txns.GetHistory(from.Date, to.Date, "StockIn", null, 2000);
    }

    public IReadOnlyList<TransactionHistoryRow> GetStockOutReport(DateTime from, DateTime to)
    {
        RequireReports();
        ValidateRange(from, to);
        return _txns.GetHistory(from.Date, to.Date, "StockOut", null, 2000);
    }

    public IReadOnlyList<TransactionHistoryRow> GetTransactionHistoryReport(
        DateTime from,
        DateTime to,
        string? transactionType,
        int? productId)
    {
        RequireReports();
        ValidateRange(from, to);

        string? typeFilter = null;
        if (!string.IsNullOrWhiteSpace(transactionType)
            && !transactionType.Equals("All", StringComparison.OrdinalIgnoreCase))
        {
            typeFilter = transactionType switch
            {
                "Stock-In" or "StockIn" => "StockIn",
                "Stock-Out" or "StockOut" => "StockOut",
                "Adjustment" => "Adjustment",
                _ => transactionType
            };
        }

        return _txns.GetHistory(from.Date, to.Date, typeFilter, productId, 2000);
    }

    public IReadOnlyList<ProductListRow> GetProductsForFilter()
    {
        RequireReports();
        return _products.GetList(null, null, null, null, null);
    }

    private static void RequireReports()
        => AuthorizationService.RequirePermission(Permissions.ViewReports);

    private static void ValidateRange(DateTime from, DateTime to)
    {
        if (from.Date > to.Date)
            throw new ValidationException("From date cannot be after To date.");
    }

    private static List<InventoryReportRow> MapInventory(IReadOnlyList<ProductListRow> rows)
    {
        return rows.Select(p => new InventoryReportRow
        {
            ProductID = p.ProductID,
            ProductName = p.ProductName,
            CategoryName = p.CategoryName,
            SupplierName = p.SupplierName,
            QuantityOnHand = p.QuantityOnHand,
            MinimumStockLevel = p.MinimumStockLevel,
            PurchasePrice = p.PurchasePrice,
            SellingPrice = p.SellingPrice,
            StockStatusLabel = p.StockStatusLabel,
            Status = p.IsActive ? "Active" : "Inactive"
        }).ToList();
    }
}

public class InventoryReportRow
{
    public int ProductID { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string SupplierName { get; set; } = string.Empty;
    public int QuantityOnHand { get; set; }
    public int MinimumStockLevel { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal SellingPrice { get; set; }
    public string StockStatusLabel { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
