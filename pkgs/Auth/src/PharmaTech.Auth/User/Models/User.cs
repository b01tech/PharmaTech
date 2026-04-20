using PharmaTech.Shared.Core;
using PharmaTech.Shared.ValueObjects;
using UserRole = PharmaTech.Auth.Role.Models.Role;

namespace PharmaTech.Auth.User.Models;

public class User : EntityBase
{
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public UserRole Role { get; private set; }
    public string? AvatarUrl { get; private set; }

    // EF Core
    private User() { }

    private User(Name name, Email email, UserRole role, string? avatarUrl = null)
    {
        Name = name;
        Email = email;
        Role = role;
        AvatarUrl = avatarUrl;
    }

    public static Result<User> Create(string nameInput, string emailInput, UserRole role, string? avatarUrl = null)
    {
        var nameResult = Name.Create(nameInput);
        var emailResult = Email.Create(emailInput);
        if (nameResult.IsFailure || emailResult.IsFailure)
            return Result<User>.Failure(Result.MergeErrors(nameResult, emailResult));

        return new User(nameResult.Data, emailResult.Data, role, avatarUrl);
    }

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

    public Result UpdateRole(UserRole role)
    {
        Role = role;
        return Result.Success();
    }
}
