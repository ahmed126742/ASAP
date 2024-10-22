using ASAP.Domain.Entities;

namespace ASAP.Application.Services.User.Survey.DTOs
{
    public class SurveyDto
    {
        public string? Location { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }

        public int? Cill { get; set; }

        public int? Horns { get; set; }

        public string? Glass { get; set; }

        public string? Extras { get; set; }

        public Guid? AttachementId { get; set; }

        public Guid? ContractItemId { get; set; }
    }
}
