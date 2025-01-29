namespace ASAP.Application.Services.User.Survey.DTOs.Retrieval
{
    public class GetSurveysReponse : SurveyDto
    {
        public Guid? Id { get; set; }

        public DateTime? SurveyDateFrom { get; set; }

        public DateTime? SurveyDateTo { get; set; }

        public Guid? SurveyorId { get; set; }

        public string? SurveyorName { get; set; }

        public string? PostalCode { get; set; }
    }
}
