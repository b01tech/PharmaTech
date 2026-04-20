using ModuleEnum = PharmaTech.Auth.Permission.Enums.Module;
using ActionEnum = PharmaTech.Auth.Permission.Enums.Action;
using PermissionModel = PharmaTech.Auth.Permission.Models.Permission;

namespace PharmaTech.Auth.Tests.Models;

public class PermissionTests
{
    [Fact]
    public void Constructor_Should_Set_Module_And_Action()
    {
        var permission = new PermissionModel(ModuleEnum.Product, ActionEnum.ReadOnly);

        Assert.Equal("Product", permission.Module);
        Assert.Equal("ReadOnly", permission.Action);
    }

    [Fact]
    public void Module_Should_Convert_To_String()
    {
        var permission = new PermissionModel(ModuleEnum.Product, ActionEnum.Write);

        Assert.Equal("Product", permission.Module);
    }

    [Fact]
    public void Action_Should_Convert_To_String()
    {
        var permission = new PermissionModel(ModuleEnum.Auth, ActionEnum.Write);

        Assert.Equal("Write", permission.Action);
    }

    [Fact]
    public void All_Modules_Should_Work()
    {
        foreach (ModuleEnum module in Enum.GetValues<ModuleEnum>())
        {
            var permission = new PermissionModel(module, ActionEnum.Write);
            Assert.Equal(module.ToString(), permission.Module);
        }
    }

    [Fact]
    public void All_Actions_Should_Work()
    {
        foreach (ActionEnum action in Enum.GetValues<ActionEnum>())
        {
            var permission = new PermissionModel(ModuleEnum.Product, action);
            Assert.Equal(action.ToString(), permission.Action);
        }
    }
}