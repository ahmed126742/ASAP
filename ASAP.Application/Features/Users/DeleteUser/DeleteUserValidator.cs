using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace ASAP.Application.Features.Users.DeleteUser
{
    public class DeleteUserValidator :  AbstractValidator<DeleteUserRequest>
    {
        public DeleteUserValidator()
        {
            RuleFor(x => x.Id)
                .NotNull();
        }
    }
}
