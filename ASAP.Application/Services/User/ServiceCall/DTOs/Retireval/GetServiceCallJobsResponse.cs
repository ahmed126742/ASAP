using ASAP.Application.Services.User.DTOs;

namespace ASAP.Application.Services.User.ServiceCall.DTOs.Retireval
{
    public class GetServiceCallJobsResponse : GetUserJobsResponse
    {
        public Guid Id { get; set; }
    }
}
