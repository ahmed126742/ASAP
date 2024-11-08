namespace ASAP.Application.Services.User.ServiceCall.DTOs.Retireval
{
    public class GetServiceCallResponse : ServiceCallDto
    {
        public Guid Id { get; set; }

        public string? ReportedIssue { get; set; }

        public bool? IsCustomerHappy { get; set; }

        public bool? WasEngineerOnTime { get; set; }

        public int? WorkQualityRate { get; set; }

        public Guid? AttachementHeaderId { get; set; }
    }
}
