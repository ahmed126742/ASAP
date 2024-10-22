using System.ComponentModel.DataAnnotations;

namespace ASAP_Task.WepApi.Authentication.Models.Dtos.Processing
{
    public class ValidateUserDate
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
