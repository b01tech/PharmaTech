using PharmaTech.Shared.Core;
using PasswordModel = PharmaTech.Auth.Password.Models.Password;

namespace PharmaTech.Auth.Tests.Models;

public class PasswordTests
{
    private static Guid UserId = Guid.NewGuid();

    [Fact]
    public void Constructor_Should_Set_Hash_And_UserId()
    {
        var password = new PasswordModel("hashed_value", UserId);

        Assert.Equal("hashed_value", password.Hash);
        Assert.Equal(UserId, password.UserId);
    }

    [Fact]
    public void AddRecentHash_Should_Add_To_List()
    {
        var password = new PasswordModel("hash1", UserId);
        password.AddRecentHash("hash1");

        Assert.Single(password.RecentHashes);
        Assert.Contains("hash1", password.RecentHashes);
    }

    [Fact]
    public void AddRecentHash_Should_Remove_Oldest_When_Full()
    {
        var password = new PasswordModel("hash1", UserId);
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
        var password = new PasswordModel("hash", UserId);

        Assert.Empty(password.RecentHashes);
    }

    [Fact]
    public void AddRecentHash_Should_Maintain_Order()
    {
        var password = new PasswordModel("hash0", UserId);
        password.AddRecentHash("hash1");
        password.AddRecentHash("hash2");

        Assert.Equal("hash1", password.RecentHashes[0]);
        Assert.Equal("hash2", password.RecentHashes[1]);
    }
}