using PharmaTech.Api.Endpoints.Product.Handlers;
using PharmaTech.Product.Product.Dtos.Responses;

namespace PharmaTech.Api.Endpoints.Product;

public static class ProductEndpoint
{
    private const string RoutePrefix = "api/products";

    public static void MapProductEndpoint(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(RoutePrefix).WithTags("Product");

        group.MapPost("/", CreateProduct.Handle)
            .Produces<ProductResponses>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithSummary("Create a new product");

        group.MapPut("/{id}", UpdateProduct.Handle)
            .Produces<ProductResponses>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithSummary("Update a product");

        group.MapDelete("/{id}", DeleteProduct.Handle)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .WithSummary("Delete a product");

        group.MapGet("/{id}", GetProductById.Handle)
            .Produces<ProductResponses>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Get a product by Id");

        group.MapGet("/", ListProducts.Handle)
            .Produces<ProductsListResponse>(StatusCodes.Status200OK)
            .WithSummary("List products with pagination and filters");
    }
}
