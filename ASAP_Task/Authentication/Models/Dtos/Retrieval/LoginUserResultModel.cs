using ASAP_Task.Authentication.Models.Dtos.Retrieval;

namespace ASAP_Task.WebAPI.Authentication.Models.Dtos.Retrieval
{
    public class LoginUserResultModel
    {
        public AuthResultModel AuthenticationResult { get; set; }

        public UserModel? User { get; set; }
    }
}
