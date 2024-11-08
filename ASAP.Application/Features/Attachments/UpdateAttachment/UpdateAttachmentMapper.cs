using ASAP.Domain.Entities;
using AutoMapper;

namespace ASAP.Application.Features.Attachments.UpdateAttachment;

public sealed class UpdateAttachmentMapper : Profile
{
    public UpdateAttachmentMapper()
    {
        CreateMap<UpdateAttachmentRequest, Attachment>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FileUniqueName))
            .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.FileUniqueNameEn))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AttachmentId))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DescriptionEn, opt => opt.MapFrom(src => src.DescriptionEn))
            .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Path))
            .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.FileContentType))
            .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => src.Extension))
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
            .ForMember(dest => dest.AttachmentHeaderId, opt => opt.Condition((src, dest, srcMember) => srcMember.HasValue)); // ignore null value in request for attachmentHeaderId

        CreateMap<Attachment, UpdateAttachmentResponse>();
    }
}
