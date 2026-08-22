using System;
using System.Security.Cryptography;

namespace NovaTechIMS.Security;

/// <summary>
/// PBKDF2 password hashing (ADR-004 / FR-AUTH-007).
/// Salt is random per password; never store plain text.
/// </summary>
public static class PasswordHasher
{
    private const int SaltSize = 16;       // 128-bit
    private const int KeySize = 32;        // 256-bit derived key
    private const int Iterations = 100_000;

    /// <summary>
    /// Creates a new random salt and derives the hash for <paramref name="password"/>.
    /// Both values are returned as Base64 strings for database storage.
    /// </summary>
    public static (string Hash, string Salt) HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            throw new ArgumentException("Password is required.", nameof(password));

        var saltBytes = RandomNumberGenerator.GetBytes(SaltSize);
        var hashBytes = Rfc2898DeriveBytes.Pbkdf2(
            password,
            saltBytes,
            Iterations,
            HashAlgorithmName.SHA256,
            KeySize);

        return (Convert.ToBase64String(hashBytes), Convert.ToBase64String(saltBytes));
    }

    /// <summary>
    /// Verifies <paramref name="password"/> against the stored Base64 hash and salt.
    /// Uses fixed-time comparison to reduce timing side-channels.
    /// </summary>
    public static bool Verify(string password, string storedHash, string storedSalt)
    {
        if (string.IsNullOrEmpty(password)
            || string.IsNullOrEmpty(storedHash)
            || string.IsNullOrEmpty(storedSalt))
            return false;

        try
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var expectedHash = Convert.FromBase64String(storedHash);
            var actualHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                saltBytes,
                Iterations,
                HashAlgorithmName.SHA256,
                KeySize);

            return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
