using PharmaTech.Core.Base;
using PharmaTech.Product.Category.Repositories;
using PharmaTech.Product.Product.Dtos.Requests;
using PharmaTech.Product.Product.Dtos.Responses;
using PharmaTech.Product.Product.Repositories;

namespace PharmaTech.Product.Product.UseCases;

public class CreateProductUseCase(
    IProductWriteRepository productWriteRepository,
    ICategoryReadOnlyRepository categoryReadOnlyRepository)
    : ICreateProductUseCase
{
    public async Task<Result<ProductResponses>> ExecuteAsync(CreateProductRequest request)
    {
        var subcategory = await categoryReadOnlyRepository.GetSubCategoryByIdAsync(request.SubcategoryId);
        if (subcategory is null)
            return Result<ProductResponses>.Failure("SUBCATEGORY_NOT_FOUND");

        var productResult = Models.Product.Create(
            request.Name,
            request.Alias,
            request.Description,
            request.Sku,
            request.Price,
            request.SubcategoryId
        );

        if (productResult.IsFailure)
            return Result<ProductResponses>.Failure(productResult.Errors);

        await productWriteRepository.CreateProductAsync(productResult.Data);

        return new ProductResponses(
            productResult.Data.Id,
            productResult.Data.Name.Value,
            productResult.Data.Alias.Value,
            productResult.Data.Description.Value,
            productResult.Data.Sku.Value,
            productResult.Data.Price,
            productResult.Data.SubcategoryId
        );
    }
}
