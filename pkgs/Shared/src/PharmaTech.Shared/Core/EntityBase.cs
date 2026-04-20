namespace PharmaTech.Shared.Core;

public abstract class EntityBase
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    // EF Core
    protected EntityBase()
    {
    }

    protected void SetUpdatedAt() => UpdatedAt = DateTime.UtcNow;
    protected void SetDeletedAt() => DeletedAt = DateTime.UtcNow;

    override public bool Equals(object? obj) => obj is EntityBase entity && Id == entity.Id;
    override public int GetHashCode() => Id.GetHashCode();
}
