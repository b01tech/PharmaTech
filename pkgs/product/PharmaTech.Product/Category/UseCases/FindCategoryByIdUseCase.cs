using PharmaTech.Core.Base;
using PharmaTech.Product.Category.Dtos.Responses;
using PharmaTech.Product.Category.Errors;
using PharmaTech.Product.Category.Repositories;

namespace PharmaTech.Product.Category.UseCases;

public class FindCategoryByIdUseCase(ICategoryReadOnlyRepository readRepository) : IFindCategoryByIdUseCase
{
    public async Task<Result<CategoryDetailedResponse>> ExecuteAsync(Guid id)
    {
        var category = await readRepository.GetCategoryByIdAsync(id);
        if (category is null)
            return Result<CategoryDetailedResponse>.Failure(CategoryErrors.NotFound);

        return new CategoryDetailedResponse(
            category.Id,
            category.Name.Value,
            category.Alias.Value,
            category.CreatedAt,
            category.Subcategories.Select(s => new SubCategoryDetailedResponse(s.Id, s.Name.Value, s.Alias.Value))
        );
    }
}
