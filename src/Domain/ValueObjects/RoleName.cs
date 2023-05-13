using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace ProFilePOC2.Domain.ValueObjects;

public class RoleName : ValueOf<string, RoleName>
{
    private static readonly Regex RoleNameRegex =
        new(@"^(?!\s*$).+",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

    protected override void Validate()
    {
        if (!RoleNameRegex.IsMatch(Value))
        {
            var message = $"{Value} is not a valid email address";
            throw new ValidationException(message, new[] { new ValidationFailure(nameof(Email), message) });
        }
    }
}