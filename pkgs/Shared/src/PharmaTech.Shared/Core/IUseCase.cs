namespace PharmaTech.Shared.Core;

public interface IUseCase<TRequest, TResponse>
{
    Task<Result<TResponse>> ExecuteAsync(TRequest request);
}
