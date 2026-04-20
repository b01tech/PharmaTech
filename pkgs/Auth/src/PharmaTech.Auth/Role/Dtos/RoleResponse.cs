using PermissionModel = PharmaTech.Auth.Permission.Models.Permission;

namespace PharmaTech.Auth.Role.Dtos;

public record RoleResponse(Guid Id, string Name, string Description, List<PermissionModel> Permissions);
