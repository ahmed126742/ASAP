using MediatR;

namespace CarMaintenance.Application.Features.Attachments.DeleteAttachment
{
    public sealed record DeleteAttachmentRequest(
     Guid AttachmentId,
     Guid UserId
    ) : IRequest<DeleteAttachmentResponse>;
}