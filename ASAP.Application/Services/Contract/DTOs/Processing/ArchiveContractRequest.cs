namespace ASAP.Application.Services.Contract.DTOs.Processing
{
    public class ArchiveContractRequest
    {
        public Guid Id { get; set; }

        public bool IsArchived { get; set; }
    }
}
