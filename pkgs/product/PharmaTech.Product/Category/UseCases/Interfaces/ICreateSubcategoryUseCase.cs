using PharmaTech.Core.Base;
using PharmaTech.Product.Category.Dtos.Requests;
using PharmaTech.Product.Category.Dtos.Responses;

namespace PharmaTech.Product.Category.UseCases;

public interface ICreateSubcategoryUseCase
{
    Task<Result<SubcategoryResponse>> ExecuteAsync(CreateSubcategoryRequest request, Guid categoryId);
}
