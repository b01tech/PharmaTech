using PharmaTech.Shared.Core;
using PharmaTech.Shared.ValueObjects;
using PermissionModel = PharmaTech.Auth.Permission.Models.Permission;

namespace PharmaTech.Auth.Role.Models;

public class Role : EntityBase
{
    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public List<PermissionModel> Permissions { get; private set; } = [];

    // EF Core
    private Role() { }

    private Role(Name name, Description description, List<PermissionModel> permissions)
    {
        Name = name;
        Description = description;
        Permissions = permissions;
    }

    public static Result<Role> Create(string inputName, string inputDescription, List<PermissionModel> permissions)
    {
        var nameResult = Name.Create(inputName);
        var descriptionResult = Description.Create(inputDescription);
        if (nameResult.IsFailure || descriptionResult.IsFailure)
            return Result<Role>.Failure(Result.MergeErrors(nameResult, descriptionResult));
        return new Role(nameResult.Data, descriptionResult.Data, permissions);
    }
}
