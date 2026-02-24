using Microsoft.AspNetCore.Mvc;
using PharmaTech.Product.Product.Dtos.Requests;
using PharmaTech.Product.Product.UseCases;

namespace PharmaTech.Api.Endpoints.Product.Handlers;

public static class UpdateProduct
{
    public static async Task<IResult> Handle(
        Guid id,
        [FromBody] UpdateProductRequest request,
        [FromServices] IUpdateProductUseCase useCase)
    {
        if (id != request.Id)
            return Results.BadRequest("Product Id mismatch");

        var result = await useCase.ExecuteAsync(request);
        return result.IsSuccess ? Results.Ok(result.Data) : Results.BadRequest(result.Errors);
    }
}
