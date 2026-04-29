using PharmaTech.Shared.ValueObjects;

namespace PharmaTech.Shared.Test.ValueObjects;

public class DescriptionTests
{
    [Fact]
    public void Create_WithValidDescription_Should_Return_Success()
    {
        var result = Description.Create("This is a valid description");

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal("This is a valid description", result.Data.Value);
    }

    [Fact]
    public void Create_WithValidDescription_Should_Trim()
    {
        var result = Description.Create("  This is a description  ");

        Assert.True(result.IsSuccess);
        Assert.Equal("This is a description", result.Data.Value);
    }

    [Fact]
    public void Create_BelowMinLength_Should_Return_Failure()
    {
        var result = Description.Create("abcd");

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal("DESCRIPTION_TOO_SHORT", result.Errors[0]);
    }

    [Fact]
    public void Create_AboveMaxLength_Should_Return_Failure()
    {
        var result = Description.Create(new string('a', 256));

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal("DESCRIPTION_TOO_LONG", result.Errors[0]);
    }

    [Fact]
    public void Create_WithNull_Should_Return_Failure()
    {
        var result = Description.Create(null);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal("DESCRIPTION_TOO_SHORT", result.Errors[0]);
    }

    [Fact]
    public void Create_WithWhitespace_Should_Return_Failure()
    {
        var result = Description.Create("   ");

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal("DESCRIPTION_TOO_SHORT", result.Errors[0]);
    }

    [Fact]
    public void Create_AtExactMinLength_Should_Return_Success()
    {
        var result = Description.Create("abcde");

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Create_AtExactMaxLength_Should_Return_Success()
    {
        var result = Description.Create(new string('a', 255));

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void ToString_Should_Return_Value()
    {
        var result = Description.Create("Test Description");

        Assert.True(result.IsSuccess);
        Assert.Equal("Test Description", result.Data.ToString());
    }
}
