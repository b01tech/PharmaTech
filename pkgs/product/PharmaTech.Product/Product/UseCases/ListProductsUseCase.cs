using PharmaTech.Core.Base;
using PharmaTech.Product.Product.Dtos.Requests;
using PharmaTech.Product.Product.Dtos.Responses;
using PharmaTech.Product.Product.Repositories;

namespace PharmaTech.Product.Product.UseCases;

public class ListProductsUseCase(IProductReadOnlyRepository readOnlyRepository)
    : IListProductsUseCase
{
    public async Task<Result<ProductsListResponse>> ExecuteAsync(ListProductsRequest request)
    {
        var products = await readOnlyRepository.GetAllProductsWithFilterAsync(
            null,
            request.CategoryId,
            request.SubcategoryId,
            request.Page,
            request.PageSize
        );

        var (totalProducts, _, _) = await readOnlyRepository.GetTotalAsync();

        var response = new ProductsListResponse
        {
            Page = request.Page,
            PageSize = request.PageSize,
            TotalItems = totalProducts,
            Items = products.Select(p => new ProductResponses(
                p.Id,
                p.Name.Value,
                p.Alias.Value,
                p.Description.Value,
                p.Sku.Value,
                p.Price,
                p.SubcategoryId
            ))
        };

        return response;
    }
}
