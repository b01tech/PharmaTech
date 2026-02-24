using PharmaTech.Core.Base;
using PharmaTech.Core.ValueObjects;

namespace PharmaTech.Core.Test.ValueObjects;

public class TextTests
{
    [Fact]
    public void Create_should_fail_when_value_is_null_or_whitespace()
    {
        var result = Text.Create(" ");

        Assert.True(result.IsFailure);
        Assert.Contains("TEXT_TOO_SHORT", result.Errors);
    }

    [Fact]
    public void Create_should_trim_value_and_succeed_within_default_limits()
    {
        var result = Text.Create(" value ");

        Assert.True(result.IsSuccess);
        Assert.Equal("value", result.Data.ToString());
    }

    [Fact]
    public void Create_should_respect_minimum_length()
    {
        var result = Text.Create("ab", min: 3);

        Assert.True(result.IsFailure);
        Assert.Contains("TEXT_TOO_SHORT", result.Errors);
    }

    [Fact]
    public void Create_should_respect_maximum_length()
    {
        var result = Text.Create("abcd", max: 3);

        Assert.True(result.IsFailure);
        Assert.Contains("TEXT_TOO_LONG", result.Errors);
    }
}
