using MediatR;

namespace ASAP.Application.Features.Attachments.UpdateAttachment
{
    public sealed record UpdateAttachmentRequest(
     Guid AttachmentId,
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
    ) : IRequest<UpdateAttachmentResponse>;
}