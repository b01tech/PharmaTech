using PharmaTech.Core.Base;
using PharmaTech.Product.Category.Dtos.Responses;

namespace PharmaTech.Product.Category.UseCases;

public interface IFindCategoryByIdUseCase : IUseCase<Guid, Result<CategoryDetailedResponse>>
{
}
