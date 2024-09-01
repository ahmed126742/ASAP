using ASAP.Application.Common.Models;
using ASAP.Application.Services;
using MediatR;


namespace ASAP.Application.Features.Users.GetFilteredUsers
{
    public class GetFilteresUsersHandler : IRequestHandler<PaginationRequest<GetFilteredUsersRequest, GetFilteredUsersResponse>, PagedReponse<GetFilteredUsersResponse>>
    {
        private readonly IUserService _userService;
        public GetFilteresUsersHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<PagedReponse<GetFilteredUsersResponse>> Handle(PaginationRequest<GetFilteredUsersRequest, GetFilteredUsersResponse> request, CancellationToken cancellationToken)
        {
            return await _userService.GetPagedFilteresUsers(request, cancellationToken);
        }
    }
}
