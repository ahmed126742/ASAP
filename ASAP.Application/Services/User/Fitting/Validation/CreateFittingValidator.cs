using ASAP.Application.Services.User.Fitting.DTOs.Processing;
using FluentValidation;

namespace ASAP.Application.Services.User.Fitting.Validation
{
    public class CreateFittingValidator : AbstractValidator<CreateFittingRequest>
    {
        public CreateFittingValidator()
        {
            RuleFor(x => x.ContractItemId)
                 .NotNull()
                 .WithMessage("ContractItemId must be provided!" );
        }
    }
}
