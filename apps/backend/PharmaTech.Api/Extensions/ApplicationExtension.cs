using PharmaTech.Product.Category.UseCases;
using PharmaTech.Product.Product.UseCases;

namespace PharmaTech.Api.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
        return services;
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<ICreateCategoryUseCase, CreateCategoryUseCase>();
        services.AddScoped<ICreateSubcategoryUseCase, CreateSubcategoryUseCase>();
        services.AddScoped<IDeleteCategoryUseCase, DeleteCategoryUseCase>();
        services.AddScoped<IRemoveSubcategoryUseCase, RemoveSubcategoryUseCase>();

        services.AddScoped<ICreateProductUseCase, CreateProductUseCase>();
        services.AddScoped<IDeleteProductUseCase, DeleteProductUseCase>();
        services.AddScoped<IGetProductByIdUseCase, GetProductByIdUseCase>();
        services.AddScoped<IListProductsUseCase, ListProductsUseCase>();
        services.AddScoped<IUpdateProductUseCase, UpdateProductUseCase>();
    }
}
