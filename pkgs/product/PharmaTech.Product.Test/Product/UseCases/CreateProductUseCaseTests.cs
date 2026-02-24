using Moq;
using PharmaTech.Core.ValueObjects;
using PharmaTech.Product.Category.Models;
using PharmaTech.Product.Category.Repositories;
using PharmaTech.Product.Product.Dtos.Requests;
using PharmaTech.Product.Product.Repositories;
using PharmaTech.Product.Product.UseCases;
using Xunit;
using ProductModel = PharmaTech.Product.Product.Models.Product;

namespace PharmaTech.Product.Test.Product.UseCases;

public class CreateProductUseCaseTests
{
    private readonly Mock<IProductWriteRepository> _writeRepositoryMock;
    private readonly Mock<ICategoryReadOnlyRepository> _categoryReadOnlyRepositoryMock;
    private readonly CreateProductUseCase _useCase;

    public CreateProductUseCaseTests()
    {
        _writeRepositoryMock = new Mock<IProductWriteRepository>();
        _categoryReadOnlyRepositoryMock = new Mock<ICategoryReadOnlyRepository>();
        _useCase = new CreateProductUseCase(_writeRepositoryMock.Object, _categoryReadOnlyRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldCreateProduct_WhenInputIsValid()
    {
        // Arrange
        var subcategoryId = Guid.CreateVersion7();
        var categoryId = Guid.CreateVersion7();
        var subcategory = Subcategory.Create("Sub", "sub", categoryId).Data;

        _categoryReadOnlyRepositoryMock.Setup(x => x.GetSubCategoryByIdAsync(subcategoryId))
            .ReturnsAsync(subcategory);

        var request = new CreateProductRequest("Product 1", "prod-1", "Description", "SKU-123", 10.0m, subcategoryId);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Product 1", result.Data.Name);
        _writeRepositoryMock.Verify(x => x.CreateProductAsync(It.IsAny<ProductModel>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFailure_WhenInputIsInvalid()
    {
        // Arrange
        var subcategoryId = Guid.CreateVersion7();
        var categoryId = Guid.CreateVersion7();
        var subcategory = Subcategory.Create("Sub", "sub", categoryId).Data;

        _categoryReadOnlyRepositoryMock.Setup(x => x.GetSubCategoryByIdAsync(subcategoryId))
            .ReturnsAsync(subcategory);

        var request = new CreateProductRequest("", "", "", "", 10.0m, subcategoryId);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.True(result.IsFailure);
        _writeRepositoryMock.Verify(x => x.CreateProductAsync(It.IsAny<ProductModel>()), Times.Never);
    }
}
