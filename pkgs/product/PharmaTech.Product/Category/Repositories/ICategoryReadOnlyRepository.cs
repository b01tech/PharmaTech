namespace PharmaTech.Product.Category.Repositories;

public interface ICategoryReadOnlyRepository
{
    Task<Models.Category> GetCategoryByIdAsync(Guid id);
    Task<IEnumerable<Models.Category>> GetAllCategoriesAsync(int page = 1, int pageSize = 25);
    Task<Models.Category> GetAllSubCategoriesAsync(Guid categoryId);
}
