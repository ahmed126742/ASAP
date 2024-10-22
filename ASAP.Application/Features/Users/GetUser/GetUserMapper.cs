using ASAP.Domain.Entities;
using AutoMapper;

namespace ASAP.Application.Features.Users.GetUser
{
    public class GetUserMapper : Profile
    {
        public GetUserMapper()
        {
            CreateMap<User, GetUserRsponse>();
        }
    }
}
