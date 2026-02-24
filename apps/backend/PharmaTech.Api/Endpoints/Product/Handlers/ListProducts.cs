using Microsoft.AspNetCore.Mvc;
using PharmaTech.Product.Product.Dtos.Requests;
using PharmaTech.Product.Product.UseCases;

namespace PharmaTech.Api.Endpoints.Product.Handlers;

public static class ListProducts
{
    public static async Task<IResult> Handle(
        [FromServices] IListProductsUseCase useCase,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 25,
        [FromQuery] Guid? categoryId = null,
        [FromQuery] Guid? subcategoryId = null)
    {
        var query = new ListProductsRequest(page, pageSize, categoryId, subcategoryId);
        var result = await useCase.ExecuteAsync(query);
        return result.IsSuccess ? Results.Ok(result.Data) : Results.BadRequest(result.Errors);
    }
}
