using PharmaTech.Auth.Role.Dtos;
using PharmaTech.Auth.Role.UseCases.Interfaces;
using PharmaTech.Shared.Core;

namespace PharmaTech.Auth.Role.UseCases;

public class CreateRoleUseCase : ICreateRoleUseCase
{
    public Task<Result<RoleResponse>> ExecuteAsync(CreateRoleRequest request)
    {
        throw new NotImplementedException();
    }
}
