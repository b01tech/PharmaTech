using Moq;
using PharmaTech.Product.Category.Dtos.Requests;
using PharmaTech.Product.Category.Repositories;
using PharmaTech.Product.Category.UseCases;
using Xunit;
using CategoryModel = PharmaTech.Product.Category.Models.Category;

namespace PharmaTech.Product.Test.Category.UseCases;

public class CreateCategoryUseCaseTests
{
    private readonly Mock<ICategoryWriteRepository> _writeRepositoryMock;
    private readonly CreateCategoryUseCase _useCase;

    public CreateCategoryUseCaseTests()
    {
        _writeRepositoryMock = new Mock<ICategoryWriteRepository>();
        _useCase = new CreateCategoryUseCase(_writeRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldCreateCategory_WhenInputIsValid()
    {
        // Arrange
        var request = new CreateCategoryRequest("Valid Name", "valid-alias");

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(request.Name, result.Data.Name);
        Assert.Equal(request.Alias, result.Data.Alias);
        _writeRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<CategoryModel>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFailure_WhenInputIsInvalid()
    {
        // Arrange
        var request = new CreateCategoryRequest("", "");

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.True(result.IsFailure);
        _writeRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<CategoryModel>()), Times.Never);
    }
}
