using PharmaTech.Core.Base;
using PharmaTech.Product.Category.Dtos.Responses;

namespace PharmaTech.Product.Category.UseCases;

public interface IFindSubcategoriesUseCase : IUseCase<Guid, Result<IEnumerable<SubCategoryDetailedResponse>>>
{
}
