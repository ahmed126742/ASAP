namespace ASAP.Application.Services.User.DTOs
{
    public class GetUserJobsResponse
    {
        public string JobId { get; set; }

        public string JobTypeId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? SurveyDateFrom { get; set; }

        public DateTime? SurveyDateTo { get; set; }

        public string Customer { get; set; }

        public string PostCode { get; set; }
    }
}
