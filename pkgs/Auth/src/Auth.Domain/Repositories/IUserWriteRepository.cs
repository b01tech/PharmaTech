using Auth.Domain.Entities;

namespace Auth.Domain.Repositories;

public interface IUserWriteRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}
