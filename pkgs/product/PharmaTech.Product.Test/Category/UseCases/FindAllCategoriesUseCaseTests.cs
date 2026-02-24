using Moq;
using PharmaTech.Product.Category.Dtos.Requests;
using PharmaTech.Product.Category.Repositories;
using PharmaTech.Product.Category.UseCases;
using Xunit;
using CategoryModel = PharmaTech.Product.Category.Models.Category;

namespace PharmaTech.Product.Test.Category.UseCases;

public class FindAllCategoriesUseCaseTests
{
    private readonly Mock<ICategoryReadOnlyRepository> _readRepositoryMock;
    private readonly FindAllCategoriesUseCase _useCase;

    public FindAllCategoriesUseCaseTests()
    {
        _readRepositoryMock = new Mock<ICategoryReadOnlyRepository>();
        _useCase = new FindAllCategoriesUseCase(_readRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnCategories_WhenCalled()
    {
        // Arrange
        var categories = new List<CategoryModel>
        {
            CategoryModel.Create("Cat 1", "cat-1").Data,
            CategoryModel.Create("Cat 2", "cat-2").Data
        };
        _readRepositoryMock.Setup(x => x.GetAllCategoriesAsync(1, 25))
            .ReturnsAsync(categories);

        var request = new ListCategoriesRequest(1, 25);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Data.Count());
    }
}
