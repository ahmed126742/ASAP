using MediatR;

namespace CarMaintenance.Application.Features.Attachments.AddAttachment
{
    public sealed record AddAttachmentRequest(
     Guid? AttachmentHeaderId,
     string FileUniqueName,
     string FileUniqueNameEn,
     string? Description,
     string? DescriptionEn,
     string Path,
     string FileContentType,
     string Extension,
     string Size,
     Guid UserId
    ) : IRequest<AddAttachmentResponse>;
}