using Auth.Domain.Entities;
using ActionEnum = Auth.Domain.Enums.Action;
using ModuleEnum = Auth.Domain.Enums.Module;

namespace Auth.Domain.Test.Entities;

public class RoleTests
{
    [Fact]
    public void Create_WithValidData_Should_Return_Success()
    {
        var result = Role.Create("Admin", "Administrator role", []);

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal("Admin", result.Data.Name.Value);
        Assert.Equal("Administrator role", result.Data.Description.Value);
    }

    [Fact]
    public void Create_WithInvalidName_Should_Return_Failure()
    {
        var result = Role.Create("Ad", "Administrator role", []);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Contains("NAME_TOO_SHORT", result.Errors[0]);
    }

    [Fact]
    public void Create_WithInvalidDescription_Should_Return_Failure()
    {
        var result = Role.Create("Admin", "abcd", []);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Contains("DESCRIPTION_TOO_SHORT", result.Errors[0]);
    }

    [Fact]
    public void Create_WithPermissions_Should_Assign_Permissions()
    {
        var permissions = new List<Permission> { new(ModuleEnum.Product, ActionEnum.ReadOnly) };
        var result = Role.Create("Admin", "Administrator role", permissions);

        Assert.True(result.IsSuccess);
        Assert.Single(result.Data.Permissions);
    }

    [Fact]
    public void Create_WithEmptyPermissions_Should_Return_EmptyList()
    {
        var result = Role.Create("Admin", "Administrator role", []);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Data.Permissions);
    }

    [Fact]
    public void Create_Should_Generate_Guid()
    {
        var result = Role.Create("Admin", "Administrator role", []);

        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Data.Id);
    }
}
