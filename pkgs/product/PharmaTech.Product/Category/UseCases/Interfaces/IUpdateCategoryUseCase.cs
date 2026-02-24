using PharmaTech.Core.Base;
using PharmaTech.Product.Category.Dtos.Requests;
using PharmaTech.Product.Category.Dtos.Responses;

namespace PharmaTech.Product.Category.UseCases;

public interface IUpdateCategoryUseCase : IUseCase<UpdateCategoryRequest, Result<CategoryResponse>>
{
}
