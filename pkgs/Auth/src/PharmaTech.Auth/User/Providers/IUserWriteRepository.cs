using UserModel = PharmaTech.Auth.User.Models.User;

namespace PharmaTech.Auth.User.Providers;

public interface IUserWriteRepository
{
    Task AddAsync(UserModel user);
    Task UpdateAsync(UserModel user);
    Task DeleteAsync(Guid userId);
}
