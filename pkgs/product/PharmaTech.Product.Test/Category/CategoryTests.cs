using PharmaTech.Product.Category.Models;
using CategoryModel = PharmaTech.Product.Category.Models.Category;

namespace PharmaTech.Product.Test.CategoryModels;

public class CategoryTests
{
    [Fact]
    public void Create_should_fail_when_name_is_invalid()
    {
        var result = CategoryModel.Create("ab", "valid alias");

        Assert.True(result.IsFailure);
        Assert.Contains("NAME_TOO_SHORT", result.Errors);
    }

    [Fact]
    public void Create_should_fail_when_alias_is_invalid()
    {
        var result = CategoryModel.Create("Valid Name", " ");

        Assert.True(result.IsFailure);
        Assert.Contains("INVALID_ALIAS", result.Errors);
    }

    [Fact]
    public void Create_should_fail_with_combined_errors_when_both_are_invalid()
    {
        var result = CategoryModel.Create("ab", " ");

        Assert.True(result.IsFailure);
        Assert.Contains("NAME_TOO_SHORT", result.Errors);
        Assert.Contains("INVALID_ALIAS", result.Errors);
    }

    [Fact]
    public void Create_should_succeed_for_valid_inputs()
    {
        var result = CategoryModel.Create("Valid Name", "Valid Alias");

        Assert.True(result.IsSuccess);
        Assert.Equal("valid-alias", result.Data.Alias.Value);
        Assert.Empty(result.Data.Subcategories);
    }
}
