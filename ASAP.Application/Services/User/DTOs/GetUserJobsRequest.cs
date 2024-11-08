namespace ASAP.Application.Services.User.DTOs
{
    public class GetUserJobsRequest
    {
        public Guid? UserId { get; set; }

        public string? SearchText { get; set; }
    }
}
