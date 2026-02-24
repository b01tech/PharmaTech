using PharmaTech.Core.Base;
using PharmaTech.Product.Category.Dtos.Requests;
using PharmaTech.Product.Category.Dtos.Responses;
using PharmaTech.Product.Category.Repositories;

namespace PharmaTech.Product.Category.UseCases;

public class FindAllCategoriesUseCase(ICategoryReadOnlyRepository readRepository) : IFindAllCategoriesUseCase
{
    public async Task<Result<IEnumerable<CategoryResponse>>> ExecuteAsync(ListCategoriesRequest request)
    {
        var categories = await readRepository.GetAllCategoriesAsync(request.Page, request.PageSize);
        return Result<IEnumerable<CategoryResponse>>.Success(categories.Select(CategoryResponse.FromModel));
    }
}
