using AutoMapper;
using ASAP.Domain.Entities;
using CarMaintenance.Application.Features.AttachmentFeatures.GetAttachmentById;

namespace ASAP.Application.Features.AttachmentFeatures.GetAttachmentById;

public sealed class GetAttachmentByIdeMapper : Profile
{
    public GetAttachmentByIdeMapper()
    {
        CreateMap<Attachment, GetAttachmentByIdResponse>();
    }
}