using PharmaTech.Core.Base;
using PharmaTech.Product.Category.Dtos.Requests;
using PharmaTech.Product.Category.Dtos.Responses;
using PharmaTech.Product.Category.Repositories;

namespace PharmaTech.Product.Category.UseCases;

public class CreateCategoryUseCase(ICategoryWriteRepository writeRepository)
    : IUseCase<CreateCategoryRequest, Result<CategoryResponse>>
{
    public async Task<Result<CategoryResponse>> ExecuteAsync(CreateCategoryRequest request)
    {
        var categoryResult = Models.Category.Create(request.Name, request.Alias);
        if (categoryResult.IsFailure)
            return Result<CategoryResponse>.Failure(categoryResult.Errors);

        await writeRepository.CreateAsync(categoryResult.Data);
        return CategoryResponse.FromModel(categoryResult.Data);
    }
}
