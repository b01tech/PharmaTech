using PharmaTech.Shared.Core;
using PharmaTech.Shared.ValueObjects;

namespace PharmaTech.Shared.Tests.ValueObjects;

public class TextTests
{
    [Fact]
    public void Create_WithValidValue_Should_Return_Success()
    {
        var result = Text.Create("Hello World");

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal("Hello World", result.Data.Value);
    }

    [Fact]
    public void Create_WithValidValue_Should_Trim()
    {
        var result = Text.Create("  Hello World  ");

        Assert.True(result.IsSuccess);
        Assert.Equal("Hello World", result.Data.Value);
    }

    [Fact]
    public void Create_WithNull_Should_Return_Failure()
    {
        var result = Text.Create(null);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal("TEXT_TOO_SHORT", result.Errors[0]);
    }

    [Fact]
    public void Create_WithWhitespace_Should_Return_Failure()
    {
        var result = Text.Create("   ");

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal("TEXT_TOO_SHORT", result.Errors[0]);
    }

    [Fact]
    public void Create_BelowMinLength_Should_Return_Failure()
    {
        var result = Text.Create("ab", min: 3, max: 10, tag: "TEST");

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal("TEST_TOO_SHORT", result.Errors[0]);
    }

    [Fact]
    public void Create_AboveMaxLength_Should_Return_Failure()
    {
        var result = Text.Create("abcdefghij", min: 1, max: 5, tag: "TEST");

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal("TEST_TOO_LONG", result.Errors[0]);
    }

    [Fact]
    public void ToString_Should_Return_Value()
    {
        var result = Text.Create("Test Value");

        Assert.True(result.IsSuccess);
        Assert.Equal("Test Value", result.Data.ToString());
    }

    [Fact]
    public void Value_Property_Should_Return_StoredValue()
    {
        var result = Text.Create("Stored Value");

        Assert.True(result.IsSuccess);
        Assert.Equal("Stored Value", result.Data.Value);
    }

    [Fact]
    public void Create_AtExactMinLength_Should_Return_Success()
    {
        var result = Text.Create("abc", min: 3, max: 10, tag: "TEST");

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Create_AtExactMaxLength_Should_Return_Success()
    {
        var result = Text.Create("abcde", min: 1, max: 5, tag: "TEST");

        Assert.True(result.IsSuccess);
    }
}