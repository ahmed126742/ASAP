using ASAP.Application.Common.Models;
using ASAP.Application.Features.Users.CreateUser;
using ASAP.Application.Features.Users.DeleteUser;
using ASAP.Application.Features.Users.GetFilteredUsers;
using ASAP.Application.Features.Users.GetUser;
using ASAP.Application.Features.Users.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace  ASAP_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetUser")]
        public async Task<ActionResult<GetUserRsponse>> GetUser(GetUserRequest request, CancellationToken cancellationToken)
        {

            return Ok(await _mediator.Send(request, cancellationToken));
        }

        [HttpPost("GetPagedFilteredUsers")]
        public async Task<ActionResult<PagedReponse<GetFilteredUsersResponse>>> GetPagedFilteredUsers(PaginationRequest<GetFilteredUsersRequest, GetFilteredUsersResponse> request, CancellationToken cancellationToken)
        {

            return Ok(await _mediator.Send(request, cancellationToken));
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<CreateUserResponse>> CreateUser(CreateUserRequest request, CancellationToken cancellationToken)
        {

            return Ok(await _mediator.Send(request, cancellationToken));
        }

        [HttpPost("UpdateUser")]
        public async Task<ActionResult> UpdateUser(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            return Ok();
        }

        [HttpPost("DeleteUser")]
        public async Task<ActionResult> DeleteUser(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            return Ok();
        }
    }
}