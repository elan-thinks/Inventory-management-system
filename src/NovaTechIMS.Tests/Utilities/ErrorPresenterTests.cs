using System.Windows.Forms;
using NovaTechIMS.Utilities;
using Xunit;

namespace NovaTechIMS.Tests.Utilities;

public class ErrorPresenterTests
{
    [Fact]
    public void Validation_Uses_Warning_And_Same_Message()
    {
        var (msg, icon) = ErrorPresenter.Classify(new ValidationException("Name is required."));
        Assert.Equal("Name is required.", msg);
        Assert.Equal(MessageBoxIcon.Warning, icon);
    }

    [Fact]
    public void InsufficientStock_Uses_Warning()
    {
        var (msg, icon) = ErrorPresenter.Classify(new InsufficientStockException(3, 10));
        Assert.Contains("Available: 3", msg);
        Assert.Contains("requested: 10", msg);
        Assert.Equal(MessageBoxIcon.Warning, icon);
    }

    [Fact]
    public void Unauthorized_Uses_Generic_Message()
    {
        var (msg, _) = ErrorPresenter.Classify(
            new UnauthorizedException("internal detail should not leak"));
        Assert.Equal("You are not authorised to perform this action.", msg);
    }

    [Fact]
    public void Unknown_Exception_Does_Not_Leak_Details()
    {
        var (msg, icon) = ErrorPresenter.Classify(new InvalidOperationException("SELECT * FROM secret"));
        Assert.DoesNotContain("SELECT", msg);
        Assert.Equal(MessageBoxIcon.Error, icon);
    }

    [Fact]
    public void Authentication_Keeps_Generic_Login_Message()
    {
        var (msg, _) = ErrorPresenter.Classify(
            new AuthenticationException("Invalid username or password."));
        Assert.Equal("Invalid username or password.", msg);
    }
}
