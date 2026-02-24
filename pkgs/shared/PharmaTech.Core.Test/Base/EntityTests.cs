using PharmaTech.Core.Base;

namespace PharmaTech.Core.Test.Base;

public class EntityTests
{
    private sealed class TestEntity : Entity { }

    [Fact]
    public void New_entity_should_have_non_empty_id_and_created_at_set()
    {
        var entity = new TestEntity();

        Assert.NotEqual(Guid.Empty, entity.Id);
        Assert.True(entity.CreatedAt <= DateTime.UtcNow);
        Assert.Null(entity.UpdatedAt);
        Assert.Null(entity.DeletedAt);
    }
}
