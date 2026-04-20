using PharmaTech.Shared.Core;
using PharmaTech.Shared.ValueObjects;
using UserModel = PharmaTech.Auth.User.Models.User;
using RoleModel = PharmaTech.Auth.Role.Models.Role;

namespace PharmaTech.Auth.Tests.Models;

public class UserTests
{
    private static RoleModel CreateValidRole() =>
        RoleModel.Create("Admin", "Administrator role", []).Data!;

    [Fact]
    public void Create_WithValidData_Should_Return_Success()
    {
        var role = CreateValidRole();
        var result = UserModel.Create("John Doe", "john@example.com", role);

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal("John Doe", result.Data.Name.Value);
        Assert.Equal("john@example.com", result.Data.Email.Address);
        Assert.Equal(role, result.Data.Role);
    }

    [Fact]
    public void Create_WithInvalidName_Should_Return_Failure()
    {
        var role = CreateValidRole();
        var result = UserModel.Create("Jo", "john@example.com", role);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Contains("NAME_TOO_SHORT", result.Errors[0]);
    }

    [Fact]
    public void Create_WithInvalidEmail_Should_Return_Failure()
    {
        var role = CreateValidRole();
        var result = UserModel.Create("John Doe", "invalid-email", role);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Contains("invalid", result.Errors[0]);
    }

    [Fact]
    public void Create_WithAvatarUrl_Should_Assign_AvatarUrl()
    {
        var role = CreateValidRole();
        var result = UserModel.Create("John Doe", "john@example.com", role, "https://example.com/avatar.png");

        Assert.True(result.IsSuccess);
        Assert.Equal("https://example.com/avatar.png", result.Data.AvatarUrl);
    }

    [Fact]
    public void Create_WithoutAvatarUrl_Should_Be_Null()
    {
        var role = CreateValidRole();
        var result = UserModel.Create("John Doe", "john@example.com", role);

        Assert.True(result.IsSuccess);
        Assert.Null(result.Data.AvatarUrl);
    }

    [Fact]
    public void Update_Name_Should_Return_Success()
    {
        var role = CreateValidRole();
        var user = UserModel.Create("John Doe", "john@example.com", role).Data;
        var result = user.Update(nameInput: "Jane Doe");

        Assert.True(result.IsSuccess);
        Assert.Equal("Jane Doe", user.Name.Value);
    }

    [Fact]
    public void Update_Email_Should_Return_Success()
    {
        var role = CreateValidRole();
        var user = UserModel.Create("John Doe", "john@example.com", role).Data;
        var result = user.Update(emailInput: "jane@example.com");

        Assert.True(result.IsSuccess);
        Assert.Equal("jane@example.com", user.Email.Address);
    }

    [Fact]
    public void Update_WithInvalidName_Should_Return_Failure()
    {
        var role = CreateValidRole();
        var user = UserModel.Create("John Doe", "john@example.com", role).Data;
        var result = user.Update(nameInput: "Jo");

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
    }

    [Fact]
    public void Update_AvatarUrl_Should_Return_Success()
    {
        var role = CreateValidRole();
        var user = UserModel.Create("John Doe", "john@example.com", role).Data;
        var result = user.Update(avatarUrl: "https://example.com/new-avatar.png");

        Assert.True(result.IsSuccess);
        Assert.Equal("https://example.com/new-avatar.png", user.AvatarUrl);
    }

    [Fact]
    public void UpdateRole_Should_Return_Success()
    {
        var role1 = RoleModel.Create("Admin", "Admin role", []).Data;
        var role2 = RoleModel.Create("User", "User role", []).Data;
        var user = UserModel.Create("John Doe", "john@example.com", role1).Data;
        var result = user.UpdateRole(role2);

        Assert.True(result.IsSuccess);
        Assert.Equal(role2, user.Role);
    }

    [Fact]
    public void Update_WithoutParameters_Should_Return_Success()
    {
        var role = CreateValidRole();
        var user = UserModel.Create("John Doe", "john@example.com", role).Data;
        var result = user.Update();

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Create_Should_Generate_Guid()
    {
        var role = CreateValidRole();
        var result = UserModel.Create("John Doe", "john@example.com", role);

        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Data.Id);
    }

    [Fact]
    public void Create_Should_Set_CreatedAt()
    {
        var role = CreateValidRole();
        var result = UserModel.Create("John Doe", "john@example.com", role);

        Assert.True(result.IsSuccess);
        Assert.True(result.Data.CreatedAt <= DateTime.UtcNow);
    }
}