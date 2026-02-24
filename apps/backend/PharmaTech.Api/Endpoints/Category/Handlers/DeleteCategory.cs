using Microsoft.AspNetCore.Mvc;
using PharmaTech.Product.Category.UseCases;

namespace PharmaTech.Api.Endpoints.Category.Handlers;

public static class DeleteCategory
{
    public static async Task<IResult> Handle(
        Guid id,
        [FromServices] IDeleteCategoryUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(id);
        return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.Errors);
    }
}
