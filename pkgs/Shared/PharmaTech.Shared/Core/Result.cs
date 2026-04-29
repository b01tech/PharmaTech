namespace PharmaTech.Shared.Core;

public class Result
{
    #region Properties
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public IList<string> Errors { get; }
    #endregion

    #region Constructors
    protected Result(bool isSuccess, IList<string> errors)
    {
        IsSuccess = isSuccess;
        Errors = new List<string>(errors);
    }

    public static Result Success() => new Result(true, []);

    public static Result Failure(IList<string> errors) => new Result(false, errors);

    public static Result Failure(string error) => new Result(false, [error]);
    #endregion

    #region Methods
    public static IList<string> MergeErrors(params Result[] results) => results.SelectMany(r => r.Errors).ToList();
    #endregion
}

public class Result<T> : Result
{
    #region Properties
    public T Data { get; } = default!;
    #endregion

    #region Constructors
    private Result(bool isSuccess, IList<string> errors, T data)
        : base(isSuccess, errors)
    {
        Data = data;
    }

    public static Result<T> Success(T data) => new Result<T>(true, [], data);

    public static new Result<T> Failure(IList<string> errors) => new(false, errors, default!);

    public static new Result<T> Failure(string error) => new(false, [error], default!);
    #endregion

    #region Operators
    public static implicit operator Result<T>(T data) => Success(data);
    #endregion
}
