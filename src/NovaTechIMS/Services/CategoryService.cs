using System;
using System.Collections.Generic;
using NovaTechIMS.Data;
using NovaTechIMS.Models;
using NovaTechIMS.Security;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Services;

/// <summary>Category business logic with authorization gates (M10).</summary>
public class CategoryService
{
    private readonly CategoryRepository _repository = new();

    public IReadOnlyList<CategoryListRow> GetList(string? searchName = null, bool? isActiveFilter = null)
    {
        AuthorizationService.RequirePermission(Permissions.ViewCategories);
        return _repository.GetList(searchName, isActiveFilter);
    }

    public Category GetById(int categoryId)
    {
        AuthorizationService.RequirePermission(Permissions.ViewCategories);
        return _repository.GetById(categoryId)
            ?? throw new NotFoundException("The selected category no longer exists.");
    }

    public Category Create(string name, string? description)
    {
        AuthorizationService.RequirePermission(Permissions.ManageCategories);
        ValidateName(name);
        ValidateDescription(description);

        if (_repository.ExistsByName(name))
            throw new DuplicateRecordException("A category with this name already exists.");

        var category = new Category
        {
            CategoryName = name.Trim(),
            Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim(),
            IsActive = true,
            CreatedDate = DateTime.UtcNow
        };

        category.CategoryID = _repository.Insert(category);
        return category;
    }

    public void Update(int categoryId, string name, string? description, bool isActive)
    {
        AuthorizationService.RequirePermission(Permissions.ManageCategories);
        ValidateName(name);
        ValidateDescription(description);

        var existing = GetById(categoryId);

        if (_repository.ExistsByName(name, excludeCategoryId: categoryId))
            throw new DuplicateRecordException("A category with this name already exists.");

        existing.CategoryName = name.Trim();
        existing.Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        existing.IsActive = isActive;
        existing.ModifiedDate = DateTime.UtcNow;

        _repository.Update(existing);
    }

    public void DeleteOrThrowIfReferenced(int categoryId)
    {
        AuthorizationService.RequirePermission(Permissions.DeleteCategories);
        _ = GetById(categoryId);

        var productCount = _repository.CountProducts(categoryId);
        if (productCount > 0)
        {
            throw new BusinessRuleException(
                $"This category contains {productCount} product(s). " +
                "Reassign or remove those products first, or mark this category Inactive.");
        }

        if (!_repository.TryHardDelete(categoryId))
        {
            throw new BusinessRuleException(
                "This category cannot be deleted because it is still referenced. " +
                "Mark it Inactive instead.");
        }
    }

    public void Deactivate(int categoryId)
    {
        AuthorizationService.RequirePermission(Permissions.DeleteCategories);
        _ = GetById(categoryId);
        _repository.SoftDeactivate(categoryId);
    }

    public int GetProductCount(int categoryId) => _repository.CountProducts(categoryId);

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Category name is required.");
        if (name.Trim().Length > 100)
            throw new ValidationException("Category name cannot exceed 100 characters.");
    }

    private static void ValidateDescription(string? description)
    {
        if (description is not null && description.Length > 500)
            throw new ValidationException("Description cannot exceed 500 characters.");
    }
}
