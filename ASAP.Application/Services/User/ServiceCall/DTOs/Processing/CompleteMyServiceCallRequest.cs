namespace ASAP.Application.Services.User.ServiceCall.DTOs.Processing
{
    public class CompleteMyServiceCallRequest  : ServiceCallIdentityDto
    {
        public bool? IsCustomerHappy { get; set; }

        public bool? WasEngineerOnTime { get; set; }

        public int? WorkQualityRate { get; set; }

        public Guid? AttachementHeaderId { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerPhoneNumber { get; set; }
    }
}
