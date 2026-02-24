using Moq;
using PharmaTech.Product.Category.Errors;
using PharmaTech.Product.Category.Models;
using PharmaTech.Product.Category.Repositories;
using PharmaTech.Product.Category.UseCases;
using Xunit;
using CategoryModel = PharmaTech.Product.Category.Models.Category;

namespace PharmaTech.Product.Test.Category.UseCases;

public class FindSubcategoriesUseCaseTests
{
    private readonly Mock<ICategoryReadOnlyRepository> _readRepositoryMock;
    private readonly FindSubcategoriesUseCase _useCase;

    public FindSubcategoriesUseCaseTests()
    {
        _readRepositoryMock = new Mock<ICategoryReadOnlyRepository>();
        _useCase = new FindSubcategoriesUseCase(_readRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnSubcategories_WhenCategoryExists()
    {
        // Arrange
        var categoryId = Guid.CreateVersion7();
        var category = CategoryModel.Create("Cat 1", "cat-1").Data;
        var subcategory = Subcategory.Create("Sub 1", "sub-1", categoryId).Data;
        category.Add(subcategory);

        _readRepositoryMock.Setup(x => x.GetAllSubCategoriesAsync(categoryId))
            .ReturnsAsync(category);

        // Act
        var result = await _useCase.ExecuteAsync(categoryId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Single(result.Data);
        Assert.Equal("Sub 1", result.Data.First().Name);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnNotFound_WhenCategoryDoesNotExist()
    {
        // Arrange
        var categoryId = Guid.CreateVersion7();
        _readRepositoryMock.Setup(x => x.GetAllSubCategoriesAsync(categoryId))
            .ReturnsAsync((CategoryModel?)null);

        // Act
        var result = await _useCase.ExecuteAsync(categoryId);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(CategoryErrors.NotFound, result.Errors.First());
    }
}
