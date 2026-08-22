using System;
using System.Collections.Generic;
using NovaTechIMS.Data;
using NovaTechIMS.Models;
using NovaTechIMS.Security;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Services;

/// <summary>
/// Stock-Out business logic (FR-SOUT-001–007, BR-011/012/016/021/025/027/029).
/// </summary>
public class StockOutService
{
    private readonly InventoryTransactionRepository _txnRepo = new();
    private readonly ProductRepository _productRepo = new();
    private readonly CustomerRepository _customerRepo = new();

    public IReadOnlyList<StockOutListRow> GetRecent(int limit = 50)
    {
        AuthorizationService.RequirePermission(Permissions.StockOut);
        try
        {
            return _txnRepo.GetRecentStockOuts(limit);
        }
        catch (Exception ex)
        {
            throw DbExceptionMapper.Map(ex);
        }
    }

    public IReadOnlyList<ProductListRow> GetActiveProducts()
    {
        AuthorizationService.RequirePermission(Permissions.StockOut);
        try
        {
            return _productRepo.GetList(null, null, null, null, isActiveFilter: true);
        }
        catch (Exception ex)
        {
            throw DbExceptionMapper.Map(ex);
        }
    }

    public IReadOnlyList<CustomerListRow> GetActiveCustomers()
    {
        AuthorizationService.RequirePermission(Permissions.StockOut);
        try
        {
            return _customerRepo.GetList(null, isActiveFilter: true);
        }
        catch (Exception ex)
        {
            throw DbExceptionMapper.Map(ex);
        }
    }

    public Product GetProduct(int productId)
    {
        AuthorizationService.RequirePermission(Permissions.StockOut);
        try
        {
            return _productRepo.GetById(productId)
                ?? throw new NotFoundException("The selected product no longer exists.");
        }
        catch (AppException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw DbExceptionMapper.Map(ex);
        }
    }

    public int Record(
        int productId,
        int? customerId,
        int quantity,
        DateTime transactionDate,
        decimal unitPrice,
        string? notes)
    {
        AuthorizationService.RequirePermission(Permissions.StockOut);

        var current = SessionContext.Current
            ?? throw new UnauthorizedException("You must be signed in to record Stock-Out.");

        if (productId <= 0)
            throw new ValidationException("Product is required.");

        if (quantity < 1)
            throw new ValidationException("Quantity must be a positive integer (at least 1).");

        if (unitPrice < 0)
            throw new ValidationException("Selling price must be greater than or equal to zero.");

        unitPrice = Math.Round(unitPrice, 2, MidpointRounding.AwayFromZero);

        if (transactionDate.Date > DateTime.Today)
            throw new ValidationException("Transaction date cannot be a future date.");

        if (notes is not null && notes.Length > 500)
            throw new ValidationException("Notes cannot exceed 500 characters.");

        Product product;
        try
        {
            product = _productRepo.GetById(productId)
                ?? throw new NotFoundException("The selected product no longer exists.");
        }
        catch (AppException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw DbExceptionMapper.Map(ex);
        }

        if (!product.IsActive)
            throw new BusinessRuleException("Stock-Out is not allowed for inactive products.");

        if (quantity > product.QuantityOnHand)
            throw new InsufficientStockException(product.QuantityOnHand, quantity);

        if (customerId is int cid && cid > 0)
        {
            try
            {
                var customer = _customerRepo.GetById(cid);
                if (customer is null || !customer.IsActive)
                    throw new ValidationException("Please select a valid active customer, or leave customer blank.");
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw DbExceptionMapper.Map(ex);
            }
        }
        else
        {
            customerId = null;
        }

        try
        {
            return _txnRepo.InsertStockOut(
                productId,
                quantity,
                transactionDate.Date,
                unitPrice,
                customerId,
                notes,
                current.UserID);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Insufficient", StringComparison.OrdinalIgnoreCase))
        {
            throw new InsufficientStockException("Insufficient stock. The quantity on hand is less than the requested amount.");
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("inactive", StringComparison.OrdinalIgnoreCase))
        {
            throw new BusinessRuleException("Stock-Out is not allowed for inactive products.");
        }
        catch (AppException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw DbExceptionMapper.Map(ex);
        }
    }
}
