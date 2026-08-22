using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;
using NovaTechIMS.Data;
using NovaTechIMS.Models;
using NovaTechIMS.Utilities;

namespace NovaTechIMS.Services;

/// <summary>
/// Customer business logic (FR-CUS, BR-010, VAL-007/008/013/014).
/// Forms call this layer only — no SQL in the UI.
/// </summary>
public class CustomerService
{
    private static readonly Regex PhonePattern = new(
        @"^[0-9+\-()\s]+$",
        RegexOptions.Compiled);

    private readonly CustomerRepository _repository = new();

    public IReadOnlyList<CustomerListRow> GetList(string? search = null, bool? isActiveFilter = null)
    {
        return _repository.GetList(search, isActiveFilter);
    }

    public Customer GetById(int customerId)
    {
        return _repository.GetById(customerId)
            ?? throw new NotFoundException("The selected customer no longer exists.");
    }

    public Customer Create(string name, string? phone, string? email, string? address)
    {
        ValidateFields(name, phone, email, address);

        var customer = new Customer
        {
            CustomerName = name.Trim(),
            Phone = NullIfWhiteSpace(phone),
            Email = NullIfWhiteSpace(email),
            Address = NullIfWhiteSpace(address),
            IsActive = true,
            CreatedDate = DateTime.UtcNow
        };

        customer.CustomerID = _repository.Insert(customer);
        return customer;
    }

    public void Update(
        int customerId,
        string name,
        string? phone,
        string? email,
        string? address,
        bool isActive)
    {
        ValidateFields(name, phone, email, address);

        var existing = GetById(customerId);
        existing.CustomerName = name.Trim();
        existing.Phone = NullIfWhiteSpace(phone);
        existing.Email = NullIfWhiteSpace(email);
        existing.Address = NullIfWhiteSpace(address);
        existing.IsActive = isActive;
        existing.ModifiedDate = DateTime.UtcNow;

        _repository.Update(existing);
    }

    /// <summary>
    /// Attempts hard delete. When Stock-Out transactions reference the customer (BR-010),
    /// throws <see cref="BusinessRuleException"/> so the UI can offer Mark Inactive.
    /// </summary>
    public void DeleteOrThrowIfReferenced(int customerId)
    {
        _ = GetById(customerId);

        var txnCount = _repository.CountStockOutTransactions(customerId);
        if (txnCount > 0)
        {
            throw new BusinessRuleException(
                $"This customer is referenced by {txnCount} Stock-Out transaction(s). " +
                "It cannot be hard-deleted. Mark it Inactive instead.");
        }

        if (!_repository.TryHardDelete(customerId))
        {
            throw new BusinessRuleException(
                "This customer cannot be deleted because it is still referenced. " +
                "Mark it Inactive instead.");
        }
    }

    public void Deactivate(int customerId)
    {
        _ = GetById(customerId);
        _repository.SoftDeactivate(customerId);
    }

    private static void ValidateFields(string name, string? phone, string? email, string? address)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Customer name is required.");

        if (name.Trim().Length > 150)
            throw new ValidationException("Customer name cannot exceed 150 characters.");

        if (!string.IsNullOrWhiteSpace(phone))
        {
            var p = phone.Trim();
            if (p.Length > 30)
                throw new ValidationException("Phone cannot exceed 30 characters.");
            if (!PhonePattern.IsMatch(p))
                throw new ValidationException(
                    "Phone may only contain digits, spaces, +, -, and parentheses.");
        }

        if (!string.IsNullOrWhiteSpace(email))
        {
            var e = email.Trim();
            if (e.Length > 100)
                throw new ValidationException("Email cannot exceed 100 characters.");
            if (!IsValidEmail(e))
                throw new ValidationException("Email format is not valid.");
        }

        // VAL-008 max 250; schema allows 300 — enforce the stricter validation rule.
        if (address is not null && address.Trim().Length > 250)
            throw new ValidationException("Address cannot exceed 250 characters.");
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address.Equals(email, StringComparison.OrdinalIgnoreCase);
        }
        catch
        {
            return false;
        }
    }

    private static string? NullIfWhiteSpace(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
