using FluentValidation;

namespace ProFilePOC2.Application.Accounts.Commands.CreateAccount;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email must not be empty")
            .EmailAddress().WithMessage("Must provide valid email address");
        RuleFor(c => c.CreatorId)
            .NotEmpty().WithMessage("Creator id must not be empty");
    }
}