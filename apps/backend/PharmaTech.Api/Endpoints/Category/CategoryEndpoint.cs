using PharmaTech.Api.Endpoints.Category.Handlers;
using PharmaTech.Product.Category.Dtos.Responses;

namespace PharmaTech.Api.Endpoints.Category;

public static class CategoryEndpoint
{
    private const string RoutePrefix = "categories";

    public static void MapCategoryEndpoint(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(RoutePrefix).WithTags("Category");

        group.MapPost("/", CreateCategory.Handle)
            .Produces<CategoryResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithSummary("Create a new category");

        group.MapPost("/{id}/subcategories", CreateSubcategory.Handle)
            .Produces<SubcategoryResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithSummary("Create a new subcategory for a category");

        group.MapDelete("/{id}", DeleteCategory.Handle)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .WithSummary("Delete a category");

        group.MapDelete("/subcategories/{id}", RemoveSubcategory.Handle)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .WithSummary("Delete a subcategory");
    }
}
