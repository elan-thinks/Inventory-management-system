using System;
using System.Collections.Generic;
using NovaTechIMS.Data;
using NovaTechIMS.Models;
using NovaTechIMS.Security;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Services;

/// <summary>
/// Inventory Adjustment (FR-ADJ-001–005). Administrator only (Permissions.PerformAdjustment).
/// </summary>
public class AdjustmentService
{
    private readonly InventoryTransactionRepository _txnRepo = new();
    private readonly ProductRepository _productRepo = new();

    public IReadOnlyList<ProductListRow> GetActiveProducts()
    {
        AuthorizationService.RequirePermission(Permissions.PerformAdjustment);
        return _productRepo.GetList(null, null, null, null, isActiveFilter: true);
    }

    public Product GetProduct(int productId)
    {
        AuthorizationService.RequirePermission(Permissions.PerformAdjustment);
        return _productRepo.GetById(productId)
            ?? throw new NotFoundException("The selected product no longer exists.");
    }

    public IReadOnlyList<TransactionHistoryRow> GetRecentAdjustments(int limit = 30)
    {
        AuthorizationService.RequirePermission(Permissions.PerformAdjustment);
        return _txnRepo.GetHistory(null, null, "Adjustment", null, limit);
    }

    /// <summary>
    /// Sets QuantityOnHand to <paramref name="newQuantity"/> and records Adjustment history.
    /// </summary>
    public int Record(
        int productId,
        int newQuantity,
        DateTime transactionDate,
        string reason,
        string? notes)
    {
        AuthorizationService.RequirePermission(Permissions.PerformAdjustment);

        var current = SessionContext.Current
            ?? throw new UnauthorizedException("You must be signed in to perform an adjustment.");

        if (productId <= 0)
            throw new ValidationException("Product is required.");

        if (newQuantity < 0)
            throw new ValidationException("New quantity cannot be negative.");

        if (string.IsNullOrWhiteSpace(reason))
            throw new ValidationException("Reason is required for inventory adjustments.");

        if (reason.Trim().Length > 250)
            throw new ValidationException("Reason cannot exceed 250 characters.");

        if (notes is not null && notes.Length > 500)
            throw new ValidationException("Notes cannot exceed 500 characters.");

        if (transactionDate.Date > DateTime.Today)
            throw new ValidationException("Transaction date cannot be a future date.");

        var product = _productRepo.GetById(productId)
            ?? throw new NotFoundException("The selected product no longer exists.");

        if (!product.IsActive)
            throw new BusinessRuleException("Adjustment is not allowed for inactive products.");

        if (newQuantity == product.QuantityOnHand)
            throw new ValidationException("New quantity is the same as the current quantity. No adjustment needed.");

        try
        {
            return _txnRepo.InsertAdjustment(
                productId,
                newQuantity,
                transactionDate.Date,
                reason.Trim(),
                notes,
                current.UserID);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("inactive", StringComparison.OrdinalIgnoreCase))
        {
            throw new BusinessRuleException("Adjustment is not allowed for inactive products.");
        }
    }
}
