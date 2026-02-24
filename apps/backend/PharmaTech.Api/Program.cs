using PharmaTech.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApiDocumentation();

var app = builder.Build();

app.UseApiDocumentation();
app.UseHttpsRedirection();
app.MapEndpoints();

app.Run();
