using FluentValidation;

namespace ASAP.Application.Features.Users.UpdateUser
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Id)
               .NotNull();

            RuleFor(x => x.Email)
                .EmailAddress();
        }
    }
}
