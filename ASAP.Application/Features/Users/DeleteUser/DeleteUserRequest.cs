using MediatR;

namespace ASAP.Application.Features.Users.DeleteUser
{
    public class DeleteUserRequest : IRequest<DeleteUserRepons>
    {
        public Guid Id { get; set; }
    }
}
