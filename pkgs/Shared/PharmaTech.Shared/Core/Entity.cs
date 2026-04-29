namespace PharmaTech.Shared.Core;

public abstract class Entity
{
    #region Properties
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    #endregion

    #region Constructor
    // EF Core
    protected Entity() { }
    #endregion

    #region Methods
    protected void SetUpdatedAt() => UpdatedAt = DateTime.UtcNow;

    protected void SetDeletedAt() => DeletedAt = DateTime.UtcNow;

    public override bool Equals(object? obj) => obj is Entity entity && Id == entity.Id;

    public override int GetHashCode() => Id.GetHashCode();
    #endregion
}
