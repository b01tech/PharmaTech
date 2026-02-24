using PharmaTech.Core.Base;
using PharmaTech.Product.Category.Dtos.Requests;
using PharmaTech.Product.Category.Dtos.Responses;

namespace PharmaTech.Product.Category.UseCases;

public interface IFindAllCategoriesUseCase : IUseCase<ListCategoriesRequest, Result<IEnumerable<CategoryResponse>>>
{
}
