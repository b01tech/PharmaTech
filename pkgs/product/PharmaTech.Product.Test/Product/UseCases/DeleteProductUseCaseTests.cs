using Moq;
using PharmaTech.Product.Product.Errors;
using PharmaTech.Product.Product.Repositories;
using PharmaTech.Product.Product.UseCases;
using Xunit;
using ProductModel = PharmaTech.Product.Product.Models.Product;

namespace PharmaTech.Product.Test.Product.UseCases;

public class DeleteProductUseCaseTests
{
    private readonly Mock<IProductWriteRepository> _writeRepositoryMock;
    private readonly Mock<IProductReadOnlyRepository> _readRepositoryMock;
    private readonly DeleteProductUseCase _useCase;

    public DeleteProductUseCaseTests()
    {
        _writeRepositoryMock = new Mock<IProductWriteRepository>();
        _readRepositoryMock = new Mock<IProductReadOnlyRepository>();
        _useCase = new DeleteProductUseCase(_readRepositoryMock.Object, _writeRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldDeleteProduct_WhenProductExists()
    {
        // Arrange
        var productId = Guid.CreateVersion7();
        var existingProduct = ProductModel.Create("Name", "alias", "Description", "SKU", 10.0m, Guid.CreateVersion7()).Data;

        _readRepositoryMock.Setup(x => x.GetProductByIdAsync(productId))
            .ReturnsAsync(existingProduct);

        // Act
        var result = await _useCase.ExecuteAsync(productId);

        // Assert
        Assert.True(result.IsSuccess);
        _writeRepositoryMock.Verify(x => x.DeleteProductAsync(existingProduct), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        var productId = Guid.CreateVersion7();
        _readRepositoryMock.Setup(x => x.GetProductByIdAsync(productId))
            .ReturnsAsync((ProductModel?)null);

        // Act
        var result = await _useCase.ExecuteAsync(productId);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(ProductErrors.NotFound, result.Errors.First());
        _writeRepositoryMock.Verify(x => x.DeleteProductAsync(It.IsAny<ProductModel>()), Times.Never);
    }
}
