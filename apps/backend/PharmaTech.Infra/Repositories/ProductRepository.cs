using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using PharmaTech.Infra.Data;
using PharmaTech.Product.Product.Repositories;
using ProductModel = PharmaTech.Product.Product.Models.Product;

namespace PharmaTech.Infra.Repositories;

public class ProductRepository(PharmaTechDbContext dbContext) : IProductWriteRepository, IProductReadOnlyRepository
{
    public async Task CreateProductAsync(ProductModel product)
    {
        await dbContext.Products.AddAsync(product);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(ProductModel product)
    {
        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(ProductModel product)
    {
        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();
    }

    public async Task<ProductModel?> GetProductByIdAsync(Guid id)
    {
        return await dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<ProductModel>> GetAllProductsWithFilterAsync(
        Expression<Func<ProductModel, bool>>? filter,
        Guid? categoryId = null,
        Guid? subcategoryId = null,
        int page = 1,
        int pageSize = 25)
    {
        var query = dbContext.Products.AsNoTracking();

        if (categoryId.HasValue)
        {
            // Join implícito para filtrar por categoria através da subcategoria
            query = query.Where(p => dbContext.Subcategories.Any(s => s.Id == p.SubcategoryId && s.CategoryId == categoryId.Value));
        }

        if (subcategoryId.HasValue)
        {
            query = query.Where(p => p.SubcategoryId == subcategoryId.Value);
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query
            .OrderBy(p => p.Name.Value)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<(int TotalProducts, int TotalCategories, int TotalSubcategories)> GetTotalAsync()
    {
        var totalProducts = await dbContext.Products.CountAsync();
        var totalCategories = await dbContext.Categories.CountAsync();
        var totalSubcategories = await dbContext.Subcategories.CountAsync();

        return (totalProducts, totalCategories, totalSubcategories);
    }
}
