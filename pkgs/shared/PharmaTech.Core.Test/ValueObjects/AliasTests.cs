using PharmaTech.Core.ValueObjects;

namespace PharmaTech.Core.Test.ValueObjects;

public class AliasTests
{
    [Fact]
    public void Create_should_fail_when_value_is_null_or_whitespace()
    {
        var result = Alias.Create(" ");

        Assert.True(result.IsFailure);
        Assert.Contains("INVALID_ALIAS", result.Errors);
    }

    [Fact]
    public void Create_should_normalize_text_removing_accents_and_special_characters()
    {
        var result = Alias.Create(" Café com Leite! ");

        Assert.True(result.IsSuccess);
        Assert.Equal("cafe-com-leite", result.Data.Value);
        Assert.Equal("cafe-com-leite", result.Data.ToString());
    }

    [Fact]
    public void Create_should_collapse_multiple_spaces_and_hyphens()
    {
        var result = Alias.Create("  test   alias--value  ");

        Assert.True(result.IsSuccess);
        Assert.Equal("test-alias-value", result.Data.Value);
    }
}
