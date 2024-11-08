using AutoMapper;
using ASAP.Domain.Entities;

namespace CarMaintenance.Application.Features.Attachments.AddAttachment
{
    public sealed class AddAttachmentMapper : Profile
    {
        public AddAttachmentMapper()
        {
            CreateMap<AddAttachmentRequest, Attachment>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FileUniqueName))
            .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.FileUniqueNameEn))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DescriptionEn, opt => opt.MapFrom(src => src.DescriptionEn))
            .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Path))
            .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.FileContentType))
            .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => src.Extension))
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size));

            CreateMap<Attachment, AddAttachmentResponse>();
        }
    }
}
