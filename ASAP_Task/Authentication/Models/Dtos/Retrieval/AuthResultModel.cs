namespace ASAP_Task.WebAPI.Authentication.Models.Dtos.Retrieval
{
    public class AuthResultModel
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public bool Result { get; set; }

        public List<string> Errors { get; set; }
    }
}
