using System;

namespace NovaTechIMS.Utilities;

/// <summary>Base application exception (Technical Design §11).</summary>
public class AppException : Exception
{
    public AppException(string message) : base(message) { }
    public AppException(string message, Exception inner) : base(message, inner) { }
}

/// <summary>Input or domain validation failure.</summary>
public class ValidationException : AppException
{
    public ValidationException(string message) : base(message) { }
}

/// <summary>Business rule prevented the operation.</summary>
public class BusinessRuleException : AppException
{
    public BusinessRuleException(string message) : base(message) { }
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

/// <summary>Caller lacks required permission (ADR-005).</summary>
public class UnauthorizedException : AppException
{
    public UnauthorizedException(string message) : base(message) { }
}
