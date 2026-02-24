using PharmaTech.Core.ValueObjects;

namespace PharmaTech.Core.Test.ValueObjects;

public class SkuTests
{
    [Fact]
    public void Create_should_fail_when_value_is_null_or_whitespace()
    {
        var result = Sku.Create(" ");

        Assert.True(result.IsFailure);
        Assert.Contains("INVALID_SKU", result.Errors);
    }

    [Fact]
    public void Create_should_fail_when_value_exceeds_max_length()
    {
        var value = new string('a', 51);

        var result = Sku.Create(value);

        Assert.True(result.IsFailure);
        Assert.Contains("INVALID_SKU", result.Errors);
    }

    [Fact]
    public void Create_should_succeed_for_valid_value_and_normalize_to_uppercase()
    {
        var result = Sku.Create("abc-123");

        Assert.True(result.IsSuccess);
        Assert.Equal("ABC-123", result.Data.Value);
    }

    [Fact]
    public void Create_should_trim_value_before_validation()
    {
        var result = Sku.Create("  abc  ");

        Assert.True(result.IsSuccess);
        Assert.Equal("ABC", result.Data.Value);
    }

    [Fact]
    public void Create_should_fail_when_first_character_is_invalid()
    {
        var result = Sku.Create("!abc");

        Assert.True(result.IsFailure);
        Assert.Contains("INVALID_SKU", result.Errors);
    }

    [Fact]
    public void Create_should_accept_numbers_and_hyphens()
    {
        var result = Sku.Create("123-45");

        Assert.True(result.IsSuccess);
        Assert.Equal("123-45", result.Data.Value);
    }
}
