using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace ProFilePOC2.Domain.ValueObjects;

public class PasswordHash : ValueOf<string, PasswordHash>
{
    private static readonly Regex PasswordHashRegex =
        new("^[a-fA-F0-9]{64}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

    protected override void Validate()
    {
        if (!PasswordHashRegex.IsMatch(Value))
        {
            var message = $"{Value} is not a valid email address";
            throw new ValidationException(message, new []
            {
                new ValidationFailure(nameof(Email), message)
            });
        }
    }
}