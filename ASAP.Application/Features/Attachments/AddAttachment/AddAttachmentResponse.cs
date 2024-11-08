namespace CarMaintenance.Application.Features.Attachments.AddAttachment;

public sealed record AddAttachmentResponse
{
    public Guid Id { get; set; }
    public Guid AttachmentHeaderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
}
