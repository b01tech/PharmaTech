using Auth.Domain.Entities;
using Auth.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Persistence.Repositories;

internal class RoleRepository(IAuthDbContext dbContext) : IRoleRepository
{
    public async Task AddAsync(Role role)
    {
        await dbContext.Roles.AddAsync(role);
    }

    public async Task DeleteAsync(Guid id)
    {
        var role = await dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
        if (role == null)
            return;
        dbContext.Roles.Remove(role);
        await Task.CompletedTask;
    }

    public async Task<Role?> GetByIdAsync(Guid id)
    {
        return await dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IList<Role>> GetAllAsync()
    {
        return await dbContext.Roles.ToListAsync();
    }
}
