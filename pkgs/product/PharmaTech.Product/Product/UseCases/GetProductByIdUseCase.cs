using PharmaTech.Core.Base;
using PharmaTech.Product.Product.Dtos.Responses;
using PharmaTech.Product.Product.Repositories;

namespace PharmaTech.Product.Product.UseCases;

public class GetProductByIdUseCase(IProductReadOnlyRepository readOnlyRepository)
    : IGetProductByIdUseCase
{
    public async Task<Result<ProductResponses>> ExecuteAsync(Guid id)
    {
        var product = await readOnlyRepository.GetProductByIdAsync(id);
        if (product is null)
            return Result<ProductResponses>.Failure("PRODUCT_NOT_FOUND");

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
