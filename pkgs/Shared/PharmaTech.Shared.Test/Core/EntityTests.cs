using PharmaTech.Shared.Core;

namespace PharmaTech.Shared.Test.Core;

public class EntityTests
{
    private sealed class TestEntity : Entity
    {
        public void TestSetUpdatedAt() => SetUpdatedAt();

        public void TestSetDeletedAt() => SetDeletedAt();
    }

    [Fact]
    public void Constructor_Should_Set_Id_And_CreatedAt()
    {
        var entity = new TestEntity();

        Assert.NotEqual(Guid.Empty, entity.Id);
        Assert.True(entity.CreatedAt <= DateTime.UtcNow);
        Assert.Null(entity.UpdatedAt);
        Assert.Null(entity.DeletedAt);
    }

    [Fact]
    public void SetUpdatedAt_Should_Set_UpdatedAt()
    {
        var entity = new TestEntity();

        entity.TestSetUpdatedAt();

        Assert.NotNull(entity.UpdatedAt);
    }

    [Fact]
    public void SetDeletedAt_Should_Set_DeletedAt()
    {
        var entity = new TestEntity();

        entity.TestSetDeletedAt();

        Assert.NotNull(entity.DeletedAt);
    }

    [Fact]
    public void Equals_Should_Return_False_When_Ids_Are_Different()
    {
        var entity1 = new TestEntity();
        var entity2 = new TestEntity();

        Assert.False(entity1.Equals(entity2));
    }

    [Fact]
    public void GetHashCode_Should_Return_Same_Value_When_Ids_Are_Equal()
    {
        var entity = new TestEntity();

        Assert.Equal(entity.GetHashCode(), entity.Id.GetHashCode());
    }
}
