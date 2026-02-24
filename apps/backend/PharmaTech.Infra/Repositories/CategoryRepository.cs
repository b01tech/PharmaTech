using Microsoft.EntityFrameworkCore;
using PharmaTech.Infra.Data;
using PharmaTech.Product.Category.Models;
using PharmaTech.Product.Category.Repositories;

namespace PharmaTech.Infra.Repositories;

public class CategoryRepository(PharmaTechDbContext dbContext) : ICategoryWriteRepository, ICategoryReadOnlyRepository
{
    public async Task CreateAsync(Category category)
    {
        await dbContext.Categories.AddAsync(category);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        dbContext.Categories.Update(category);
        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(Guid id)
    {
        var category = await dbContext.Categories.FindAsync(id);
        if (category != null)
        {
            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task CreateSubCategoryAsync(Subcategory subcategory)
    {
        await dbContext.Subcategories.AddAsync(subcategory);
        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveSubCategoryAsync(Guid id)
    {
        var subcategory = await dbContext.Subcategories.FindAsync(id);
        if (subcategory != null)
        {
            dbContext.Subcategories.Remove(subcategory);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid id)
    {
        return await dbContext.Categories
            .Include(c => c.Subcategories)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Subcategory?> GetSubCategoryByIdAsync(Guid id)
    {
        return await dbContext.Subcategories.FindAsync(id);
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync(int page = 1, int pageSize = 25)
    {
        return await dbContext.Categories
            .AsNoTracking()
            .OrderBy(c => c.Name.Value)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Category?> GetAllSubCategoriesAsync(Guid categoryId)
    {
        return await dbContext.Categories
            .Include(c => c.Subcategories)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == categoryId);
    }
}
