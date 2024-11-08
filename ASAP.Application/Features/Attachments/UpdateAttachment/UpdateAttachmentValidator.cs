using ASAP.Application.Features.Attachments.UpdateAttachment;
using FluentValidation;

namespace CarMaintenance.Application.Features.Attachments.UpdateAttachment
{
    public class UpdateAttachmentValidator : AbstractValidator<UpdateAttachmentRequest>
    {
        public UpdateAttachmentValidator()
        {
            RuleFor(x => x.AttachmentId).NotEmpty();
            RuleFor(x => x.FileUniqueName).NotEmpty();
            RuleFor(x => x.FileContentType).NotEmpty();
            RuleFor(x => x.Extension).NotEmpty();
            RuleFor(x => x.Path).NotEmpty();
            RuleFor(x => x.Size).NotEmpty();
        }
    }
}