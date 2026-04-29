using PharmaTech.Auth.Domain.Entities;
using ActionEnum = PharmaTech.Auth.Domain.Enums.Action;
using ModuleEnum = PharmaTech.Auth.Domain.Enums.Module;

namespace PharmaTech.Auth.Test.Domain.Entities;

public class PermissionTests
{
    [Fact]
    public void Constructor_Should_Set_Module_And_Action()
    {
        var permission = new Permission(ModuleEnum.Product, ActionEnum.ReadOnly);

        Assert.Equal("Product", permission.Module);
        Assert.Equal("ReadOnly", permission.Action);
    }

    [Fact]
    public void Module_Should_Convert_To_String()
    {
        var permission = new Permission(ModuleEnum.Product, ActionEnum.Write);

        Assert.Equal("Product", permission.Module);
    }

    [Fact]
    public void Action_Should_Convert_To_String()
    {
        var permission = new Permission(ModuleEnum.Auth, ActionEnum.Write);

        Assert.Equal("Write", permission.Action);
    }

    [Fact]
    public void All_Modules_Should_Work()
    {
        foreach (ModuleEnum module in Enum.GetValues<ModuleEnum>())
        {
            var permission = new Permission(module, ActionEnum.Write);
            Assert.Equal(module.ToString(), permission.Module);
        }
    }

    [Fact]
    public void All_Actions_Should_Work()
    {
        foreach (ActionEnum action in Enum.GetValues<ActionEnum>())
        {
            var permission = new Permission(ModuleEnum.Product, action);
            Assert.Equal(action.ToString(), permission.Action);
        }
    }
}
