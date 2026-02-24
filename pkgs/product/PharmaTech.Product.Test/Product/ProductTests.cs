using PharmaTech.Product.Product.Models;
using ProductModel = PharmaTech.Product.Product.Models.Product;

namespace PharmaTech.Product.Test.ProductModels;

public class ProductTests
{
    [Fact]
    public void Create_should_fail_when_name_is_invalid()
    {
        var result = ProductModel.Create("ab", "Valid Alias", new string('a', 50), "abc-123", 10m, Guid.CreateVersion7());

        Assert.True(result.IsFailure);
        Assert.Contains("NAME_TOO_SHORT", result.Errors);
    }

    [Fact]
    public void Create_should_fail_when_alias_is_invalid()
    {
        var result = ProductModel.Create("Valid Name", " ", new string('a', 50), "abc-123", 10m, Guid.CreateVersion7());

        Assert.True(result.IsFailure);
        Assert.Contains("INVALID_ALIAS", result.Errors);
    }

    [Fact]
    public void Create_should_fail_when_description_is_invalid()
    {
        var result = ProductModel.Create(
            "Valid Name",
            "Valid Alias",
            "abcd",
            "abc-123",
            10m,
            Guid.CreateVersion7()
        );

        Assert.True(result.IsFailure);
        Assert.Contains("DESCRIPTION_TOO_SHORT", result.Errors);
    }

    [Fact]
    public void Create_should_fail_when_sku_is_invalid()
    {
        var result = ProductModel.Create("Valid Name", "Valid Alias", new string('a', 50), " ", 10m, Guid.CreateVersion7());

        Assert.True(result.IsFailure);
        Assert.Contains("INVALID_SKU", result.Errors);
    }

    [Fact]
    public void Create_should_fail_with_combined_errors_when_all_are_invalid()
    {
        var result = ProductModel.Create("ab", " ", "abcd", " ", 10m, Guid.CreateVersion7());

        Assert.True(result.IsFailure);
        Assert.Contains("NAME_TOO_SHORT", result.Errors);
        Assert.Contains("INVALID_ALIAS", result.Errors);
        Assert.Contains("DESCRIPTION_TOO_SHORT", result.Errors);
        Assert.Contains("INVALID_SKU", result.Errors);
    }

    [Fact]
    public void Create_should_succeed_for_valid_inputs()
    {
        var subcategoryId = Guid.CreateVersion7();

        var result = ProductModel.Create(
            "Valid Name",
            "Valid Alias",
            new string('a', 50),
            "abc-123",
            99.9m,
            subcategoryId
        );

        Assert.True(result.IsSuccess);
        Assert.Equal("valid-alias", result.Data.Alias.Value);
        Assert.Equal("ABC-123", result.Data.Sku.Value);
        Assert.Equal(99.9m, result.Data.Price);
        Assert.Equal(subcategoryId, result.Data.SubcategoryId);
    }
}
