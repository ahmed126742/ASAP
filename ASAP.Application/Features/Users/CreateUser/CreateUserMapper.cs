using ASAP.Domain.Entities;
using AutoMapper;

namespace ASAP.Application.Features.Users.CreateUser
{
    public class CreateUserMapper : Profile
    {
        public CreateUserMapper()
        {
            CreateMap<CreateUserRequest, User>();
        }
    }
}
