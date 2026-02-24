namespace PharmaTech.Product.Product.Repositories;

public interface IProductWriteRepository
{
    Task CreateProductAsync(Models.Product product);
    Task UpdateProductAsync(Models.Product product);
    Task DeleteProductAsync(Models.Product product);
}
