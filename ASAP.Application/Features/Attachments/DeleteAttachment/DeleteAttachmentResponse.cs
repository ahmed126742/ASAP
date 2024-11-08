namespace CarMaintenance.Application.Features.Attachments.DeleteAttachment
{
    public sealed record DeleteAttachmentResponse
    {
        public Guid Id { get; set; }
        public DateTime DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
    }
}
