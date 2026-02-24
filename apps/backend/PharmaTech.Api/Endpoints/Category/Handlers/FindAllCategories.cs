using Microsoft.AspNetCore.Mvc;
using PharmaTech.Product.Category.Dtos.Requests;
using PharmaTech.Product.Category.UseCases;

namespace PharmaTech.Api.Endpoints.Category.Handlers;

public static class FindAllCategories
{
    public static async Task<IResult> Handle(
        [AsParameters] ListCategoriesRequest request,
        [FromServices] IFindAllCategoriesUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(request);
        return result.IsSuccess ? Results.Ok(result.Data) : Results.BadRequest(result.Errors);
    }
}
