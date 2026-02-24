using PharmaTech.Product.Category.Models;
using SubcategoryModel = PharmaTech.Product.Category.Models.Subcategory;

namespace PharmaTech.Product.Test.CategoryModels;

public class SubcategoryTests
{
    [Fact]
    public void Create_should_fail_when_name_is_invalid()
    {
        var result = SubcategoryModel.Create("ab", "valid alias", Guid.CreateVersion7());

        Assert.True(result.IsFailure);
        Assert.Contains("NAME_TOO_SHORT", result.Errors);
    }

    [Fact]
    public void Create_should_fail_when_alias_is_invalid()
    {
        var result = SubcategoryModel.Create("Valid Name", " ", Guid.CreateVersion7());

        Assert.True(result.IsFailure);
        Assert.Contains("INVALID_ALIAS", result.Errors);
    }

    [Fact]
    public void Create_should_fail_with_combined_errors_when_both_are_invalid()
    {
        var result = SubcategoryModel.Create("ab", " ", Guid.CreateVersion7());

        Assert.True(result.IsFailure);
        Assert.Contains("NAME_TOO_SHORT", result.Errors);
        Assert.Contains("INVALID_ALIAS", result.Errors);
    }

    [Fact]
    public void Create_should_succeed_for_valid_inputs()
    {
        var categoryId = Guid.CreateVersion7();
        var result = SubcategoryModel.Create("Valid Name", "Valid Alias", categoryId);

        Assert.True(result.IsSuccess);
        Assert.Equal("valid-alias", result.Data.Alias.Value);
        Assert.Equal(categoryId, result.Data.CategoryId);
    }
}
