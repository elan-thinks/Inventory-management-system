using System;
using System.Collections.Generic;
using System.Linq;
using NovaTechIMS.Data;
using NovaTechIMS.Models.Enums;
using NovaTechIMS.Security;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Services;

/// <summary>
/// Dashboard metrics + recent activity (FR-DASH / SCR-002).
/// Both roles see the same metric set.
/// </summary>
public class DashboardService
{
    private readonly ProductRepository _products = new();
    private readonly CategoryRepository _categories = new();
    private readonly SupplierRepository _suppliers = new();
    private readonly InventoryTransactionRepository _txns = new();

    public DashboardMetrics GetMetrics()
    {
        AuthorizationService.RequirePermission(Permissions.ViewDashboard);

        try
        {
            var activeProducts = _products.GetList(null, null, null, null, isActiveFilter: true);
            var categories = _categories.GetList(null, isActiveFilter: true);
            var suppliers = _suppliers.GetList(null, isActiveFilter: true);

            return new DashboardMetrics
            {
                ActiveProductCount = activeProducts.Count,
                CategoryCount = categories.Count,
                SupplierCount = suppliers.Count,
                TotalQuantityOnHand = activeProducts.Sum(p => p.QuantityOnHand),
                LowStockCount = activeProducts.Count(p =>
                    p.QuantityOnHand > 0 && p.QuantityOnHand <= p.MinimumStockLevel),
                OutOfStockCount = activeProducts.Count(p => p.QuantityOnHand <= 0)
            };
        }
        catch (Exception ex)
        {
            throw DbExceptionMapper.Map(ex);
        }
    }

    public IReadOnlyList<TransactionHistoryRow> GetRecentActivity(int limit = 10)
    {
        AuthorizationService.RequirePermission(Permissions.ViewDashboard);

        try
        {
            return _txns.GetHistory(null, null, null, null, limit);
        }
        catch (Exception ex)
        {
            throw DbExceptionMapper.Map(ex);
        }
    }
}

public class DashboardMetrics
{
    public int ActiveProductCount { get; set; }
    public int CategoryCount { get; set; }
    public int SupplierCount { get; set; }
    public int TotalQuantityOnHand { get; set; }
    public int LowStockCount { get; set; }
    public int OutOfStockCount { get; set; }
}
