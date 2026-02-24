using PharmaTech.Api.Extensions;
using PharmaTech.Infra.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiDocumentation()
    .AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddCorsPolicy();

var app = builder.Build();

await app.Services.InitializeDatabaseAsync();

app.UseCors("AllowAll");
app.UseApiDocumentation();
app.UseHttpsRedirection();
app.MapEndpoints();

app.Run();
