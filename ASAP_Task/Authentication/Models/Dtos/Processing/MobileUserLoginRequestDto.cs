using System.ComponentModel.DataAnnotations;

namespace ASAP_Task.WebAPI.Authentication.Models.Dtos.Processing
{
    public class MobileUserLoginRequestDto
    {
        [Required]
        public string MobileNumber { get; set; }

    }
}
