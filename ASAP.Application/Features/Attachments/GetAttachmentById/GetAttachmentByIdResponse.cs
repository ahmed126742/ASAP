namespace CarMaintenance.Application.Features.AttachmentFeatures.GetAttachmentById;

public sealed record GetAttachmentByIdResponse
{
    public Guid Id { get; set; }
    public Guid? AttachmentHeaderId { get; set; }
    public string? Path { get; set; }
    public string? ContentType { get; set; }
    public string? Name { get; set; }
    public string? NameEn { get; set; }
    public string? Description { get; set; }
    public string? DescriptionEn { get; set; }
    public int Size { get; set; }
    public string? Extension { get; set; }
}
