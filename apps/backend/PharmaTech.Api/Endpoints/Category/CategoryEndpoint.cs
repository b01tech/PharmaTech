using PharmaTech.Api.Endpoints.Category.Handlers;
using PharmaTech.Product.Category.Dtos.Responses;

namespace PharmaTech.Api.Endpoints.Category;

public static class CategoryEndpoint
{
    private const string RoutePrefix = "api/categories";

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

        group.MapPut("/{id}", UpdateCategory.Handle)
            .Produces<CategoryResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithSummary("Update a category");

        group.MapGet("/{id}", FindCategoryById.Handle)
            .Produces<CategoryDetailedResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Get a category by id");

        group.MapGet("/", FindAllCategories.Handle)
            .Produces<IEnumerable<CategoryResponse>>(StatusCodes.Status200OK)
            .WithSummary("Get all categories");

        group.MapGet("/{id}/subcategories", FindSubcategories.Handle)
            .Produces<IEnumerable<SubCategoryDetailedResponse>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Get all subcategories for a category");
    }
}
