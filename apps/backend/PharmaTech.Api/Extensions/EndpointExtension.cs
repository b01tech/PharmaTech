using PharmaTech.Api.Endpoints.ApiHealth;
using PharmaTech.Api.Endpoints.Category;
using PharmaTech.Api.Endpoints.Product;

namespace PharmaTech.Api.Extensions;

public static class EndpointExtension
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapApiHealthEndpoint();
        app.MapCategoryEndpoint();
        app.MapProductEndpoint();
    }
}
