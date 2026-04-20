using PharmaTech.Shared.Core;

namespace PharmaTech.Shared.ValueObjects;

public record Name : Text
{
    private const string Tag = "NAME";
    const int DefaultMinLenght = 3;
    const int DefaultMaxLenght = 50;

    private Name(string value)
        : base(value) { }

    public static Result<Name> Create(string value)
    {
        var result = Text.Create(value, DefaultMinLenght, DefaultMaxLenght, Tag);
        return result.IsFailure ? Result<Name>.Failure(result.Errors) : new Name(result.Data.Value);
    }

    public override string ToString() => Value;
};
