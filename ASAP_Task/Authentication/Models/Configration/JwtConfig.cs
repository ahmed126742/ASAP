namespace ASAP_Task.WebAPI.Authentication.Models.Configration
{
    public class JwtConfig
    {
        public string Secret { get; set; }

        public TimeSpan ExpiryTimeFrame { get; set; }
    }
}
