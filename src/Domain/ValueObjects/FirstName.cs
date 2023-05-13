using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace ProFilePOC2.Domain.ValueObjects;

public class FirstName : ValueOf<string, FirstName>
{
    private static readonly Regex FirstNameRegex =
        new("^[A-Za-z]{1,50}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

    protected override void Validate()
    {
        if (!FirstNameRegex.IsMatch(Value))
        {
            var message = $"{Value} is not a valid email address";
            throw new ValidationException(message, new []
            {
                new ValidationFailure(nameof(Email), message)
            });
        }
    }
}