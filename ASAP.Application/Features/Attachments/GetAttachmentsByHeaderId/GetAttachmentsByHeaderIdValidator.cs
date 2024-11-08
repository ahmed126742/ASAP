using FluentValidation;

namespace CarMaintenance.Application.Features.Attachments.GetAttachmentsByHeaderId
{
    public class GetAttachmentsByHeaderIdValidator : AbstractValidator<GetAttachmentsByHeaderIdRequest>
    {
        public GetAttachmentsByHeaderIdValidator()
        {
            RuleFor(x => x.AttachmentHeaderId).NotEmpty();
        }
    }
}