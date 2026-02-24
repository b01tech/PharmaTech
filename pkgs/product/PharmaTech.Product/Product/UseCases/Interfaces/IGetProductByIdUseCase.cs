using PharmaTech.Core.Base;
using PharmaTech.Product.Product.Dtos.Responses;

namespace PharmaTech.Product.Product.UseCases;

public interface IGetProductByIdUseCase : IUseCase<Guid, Result<ProductResponses>>
{
}
