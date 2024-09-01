using FluentValidation;

namespace ASAP.Application.Features.Users.GetUser
{
    public class GetUserValidator : AbstractValidator<GetUserRequest>
    {
        public GetUserValidator()
        {
            RuleFor(x => x.Id)
                .NotNull();
        }
    }
}
