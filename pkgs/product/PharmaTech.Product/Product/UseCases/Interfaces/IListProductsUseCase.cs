using PharmaTech.Core.Base;
using PharmaTech.Product.Product.Dtos.Requests;
using PharmaTech.Product.Product.Dtos.Responses;

namespace PharmaTech.Product.Product.UseCases;

public interface IListProductsUseCase : IUseCase<ListProductsRequest, Result<ProductsListResponse>>
{
}
