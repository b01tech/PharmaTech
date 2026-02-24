using PharmaTech.Core.ValueObjects;

namespace PharmaTech.Core.Test.ValueObjects;

public class DescriptionTests
{
    [Fact]
    public void Create_should_fail_when_too_short()
    {
        var result = Description.Create("short description");

        Assert.True(result.IsFailure);
        Assert.Contains("DESCRIPTION_TOO_SHORT", result.Errors);
    }

    [Fact]
    public void Create_should_fail_when_too_long()
    {
        var value = new string('a', 256);

        var result = Description.Create(value);

        Assert.True(result.IsFailure);
        Assert.Contains("DESCRIPTION_TOO_LONG", result.Errors);
    }

    [Fact]
    public void Create_should_succeed_for_valid_length()
    {
        var value = new string('a', 50);

        var result = Description.Create(value);

        Assert.True(result.IsSuccess);
    }
}
