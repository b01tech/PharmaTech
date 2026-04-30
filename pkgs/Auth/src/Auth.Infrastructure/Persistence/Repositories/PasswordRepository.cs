using Auth.Domain.Entities;
using Auth.Domain.Repositories;

namespace Auth.Infrastructure.Persistence.Repositories;

internal class PasswordRepository(IAuthDbContext dbContext) : IPasswordRepository
{
    public async Task AddAsync(Password password)
    {
        await dbContext.Passwords.AddAsync(password);
    }

    public async Task UpdateAsync(Password password)
    {
        dbContext.Passwords.Update(password);
        await Task.CompletedTask;
    }
}
