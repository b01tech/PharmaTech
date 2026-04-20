using PharmaTech.Shared.Core;
using PharmaTech.Shared.ValueObjects;

namespace PharmaTech.Shared.Tests.ValueObjects;

public class EmailTests
{
    [Fact]
    public void Create_WithValidEmail_Should_Return_Success()
    {
        var result = Email.Create("test@example.com");

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal("test@example.com", result.Data.Address);
    }

    [Fact]
    public void Create_WithValidEmail_Should_TrimAndLowercase()
    {
        var result = Email.Create("  TEST@EXAMPLE.COM  ");

        Assert.True(result.IsSuccess);
        Assert.Equal("test@example.com", result.Data.Address);
    }

    [Fact]
    public void Create_WithNull_Should_Return_Failure()
    {
        var result = Email.Create(null);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal("Email address is empty", result.Errors[0]);
    }

    [Fact]
    public void Create_WithWhitespace_Should_Return_Failure()
    {
        var result = Email.Create("   ");

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal("Email address is empty", result.Errors[0]);
    }

    [Fact]
    public void Create_WithEmptyString_Should_Return_Failure()
    {
        var result = Email.Create("");

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal("Email address is empty", result.Errors[0]);
    }

    [Fact]
    public void Create_WithInvalidFormat_Should_Return_Failure()
    {
        var result = Email.Create("not-an-email");

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal("Email address is invalid", result.Errors[0]);
    }

    [Fact]
    public void Create_WithoutAtSign_Should_Return_Failure()
    {
        var result = Email.Create("testexample.com");

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal("Email address is invalid", result.Errors[0]);
    }

    [Fact]
    public void Create_WithoutDomain_Should_Return_Failure()
    {
        var result = Email.Create("test@");

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal("Email address is invalid", result.Errors[0]);
    }

    [Fact]
    public void Create_WithTooLongEmail_Should_Return_Failure()
    {
        var longEmail = new string('a', 243) + "@example.com";
        var result = Email.Create(longEmail);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal("Email address is too long", result.Errors[0]);
    }

    [Fact]
    public void Create_AtMaxLength_Should_Return_Success()
    {
        var maxEmail = new string('a', 239) + "@example.com";
        var result = Email.Create(maxEmail);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void ToString_Should_Return_Address()
    {
        var result = Email.Create("test@example.com");

        Assert.True(result.IsSuccess);
        Assert.Equal("test@example.com", result.Data.ToString());
    }

    [Fact]
    public void Address_Property_Should_Return_Lowercase()
    {
        var result = Email.Create("TEST@EXAMPLE.COM");

        Assert.True(result.IsSuccess);
        Assert.Equal("test@example.com", result.Data.Address);
    }

    [Fact]
    public void Create_WithDotInLocalPart_Should_Return_Success()
    {
        var result = Email.Create("john.doe@example.com");

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Create_WithPlusInLocalPart_Should_Return_Success()
    {
        var result = Email.Create("test+tag@example.com");

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Create_WithSubdomain_Should_Return_Success()
    {
        var result = Email.Create("test@mail.example.com");

        Assert.True(result.IsSuccess);
    }
}
