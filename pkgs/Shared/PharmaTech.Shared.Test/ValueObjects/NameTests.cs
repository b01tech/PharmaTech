using PharmaTech.Shared.ValueObjects;

namespace PharmaTech.Shared.Test.ValueObjects;

public class NameTests
{
    [Fact]
    public void Create_WithValidName_Should_Return_Success()
    {
        var result = Name.Create("John Doe");

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal("John Doe", result.Data.Value);
    }

    [Fact]
    public void Create_WithValidName_Should_Trim()
    {
        var result = Name.Create("  John Doe  ");

        Assert.True(result.IsSuccess);
        Assert.Equal("John Doe", result.Data.Value);
    }

    [Fact]
    public void Create_BelowMinLength_Should_Return_Failure()
    {
        var result = Name.Create("Jo");

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal("NAME_TOO_SHORT", result.Errors[0]);
    }

    [Fact]
    public void Create_AboveMaxLength_Should_Return_Failure()
    {
        var result = Name.Create(new string('a', 51));

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal("NAME_TOO_LONG", result.Errors[0]);
    }

    [Fact]
    public void Create_WithNull_Should_Return_Failure()
    {
        var result = Name.Create(null);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal("NAME_TOO_SHORT", result.Errors[0]);
    }

    [Fact]
    public void Create_WithWhitespace_Should_Return_Failure()
    {
        var result = Name.Create("   ");

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal("NAME_TOO_SHORT", result.Errors[0]);
    }

    [Fact]
    public void Create_AtExactMinLength_Should_Return_Success()
    {
        var result = Name.Create("abc");

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Create_AtExactMaxLength_Should_Return_Success()
    {
        var result = Name.Create(new string('a', 50));

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void ToString_Should_Return_Value()
    {
        var result = Name.Create("Test Name");

        Assert.True(result.IsSuccess);
        Assert.Equal("Test Name", result.Data.ToString());
    }
}
