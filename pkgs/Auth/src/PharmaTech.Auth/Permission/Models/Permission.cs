using PharmaTech.Auth.Permission.Enums;
using PharmaTech.Shared.Core;

namespace PharmaTech.Auth.Permission.Models;

public class Permission : EntityBase
{
    public string Module { get; private set; } = string.Empty;
    public string Action { get; private set; } = string.Empty;

    // EF Core
    protected Permission() { }

    public Permission(Module module, Enums.Action action)
    {
        Module = module.ToString();
        Action = action.ToString();
    }
}
