using Auth.Domain.Entities;

namespace Auth.Domain.Test.Entities;

public class PasswordTests
{
    private static readonly Guid UserId = Guid.CreateVersion7();

    [Fact]
    public void Create_Should_Set_Hash_And_UserId()
    {
        var password = Password.Create("hashed_value", UserId);

        Assert.Equal("hashed_value", password.Data.Hash);
        Assert.Equal(UserId, password.Data.UserId);
    }

    [Fact]
    public void AddRecentHash_Should_Add_To_List()
    {
        var passwordResult = Password.Create("hash1", UserId);
        var password = passwordResult.Data;
        password.AddRecentHash("hash1");

        Assert.Single(password.RecentHashes);
        Assert.Contains("hash1", password.RecentHashes);
    }

    [Fact]
    public void AddRecentHash_Should_Remove_Oldest_When_Full()
    {
        var passwordResult = Password.Create("hash1", UserId);
        var password = passwordResult.Data;
        password.AddRecentHash("hash2");
        password.AddRecentHash("hash3");
        password.AddRecentHash("hash4");
        password.AddRecentHash("hash5");
        password.AddRecentHash("hash6");

        Assert.Equal(5, password.RecentHashes.Count);
        Assert.DoesNotContain("hash1", password.RecentHashes);
    }

    [Fact]
    public void RecentHashes_Should_Start_Empty()
    {
        var passwordResult = Password.Create("hash", UserId);
        var password = passwordResult.Data;

        Assert.Empty(password.RecentHashes);
    }

    [Fact]
    public void AddRecentHash_Should_Maintain_Order()
    {
        var passwordResult = Password.Create("hash0", UserId);
        var password = passwordResult.Data;
        password.AddRecentHash("hash1");
        password.AddRecentHash("hash2");

        Assert.Equal("hash1", password.RecentHashes[0]);
        Assert.Equal("hash2", password.RecentHashes[1]);
    }
}
