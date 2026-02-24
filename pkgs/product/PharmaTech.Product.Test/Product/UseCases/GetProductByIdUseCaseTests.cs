using Moq;
using PharmaTech.Product.Product.Errors;
using PharmaTech.Product.Product.Repositories;
using PharmaTech.Product.Product.UseCases;
using Xunit;
using ProductModel = PharmaTech.Product.Product.Models.Product;

namespace PharmaTech.Product.Test.Product.UseCases;

public class GetProductByIdUseCaseTests
{
    private readonly Mock<IProductReadOnlyRepository> _readRepositoryMock;
    private readonly GetProductByIdUseCase _useCase;

    public GetProductByIdUseCaseTests()
    {
        _readRepositoryMock = new Mock<IProductReadOnlyRepository>();
        _useCase = new GetProductByIdUseCase(_readRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnProduct_WhenProductExists()
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
        Assert.Equal(existingProduct.Name.Value, result.Data.Name);
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
    }
}
