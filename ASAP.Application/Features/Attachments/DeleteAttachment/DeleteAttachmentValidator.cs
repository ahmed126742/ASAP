using FluentValidation;

namespace CarMaintenance.Application.Features.Attachments.DeleteAttachment
{
    public class DeleteAttachmentValidator : AbstractValidator<DeleteAttachmentRequest>
    {
        public DeleteAttachmentValidator()
        {
            RuleFor(x => x.AttachmentId).NotEmpty();
        }
    }
}