namespace ASAP.Application.Services.User.Fitting.DTOs.Processing
{
    public class CreateOutstandingFittingRequest
    {
        public string? Comment { get; set; }

        public Guid AttachementHeaderId { get; set; }

        public Guid ContractItemId { get; set; }

        public string? ReportedIssue { get; set; }
    }
}
