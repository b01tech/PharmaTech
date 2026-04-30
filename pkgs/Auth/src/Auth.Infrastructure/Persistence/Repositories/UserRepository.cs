using Auth.Domain.Entities;
using Auth.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Persistence.Repositories;

internal class UserRepository(IAuthDbContext dbContext) : IUserReadOnlyRepository, IUserWriteRepository
{
    #region Readonly methods
    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IList<User>> GetAllAsync(int page, int pageSize)
    {
        return await dbContext.Users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetCountAsync() => await dbContext.Users.CountAsync();
    #endregion

    #region Write methods
    public async Task AddAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
    }

    public async Task UpdateAsync(User user)
    {
        dbContext.Users.Update(user);
        await Task.CompletedTask;
    }
    #endregion
}
