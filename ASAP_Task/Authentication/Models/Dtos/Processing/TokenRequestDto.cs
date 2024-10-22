using System.ComponentModel.DataAnnotations;

namespace ASAP_Task.WebAPI.Authentication.Models.Dtos.Processing
{
    public class TokenRequestDto
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
