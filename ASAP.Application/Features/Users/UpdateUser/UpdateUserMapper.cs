using ASAP.Domain.Entities;
using AutoMapper;

namespace ASAP.Application.Features.Users.UpdateUser
{
    public class UpdateUserMapper : Profile
    {
        public UpdateUserMapper()
        {
            CreateMap<UpdateUserRequest, Client>();
        }
    }
}
