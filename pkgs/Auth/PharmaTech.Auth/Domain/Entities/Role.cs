using PharmaTech.Shared.Core;
using PharmaTech.Shared.ValueObjects;

namespace PharmaTech.Auth.Domain.Entities;

public class Role : Entity
{
    #region Properties
    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public List<Permission> Permissions { get; private set; } = [];
    #endregion

    #region Constructors
    // EF Core
    private Role() { }

    private Role(Name name, Description description, List<Permission> permissions)
    {
        Name = name;
        Description = description;
        Permissions = permissions;
    }

    public static Result<Role> Create(string inputName, string inputDescription, List<Permission> permissions)
    {
        var nameResult = Name.Create(inputName);
        var descriptionResult = Description.Create(inputDescription);
        if (nameResult.IsFailure || descriptionResult.IsFailure)
            return Result<Role>.Failure(Result.MergeErrors(nameResult, descriptionResult));
        return new Role(nameResult.Data, descriptionResult.Data, permissions);
    }
    #endregion
}
