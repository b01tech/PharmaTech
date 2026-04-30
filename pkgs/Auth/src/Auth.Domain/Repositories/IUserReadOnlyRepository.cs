using Auth.Domain.Entities;

namespace Auth.Domain.Repositories;

public interface IUserReadOnlyRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<IList<User>> GetAllAsync(int page, int pageSize);
    Task<int> GetCountAsync();
}
