using Microsoft.AspNetCore.Mvc;
using PharmaTech.Product.Category.Dtos.Requests;
using PharmaTech.Product.Category.UseCases;

namespace PharmaTech.Api.Endpoints.Category.Handlers;

public static class CreateCategory
{
    public static async Task<IResult> Handle(
        [FromBody] CreateCategoryRequest request,
        [FromServices] ICreateCategoryUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(request);
        return result.IsSuccess ? Results.Created(string.Empty, result.Data) : Results.BadRequest(result.Errors);
    }
}
