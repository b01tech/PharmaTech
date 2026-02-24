using Microsoft.AspNetCore.Mvc;
using PharmaTech.Product.Category.UseCases;

namespace PharmaTech.Api.Endpoints.Category.Handlers;

public static class FindSubcategories
{
    public static async Task<IResult> Handle(
        [FromRoute] Guid id,
        [FromServices] IFindSubcategoriesUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(id);
        return result.IsSuccess ? Results.Ok(result.Data) : Results.NotFound(result.Errors);
    }
}
