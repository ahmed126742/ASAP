using ASAP.Domain.Entities;
using AutoMapper;

namespace ASAP.Application.Features.Users.GetFilteredUsers
{
    public class GetFilteredUsersMapper : Profile
    {
        public GetFilteredUsersMapper()
        {
            CreateMap<User, GetFilteredUsersResponse>();
        }
    }
}
