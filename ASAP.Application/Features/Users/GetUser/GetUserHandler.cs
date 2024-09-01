using ASAP.Application.Services;
using MediatR;

namespace ASAP.Application.Features.Users.GetUser
{
    public class GetUserHandler : IRequestHandler<GetUserRequest, GetUserRsponse>
    {
        private readonly IUserService _userService;
        public GetUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetUserRsponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
           return await _userService.GetUserAsync(request, cancellationToken);
        }
    }
}
