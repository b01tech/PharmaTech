using System.Linq.Expressions;

namespace PharmaTech.Product.Product.Repositories;

public interface IProductReadOnlyRepository
{
    Task<Models.Product?> GetProductByIdAsync(Guid id);

    Task<IEnumerable<Models.Product>> GetAllProductsWithFilterAsync(
        Expression<Func<Models.Product, bool>>? filter,
        Guid? categoryId = null,
        Guid? subcategoryId = null,
        int page = 1,
        int pageSize = 25
    );
    Task<(int TotalProducts, int TotalCategories, int TotalSubcategories)> GetTotalAsync();
}
