namespace ASAP.Application.Features.Attachments.UpdateAttachment
{
    public sealed record UpdateAttachmentResponse
    {
        public Guid Id { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
