using Moq;
using PharmaTech.Product.Category.Dtos.Requests;
using PharmaTech.Product.Category.Errors;
using PharmaTech.Product.Category.Repositories;
using PharmaTech.Product.Category.UseCases;
using Xunit;
using CategoryModel = PharmaTech.Product.Category.Models.Category;

namespace PharmaTech.Product.Test.Category.UseCases;

public class UpdateCategoryUseCaseTests
{
    private readonly Mock<ICategoryWriteRepository> _writeRepositoryMock;
    private readonly Mock<ICategoryReadOnlyRepository> _readRepositoryMock;
    private readonly UpdateCategoryUseCase _useCase;

    public UpdateCategoryUseCaseTests()
    {
        _writeRepositoryMock = new Mock<ICategoryWriteRepository>();
        _readRepositoryMock = new Mock<ICategoryReadOnlyRepository>();
        _useCase = new UpdateCategoryUseCase(_writeRepositoryMock.Object, _readRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldUpdateCategory_WhenCategoryExistsAndInputIsValid()
    {
        // Arrange
        var categoryId = Guid.CreateVersion7();
        var existingCategory = CategoryModel.Create("Old Name", "old-alias").Data;

        _readRepositoryMock.Setup(x => x.GetCategoryByIdAsync(categoryId))
            .ReturnsAsync(existingCategory);

        var request = new UpdateCategoryRequest(categoryId, "New Name", "new-alias");

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("New Name", result.Data.Name);
        Assert.Equal("new-alias", result.Data.Alias);

        _writeRepositoryMock.Verify(x => x.UpdateAsync(It.Is<CategoryModel>(c => c.Name.Value == "New Name")), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnNotFound_WhenCategoryDoesNotExist()
    {
        // Arrange
        var categoryId = Guid.CreateVersion7();
        _readRepositoryMock.Setup(x => x.GetCategoryByIdAsync(categoryId))
            .ReturnsAsync((CategoryModel?)null);

        var request = new UpdateCategoryRequest(categoryId, "New Name", "new-alias");

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(CategoryErrors.NotFound, result.Errors.First());
        _writeRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<CategoryModel>()), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFailure_WhenInputIsInvalid()
    {
        // Arrange
        var categoryId = Guid.CreateVersion7();
        var existingCategory = CategoryModel.Create("Old Name", "old-alias").Data;
        _readRepositoryMock.Setup(x => x.GetCategoryByIdAsync(categoryId))
            .ReturnsAsync(existingCategory);

        var request = new UpdateCategoryRequest(categoryId, "", ""); // Invalid name/alias

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.True(result.IsFailure);
        _writeRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<CategoryModel>()), Times.Never);
    }
}
