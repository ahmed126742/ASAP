using ASAP.Application.Services;
using MediatR;

namespace ASAP.Application.Features.Users.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        private readonly IUserService _userService;

        public CreateUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            return await _userService.CreateUserAsync(request, cancellationToken);
        }
    }
}
