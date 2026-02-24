using PharmaTech.Core.Base;
using PharmaTech.Product.Category.Errors;
using PharmaTech.Product.Category.Repositories;

namespace PharmaTech.Product.Category.UseCases;

public class RemoveSubcategoryUseCase(
    ICategoryWriteRepository writeRepository,
    ICategoryReadOnlyRepository readRepository
) : IRemoveSubcategoryUseCase
{
    public async Task<Result> ExecuteAsync(Guid request)
    {
        var subcategory = await readRepository.GetSubCategoryByIdAsync(request);
        if (subcategory is null)
            return Result.Failure(SubCategoryErrors.NotFound);

        await writeRepository.RemoveSubCategoryAsync(request);
        return Result.Success();
    }
}
