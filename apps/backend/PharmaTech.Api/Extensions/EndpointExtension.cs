using PharmaTech.Api.Endpoints.ApiHealth;

namespace PharmaTech.Api.Extensions;

public static class EndpointExtension
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapApiHealthEndpoint();
    }
}
