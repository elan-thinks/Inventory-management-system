using NovaTechIMS.Security;
using Xunit;

namespace NovaTechIMS.Tests.Security;

public class PasswordHasherTests
{
    [Fact]
    public void HashPassword_Produces_NonEmpty_Hash_And_Salt()
    {
        var (hash, salt) = PasswordHasher.HashPassword("Admin@123");

        Assert.False(string.IsNullOrWhiteSpace(hash));
        Assert.False(string.IsNullOrWhiteSpace(salt));
        Assert.NotEqual(hash, salt);
    }

    [Fact]
    public void HashPassword_Uses_Unique_Salt_Each_Call()
    {
        var a = PasswordHasher.HashPassword("same-password");
        var b = PasswordHasher.HashPassword("same-password");

        Assert.NotEqual(a.Salt, b.Salt);
        Assert.NotEqual(a.Hash, b.Hash);
    }

    [Fact]
    public void Verify_Succeeds_For_Correct_Password()
    {
        var (hash, salt) = PasswordHasher.HashPassword("Secret#1");
        Assert.True(PasswordHasher.Verify("Secret#1", hash, salt));
    }

    [Fact]
    public void Verify_Fails_For_Wrong_Password()
    {
        var (hash, salt) = PasswordHasher.HashPassword("Secret#1");
        Assert.False(PasswordHasher.Verify("wrong", hash, salt));
    }

    [Fact]
    public void Verify_Fails_For_Empty_Inputs()
    {
        Assert.False(PasswordHasher.Verify("", "x", "y"));
        Assert.False(PasswordHasher.Verify("p", "", "y"));
        Assert.False(PasswordHasher.Verify("p", "x", ""));
    }

    [Fact]
    public void HashPassword_Throws_On_Empty()
    {
        Assert.Throws<ArgumentException>(() => PasswordHasher.HashPassword(""));
    }
}
