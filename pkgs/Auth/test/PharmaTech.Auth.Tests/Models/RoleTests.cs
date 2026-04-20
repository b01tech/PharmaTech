using PharmaTech.Shared.Core;
using PharmaTech.Shared.ValueObjects;
using RoleModel = PharmaTech.Auth.Role.Models.Role;
using PermissionModel = PharmaTech.Auth.Permission.Models.Permission;
using ModuleEnum = PharmaTech.Auth.Permission.Enums.Module;
using ActionEnum = PharmaTech.Auth.Permission.Enums.Action;

namespace PharmaTech.Auth.Tests.Models;

public class RoleTests
{
    [Fact]
    public void Create_WithValidData_Should_Return_Success()
    {
        var result = RoleModel.Create("Admin", "Administrator role", []);

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal("Admin", result.Data.Name.Value);
        Assert.Equal("Administrator role", result.Data.Description.Value);
    }

    [Fact]
    public void Create_WithInvalidName_Should_Return_Failure()
    {
        var result = RoleModel.Create("Ad", "Administrator role", []);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Contains("NAME_TOO_SHORT", result.Errors[0]);
    }

    [Fact]
    public void Create_WithInvalidDescription_Should_Return_Failure()
    {
        var result = RoleModel.Create("Admin", "abcd", []);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Contains("DESCRIPTION_TOO_SHORT", result.Errors[0]);
    }

    [Fact]
    public void Create_WithPermissions_Should_Assign_Permissions()
    {
        var permissions = new List<PermissionModel>
        {
            new(ModuleEnum.Product, ActionEnum.ReadOnly)
        };
        var result = RoleModel.Create("Admin", "Administrator role", permissions);

        Assert.True(result.IsSuccess);
        Assert.Single(result.Data.Permissions);
    }

    [Fact]
    public void Create_WithEmptyPermissions_Should_Return_EmptyList()
    {
        var result = RoleModel.Create("Admin", "Administrator role", []);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Data.Permissions);
    }

    [Fact]
    public void Create_Should_Generate_Guid()
    {
        var result = RoleModel.Create("Admin", "Administrator role", []);

        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Data.Id);
    }
}