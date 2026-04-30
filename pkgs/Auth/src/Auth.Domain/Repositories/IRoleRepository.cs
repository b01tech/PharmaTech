using Auth.Domain.Entities;

namespace Auth.Domain.Repositories;

public interface IRoleRepository
{
    Task AddAsync(Role role);
    Task DeleteAsync(Guid id);
    Task<Role?> GetByIdAsync(Guid id);
    Task<List<Role>> GetAllAsync();
}
