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
    public List<string> RecentHashes { get; private set; } = new();
    #endregion

    #region Constructors
    // EF Core
    protected Password() { }

    private Password(string hash, Guid userId)
    {
        Hash = hash;
        UserId = userId;
    }

    public static Result<Password> Create(string hash, Guid userId)
    {
        if (string.IsNullOrEmpty(hash))
            return Result<Password>.Failure("Hash é obrigatório");
        if (userId == Guid.Empty)
            return Result<Password>.Failure("UserId é obrigatório");
        return new Password(hash, userId);
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
