using RoleModel = PharmaTech.Auth.Role.Models.Role;

namespace PharmaTech.Auth.Role.Providers;

public interface IRoleRepository
{
    Task AddAsync(RoleModel role);
    Task<RoleModel?> GetNameAsync(string name);
}
