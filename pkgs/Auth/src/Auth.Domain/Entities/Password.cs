using PharmaTech.Shared.Core;

namespace Auth.Domain.Entities;

public class Password : Entity
{
    #region Constants
    const int MaxRecentHashes = 5;
    #endregion

    #region Properties
    public string Hash { get; private set; } = string.Empty;
    public Guid UserId { get; private set; }
    public List<string> RecentHashes { get; private set; } = new List<string>();
    #endregion

    #region Constructors
    // EF Core
    protected Password() { }

    public Password(string hash, Guid userId)
    {
        Hash = hash;
        UserId = userId;
    }
    #endregion

    #region Methods
    public void AddRecentHash(string hash)
    {
        if (RecentHashes.Count >= MaxRecentHashes)
            RecentHashes.RemoveAt(0);
        RecentHashes.Add(hash);
    }
    #endregion
}
