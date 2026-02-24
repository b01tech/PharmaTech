using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using PharmaTech.Core.Base;

namespace PharmaTech.Core.ValueObjects;

public record Alias
{
    private const string InvalidAlias = "INVALID_ALIAS";
    public string Value { get; }

    private Alias(string value)
    {
        Value = value;
    }

    public static Result<Alias> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<Alias>.Failure(InvalidAlias);
        var result = NormalizeText(value);
        return new Alias(result);
    }

    private static string NormalizeText(string text)
    {
        var normalized = text.Normalize(NormalizationForm.FormD);

        var sb = new StringBuilder();

        foreach (var c in normalized)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(c);
            }
        }

        var cleaned = sb.ToString().Normalize(NormalizationForm.FormC).ToLowerInvariant();

        cleaned = Regex.Replace(cleaned, @"\s+", "-");
        cleaned = Regex.Replace(cleaned, @"[^a-z0-9-]", "");
        cleaned = Regex.Replace(cleaned, @"-+", "-");
        cleaned = cleaned.Trim('-');

        return cleaned;
    }

    public override string ToString() => Value;
};
