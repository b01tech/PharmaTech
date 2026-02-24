using PharmaTech.Core.ValueObjects;

namespace PharmaTech.Core.Test.ValueObjects;

public class NameTests
{
    [Fact]
    public void Create_should_fail_when_too_short()
    {
        var result = Name.Create("ab");

        Assert.True(result.IsFailure);
        Assert.Contains("NAME_TOO_SHORT", result.Errors);
    }

    [Fact]
    public void Create_should_fail_when_too_long()
    {
        var value = new string('a', 51);

        var result = Name.Create(value);

        Assert.True(result.IsFailure);
        Assert.Contains("NAME_TOO_LONG", result.Errors);
    }

    [Fact]
    public void Create_should_succeed_for_valid_length()
    {
        var result = Name.Create("John Doe");

        Assert.True(result.IsSuccess);
    }
}
