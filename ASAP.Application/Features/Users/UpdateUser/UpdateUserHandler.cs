using ASAP.Application.Services;
using MediatR;

namespace ASAP.Application.Features.Users.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest>
    {
        private readonly IUserService _userService;
        public UpdateUserHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            await _userService.UpdateUserAsync(request, cancellationToken);
        }
    }
}
