namespace ASAP.Application.Services.User.Survey.DTOs.Retrieval
{
    public class GetSurveysByContractItemResponse
    {
        public GetSurveysByContractItemResponse()
        {
            Surveys = new List<GetSurveyResponse>();
        }
        public DateTime? SurveyDateFrom { get; set; }

        public DateTime? SurveyDateTo { get; set; }

        public Guid? SurveyorId { get; set; }

        public string? SurveyorName { get; set; }


        public string? PostalCode { get; set; }

        public IList<GetSurveyResponse> Surveys { get; set; }
    }
}
