using MediatR;

namespace ASAP.Application.Features.Users.DeleteUser
{
    public class DeleteUserRequest : IRequest
    {
        public Guid Id { get; set; }
    }
}
