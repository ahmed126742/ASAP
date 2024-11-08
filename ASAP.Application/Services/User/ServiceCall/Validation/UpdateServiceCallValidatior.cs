using ASAP.Application.Services.User.ServiceCall.DTOs.Processing;
using FluentValidation;

namespace ASAP.Application.Services.User.ServiceCall.Validation
{
    public class UpdateServiceCallValidatior :  AbstractValidator<UpdateServiceCallRequest>
    {
        public UpdateServiceCallValidatior()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("Id must be provided!");

            RuleFor(x => x.ContractItemId)
              .NotNull()
              .WithMessage("ContractItemId must be provided!");
        }
    }
}
