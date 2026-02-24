using PharmaTech.Core.Base;
using PharmaTech.Product.Category.Repositories;
using PharmaTech.Product.Product.Dtos.Requests;
using PharmaTech.Product.Product.Dtos.Responses;
using PharmaTech.Product.Product.Repositories;

namespace PharmaTech.Product.Product.UseCases;

public class UpdateProductUseCase(
    IProductReadOnlyRepository readOnlyRepository,
    IProductWriteRepository writeRepository,
    ICategoryReadOnlyRepository categoryReadOnlyRepository)
    : IUpdateProductUseCase
{
    public async Task<Result<ProductResponses>> ExecuteAsync(UpdateProductRequest request)
    {
        var product = await readOnlyRepository.GetProductByIdAsync(request.Id);
        if (product is null)
            return Result<ProductResponses>.Failure("PRODUCT_NOT_FOUND");

        var subcategory = await categoryReadOnlyRepository.GetSubCategoryByIdAsync(request.SubcategoryId);
        if (subcategory is null)
            return Result<ProductResponses>.Failure("SUBCATEGORY_NOT_FOUND");

        var updateResult = product.Update(
            request.Name,
            request.Alias,
            request.Description,
            request.Sku,
            request.Price,
            request.SubcategoryId
        );

        if (updateResult.IsFailure)
            return Result<ProductResponses>.Failure(updateResult.Errors);

        await writeRepository.UpdateProductAsync(product);

        return new ProductResponses(
            product.Id,
            product.Name.Value,
            product.Alias.Value,
            product.Description.Value,
            product.Sku.Value,
            product.Price,
            product.SubcategoryId
        );
    }
}
