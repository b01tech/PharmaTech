using PharmaTech.Core.Base;
using PharmaTech.Product.Category.Dtos.Responses;
using PharmaTech.Product.Category.Errors;
using PharmaTech.Product.Category.Repositories;

namespace PharmaTech.Product.Category.UseCases;

public class FindSubcategoriesUseCase(ICategoryReadOnlyRepository readRepository) : IFindSubcategoriesUseCase
{
    public async Task<Result<IEnumerable<SubCategoryDetailedResponse>>> ExecuteAsync(Guid categoryId)
    {
        var category = await readRepository.GetAllSubCategoriesAsync(categoryId);
        if (category is null)
            return Result<IEnumerable<SubCategoryDetailedResponse>>.Failure(CategoryErrors.NotFound);

        return Result<IEnumerable<SubCategoryDetailedResponse>>.Success(
            category.Subcategories.Select(s => new SubCategoryDetailedResponse(s.Id, s.Name.Value, s.Alias.Value))
        );
    }
}
