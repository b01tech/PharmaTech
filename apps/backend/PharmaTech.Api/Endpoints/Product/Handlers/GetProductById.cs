using Microsoft.AspNetCore.Mvc;
using PharmaTech.Product.Product.UseCases;

namespace PharmaTech.Api.Endpoints.Product.Handlers;

public static class GetProductById
{
    public static async Task<IResult> Handle(
        Guid id,
        [FromServices] IGetProductByIdUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(id);
        return result.IsSuccess ? Results.Ok(result.Data) : Results.NotFound(result.Errors);
    }
}
