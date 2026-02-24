namespace PharmaTech.Product.Category.Repositories;

public interface ICategoryWriteRepository
{
    Task CreateAsync(Models.Category category);
    Task UpdateAsync(Models.Category category);
    Task RemoveAsync(Guid id);
    Task CreateSubCategoryAsync(Models.Subcategory subcategory);
    Task RemoveSubCategoryAsync(Guid id);
}
