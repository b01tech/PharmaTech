using System.Text.RegularExpressions;
using PharmaTech.Core.Base;

namespace PharmaTech.Core.ValueObjects;

public record Sku
{
    private const string InvalidSku = "INVALID_SKU";
    private const int MaxLenght = 50;

    public string Value { get; }

    // EF Constructor
    protected Sku() { }

    private Sku(string value)
    {
        Value = value;
    }

    public static Result<Sku> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<Sku>.Failure(InvalidSku);
        var sku = value.Trim();
        if (sku.Length > MaxLenght || !Regex.IsMatch(sku, @"^[A-Za-z0-9-]"))
            return Result<Sku>.Failure(InvalidSku);
        return new Sku(sku.ToUpper());
    }
}
