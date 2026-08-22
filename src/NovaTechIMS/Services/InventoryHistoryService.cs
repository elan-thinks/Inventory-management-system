using System;
using System.Collections.Generic;
using NovaTechIMS.Data;
using NovaTechIMS.Security;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Services;

/// <summary>
/// Read-only inventory transaction history (Milestone 13, FR-SRCH-003).
/// No update or delete of transactions.
/// </summary>
public class InventoryHistoryService
{
    private readonly InventoryTransactionRepository _txnRepo = new();
    private readonly ProductRepository _productRepo = new();

    public IReadOnlyList<TransactionHistoryRow> GetHistory(
        DateTime? dateFrom,
        DateTime? dateTo,
        string? transactionType,
        int? productId)
    {
        AuthorizationService.RequirePermission(Permissions.ViewInventoryHistory);

        if (dateFrom is not null && dateTo is not null && dateFrom.Value.Date > dateTo.Value.Date)
            throw new ValidationException("From date cannot be after To date.");

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

        return _txnRepo.GetHistory(dateFrom, dateTo, typeFilter, productId, limit: 500);
    }

    public IReadOnlyList<ProductListRow> GetProductsForFilter()
    {
        AuthorizationService.RequirePermission(Permissions.ViewInventoryHistory);
        // Include inactive products so historical rows remain findable
        return _productRepo.GetList(null, null, null, null, isActiveFilter: null);
    }
}
