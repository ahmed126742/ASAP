using System.ComponentModel.DataAnnotations;

namespace ASAP_Task.WepApi.Authentication.Models.Dtos.Processing
{
    public class UserRegisterRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }


        public string? PhoneNumber { get; set; }

    }
}
