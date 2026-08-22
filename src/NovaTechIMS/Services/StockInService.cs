using System;
using System.Collections.Generic;
using System.Linq;
using NovaTechIMS.Data;
using NovaTechIMS.Models;
using NovaTechIMS.Security;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Services;

/// <summary>
/// Stock-In business logic (FR-SIN-001–006, BR-012, BR-016, BR-021, BR-027, BR-028).
/// </summary>
public class StockInService
{
    private readonly InventoryTransactionRepository _txnRepo = new();
    private readonly ProductRepository _productRepo = new();
    private readonly SupplierRepository _supplierRepo = new();

    public IReadOnlyList<StockInListRow> GetRecent(int limit = 50)
    {
        AuthorizationService.RequirePermission(Permissions.StockIn);
        return _txnRepo.GetRecentStockIns(limit);
    }

    /// <summary>
    /// Active products for the Stock-In product combo.
    /// </summary>
    public IReadOnlyList<ProductListRow> GetActiveProducts()
    {
        AuthorizationService.RequirePermission(Permissions.StockIn);
        return _productRepo.GetList(null, null, null, null, isActiveFilter: true);
    }

    public IReadOnlyList<SupplierListRow> GetActiveSuppliers()
    {
        AuthorizationService.RequirePermission(Permissions.StockIn);
        return _supplierRepo.GetList(null, isActiveFilter: true);
    }

    public Product GetProduct(int productId)
    {
        AuthorizationService.RequirePermission(Permissions.StockIn);
        var p = _productRepo.GetById(productId)
            ?? throw new NotFoundException("The selected product no longer exists.");
        return p;
    }

    /// <summary>
    /// Records Stock-In: append transaction + increase QuantityOnHand.
    /// </summary>
    public int Record(
        int productId,
        int supplierId,
        int quantity,
        DateTime transactionDate,
        decimal unitPrice,
        string? notes)
    {
        AuthorizationService.RequirePermission(Permissions.StockIn);

        var current = SessionContext.Current
            ?? throw new UnauthorizedException("You must be signed in to record Stock-In.");

        if (productId <= 0)
            throw new ValidationException("Product is required.");

        if (supplierId <= 0)
            throw new ValidationException("Supplier is required.");

        if (quantity < 1)
            throw new ValidationException("Quantity must be a positive integer (at least 1).");

        if (unitPrice < 0)
            throw new ValidationException("Purchase price must be greater than or equal to zero.");

        unitPrice = Math.Round(unitPrice, 2, MidpointRounding.AwayFromZero);

        var today = DateTime.Today;
        if (transactionDate.Date > today)
            throw new ValidationException("Transaction date cannot be a future date.");

        if (notes is not null && notes.Length > 500)
            throw new ValidationException("Notes cannot exceed 500 characters.");

        var product = _productRepo.GetById(productId)
            ?? throw new NotFoundException("The selected product no longer exists.");

        if (!product.IsActive)
            throw new BusinessRuleException("Stock-In is not allowed for inactive products.");

        var supplier = _supplierRepo.GetById(supplierId);
        if (supplier is null || !supplier.IsActive)
            throw new ValidationException("Please select a valid active supplier.");

        try
        {
            return _txnRepo.InsertStockIn(
                productId,
                quantity,
                transactionDate.Date,
                unitPrice,
                supplierId,
                notes,
                current.UserID);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("inactive", StringComparison.OrdinalIgnoreCase))
        {
            throw new BusinessRuleException("Stock-In is not allowed for inactive products.");
        }
    }
}
