namespace PharmaTech.Shared.Tests.Core;
using PharmaTech.Shared.Core;

public class UseCaseTests
{
    private sealed class TestUseCase : IUseCase<string, string>
    {
        public Task<Result<string>> ExecuteAsync(string request)
        {
            return Task.FromResult(Result<string>.Success($"Response: {request}"));
        }
    }

    [Fact]
    public async Task ExecuteAsync_Should_Return_Response()
    {
        var useCase = new TestUseCase();
        var result = await useCase.ExecuteAsync("Test");

        Assert.Equal("Response: Test", result.Data);
    }
}
