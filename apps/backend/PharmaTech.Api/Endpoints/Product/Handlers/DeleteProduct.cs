using Microsoft.AspNetCore.Mvc;
using PharmaTech.Product.Product.UseCases;

namespace PharmaTech.Api.Endpoints.Product.Handlers;

public static class DeleteProduct
{
    public static async Task<IResult> Handle(
        Guid id,
        [FromServices] IDeleteProductUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(id);
        return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.Errors);
    }
}
