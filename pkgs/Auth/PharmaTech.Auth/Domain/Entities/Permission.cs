using PharmaTech.Auth.Domain.Enums;
using PharmaTech.Shared.Core;

namespace PharmaTech.Auth.Domain.Entities;

public class Permission : Entity
{
    #region Properties
    public string Module { get; private set; } = string.Empty;
    public string Action { get; private set; } = string.Empty;
    #endregion

    #region Constructors
    // EF Core
    protected Permission() { }

    public Permission(Module module, Enums.Action action)
    {
        Module = module.ToString();
        Action = action.ToString();
    }
    #endregion
}
