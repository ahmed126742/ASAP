using ASAP_Task.WebAPI.Authentication.Enums;
using ASAP_Task.WepApi.Authentication.Models.Dtos.Processing;

namespace ASAP_Task.WebAPI.Authentication.Models.Dtos.Processing
{
    public class UserRoleRegisterRequestDto : UserRegisterRequestDto
    {
        public RoleEnum? RoleType { get; set; }
    }
}
