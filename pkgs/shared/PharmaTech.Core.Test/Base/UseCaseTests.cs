using PharmaTech.Core.Base;

namespace PharmaTech.Core.Test.Base;

public class UseCaseTests
{
    private sealed class EchoUseCase : IUseCase<string, string>
    {
        public Task<string> ExecuteAsync(string request)
        {
            return Task.FromResult(request);
        }
    }

    [Fact]
    public async Task ExecuteAsync_should_return_expected_result()
    {
        var useCase = new EchoUseCase();

        var result = await useCase.ExecuteAsync("request");

        Assert.Equal("request", result);
    }
}
