using Microsoft.AspNetCore.Mvc;
using PharmaTech.Product.Category.Dtos.Requests;
using PharmaTech.Product.Category.UseCases;

namespace PharmaTech.Api.Endpoints.Category.Handlers;

public static class UpdateCategory
{
    public static async Task<IResult> Handle(
        [FromRoute] Guid id,
        [FromBody] UpdateCategoryRequest request,
        [FromServices] IUpdateCategoryUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(request with { Id = id });
        return result.IsSuccess ? Results.Ok(result.Data) : Results.BadRequest(result.Errors);
    }
}
