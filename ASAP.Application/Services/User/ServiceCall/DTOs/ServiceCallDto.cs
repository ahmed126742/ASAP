namespace ASAP.Application.Services.User.ServiceCall.DTOs
{
    public class ServiceCallDto
    {
        public Guid? UserId { get; set; }

        public DateTime? RequiredDate { get; set; }

        public string? ReportedIssue { get; set; }

        public string? WorkDescription { get; set; }

        public string? PartsRequired { get; set; }
    }
}
