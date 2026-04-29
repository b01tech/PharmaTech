using PharmaTech.Shared.Core;

namespace PharmaTech.Shared.ValueObjects;

public record Text : ValueObject
{
    #region Constants
    private const string TooShort = "TOO_SHORT";
    private const string TooLong = "TOO_LONG";
    const int DefaultMinLenght = 1;
    const int DefaultMaxLenght = int.MaxValue;
    #endregion

    #region Properties
    public string Value { get; }
    #endregion

    #region Constructor
    protected Text(string value)
    {
        Value = value;
    }

    public static Result<Text> Create(string value, int? min = null, int? max = null, string tag = "TEXT")
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<Text>.Failure($"{tag}_{TooShort}");
        value = value.Trim();
        if (value.Length < (min ?? DefaultMinLenght))
            return Result<Text>.Failure($"{tag}_{TooShort}");
        if (value.Length > (max ?? DefaultMaxLenght))
            return Result<Text>.Failure($"{tag}_{TooLong}");

        return new Text(value);
    }
    #endregion

    #region Methods
    public override string ToString() => Value;
    #endregion
}
