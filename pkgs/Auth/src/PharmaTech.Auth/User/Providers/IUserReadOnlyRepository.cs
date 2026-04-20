using PharmaTech.Shared.Core;
using UserModel = PharmaTech.Auth.User.Models.User;

namespace PharmaTech.Auth.User.Providers;

public interface IUserReadOnlyRepository
{
    Task<UserModel?> GetByIdAsync(Guid userId);
    Task<UserModel?> GetByEmailAsync(string email);
    Task<Pagination<UserModel>> GetAllAsync(int page, int pageSize);
}
