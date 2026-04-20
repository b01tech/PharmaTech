using PharmaTech.Shared.Core;

namespace PharmaTech.Auth.Password.Models;

public class Password : EntityBase
{
    const int MaxRecentHashes = 5;
    public string Hash { get; private set; } = string.Empty;
    public Guid UserId { get; private set; }
    public List<string> RecentHashes { get; private set; } = new List<string>();

    // EF Core
    protected Password() { }

    public Password(string hash, Guid userId)
    {
        Hash = hash;
        UserId = userId;
    }

    public void AddRecentHash(string hash)
    {
        if (RecentHashes.Count >= MaxRecentHashes)
            RecentHashes.RemoveAt(0);
        RecentHashes.Add(hash);
    }
}
