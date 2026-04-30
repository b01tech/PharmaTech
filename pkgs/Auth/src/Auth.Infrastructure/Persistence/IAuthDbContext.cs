using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Persistence;

public interface IAuthDbContext
{
    DbSet<Password> Passwords { get; }
    DbSet<Role> Roles { get; }
    DbSet<User> Users { get; }
}
