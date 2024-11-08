using MediatR;

namespace CarMaintenance.Application.Features.AttachmentFeatures.GetAttachmentById;

public sealed record GetAttachmentByIdRequest(Guid Id) : IRequest<GetAttachmentByIdResponse>;