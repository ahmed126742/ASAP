using AutoMapper;
using ASAP.Domain.Entities;

namespace CarMaintenance.Application.Features.Attachments.GetAttachmentsByHeaderId
{
    public sealed class GetAttachmentsByHeaderIdMapper : Profile
    {
        public GetAttachmentsByHeaderIdMapper()
        {
            CreateMap<GetAttachmentsByHeaderIdRequest, Attachment>();

            CreateMap<Attachment, GetAttachmentsByHeaderIdResponse>();
        }
    }
}
