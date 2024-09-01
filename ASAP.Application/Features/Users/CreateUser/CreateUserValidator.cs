using FluentValidation;

namespace ASAP.Application.Features.Users.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email must be provided!");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Email must be email fromate!");

        }
    }
}
