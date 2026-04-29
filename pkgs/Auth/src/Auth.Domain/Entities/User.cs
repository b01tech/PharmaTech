using PharmaTech.Shared.Core;
using PharmaTech.Shared.ValueObjects;

namespace Auth.Domain.Entities;

public class User : Entity
{
    #region Properties
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public Role Role { get; private set; }
    public string? AvatarUrl { get; private set; }
    #endregion

    #region  Constructors
    // EF Core
    private User() { }

    private User(Name name, Email email, Role role, string? avatarUrl = null)
    {
        Name = name;
        Email = email;
        Role = role;
        AvatarUrl = avatarUrl;
    }

    public static Result<User> Create(string nameInput, string emailInput, Role role, string? avatarUrl = null)
    {
        var nameResult = Name.Create(nameInput);
        var emailResult = Email.Create(emailInput);
        if (nameResult.IsFailure || emailResult.IsFailure)
            return Result<User>.Failure(Result.MergeErrors(nameResult, emailResult));

        return new User(nameResult.Data, emailResult.Data, role, avatarUrl);
    }
    #endregion

    #region Methods
    public Result Update(string? nameInput = null, string? emailInput = null, string? avatarUrl = null)
    {
        var nameResult = Name.Create(nameInput ?? Name.Value);
        var emailResult = Email.Create(emailInput ?? Email.Address);
        if (nameResult.IsFailure || emailResult.IsFailure)
            return Result<User>.Failure(Result.MergeErrors(nameResult, emailResult));

        Name = nameResult.Data;
        Email = emailResult.Data;
        AvatarUrl = avatarUrl;
        return Result.Success();
    }

    public Result UpdateRole(Role role)
    {
        Role = role;
        return Result.Success();
    }
    #endregion
}
