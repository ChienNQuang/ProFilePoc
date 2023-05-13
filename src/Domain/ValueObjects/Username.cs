using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace ProFilePOC2.Domain.ValueObjects;

public class Username : ValueOf<string, Username>
{
    private static readonly Regex UsernameRegex =
        new("^[A-Za-z0-9_]{4,20}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

    protected override void Validate()
    {
        if (!UsernameRegex.IsMatch(Value))
        {
            var message = $"{Value} is not a valid email address";
            throw new ValidationException(message, new []
            {
                new ValidationFailure(nameof(Email), message)
            });
        }
    }
}