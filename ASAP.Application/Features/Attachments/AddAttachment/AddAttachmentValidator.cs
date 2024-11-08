using FluentValidation;

namespace CarMaintenance.Application.Features.Attachments.AddAttachment
{
    public class AddAttachmentValidator : AbstractValidator<AddAttachmentRequest>
    {
        public AddAttachmentValidator()
        {
            RuleFor(x => x.FileUniqueName).NotEmpty();
            RuleFor(x => x.FileContentType).NotEmpty();
            RuleFor(x => x.Extension).NotEmpty();
            RuleFor(x => x.Path).NotEmpty();
            RuleFor(x => x.Size).NotEmpty();
        }
    }
}