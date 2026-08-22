using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;
using NovaTechIMS.Data;
using NovaTechIMS.Models;
using NovaTechIMS.Security;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Services;

/// <summary>Supplier business logic with authorization gates (M10).</summary>
public class SupplierService
{
    private static readonly Regex PhonePattern = new(@"^[0-9+\-()\s]+$", RegexOptions.Compiled);
    private readonly SupplierRepository _repository = new();

    public IReadOnlyList<SupplierListRow> GetList(string? searchName = null, bool? isActiveFilter = null)
    {
        AuthorizationService.RequirePermission(Permissions.ViewSuppliers);
        return _repository.GetList(searchName, isActiveFilter);
    }

    public Supplier GetById(int supplierId)
    {
        AuthorizationService.RequirePermission(Permissions.ViewSuppliers);
        return _repository.GetById(supplierId)
            ?? throw new NotFoundException("The selected supplier no longer exists.");
    }

    public Supplier Create(string name, string? contactPerson, string? phone, string? email, string? address)
    {
        AuthorizationService.RequirePermission(Permissions.ManageSuppliers);
        ValidateFields(name, contactPerson, phone, email, address);

        var supplier = new Supplier
        {
            SupplierName = name.Trim(),
            ContactPerson = NullIfWhiteSpace(contactPerson),
            Phone = NullIfWhiteSpace(phone),
            Email = NullIfWhiteSpace(email),
            Address = NullIfWhiteSpace(address),
            IsActive = true,
            CreatedDate = DateTime.UtcNow
        };

        supplier.SupplierID = _repository.Insert(supplier);
        return supplier;
    }

    public void Update(int supplierId, string name, string? contactPerson, string? phone, string? email, string? address, bool isActive)
    {
        AuthorizationService.RequirePermission(Permissions.ManageSuppliers);
        ValidateFields(name, contactPerson, phone, email, address);

        var existing = GetById(supplierId);
        existing.SupplierName = name.Trim();
        existing.ContactPerson = NullIfWhiteSpace(contactPerson);
        existing.Phone = NullIfWhiteSpace(phone);
        existing.Email = NullIfWhiteSpace(email);
        existing.Address = NullIfWhiteSpace(address);
        existing.IsActive = isActive;
        existing.ModifiedDate = DateTime.UtcNow;

        _repository.Update(existing);
    }

    public void DeleteOrThrowIfReferenced(int supplierId)
    {
        AuthorizationService.RequirePermission(Permissions.DeleteSuppliers);
        _ = GetById(supplierId);

        var productCount = _repository.CountProducts(supplierId);
        var txnCount = _repository.CountTransactions(supplierId);

        if (productCount > 0 || txnCount > 0)
        {
            throw new BusinessRuleException(
                $"This supplier is referenced by {productCount} product(s) and {txnCount} inventory transaction(s). " +
                "It cannot be hard-deleted. Mark it Inactive instead.");
        }

        if (!_repository.TryHardDelete(supplierId))
        {
            throw new BusinessRuleException(
                "This supplier cannot be deleted because it is still referenced. " +
                "Mark it Inactive instead.");
        }
    }

    public void Deactivate(int supplierId)
    {
        AuthorizationService.RequirePermission(Permissions.DeleteSuppliers);
        _ = GetById(supplierId);
        _repository.SoftDeactivate(supplierId);
    }

    private static void ValidateFields(string name, string? contactPerson, string? phone, string? email, string? address)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Supplier name is required.");
        if (name.Trim().Length > 150)
            throw new ValidationException("Supplier name cannot exceed 150 characters.");
        if (contactPerson is not null && contactPerson.Trim().Length > 100)
            throw new ValidationException("Contact person cannot exceed 100 characters.");
        if (!string.IsNullOrWhiteSpace(phone))
        {
            var p = phone.Trim();
            if (p.Length > 30)
                throw new ValidationException("Phone cannot exceed 30 characters.");
            if (!PhonePattern.IsMatch(p))
                throw new ValidationException("Phone may only contain digits, spaces, +, -, and parentheses.");
        }
        if (!string.IsNullOrWhiteSpace(email))
        {
            var e = email.Trim();
            if (e.Length > 100)
                throw new ValidationException("Email cannot exceed 100 characters.");
            if (!IsValidEmail(e))
                throw new ValidationException("Email format is not valid.");
        }
        if (address is not null && address.Trim().Length > 300)
            throw new ValidationException("Address cannot exceed 300 characters.");
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address.Equals(email, StringComparison.OrdinalIgnoreCase);
        }
        catch { return false; }
    }

    private static string? NullIfWhiteSpace(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
