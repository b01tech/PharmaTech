using Microsoft.AspNetCore.Mvc;
using PharmaTech.Product.Category.UseCases;

namespace PharmaTech.Api.Endpoints.Category.Handlers;

public static class FindCategoryById
{
    public static async Task<IResult> Handle(
        [FromRoute] Guid id,
        [FromServices] IFindCategoryByIdUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(id);
        return result.IsSuccess ? Results.Ok(result.Data) : Results.NotFound(result.Errors);
    }
}
