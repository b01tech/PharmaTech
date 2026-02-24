using Microsoft.AspNetCore.Mvc;
using PharmaTech.Product.Category.Dtos.Requests;
using PharmaTech.Product.Category.UseCases;

namespace PharmaTech.Api.Endpoints.Category.Handlers;

public static class CreateSubcategory
{
    public static async Task<IResult> Handle(
        Guid id,
        [FromBody] CreateSubcategoryRequest request,
        [FromServices] ICreateSubcategoryUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(request, id);
        return result.IsSuccess ? Results.Created(string.Empty, result.Data) : Results.BadRequest(result.Errors);
    }
}
