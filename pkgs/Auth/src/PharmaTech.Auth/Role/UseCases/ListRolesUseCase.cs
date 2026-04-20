namespace PharmaTech.Auth.Role.UseCases;

using PharmaTech.Auth.Role.Dtos;
using PharmaTech.Auth.Role.UseCases.Interfaces;
using PharmaTech.Shared.Core;

public class ListRolesUseCase : IListRoleUseCase
{
    public Task<Result<Result<Pagination<RoleResponse>>>> ExecuteAsync(ListRolesRequest request)
    {
        throw new NotImplementedException();
    }
}
