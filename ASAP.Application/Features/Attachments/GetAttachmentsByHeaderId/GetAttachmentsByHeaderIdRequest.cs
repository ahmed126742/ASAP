using MediatR;

namespace CarMaintenance.Application.Features.Attachments.GetAttachmentsByHeaderId
{
    public sealed record GetAttachmentsByHeaderIdRequest(
     Guid AttachmentHeaderId

    ) : IRequest<List<GetAttachmentsByHeaderIdResponse>>;
}