using PharmaTech.Core.Base;

namespace PharmaTech.Core.ValueObjects;

public record Description : Text
{
    private const string Tag = "DESCRIPTION";
    const int DefaultMinLenght = 20;
    const int DefaultMaxLenght = 255;

    public Description(string value)
        : base(value) { }

    public static Result<Description> Create(string value)
    {
        var result = Text.Create(value, DefaultMinLenght, DefaultMaxLenght, Tag);
        return result.IsFailure ? Result<Description>.Failure(result.Errors) : new Description(value);
    }
}
