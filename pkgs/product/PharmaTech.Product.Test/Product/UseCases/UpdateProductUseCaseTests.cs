using Moq;
using PharmaTech.Product.Category.Models;
using PharmaTech.Product.Category.Repositories;
using PharmaTech.Product.Product.Dtos.Requests;
using PharmaTech.Product.Product.Errors;
using PharmaTech.Product.Product.Repositories;
using PharmaTech.Product.Product.UseCases;
using Xunit;
using ProductModel = PharmaTech.Product.Product.Models.Product;

namespace PharmaTech.Product.Test.Product.UseCases;

public class UpdateProductUseCaseTests
{
    private readonly Mock<IProductWriteRepository> _writeRepositoryMock;
    private readonly Mock<IProductReadOnlyRepository> _readRepositoryMock;
    private readonly Mock<ICategoryReadOnlyRepository> _categoryReadOnlyRepositoryMock;
    private readonly UpdateProductUseCase _useCase;

    public UpdateProductUseCaseTests()
    {
        _writeRepositoryMock = new Mock<IProductWriteRepository>();
        _readRepositoryMock = new Mock<IProductReadOnlyRepository>();
        _categoryReadOnlyRepositoryMock = new Mock<ICategoryReadOnlyRepository>();
        _useCase = new UpdateProductUseCase(_readRepositoryMock.Object, _writeRepositoryMock.Object, _categoryReadOnlyRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldUpdateProduct_WhenProductExistsAndInputIsValid()
    {
        // Arrange
        var productId = Guid.CreateVersion7();
        var categoryId = Guid.CreateVersion7();
        var subcategoryId = Guid.CreateVersion7();
        var existingProduct = ProductModel.Create("Old Name", "old-alias", "Old Desc", "OLD-SKU", 10.0m, subcategoryId).Data;
        var subcategory = Subcategory.Create("Sub", "sub", categoryId).Data;

        _readRepositoryMock.Setup(x => x.GetProductByIdAsync(productId))
            .ReturnsAsync(existingProduct);

        _categoryReadOnlyRepositoryMock.Setup(x => x.GetSubCategoryByIdAsync(subcategoryId))
            .ReturnsAsync(subcategory);

        var request = new UpdateProductRequest(productId, "New Name", "new-alias", "New Desc", "NEW-SKU", 20.0m, subcategoryId);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("New Name", result.Data.Name);
        _writeRepositoryMock.Verify(x => x.UpdateProductAsync(It.Is<ProductModel>(p => p.Name.Value == "New Name")), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        var productId = Guid.CreateVersion7();
        _readRepositoryMock.Setup(x => x.GetProductByIdAsync(productId))
            .ReturnsAsync((ProductModel?)null);

        var request = new UpdateProductRequest(productId, "Name", "alias", "Desc", "SKU", 10.0m, Guid.CreateVersion7());

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(ProductErrors.NotFound, result.Errors.First());
        _writeRepositoryMock.Verify(x => x.UpdateProductAsync(It.IsAny<ProductModel>()), Times.Never);
    }
}
