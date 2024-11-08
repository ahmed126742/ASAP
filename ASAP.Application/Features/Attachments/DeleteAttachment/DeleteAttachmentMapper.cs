using AutoMapper;
using ASAP.Domain.Entities;
using CarMaintenance.Application.Features.Attachments.DeleteAttachment;

namespace ASAP.Application.Features.Attachments.DeleteAttachment
{
    public sealed class DeleteAttachmentMapper : Profile
    {
        public DeleteAttachmentMapper()
        {
            CreateMap<DeleteAttachmentRequest, Attachment>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AttachmentId));
            CreateMap<Attachment, DeleteAttachmentResponse>()
                .ForMember(dest => dest.DeletedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.DeletedBy, opt => opt.MapFrom(src => src.UpdatedBy));
        }
    }
}
