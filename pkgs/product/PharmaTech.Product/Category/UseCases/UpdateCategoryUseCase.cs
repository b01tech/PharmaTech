using PharmaTech.Core.Base;
using PharmaTech.Product.Category.Dtos.Requests;
using PharmaTech.Product.Category.Dtos.Responses;
using PharmaTech.Product.Category.Errors;
using PharmaTech.Product.Category.Repositories;

namespace PharmaTech.Product.Category.UseCases;

public class UpdateCategoryUseCase(
    ICategoryWriteRepository writeRepository,
    ICategoryReadOnlyRepository readRepository
) : IUpdateCategoryUseCase
{
    public async Task<Result<CategoryResponse>> ExecuteAsync(UpdateCategoryRequest request)
    {
        var category = await readRepository.GetCategoryByIdAsync(request.Id);
        if (category is null)
            return Result<CategoryResponse>.Failure(CategoryErrors.NotFound);

        var updateResult = category.Update(request.Name, request.Alias);
        if (updateResult.IsFailure)
            return Result<CategoryResponse>.Failure(updateResult.Errors);

        await writeRepository.UpdateAsync(category);
        return CategoryResponse.FromModel(category);
    }
}
