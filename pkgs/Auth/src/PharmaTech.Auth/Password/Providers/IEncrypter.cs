namespace PharmaTech.Auth.Password.Providers;

public interface IEncrypter
{
    string Encrypt(string password);
    bool Verify(string password, string hash);
}
