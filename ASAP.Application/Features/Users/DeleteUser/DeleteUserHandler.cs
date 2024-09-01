using ASAP.Application.Services;
using MediatR;

namespace ASAP.Application.Features.Users.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest>
    {
        private readonly IUserService _userService;

        public DeleteUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
             await _userService.DeleteUserAsync(request, cancellationToken);
        }
    }
}
