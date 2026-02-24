using Moq;
using PharmaTech.Product.Category.Errors;
using PharmaTech.Product.Category.Repositories;
using PharmaTech.Product.Category.UseCases;
using Xunit;
using CategoryModel = PharmaTech.Product.Category.Models.Category;

namespace PharmaTech.Product.Test.Category.UseCases;

public class FindCategoryByIdUseCaseTests
{
    private readonly Mock<ICategoryReadOnlyRepository> _readRepositoryMock;
    private readonly FindCategoryByIdUseCase _useCase;

    public FindCategoryByIdUseCaseTests()
    {
        _readRepositoryMock = new Mock<ICategoryReadOnlyRepository>();
        _useCase = new FindCategoryByIdUseCase(_readRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnCategory_WhenCategoryExists()
    {
        // Arrange
        var categoryId = Guid.CreateVersion7();
        var existingCategory = CategoryModel.Create("Valid Name", "valid-alias").Data;
        _readRepositoryMock.Setup(x => x.GetCategoryByIdAsync(categoryId))
            .ReturnsAsync(existingCategory);

        // Act
        var result = await _useCase.ExecuteAsync(categoryId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(existingCategory.Name.Value, result.Data.Name);
        Assert.Equal(existingCategory.Alias.Value, result.Data.Alias);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnNotFound_WhenCategoryDoesNotExist()
    {
        // Arrange
        var categoryId = Guid.CreateVersion7();
        _readRepositoryMock.Setup(x => x.GetCategoryByIdAsync(categoryId))
            .ReturnsAsync((CategoryModel?)null);

        // Act
        var result = await _useCase.ExecuteAsync(categoryId);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(CategoryErrors.NotFound, result.Errors.First());
    }
}
