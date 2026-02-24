using PharmaTech.Core.Base;

namespace PharmaTech.Core.ValueObjects;

public record Text
{
    private const string TooShort = "TOO_SHORT";
    private const string TooLong = "TOO_LONG";
    const int DefaultMinLenght = 1;
    const int DefaultMaxLenght = int.MaxValue;

    public string Value { get; }

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

    public override string ToString() => Value;
}
