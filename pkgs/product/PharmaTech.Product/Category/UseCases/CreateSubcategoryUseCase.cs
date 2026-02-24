using PharmaTech.Core.Base;
using PharmaTech.Product.Category.Dtos.Requests;
using PharmaTech.Product.Category.Dtos.Responses;
using PharmaTech.Product.Category.Errors;
using PharmaTech.Product.Category.Models;
using PharmaTech.Product.Category.Repositories;

namespace PharmaTech.Product.Category.UseCases;

public class CreateSubcategoryUseCase(
    ICategoryWriteRepository writeRepository,
    ICategoryReadOnlyRepository readRepository
) : ICreateSubcategoryUseCase
{
    public async Task<Result<SubcategoryResponse>> ExecuteAsync(CreateSubcategoryRequest request, Guid categoryId)
    {
        var category = await readRepository.GetCategoryByIdAsync(categoryId);
        if (category is null)
            return Result<SubcategoryResponse>.Failure(CategoryErrors.NotFound);

        var subcategoryResult = Subcategory.Create(request.Name, request.Alias, categoryId);
        if (subcategoryResult.IsFailure)
            return Result<SubcategoryResponse>.Failure(subcategoryResult.Errors);

        await writeRepository.CreateSubCategoryAsync(subcategoryResult.Data);
        return SubcategoryResponse.FromModel(subcategoryResult.Data);
    }
}
