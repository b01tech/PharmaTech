using PharmaTech.Api.Extensions;
using PharmaTech.Infra.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiDocumentation()
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

var app = builder.Build();

app.UseApiDocumentation();
app.UseHttpsRedirection();
app.MapEndpoints();

app.Run();
