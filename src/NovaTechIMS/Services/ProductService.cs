using System;
using System.Collections.Generic;
using NovaTechIMS.Data;
using NovaTechIMS.Models;
using NovaTechIMS.Models.Enums;
using NovaTechIMS.Security;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Services;

/// <summary>Product business logic with authorization gates (M10).</summary>
public class ProductService
{
    private readonly ProductRepository _repository = new();
    private readonly CategoryRepository _categoryRepository = new();
    private readonly SupplierRepository _supplierRepository = new();

    public IReadOnlyList<ProductListRow> GetList(
        string? search = null,
        int? categoryId = null,
        int? supplierId = null,
        StockStatus? stockStatus = null,
        bool? isActiveFilter = null)
    {
        AuthorizationService.RequirePermission(Permissions.ViewProducts);
        return _repository.GetList(search, categoryId, supplierId, stockStatus, isActiveFilter);
    }

    public Product GetById(int productId)
    {
        AuthorizationService.RequirePermission(Permissions.ViewProducts);
        return _repository.GetById(productId)
            ?? throw new NotFoundException("The selected product no longer exists.");
    }

    public Product Create(
        string name,
        int categoryId,
        int supplierId,
        string? description,
        decimal purchasePrice,
        decimal sellingPrice,
        int minimumStockLevel)
    {
        AuthorizationService.RequirePermission(Permissions.ManageProducts);
        Validate(name, categoryId, supplierId, description, purchasePrice, sellingPrice, minimumStockLevel);
        EnsureCategoryExists(categoryId);
        EnsureSupplierExists(supplierId);

        var product = new Product
        {
            ProductName = name.Trim(),
            CategoryID = categoryId,
            SupplierID = supplierId,
            Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim(),
            PurchasePrice = RoundMoney(purchasePrice),
            SellingPrice = RoundMoney(sellingPrice),
            QuantityOnHand = 0,
            MinimumStockLevel = minimumStockLevel,
            IsActive = true,
            CreatedDate = DateTime.UtcNow
        };

        product.ProductID = _repository.Insert(product);
        return product;
    }

    public void Update(
        int productId,
        string name,
        int categoryId,
        int supplierId,
        string? description,
        decimal purchasePrice,
        decimal sellingPrice,
        int minimumStockLevel,
        bool isActive)
    {
        AuthorizationService.RequirePermission(Permissions.ManageProducts);
        Validate(name, categoryId, supplierId, description, purchasePrice, sellingPrice, minimumStockLevel);
        EnsureCategoryExists(categoryId);
        EnsureSupplierExists(supplierId);

        var existing = GetById(productId);
        existing.ProductName = name.Trim();
        existing.CategoryID = categoryId;
        existing.SupplierID = supplierId;
        existing.Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        existing.PurchasePrice = RoundMoney(purchasePrice);
        existing.SellingPrice = RoundMoney(sellingPrice);
        existing.MinimumStockLevel = minimumStockLevel;
        existing.IsActive = isActive;
        existing.ModifiedDate = DateTime.UtcNow;

        _repository.Update(existing);
    }

    public void DeleteOrThrowIfReferenced(int productId)
    {
        AuthorizationService.RequirePermission(Permissions.DeleteProducts);
        _ = GetById(productId);

        var txnCount = _repository.CountTransactions(productId);
        if (txnCount > 0)
        {
            throw new BusinessRuleException(
                $"This product has {txnCount} inventory transaction(s). " +
                "It cannot be hard-deleted. Mark it Inactive instead.");
        }

        if (!_repository.TryHardDelete(productId))
        {
            throw new BusinessRuleException(
                "This product cannot be deleted because it is still referenced. " +
                "Mark it Inactive instead.");
        }
    }

    public void Deactivate(int productId)
    {
        AuthorizationService.RequirePermission(Permissions.DeleteProducts);
        _ = GetById(productId);
        _repository.SoftDeactivate(productId);
    }

    private void EnsureCategoryExists(int categoryId)
    {
        if (_categoryRepository.GetById(categoryId) is null)
            throw new ValidationException("Please select a valid category.");
    }

    private void EnsureSupplierExists(int supplierId)
    {
        if (_supplierRepository.GetById(supplierId) is null)
            throw new ValidationException("Please select a valid default supplier.");
    }

    private static void Validate(
        string name, int categoryId, int supplierId, string? description,
        decimal purchasePrice, decimal sellingPrice, int minimumStockLevel)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Product name is required.");
        if (name.Trim().Length > 100)
            throw new ValidationException("Product name cannot exceed 100 characters.");
        if (categoryId <= 0)
            throw new ValidationException("Category is required.");
        if (supplierId <= 0)
            throw new ValidationException("Default supplier is required.");
        if (description is not null && description.Length > 500)
            throw new ValidationException("Description cannot exceed 500 characters.");
        if (purchasePrice < 0)
            throw new ValidationException("Purchase price must be greater than or equal to zero.");
        if (sellingPrice < 0)
            throw new ValidationException("Selling price must be greater than or equal to zero.");
        if (minimumStockLevel < 0)
            throw new ValidationException("Minimum stock level must be greater than or equal to zero.");
    }

    private static decimal RoundMoney(decimal value)
        => Math.Round(value, 2, MidpointRounding.AwayFromZero);
}
