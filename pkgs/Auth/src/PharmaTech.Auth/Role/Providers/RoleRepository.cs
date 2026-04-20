using RoleModel = PharmaTech.Auth.Role.Models.Role;

namespace PharmaTech.Auth.Role.Providers;

public interface IRoleRepository
{
    Task AddAsync(RoleModel role);
    Task DeleteAsync(Guid roleId);
    Task<RoleModel?> GetNameAsync(string name);
}
