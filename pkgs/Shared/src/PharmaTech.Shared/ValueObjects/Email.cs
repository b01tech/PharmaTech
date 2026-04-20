using System.Net.Mail;
using PharmaTech.Shared.Core;

namespace PharmaTech.Shared.ValueObjects;

public record Email
{
    const string EMAIL_EMPTY = "Email address is empty";
    const string EMAIL_TOO_LONG = "Email address is too long";
    const string EMAIL_INVALID_FORMAT = "Email address is invalid";

    public string Address { get; init; } = string.Empty;

    private Email(string address)
    {
        Address = address;
    }

    public static Result<Email> Create(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            return Result<Email>.Failure(EMAIL_EMPTY);

        if (address.Length > 254)
            return Result<Email>.Failure(EMAIL_TOO_LONG);

        if (!IsValidFormat(address))
            return Result<Email>.Failure(EMAIL_INVALID_FORMAT);

        return new Email(address.Trim().ToLowerInvariant());
    }

    public override string ToString() => Address;

    private static bool IsValidFormat(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email.Trim();
        }
        catch
        {
            return false;
        }
    }
};
