using PharmaTech.Core.Base;
using PharmaTech.Product.Category.Errors;
using PharmaTech.Product.Category.Repositories;

namespace PharmaTech.Product.Category.UseCases;

public class DeleteCategoryUseCase(ICategoryWriteRepository writeRepository, ICategoryReadOnlyRepository readRepository)
    : IDeleteCategoryUseCase
{
    public async Task<Result> ExecuteAsync(Guid id)
    {
        var category = await readRepository.GetCategoryByIdAsync(id);
        if (category is null)
            return Result.Failure(CategoryErrors.NotFound);

        await writeRepository.RemoveAsync(id);
        return Result.Success();
    }
}
