namespace Auth.Domain.Services;

public interface IEncrypter
{
    string Encrypt(string password);
    bool Verify(string password, string hash);
}
