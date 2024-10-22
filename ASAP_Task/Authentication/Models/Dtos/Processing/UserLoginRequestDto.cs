using System.ComponentModel.DataAnnotations;

namespace ASAP_Task.WepApi.Authentication.Models.Dtos.Processing
{
    public class UserLoginRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
