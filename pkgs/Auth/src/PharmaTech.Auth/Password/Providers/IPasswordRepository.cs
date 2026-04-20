using PasswordModel = PharmaTech.Auth.Password.Models.Password;

namespace PharmaTech.Auth.Password.Providers;

public interface IPasswordRepository
{
    Task AddAsync(PasswordModel password);
    Task UpdateAsync(PasswordModel password);
    Task<PasswordModel?> GetByUserIdAsync(Guid userId);
}
