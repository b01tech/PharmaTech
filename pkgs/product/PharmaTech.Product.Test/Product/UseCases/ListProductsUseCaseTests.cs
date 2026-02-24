using System.Linq.Expressions;
using Moq;
using PharmaTech.Product.Product.Dtos.Requests;
using PharmaTech.Product.Product.Repositories;
using PharmaTech.Product.Product.UseCases;
using Xunit;
using ProductModel = PharmaTech.Product.Product.Models.Product;

namespace PharmaTech.Product.Test.Product.UseCases;

public class ListProductsUseCaseTests
{
    private readonly Mock<IProductReadOnlyRepository> _readRepositoryMock;
    private readonly ListProductsUseCase _useCase;

    public ListProductsUseCaseTests()
    {
        _readRepositoryMock = new Mock<IProductReadOnlyRepository>();
        _useCase = new ListProductsUseCase(_readRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnProducts_WhenCalled()
    {
        // Arrange
        var products = new List<ProductModel>
        {
            ProductModel.Create("Prod 1", "prod-1", "Description 1", "SKU1", 10.0m, Guid.CreateVersion7()).Data,
            ProductModel.Create("Prod 2", "prod-2", "Description 2", "SKU2", 20.0m, Guid.CreateVersion7()).Data
        };

        _readRepositoryMock.Setup(x => x.GetAllProductsWithFilterAsync(
                It.IsAny<Expression<Func<ProductModel, bool>>?>(),
                It.IsAny<Guid?>(),
                It.IsAny<Guid?>(),
                1,
                10))
            .ReturnsAsync(products);

        _readRepositoryMock.Setup(x => x.GetTotalAsync())
            .ReturnsAsync((2, 1, 1)); // totalProducts, totalCategories, totalSubcategories

        var request = new ListProductsRequest(1, 10, null, null);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Data.Items.Count());
        Assert.Equal(2, result.Data.TotalItems);
    }
}
