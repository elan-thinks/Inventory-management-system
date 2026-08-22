using System;

namespace NovaTechIMS.Utilities;

/// <summary>
/// Base application exception (Technical Design §11).
/// Forms should catch these and never show stack traces to end users.
/// </summary>
public class AppException : Exception
{
    public AppException(string message) : base(message) { }
    public AppException(string message, Exception inner) : base(message, inner) { }
}

/// <summary>Input or domain validation failure (VAL-*).</summary>
public class ValidationException : AppException
{
    public ValidationException(string message) : base(message) { }
}

/// <summary>Business rule prevented the operation (BR-*).</summary>
public class BusinessRuleException : AppException
{
    public BusinessRuleException(string message) : base(message) { }
}

/// <summary>Stock-Out quantity exceeds QuantityOnHand (VAL-020 / BR-011).</summary>
public class InsufficientStockException : BusinessRuleException
{
    public int Available { get; }
    public int Requested { get; }

    public InsufficientStockException(int available, int requested)
        : base($"Insufficient stock. Available: {available}, requested: {requested}.")
    {
        Available = available;
        Requested = requested;
    }

    public InsufficientStockException(string message) : base(message)
    {
        Available = 0;
        Requested = 0;
    }
}

/// <summary>Unique constraint / duplicate name.</summary>
public class DuplicateRecordException : AppException
{
    public DuplicateRecordException(string message) : base(message) { }
}

/// <summary>Requested entity was not found.</summary>
public class NotFoundException : AppException
{
    public NotFoundException(string message) : base(message) { }
}

/// <summary>Login / credential failure (FR-AUTH-003).</summary>
public class AuthenticationException : AppException
{
    public AuthenticationException(string message) : base(message) { }
}

/// <summary>Caller lacks required permission (ADR-005). Shown as a simple authorised message.</summary>
public class UnauthorizedException : AppException
{
    public UnauthorizedException(string message) : base(message) { }
}

/// <summary>Alias name from technical design §11 (same behaviour as UnauthorizedException).</summary>
public class AuthorizationException : UnauthorizedException
{
    public AuthorizationException(string message) : base(message) { }
}

/// <summary>Unexpected data-access / infrastructure failure (mapped to a friendly message).</summary>
public class DataAccessException : AppException
{
    public DataAccessException(string message) : base(message) { }
    public DataAccessException(string message, Exception inner) : base(message, inner) { }
}
