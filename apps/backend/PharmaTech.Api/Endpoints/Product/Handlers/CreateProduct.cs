using Microsoft.AspNetCore.Mvc;
using PharmaTech.Product.Product.Dtos.Requests;
using PharmaTech.Product.Product.UseCases;

namespace PharmaTech.Api.Endpoints.Product.Handlers;

public static class CreateProduct
{
    public static async Task<IResult> Handle(
        [FromBody] CreateProductRequest request,
        [FromServices] ICreateProductUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(request);
        return result.IsSuccess ? Results.Created(string.Empty, result.Data) : Results.BadRequest(result.Errors);
    }
}
