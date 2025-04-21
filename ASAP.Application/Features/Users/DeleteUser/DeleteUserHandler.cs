using ASAP.Application.Services;
using MediatR;

namespace ASAP.Application.Features.Users.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, DeleteUserRepons>
    {
        private readonly IUserService _userService;

        public DeleteUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<DeleteUserRepons> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
             return  await _userService.DeleteUserAsync(request, cancellationToken);
        }
    }
}
