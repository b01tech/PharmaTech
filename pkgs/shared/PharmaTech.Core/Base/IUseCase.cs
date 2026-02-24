namespace PharmaTech.Core.Base;

public interface IUseCase<TIn, TOut>
{
    Task<TOut> ExecuteAsync(TIn request);
}
