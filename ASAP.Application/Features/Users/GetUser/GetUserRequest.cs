using MediatR;

namespace ASAP.Application.Features.Users.GetUser
{
    public class GetUserRequest : IRequest<GetUserRsponse>
    {
        public Guid Id  { get; set; }
    }
}
