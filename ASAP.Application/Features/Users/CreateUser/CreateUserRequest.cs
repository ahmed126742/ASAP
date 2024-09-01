using MediatR;

namespace ASAP.Application.Features.Users.CreateUser
{
    public class CreateUserRequest : IRequest<CreateUserResponse>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
