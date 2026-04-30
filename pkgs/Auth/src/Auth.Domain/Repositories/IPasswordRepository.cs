using Auth.Domain.Entities;

namespace Auth.Domain.Repositories;

public interface IPasswordRepository
{
    Task AddAsync(Password password);
    Task UpdateAsync(Password password);
}
