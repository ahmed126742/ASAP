using ASAP.Application.Services;
using MediatR;

namespace ASAP.Application.Features.Users.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UpdateUserResponse>
    {
        private readonly IUserService _userService;
        public UpdateUserHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateUserAsync(request, cancellationToken);
        }
    }
}
