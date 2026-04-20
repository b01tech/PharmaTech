namespace PharmaTech.Auth.Role.UseCases.Interfaces;

using PharmaTech.Auth.Role.Dtos;
using PharmaTech.Shared.Core;

public interface IListRoleUseCase : IUseCase<ListRolesRequest, Result<Pagination<RoleResponse>>> { }
